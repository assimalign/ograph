import * as vscode from 'vscode'
import { getNonce } from '../../utils'


export class OGraphQueryEditorProvider implements vscode.CustomTextEditorProvider {

    //#region Static Members
    private static readonly viewType = 'ograph.editor'

    public static register(context: vscode.ExtensionContext): vscode.Disposable {
        const provider = new OGraphQueryEditorProvider(context);
        const providerRegistration = vscode.window.registerCustomEditorProvider(OGraphQueryEditorProvider.viewType, provider);
        return providerRegistration;
    }
    //#endregion

    //#region Constructor

    private content: string = ''

    constructor(
        private readonly context: vscode.ExtensionContext
    ) { }
    //#endregion


    resolveCustomTextEditor(
        document: vscode.TextDocument,
        panel: vscode.WebviewPanel,
        token: vscode.CancellationToken): Thenable<void> | void {

        let isUpdateFromWebview = false;
        let isBuffer = false;

        this.content = document.getText();

        // Setup initial content for the webview
        panel.webview.options = {
            enableScripts: true,
            localResourceRoots: [
                vscode.Uri.joinPath(this.context.extensionUri, 'resources'),
                vscode.Uri.joinPath(this.context.extensionUri, 'client', 'out'),
            ]
        };

        panel.webview.html = this.getHtmlForWebview(
            panel.webview, 
            this.context.extensionUri, 
            this.content);

        const updateWebview = (msgType: string) => {
            if (panel.visible) {
                panel.webview.postMessage({
                    type: msgType,
                    text: this.content,
                })
                    .then((success) => {
                        if (success) {
                            // ...
                        }
                    }, (reason) => {
                        // If the editor is closed and the changes are not being saved the text editor does an undo,
                        // which will trigger this function and try to send a message to the destroyed webview.
                        if (!document.isClosed) {
                            console.error('Json Editor', reason);
                        }
                    });
            }
        }

        // Receive message from the webview
        panel.webview.onDidReceiveMessage(e => {
            switch (e.type) {
                case OGraphQueryEditorProvider.viewType + '.updateFromWebview': {
                    isUpdateFromWebview = true;
                    this.writeChangesToDocument(document, e.content);
                    break;
                }
            }
        });

        /**
         * When changes are made inside the webview a message to the extension will be sent with the new data.
         * This will also change the model (= document). If the model is changed the onDidChangeTextDocument event
         * will trigger and the SAME data would be sent back to the webview.
         * To prevent this we check from where the changes came from (webview or somewhere else).
         * If the changes are made inside the webview (this.isUpdateFromWebview === true) then we will send NO data
         * to the webview. For example if the changes are made inside a separate editor then the data will be sent to
         * the webview to synchronize it with the current content of the model.
         */
        const changeDocumentSubscription = vscode.workspace.onDidChangeTextDocument(e => {
            if (e.document.uri.toString() === document.uri.toString() && e.contentChanges.length !== 0) {

                this.content = e.document.getText();

                // If the webview is in the background then no messages can be sent to it.
                // So we have to remember that we need to update its content the next time the webview regain its focus.
                if (!panel.visible) {
                    isBuffer = true;
                    return;
                }

                // Update the webviews content.
                switch (e.reason) {
                    case 1: {   // Undo
                        updateWebview(OGraphQueryEditorProvider.viewType + '.undo');
                        break;
                    }
                    case 2: {   // Redo
                        updateWebview(OGraphQueryEditorProvider.viewType + '.redo');
                        break;
                    }
                    case undefined: {
                        // If the initial update came from the webview then we don't need to update the webview.
                        if (!isUpdateFromWebview) {
                            updateWebview(OGraphQueryEditorProvider.viewType + '.updateFromExtension');
                        }
                        isUpdateFromWebview = false;
                        break;
                    }
                }
            }
        });

        // Called when the view state changes (e.g. user switch the tab)
        panel.onDidChangeViewState(() => {
            switch (true) {
                case panel.active: {
                    this.content = document.getText();
                    /* falls through */
                }
                case panel.visible: {
                    // If changes has been made while the webview was not visible no messages could have been sent to the
                    // webview. So we have to update the webview if it gets its focus back.
                    if (isBuffer) {
                        updateWebview(OGraphQueryEditorProvider.viewType + '.updateFromExtension');
                        isBuffer = false;
                    }
                }
            }
        });

        // Cleanup after editor was closed.
        panel.onDidDispose(() => {
            changeDocumentSubscription.dispose();
        });
    }


    private getHtmlForWebview(webview: vscode.Webview, extensionUri: vscode.Uri, initialContent: string): string {
        const vueAppUri = webview.asWebviewUri(vscode.Uri.joinPath(
            extensionUri, 'client',  'out', 'views', 'editor.mjs'
        ));

        const styleResetUri = webview.asWebviewUri(vscode.Uri.joinPath(
            extensionUri, 'resources', 'css', 'reset.css'
        ));

        const styleAppUri = webview.asWebviewUri(vscode.Uri.joinPath(
            extensionUri, 'client', 'out', 'views', 'editor.css'
        ));

        const nonce = getNonce();

        return `
            <!DOCTYPE html>
            <html lang="en">
            <head>
                <meta charset="utf-8" />

                <meta http-equiv="Content-Security-Policy" content="default-src 'none';
                    style-src ${webview.cspSource};
                    img-src ${webview.cspSource};
                    script-src 'nonce-${nonce}';">

                <meta name="viewport" content="width=device-width, initial-scale=1.0">

                <link href="${styleResetUri}" rel="stylesheet" />
                <link href="${styleAppUri}" rel="stylesheet" />

                <title>OGraph Editor</title>
            </head>
            <body>
                <div id="app"></div>
                <script nonce="${nonce}">
                    // Store the VsCodeAPI in a global variable
                    const vscode = acquireVsCodeApi();
                    // Set the initial state of the webview
                    vscode.setState({
                        viewType: '${OGraphQueryEditorProvider.viewType}',
                        text: '${initialContent}'
                    });
                </script>
                <script type="text/javascript" src="${vueAppUri}" nonce="${nonce}"></script>
            </body>
            </html>
        `;
    }

    private writeChangesToDocument(document: vscode.TextDocument, content: string): Thenable<boolean> {
        const edit = new vscode.WorkspaceEdit();
        const text = content

        edit.replace(
            document.uri,
            new vscode.Range(0, 0, document.lineCount, 0),
            text
        );

        return vscode.workspace.applyEdit(edit)
            .then((success) => {
                if (success) {
                    this.content = content;
                }
                return success
            });
    }
}
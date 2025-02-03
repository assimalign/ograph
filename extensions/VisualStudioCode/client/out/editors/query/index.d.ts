import * as vscode from 'vscode';
export declare class OGraphQueryEditorProvider implements vscode.CustomTextEditorProvider {
    private readonly context;
    private static readonly viewType;
    static register(context: vscode.ExtensionContext): vscode.Disposable;
    private content;
    constructor(context: vscode.ExtensionContext);
    resolveCustomTextEditor(document: vscode.TextDocument, panel: vscode.WebviewPanel, token: vscode.CancellationToken): Thenable<void> | void;
    private getHtmlForWebview;
    private writeChangesToDocument;
}

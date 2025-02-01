"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.deactivate = exports.activate = void 0;
const path = require("path");
const vscode_1 = require("vscode");
const node_1 = require("vscode-languageclient/node");
const editors_1 = require("./editors");
let client;
const initClient = (context) => {
    // The server is implemented in node
    const module = context.asAbsolutePath(path.join('server', 'out', 'server.js'));
    // Create the language client and start the client.
    client = new node_1.LanguageClient('ographLanguageServer', 'OGraph Language Server', 
    // If the extension is launched in debug mode then the debug server options are used
    // Otherwise the run options are used
    {
        run: {
            module: module,
            transport: node_1.TransportKind.ipc
        },
        debug: {
            module: module,
            transport: node_1.TransportKind.ipc,
        }
    }, {
        // Register the server for plain text documents
        documentSelector: [
            {
                scheme: 'file',
                language: 'ograph'
            }
        ],
        synchronize: {
            // Notify the server about file changes to '.ograph files contained in the workspace
            fileEvents: [
                vscode_1.workspace.createFileSystemWatcher('**/.ograph'),
                vscode_1.workspace.createFileSystemWatcher('**/.og')
            ]
        }
    });
    // Start the client. This will also launch the server
    client.start();
};
const initEditors = (context) => {
    context.subscriptions.push(editors_1.OGraphQueryEditorProvider.register(context));
};
const activate = (context) => {
    initClient(context);
    initEditors(context);
};
exports.activate = activate;
const deactivate = () => {
    if (!client) {
        return undefined;
    }
    return client.stop();
};
exports.deactivate = deactivate;

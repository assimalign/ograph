"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const node_1 = require("vscode-languageserver/node");
const vscode_languageserver_textdocument_1 = require("vscode-languageserver-textdocument");
// Creates the LSP connection
const connection = (0, node_1.createConnection)(node_1.ProposedFeatures.all);
// Create a manager for open text documents
const documents = new node_1.TextDocuments(vscode_languageserver_textdocument_1.TextDocument);
// The workspace folder this server is operating on
let workspaceFolder;
documents.onDidOpen(({ document }) => {
    const query = document.getText();
    connection.console.log(`My Query: ${query}`);
});
documents.onDidChangeContent(({ document }) => {
    const query = document.getText();
    connection.console.log(`My Query: ${query}`);
});
documents.listen(connection);
connection.onInitialize((params) => {
    workspaceFolder = params.rootUri;
    connection.console.log(`[Server(${process.pid}) ${workspaceFolder}] Started and initialize received`);
    return {
        capabilities: {
            textDocumentSync: {
                openClose: true,
                change: node_1.TextDocumentSyncKind.None
            }
        }
    };
});
connection.listen();

import {
	createConnection,
	TextDocuments,
	ProposedFeatures,
	TextDocumentSyncKind
} from 'vscode-languageserver/node'

import { TextDocument } from 'vscode-languageserver-textdocument'


// Creates the LSP connection
const connection = createConnection(ProposedFeatures.all);

// Create a manager for open text documents
const documents = new TextDocuments(TextDocument);

// The workspace folder this server is operating on
let workspaceFolder: string | null;

documents.onDidOpen(({ document }) => {
	const query = document.getText()
	connection.console.log(`My Query: ${query}`)
});
documents.onDidChangeContent(({ document }) => {
	const query = document.getText()
	connection.console.log(`My Query: ${query}`)
})

documents.listen(connection);

connection.onInitialize((params) => {
	workspaceFolder = params.rootUri;
	connection.console.log(`[Server(${process.pid}) ${workspaceFolder}] Started and initialize received`);
	return {
		capabilities: {
			textDocumentSync: {
				openClose: true,
				change: TextDocumentSyncKind.None
			}
		}
	};
});
connection.listen();
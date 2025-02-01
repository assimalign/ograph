import * as path from 'path'
import { workspace, ExtensionContext } from 'vscode'
import { LanguageClient, TransportKind } from 'vscode-languageclient/node'
import { OGraphQueryEditorProvider } from './editors';

let client: LanguageClient;

const initClient = (context: ExtensionContext) => {
	// The server is implemented in node
	const module = context.asAbsolutePath(
		path.join('server', 'out', 'server.js')
	)

	// Create the language client and start the client.
	client = new LanguageClient(
		'ographLanguageServer',
		'OGraph Language Server',
		// If the extension is launched in debug mode then the debug server options are used
		// Otherwise the run options are used
		{
			run: {
				module: module,
				transport: TransportKind.ipc
			},
			debug: {
				module: module,
				transport: TransportKind.ipc,
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
				workspace.createFileSystemWatcher('**/.ograph'),
				workspace.createFileSystemWatcher('**/.og')
			]
		}
	})

	// Start the client. This will also launch the server
	client.start();
}
const initEditors = (context: ExtensionContext) => {
	context.subscriptions.push(OGraphQueryEditorProvider.register(context))
}


export const activate = (context: ExtensionContext) => {
	initClient(context)
	initEditors(context)
}
export const deactivate = (): Thenable<void> | undefined => {
	if (!client) {
		return undefined;
	}
	return client.stop();
}
import * as vscode from 'vscode';
import {
	LanguageClient,
	LanguageClientOptions,
	ServerOptions,
	TransportKind
} from 'vscode-languageclient/node';
import { DiagnosticProvider } from './diagnostics/provider';


export function activate(context: vscode.ExtensionContext) {


    
    const diagnosticProvider = new DiagnosticProvider();

    context.subscriptions.push(vscode.commands.registerCommand('', () => {

    }))


    let client = new LanguageClient('', {
        command: ''
    }, {

    })
}

export function deactivate() {
   
}
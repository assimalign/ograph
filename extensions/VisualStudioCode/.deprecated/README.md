# README.md

# VS Code Extension for Advanced Syntax Analysis

This project is a Visual Studio Code extension that provides advanced syntax analysis and error highlighting for a custom language. It leverages TypeScript for development and includes a diagnostic provider to analyze the syntax of documents in real-time.

## Features

- Real-time error highlighting based on syntax analysis.
- Custom syntax highlighting rules defined in `ograph.tmLanguage.json`.
- Utility functions for managing diagnostics and formatting error messages.

## Project Structure

```
vscode-extension
├── src
│   ├── extension.ts          # Main entry point for the extension
│   ├── diagnostics
│   │   ├── provider.ts       # Diagnostic provider for error highlighting
│   │   └── utils.ts          # Utility functions for diagnostics
│   ├── parser
│   │   └── analyzer.ts       # Syntax analysis logic
│   └── types
│       └── index.ts          # Type definitions for diagnostics
├── syntaxes
│   └── ograph.tmLanguage.json # Syntax highlighting rules
├── package.json               # NPM configuration
├── tsconfig.json              # TypeScript configuration
└── README.md                  # Project documentation
```

## Installation

1. Clone the repository:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd vscode-extension
   ```
3. Install dependencies:
   ```
   npm install
   ```

## Usage

1. Open the project in Visual Studio Code.
2. Press `F5` to run the extension in a new Extension Development Host window.
3. Open a file with the custom language syntax to see error highlighting in action.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
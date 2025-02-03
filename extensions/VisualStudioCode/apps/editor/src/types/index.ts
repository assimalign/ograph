export type VsCode = {
    postMessage(message: {
        type: string;
        content: JSON;
    }): void;
    getState(): {
        viewType: string;
        text: string;
    };
    setState(state: {
        viewType: string;
        text: string;
    }): void;
}
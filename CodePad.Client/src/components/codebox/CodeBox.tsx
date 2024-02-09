import React, { FunctionComponent, useEffect } from 'react';
import Editor from 'react-simple-code-editor';
import { highlight, languages } from 'prismjs';
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-javascript';
import 'prismjs/themes/prism.css';

class CodeBoxConfig {
    text?: string;
    show: boolean = true;
    onChange?: (code: string) => void;
}

export const CodeBox: FunctionComponent<CodeBoxConfig> = (props: CodeBoxConfig) => {
    const text = props.text ?? '';
    const [code, setCode] = React.useState(text);

    useEffect(() => {
        setCode(text);
    }, [text]);

    function onChange(code: string) {
        setCode(code);
        if (props.onChange != null) {
            props.onChange(code);
        }
    }
    return (
        <Editor
            value={code}
            onValueChange={code => onChange(code)}
            highlight={code => highlight(code, languages.clike, "c#")}
            padding={10}
            style={{
                fontFamily: '"Fira code", "Fira Mono", monospace',
                fontSize: 12,
            }}
        />
    );
}
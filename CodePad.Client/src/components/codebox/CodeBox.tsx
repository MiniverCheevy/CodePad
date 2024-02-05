import React, { FunctionComponent } from 'react';
import Editor from 'react-simple-code-editor';
import { highlight, languages } from 'prismjs';
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-javascript';
import 'prismjs/themes/prism.css'; //Example style, you can use another


class CodeBoxConfig {
    text?: string;
    show:boolean = true;
}

export const CodeBox: FunctionComponent<CodeBoxConfig> = (props: CodeBoxConfig) => {
    const text = props.text ?? '';
    const [code, setCode] = React.useState(text);
    return (
        <Editor
            value={code}
            onValueChange={code => setCode(code)}
            highlight={code => highlight(code, languages.clike,"c#")}
            padding={10}
            style={{
                fontFamily: '"Fira code", "Fira Mono", monospace',
                fontSize: 12,
            }}
        />
    );
}
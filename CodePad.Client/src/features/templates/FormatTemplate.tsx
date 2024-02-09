import { FunctionComponent } from "react";
import { Collapsible, CollapsibleItem, CodeBox } from "../../components";
import * as server from "../../clients/server/server-clients.generated";
import React from "react";
export class FormatTemplateConfig {

}

export const FormatTemplate: FunctionComponent<FormatTemplateConfig> = (props: FormatTemplateConfig) => {
    const [source, setSource] = React.useState('');
    const [format, setFormat] = React.useState('');
    const [output, setOutput] = React.useState('');
    const files = server.FormatTemplateGetAllClient.GetAll({});
    async function doClean() {
        const reply = await server.SortedDistinctListClient.Clean({text:output});
        if (reply.isOk) {
            setOutput(reply.text ?? '');
        }
    }
    async function doFormat() {
        const reply = await server.FormatClient.Format({format:format, source:source});
        if (reply.isOk) {
            setOutput(reply.data ?? '');
            debugger;
            console.log(reply.data);
        }
    }
    
    return (
        <div className="row border">
            <div className="col s2">
                
            </div>
            <div className="col s10">
                <Collapsible>
                    <CollapsibleItem open={true} header='Source'>
                        <CodeBox show={true} text={source} onChange={setSource} />
                    </CollapsibleItem>
                    <CollapsibleItem open={false} header='Format'>
                        <div className="row">
                            <div className=" col s8">
                                <CodeBox show={true} text={format} onChange={setFormat} />
                            </div>
                            <div className="col s4">
                                <p className="small-text flow-text">
                                    Any .Net Format String should Work, plus a few extra:<br />
                                    &#123;0:!&#125; Friendly Name<br />
                                    &#123;0:^&#125; Proper Case<br />
                                    &#123;#&#125; 0 based row number<br />
                                    &#123;+&#125; 1 based row number<br />

                                    *double up the &#123;and&#125; characters in your format string<br />
                                    ex: public &#123;0&#125; &#123;1&#125; &#123;&#123; get; set; &#125;&#125;<br />
                                    <br />
                                    pending unfriendly string suitable for property or file name<br />
                                </p>
                            </div>
                        </div>
                    </CollapsibleItem>
                    <CollapsibleItem open={false} header='Output'  >
                        <CodeBox show={true} text={output} onChange={setOutput}  />
                        <button className="waves-effect waves-light btn" onClick={doClean}>Sorted Distinct List</button>
                        <button className="waves-effect waves-light btn" onClick={doFormat}>Format</button>
                    </CollapsibleItem>
                </Collapsible>
            </div>
        </div>
    );
}
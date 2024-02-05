import { FunctionComponent } from "react";
import { Collapsible, CollapsibleItem, CodeBox } from "../../components";

export class FormatTemplateConfig {

}

export const FormatTemplate: FunctionComponent<FormatTemplateConfig> = (props: FormatTemplateConfig) => {

    return (
        <div className="row border">
            <div className="col-2">
            
            </div>
            <div className="col-10">
                <Collapsible>
                    <CollapsibleItem open={true} header='Source'>
                        <CodeBox show={true} />
                    </CollapsibleItem>
                    <CollapsibleItem open={false} header='Format'>
                        <div className="row">
                    <div className=" col s8">
                                <CodeBox show={true} />
                            </div>
                            <div className="col s4">
                                <p className="small-text flow-text">
                                    Any .Net Format String should Work, plus a few extra:<br/>
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
                    <CollapsibleItem open={false} header='Output'>
                        <CodeBox show={true} />
                    </CollapsibleItem>
                </Collapsible>
            </div>
        </div>
    );
}
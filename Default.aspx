<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
	    <title>Survey</title>
	    <link href="style.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
	    <div id="container">
		    <div id="spacer"></div>
            <header id="title">
			    <h1>HomeRoom</h1>
		    </header>
		    <div id="content">
			    <form id="queryform" runat="server">				    
                    <asp:Panel id="surveyHeader" CssClass="surveyHeader" runat="server">User information</asp:Panel>
                    <asp:Panel id="surveyContent" CssClass="surveyContent" runat="server">
                        <div class="slider">
                            <div>Visual: <asp:Label id="visualLabel" /></div>
                            <asp:TextBox id="visual" CssClass="slider" runat="server"/>
                        </div>
                        <div class="slider">
                            <div>Aural: <asp:Label id="auralLabel" /></div>
                            <asp:TextBox id="aural" CssClass="slider" runat="server"/>
                        </div>
                        <div class="slider">
                            <div>Tactile: <asp:Label id="tactileLabel" /></div>
                            <asp:TextBox id="tactile" CssClass="slider" runat="server"/>
                        </div>
                        <div style="clear: both;"></div>
				    </asp:Panel>
                    
                    <asp:Panel id="queryHeader" CssClass="queryHeader" runat="server">Query</asp:Panel>
                    <asp:Panel id="queryContent" CssClass="queryContent" runat="server">
                        <asp:TextBox id="query" Width="100%" Font-Size="36pt" runat="server" />
				        <asp:Button id="submit" text="Submit" OnClick="submitQuery" runat="server" />
                        <asp:Label id="results" runat="server" />
                    </asp:Panel>
                    
                    

                    <asp:ScriptManager ID="ScriptManager" runat="server" />
                    <ajaxToolkit:CollapsiblePanelExtender id="expander" runat="server" TargetControlID="surveyContent" ExpandControlID="surveyHeader" CollapseControlID="surveyHeader" />
                    <ajaxToolkit:SliderExtender id="sliderV" Minimum="0" Maximum="100" runat="server" TargetControlID="visual" BoundControlID="visualLabel" EnableHandleAnimation="true" />
                    <ajaxToolkit:SliderExtender id="sliderA" Minimum="0" Maximum="100" runat="server" TargetControlID="aural" BoundControlID="auralLabel" EnableHandleAnimation="true" />
                    <ajaxToolkit:SliderExtender id="sliderT" Minimum="0" Maximum="100" runat="server" TargetControlID="tactile" BoundControlID="tactileLabel" EnableHandleAnimation="true" />
			    </form>
		    </div>
		    <footer>
		       <h6>Brian Guthrie, Joe Hessling, Colin Murphy</h6>
		    </footer>    
	    </div>
    </body>
</html>
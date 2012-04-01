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
                    <asp:Panel id="configHeader" CssClass="surveyHeader" runat="server">Configuration information</asp:Panel>
                    <asp:Panel id="configContent" CssClass="surveyContent" runat="server">
                        How fast is your internet connection?
                        <asp:RadioButtonList id="speed" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="50" Text="Dialup, ISDN">Dialup</asp:ListItem>
                            <asp:ListItem Value="500" Text="Satellite, EDGE, EVDO, 3G">Satellite</asp:ListItem>
                            <asp:ListItem Value="1500" Text="DSL" Selected="True">DSL</asp:ListItem>
                            <asp:ListItem Value="3000" Text="4G">4G</asp:ListItem>
                            <asp:ListItem Value="5000" Text="Cable, Fiber">Cable</asp:ListItem>
                        </asp:RadioButtonList>
                        <div style="clear: both;"></div>
				    </asp:Panel>

                    <asp:Panel id="surveyHeader" CssClass="surveyHeader" runat="server">User survey</asp:Panel>
                    <asp:Panel id="surveyContent" CssClass="surveyContent" runat="server">
                        <!--#include file="questions.aspx"-->
				    </asp:Panel>
                    
                    <asp:Panel id="queryHeader" CssClass="queryHeader" runat="server">Query</asp:Panel>
                    <asp:Panel id="queryContent" CssClass="queryContent" runat="server">
                        <asp:TextBox id="query" Width="100%" Font-Size="36pt" runat="server" />
				        <asp:Button id="submit" text="Submit" OnClick="submitQuery" runat="server" />
                        <asp:Label id="results" runat="server" />
                        <asp:ValidationSummary runat="server" />
                    </asp:Panel>

                    <asp:RequiredFieldValidator id="speedValidator" ControlToValidate="speed" ErrorMessage="Please choose a connection speed" Display="None" runat="server" />
                    <asp:RequiredFieldValidator id="queryValidator" ControlToValidate="query" ErrorMessage="Please enter a topic" Display="None" runat="server" />

                    <asp:ScriptManager id="ScriptManager" runat="server" />
                    <ajaxToolkit:CollapsiblePanelExtender id="expanderC" runat="server" TargetControlID="configContent" ExpandControlID="configHeader" CollapseControlID="configHeader" />
                    <ajaxToolkit:CollapsiblePanelExtender id="expanderS" runat="server" TargetControlID="surveyContent" ExpandControlID="surveyHeader" CollapseControlID="surveyHeader" />
			    </form>
		    </div>
		    <footer>
		       <h6>Brian Guthrie, Joe Hessling, Colin Murphy</h6>
		    </footer>    
	    </div>
    </body>
</html>

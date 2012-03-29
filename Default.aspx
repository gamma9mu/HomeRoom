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
			    <form id="surveyform" runat="server">
				    <asp:TextBox id="visual" runat="server" Columns="3" MaxLength="3" />% visual
				    <asp:RequiredFieldValidator id="visualvalidator" ControlToValidate="visual" ErrorMessage="Enter a value between 1 and 100" Font-Size="Small" Display="Dynamic" runat="server"/>
				    <asp:RangeValidator id="visualrangevalidator" ControlToValidate="visual" Type="Integer" MinimumValue="1" MaximumValue="100" ErrorMessage="Enter a value between 1 and 100" Font-Size="Small" Display="Dynamic" runat="server"/>
				    <br />
				    <asp:TextBox id="aural" runat="server" Columns="3" MaxLength="3" />% aural
				    <asp:RequiredFieldValidator id="auralvalidator" ControlToValidate="aural" ErrorMessage="Enter a value between 1 and 100" Font-Size="Small" Display="Dynamic" runat="server"/>
				    <asp:RangeValidator id="auralrangevalidator" ControlToValidate="aural" Type="Integer" MinimumValue="1" MaximumValue="100" ErrorMessage="Enter a value between 1 and 100" Font-Size="Small" Display="Dynamic" runat="server"/>
				    <br />
				    <asp:TextBox id="tactile" runat="server" Columns="3" MaxLength="3" />% tactile
				    <asp:RequiredFieldValidator id="tactilevalidator" ControlToValidate="tactile" ErrorMessage="Enter a value between 1 and 100" Font-Size="Small" Display="Dynamic" runat="server"/>
				    <asp:RangeValidator id="tactilerangevalidator" ControlToValidate="tactile" Type="Integer" MinimumValue="1" MaximumValue="100" ErrorMessage="Enter a value between 1 and 100" Font-Size="Small" Display="Dynamic" runat="server"/>
				    <br />
				    <asp:Button id="submit" text="Submit" OnClick="submitSurvey" runat="server" />
			    </form>
                <asp:Label id="results" runat="server" />
		    </div>
		    <footer>
		       <h6>Brian Guthrie, Joe Hessling, Colin Murphy</h6>
		    </footer>
	    </div>
    </body>
</html>
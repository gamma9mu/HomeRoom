<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<div id="container">
		<header id="title">
			<h1>HomeRoom</h1>
            <h2>Student Survey</h2>
		</header>
		<div id="content">
            <form id="surveyform" runat="server">
                Visual Learning % <asp:TextBox id="visualpercent" runat="server" Columns="3" MaxLength="3" />
                <asp:RequiredFieldValidator id="visualvalidator" ControlToValidate="visualpercent" ErrorMessage="Enter a value between 1 and 100" Display="Dynamic" runat="server"/>
                <asp:RangeValidator id="visualrangevalidator" ControlToValidate="visualpercent" Type="Integer" MinimumValue="1" MaximumValue="100" ErrorMessage="Enter a value between 1 and 100" Display="Dynamic" runat="server"/>
                <br />
                Aural Learning % <asp:TextBox id="auralpercent" runat="server" Columns="3" MaxLength="3" />
                <asp:RequiredFieldValidator id="auralvalidator" ControlToValidate="auralpercent" ErrorMessage="Enter a value between 1 and 100" Display="Dynamic" runat="server"/>
                <asp:RangeValidator id="auralrangevalidator" ControlToValidate="auralpercent" Type="Integer" MinimumValue="1" MaximumValue="100" ErrorMessage="Enter a value between 1 and 100" Display="Dynamic" runat="server"/>
                <br />
                Tactile Learning % <asp:TextBox id="tactilepercent" runat="server" Columns="3" MaxLength="3" />
                <asp:RequiredFieldValidator id="tactilevalidator" ControlToValidate="tactilepercent" ErrorMessage="Enter a value between 1 and 100" Display="Dynamic" runat="server"/>
                <asp:RangeValidator id="tactilerangevalidator" ControlToValidate="tactilepercent" Type="Integer" MinimumValue="1" MaximumValue="100" ErrorMessage="Enter a value between 1 and 100" Display="Dynamic" runat="server"/>
                <br />
                <asp:Button id="submit" text="Submit" OnClick="submitSurvey" runat="server" />
                <asp:Label id="results" runat="server" />
            </form>
   	    </div>
	    <footer>
	       <h6>Brian Guthrie, Joe Hessling, Colin Murphy</h6>
	    </footer>
	</div>
</body>
</html>

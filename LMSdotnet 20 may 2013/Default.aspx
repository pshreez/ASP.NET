<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <style type="text/css">
        .lbl
        {
        	font-size:large;
        	color:Red;
        	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div align="center">
        <table>
            <tr>
                <td colspan="2">
                    <b>Authentication</b>
                </td>
            </tr>
            <tr>
                <td>User Id</td>
                <td>
                    <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Password
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <cc1:PasswordStrength ID="PasswordStrength1" PreferredPasswordLength="10" MinimumLowerCaseCharacters="5" RequiresUpperAndLowerCaseCharacters="true" TargetControlID="txtPassword" runat="server">
                    </cc1:PasswordStrength>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLogIn" runat="server" Text="Log In" 
                        onclick="btnLogIn_Click" />
                </td>
                <td>
                    <asp:Button ID="btnReset" runat="server" Text="Reset" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label CssClass="lbl" ID="lblmsg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

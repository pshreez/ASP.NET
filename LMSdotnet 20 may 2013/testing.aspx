<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testing.aspx.cs" Inherits="testing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
        function hello() {
            __doPostBack("TextBox1");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:TextBox ID="TextBox1" runat="server" onkeyup="hello();" 
            ontextchanged="TextBox1_TextChanged"></asp:TextBox>
        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox1" PopupButtonID="TextBox1" Format="dd/MMM/yyyy" runat="server">
        </cc1:CalendarExtender>
        
        <asp:FileUpload ID="FileUpload1" runat="server" />
        
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditBooksRecord.aspx.cs" Inherits="EditBooksRecord" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <script type="text/javascript" language="javascript">
        function closepage() {
            
            //try {
              //  window.opener.document.getElementById('btnCancel').click();
            //}
            //catch (e) {
              //  alert(e);
            //}
            //alert("hello");
            
            
            //window.opener.close();
            //window.open("BooksRecord.aspx");
            //window.opener.location.href = "BooksRecord.aspx";
            window.opener.dkfdk();
            self.close();

            window.opener.focus();
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table>
                    <tr>
                        <td colspan="4" align="center">
                            <h1>Update Book Record</h1>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Book ID
                        </td>
                        <td>
                            <asp:TextBox ID="txtBookID" runat="server"></asp:TextBox>
                            <!--hrkk-->
                            
                        </td>
                        <td>
                            Title
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTitle" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Purchase Date
                        </td>
                        <td>
                            
                            <asp:TextBox ID="txtPurchaseDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtPurchaseDate" Format="dd/MMM/yyyy" runat="server">
                            </cc1:CalendarExtender>
                            <asp:DropDownList ID="ddlday" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlmonth" runat="server">
                                <asp:ListItem Value="01">Jan</asp:ListItem>
                                <asp:ListItem Value="02">Feb</asp:ListItem>
                                <asp:ListItem Value="03">Mar</asp:ListItem>
                                <asp:ListItem Value="04">Apr</asp:ListItem>
                                <asp:ListItem Value="05">May</asp:ListItem>
                                <asp:ListItem Value="06">Jun</asp:ListItem>
                                <asp:ListItem Value="07">Jul</asp:ListItem>
                                <asp:ListItem Value="08">Aug</asp:ListItem>
                                <asp:ListItem Value="09">Sep</asp:ListItem>
                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                <asp:ListItem Value="12">Dec</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlyear" runat="server">
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="lblentrydate" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            Author/s
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAuthorlist" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlAuthorlist_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:ListBox ID="lstboxAuthor" runat="server" Width="100px"></asp:ListBox>
                            <br />
                            <asp:Button ID="btnRemove" runat="server" Text="Remove from list" 
                                onclick="btnRemove_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Publisher Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtPublisher" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Publisher Email
                        </td>
                        <td>
                            <asp:TextBox ID="txtPubEmail" runat="server"></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ISBN No.
                        </td>
                        <td>
                            <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Issue Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlissuetype" runat="server">
                                <asp:ListItem Text="Issuable" Value="I"></asp:ListItem>
                                <asp:ListItem Text="Reference" Value="R"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Book Condition
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBookCondition" runat="server">
                                <asp:ListItem Text="Fine" Value="F"></asp:ListItem>
                                <asp:ListItem Text="Damaged" Value="D"></asp:ListItem>
                                <asp:ListItem Text="Lost" Value="L"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Update" onclick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
    </div>
    </form>
</body>
</html>

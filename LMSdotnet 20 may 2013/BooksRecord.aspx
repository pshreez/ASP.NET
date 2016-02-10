<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BooksRecord.aspx.cs" Inherits="BooksRecord" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
        function btnCancel_onclick() {
            return true;
        }
        function dkfdk() {
            document.getElementById('<%=btnCancel.ClientID %>').click();
        }
        function javakeypress() {
            __doPostBack("txtsrchBookID");
            return true;
        }
        function openeditwindow(bookid) {
            //alert("hello");
            window.open("EditBooksRecord.aspx?id=" + bookid + "","", "width=700px,height=500px,margin-left=50px,scrollbars=no,resizable=no,toolbars=no,status=no,menubar=no,location=no");
            //document.getElementById('<%=txtBookID.ClientID %>').value = bookid;
            //document.getElementById('<%=txtPublisher.ClientID %>').value = publishername;
            //window.location.href = "BooksRecord.aspx?bookid=" + bookid + "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div runat="server">
        
        
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <h1>Books Record</h1>
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
                            <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="txtPurchaseDate" Format="dd/MMM/yyyy" TargetControlID="txtPurchaseDate" runat="server">
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
                        <td>
                            Book Logo 
                        </td>
                        <td>
                            <asp:FileUpload ID="fupBookLogo" runat="server" />
                            
                        </td>
                    </tr>
                    <tr>
                        
                            <td align="center" colspan="4">
                                <asp:Button ID="btnNew" runat="server" Text="New" onclick="btnNew_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" 
                                     onclick="btnSave_Click" 
                                     />
                                     
                                     <input type="hidden" id="hdnflag" name="hdnflag" runat="server" />
                                
                                <input type="submit" runat="server" id="btnDel" name="btnDel" value="Delete" onserverclick="btnDel_clik" />
                                
                                <input type="submit" id="btnCancel" name="btnCancel" runat="server" onserverclick="btnCancel_Click" value="Cancel" />
                                <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="return btnCancel_onclick();" OnClick="btnCancel_Click" />--%>
                            </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            Book ID
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtsrchBookID" runat="server"
                                onkeyup="return javakeypress();" ontextchanged="txtsrchBookID_TextChanged"
                                ></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            Book Title
                            <asp:DropDownList ID="ddlsrchBookTitle" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtsrchBookTitle" runat="server"></asp:TextBox> 
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                 />
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="center" colspan="4">
                            <div style="overflow:auto;height:100px">
                                <asp:Label ID="showdata" runat="server" Text=""></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnPrevious" runat="server" Text="<<" 
                                onclick="btnPrevious_Click" />
                            <asp:Button ID="btnNext" runat="server" Text=">>" onclick="btnNext_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="lblpaginationincr" runat="server" Text=""></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text=" of "></asp:Label>
                            <asp:Label ID="lblpaginationtot" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
          
                    
    </div>
    
</asp:Content>


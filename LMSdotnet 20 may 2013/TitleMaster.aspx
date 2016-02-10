<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TitleMaster.aspx.cs" Inherits="TitleMaster" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="center">
        <table align="center">
            <tr>
                <td align="center" colspan="2" style="font-size:20px;font-weight:bold;">Title Master</td>
            </tr>
            <tr>
                <td align="center">Title Name</td>
                <td align="center">
                    <asp:TextBox ID="txtTitleName" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTitleName" ControlToValidate="txtTitleName" Display="Dynamic" ForeColor="Red" ValidationGroup="vgsave" runat="server" ErrorMessage="*Tile please!!"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnNew" runat="server" Text="New" onclick="btnNew_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" onclick="btnEdit_Click" />
                    <asp:Button ID="btnSave" ValidationGroup="vgsave" runat="server" Text="Save" 
                        onclick="btnSave_Click" />
                    <asp:Button ID="btnDel" runat="server" Text="Delete" onclick="btnDel_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                        onclick="btnCancel_Click" />
                    <asp:Label ID="lbliID" runat="server" Text="" Visible="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    
                        <asp:GridView ID="gvTitle" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                        onpageindexchanging="gvTitle_PageIndexChanging" 
                         CellPadding="4" 
                        ForeColor="#333333" GridLines="None" onrowediting="gvTitle_RowEditing" 
                            onrowupdating="gvTitle_RowUpdating" 
                            onrowcancelingedit="gvTitle_RowCancelingEdit" 
                            onrowdeleting="gvTitle_RowDeleting" 
                            onselectedindexchanged="gvTitle_SelectedIndexChanged" 
                            onrowdatabound="gvTitle_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Select" ControlStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="true" OnCheckedChanged="chkselect_CheckedChange" />
                                        <asp:Label ID="lblgvID" runat="server" Text='<%#Bind("iID") %>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    
<ControlStyle Width="30px"></ControlStyle>
                                    
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Title" ControlStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTitle" runat="server" Text='<%#Bind("sBookTitle") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTitle" runat="server" Text='<%#Bind("sBookTitle") %>'></asp:TextBox>
                                    </EditItemTemplate>

<ControlStyle Width="150px"></ControlStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Date/Time" ControlStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDateTime" runat="server" Text='<%#Bind("dtdatetime") %>'></asp:Label>
                                    </ItemTemplate>

<ControlStyle Width="100px"></ControlStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="User" ControlStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUserName" runat="server" Text='<%#Bind("sUserID") %>'></asp:Label>
                                    </ItemTemplate>

<ControlStyle Width="100px"></ControlStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Status" ControlStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlgvStatus" runat="server" DataValueField='<%#Bind("sStatus") %>'>
                                            <asp:ListItem Value="A">Active</asp:ListItem>
                                            <asp:ListItem Value="D">Delete</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>

<ControlStyle Width="50px"></ControlStyle>
                                </asp:TemplateField>
                                
                                <asp:CommandField ShowEditButton="true" />
                                <asp:CommandField ShowDeleteButton="true" />
                                
                            </Columns>
                            
                        </asp:GridView>
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="testing2.aspx.cs" Inherits="testing2" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox1" Format="dd/MMM/yyyy" runat="server">
    </cc1:CalendarExtender>
</asp:Content>


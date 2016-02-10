<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Adrotator.aspx.cs" Inherits="Adrotator" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="Timer1" Interval="5000" runat="server">
            </asp:Timer>
            <asp:AdRotator ID="AdRotator1" KeywordFilter="small" runat="server" AdvertisementFile="~/XMLFile.xml" />        
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>


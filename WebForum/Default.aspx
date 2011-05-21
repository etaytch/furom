<%@ Page Title="Furom" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebForum._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="welcomePanel" runat="server">
        <asp:Label ID="_lable" runat="server" Font-Bold="True" Font-Italic="True" 
        Font-Names="Forte" Font-Size="XX-Large" Text="Welcome to the Furom" 
            style="text-align: center"></asp:Label>
        &nbsp;
        <br />
        please <a href="Login.aspx">Log In</a><br />
        <br />
    </asp:Panel>
</asp:Content>

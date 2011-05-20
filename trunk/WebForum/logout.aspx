<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="WebForum.logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="logoutButton" runat="server" onclick="Button1_Click" 
    Text="logout" />
<asp:Label ID="error" runat="server" Text="Label" 
        style="color: #990000; font-size: x-large" Visible="False"></asp:Label>
</asp:Content>

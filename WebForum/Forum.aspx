<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="WebForum.Forum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
<asp:BulletedList ID="BulletedList1" runat="server" Height="220px" 
    style="margin-top: 32px; margin-bottom: 200px" Width="283px">
</asp:BulletedList>
</asp:Content>

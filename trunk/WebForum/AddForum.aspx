<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddForum.aspx.cs" Inherits="WebForum.AddForum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" style="font-size: large" 
        Text="New Forum Name "></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="ForumName" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="createButton" runat="server" onclick="createButton_Click" 
        Text="Create new Forum" Width="163px" />
    <br />
    <br />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="WebForum.Friends" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="removFriendButton" runat="server" Text="Remov friends" 
        onclick="AddFriendButton_Click" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Friends"></asp:Label>
    <asp:Label ID="removSecceed" runat="server" Text="remov secceed" 
        Visible="False"></asp:Label>
    <strong><span class="style1"><br />
    <asp:CheckBoxList ID="friendList" runat="server">
    </asp:CheckBoxList>
    <br /></span></strong>
</asp:Content>

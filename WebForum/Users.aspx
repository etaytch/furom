<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WebForum.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <br />
<asp:Button ID="AddFriendButton" runat="server" Text="Add as friends" 
        onclick="AddFriendButton_Click" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <asp:Label ID="friendAdded" runat="server" Text="Friend added successfuly" 
        Visible="False"></asp:Label>
<br />
    <strong><span class="style1">&nbsp;
    <asp:Label ID="Label1" runat="server" Text="Availible Users:" Visible="False"></asp:Label>
    <br />
    <asp:CheckBoxList ID="userList" runat="server" Visible="False">
    </asp:CheckBoxList>
    <br />
    <asp:Panel ID="AdminPanel" runat="server">
        <asp:Button ID="removeButton" runat="server" 
    Text="Remove Users" onclick="removeButton_Click" Visible="False" />
    </asp:Panel>
    </span></strong>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="WebForum.Forum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    <asp:Panel ID="ForumListPanel" runat="server" HorizontalAlign="Justify" 
        Visible="False">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" style="font-size: large; color: #000066" 
            Text="Welcome to furom"></asp:Label>
        <asp:GridView ID="ForumTable" runat="server" CellPadding="4" 
            AutoGenerateColumns="False" 
            OnRowCommand="ForumTable_RowCommsnd"
            ForeColor="#333333" GridLines="None" Width="915px">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <br />
        <asp:Panel ID="adminPanel" runat="server" Visible="False">
            <asp:Button ID="AddForumButton" runat="server" onclick="AddForumButton_Click" 
                Text="Add Forum" Width="912px" />
        </asp:Panel>
    </asp:Panel>

    <br />


    <asp:Panel ID="welcomePanel" runat="server">
        <asp:Label ID="_lable" runat="server" Font-Bold="True" Font-Italic="True" 
        Font-Names="Forte" Font-Size="XX-Large" Text="Welcome" 
            style="text-align: center"></asp:Label>
        &nbsp;
        <br />
        Please <a href="Login.aspx">log in</a><br /> <br />
        </asp:Panel>
    </asp:Content>

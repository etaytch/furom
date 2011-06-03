<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="WebForum.Forum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style6
        {
            color: #000066;
            font-size: x-large;
            direction: ltr;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    <asp:Panel ID="ForumListPanel" runat="server" HorizontalAlign="Justify" 
        Visible="False">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" style="font-size: large; color: #000066" 
            Text="Welcome To FUROM"></asp:Label>
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
    </asp:Panel>
    <br />
        <asp:Panel ID="ForumWithThreadsPanel" runat="server" Visible="False">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" style="font-size: large; color: #000066" 
                Text="Welcome To "></asp:Label>
            &nbsp;<asp:Label ID="forumName" runat="server" 
                style="font-size: large; color: #0000CC" Text="Label"></asp:Label>
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
                ID="Label1" runat="server" style="color: #003300" Text="Autor: "></asp:Label>
            &nbsp;<asp:Label ID="AutorName" runat="server" style="color: #006600" 
                Text="Label"></asp:Label>
            <br />
            <asp:GridView ID="threadTable" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                OnRowCommand="ThreadTable_RowCommsnd" Width="913px">
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
            <asp:Button ID="addthreadButton" runat="server" BorderColor="#0000CC" 
                BorderStyle="Groove" CssClass="menu" Font-Bold="False" Font-Names="Ron" 
                Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" 
                onclick="addthreadButton_Click" Text="Add Thread" Width="911px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
        </asp:Panel>
    <asp:Panel ID="welcomePanel" runat="server">
        <asp:Label ID="_lable" runat="server" Font-Bold="True" Font-Italic="True" 
        Font-Names="Forte" Font-Size="XX-Large" Text="Welcome to the Furom" 
            style="text-align: center"></asp:Label>
        &nbsp;
        <br />
        please <a href="Login.aspx">Log In</a><br /> <br />
        </asp:Panel>
    <asp:Panel ID="addThreadPanel" runat="server" Visible="False">
        <span class="style6">Add New Thread</span><br />
        topic:
        <br />
        <asp:TextBox ID="threadTopicBox" runat="server"></asp:TextBox>
        <br />
        content:<br />
        <asp:TextBox ID="threadContentBox" runat="server" Height="106px" 
            TextMode="MultiLine" Width="562px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="addThreadError" runat="server" style="color: #FF0000" 
            Text="Label" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Button ID="okThreadButton" runat="server" Text="OK" 
            onclick="okThreadButton_Click" Width="94px" />
        &nbsp;<asp:Button ID="threadcancelButton" runat="server" 
            onclick="threadcancelButton_Click" Text="Cancel" Height="26px" 
            Width="104px" />
    </asp:Panel>
    </asp:Content>

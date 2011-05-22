<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="WebForum.Forum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2
        {
            font-size: x-large;
        }
        .style4
        {
            color: #000066;
        }
        .style5
        {
            color: #000066;
            font-size: xx-large;
        }
        .style6
        {
            color: #000066;
            font-size: x-large;
        }
        .style7
        {
            font-size: x-large;
            color: #0000CC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    <asp:Panel ID="ForumListPanel" runat="server" HorizontalAlign="Justify" 
        Visible="False">
        <span class="style1"><span class="style4">Forums</span></span><br />
        <asp:GridView ID="ForumTable" runat="server" CellPadding="4" 
            AutoGenerateColumns="False" 
            OnRowCommand="ForumTable_RowCommsnd"
            ForeColor="#333333" GridLines="None">
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
                Text="Forum:"></asp:Label>
            &nbsp;&nbsp;
            <asp:Label ID="forumName" runat="server" 
                style="font-size: large; color: #0000CC" Text="Label"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" style="color: #003300" Text="Autor: "></asp:Label>
            <asp:Label ID="AutorName" runat="server" style="color: #006600" Text="Label"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:GridView ID="threadTable" runat="server" CellPadding="4" 
                 AutoGenerateColumns="False" 
                OnRowCommand="ThreadTable_RowCommsnd"
                ForeColor="#333333" GridLines="None">
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
            <asp:Button ID="addthreadButton" runat="server" onclick="addthreadButton_Click" 
                Text="add Thread" />
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
    <asp:Panel 
        ID="ThreadWithPostsPanel" runat="server" Visible="False">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" CssClass="style2" 
            style="color: #000066" Text="Forum:"></asp:Label>
        <asp:Label ID="forumNameInThread" runat="server" CssClass="style2" 
            style="color: #0000CC" Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" style="font-size: large; color: #003366" 
            Text="Thread:"></asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="ThreadName" runat="server" 
            style="font-size: large; color: #0099FF" Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" style="color: #003300" Text="Autor: "></asp:Label>
        <asp:Label ID="ThreadAutorName" runat="server" style="color: #006600" 
            Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:GridView ID="PostTable" OnRowCommand="PostsTable_RowCommsnd" runat="server" CellPadding="4" 
            ForeColor="#333333" GridLines="None">
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;<asp:Label 
            ID="removeThreadError" runat="server" style="color: #FF0000" Text="Label" 
            Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Button ID="removeThreadButton" runat="server" 
            onclick="removeThreadButton_Click" Text="Remove thread" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
    </asp:Panel>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Panel 
        ID="PostPanel" runat="server" Visible="False">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" style="font-size: large; color: #660066" 
            Text="Post:"></asp:Label>
        &nbsp;<asp:Label ID="postName" runat="server" 
            style="font-size: large; color: #9900FF" Text="Label"></asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" style="color: #003300" Text="Autor: "></asp:Label>
        <asp:Label ID="PostAutorName" runat="server" style="color: #006600" 
            Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="postText" runat="server" 
            Height="133px" ReadOnly="True" 
            TextMode="MultiLine" Width="463px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="addThreadPanel" runat="server" Visible="False">
        <span class="style6">ADD THREAD</span><span class="style5"><br /> </span>
        <br />
        topic:
        <br />
        <br />
        <asp:TextBox ID="threadTopicBox" runat="server"></asp:TextBox>
        <br />
        <br />
        content:<br />
        <asp:TextBox ID="threadContentBox" runat="server" Height="106px" Width="277px" 
            TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="addThreadError" runat="server" style="color: #FF0000" 
            Text="Label" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Button ID="okThreadButton" runat="server" Text="OK" 
            onclick="okThreadButton_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="threadcancelButton" runat="server" 
            onclick="threadcancelButton_Click" Text="Cancel" Height="26px" />
    </asp:Panel>
    <br />
    <asp:Panel ID="addPostPanel" runat="server" Visible="False">
        <span class="style7">ADD POST</span><br />
        <br />
        topic:<br />
        <br />
        <asp:TextBox ID="postTopicBox" runat="server" Height="22px"></asp:TextBox>
        <br />
        content:<br />
        <br />
        <asp:TextBox ID="PostContextBox" runat="server" Height="55px" Width="181px" 
            TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="addPostError" runat="server" 
            style="color: #CC0000; font-size: medium" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="OkPostButton" runat="server" onclick="OkPostButton_Click" 
            Text="ok" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CancelPost" runat="server" onclick="CancelPost_Click" 
            Text="Cancel" />
    </asp:Panel>
    <br />
</asp:Content>

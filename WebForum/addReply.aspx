<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addReply.aspx.cs" Inherits="WebForum.addReplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
    <div id="currentPost" runat="server">
        <br />
        <asp:Label ID="Label4" runat="server" style="font-size: xx-large" 
            Text="Original Massage:"></asp:Label>
        <br />
        <br />
        <asp:Label ID="OriginalTopic" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="OriginalContent" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" style="font-size: xx-large" Text="REPLY"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" style="font-size: x-large" Text="Topic:"></asp:Label>
        <br />
        <asp:TextBox ID="topic" runat="server" Width="529px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" style="font-size: x-large" Text="content"></asp:Label>
        <br />
        <asp:TextBox ID="content" runat="server" Height="187px" TextMode="MultiLine" 
            Width="519px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="OkButton" runat="server" onclick="Button1_Click" Text="OK" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CancelButton" runat="server" onclick="CancelButton_Click" 
            Text="Cancel" />
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <br />
        <asp:Label ID="Label5" runat="server" style="font-size: xx-large" 
            Text="Forum"></asp:Label>
        <br />
        <br />
        <asp:Label ID="ForumName" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" style="font-size: xx-large" 
            Text="NEW THREAD"></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" style="font-size: x-large" Text="Topic:"></asp:Label>
        <br />
        <asp:TextBox ID="newThreadtopic" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label8" runat="server" style="font-size: x-large" Text="content"></asp:Label>
        :<br />
        <asp:TextBox ID="NewThreadContent" runat="server" Height="146px" 
            TextMode="MultiLine" Width="502px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="newThreadButtenOk" runat="server" 
            onclick="newThreadButtenOk_Click" Text="OK" />
        &nbsp;
        <asp:Button ID="NewThreadCancelButton" runat="server" 
            onclick="NewThreadCancelButton_Click" Text="cancel" />
    </asp:Panel>
</asp:Content>

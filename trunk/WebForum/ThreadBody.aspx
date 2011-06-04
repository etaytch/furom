<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThreadBody.aspx.cs" Inherits="WebForum.ThreadBody" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/demos.css" rel="stylesheet" type="text/css" />
  <link href="css/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
        <br />
        <asp:ImageButton ID="replay" runat="server" Height="66px" 
            ImageUrl="~/replay2.png" onclick="ImageButton1_Click" Width="54px" />
        <br />
        Add Replay<br />

    
        <script src="js/jquery-1.5.1.js" type="text/javascript"></script>
        <script src="js/ui/jquery.ui.core.js" type="text/javascript"></script>
        <script src="js/ui/jquery.ui.widget.js" type="text/javascript"></script>
        <script src="js/ui/jquery.ui.accordion.js" type="text/javascript"></script>
        <script language="javascript" type= "text/javascript">
            $(document).ready(function () {
                $('div[name^="accordion"]').append('<asp:ImageButton runat="server" Height="66px"  ImageUrl="~/replay2.png" onclick="ImageButton1_Click" Width="54px" />');
                $('div[name^="accordion"]').accordion({ collapsible: true, autoHeight: false });
                $('div[name^="accordion"]').resize();
            });
        </script>
        <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
                <br />
                <br />
                <br />
                <br />
            </asp:Panel>
        </asp:Panel>
</asp:Content>


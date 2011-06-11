<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForumPage.aspx.cs" Inherits="WebForum.ForumPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

  <link rel="stylesheet" type="text/css" href="Styles/structure.css"/>
  <link rel="stylesheet" type="text/css" href="Styles/main.css"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

		<script type="text/javascript"><!--
		    var plusminus = new Array(new Image(), new Image());
		    plusminus[0].src = "image/button-plus.gif";
		    plusminus[1].src = "image/button-minus.gif";

		    function addthread() {
		        document.getElementById('ForumPanel').setAttribute("Visible", false);
		        document.getElementById('addThreadPanel').setAttribute("Visible", true);
		    }
            
            function toggle_thread(id) {
		        var div = document.getElementById('level_' + id);
		        var img = document.getElementById('img_toggle_' + id);
		        if (div.style.display == 'none') {
		            div.style.display = 'block';
		            img.src = plusminus[1].src;
		            img.alt = '[-]';
		        }
		        else {
		            div.style.display = 'none';
		            img.src = plusminus[0].src;
		            img.alt = '[+]';
		        }
		        return true;
		    }
		//-->
		</script>

        <asp:Panel ID="ForumPanel" runat="server">     
        </asp:Panel>

</asp:Content>
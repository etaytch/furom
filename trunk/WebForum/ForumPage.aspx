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

        <asp:Panel ID="addThreadPanel" runat="server" Visible="False">
            <span class="style6">Add new thread</span><br />
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
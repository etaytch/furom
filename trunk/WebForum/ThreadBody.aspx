<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThreadBody.aspx.cs" Inherits="WebForum.ThreadBody" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/demos.css" rel="stylesheet" type="text/css" />
  <link href="css/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    

    
        <script src="js/jquery-1.5.1.js" type="text/javascript"></script>
        <script src="js/ui/jquery.ui.core.js" type="text/javascript"></script>
        <script src="js/ui/jquery.ui.widget.js" type="text/javascript"></script>
        <script src="js/ui/jquery.ui.accordion.js" type="text/javascript"></script>
        <script language="javascript" type= "text/javascript">
            $(document).ready(function () {


                $('div[name^="accordion"]').append('<asp:ImageButton name= "replay" runat="server" Height="26px"  ImageUrl="~/replay2.png" OnClientClick= "replayfunction()" Width="24px"  />Replay<br/><asp:ImageButton name= "replay" runat="server" Height="26px"  ImageUrl="~/replay2.png" OnClick= "deletePostFunction" Width="24px"  />delete<br/>');
                $('div[name^="accordion"]').accordion({ collapsible: true, autoHeight: false });
                $('div[name^="accordion"]').resize();
                $('div[name^="accordion"] > h3 > a').click(function () {
                    $("#myPost").attr("postID", $(this).parent().parent().attr("postID"));
                    //  $("#myPost").attr("perantID", $(this).parent().parent().attr("perantID"));
                    //  $("#myPost").attr("subject", $(this).parent().parent().attr("subject"));
                    //  $("#myPost").attr("userName", $(this).parent().parent().attr("userName"));
                })

            });

            function replayfunction() {
                window.location = "/addReply.aspx?postID=" + $("#myPost").attr("postID");
                //    window.location = "/addReply.aspx?userName=" + $("#myPost").attr("userName") + "&postID=" + $("#myPost").attr("postID") + "&perantID=" + $("#myPost").attr("perantID") + "&subject=" + $("#myPost").attr("subject");
            }



        </script>
        <asp:Panel ID="Panel1" runat="server">
        <br />        
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
                <br />
                <asp:ImageButton ID="removeThreadButton"  runat="server" Height="46px" ImageUrl="~/chat-delete.png" Width="44px" onclick="removeThreadButton_Click"  />Remove Thread<br/>';
                <br />
            </asp:Panel>
</asp:Content>


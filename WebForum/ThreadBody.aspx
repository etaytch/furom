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
                $('div[name^="wrapper"]').append('<asp:ImageButton name= "replay" runat="server" Height="26px"  ImageUrl="~/replay2.png" OnClientClick= "replayfunction()" Width="24px"  />Replay<br/>');
                var admin = $("#myPost").attr("admin");
                if (admin == "true") {
                    $('div[name^="wrapper"]').append('<asp:ImageButton name= "replay" runat="server" Height="26px"  ImageUrl="~/replay2.png" OnClientClick= "deletePostFunction()" Width="24px"  />delete<br/>');
                    $("#Panel2").attr["Visible"] = "true";

                }
                else {
                    var myUserName = $("#myPost").attr("currentUserName");
                    $('div[author="' + myUserName + '"]').append('<asp:ImageButton name= "replay" runat="server" Height="26px"  ImageUrl="~/replay2.png" OnClientClick= "deletePostFunction()" Width="24px"  />delete<br/>');
                    $("#Panel2").attr["Visible"] = "false";
                }
                $('div[name^="accordion"]').accordion({ collapsible: true, autoHeight: false });
                $('div[name^="wrapper"]').accordion({ collapsible: true, autoHeight: false });
                $('div[name^="accordion"]').resize();
                $('div[name^="accordion"] > h3 > a').click(function () {                
                    var id = $(this).attr("postID");
                    $("#myPost").attr("postID", id);             
                })
                $('[name^="replay"]').click(function () {
                    var id = $(this).parent().attr("postID");
                    $("#myPost").attr("postID", id);
                })

            });
            function deletePostFunction() {
                id = $("#myPost").attr("postID");
                //alert("post  " + id);
                window.location = "/removePost.aspx?postID=" + id;
            }
            function replayfunction() {
                id = $("#myPost").attr("postID");
               // alert("post  " + id);
                window.location = "/addReply.aspx?postID=" + id;
            }

        </script>
        <asp:Panel ID="Panel1" runat="server">
        <br />        
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
                <br />
                <asp:ImageButton ID="removeThreadButton"  runat="server" Height="46px" ImageUrl="~/chat-delete.png" Width="44px" onclick="removeThreadButton_Click"  />Remove Thread<br />
            </asp:Panel>
</asp:Content>


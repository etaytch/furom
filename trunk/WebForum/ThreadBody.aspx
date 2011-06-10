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
                    var myUserName = $("#myPost").attr("currentUserName");
                    alert("I am admin: " + myUserName);
                    $('div[name^="wrapper"]').append('<asp:ImageButton name= "replay" runat="server" Height="26px"  ImageUrl="~/replay2.png" OnClick= "deletePostFunction" Width="24px"  />delete<br/>');
                    $("#Panel2").attr["Visible"] = "true";

                }
                else {
                    var myUserName = $("#myPost").attr("currentUserName");
                    alert("I am user: " + myUserName);
                    $('div[author="' + myUserName + '"]').append('<asp:ImageButton name= "replay" runat="server" Height="26px"  ImageUrl="~/replay2.png" OnClick= "deletePostFunction" Width="24px"  />delete<br/>');
                    $("#Panel2").attr["Visible"] = "false";
                }


                $('div[name^="accordion"]').accordion({ collapsible: true, autoHeight: false });
                $('div[name^="wrapper"]').accordion({ collapsible: true, autoHeight: false });
                $('div[name^="accordion"]').resize();
                $('div[name^="accordion"] > h3 > a').click(function () {
                    //if ($(this).parent().parent().attr("postID") == "0") {
                    var id = $(this).attr("postID");
                    //var name = $(this).parent().parent().attr("content");
                    //alert("post is "+id+" in ");
                    //}
                    $("#myPost").attr("postID", id);
                    //  $("#myPost").attr("perantID", $(this).parent().parent().attr("perantID"));
                    //  $("#myPost").attr("subject", $(this).parent().parent().attr("subject"));
                    //  $("#myPost").attr("userName", $(this).parent().parent().attr("userName"));
                })
                $('[name^="replay"]').click(function () {
                    var id = $(this).parent().attr("postID");
                    //var name = $(this).parent().parent().attr("content");
                    //    alert("post is "+this+" in ");
                    //}
                    $("#myPost").attr("postID", id);
                })

            });

            function replayfunction() {
              //  var name = $(this).parent.attr("name");
              
                //alert(" in " + name);
                id = $("#myPost").attr("postID");
                //name = $(this).parent().attr("content");
                alert("post  " + id);

                window.location = "/addReply.aspx?postID=" + id;

                //    window.location = "/addReply.aspx?userName=" + $("#myPost").attr("userName") + "&postID=" + $("#myPost").attr("postID") + "&perantID=" + $("#myPost").attr("perantID") + "&subject=" + $("#myPost").attr("subject");
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


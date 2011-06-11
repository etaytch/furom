<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="WebForum.Login" %>





<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<script src="Scripts/jquery-1.6.1.js" type="text/javascript"></script>


<script type="text/javascript">

    $(document).ready(function () {
        var $LoginButton = $('#<%=LoginButton.ClientID%>');
        $LoginButton.attr("disabled", "true");
        //$LoginButton.attr("enabaled", "true");
        if ($('#<%=UserName.ClientID%>').val() != "")
            $.ajax({ url: "davar.aspx?userName=" + $('#<%=UserName.ClientID%>').val(), cache: false, success: function (data) {
                if (data == "true") {
                    $LoginButton.attr("disabled", "true");
                    $('#usernameError').html("userName don't exist").fadeIn();
                }
                else {
                    $LoginButton.removeAttr("disabled");
                    $('#usernameError').html("").fadeIn();
                }
            }
            });
        $('#<%=UserName.ClientID%>').blur(function () {
            $.ajax({ url: "davar.aspx?userName=" + $('#<%=UserName.ClientID%>').val(), cache: false, success: function (data) {
                if (data == "true") {
                    $LoginButton.attr("disabled", "true");
                    $('#usernameError').html("userName don't exist").fadeIn();
                }
                else if (data == "empty") {
                    $LoginButton.attr("disabled", "true");
                    $('#usernameError').html("").fadeIn();
                }
                else {
                    $LoginButton.removeAttr("disabled");
                    $('#usernameError').html("").fadeIn();
                }
            } 
            });
        });

    });

function validate() 
    {
        if (document.getElementById("<%=UserName.ClientID%>").value == "") {
            $('#DivError').html("UserName can not be blank").fadeIn();
            document.getElementById("<%=UserName.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=Password.ClientID %>").value == "") {
            $('#DivError').html("Password can not be blank").fadeIn();
            document.getElementById("<%=Password.ClientID %>").focus();
            return false;
        }
        $('#DivError').html("").fadeIn();
    }

</script>





    <h2>
        &nbsp;<asp:Label ID="error" runat="server" style="color: #FF3300" Text="Label" 
            Visible="False"></asp:Label>
    </h2>
    <asp:Panel ID="Panel1" runat="server">
        <br />
        <h2 ID="hade">
            Log In</h2>
        <p>
            Please enter your username and <span class="style2">password</span>.
            <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink>
            if you don&#39;t have an account.
        </p>
        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
        <br /><div style="position:relative; top: 5px; left: 1px;"></div>
        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
        <div style="position:absolute; left:160px; color:Red; font-size:small; font-weight:normal; top: -2px; height: 19px;" 
                id="usernameError"></div>
        <br />
        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
        <br />
        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" 
                            TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <br />
    </asp:Panel>
    <br />
    <div style="position:absolute;  color:Red; font-size:small; font-weight:normal; top: 380px;" 
                id="DivError"></div>
    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" 
       OnClientClick="return validate()" onclick="LoginButton_Click" style="height: 26px"/>
                </asp:Content>

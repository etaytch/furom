<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="WebForum.Register" %>




<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
<script src="Scripts/jquery-1.6.1.js" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {
        var $submitButton = $('#<%=registerButton.ClientID%>');
        $submitButton.attr("disabled", "true");
        $('#<%=UserName.ClientID%>').blur(function () {
            $.ajax({ url: "davar.aspx?userName=" + $('#<%=UserName.ClientID%>').val(), cache: false, success: function (data) {
                if (data == "true") {
                    $submitButton.removeAttr("disabled");
                    $('#usernameError').html("").fadeIn();
                }
                else if (data == "empty") {
                    $submitButton.attr("disabled", "true");
                    $('#usernameError').html("").fadeIn();
                }
                else {
                    $submitButton.attr("disabled", "true");
                    $('#usernameError').html("userName not available").fadeIn();
                }
            }
            });

        });
    });


    function validate() {
        if (document.getElementById("<%=UserName.ClientID%>").value == "") {
            $('#DivError').html("UserName can not be blank").fadeIn();
            document.getElementById("<%=UserName.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=First.ClientID %>").value == "") {
            $('#DivError').html("FirstName can not be blank").fadeIn(); 
            document.getElementById("<%=First.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=Last.ClientID %>").value == "") {
            $('#DivError').html("LastName can not be blank").fadeIn();
            document.getElementById("<%=Last.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=Password.ClientID %>").value == "") {
            $('#DivError').html("Password can not be blank").fadeIn();
            document.getElementById("<%=Password.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=RePassword.ClientID %>").value == "") {
            $('#DivError').html("RePassword can not be blank").fadeIn();
            document.getElementById("<%=RePassword.ClientID %>").focus();
            return false;
        }
        
        if (document.getElementById("<%=Email.ClientID %>").value == "") {
            $('#DivError').html("Email can not be blank").fadeIn(); 
            document.getElementById("<%=Email.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=Password.ClientID %>").value != document.getElementById("<%=RePassword.ClientID %>").value) {
            $('#DivError').html("Passwords don't match").fadeIn();
            document.getElementById("<%=RePassword.ClientID %>").focus();
            return false;
        }
        var emailPat = /^(\".*\"|[A-Za-z]\w*)@(\[\d{1,3}(\.\d{1,3}){3}]|[A-Za-z]\w*(\.[A-Za-z]\w*)+)$/;
        var emailid = document.getElementById("<%=Email.ClientID %>").value;
        var matchArray = emailid.match(emailPat);
        if (matchArray == null) {
            $('#DivError').html("Your email address seems incorrect. Please try again.").fadeIn(); 
            document.getElementById("<%=Email.ClientID %>").focus();
            return false;
        }
        $('#DivError').html("").fadeIn();     
    }

    
</script>
    

    <asp:Panel ID="Panel1" runat="server">
        <span class="style1"><strong>CREATE A NEW ACCOUNT</strong><br /> 
        <asp:Panel ID="errorPanel" runat="server" Visible="False">
            <span class="style1">
            <asp:Label ID="error" runat="server" style="color: #FF0000"></asp:Label>
            </span>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server" Visible="False">
            <span class="style1">
            <asp:Label ID="success0" runat="server" Text="Register Complete"></asp:Label>
            </span>
        </asp:Panel>
        </span><div style="position:relative">
        <asp:Panel ID="Panel2" runat="server">
            <br />
            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="UserNameLabel0" runat="server" AssociatedControlID="UserName">First name:</asp:Label>
            <div style="position:absolute; left:155px; color:Red; font-size:small; font-weight:normal;" id="usernameError"><img id="loadergif" src="6-0.gif" style="display:none;" /></div>
            <br />
            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="First" runat="server" CssClass="textEntry" Width="137px"></asp:TextBox>
            <br />
            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="PasswordLabel0" runat="server" AssociatedControlID="Password">Last name:</asp:Label>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <br />
            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" 
                TextMode="Password"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Last" runat="server" CssClass="textEntry" Width="136px"></asp:TextBox>
            <br />
            <asp:Label ID="UserNameLabel1" runat="server" AssociatedControlID="UserName">Repeat password</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="countryLable" runat="server" AssociatedControlID="UserName">Country:</asp:Label>
            <br />
            <asp:TextBox ID="RePassword" runat="server" CssClass="passwordEntry" 
                TextMode="Password"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="Country" runat="server">
<asp:ListItem Value="Afghanistan"></asp:ListItem>
<asp:ListItem Value="Albania"></asp:ListItem>
<asp:ListItem Value="Algeria"></asp:ListItem>
<asp:ListItem Value="Andorra"></asp:ListItem>
<asp:ListItem Value="Angola"></asp:ListItem>
<asp:ListItem Value="Antigua & Deps"></asp:ListItem>
<asp:ListItem Value="Argentina"></asp:ListItem>
<asp:ListItem Value="Armenia"></asp:ListItem>
<asp:ListItem Value="Australia"></asp:ListItem>
<asp:ListItem Value="Austria"></asp:ListItem>
<asp:ListItem Value="Azerbaijan"></asp:ListItem>
<asp:ListItem Value="Bahamas"></asp:ListItem>
<asp:ListItem Value="Bahrain"></asp:ListItem>
<asp:ListItem Value="Bangladesh"></asp:ListItem>
<asp:ListItem Value="Barbados"></asp:ListItem>
<asp:ListItem Value="Belarus"></asp:ListItem>
<asp:ListItem Value="Belgium"></asp:ListItem>
<asp:ListItem Value="Belize"></asp:ListItem>
<asp:ListItem Value="Benin"></asp:ListItem>
<asp:ListItem Value="Bhutan"></asp:ListItem>
<asp:ListItem Value="Bolivia"></asp:ListItem>
<asp:ListItem Value="Bosnia Herzegovina"></asp:ListItem>
<asp:ListItem Value="Botswana"></asp:ListItem>
<asp:ListItem Value="Brazil"></asp:ListItem>
<asp:ListItem Value="Brunei"></asp:ListItem>
<asp:ListItem Value="Bulgaria"></asp:ListItem>
<asp:ListItem Value="Burkina"></asp:ListItem>
<asp:ListItem Value="Burundi"></asp:ListItem>
<asp:ListItem Value="Cambodia"></asp:ListItem>
<asp:ListItem Value="Cameroon"></asp:ListItem>
<asp:ListItem Value="Canada"></asp:ListItem>
<asp:ListItem Value="Cape Verde"></asp:ListItem>
<asp:ListItem Value="Central African Rep"></asp:ListItem>
<asp:ListItem Value="Chad"></asp:ListItem>
<asp:ListItem Value="Chile"></asp:ListItem>
<asp:ListItem Value="China"></asp:ListItem>
<asp:ListItem Value="Colombia"></asp:ListItem>
<asp:ListItem Value="Comoros"></asp:ListItem>
<asp:ListItem Value="Congo"></asp:ListItem>
<asp:ListItem Value="Congo {Democratic Rep}"></asp:ListItem>
<asp:ListItem Value="Costa Rica"></asp:ListItem>
<asp:ListItem Value="Croatia"></asp:ListItem>
<asp:ListItem Value="Cuba"></asp:ListItem>
<asp:ListItem Value="Cyprus"></asp:ListItem>
<asp:ListItem Value="Czech Republic"></asp:ListItem>
<asp:ListItem Value="Denmark"></asp:ListItem>
<asp:ListItem Value="Djibouti"></asp:ListItem>
<asp:ListItem Value="Dominica"></asp:ListItem>
<asp:ListItem Value="Dominican Republic"></asp:ListItem>
<asp:ListItem Value="East Timor"></asp:ListItem>
<asp:ListItem Value="Ecuador"></asp:ListItem>
<asp:ListItem Value="Egypt"></asp:ListItem>
<asp:ListItem Value="El Salvador"></asp:ListItem>
<asp:ListItem Value="Equatorial Guinea"></asp:ListItem>
<asp:ListItem Value="Eritrea"></asp:ListItem>
<asp:ListItem Value="Estonia"></asp:ListItem>
<asp:ListItem Value="Ethiopia"></asp:ListItem>
<asp:ListItem Value="Fiji"></asp:ListItem>
<asp:ListItem Value="Finland"></asp:ListItem>
<asp:ListItem Value="France"></asp:ListItem>
<asp:ListItem Value="Gabon"></asp:ListItem>
<asp:ListItem Value="Gambia"></asp:ListItem>
<asp:ListItem Value="Georgia"></asp:ListItem>
<asp:ListItem Value="Germany"></asp:ListItem>
<asp:ListItem Value="Ghana"></asp:ListItem>
<asp:ListItem Value="Greece"></asp:ListItem>
<asp:ListItem Value="Grenada"></asp:ListItem>
<asp:ListItem Value="Guatemala"></asp:ListItem>
<asp:ListItem Value="Guinea"></asp:ListItem>
<asp:ListItem Value="Guinea-Bissau"></asp:ListItem>
<asp:ListItem Value="Guyana"></asp:ListItem>
<asp:ListItem Value="Haiti"></asp:ListItem>
<asp:ListItem Value="Honduras"></asp:ListItem>
<asp:ListItem Value="Hungary"></asp:ListItem>
<asp:ListItem Value="Iceland"></asp:ListItem>
<asp:ListItem Value="India"></asp:ListItem>
<asp:ListItem Value="Indonesia"></asp:ListItem>
<asp:ListItem Value="Iran"></asp:ListItem>
<asp:ListItem Value="Iraq"></asp:ListItem>
<asp:ListItem Value="Ireland {Republic}"></asp:ListItem>
<asp:ListItem Value="Israel"></asp:ListItem>
<asp:ListItem Value="Italy"></asp:ListItem>
<asp:ListItem Value="Ivory Coast"></asp:ListItem>
<asp:ListItem Value="Jamaica"></asp:ListItem>
<asp:ListItem Value="Japan"></asp:ListItem>
<asp:ListItem Value="Jordan"></asp:ListItem>
<asp:ListItem Value="Kazakhstan"></asp:ListItem>
<asp:ListItem Value="Kenya"></asp:ListItem>
<asp:ListItem Value="Kiribati"></asp:ListItem>
<asp:ListItem Value="Korea North"></asp:ListItem>
<asp:ListItem Value="Korea South"></asp:ListItem>
<asp:ListItem Value="Kosovo"></asp:ListItem>
<asp:ListItem Value="Kuwait"></asp:ListItem>
<asp:ListItem Value="Kyrgyzstan"></asp:ListItem>
<asp:ListItem Value="Laos"></asp:ListItem>
<asp:ListItem Value="Latvia"></asp:ListItem>
<asp:ListItem Value="Lebanon"></asp:ListItem>
<asp:ListItem Value="Lesotho"></asp:ListItem>
<asp:ListItem Value="Liberia"></asp:ListItem>
<asp:ListItem Value="Libya"></asp:ListItem>
<asp:ListItem Value="Liechtenstein"></asp:ListItem>
<asp:ListItem Value="Lithuania"></asp:ListItem>
<asp:ListItem Value="Luxembourg"></asp:ListItem>
<asp:ListItem Value="Macedonia"></asp:ListItem>
<asp:ListItem Value="Madagascar"></asp:ListItem>
<asp:ListItem Value="Malawi"></asp:ListItem>
<asp:ListItem Value="Malaysia"></asp:ListItem>
<asp:ListItem Value="Maldives"></asp:ListItem>
<asp:ListItem Value="Mali"></asp:ListItem>
<asp:ListItem Value="Malta"></asp:ListItem>
<asp:ListItem Value="Marshall Islands"></asp:ListItem>
<asp:ListItem Value="Mauritania"></asp:ListItem>
<asp:ListItem Value="Mauritius"></asp:ListItem>
<asp:ListItem Value="Mexico"></asp:ListItem>
<asp:ListItem Value="Micronesia"></asp:ListItem>
<asp:ListItem Value="Moldova"></asp:ListItem>
<asp:ListItem Value="Monaco"></asp:ListItem>
<asp:ListItem Value="Mongolia"></asp:ListItem>
<asp:ListItem Value="Montenegro"></asp:ListItem>
<asp:ListItem Value="Morocco"></asp:ListItem>
<asp:ListItem Value="Mozambique"></asp:ListItem>
<asp:ListItem Value="Myanmar, {Burma}"></asp:ListItem>
<asp:ListItem Value="Namibia"></asp:ListItem>
<asp:ListItem Value="Nauru"></asp:ListItem>
<asp:ListItem Value="Nepal"></asp:ListItem>
<asp:ListItem Value="Netherlands"></asp:ListItem>
<asp:ListItem Value="New Zealand"></asp:ListItem>
<asp:ListItem Value="Nicaragua"></asp:ListItem>
<asp:ListItem Value="Niger"></asp:ListItem>
<asp:ListItem Value="Nigeria"></asp:ListItem>
<asp:ListItem Value="Norway"></asp:ListItem>
<asp:ListItem Value="Oman"></asp:ListItem>
<asp:ListItem Value="Pakistan"></asp:ListItem>
<asp:ListItem Value="Palau"></asp:ListItem>
<asp:ListItem Value="Panama"></asp:ListItem>
<asp:ListItem Value="Papua New Guinea"></asp:ListItem>
<asp:ListItem Value="Paraguay"></asp:ListItem>
<asp:ListItem Value="Peru"></asp:ListItem>
<asp:ListItem Value="Philippines"></asp:ListItem>
<asp:ListItem Value="Poland"></asp:ListItem>
<asp:ListItem Value="Portugal"></asp:ListItem>
<asp:ListItem Value="Qatar"></asp:ListItem>
<asp:ListItem Value="Romania"></asp:ListItem>
<asp:ListItem Value="Russian Federation"></asp:ListItem>
<asp:ListItem Value="Rwanda"></asp:ListItem>
<asp:ListItem Value="St Kitts & Nevis"></asp:ListItem>
<asp:ListItem Value="St Lucia"></asp:ListItem>
<asp:ListItem Value="Saint Vincent & the Grenadines"></asp:ListItem>
<asp:ListItem Value="Samoa"></asp:ListItem>
<asp:ListItem Value="San Marino"></asp:ListItem>
<asp:ListItem Value="Sao Tome & Principe"></asp:ListItem>
<asp:ListItem Value="Saudi Arabia"></asp:ListItem>
<asp:ListItem Value="Senegal"></asp:ListItem>
<asp:ListItem Value="Serbia"></asp:ListItem>
<asp:ListItem Value="Seychelles"></asp:ListItem>
<asp:ListItem Value="Sierra Leone"></asp:ListItem>
<asp:ListItem Value="Singapore"></asp:ListItem>
<asp:ListItem Value="Slovakia"></asp:ListItem>
<asp:ListItem Value="Slovenia"></asp:ListItem>
<asp:ListItem Value="Solomon Islands"></asp:ListItem>
<asp:ListItem Value="Somalia"></asp:ListItem>
<asp:ListItem Value="South Africa"></asp:ListItem>
<asp:ListItem Value="Spain"></asp:ListItem>
<asp:ListItem Value="Sri Lanka"></asp:ListItem>
<asp:ListItem Value="Sudan"></asp:ListItem>
<asp:ListItem Value="Suriname"></asp:ListItem>
<asp:ListItem Value="Swaziland"></asp:ListItem>
<asp:ListItem Value="Sweden"></asp:ListItem>
<asp:ListItem Value="Switzerland"></asp:ListItem>
<asp:ListItem Value="Syria"></asp:ListItem>
<asp:ListItem Value="Taiwan"></asp:ListItem>
<asp:ListItem Value="Tajikistan"></asp:ListItem>
<asp:ListItem Value="Tanzania"></asp:ListItem>
<asp:ListItem Value="Thailand"></asp:ListItem>
<asp:ListItem Value="Togo"></asp:ListItem>
<asp:ListItem Value="Tonga"></asp:ListItem>
<asp:ListItem Value="Trinidad & Tobago"></asp:ListItem>
<asp:ListItem Value="Tunisia"></asp:ListItem>
<asp:ListItem Value="Turkey"></asp:ListItem>
<asp:ListItem Value="Turkmenistan"></asp:ListItem>
<asp:ListItem Value="Tuvalu"></asp:ListItem>
<asp:ListItem Value="Uganda"></asp:ListItem>
<asp:ListItem Value="Ukraine"></asp:ListItem>
<asp:ListItem Value="United Arab Emirates"></asp:ListItem>
<asp:ListItem Value="United Kingdom"></asp:ListItem>
<asp:ListItem Value="United States"></asp:ListItem>
<asp:ListItem Value="Uruguay"></asp:ListItem>
<asp:ListItem Value="Uzbekistan"></asp:ListItem>
<asp:ListItem Value="Vanuatu"></asp:ListItem>
<asp:ListItem Value="Vatican City"></asp:ListItem>
<asp:ListItem Value="Venezuela"></asp:ListItem>
<asp:ListItem Value="Vietnam"></asp:ListItem>
<asp:ListItem Value="Yemen"></asp:ListItem>
<asp:ListItem Value="Zambia"></asp:ListItem>
<asp:ListItem Value="Zimbabwe"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="PasswordLabel1" runat="server" AssociatedControlID="Password">E-mail:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;
            <asp:Label ID="PasswordLabel2" runat="server" AssociatedControlID="Password">City:</asp:Label>
            <br />
            <asp:TextBox ID="Email" runat="server" CssClass="passwordEntry"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="City" runat="server" CssClass="textEntry"></asp:TextBox>
            <br />
            <asp:Label ID="PasswordLabel4" runat="server" AssociatedControlID="Password">Sex:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            BitrhDay:<br />&nbsp;<asp:DropDownList ID="Sex" runat="server" 
                style="height: 22px">
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Birthday" runat="server"></asp:TextBox>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:Button ID="registerButton" OnClientClick=" return validate()" 
                runat="server" onclick="registerButton_Click" 
                Text="Register" />
            <div style="position:absolute;  color:Red; font-size:small; font-weight:normal;" 
                id="DivError"></div>
        </asp:Panel></div>
        <br />
    </asp:Panel>
</asp:Content>

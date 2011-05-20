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
        <br />
        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" 
            ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
        <br />
        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" 
                            TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
            ControlToValidate="Password" CssClass="failureNotification" 
            ErrorMessage="Password is required." ToolTip="Password is required." 
            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
        <br />
        <br />
        <br />
    </asp:Panel>
    <br />
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" 
        onclick="LoginButton_Click" style="height: 26px"/>
                </asp:Content>

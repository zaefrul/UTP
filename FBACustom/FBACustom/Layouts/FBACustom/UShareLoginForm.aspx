<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UShareLoginForm.aspx.cs" Inherits="FBACustom.Layouts.FBACustom.UShareLoginForm" MasterPageFile="~/_layouts/15/errorv15.master" %>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitle" runat="server" Visible="false">
    <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageTitle" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <link rel="icon" type="image/png" href="images/icons/favicon.ico"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="css/util.css">
	<link rel="stylesheet" type="text/css" href="css/main.css">
    <div id="SslWarning" style="color: red; display: none">
        <asp:Label runat="server" Text="This is a prove custom wrokds"></asp:Label>
        <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageMessage" />
    </div>
    <%--<script language="javascript">
        if (document.location.protocol != 'https:') {
            var SslWarning = document.getElementById('SslWarning');
            SslWarning.style.display = '';
        }
    </script>--%>
    <asp:Login ID="signInControl" FailureText="<%$Resources:wss,login_pageFailureText%>" runat="server" Width="100%">
        <LayoutTemplate>

            <div class="limiter" id="formloginmain" style="display:none;">
                <div class="container-login100">
                    <div class="wrap-login100">
                        <div class="login100-right">
                            <div class="logo" style="background-image: url('images/UTP-logo.png'); background-size: 50%; background-position: center; background-repeat: no-repeat; size:cover; height:250px; width:100%;"></div>
                            <div class="login100-form">
                                <span class="login100-form-title p-b-34">Account Login
                                </span>
                                <div class="w-full text-center">
                                    <asp:Label ID="FailureText" class="ms-error" runat="server" />
                                </div>
                                <div class="wrap-input100 rs1-wrap-input100 validate-input m-b-20">
                                    <%--<input id="first-name" class="input100" type="text" name="username" placeholder="User name">--%>
                                    <asp:TextBox ID="UserName" placeholder="Username" autocomplete="off" runat="server" class="input100" Width="99%" />
                                    <span class="focus-input100"></span>
                                </div>
                                <div class="wrap-input100 rs2-wrap-input100 validate-input m-b-20">
                                    <%--<input class="input100" type="password" name="pass" placeholder="Password">--%>
                                    <asp:TextBox ID="password" placeholder="Password" TextMode="Password" autocomplete="off" runat="server" class="input100" Width="99%" />
                                    <span class="focus-input100"></span>
                                </div>

                                <div class="container-login100-form-btn">
                                    <%--<button class="login100-form-btn">
                                        Sign in
                                    </button>--%>
                                     <asp:Button ID="login" class="login100-form-btn" CommandName="Login" Text="<%$Resources:wss,login_pagetitle%>" runat="server" />
                                </div>
                                <div class="w-full text-center p-t-27 p-b-239">
							        <span class="txt1">
								        Sign in using
							        </span>
                                    <a href="/_login/default.aspx" id="windowtoken" class="txt2">
								        windows user
							        </a>
						        </div>
                                <div class="w-full text-center">
								        © Microsoft 2013
						        </div>
                            </div>
                        </div>
                        
                        <div class="login100-more" style="background-image: url('images/UshareLogin.jpg');"></div>
                    </div>
                </div>
            </div>



            <div id="dropDownSelect1"></div>





            <%--<table width="100%" border="2">
                <tr>
                    <td nowrap="nowrap">
                        <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,login_pageUserName%>" EncodeMethod='HtmlEncode' /></td>
                    <td width="100%">
                        </td>
                </tr>
                <tr>
                    <td nowrap="nowrap">
                        <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,login_pagePassword%>" EncodeMethod='HtmlEncode' /></td>
                    <td width="100%">
                        </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                       </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="RememberMe" Text="<%$SPHtmlEncodedResources:wss,login_pageRememberMe%>" runat="server" /></td>
                </tr>
            </table>--%>
        </LayoutTemplate>
    </asp:Login>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById("ms-error-header").setAttribute("style", "display:none");
            function parse_query_string(query) {
                var vars = query.split("&");
                var query_string = {};
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    var key = decodeURIComponent(pair[0]);
                    var value = decodeURIComponent(pair[1]);
                    // If first entry with this name
                    if (typeof query_string[key] === "undefined") {
                        query_string[key] = decodeURIComponent(value);
                        // If second entry with this name
                    } else if (typeof query_string[key] === "string") {
                        var arr = [query_string[key], decodeURIComponent(value)];
                        query_string[key] = arr;
                        // If third or later entry with this name
                    } else {
                        query_string[key].push(decodeURIComponent(value));
                    }
                }
                return query_string;
            }
            var query_string = window.location.search.substring(1);
		    var parsed_qs = parse_query_string(query_string);
            
            var parameter = parsed_qs.ReturnUrl != null ? parsed_qs.ReturnUrl : "";
            var a = document.getElementById("windowtoken"); 
            a.href = "/_login/default.aspx?ReturnUrl=" + parameter;
            document.getElementById("formloginmain").removeAttribute("style");
        }
    </script>
</asp:Content>

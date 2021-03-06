﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MegaDropdown.ascx.cs" Inherits="UTP.MegaDropdown.MegaDropdown" %>
<div id="megaDropdown" class="col-megaNav">
    <section class="mega-sec-one">
        <asp:Literal runat="server" ID="MegaSec1"></asp:Literal>
    </section>
    <section class="mega-sec-two">
        <asp:Literal runat="server" ID="MegaSec2"></asp:Literal>
    </section>
    <section class="mega-sec-three">
        <asp:Literal runat="server" ID="MegaSec3"></asp:Literal>
    </section>
    <section class="mega-sec-four">
        <asp:Literal runat="server" ID="MegaSec4"></asp:Literal>
    </section>
</div>
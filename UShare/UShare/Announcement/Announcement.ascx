<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Announcement.ascx.cs" Inherits="UShare.Announcement.Announcement" %>
<%--<link href="../_layouts/15/UShare/news.css" rel="stylesheet" />--%>
<section class="sec-announcements">
    <section class="sec-announcements-title">
        <asp:Label runat="server" ID="WPTitle"></asp:Label></section>
    <section class="sec-announcements-subTitle">
        <p>
            <asp:Label runat="server" ID="WPSubTitle"></asp:Label></p>
    </section>
    <asp:Literal runat="server" ID="ltrTheLatest"></asp:Literal>

    <section class="sec-announcements-more">
        <a runat="server" id="btnMore">
            <section class="sec-announcements-btn">Read More <i class="fas fa-angle-right fa-more-btn"></i></section>
        </a>
    </section>
</section>

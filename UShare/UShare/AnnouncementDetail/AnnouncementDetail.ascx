<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnnouncementDetail.ascx.cs" Inherits="UShare.AnnouncementDetail.AnnouncementDetail" %>
<section class="sec-announcements-detail">
    <section class="sec-announcements-detail-title">Announcement Detail</section>
    <section class="sec-announcements-detail-row">
        <section class="sec-announcements-detail-date">
            <div class="month"><asp:Literal runat="server" ID="Month"></asp:Literal></div>
            <div class="day"><asp:Literal runat="server" ID="Day"></asp:Literal></div>
            <div class="year"><asp:Literal runat="server" ID="Year"></asp:Literal></div>
        </section>
        <section class="sec-announcements-detail-name">
            <asp:Literal runat="server" ID="ATitle"></asp:Literal>
        </section>
        <section class="sec-announcements-detail-Author">
            Created by <b><asp:Literal runat="server" ID="CreatedBy" /></b> at <b><asp:Literal runat="server" ID="CreatedText" /></b>
        </section>
        <section class="sec-announcements-detail-body">
            <asp:Literal runat="server" ID="Body"></asp:Literal>
        </section>

        <section class="sec-announcements-detail-more">
            <a href="#" runat="server" id="BackURL">
                <section class="sec-announcements-detail-btn"><i class="fas fa-arrow-left"></i>Back To Listing</section>
            </a>
        </section>
    </section>
</section>

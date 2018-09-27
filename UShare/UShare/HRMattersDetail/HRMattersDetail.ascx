<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HRMattersDetail.ascx.cs" Inherits="UShare.HRMattersDetail.HRMattersDetail" %>
<section class="sec-hrmatters-detail">
    <section class="sec-hrmatters-detail-title">HR Matters Detail</section>
    <section class="sec-hrmatters-detail-row">
        <section class="sec-hrmatters-detail-date">
            <div class="month"><asp:Literal runat="server" ID="Month" /></div>
            <div class="day"><asp:Literal runat="server" ID="Day" /></div>
            <div class="year"><asp:Literal runat="server" ID="Year" /></div>
        </section>
        <section class="sec-hrmatters-detail-name">
            <asp:Literal runat="server" ID="Title" />
        </section>
        <section class="sec-hrmatters-detail-Author">
            Created by <b><asp:Literal runat="server" ID="Author" /></b> at <b><asp:Literal runat="server" ID="CreatedTime" /></b>
        </section>
        <section class="sec-hrmatters-detail-body">
            <asp:Literal runat="server" ID="Body" />
        </section>

        <section class="sec-hrmatters-detail-more">
            <a runat="server" id="ListURL" href="#">
                <section class="sec-hrmatters-detail-btn"><i class="fas fa-arrow-left"></i>Back To Listing</section>
            </a>
        </section>

    </section>


</section>

<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HSEnvDetail.ascx.cs" Inherits="UShare.HSEnvDetail.HSEnvDetail" %>
<section class="sec-hse-detail">
    <section class="sec-hse-detail-title">
        <asp:Label runat="server" ID="WPTitle"></asp:Label>
    </section>
    <section class="sec-hse-detail-row">
        <section class="sec-hse-detail-image" runat="server" id="SectionImage" style="background-image: url(image/admission.jpg)">
        </section>
        <section class="sec-hse-detail-date">
            <i class="far fa-calendar-alt"></i><asp:Literal runat="server" ID="Date"></asp:Literal>
        </section>
        <section class="sec-hse-detail-name" runat="server" id="Title"></section>
        <section class="sec-hse-detail-body">
            <asp:Literal runat="server" ID="Body" ></asp:Literal>
        </section>

        <section class="sec-hse-detail-more">
            <a href="Ushare%20HSE.html" runat="server" id="ListsURL">
                <section class="sec-hse-detail-btn"><i class="fas fa-arrow-left"></i>Back To Listing</section>
            </a>
        </section>

    </section>


</section>

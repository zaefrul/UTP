<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpcomingEvents.ascx.cs" Inherits="UShare.UpcomingEvents.UpcomingEvents" %>
<%--<link href="../_layouts/15/UShare/event.css" rel="stylesheet" />--%>
<section class="sec-events">
    <section class="sec-events-title">
        <asp:Label runat="server" ID="Title" />
    </section>
    <section class="sec-events-subTitle">
        <asp:Label runat="server" ID="SubTitle" />
    </section>
    <asp:Literal runat="server" ID="ltrEvent"></asp:Literal>
    <section class="sec-events-more">
        <a runat="server" id="showmoreLink">
            <section class="sec-events-btn">Show More <i class="fas fa-angle-right fa-more-btn"></i></section>
        </a>
    </section>
</section>
<script type="text/JavaScript">	
    function megaFunction() {
        var x = document.getElementById("megaDropdown");
        if (x.style.display === "block") {
            x.style.display = "none";

        } else {
            x.style.display = "block";
        }
    }

    function searchFunction() {
        var x = document.getElementById("searchDropdown");
        if (x.style.display === "table") {
            x.style.display = "none";

        } else {
            x.style.display = "table";
        }
    }
</script>

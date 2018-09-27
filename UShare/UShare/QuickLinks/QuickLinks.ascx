<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickLinks.ascx.cs" Inherits="UShare.QuickLinks.QuickLinks" %>
<%--<link href="../_layouts/15/UShare/quicklink.css" rel="stylesheet" />--%>
<div class="col-link">
    <div class="link-content">
        <section class="sec-one">
            <asp:Literal runat="server" ID="QuickLink1" />
        </section>
        <section class="sec-two">
            <asp:Literal runat="server" ID="QuickLink2" />
        </section>
        <section class="sec-three">
            <asp:Literal runat="server" ID="QuickLink3" />
        </section>
        <section class="sec-four">
            <asp:Literal runat="server" ID="QuickLink4" />
        </section>
    </div>
</div>
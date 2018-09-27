<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpcomingEventsList.ascx.cs" Inherits="UTP.UpcomingEventsList.UpcomingEventsList" %>


<section class="sec-event-listing">
	<section class="sec-event-listing-title"><asp:Literal runat="server" ID="ltrTitle" /></section>
	<section class="sec-event-listing-subTitle">
		<p><asp:Literal runat="server" ID="ltrSubTitle" /></p>
	</section>

    <asp:Literal runat="server" ID="ltrRow" />
			

<section class="sec-event-listing-paginated">
	<section class="sec-event-listing-paginated-column">
        <asp:Literal runat="server" ID="ltrPaginated" />

	</section>
</section>
				
</section>
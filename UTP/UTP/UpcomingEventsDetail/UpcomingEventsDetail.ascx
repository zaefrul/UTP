<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpcomingEventsDetail.ascx.cs" Inherits="UTP.UpcomingEventsDetail.UpcomingEventsDetail" %>

<section class="sec-event-detail">
	<section class="sec-event-detail-title"><asp:Literal runat="server" ID="ltrTitle" /></section>
				
	<section class="sec-event-detail-row">
		<section class="sec-event-detail-date">
			<div class="day"><asp:Literal runat="server" ID="ltrDay" /></div> 
			<div class="month"><asp:Literal runat="server" ID="ltrMonth" /></div> 
		</section>
		<section class="sec-event-detail-location"><i class="fas fa-map-marker-alt iconPin"></i> <asp:Literal runat="server" ID="ltrLocation" /></section>
		<section class="sec-event-detail-time"><i class="far fa-clock iconClock"></i> <asp:Literal runat="server" ID="ltrTime" /></section>
		<section class="sec-event-detail-name">
			<asp:Literal runat="server" ID="ltrName" />
		</section>
		<section class="sec-event-detail-body"><asp:Literal runat="server" ID="ltrBody" /></section>
		<section class="sec-event-detail-more">
            <asp:Literal runat="server" ID="ltrMore" />
		</section>
	</section>
				
</section>
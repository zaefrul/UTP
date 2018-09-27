<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpcomingEventsList.ascx.cs" Inherits="UShare.UpcomingEventsList.UpcomingEventsList" %>
<%--<link href="../_layouts/15/UShare/event.css" rel="stylesheet" />--%>
<div class="col-segment col-segment-padding">
    <section class="sec-event-listing">
        <section class="sec-event-listing-title" runat="server" id="PaginatedTitle">Upcoming Events</section>
        <section class="sec-event-listing-subTitle" >
            <p runat="server" id="PaginatedSubTitle">Lorem ipsum dolor sit amet</p>
        </section>

        <asp:Literal runat="server" ID="Output"></asp:Literal>

        <section class="sec-event-listing-paginated">
            <section class="sec-event-listing-paginated-column">
                <asp:Literal runat="server" ID="PaginatedButton"></asp:Literal>
            </section>
        </section>
    </section>

    <asp:PlaceHolder runat="server" ID="DetailView" Visible="false">
        <section class="sec-event-listing-popup">
				<section class="sec-event-listing-popup-content">
					<section class="sec-event-popup-btn-close"><asp:LinkButton runat="server" ID="Detail_Close_Lb" OnClick="Detail_Close_Click">Test</asp:LinkButton></section>
					<section class="sec-event-listing-date">
						<div class="day"><asp:Literal runat="server" ID="Detail_Day" /></div> 
						<div class="month"><asp:Literal runat="server" ID="Detail_Month" /></div> 
					</section>
					<section class="sec-event-listing-name">
						<a href="#" class=""><asp:Literal runat="server" ID="Detail_Title" /></a>
					</section>
					<section class="sec-event-listing-summary"><asp:Literal runat="server" ID="Detail_Summary" /></section>
				</section>
			</section>
    </asp:PlaceHolder>
</div>

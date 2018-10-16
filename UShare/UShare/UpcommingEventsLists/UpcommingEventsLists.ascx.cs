using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace UShare.UpcommingEventsLists
{
    [ToolboxItemAttribute(false)]
    public partial class UpcommingEventsLists : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }

        [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public uint RowLimit { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public UpcommingEventsLists()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string output = "";
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                string date = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
                SPQuery Query = new SPQuery()
                {
                    Query = @"<Where><Geq><FieldRef Name='EventDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"+ date + "</Value></Geq></Where><OrderBy><FieldRef Name='EventDate' Ascending='True' /></OrderBy>",
                    ViewFields = "<FieldRef Name='Title' /><FieldRef Name='EventDate' /><FieldRef Name='Description' /><FieldRef Name='Location' />",
                    RowLimit = RowLimit
                };
                SPListItemCollection Items = Web.Lists[ListName].GetItems(Query);
                int dataid = 0;
                foreach (SPListItem Item in Items)
                {
                    DateTime StartEvent = DateTime.Parse(Item["EventDate"].ToString());
                    output += "<section class=\"sec-event-list-row\">";
                    output += "<section class=\"sec-event-list-row-content\" data-id=\""+ ++dataid +"\">";
                    output += "<section class=\"sec-event-list-date\">";
                    output += "<div class=\"day\">" + StartEvent.ToString("dd MMMM yyyy") + "</div>";
                    output += "</section>";
                    output += "<section class=\"sec-event-list-name\">" + Item["Title"].ToString() + "</section>";
                    output += "</section>";
                    output += "<section class=\"sec-event-list-detail\" data-id=\""+ dataid +"\">";
                    output += "<div class=\"location\"><i class=\"fas fa-map-marker-alt iconPin\"></i>" + Item["Location"] != null ? Item["Location"].ToString() : "No location" + " | <i class=\"far fa-clock iconClock\"></i>" + StartEvent.ToString("hh:mm tt") + "</div>";
                    output += "<div class=\"body\">" + Item["Description"] != null ? Item["Description"].ToString() : string.Empty + "</div>";
                    output += "</section>";
                    output += "</section>";
                }
                UPRow.Text = output;
            }
            catch (Exception ex)
            {
                UPRow.Text = ex.Message;
            }
        }
    }
}

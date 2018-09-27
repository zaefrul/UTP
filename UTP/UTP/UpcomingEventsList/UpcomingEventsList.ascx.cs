using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UTP.UpcomingEventsList
{
    [ToolboxItemAttribute(false)]
    public partial class UpcomingEventsList : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]

        [WebBrowsable(true), WebDisplayName("Component Title"), WebDescription("Component Title"), Personalizable(PersonalizationScope.Shared)]
        public string PageTitle { get; set; }
        [WebBrowsable(true), WebDisplayName("Component Sub-Title"), WebDescription("Component Sub-Title"), Personalizable(PersonalizationScope.Shared)]
        public string PageSubTitle { get; set; }
        [WebBrowsable(true), WebDisplayName("Component List Name"), WebDescription("Component List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Component Row Limit"), WebDescription("Component Row Limit"), Personalizable(PersonalizationScope.Shared)]
        public int RowLimit { get; set; }
        [WebBrowsable(true), WebDisplayName("Component List Page URL"), WebDescription("Component List Page URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailPageUrl { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata site url"), WebDescription("Metadata site url"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }

        private SPListItemCollection List { get; set; }
        public UpcomingEventsList()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Output = "";
            try
            {
                ltrTitle.Text = PageTitle;
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                string date = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
                SPListItemCollection events = Web.Lists[ListName].GetItems(new SPQuery()
                {
                    Query = "<Where><Geq><FieldRef Name='Expires' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"+date+"</Value></Geq></Where><OrderBy><FieldRef Name='Expires' Ascending='True' /></OrderBy>",
                    RowLimit = uint.Parse(RowLimit.ToString())
                });
                int index = 0;
                var Page = HttpContext.Current.Request.Params["page"] != null ? HttpContext.Current.Request.Params["page"].Trim() != "1" ? HttpContext.Current.Request.Params["page"].Trim() : "" : "";
                if (Page != "")
                {
                    index = (Int32.Parse(Page) - 1) * RowLimit;
                }
                int loopbreaker = index + RowLimit;
                for (int i = index; i < loopbreaker; i++)
                {
                    if (i >= events.Count)
                    {
                        break;
                    }
                    Output += StructureBuilder(events[i]);
                }
                string POutput = "<a href='?page=1'><section class='sec-event-listing-paginated-btn'>1</section></a>";

                if (events.Count > RowLimit)
                {
                    int paginated = events.Count % RowLimit > 0 ? 1 : 0;
                    paginated += events.Count / RowLimit;
                    for (int i = 2; i <= paginated; i++)
                    {
		                POutput += string.Format("<a href='?page={0}'><section class='sec-event-listing-paginated-btn'>{0}</section></a>",i);
                    }
                }
                ltrPaginated.Text = POutput;
            }
            catch(Exception ex)
            {
                Output = "<div>" + ex.Message + "</div>";
            }
            ltrRow.Text = Output;
        }

        private string StructureBuilder(SPListItem Item)
        {
            string Output;
            DateTime date = DateTime.Parse(Item["Expires"].ToString());
            string URL = DetailPageUrl + "?event=" + Item.ID;

            Output = string.Format("<section class='sec-event-listing-row'>");
            Output += string.Format("<section class='sec-event-listing-date'>");
            Output += string.Format("<i class='far fa-calendar-alt event-date-ico'></i>");
            Output += string.Format("<div class='day'>{0}</div>", date.ToString("dd"));
            Output += string.Format("<div class='month'>{0}</div>", date.ToString("MMM")); 
            Output += string.Format("</section>");
            Output += string.Format("<section class='sec-event-listing-name'>{1}</section>", URL, Item["Title"].ToString());
            Output += string.Format("<section class='sec-event-listing-more'>");
            Output += string.Format("<a href='{0}'><section class='sec-event-btn'>Read More <i class='fas fa-angle-right fa-more-btn'></i></section></a>", URL);
            Output += string.Format("</section>");
            Output += string.Format("</section>");

            return Output;
        }


    }
}

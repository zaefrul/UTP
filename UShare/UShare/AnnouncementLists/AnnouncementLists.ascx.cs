using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UShare.AnnouncementLists
{
    [ToolboxItemAttribute(false)]
    public partial class AnnouncementLists : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Component Title"), WebDescription("Component Title"), Personalizable(PersonalizationScope.Shared)]
        public string ComponentTitle { get; set; }

        [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }

        [WebBrowsable(true), WebDisplayName("Detail Page URL"), WebDescription("Detail Page URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailPageUrl { get; set; }

        [WebBrowsable(true), WebDisplayName("Component Row Limit"), WebDescription("Component Row Limit"), Personalizable(PersonalizationScope.Shared)]
        public int RowLimit { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public AnnouncementLists()
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
                Title.Text = ComponentTitle;
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                string date = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
                SPListItemCollection Announcements = Web.Lists[ListName].GetItems(new SPQuery()
                {
                    Query = Helper.TLQuery
                    //RowLimit = uint.Parse(RowLimit.ToString())
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
                    if (i >= Announcements.Count)
                    {
                        break;
                    }
                    Output += StructureBuilder(Announcements[i]);
                }
                string POutput = "<div class=\"paginated-no\"><a href=\"?page=1\">1</a></div>";
                if (Announcements.Count > RowLimit)
                {
                    int paginated = Announcements.Count % RowLimit > 0 ? 1 : 0;
                    paginated += Announcements.Count / RowLimit;
                    for (int i = 2; i <= paginated; i++)
                    {
                        POutput += string.Format("<div class=\"paginated-no\"><a href=\"?page={0}\">{0}</a></div>",i);
                    }
                }
                PaginatedButton.Text = POutput;
            }
            catch(Exception ex)
            {
                Output = "<div>" + ex.Message + "</div>";
            }
            OutputWeb.Text = Output;
        }

        private string StructureBuilder(SPListItem Item)
        {
            string Output;
            DateTime DAnnounce = DateTime.Parse(Item["Expires"].ToString());
            Output = string.Format("<section class=\"sec-announcements-list-row\">");
            Output += string.Format("<section class=\"sec-announcements-list-date\"><div class=\"month\">{0}</div><div class=\"day\">{1}</div><div class=\"year\">{2}</div></section>", DAnnounce.ToString("MMM"), DAnnounce.ToString("dd"), DAnnounce.ToString("yyyy"));
            Output += string.Format("<section class=\"sec-announcements-list-name\">");
            string URL = DetailPageUrl + "?announcement=" + Item.ID;
            Output += string.Format("<a href=\"{0}\" class=\"\">{1}</a>", URL, Item["Title"].ToString());
            Output += string.Format("</section></section>");
            return Output;
        }
    }
}

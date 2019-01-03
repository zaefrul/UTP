using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UShare.HSEnvLists
{
    [ToolboxItemAttribute(false)]
    public partial class HSEnvLists : WebPart
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
        public HSEnvLists()
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
                string Output = "";
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                int index = 0;
                var Page = HttpContext.Current.Request.Params["page"] != null ? HttpContext.Current.Request.Params["page"].Trim() != "1" ? HttpContext.Current.Request.Params["page"].Trim() : "" : "";
                if (Page != "")
                {
                    index = (Int32.Parse(Page) - 1) * RowLimit;
                }
                int loopbreaker = index + RowLimit;
                string query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                SPQuery Query = new SPQuery()
                {
                    Query = query,
                    ViewFields = @"<FieldRef Name='Title' /><FieldRef Name='Image' /><FieldRef Name='ID' /><FieldRef Name='Expires' />"
                };
                SPListItemCollection HSEItems = Web.Lists[ListName].GetItems(Query);
                for (int i = index; i < loopbreaker; i++)
                {
                    if (i >= HSEItems.Count)
                    {
                        break;
                    }
                    Output += StructureBuilder(HSEItems[i]);
                }
                HSERow.Text = Output;
                string POutput = "<div class=\"paginated-no\"><a href=\"?page=1\">1</a></div>";
                if (HSEItems.Count > RowLimit)
                {
                    int paginated = HSEItems.Count % RowLimit > 0 ? 1 : 0;
                    paginated += HSEItems.Count / RowLimit;
                    for (int i = 2; i <= paginated; i++)
                    {
                        POutput += string.Format("<div class=\"paginated-no\"><a href=\"?page={0}\">{0}</a></div>", i);
                    }
                }
                Paginated.Text = POutput;
            }
            catch (Exception ex)
            {
                HSERow.Text = ex.Message;
            }

        }

        private string StructureBuilder(SPListItem Item)
        {
            string structure = "";
            //SPFieldUrlValue ImageLink = new SPFieldUrlValue(Item["Image"].ToString());
            string ImageUrl = Item["Image"] != null ? (new SPFieldUrlValue(Item["Image"].ToString())).Url : Helper.NoImageURL;
            DateTime Expires = DateTime.Parse(Item["Expires"].ToString());
            structure += "<section class=\"sec-hse-list-row\">";
            structure += "<section class=\"sec-hse-list-image\" style=\"background-image:url(" + ImageUrl + ")\">";
            structure += "</section>";
            structure += "<section class=\"sec-hse-list-date\"><i class=\"far fa-calendar-alt\"></i>" + Expires.ToString("dd MMMM yyyy") + "</section>";
            structure += "<section class=\"sec-hse-list-name\">";
            structure += "<a href=\"" + DetailPageUrl + "?hseid=" + Item.ID + "\" class=\"\">" + Item["Title"].ToString() + "</a>";
            structure += "</section>";
            structure += "</section>";
            return structure;
        }
    }
}

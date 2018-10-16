using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UShare.HRMattersList
{
    [ToolboxItemAttribute(false)]
    public partial class HRMattersList : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Page Title"), WebDescription("Component Title"), Personalizable(PersonalizationScope.Shared)]
        public string ComponentTitle { get; set; }

        [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }

        [WebBrowsable(true), WebDisplayName("Detail Page URL"), WebDescription("Detail Page URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailPageUrl { get; set; }

        [WebBrowsable(true), WebDisplayName("Component Row Limit"), WebDescription("Component Row Limit"), Personalizable(PersonalizationScope.Shared)]
        public int RowLimit { get; set; }

        [WebBrowsable(true), WebDisplayName("Item Detail URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public HRMattersList()
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
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                //SPQuery Query = new SPQuery()
                //{
                //    Query = "",
                //    RowLimit = 9
                //};
                SPListItemCollection HRItems = Web.Lists[ListName].GetItems(Helper.TLQuery);
                var Page = HttpContext.Current.Request.Params["page"];
                StringBuilder sb = new StringBuilder();
                int index = 0;
                if (Page != null && Page.Trim() != "1")
                {
                    index = (Int32.Parse(Page) - 1) * RowLimit;
                }
                int loopbreaker = index + RowLimit;
                for(int i=index; i<loopbreaker;i++)
                {
                    if(i>=HRItems.Count)
                    {
                        break;
                    }
                    sb.Append(HtmlFactory(HRItems[i]));
                }
                string POutput = "<div class=\"paginated-no\"><a href=\"?page=1\">1</a></div>";
                if (HRItems.Count > RowLimit)
                {
                    int paginated = HRItems.Count % RowLimit > 0 ? 1 : 0;
                    paginated += HRItems.Count / RowLimit;
                    for (int i = 2; i <= paginated; i++)
                    {
                        POutput += string.Format("<div class=\"paginated-no\"><a href=\"?page={0}\">{0}</a></div>",i);
                    }
                }
                Paginated.Text = POutput;
                HRMattersRow.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                HRMattersRow.Text = ex.Message;
            }
        }

        private string HtmlFactory(SPListItem Item)
        {
            string section = "";
            DateTime Expires = DateTime.Parse(Item["Expires"].ToString());
            section += "<section class=\"sec-hrmatters-list-row\">";
            section += "<section class=\"sec-hrmatters-list-date\">";
            section += "<div class=\"month\">"+ Expires.ToString("MMMM") + "</div>";
            section += "<div class=\"day\">"+ Expires.ToString("dd") + "</div>";
            section += "<div class=\"year\">"+ Expires.ToString("yyyy") + "</div>";
            section += "</section>";
            section += "<section class=\"sec-hrmatters-list-name\">";
            section += "<a href=\""+ DetailPageUrl + "?hrid="+Item.ID+"\" class=\"\">"+Item["Title"].ToString()+"</a>";
            section += "</section>";
            section += "</section>";
            return section;
        }
    }
}

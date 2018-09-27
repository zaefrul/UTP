using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UTP.TheLatestList
{
    [ToolboxItemAttribute(false)]
    public partial class TheLatestList : WebPart
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
        [WebBrowsable(true), WebDisplayName("Component Detail Page"), WebDescription("Component Detail Page"), Personalizable(PersonalizationScope.Shared)]
        public string DetailPageUrl { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata site url"), WebDescription("Metadata site url"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }

        public TheLatestList()
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
                string q = $"";
                string today = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
                SPListItemCollection News = Web.Lists[ListName].GetItems(new SPQuery()
                {
                    Query = $"<Where><And><Geq><FieldRef Name='Expires' /><Value IncludeTimeValue='TRUE' Type='DateTime'>{today}</Value></Geq><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></And></Where><OrderBy><FieldRef Name='Created' Ascending='Flase' /></OrderBy>",
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
                    if (i >= News.Count)
                    {
                        break;
                    }
                    Output += StructureBuilder(News[i]);
                }
                string POutput = "<a href='?page=1'><section class='sec-news-listing-paginated-bt'>1</section></a>";
                if (News.Count > RowLimit)
                {
                    int paginated = News.Count % RowLimit > 0 ? 1 : 0;
                    paginated += News.Count / RowLimit;
                    for (int i = 2; i <= paginated; i++)
                    {
                        POutput += string.Format("<a href='?page={0}'><section class='sec-news-listing-paginated-bt'>{0}</section></a>", i);
                    }
                }
                ltrPaginatedButton.Text = POutput;
            }
            catch(Exception ex)
            {
                Output = "<div>" + ex.Message + "</div>";
            }
            ltrOutputWeb.Text = Output;
        }

        private string StructureBuilder(SPListItem Item)
        {
            string Output;
            DateTime date = DateTime.Parse(Item["Expires"].ToString());
            string URL = DetailPageUrl + "?news=" + Item.ID;
            SPFieldUrlValue Image = new SPFieldUrlValue(Item["Image"].ToString());

            Output = string.Format("<section class='sec-news-listing-row'>");
            Output += $"<section class=\"sec-news-listing-img\" style=\"background-image:url('{Image.Url}')\"></section>";
            Output += string.Format("<section class='sec-news-listing-name'>");
            Output += string.Format("<a href='{0}' class=''>{1}</a>", URL, Item["Title"].ToString());
            Output += string.Format("</section>");
            Output += string.Format("<section class='sec-news-listing-date'><i class='far fa-calendar-alt news-listing-date-ico'></i>{0}</section>",date.ToString("dd MMMM yyyy"));
            Output += string.Format("</section>");
            return Output;
        }
    }
}

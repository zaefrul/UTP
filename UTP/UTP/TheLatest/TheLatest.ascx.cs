using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UTP.TheLatest
{
    [ToolboxItemAttribute(false)]
    public partial class TheLatest : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Component Title"), WebDescription("Component Title"), Personalizable(PersonalizationScope.Shared)]
        public string PageTitle { get; set; }
        [WebBrowsable(true), WebDisplayName("Component Sub-Title"), WebDescription("Component Title"), Personalizable(PersonalizationScope.Shared)]
        public string PageSubTitle { get; set; }
        [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Component Row Limit"), WebDescription("Component Row Limit"), Personalizable(PersonalizationScope.Shared)]
        public int RowLimit { get; set; }
        [WebBrowsable(true), WebDisplayName("Item Detail URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailURL { get; set; }
        [WebBrowsable(true), WebDisplayName("View More URL"), WebDescription("View More URL"), Personalizable(PersonalizationScope.Shared)]
        public string MoreURL { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Metadata site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }

        public TheLatest()
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
                EnsureChildControls();
                ltrTitle.Text = PageTitle != null ? PageTitle : "";
                ltrSubTitle.Text = PageSubTitle != null ? PageSubTitle : "";
                string today = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
                string q = $"<Where><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                //string query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListItemCollection sPListItem = Web.Lists[ListName].GetItems(new SPQuery() { Query = q, RowLimit = uint.Parse(RowLimit.ToString()) });
                StringBuilder stringBuilder = new StringBuilder();
                //int limit = RowLimit == 0 ? RowLimit : 5;

                //for (int i = 0; i < limit; i++)
                //{
                //    DateTime date = DateTime.Parse(sPListItem[i]["Expires"].ToString());
                //    SPFieldUrlValue Image = new SPFieldUrlValue(sPListItem[i]["Image"].ToString());
                //    stringBuilder.Append("<section class='sec-news-row'>");
                //    stringBuilder.AppendFormat("<section class='sec-news-img' style='background-image: url({0})'></section>", Image.Url);
                //    stringBuilder.Append("<section class='sec-news-name'>");
                //    stringBuilder.AppendFormat("<a href ='{0}?news={1}' class=''>{2}</a>", DetailURL, sPListItem[i].ID, sPListItem[i]["Title"].ToString());
                //    stringBuilder.Append("</section></section>");
                //}

                foreach (SPListItem Item in sPListItem)
                {
                    DateTime date = DateTime.Parse(Item["Expires"].ToString());
                    SPFieldUrlValue Image = new SPFieldUrlValue(Item["Image"].ToString());
                    stringBuilder.Append("<section class='sec-news-row'>");
                    stringBuilder.AppendFormat("<section class='sec-news-img' style='background-image: url({0})'></section>", Image.Url);
                    stringBuilder.Append("<section class='sec-news-name'>");
                    stringBuilder.AppendFormat("<a href ='{0}?news={1}' class=''>{2}</a>", DetailURL, Item.ID, Item["Title"].ToString());
                    stringBuilder.Append("</section></section>");
                }
                ltrRow.Text = stringBuilder.ToString();
                ltrMore.Text = "<section class='sec-news-more'><a href='"+MoreURL+"'><section class='sec-news-btn'>Read More <i class='fas fa-angle-right fa-more-btn'></i></section></a></section>";
        
            }
            catch (Exception ex)
            {
                ltrRow.Text = ex.Message;
                //Helper.Log(ex.Message);
            }


        }
    }
}

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UTP.UpcomingEvents
{
    [ToolboxItemAttribute(false)]
    public partial class UpcomingEvents : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]

        [WebBrowsable(true), WebDisplayName("Component title"), WebDescription("Component title"), Personalizable(PersonalizationScope.Shared)]
        public string PageTitle { get; set; }

        [WebBrowsable(true), WebDisplayName("Component sub title"), WebDescription("Component sub title"), Personalizable(PersonalizationScope.Shared)]
        public string PageSubTitle { get; set; }

        [WebBrowsable(true), WebDisplayName("Component lists name"), WebDescription("Component lists name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }

        [WebBrowsable(true), WebDisplayName("Component Row Limit"), WebDescription("Component Row Limit"), Personalizable(PersonalizationScope.Shared)]
        public int RowLimit { get; set; }

        [WebBrowsable(true), WebDisplayName("Component Detail Page URL"), WebDescription("Component Detail Page URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailUrl { get; set; }

        [WebBrowsable(true), WebDisplayName("Component More Page URL"), WebDescription("Component More Page URL"), Personalizable(PersonalizationScope.Shared)]
        public string MoreURL { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Metadata site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }

        public UpcomingEvents()
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
                string q = $"<Where><Geq><FieldRef Name='Expires' /><Value IncludeTimeValue='TRUE' Type='DateTime'>{today}</Value></Geq></Where><OrderBy><FieldRef Name='EventDate' Ascending='True' /></OrderBy>";
                //string query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListItemCollection sPListItem = Web.Lists[ListName].GetItems(new SPQuery() { Query = q, RowLimit = uint.Parse(RowLimit.ToString()) });
                StringBuilder stringBuilder = new StringBuilder();
                //int limit = RowLimit == 0 ? 0 : sPListItem.Count >= RowLimit ? RowLimit : sPListItem.Count >= 5 ? 5 : sPListItem.Count;

                //for (int i = 0; i < limit; i++)
                //{
                //    DateTime date = DateTime.Parse(sPListItem[i]["Expires"].ToString());
                //    stringBuilder.Append("<section class='sec-event-row'>");
                //    stringBuilder.Append("<section class='sec-event-date'>");
                //    stringBuilder.AppendFormat("<div class='day'>{0}</div>", date.ToString("dd"));
                //    stringBuilder.AppendFormat("<div class='month'>{0}</div></section>", date.ToString("MMM"));
                //    stringBuilder.Append("<section class='sec-event-name'>");
                //    stringBuilder.AppendFormat("<a href='{0}?event={1}' class=''>{2}</a>", DetailUrl, sPListItem[i].ID, sPListItem[i]["Title"].ToString());
                //    stringBuilder.Append("</section>");
                //    stringBuilder.Append("</section>");
                //}

                foreach (SPListItem Item in sPListItem)
                {
                    DateTime date = DateTime.Parse(Item["EventDate"].ToString());
                    stringBuilder.Append("<section class='sec-event-row'>");
                    stringBuilder.Append("<section class='sec-event-date'>");
                    stringBuilder.AppendFormat("<div class='day'>{0}</div>", date.ToString("dd"));
                    stringBuilder.AppendFormat("<div class='month'>{0}</div></section>", date.ToString("MMM"));
                    stringBuilder.Append("<section class='sec-event-name'>");
                    stringBuilder.AppendFormat("<a href='{0}?event={1}' class=''>{2}</a>", DetailUrl, Item.ID, Item["Title"].ToString());
                    stringBuilder.Append("</section>");
                    stringBuilder.Append("</section>");
                }

                ltrRow.Text = stringBuilder.ToString();
                ltrMore.Text = "<section class='sec-event-more'><a href='" + MoreURL + "'><section class='sec-event-btn'>Read More <i class='fas fa-angle-right fa-more-btn'></i></section></a></section>";

            }
            catch (Exception ex)
            {
                ltrRow.Text = ex.Message;
                //Helper.Log(ex.Message);
            }


        }
    }
}

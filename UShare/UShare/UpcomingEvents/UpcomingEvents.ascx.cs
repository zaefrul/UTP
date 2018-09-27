using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace UShare.UpcomingEvents
{
    [ToolboxItemAttribute(false)]
    public partial class UpcomingEvents : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("please enter source lists name"), WebDescription("please enter source lists name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }

        [WebBrowsable(true), WebDisplayName("please enter section title"), WebDescription("please enter section title"), Personalizable(PersonalizationScope.Shared)]
        public string TitleSetting { get; set; }

        [WebBrowsable(true), WebDisplayName("please enter section sub-title"), WebDescription("please enter section sub-title"), Personalizable(PersonalizationScope.Shared)]
        public string SubTitleSetting { get; set; }

        [WebBrowsable(true), WebDisplayName("please enter section Detailed lists URL"), WebDescription("please enter section Detailed lists URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailLatestURL { get; set; }

        [WebBrowsable(true), WebDisplayName("please enter item Detailed URL"), WebDescription("please enter item Detailed URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailLink { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata site url"), WebDescription("Metadata site url"), Personalizable(PersonalizationScope.Shared)]
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
                Title.Text = TitleSetting;
                SubTitle.Text = SubTitleSetting;
                showmoreLink.HRef = DetailLatestURL;
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPQuery query = new SPQuery() { Query = Helper.UPQuery, RowLimit = 5 };
                SPListItemCollection Items = Web.Lists[ListName].GetItems(query);
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                foreach (SPListItem Item in Items)
                {
                    DateTime date = DateTime.Parse(Item[Helper.UPExpiresField].ToString());
                    stringBuilder.Append("<section class='sec-events-row'>");
                    stringBuilder.Append("<section class='sec-events-date'>");
                    stringBuilder.Append($"<div class='day'>{date.ToString("dd")}</div>");
                    stringBuilder.Append($"<div class='month'>{date.ToString("MMM")}</div></section>");
                    stringBuilder.Append("<section class='sec-events-name'>");
                    stringBuilder.Append($"<a href='{string.Format("{0}?ItemID={1}", DetailLink, Item.ID)}' class=''>{Item[Helper.UPTitleField].ToString()}</a>");
                    stringBuilder.Append("</section>");
                    stringBuilder.Append("</section>");
                }
                ltrEvent.Text = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                ltrEvent.Text = ex.Message;
            }
        }
    }
}

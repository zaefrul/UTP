using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UShare.AnnouncementDetail
{
    [ToolboxItemAttribute(false)]
    public partial class AnnouncementDetail : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("List Item URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string ListsPageUrl { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public AnnouncementDetail()
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
                int ID = 0;
                var Param = HttpContext.Current.Request.Params["announcement"];
                if (Param != null && !Int32.TryParse(Param, out ID))
                {

                }
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListItem Item = Web.Lists[ListName].Items.GetItemById(ID);
                DateTime Created = DateTime.Parse(Item["Created"].ToString());
                DateTime Expires = DateTime.Parse(Item["Expires"].ToString());
                Month.Text = Created.ToString("MMM");
                Day.Text = Created.ToString("dd");
                Year.Text = Created.ToString("yyyy");
                ATitle.Text = Item["Title"] != null ? Item["Title"].ToString() : "";
                SPFieldUserValue author = new SPFieldUserValue(Web, Item["Author"].ToString());
                CreatedBy.Text = author.User.Name;
                CreatedText.Text = Created.ToString("dd MMMM yyyy");
                Body.Text = Item["Body"] != null ? Item["Body"].ToString() : "";
                BackURL.HRef = ListsPageUrl;
            }
            catch(Exception ex)
            {
                Body.Text = ex.Message;
            }
        }
    }
}

using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UTP.TheLatestDetails
{
    [ToolboxItemAttribute(false)]
    public partial class TheLatestDetails : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]

        [WebBrowsable(true), WebDisplayName("Component Title"), WebDescription("Component Title"), Personalizable(PersonalizationScope.Shared)]
        public string PageTitle { get; set; }
        [WebBrowsable(true), WebDisplayName("Component List Name"), WebDescription("Component List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Component List Item URL"), WebDescription("Component Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string ListsPageUrl { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        
        public TheLatestDetails()
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
                var Param = HttpContext.Current.Request.Params["news"];
                if (Param != null && !Int32.TryParse(Param, out ID))
                {
                    HttpContext.Current.Response.Redirect(ListsPageUrl);
                }
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPSite Site = new SPSite(SiteCollection))
                    {
                        SPWeb Web = Site.OpenWeb();
                        Web = new SPSite(SiteCollection).OpenWeb();
                        SPListItem Item = Web.Lists[ListName].Items.GetItemById(ID);
                        DateTime Created = DateTime.Parse(Item["Created"].ToString());
                        DateTime Expires = DateTime.Parse(Item["Expires"].ToString());
                        ltrTitle.Text = PageTitle;
                        ltrName.Text = Item["Title"] != null ? Item["Title"].ToString() : "";
                        SPFieldUserValue author = new SPFieldUserValue(Web, Item["Author"].ToString());
                        //ltrAuthor.Text = author.User.Name;
                        ltrDate.Text = Expires.ToString("dd MMMM yyyy");
                        ltrBody.Text = Item["Body"] != null ? Item["Body"].ToString() : "";
                        ltrMore.Text = "<a href='" + ListsPageUrl + "'><section class='sec-news-detail-btn'><i class='fas fa-arrow-left'></i> Back To Listing</section></a>";
                    }
                });
            }
            catch (Exception ex)
            {
                ltrBody.Text = ex.Message;
            }
        }
    }
}

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UShare.HSEnvDetail
{
    [ToolboxItemAttribute(false)]
    public partial class HSEnvDetail : WebPart
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
        public HSEnvDetail()
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
                var HRid = HttpContext.Current.Request.Params["hseid"];
                int ID = 0;
                if (HRid != null && !Int32.TryParse(HRid, out ID))
                {
                    if (ID == 0)
                    {
                        SPUtility.Redirect(ListsPageUrl, SPRedirectFlags.Default, HttpContext.Current);
                    }
                }
                else
                {
                    SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                    SPListItem Item = Web.Lists[ListName].GetItemById(ID);
                    DateTime Expires = DateTime.Parse(Item["Expires"].ToString());
                    SPFieldUrlValue Image = new SPFieldUrlValue(Item["Image"].ToString());
                    string ImageUrl = Item["Image"] != null ? (new SPFieldUrlValue(Item["Image"].ToString())).Url : Helper.NoImageURL;
                    SectionImage.Style.Value = "background-image: url("+ ImageUrl + ")";
                    Date.Text = Expires.ToString("dd MMMM yyyy");
                    Title.InnerText = Item["Title"].ToString();
                    Body.InnerText = Item["Body"].ToString();
                    ListsURL.HRef = ListsPageUrl;
                }
            }
            catch (Exception ex)
            {
                Body.InnerText = ex.Message;
            }
        }
    }
}

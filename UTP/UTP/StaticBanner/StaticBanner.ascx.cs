using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace UTP.StaticBanner
{
    [ToolboxItemAttribute(false)]
    public partial class StaticBanner : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDescription("Please enter static banner list"), WebDisplayName("Enter Static Banner Lists Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDescription("Metadata Site URL"), WebDisplayName("Metadata Site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public StaticBanner()
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
                string query = @"<Where><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                SPListItem Banner = Helper.GetListItems(ListName, query, 1)[0];
                BannerTag.ImageUrl = Web.Url+"/"+Banner.Url;
            }
            catch
            {

            }
        }
    }
}

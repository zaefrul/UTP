using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UTP.QuickLinkButton
{
    [ToolboxItemAttribute(false)]
    public partial class QuickLinkButton : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]

        [WebBrowsable(true), WebDisplayName("Component List Name"), WebDescription("Component List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Metadata site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        
        public QuickLinkButton()
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
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListItemCollection sPListItem = Web.Lists[ListName].GetItems(new SPQuery() { Query = Helper.QBQuery });
                StringBuilder stringBuilder = new StringBuilder();

                foreach (SPListItem Item in sPListItem)
                {
                    SPFieldUrlValue FieldURL = new SPFieldUrlValue(Item["URL"].ToString());
                    stringBuilder.Append("<a href=\"" + FieldURL.Url + "\"><div class='link-btn'>" + FieldURL.Description + "</div></a>");

                }
                QuickLinkbutton.Text = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                QuickLinkbutton.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

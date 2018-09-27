using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UShare.HeaderNavigation
{
    [ToolboxItemAttribute(false)]
    public partial class HeaderNavigation : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Site URL"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollectionURL { get; set; }
        [WebBrowsable(true), WebDisplayName("Header Navigation List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Import JQuery"), WebDescription("Import JQuery"), Personalizable(PersonalizationScope.Shared)]
        public string Import { get; set; }
        private Dictionary<string, List<string>> SubSite = new Dictionary<string, List<string>>();
        public HeaderNavigation()
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
                SPWeb Web = new SPSite(SiteCollectionURL).OpenWeb();

                SPQuery Query = new SPQuery()
                {
                    Query = @"<Where><Eq><FieldRef Name='Active'/><Value Type='Boolean'>1</Value></Eq></Where><OrderBy><FieldRef Name='Sequence' Ascending='True' /></OrderBy>",
                    ViewFields = @"<FieldRef Name='URL' /><FieldRef Name='DivisionMenu' />"
                };
                SPListItemCollection Items = Web.Lists[ListName].GetItems(Query);
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");
                foreach (SPListItem Item in Items)
                {
                    SPFieldUrlValue FieldURL = new SPFieldUrlValue(Item["URL"].ToString());

                    if (Item["DivisionMenu"].ToString() == "True")
                    {
                        sb.AppendFormat("<li><a href='#' onclick='megaFunction()'>{0}</a></li>", FieldURL.Description);
                    }
                    else
                    {
                        sb.AppendFormat("<li><a href='{0}'>{1}</a></li>", FieldURL.Url, FieldURL.Description);
                    }
                }
                sb.Append("</ul>");
                HeaderMenu.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                HeaderMenu.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

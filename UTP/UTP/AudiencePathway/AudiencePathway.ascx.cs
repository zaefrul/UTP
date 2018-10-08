using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UTP.AudiencePathway
{
    [ToolboxItemAttribute(false)]
    public partial class AudiencePathway : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Lists Name"), WebDescription("Audience Pathaway List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Metadata site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public AudiencePathway()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*try
            {
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListItemCollection APItems = Web.Lists[ListName].Items;
                string output = "<ul>";
                foreach (SPListItem apItem in APItems)
                {
                    SPFieldUrlValue Url = new SPFieldUrlValue(apItem["URL"].ToString());
                    output += "<li><a href=\"" +Url.Url+"\">"+Url.Description+"</a></li>";
                }
                output += "</ul>";
                AudiencePathaway.Text = output;
            }
            catch (Exception ex)
            {
                AudiencePathaway.Text = ex.Message;
            }*/



            try
            {
                EnsureChildControls();
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListItemCollection sPListItem = Web.Lists[ListName].GetItems(new SPQuery() { Query = Helper.APQuery});
                StringBuilder stringBuilder = new StringBuilder();
                
                stringBuilder.Append("<ul>");
                foreach (SPListItem Item in sPListItem)
                {
                    SPFieldUrlValue FieldURL = new SPFieldUrlValue(Item["URL"].ToString());
                    string target = Item["ExternalTab"].ToString() == "True" ? "target='_blank'" : "";
                    stringBuilder.Append($"<li><a href=\"{FieldURL.Url}\" {target} >{FieldURL.Description}</a></li>");
                }
                stringBuilder.Append("</ul>");
                AudiencePathaway.Text = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                AudiencePathaway.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

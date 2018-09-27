using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UShare.Slider
{
    [ToolboxItemAttribute(false)]
    public partial class Slider : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Component Title"), WebDescription("Component Title"), Personalizable(PersonalizationScope.Shared)]
        public string ComponentTitle { get; set; }

        [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata List URL"), WebDescription("List Name"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public Slider()
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
                string query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                SPListItemCollection sPListItem = Helper.GetListItems(ListName, query);
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilderID = new StringBuilder();

                int rowNo = 1;
                foreach (SPListItem Item in sPListItem)
                {
                    SPFieldUrlValue ImageUrl = new SPFieldUrlValue(Item["FileRef"].ToString());
                    string Photo = Item["FileRef"] != null ? ImageUrl.Url : Helper.NoImageURL;

                    stringBuilder.AppendFormat("<img class='mySlides' style='background:url({0});background-size:cover;'>", Photo);

                    //stringBuilderID.AppendFormat("<span class='w3-badge demo w3-border w3-transparent w3-hover-white' onclick='currentDiv({0})'></span>", rowNo);
                    stringBuilderID.AppendFormat("<div class='w3-badge demo w3-border w3-transparent w3-hover-white' onclick='currentDiv({0})'><i class='fas fa-circle'></i></div>", rowNo);

                    rowNo++;

                }
                ltrSlider.Text = stringBuilder.ToString();
                ltrSliderID.Text = stringBuilderID.ToString();
            }
            catch (Exception ex)
            {
                ltrSlider.Text = ex.Message;
                ltrSliderID.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

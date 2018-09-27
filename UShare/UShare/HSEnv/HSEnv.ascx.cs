using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UShare.HSEnv
{
    [ToolboxItemAttribute(false)]
    public partial class HSEnv : WebPart
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

        [WebBrowsable(true), WebDisplayName("Source List URL"), WebDescription("Source List URL"), Personalizable(PersonalizationScope.Shared)]
        public string FullListURL { get; set; }

        [WebBrowsable(true), WebDisplayName("Component Row Limit"), WebDescription("Component Row Limit"), Personalizable(PersonalizationScope.Shared)]
        public uint RowLimit { get; set; }

        [WebBrowsable(true), WebDisplayName("Item Detail URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailURL { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Metadata site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public HSEnv()
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
                WPTitle.Text = ComponentTitle;
                string query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                if(RowLimit < 2)
                {
                    RowLimit = 2;
                }
                SPListItemCollection sPListItem = Web.Lists[ListName].GetItems(new SPQuery() { Query = query, RowLimit = RowLimit });
                StringBuilder stringBuilder = new StringBuilder();

                int rowNo = 0;
                foreach (SPListItem Item in sPListItem)
                {
                    DateTime date = DateTime.Parse(Item["Expires"].ToString());
                    //SPFieldUrlValue ImageUrl = new SPFieldUrlValue(Item["Image"].ToString());
                    string Photo = Item["Image"] != null ? (new SPFieldUrlValue(Item["Image"].ToString())).Url : Helper.NoImageURL;
                    string Summary = Item["Summary"] != null ? Item["Summary"].ToString() : string.Empty;
                    stringBuilder.Append("<section class='sec-hse-row'>");
                    if (rowNo % 2 == 0)
                    {
                        stringBuilder.AppendFormat("<section class='sec-hse-left' style='background-image:url({0})'>", Photo);
                        stringBuilder.AppendFormat("<a href='{0}' class=''><div class='sec-hse-more'>More</div></a>", string.Format("{0}?hseid={1}", DetailURL, Item.ID));
                        stringBuilder.Append("</section>");
                        stringBuilder.Append("<section class='sec-hse-right'>");
                        stringBuilder.AppendFormat("<section class='sec-hse-name'>{0}</section>", Item["Title"].ToString());
                        stringBuilder.AppendFormat("<section class='sec-hse-info'>{0}</section>", Summary);
                        stringBuilder.Append("</section>");
                    }
                    else
                    {
                        stringBuilder.Append("<section class='sec-hse-right'>");
                        stringBuilder.AppendFormat("<section class='sec-hse-name'>{0}</section>", Item["Title"].ToString());
                        stringBuilder.AppendFormat("<section class='sec-hse-info'>{0}</section>", Summary);
                        stringBuilder.Append("</section>");
                        stringBuilder.AppendFormat("<section class='sec-hse-left' style='background-image:url({0})'>", Photo);
                        stringBuilder.AppendFormat("<a href='{0}' class=''><div class='sec-hse-more'>More</div></a>", string.Format("{0}?hseid={1}", DetailURL, Item.ID));
                        stringBuilder.Append("</section>");
                    }
                    stringBuilder.Append("</section>");

                    rowNo++;

                }
                ltrHSE.Text = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                ltrHSE.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

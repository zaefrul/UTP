using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UTP.MegaDropdown
{
    [ToolboxItemAttribute(false)]
    public partial class MegaDropdown : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
       [WebBrowsable(true), WebDisplayName("List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Metadata Site URL"), WebDescription("Metadata Site Name"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public MegaDropdown()
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
                var q = new SPQuery()
                {
                    ViewXml = Helper.MDQuery
                };
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                //SPView viewQuery = Web.Lists[ListName].Views["Query"];
                SPListItemCollection Items = Web.Lists[ListName].GetItems(q);
                StringBuilder section1_SB = new StringBuilder();
                StringBuilder section2_SB = new StringBuilder();
                StringBuilder section3_SB = new StringBuilder();
                StringBuilder section4_SB = new StringBuilder();
                bool _sec1, _sec2, _sec3, _sec4;
                _sec1 = _sec2 = _sec3 = _sec4 = false;
                string _tempCategory, Category = "";
                foreach (SPListItem Item in Items)
                {
                    int _section = Convert.ToInt32(Convert.ToDouble(Item[Helper.MDSectionField].ToString().Contains("#") ? Item[Helper.MDSectionField].ToString().Split('#')[1] : Item[Helper.MDSectionField].ToString()));
                    StringBuilder stringBuilder;
                    switch (_section)
                    {
                        case 1: stringBuilder = section1_SB; _sec1 = true; break;
                        case 2: if (!_sec2) { Category = ""; } stringBuilder = section2_SB; _sec2 = true; break;
                        case 3: if (!_sec3) { Category = ""; } stringBuilder = section3_SB; _sec3 = true; break;
                        case 4: if (!_sec4) { Category = ""; } stringBuilder = section4_SB; _sec4 = true; break;
                        default: stringBuilder = new StringBuilder(); break; //Hopefully this case never happen
                    }

                    _tempCategory = Item[Helper.MDCategoryField].ToString().Contains("#") ? Item[Helper.MDCategoryField].ToString().Split('#')[1] : Item[Helper.MDCategoryField].ToString();
                    if (_tempCategory != Category)
                    {
                        if (Category != "")
                        {
                            stringBuilder.Append("</ul>");
                            stringBuilder.Append("</section>");
                        }
                        Category = _tempCategory;
                        stringBuilder.Append("<section class='mega-content'>");
                        stringBuilder.Append("<div class='mega-category'>"+Category+"</div>");
                        stringBuilder.Append("<ul>");
                    }
                    SPFieldUrlValue Value = new SPFieldUrlValue(Item[Helper.MDUrlField].ToString());
                    stringBuilder.Append("<li><a href='"+Value.Url+"'>"+Value.Description+"</a></li>");
                }
                if (_sec1)
                {
                    section1_SB.Append("</ul>");
                    section1_SB.Append("</section>");
                }
                if (_sec2)
                {
                    section2_SB.Append("</ul>");
                    section2_SB.Append("</section>");
                }
                if (_sec3)
                {
                    section3_SB.Append("</ul>");
                    section3_SB.Append("</section>");
                }
                if (_sec4)
                {
                    section4_SB.Append("</ul>");
                    section4_SB.Append("</section>");
                }
                MegaSec1.Text = section1_SB.ToString();
                MegaSec2.Text = section2_SB.ToString();
                MegaSec3.Text = section3_SB.ToString();
                MegaSec4.Text = section4_SB.ToString();
            }
            catch (Exception ex)
            {
                MegaSec1.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

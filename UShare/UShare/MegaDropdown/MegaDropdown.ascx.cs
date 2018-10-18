using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace UShare.MegaDropdown
{
    [ToolboxItemAttribute(false)]
    public partial class MegaDropdown : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Site URL"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollectionURL { get; set; }
        [WebBrowsable(true), WebDisplayName("Division List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string DivisionListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Department List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string DepartmentListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Unit List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string UnitsListName { get; set; }
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
                SPWeb Web = new SPSite(SiteCollectionURL).OpenWeb();
                SPListItemCollection Division = Web.Lists[DivisionListName].Items;

                string division = "<div class='mega-category'>Division</div><ul>";
                string departmentsmenu = "";
                string unitsmenu = "";
                foreach (SPListItem ItemDiv in Division)
                {
                    SPListItemCollection Department = Web.Lists[DepartmentListName].GetItems(new SPQuery() { Query = Helper.DepartmentQuery(ItemDiv.ID), ViewFields = Helper.DepartmentViewField });
                    division += $"<li><a href='#' class='btnDivNav' onclick='divisionBtn(\"{ItemDiv.Title.Replace(" ", "")}\")'>{ItemDiv.Title}</a></li>";
                    departmentsmenu += $"<section class='mega-content'  style='display:none' id='{ItemDiv.Title.Replace(" ", "")}'><div class='mega-category'>{ItemDiv.Title}</div><ul>";
                    foreach (SPListItem ItemDepart in Department)
                    {
                        SPListItemCollection Units = Web.Lists[UnitsListName].GetItems(new SPQuery() { Query = Helper.UnitsQuery(ItemDepart.ID), ViewFields = Helper.UnitsViewField });
                        SPFieldUrlValue UrlDepart = new SPFieldUrlValue(ItemDepart[Helper.DepartURL].ToString());
                        departmentsmenu += $"<li><a href='#' class='btnDeptNav' onclick='departmentBtn(\"depart_{UrlDepart.Description.Replace(" ", "")}\")'>{UrlDepart.Description}</a></li>";
                        unitsmenu += $"<section class='mega-content' style='display:none' id='depart_{UrlDepart.Description.Replace(" ", "")}'><div class='mega-category'><a href='{UrlDepart.Url}'>{UrlDepart.Description}</a></div><ul>";
                        foreach (SPListItem ItemUnit in Units)
                        {
                            SPFieldUrlValue Url = new SPFieldUrlValue(ItemUnit[Helper.UnitURL].ToString());
                            unitsmenu += $"<li><a href='{Url.Url}' >{Url.Description}</a></li>";
                        }
                        unitsmenu += $"</ul></section>";
                    }
                    departmentsmenu += $"</ul></section>";
                }
                division += $"</ul>";
                mega_division.InnerHtml = division;
                mega_academic.InnerHtml = departmentsmenu;
                mega_Units.InnerHtml = unitsmenu;
            }
            catch (Exception ex)
            {
                mega_Units.InnerHtml = $"<div>{ex.Message}</div>";
            }
        }
    }
}

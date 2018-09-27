using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace UShare.UpcomingEventsList
{
    [ToolboxItemAttribute(false)]
    public partial class UpcomingEventsList : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Please Page Title"), WebDescription("Please Page Title"), Personalizable(PersonalizationScope.Shared)]
        public string PageTitle { get; set; }
        [WebBrowsable(true), WebDisplayName("Please Page Sub-Title"), WebDescription("Please Page Sub-Title"), Personalizable(PersonalizationScope.Shared)]
        public string PageSubTitle { get; set; }
        [WebBrowsable(true), WebDisplayName("Please Enter ListName"), WebDescription("Please enter source list for this webpart"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }
        [WebBrowsable(true), WebDisplayName("number of box per page"), WebDescription("number of box per page"), Personalizable(PersonalizationScope.Shared)]
        public int RowLimit { get; set; }
        private SPListItemCollection List { get; set; }
        public UpcomingEventsList()
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
                PaginatedTitle.InnerText = PageTitle;
                PaginatedSubTitle.InnerText = PageSubTitle;
                Output.Visible = false;
                string date = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Now);
                SPQuery query = new SPQuery()
                {
                    Query = $"<Where><Geq><FieldRef Name='Expires' /><Value IncludeTimeValue='TRUE' Type='DateTime'>{date}</Value></Geq></Where><OrderBy><FieldRef Name='Expires' Ascending='True' /></OrderBy>"
                };
                List = SPContext.Current.Web.Lists[ListName].GetItems(query);
                StringBuilder sb = new StringBuilder();
                int index = 0;
                if (HttpContext.Current.Request.Params["page"] != null && HttpContext.Current.Request.Params["page"].Trim() != "1")
                {
                    index = (Int32.Parse(HttpContext.Current.Request.Params["page"]) - 1) * RowLimit;
                }
                int ID;
                if (HttpContext.Current.Request.Params["Event"] != null && Int32.TryParse(HttpContext.Current.Request.Params["Event"], out ID))
                {
                    ShowsDetail(SPContext.Current.Web.Lists[ListName].GetItemById(ID));
                }
                int loopbreaker = index + RowLimit;
                for (int i = index; i < loopbreaker; i++)
                {
                    if (i >= List.Count)
                    {
                        break;
                    }
                    sb.Append(SectionBuilder(List[i]));
                }
                string POutput = "<a href='?page=1' ><section class='sec-event-listing-paginated-btn'>1</section></a>";
                if (List.Count > RowLimit)
                {
                    int paginated = List.Count % RowLimit > 0 ? 1 : 0;
                    paginated += List.Count / RowLimit;
                    for (int i = 2; i <= paginated; i++)
                    {
                        POutput += $"<a href='?page={i}' ><section class='sec-event-listing-paginated-btn'>{i}</section></a>";
                    }
                }
                PaginatedButton.Text = POutput;
                Output.Text = sb.ToString();
                Output.Visible = true;
            }
            catch (Exception ex)
            {
                Output.Text = ex.Message;
            }
        }
        private string SectionBuilder(SPListItem Item)
        {
            DateTime dt = DateTime.Parse(Item[Helper.UPExpiresField].ToString());
            string title = Item[Helper.UPTitleField].ToString();
            string structure;
            structure = "<section class='sec-event-listing-row'>";
            structure += "<section class='sec-event-listing-date'>";
            structure += "<i class='far fa-calendar-alt event-date-ico'></i>";
            structure += $"<div class='day'>{dt.Day}</div>";
            structure += $"<div class='month'>{dt.ToString("MMM")}</div>";
            structure += "</section>";
            structure += "<section class='sec-event-listing-name'>";
            structure += $"<a href='#' class=''>{title}</a>";
            structure += "</section>";
            structure += $"<section class='sec-event-listing-summary'></section>";
            structure += "<section class='sec-event-listing-more'>";
            if (HttpContext.Current.Request.Params["page"] != null && HttpContext.Current.Request.Params["page"].Trim() != "1")
            {
                structure += $"<a href='?page={HttpContext.Current.Request.Params["page"].Trim()}&Event={Item.ID.ToString()}'><section class='sec-event-btn'>Read More <i class='fas fa-angle-right fa-more-btn'></i></section></a>";
            }
            else
            {
                structure += $"<a href='?Event={Item.ID.ToString()}'><section class='sec-event-btn'>Read More <i class='fas fa-angle-right fa-more-btn'></i></section></a>";
            }
            structure += "</section>";
            structure += "</section>";
            return structure;
        }

        private void ShowsDetail(SPListItem Item)
        {
            DateTime dt = DateTime.Parse(Item[Helper.UPExpiresField].ToString());
            Detail_Day.Text = dt.Day.ToString();
            Detail_Month.Text = dt.ToString("MMM");
            Detail_Title.Text = Item[Helper.UPTitleField].ToString();
            Detail_Summary.Text = Item[Helper.UPBodyField] != null ? Item[Helper.UPBodyField].ToString() : string.Empty;
            DetailView.Visible = true;
            if (HttpContext.Current.Request.Params["page"] != null && HttpContext.Current.Request.Params["page"].Trim() != "1")
            {
                Detail_Close_Lb.PostBackUrl = $"{HttpContext.Current.Request.CurrentExecutionFilePath}?page={HttpContext.Current.Request.Params["page"]}";
            }
        }

        protected void Detail_Close_Click(object sender, EventArgs e)
        {
            DetailView.Visible = false;
        }
    }
}

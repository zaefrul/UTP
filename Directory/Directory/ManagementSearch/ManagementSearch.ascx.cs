using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Directory.ManagementSearch
{
    [ToolboxItemAttribute(false)]
    public partial class ManagementSearch : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Management Department Menu"), WebDescription("Management Department Menu source list"), Personalizable(PersonalizationScope.Shared)]
        public string MangementDepartment { get; set; }
        [WebBrowsable(true), WebDisplayName("Management Position Menu"), WebDescription("Management Position Menu source list"), Personalizable(PersonalizationScope.Shared)]
        public string MangementPosition { get; set; }
        [WebBrowsable(true), WebDisplayName("Staff List Name"), WebDescription("Staff List Name"), Personalizable(PersonalizationScope.Shared)]
        public string StaffList { get; set; }
        //Site URL Setting
        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Metadata site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }

        private SPWeb Web { get; set; }
        public ManagementSearch()
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
                Web = new SPSite(SiteCollection).OpenWeb();
                SPListItemCollection Positions, Departments;
                SPQuery query = new SPQuery()
                {
                    ViewXml = "<View><Query><OrderBy><FieldRef Name='Title' Ascending='True' /></OrderBy></Query><ViewFields><FieldRef Name='Title' /><FieldRef Name='ID' /></ViewFields><QueryOptions /></View>"
                };
                Positions = Web.Lists[MangementPosition].GetItems(query);
                Departments = Web.Lists[MangementDepartment].GetItems(query);
                Position = DropDownControlFactory(Position, Positions.GetDataTable(), "Title", "ID");
                Department = DropDownControlFactory(Department, Departments.GetDataTable(), "Title", "ID");
                if (System.Web.HttpContext.Current.Request.Params["persondetail"] != null)
                {
                    GetUserDetails(Int32.Parse(System.Web.HttpContext.Current.Request.Params["persondetail"]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GetUserDetails(int v)
        {

        }

        public DropDownList DropDownControlFactory(DropDownList DDContext, System.Data.DataTable Data, string ViewField, string ValueField)
        {
            DDContext.DataSource = Data;
            DDContext.DataTextField = ViewField;
            DDContext.DataValueField = ValueField;
            DDContext.DataBind();
            DDContext.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            DDContext.SelectedIndex = 0;
            return DDContext;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Details.Visible = false;
            Results.Text = GenerateResultTable(searchStaff(Name.Text, Position.SelectedValue, Department.SelectedValue));
            Results.Visible = true;
        }

        private SPQuery QueryBuilder(string name, string position, string department)
        {
            var myQuery = "<Neq><FieldRef Name='Status' /><Value Type='Choice'>Resigned</Value></Neq>";
            if (name != "")
            {
                //console.log('Name specified!');
                var subQuery = $"<Contains><FieldRef Name='Title' /><Value Type='Text'>{name}</Value></Contains>";
                myQuery = $"<And>{myQuery}{subQuery}</And>";
            }
            // Check for Position column
            if (position != "")
            {
                var subQuery = $"<Eq><FieldRef Name='Management_x0020_Position_x0020_' /><Value Type='Text'>{position}</Value></Eq>";
                myQuery = $"<And>{myQuery}{subQuery}</And>";
            }
            else
            {
                var subQuery = "<IsNotNull><FieldRef Name='Management_x0020_Position_x0020_' /></IsNotNull>";
                myQuery = $"<And>{myQuery}{subQuery}</And>";
            }
            // Check for Department column
            if (department != "")
            {
                var subQuery = $"<Eq><FieldRef Name='Management_x0020_Department' /><Value Type='Text'><![CDATA[{department}]]></Value></Eq>";
                myQuery = $"<And>{myQuery}{subQuery}</And>";
            }

            myQuery = $"<Where>{myQuery}</Where><OrderBy><FieldRef Name='Management_x0020_Position_x0020_' Ascending='True' /><FieldRef Name='Title' Ascending='True' /></OrderBy>";

            var sViewFields = "<FieldRef Name='Title' /><FieldRef Name='Management_x0020_Position_x0020_' /><FieldRef Name='Management_x0020_Department' /><FieldRef Name='Username' /><FieldRef Name='Ext' /><FieldRef Name='Email' /><FieldRef Name='Block' /><FieldRef Name='Unit' />";

            return new SPQuery() { Query = myQuery, ViewFields = sViewFields };
        }

        private SPListItemCollection searchStaff(string name, string position, string department)
        {
            SPQuery Q = QueryBuilder(name, position, department);
            return Web.Lists[StaffList].GetItems(Q);
        }

        private string GenerateResultTable(SPListItemCollection Results)
        {
            string results = "<table class=\"table table-striped\">"
                + "<thead class=\"thead-dark\"><tr>"
                      + "<th>No</td>"
                    + "<th>Position</th>"
                    + "<th>Name</th>"
                    + "<th>Department</th>"
                    + "<th>Unit</th>"
                    + "<th>Email</th>"
                    + "<th>Phone</th>"
                + "<tr></thead>";
            int counter = 0;
            foreach(SPListItem Item in Results)
            {
                string name = Item["Title"] != null ? Item["Title"].ToString() : string.Empty;
                string position = Item["Management_x0020_Position_x0020_"] != null ? Item["Management_x0020_Position_x0020_"].ToString() : string.Empty;
                string Department = Item["Management_x0020_Department"] != null ? Item["Management_x0020_Department"].ToString() : string.Empty;
                string Unit = Item["Unit"] != null ? Item["Unit"].ToString() : string.Empty;
                string Email = Item["Email"] != null ? Item["Email"].ToString() : string.Empty;
                string Phone = Item["Ext"] != null ? Item["Ext"].ToString() : string.Empty;
                results += "<tr>";
                results += $"<td>{++counter}</td>";
                results += $"<td>{name}</td>";
                string pos = position.Contains("#") ? position.Split('#')[1] : position;
                results += $"<td>{pos}</td>";
                string dep = Department.Contains("#") ? Department.Split('#')[1] : Department;
                results += $"<td>{dep}</td>";
                results += $"<td>{Unit}</td>";
                results += $"<td>{Email}</td>";
                results += $"<td>{Phone}</td>";
                results += $"</tr>";
            }
            results += "</table>";
            return results;
        }
    }
}

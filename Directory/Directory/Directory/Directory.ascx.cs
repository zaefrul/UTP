﻿using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Directory.Directory
{
    [ToolboxItemAttribute(false)]
    public partial class Directory : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Position List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListPositionName { get; set; }
        [WebBrowsable(true), WebDisplayName("Department List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListDepartmentName { get; set; }
        [WebBrowsable(true), WebDisplayName("Nationality List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListNationalityName { get; set; }
        [WebBrowsable(true), WebDisplayName("Source List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListStaffName { get; set; }

        //Directory WebPart Helper
        [WebBrowsable(true), WebDisplayName("Qualification List Name"), WebDescription("Qualification List Name"), Personalizable(PersonalizationScope.Shared)]
        public string QualificationsListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Specialization List Name"), WebDescription("Specialization List Name"), Personalizable(PersonalizationScope.Shared)]
        public string SpecializationListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Research List Name"), WebDescription("Research List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ResearchListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Publication List Name"), WebDescription("Publication List Name"), Personalizable(PersonalizationScope.Shared)]
        public string PublicationsListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Teaching Experience List Name"), WebDescription("Teaching Experience List Name"), Personalizable(PersonalizationScope.Shared)]
        public string TeachingExpListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Membership List Name"), WebDescription("Membership List Name"), Personalizable(PersonalizationScope.Shared)]
        public string MembershipListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Recognition List Name"), WebDescription("Recognition List Name"), Personalizable(PersonalizationScope.Shared)]
        public string RecognitionListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Working Experience List Name"), WebDescription("Working Experience List Name"), Personalizable(PersonalizationScope.Shared)]
        public string WorkingExperienceListName { get; set; }
        [WebBrowsable(true), WebDisplayName("Profile Page URL"), WebDescription("Profile Page URL"), Personalizable(PersonalizationScope.Shared)]
        public string ProfilePageURL { get; set; }
        [WebBrowsable(true), WebDescription("Personal Photo List Name"), WebDisplayName("Personal Photo List Name"), Personalizable(PersonalizationScope.Shared)]
        public string PersonalPhotoListName { get; set; }
        //Site URL Setting
        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Metadata site URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public Directory()
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

                if (!Page.IsPostBack)
                {
                    SPListItemCollection Positions, Departments, National;
                    SPQuery query = new SPQuery()
                    {
                        ViewXml = "<View><Query><OrderBy><FieldRef Name='Title' Ascending='True' /></OrderBy></Query><ViewFields><FieldRef Name='Title' /><FieldRef Name='ID' /></ViewFields><QueryOptions /></View>"
                    };
                    Positions = SPContext.Current.Web.Lists[ListPositionName].GetItems(query);
                    Departments = SPContext.Current.Web.Lists[ListDepartmentName].GetItems(query);
                    National = SPContext.Current.Web.Lists[ListNationalityName].GetItems(query);
                    Position = DropDownControlFactory(Position, Positions.GetDataTable(), "Title", "ID");
                    Department = DropDownControlFactory(Department, Departments.GetDataTable(), "Title", "ID");
                    Nationality = DropDownControlFactory(Nationality, National.GetDataTable(), "Title", "ID");
                }
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
            try
            {
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListCollection WebLists = Web.Lists;
                SPItem item = Web.Lists[ListStaffName].GetItemById(v);
                SPQuery query = new SPQuery() { Query = $"<Where><Eq><FieldRef Name='Staff_x0020_No' /><Value Type='Text'>{item["Staff_x0020_No"].ToString()}</Value></Eq></Where>" };
                SPQuery spQuery = new SPQuery() { Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{item["Staff_x0020_No"].ToString()}</Value></Eq></Where>" };
                SPListItemCollection qualifications = WebLists[QualificationsListName].GetItems(query);
                SPQuery spclQ = new SPQuery();
                spclQ.Query = spQuery.Query;
                spclQ.ViewFields = "<FieldRef Name='e6jh' /><FieldRef Name='j16h' />";
                spclQ.ViewFieldsOnly = true;
                SPListItemCollection specialization = WebLists[SpecializationListName].GetItems(spclQ);
                SPListItemCollection research = WebLists[ResearchListName].GetItems(spQuery);
                SPListItemCollection publications = WebLists[PublicationsListName].GetItems(spQuery);
                SPListItemCollection workexp = WebLists[WorkingExperienceListName].GetItems(spQuery);
                SPListItemCollection membership = WebLists[MembershipListName].GetItems(spQuery);
                //SPListItemCollection teachingExp = WebLists[TeachingExpListName].GetItems(query);
                SPListItemCollection recognition = WebLists[RecognitionListName].GetItems(spQuery);
                string AcaPos = item["Academic_x0020_Position_x0020_2"] != null ? item["Academic_x0020_Position_x0020_2"].ToString() : string.Empty;
                string username = item["Title"] != null ? item["Title"].ToString() : string.Empty;
                string citizen = item["Citizenship_x0020_2"] != null ? item["Citizenship_x0020_2"].ToString() : string.Empty;
                string acaDepart = item["Academic_x0020_Department"] != null ? item["Academic_x0020_Department"].ToString() : string.Empty;
                string expertise = item["Area_x0020_of_x0020_Expertise"] != null ? item["Area_x0020_of_x0020_Expertise"].ToString() : string.Empty;
                string phone = item["Ext"] != null ? item["Ext"].ToString() : string.Empty;
                string email = item["Email"] != null ? item["Email"].ToString() : string.Empty;

                string opt = "<section class='content'>";
                SPList PersonalPhotoList = SPContext.Current.Web.Lists[PersonalPhotoListName];
                string Name = string.Empty;
                string url = SPContext.Current.Site.Url;
                if (email.Length != 0)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite site = new SPSite(url))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;
                                SPUser ForPhoto = web.EnsureUser(item["Email"].ToString());
                                Name = ForPhoto.Name;
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    });
                }
                else
                {
                    Name = item["Title"].ToString();
                }
                SPQuery PhotoQuery = new SPQuery()
                {
                    Query = $"<Where><Eq><FieldRef Name='Username' /><Value Type='User'>{Name}</Value></Eq></Where>",
                    ViewFields = @"<FieldRef Name='Title' />"
                };
                SPListItemCollection Result = PersonalPhotoList.GetItems(PhotoQuery);
                string image;
                if (Result.Count > 0)
                {
                    SPListItem Photo = Result[0];
                    //image = $"<div class='staffPhoto' style='background-image: url('{Photo["Title"].ToString().Replace(@"/The-University", "")}');background-size: cover;height: 175px;width: 150px;'></div>";
                    image = $"<img src=\"{Photo["Title"].ToString().Replace(@"/The-University", "")}\" style=\"height: 175px;width: 150px;\"/>";
                }
                else
                {
                    //image = "<div class=\"staffPhoto\" style=\"background-image: url('/directories/PublishingImages/person-icon.png');background-size: cover;height: 175px;width: 150px;\"></div>";
                    image = $"<img src=\"/directories/PublishingImages/person-icon.png\" style=\"height: 175px;width: 150px;\"/>";
                }

                opt += "<section class='drows'>";
                opt += "<section class='col-left'>Photo</section>";
                opt += $"<section class='col-right' id='picture'>{image}</section>";
                opt += "</section>";

                if (SPContext.Current.Web.CurrentUser != null && (SPContext.Current.Web.UserIsSiteAdmin || SPContext.Current.Web.CurrentUser.Email == (string)item["Email"]))
                {
                    opt += "<section class='drows'>";
                    opt += "<section class='col-left'>Action</section>";
                    opt += $"<section class='col-right'><a class='btn btn-warning' href='{SPContext.Current.Web.Url}/Pages/admin.aspx?staffid={item["Staff_x0020_No"].ToString()}'>Edit Profile</a></section>";
                    opt += "</section>";
                }

                opt += "<section class='drows'>";
                opt += "<section class='col-left'>Position</section>";
                AcaPos = AcaPos.Contains("#") ? AcaPos.Split('#')[1] : AcaPos;
                opt += $"<section class='col-right'>{AcaPos}</section>";
                opt += "</section>";

                opt += "<section class='drows'>";
                opt += "<section class='col-left'>Name</section>";
                opt += $"<section class='col-right'>{username}</section>";
                opt += "</section>";

                opt += "<section class='drows'>";
                opt += "<section class='col-left'>Phone</section>";
                opt += $"<section class='col-right'>{phone}</section>";
                opt += "</section>";

                opt += "<section class='drows'>";
                opt += "<section class='col-left'>Nationality</section>";
                citizen = citizen.Contains("#") ? citizen.Split('#')[1] : citizen;
                opt += $"<section class='col-right'>{citizen}</section>";
                opt += "</section>";

                opt += "<section class='drows'>";
                opt += "<section class='col-left'>Email</section>";
                opt += $"<section class='col-right'>{email}</section>";
                opt += "</section>";

                opt += "<section class='drows'>";
                opt += "<section class='col-left'>Department</section>";
                acaDepart = acaDepart.Contains("#") ? acaDepart.Split('#')[1] : acaDepart;
                opt += $"<section class='col-right'>{acaDepart}</section>";
                opt += "</section>";

                if (qualifications != null && qualifications.Count > 0)
                {
                    opt += "<section class='drows'>";
                    opt += "<section class='col-left'>Qualifications</section>";
                    opt += $"<section class='col-right'>";
                    foreach (SPListItem qItem in qualifications)
                    {
                        string year = qItem["Year"] != null ? qItem["Year"].ToString() : string.Empty;
                        string level = qItem["Level_x0020_of_x0020_Qualificati"] != null ? qItem["Level_x0020_of_x0020_Qualificati"].ToString() : string.Empty;
                        string qualification = qItem["Qualifications"] != null ? qItem["Qualifications"].ToString() : string.Empty;
                        string university = qItem["Title"] != null ? qItem["Title"].ToString() : string.Empty;
                        opt += $"<p>{year}, {level} in {qualification} by {university}</p>";
                    }
                    opt += "</section>";
                    opt += "</section>";
                }
                if (specialization != null && specialization.Count > 0)
                {
                    opt += "<section class='drows'>";
                    opt += "<section class='col-left'>Specialization</section>";
                    opt += $"<section class='col-right'><ol>";
                    foreach (SPListItem qItem in specialization)
                    {
                        string area = (string)qItem["e6jh"];
                        string option = (string)qItem["j16h"];
                        opt += $"<li class='dir-header'>{area}";
                        if(option != null && option != string.Empty)
                        {
                            opt += $"<ul><li class='dir-option'>{option}</li></ul></li>";
                        }
                        else
                        {
                            opt += "</li>";
                        }
                    }
                    opt += "</ol></section>";
                    opt += "</section>";
                }
                if (research != null && research.Count > 0)
                {
                    opt += "<section class='drows'>";
                    opt += "<section class='col-left'>Research</section>";
                    opt += $"<section class='col-right'>";
                    foreach (SPListItem qItem in research)
                    {
                        string text = qItem["Research_x0020_Title"] != null ? qItem["Research_x0020_Title"].ToString() : string.Empty;
                        opt += $"<p>{text}</p>";
                    }
                    opt += "</section>";
                    opt += "</section>";
                }
                opt += "</section>";
                if (publications != null && publications.Count > 0)
                {
                    opt += "<section class='content'>";
                    opt += "<table class=\"table table-striped\">";
                    opt += "<thead class=\"thead-dark\">";
                    opt += "<tr><th class='table-header' colspan='2'>Publications</th></tr>";
                    opt += "<tr><th>No.</th>";
                    opt += "<th>Title of Publication</th></tr></thead>";
                    int count = 0;
                    foreach (SPListItem qItem in publications)
                    {
                        opt += $"<tr><td>{++count}</td>";
                        string text = qItem["Description"] != null ? qItem["Description"].ToString() : string.Empty;
                        opt += $"<td>{text}</td></tr>";
                    }
                    opt += "</table>";
                    opt += "</section>";
                }

                if (workexp != null && workexp.Count > 0)
                {
                    opt += "<section class='content'>";
                    opt += "<table class=\"table table-striped\">";
                    opt += "<thead class=\"thead-dark\">";
                    opt += "<tr><th class='table-header' colspan='4'>Working Experience</th></tr>";
                    opt += "<tr><th>No.</th>";
                    opt += "<th>Position</th>";
                    opt += "<th>From - To (Year)</th>";
                    opt += "<th>Company Name</th></tr></thead>";
                    int count = 0;
                    foreach (SPListItem qItem in workexp)
                    {
                        opt += "<tr>";
                        opt += $"<td>{++count}</td>";
                        string text = qItem["Position"] != null ? qItem["Position"].ToString() : string.Empty;
                        opt += $"<td>{text}</td>";
                        string from = qItem["From_x0020__x0028_Year_x0029_"] != null ? qItem["From_x0020__x0028_Year_x0029_"].ToString() : string.Empty;
                        string to = qItem["To_x0020__x0028_Year_x0029_"] != null ? qItem["To_x0020__x0028_Year_x0029_"].ToString() : string.Empty;
                        opt += $"<td>{from} - {to}</td>";
                        string company = qItem["pldr"] != null ? qItem["pldr"].ToString() : string.Empty;
                        opt += $"<td>{company}</td>";
                        opt += "</tr>";
                    }
                    opt += "</table>";
                    opt += "</section>";
                }

                if (membership != null && membership.Count > 0)
                {
                    opt += "<section class='content'>";
                    opt += "<table class=\"table table-striped\">";
                    opt += "<thead class=\"thead-dark\">";
                    opt += "<tr><th class='table-header' colspan='4'>Membership</th></tr>";
                    opt += "<tr><th>No.</th>";
                    opt += "<th>Position</th>";
                    opt += "<th>From - To (Year)</th>";
                    opt += "<th>Professional Bodies / Association</th></tr></thead>";
                    int count = 0;
                    foreach (SPListItem qItem in membership)
                    {
                        opt += "<tr>";
                        opt += $"<td>{++count}</td>";
                        string text = qItem["Position"] != null ? qItem["Position"].ToString() : string.Empty;
                        opt += $"<td>{text}</td>";
                        string from = qItem["From_x0020__x0028_Year_x0029_"] != null ? qItem["From_x0020__x0028_Year_x0029_"].ToString() : string.Empty;
                        string to = qItem["To_x0020__x0028_Year_x0029_"] != null ? qItem["To_x0020__x0028_Year_x0029_"].ToString() : string.Empty;
                        opt += $"<td>{from} - {to}</td>";
                        string pba = qItem["Profesional_x0020_Bodies_x002f_A"] != null ? qItem["Profesional_x0020_Bodies_x002f_A"].ToString() : string.Empty;
                        opt += $"<td>{pba}</td>";
                        opt += "</tr>";
                    }
                    opt += "</table>";
                    opt += "</section>";
                }

                if (research != null && research.Count > 0)
                {
                    opt += "<section class='content'>";
                    opt += "<table class=\"table table-striped\">";
                    opt += "<thead class=\"thead-dark\">";
                    opt += "<tr><th class='table-header' colspan='3'>Research</th></tr>";
                    opt += "<tr><th>No.</th>";
                    opt += "<th>Level of involvement</th>";
                    opt += "<th>Research Title</th></tr></thead>";
                    int count = 0;
                    foreach (SPListItem qItem in research)
                    {
                        opt += "<tr>";
                        opt += $"<td>{++count}</td>";
                        string text = qItem["Level_x0020_of_x0020_Involvement"] != null ? qItem["Level_x0020_of_x0020_Involvement"].ToString() : string.Empty;
                        opt += $"<td>{text}</td>";
                        string pba = qItem["Research_x0020_Title"] != null ? qItem["Research_x0020_Title"].ToString() : string.Empty;
                        opt += $"<td>{pba}</td>";
                        opt += "</tr>";
                    }
                    opt += "</table>";
                    opt += "</section>";
                }

                if (recognition != null && recognition.Count > 0)
                {
                    opt += "<section class='content'>";
                    opt += "<table class=\"table table-striped\">";
                    opt += "<thead class=\"thead-dark\">";
                    opt += "<tr><th class='table-header' colspan='4'>Recognition</th></tr>";
                    opt += "<tr><th>No.</th>";
                    opt += "<th>Recognition Title</th>";
                    opt += "<th>From - To (Year)</th>";
                    opt += "<th>Organizer / Bodies</th></tr></thead>";
                    int count = 0;
                    foreach (SPListItem qItem in recognition)
                    {
                        opt += "<tr>";
                        opt += $"<td>{++count}</td>";
                        string text = qItem["Recognition_x0020_Title"] != null ? qItem["Recognition_x0020_Title"].ToString() : string.Empty;
                        opt += $"<td>{text}</td>";
                        string from = qItem["From_x0020__x0028_Year_x0029_"] != null ? qItem["From_x0020__x0028_Year_x0029_"].ToString() : string.Empty;
                        string to = qItem["To_x0020__x0028_Year_x0029_"] != null ? qItem["To_x0020__x0028_Year_x0029_"].ToString() : string.Empty;
                        opt += $"<td>{from} - {to}</td>";
                        string pba = qItem["Organiser_x002f_Bodies"] != null ? qItem["Organiser_x002f_Bodies"].ToString() : string.Empty;
                        opt += $"<td>{pba}</td>";
                        opt += "</tr>";
                    }
                    opt += "</table>";
                    opt += "</section>";
                }
                Details.Text = opt;
                Details.Visible = true;
            }
            catch (Exception ex)
            {
                Details.Text = ex.Message;
                Details.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Details.Visible = false;
            Results.Text = GenerateTableResults(SearchStaff(Position.SelectedItem.Text, Department.SelectedItem.Text, Nationality.SelectedItem.Text, Name.Text, AOExperties.Text));
            Results.Visible = true;
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

        private SPListItemCollection SearchStaff(string position, string department, string nationality, string name, string AOExperties)
        {
            SPQuery sPQuery = QueryBuilder(name, position, department, nationality, AOExperties);
            SPListItemCollection results = SPContext.Current.Web.Lists[ListStaffName].GetItems(sPQuery);
            return results;
        }

        private SPQuery QueryBuilder(string name, string position, string department, string nationality, string aoe)
        {
            string MQuery = "<Neq><FieldRef Name='Status' /><Value Type='Choice'>Resigned</Value></NeTexq>";
            if (name.Trim() != string.Empty)
            {
                string sQuery = $"<Contains><FieldRef Name='Title' /><Value Type='Text'>{name}</Value></Contains>";
                MQuery = $"<And>{MQuery}{sQuery}</And>";
            }
            if (position.Trim() != string.Empty)
            {
                string sQuery = $"<Eq><FieldRef Name='Academic_x0020_Position_x0020_2' /><Value Type='Lookup'>{position}</Value></Eq>";
                MQuery = $"<And>{MQuery}{sQuery}</And>";
            }
            else
            {
                string sQuery = "<IsNotNull><FieldRef Name='Academic_x0020_Position_x0020_2' /></IsNotNull>";
                MQuery = $"<And>{MQuery}{sQuery}</And>";
            }
            if (department.Trim() != string.Empty)
            {
                string sQuery = $"<Eq><FieldRef Name='Academic_x0020_Department' /><Value Type='Lookup'>{department}</Value></Eq>";
                MQuery = $"<And>{MQuery}{sQuery}</And>";
            }
            if (nationality.Trim() != string.Empty)
            {
                string sQuery = $"<Eq><FieldRef Name='Citizenship_x0020_2' /><Value Type='Lookup'>{nationality}</Value></Eq>";
                MQuery = $"<And>{MQuery}{sQuery}</And>";
            }
            if (aoe.Trim() != string.Empty)
            {
                string sQuery = $"<Contains><FieldRef Name='Area_x0020_of_x0020_Expertise' /><Value Type='Text'>{aoe}</Value></Contains>";
                MQuery = $"<And>{MQuery}{sQuery}</And>";
            }
            string FQuery = $"<Where>{MQuery}</Where><OrderBy><FieldRef Name='Academic_x0020_Position_x003a_Se' Ascending='True' /><FieldRef Name='Name' Ascending='True' /></OrderBy>";
            string ViewField = @"<FieldRef Name='Title' /><FieldRef Name='Academic_x0020_Department' /><FieldRef Name='Academic_x0020_Position_x0020_2' /><FieldRef Name='Area_x0020_of_x0020_Expertise' /><FieldRef Name='Citizenship_x0020_2' /><FieldRef Name='Status' /><FieldRef Name='Staff_x0020_No' />";


            return new SPQuery() { Query = FQuery, ViewFields = ViewField };
        }

        private string GenerateTableResults(SPListItemCollection sp)
        {
            string results = "<table class=\"table table-striped\">"
                + "<thead class=\"thead-dark\">" +
                "<tr><th class='table-header' colspan='6'>Search Results</th></tr>" +
                "<tr>"
                      + "<th>No</td>"
                    + "<th>Position</td>"
                    + "<th>Name</td>"
                    + "<th>Nationality</td>"
                    + "<th>Department</td>"
                    + "<th>Specialization</td>"
                + "<tr></thead>";
            int counter = 0;
            foreach (SPListItem item in sp)
            {
                if (((string)item["Staff_x0020_No"]) == null)
                {
                    continue;
                }
                SPQuery spQuery = new SPQuery() { Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{item["Staff_x0020_No"].ToString()}</Value></Eq></Where>" };
                SPListItemCollection specialization = SPContext.Current.Web.Lists[SpecializationListName].GetItems(spQuery);
                string AcaPos = item["Academic_x0020_Position_x0020_2"] != null ? item["Academic_x0020_Position_x0020_2"].ToString() : string.Empty;
                string username = item["Title"] != null ? item["Title"].ToString() : string.Empty;
                string citizen = item["Citizenship_x0020_2"] != null ? item["Citizenship_x0020_2"].ToString() : string.Empty;
                string acaDepart = item["Academic_x0020_Department"] != null ? item["Academic_x0020_Department"].ToString() : string.Empty;
                string expertise = item["Area_x0020_of_x0020_Expertise"] != null ? item["Area_x0020_of_x0020_Expertise"].ToString() : string.Empty;
                string speci = string.Empty;
                if (specialization.Count > 0)
                {
                    speci = (string)specialization[0]["e6jh"];
                }
                results += "<tr>";
                results += $"<td>{++counter}</td>";
                AcaPos = AcaPos.Contains("#") ? AcaPos.Split('#')[1] : AcaPos;
                results += $"<td>{AcaPos}</td>";
                results += $"<td><a href='?persondetail={item["ID"].ToString()}'>{username}</a></td>";
                citizen = citizen.Contains("#") ? citizen.Split('#')[1] : citizen;
                results += $"<td>{citizen}</td>";
                acaDepart = acaDepart.Contains("#") ? acaDepart.Split('#')[1] : acaDepart;
                results += $"<td>{acaDepart}</td>";
                results += $"<td>{speci}</td>";
                results += "</tr>";
            }
            results += "</table>";
            return results;
        }
    }
}

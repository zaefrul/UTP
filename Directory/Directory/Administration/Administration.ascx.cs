using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
namespace Directory.Administration
{
    [ToolboxItemAttribute(false)]
    public partial class Administration : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDescription("Staff List Name"), WebDisplayName("Staff List Name"),Personalizable(PersonalizationScope.Shared)]
        public string StaffListName { get; set; }
        [WebBrowsable(true), WebDescription("Specialization List Name"), WebDisplayName("Specialization List Name"), Personalizable(PersonalizationScope.Shared)]
        public string SpacializationListName { get; set; }
        [WebBrowsable(true), WebDescription("Qualification List Name"), WebDisplayName("Qualification List Name"), Personalizable(PersonalizationScope.Shared)]
        public string QualificationListName { get; set; }
        [WebBrowsable(true), WebDescription("Publication List Name"), WebDisplayName("Publication List Name"), Personalizable(PersonalizationScope.Shared)]
        public string PublicationListName { get; set; }
        [WebBrowsable(true), WebDescription("Supervision List Name"), WebDisplayName("Supervision List Name"), Personalizable(PersonalizationScope.Shared)]
        public string SupervisionListName { get; set; }
        [WebBrowsable(true), WebDescription("Research List Name"), WebDisplayName("Research List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ResearchListName { get; set; }
        [WebBrowsable(true), WebDescription("Recognition List Name"), WebDisplayName("Recognition List Name"), Personalizable(PersonalizationScope.Shared)]
        public string RecognitionListName { get; set; }
        [WebBrowsable(true), WebDescription("Membership List Name"), WebDisplayName("Membership List Name"), Personalizable(PersonalizationScope.Shared)]
        public string MembershipListName { get; set; }
        [WebBrowsable(true), WebDescription("Staff Development Program List Name"), WebDisplayName("Staff Development Program List Name"), Personalizable(PersonalizationScope.Shared)]
        public string SDPListName { get; set; }
        [WebBrowsable(true), WebDescription("Course Attended List Name"), WebDisplayName("Course Attended List Name"), Personalizable(PersonalizationScope.Shared)]
        public string CourseAttendedListName { get; set; }
        [WebBrowsable(true), WebDescription("Work Experience List Name"), WebDisplayName("Work Experience List Name"), Personalizable(PersonalizationScope.Shared)]
        public string WorkExperienceListName { get; set; }
        [WebBrowsable(true), WebDescription("Teaching Experience List Name"), WebDisplayName("Teaching Experience List Name"), Personalizable(PersonalizationScope.Shared)]
        public string TEListName { get; set; }
        [WebBrowsable(true), WebDescription("Personal Photo List Name"), WebDisplayName("Personal Photo List Name"), Personalizable(PersonalizationScope.Shared)]
        public string PersonalPhotoListName { get; set; }
        public Administration()
        {
        }
        private bool uploadimage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!Page.IsPostBack)
                {
                    SPUser Current = SPContext.Current.Web.CurrentUser;
                    string query = "";
                    if (Page.Request["staffid"] != null)
                    {
                        query = $"<Where><Eq><FieldRef Name='Staff_x0020_No' /><Value Type='Text'>{Page.Request["staffid"]}</Value></Eq></Where>";
                    }
                    else
                    {
                        query = $"<Where><Eq><FieldRef Name='Email' /><Value Type='Text'>{SPContext.Current.Web.CurrentUser.Email}</Value></Eq></Where>";
                    }
                    SPQuery Query = new SPQuery()
                    {
                        Query = query,
                        RowLimit = 1,
                        ViewFields = @"
                                  <FieldRef Name='Title' />
                                  <FieldRef Name='Staff_x0020_No' />
                                  <FieldRef Name='Photo' />
                                  <FieldRef Name='IC_x0020_No_x002f__x0020_Passpor' />
                                  <FieldRef Name='Novell_x002f__x0020_Outlook_x002' />
                                  <FieldRef Name='Grade' />
                                  <FieldRef Name='Academic_x0020_Position_x0020_2' />
                                  <FieldRef Name='Management_x0020_Position_x0020_' />
                                  <FieldRef Name='Department' />
                                  <FieldRef Name='Citizenship_x0020_2' />
                                  <FieldRef Name='Country_x0020_of_x0020_Origin' />
                                  <FieldRef Name='Status_x0020_Employment' />
                                  <FieldRef Name='Email' />
                                  <FieldRef Name='Date_x0020_of_x0020_Birth' />
                                  <FieldRef Name='Date_x0020_of_x0020_Joining_x002' />
                                  <FieldRef Name='Resignation_x0020_Date_x0020_fro' />
                                  <FieldRef Name='Status' />
                                "
                    };
                    SPListItem User = SPContext.Current.Web.Lists[StaffListName].GetItems(Query)[0];
                    SPList PersonalPhotoList = SPContext.Current.Web.Lists[PersonalPhotoListName];
                    string Name = string.Empty;
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                        {
                            using (SPWeb Web = site.OpenWeb())
                            {
                                Web.AllowUnsafeUpdates = true;
                                SPUser ForPhoto = Web.EnsureUser(User["Email"].ToString());
                                Name = ForPhoto.Name;
                                Web.AllowUnsafeUpdates = false;
                            }
                        }
                    });
                    SPQuery PhotoQuery = new SPQuery()
                    {
                        Query = $"<Where><Eq><FieldRef Name='Username' /><Value Type='User'>{Name}</Value></Eq></Where>",
                        ViewFields = @"<FieldRef Name='Title' />"
                    };
                    SPListItemCollection Result = PersonalPhotoList.GetItems(PhotoQuery);
                    string item = "";
                    if (Result.Count > 0)
                    {
                        SPListItem Photo = Result[0];
                        item = $"<div class='staffPhoto' style='background-image: url(\"{Photo["Title"].ToString().Replace(@"/The-University", "")}\")'></div>";
                    }
                    else
                    {
                        item = "<div class=\"staffPhoto\" style=\"background-image: url('/directories/PublishingImages/person-icon.png')\"></div>";
                    }
                    item += $"<div class=\"staffName\">{(string)User["Title"]}</div>";
                    item += $"<div class=\"staffID\">{(string)User["Staff No"]}</div>";
                    staffid.Value = (string)User["Staff No"];
                    UserEmail.Value = (string)User["Email"];
                    MiniProfile.Text = item;
                    PDetail.Text = ProfileDetailGenerator(User);
                    LoadData();
                    RSFCurrVal.Text = 1.ToString();
                }
                if (Page.Request["listname"] != null)
                {
                    if(Page.Request["action"] == "Delete")
                    {
                        DeleteButton(Page.Request["listname"], Page.Request["itemid"], Page.Request["section"]);
                    }
                    else
                    {
                        EditButton(Page.Request["listname"], Page.Request["itemid"], Page.Request["section"]);
                    }
                }
            }
            catch (Exception ex)
            {
                PDetail.Text = ex.Message;
            }
            if(uploadimage)
            {
                uploadimage = false;
                Page.Response.RedirectPermanent(Page.Request.RawUrl);
            }
        }
        private string ProfileDetailGenerator(SPListItem User)
        {
            string pDetail = "";
            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Name</label>";
            pDetail += $"<input readonly value=\"{(string) User["Title"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">IC no. / Passport no.</label>";
            pDetail += $"<input readonly value=\"{(string) User["IC_x0020_No_x002f__x0020_Passpor"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Staff No.</label>";
            pDetail += $"<input readonly value=\"{(string) User["Staff_x0020_No"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Novel ID</label>";
            pDetail += $"<input readonly value=\"{(string) User["Novell_x002f__x0020_Outlook_x002"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Grade</label>";
            pDetail += $"<input readonly value=\"{(string) User["Grade"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Academic Position</label>";
            string acadposval = (string)User["Academic_x0020_Position_x0020_2"];
            if(String.IsNullOrEmpty(acadposval))
            {
                acadposval = string.Empty;
            }
            pDetail += $"<input readonly value=\"{(acadposval.Contains("#")?acadposval.Split('#')[1]:"")}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Management Position</label>";
            string mgmtposval = (string)User["Management_x0020_Position_x0020_"];
            if(String.IsNullOrEmpty(mgmtposval))
            {
                mgmtposval = string.Empty;
            }
            pDetail += $"<input readonly value=\"{(mgmtposval.Contains("#")?mgmtposval.Split('#')[1]:string.Empty)}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Department</label>";
            pDetail += $"<input readonly value=\"{(string) User["Department"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Citizenship</label>";
            string ctznval = (string)User["Citizenship_x0020_2"];
            if(String.IsNullOrEmpty(ctznval))
            {
                ctznval = string.Empty;
            }
            pDetail += $"<input readonly value=\"{(ctznval.Contains("#")?ctznval.Split('#')[1]:string.Empty)}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Country of Origin</label>";
            pDetail += $"<input readonly value=\"{(string) User["Country_x0020_of_x0020_Origin"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Status Employment</label>";
            pDetail += $"<input readonly value=\"{(string) User["Status_x0020_Employment"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Email</label>";
            pDetail += $"<input readonly value=\"{(string) User["Email"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Date of Birth</label>";
            pDetail += $"<input readonly value=\"{(string) User["Date_x0020_of_x0020_Birth"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Date of Joining UTP</label>";
            pDetail += $"<input readonly value=\"{(string) User["Date_x0020_of_x0020_Joining_x002"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Resignation Date from UTP</label>";
            pDetail += $"<input readonly value=\"{(string) User["Resignation_x0020_Date_x0020_fro"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";

            pDetail += "<div class=\"item-input \">";
            pDetail += "<label for=\"InputName\">Status</label>";
            pDetail += $"<input readonly value=\"{(string) User["Status"]}\" type=\"text\" class=\"form-control\" id=\"InputName\" placeholder=\"\">";
            pDetail += "</div>";
            return pDetail;
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            
        }

        protected void SpecializationBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void LoadData()
        {
            SpecializationDataLoad();
            QualificationDataLoad();
            PublicationDataLoad();
            SupervisionDataLoad();
            ResearchDataLoad();
            RecognitionDataLoad();
            MembershipDataLoad();
            SDPDataLoad();
            CourseAttendedDataLoad();
            WorkingExperienceDataLoad();
            TeachingExperienceDataLoad();
        }

        #region Delete_Function

        private void DeleteButton(string ListName, string ItemID, string Section)
        {
            SPContext.Current.Web.AllowUnsafeUpdates = true;
            SPListItem item = SPContext.Current.Web.Lists[ListName].GetItemById(Int32.Parse(ItemID));
            item.Delete();
            RSFCurrVal.Text = Section;
            if(ListName == SpacializationListName)
            {
                SpecializationDataLoad();
            }
            else if (ListName == QualificationListName)
            {
                QualificationDataLoad();
            }
            else if (ListName == PublicationListName)
            {
                PublicationDataLoad();
            }
            else if (ListName == SupervisionListName)
            {
                SupervisionDataLoad();
            }
            else if (ListName == ResearchListName)
            {
                ResearchDataLoad();
            }
            else if (ListName == RecognitionListName)
            {
                RecognitionDataLoad();
            }
            else if (ListName == MembershipListName)
            {
                MembershipDataLoad();
            }
            else if (ListName == SDPListName)
            {
                SDPDataLoad();
            }
            else if (ListName == CourseAttendedListName)
            {
                CourseAttendedDataLoad();
            }
            else if (ListName == WorkExperienceListName)
            {
                WorkingExperienceDataLoad();
            }
            else if (ListName == TEListName)
            {
                TeachingExperienceDataLoad();
            }
            SPContext.Current.Web.AllowUnsafeUpdates = false;
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            LinkButton Current = (LinkButton)sender;
            string[] formDetail = Current.ID.Split('_');
            SPListItem item = SPContext.Current.Web.Lists[formDetail[0]].GetItemById(Int32.Parse(formDetail[1]));
            item.Delete();
        }

        #endregion

        #region Data_Rendering
        private void SpecializationDataLoad()
        {
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"<FieldRef Name='e6jh' /><FieldRef Name='j16h' /><FieldRef Name='Attachments' />"
            };
            SPListItemCollection SpacializationListItem = SPContext.Current.Web.Lists[SpacializationListName].GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Area</th>";
            Spacialization += "<th scope=\"col\">Consultancy</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in SpacializationListItem)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["e6jh"]}</td>";
                Spacialization += $"<td>{(string)sItem["j16h"]}</td>";
                if(sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix+sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                LinkButton DelBtn = new LinkButton();
                DelBtn.ID = SpacializationListItem + "_" + sItem.ID;
                DelBtn.Click += DeleteBtn_Click;
                DelBtn.Text = "Delete";
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{SpacializationListName}_{sItem.ID}_2' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{SpacializationListName}_{sItem.ID}_2' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            SDetail.Text = Spacialization;
        }
        private void QualificationDataLoad()
        {
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Staff_x0020_No' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"<FieldRef Name='Attachments' /><FieldRef Name='Qualifications' /><FieldRef Name='Level_x0020_of_x0020_Qualificati' /><FieldRef Name='Title' /><FieldRef Name='Year' /><FieldRef Name='w91i' /><FieldRef Name='Staff_x0020_No' />"
            };

            SPListItemCollection QualificationListItemCollection = SPContext.Current.Web.Lists[QualificationListName].GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Level of Qualification</th>";
            Spacialization += "<th scope=\"col\">University</th>";
            Spacialization += "<th scope=\"col\">Year</th>";
            Spacialization += "<th scope=\"col\">Area of Experties</th>";
            Spacialization += "<th scope=\"col\">Qualification</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach(SPListItem qItem in QualificationListItemCollection)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)qItem["Level_x0020_of_x0020_Qualificati"]}</td>";
                Spacialization += $"<td>{(string)qItem["Title"]}</td>";
                Spacialization += $"<td>{(string)qItem["Year"]}</td>";
                Spacialization += $"<td>{(string)qItem["w91i"]}</td>";
                Spacialization += $"<td>{(string)qItem["Qualifications"]}</td>";
                if (qItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{qItem.Attachments.UrlPrefix + qItem.Attachments[0]}'>{qItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{QualificationListName}_{qItem.ID}_3' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{QualificationListName}_{qItem.ID}_3' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            QDetail.Text = Spacialization;
        }
        private void PublicationDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[PublicationListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"<FieldRef Name='Attachments' /><FieldRef Name='Description' /><FieldRef Name='Name' />"
            };
            var PublicationLIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">URL Publication</th>";
            Spacialization += "<th scope=\"col\">Name</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach(SPListItem pItem in PublicationLIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)pItem["Description"]}</td>";
                Spacialization += $"<td>{(string)pItem["Name"]}</td>";
                if (pItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{pItem.Attachments.UrlPrefix + pItem.Attachments[0]}'>{pItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{PublicationListName}_{pItem.ID}_4' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{PublicationListName}_{pItem.ID}_4' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            PubDetail.Text = Spacialization;
        }
        private void SupervisionDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[SupervisionListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Staff_x0020_No' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"<FieldRef Name='Attachments' /><FieldRef Name='Title' /><FieldRef Name='l79i' /><FieldRef Name='Enroll_x0020_Year' /><FieldRef Name='Level_x0020_of_x0020_Qualificati' /><FieldRef Name='Status' /><FieldRef Name='Remarks' /><FieldRef Name='Student_x0020_Grade' /><FieldRef Name='Student_x0020_Enroll' />"
            };
            var SupervisionLIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Student Name</th>";
            Spacialization += "<th scope=\"col\">Metric ID</th>";
            Spacialization += "<th scope=\"col\">Enroll Year</th>";
            Spacialization += "<th scope=\"col\">Level of Qualification</th>";
            Spacialization += "<th scope=\"col\">Status</th>";
            Spacialization += "<th scope=\"col\">Remarks</th>";
            Spacialization += "<th scope=\"col\">Student Grade</th>";
            Spacialization += "<th scope=\"col\">Student Enroll</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in SupervisionLIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["Title"]}</td>";
                Spacialization += $"<td>{(string)sItem["l79i"]}</td>";
                Spacialization += $"<td>{(string)sItem["Enroll_x0020_Year"]}</td>";
                Spacialization += $"<td>{(string)sItem["Level_x0020_of_x0020_Qualificati"]}</td>";
                Spacialization += $"<td>{(string)sItem["Status"]}</td>";
                Spacialization += $"<td>{(string)sItem["Remarks"]}</td>";
                Spacialization += $"<td>{(string)sItem["Student_x0020_Grade"]}</td>";
                Spacialization += $"<td>{(string)sItem["Student_x0020_Enroll"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{SupervisionListName}_{sItem.ID}_5' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{SupervisionListName}_{sItem.ID}_5' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            SuperDetail.Text = Spacialization;
        }
        private void ResearchDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[ResearchListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='LinkTitleNoMenu' /><Value Type='Computed'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Research_x0020_Title' />
                  <FieldRef Name='Level_x0020_of_x0020_Involvement' />
                  <FieldRef Name='Funded_x002f__x0020_Non_x0020_Fu' />
                  <FieldRef Name='Funded_x0020_Amount_x0020__x0028' />
                  <FieldRef Name='Type_x0020_of_x0020_Funding_x002' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Major_x0020_Output' />
                  <FieldRef Name='Other_x0020_Output' />
                  <FieldRef Name='Remarks' />
                  <FieldRef Name='Position' />
                  <FieldRef Name='Publishing_x0020_Level' />
                  <FieldRef Name='Publishing_x0020_Type' />
                  <FieldRef Name='Other_x0020_Publishing_x0020_Typ' />
                  <FieldRef Name='Source_x0020_Detail' />
                  <FieldRef Name='Amount' />
                  <FieldRef Name='Funding_x0020_Agencies' /><FieldRef Name='Attachments' />
                "
            };
            var ResearchLIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Research Title</th>";
            Spacialization += "<th scope=\"col\">Level of Involvement</th>";
            Spacialization += "<th scope=\"col\">Funded/Non Funded</th>";
            Spacialization += "<th scope=\"col\">Funded Amount (RM)</th>";
            Spacialization += "<th scope=\"col\">Type of Funding Agencies</th>";
            Spacialization += "<th scope=\"col\">From (Year)</th>";
            Spacialization += "<th scope=\"col\">To (Year)</th>";
            Spacialization += "<th scope=\"col\">Major Output</th>";
            //Spacialization += "<th scope=\"col\">Other Output</th>";
            //Spacialization += "<th scope=\"col\">Remarks</th>";
            Spacialization += "<th scope=\"col\">Position</th>";
            Spacialization += "<th scope=\"col\">Publishing Level</th>";
            Spacialization += "<th scope=\"col\">Publishing Type</th>";
            Spacialization += "<th scope=\"col\">Other Publishing Type</th>";
            Spacialization += "<th scope=\"col\">Source Detail</th>";
            Spacialization += "<th scope=\"col\">Amount</th>";
            Spacialization += "<th scope=\"col\">Funding Agencies</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in ResearchLIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["Research_x0020_Title"]}</td>";
                Spacialization += $"<td>{(string)sItem["Level_x0020_of_x0020_Involvement"]}</td>";
                Spacialization += $"<td>{(string)sItem["Funded_x002f__x0020_Non_x0020_Fu"]}</td>";
                Spacialization += $"<td>{(string)sItem["Funded_x0020_Amount_x0020__x0028"]}</td>";
                Spacialization += $"<td>{(string)sItem["Type_x0020_of_x0020_Funding_x002"]}</td>";
                Spacialization += $"<td>{(string)sItem["From_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["To_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["Major_x0020_Output"]}</td>";
                //Spacialization += $"<td>{(string)sItem["Other_x0020_Output"]}</td>";
                //Spacialization += $"<td>{(string)sItem["Remarks"]}</td>";
                Spacialization += $"<td>{(string)sItem["Position"]}</td>";
                Spacialization += $"<td>{(string)sItem["Publishing_x0020_Level"]}</td>";
                Spacialization += $"<td>{(string)sItem["Publishing_x0020_Type"]}</td>";
                Spacialization += $"<td>{(string)sItem["Other_x0020_Publishing_x0020_Typ"]}</td>";
                Spacialization += $"<td>{(string)sItem["Source_x0020_Detail"]}</td>";
                Spacialization += $"<td>{(string)sItem["Amount"]}</td>";
                Spacialization += $"<td>{(string)sItem["Funding_x0020_Agencies"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{ResearchListName}_{sItem.ID}_6' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{ResearchListName}_{sItem.ID}_6' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            ResearchDetail.Text = Spacialization;
        }
        private void RecognitionDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[RecognitionListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Recognition_x0020_Title' />
                  <FieldRef Name='Level' />
                  <FieldRef Name='Recognition_x0020_Type' />
                  <FieldRef Name='Organiser_x002f_Bodies' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Other_x0020_Recognition_x0020_Ty' />
                  <FieldRef Name='Recognition_x0020_From' />
                  <FieldRef Name='Recognition_x0020_Details' />
                  <FieldRef Name='Attachments' />
                "
            };
            var RecognitionLIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Recognition Title</th>";
            Spacialization += "<th scope=\"col\">Level</th>";
            Spacialization += "<th scope=\"col\">Recognition Type</th>";
            Spacialization += "<th scope=\"col\">Organizer/Bodies</th>";
            Spacialization += "<th scope=\"col\">From (Year)</th>";
            Spacialization += "<th scope=\"col\">To (Year)</th>";
            Spacialization += "<th scope=\"col\">Other Recognition Type</th>";
            Spacialization += "<th scope=\"col\">Other Recognition Details</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in RecognitionLIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["Recognition_x0020_Title"]}</td>";
                Spacialization += $"<td>{(string)sItem["Level"]}</td>";
                Spacialization += $"<td>{(string)sItem["Recognition_x0020_Type"]}</td>";
                Spacialization += $"<td>{(string)sItem["Organiser_x002f_Bodies"]}</td>";
                Spacialization += $"<td>{(string)sItem["From_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["To_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["Other_x0020_Recognition_x0020_Ty"]}</td>";
                Spacialization += $"<td>{(string)sItem["Recognition_x0020_Details"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{RecognitionListName}_{sItem.ID}_7' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{RecognitionListName}_{sItem.ID}_7' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            RecognitionDetail.Text = Spacialization;
        }
        private void MembershipDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[MembershipListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Membership_x0020_Number' />
                  <FieldRef Name='Level' />
                  <FieldRef Name='Position' />
                  <FieldRef Name='Profesional_x0020_Bodies_x002f_A' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Membership_x0020_Category' />
                    <FieldRef Name='Attachments' />
                "
            };
            var MembershipLIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Membership Number</th>";
            Spacialization += "<th scope=\"col\">Level</th>";
            Spacialization += "<th scope=\"col\">Position</th>";
            Spacialization += "<th scope=\"col\">Professional Bodies/Association</th>";
            Spacialization += "<th scope=\"col\">From (Year)</th>";
            Spacialization += "<th scope=\"col\">To (Year)</th>";
            Spacialization += "<th scope=\"col\">Membership Category</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in MembershipLIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["Membership_x0020_Number"]}</td>";
                Spacialization += $"<td>{(string)sItem["Level"]}</td>";
                Spacialization += $"<td>{(string)sItem["Position"]}</td>";
                Spacialization += $"<td>{(string)sItem["Profesional_x0020_Bodies_x002f_A"]}</td>";
                Spacialization += $"<td>{(string)sItem["From_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["To_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["Membership_x0020_Category"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{MembershipListName}_{sItem.ID}_8' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{MembershipListName}_{sItem.ID}_8' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            MembershipDetail.Text = Spacialization;
        }
        private void SDPDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[SDPListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Duration' />
                  <FieldRef Name='Start_x0020_Date' />
                  <FieldRef Name='tbir' />
                    <FieldRef Name='Attachments' />
                "
            };
            var SDPLIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Duration</th>";
            Spacialization += "<th scope=\"col\">Start Date</th>";
            Spacialization += "<th scope=\"col\">Location</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in SDPLIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["Duration"]}</td>";
                Spacialization += $"<td>{(string)sItem["Start_x0020_Date"]}</td>";
                Spacialization += $"<td>{(string)sItem["tbir"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{SDPListName}_{sItem.ID}_10' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{SDPListName}_{sItem.ID}_10' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            SDPDetail.Text = Spacialization;
        }
        private void CourseAttendedDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[CourseAttendedListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Course_x0020_Name' />
                  <FieldRef Name='Start_x0020_Date' />
                  <FieldRef Name='Programme' />
                  <FieldRef Name='Year' />
                  <FieldRef Name='Duration' />
                    <FieldRef Name='Attachments' />
                "
            };
            var CourseAttendedLIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Course Name</th>";
            Spacialization += "<th scope=\"col\">Start Date</th>";
            Spacialization += "<th scope=\"col\">Programme</th>";
            Spacialization += "<th scope=\"col\">Year</th>";
            Spacialization += "<th scope=\"col\">Duration</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in CourseAttendedLIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["Course_x0020_Name"]}</td>";
                string sdDT = sItem["Start_x0020_Date"] != null ? ((DateTime)sItem["Start_x0020_Date"]).ToString("dd-MMM-yyyy") : string.Empty;
                Spacialization += $"<td>{sdDT}</td>";
                Spacialization += $"<td>{(string)sItem["Programme"]}</td>";
                Spacialization += $"<td>{(string)sItem["Year"]}</td>";
                Spacialization += $"<td>{(string)sItem["Duration"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{CourseAttendedListName}_{sItem.ID}_11' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{CourseAttendedListName}_{sItem.ID}_11' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            CADetail.Text = Spacialization;
        }
        private void WorkingExperienceDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[WorkExperienceListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>{staffid.Value}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='pldr' />
                  <FieldRef Name='Work_x0020_Experience' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Position' />
                  <FieldRef Name='Attachments' />
                "
            };
            var WELIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Company Name</th>";
            Spacialization += "<th scope=\"col\">Work Experience</th>";
            Spacialization += "<th scope=\"col\">From (Year)</th>";
            Spacialization += "<th scope=\"col\">To (Year)</th>";
            Spacialization += "<th scope=\"col\">Position</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in WELIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["pldr"]}</td>";
                Spacialization += $"<td>{(string)sItem["Work_x0020_Experience"]}</td>";
                Spacialization += $"<td>{(string)sItem["From_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["To_x0020__x0028_Year_x0029_"]}</td>";
                Spacialization += $"<td>{(string)sItem["Position"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{WorkExperienceListName}_{sItem.ID}_12' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{WorkExperienceListName}_{sItem.ID}_12' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            WEDetail.Text = Spacialization;

        }
        private void TeachingExperienceDataLoad()
        {
            SPList list = SPContext.Current.Web.Lists[TEListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='Username' LookupId='TRUE' /><Value Type='int'>{SPContext.Current.Web.CurrentUser.ID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Title' />
                  <FieldRef Name='Programme' />
                  <FieldRef Name='Year_x0020_of_x0020_Standing' />
                  <FieldRef Name='Attachments' />
                "
            };
            var TELIC = list.GetItems(q);
            string Spacialization = "";
            Spacialization += "<table class=\"table table-striped\">";
            Spacialization += "<thead class=\"\">";
            Spacialization += "<tr>";
            Spacialization += "<th scope=\"col\" style=\"width:50px;\">No</th>";
            Spacialization += "<th scope=\"col\">Course Name</th>";
            Spacialization += "<th scope=\"col\">Programme</th>";
            Spacialization += "<th scope=\"col\">Year of Standing</th>";
            Spacialization += "<th scope=\"col\">Attachment</th>";
            Spacialization += "<th scope=\"col\" style=\"width:120px;\">Option</th>";
            Spacialization += "</tr>";
            Spacialization += "</thead>";
            Spacialization += "<tbody>";
            int count = 0;
            foreach (SPListItem sItem in TELIC)
            {
                Spacialization += "<tr>";
                Spacialization += $"<th scope=\"row\">{++count}</th>";
                Spacialization += $"<td>{(string)sItem["Title"]}</td>";
                Spacialization += $"<td>{(string)sItem["Programme"]}</td>";
                Spacialization += $"<td>{(string)sItem["Year_x0020_of_x0020_Standing"]}</td>";
                if (sItem.Attachments.Count > 0)
                {
                    Spacialization += $"<td><a href='{sItem.Attachments.UrlPrefix + sItem.Attachments[0]}'>{sItem.Attachments[0]}</a></td>";
                }
                else
                {
                    Spacialization += "<td></td>";
                }
                Spacialization += $"<td><input type='button' class='btn btn-danger delete' id='{TEListName}_{sItem.ID}_13' onclick='onclickdelete(this)' value=\"Delete\" /> <input type='button' class='btn btn-success' id='{TEListName}_{sItem.ID}_13' onclick='onEditClick(this)' value=\"Edit\" /></td>";
                Spacialization += "</tr>";
            }
            Spacialization += "</tbody>";
            Spacialization += "</table>";
            TEDetail.Text = Spacialization;
        }
        #endregion

        #region Add_Function

        protected void Specialization_Submit_Click(object sender, EventArgs e)
        {
            using (SPWeb Web = SPContext.Current.Site.OpenWeb())
            {
                SPList SpecializationList = Web.Lists[SpacializationListName];
                string usrInput_Area = Spacialization_Area.Text;
                string usrInput_Consultancy = Specialization_Consultancy.Text;
                SPListItem lic = SpecializationList.AddItem();
                lic["e6jh"] = usrInput_Area;
                lic["j16h"] = usrInput_Consultancy;
                lic["Title"] = staffid.Value;
                if(Specialization_Attachment.HasFile)
                {
                    lic.Attachments.Add(Specialization_Attachment.FileName, Specialization_Attachment.FileBytes);
                }
                lic.Update();
            }
            SpecializationDataLoad();
            RSFCurrVal.Text = 2.ToString();
        }

        protected void QualificationBtn_Click(object sender, EventArgs e)
        {
            SPList QualificationList = SPContext.Current.Web.Lists[QualificationListName];
            string usrInput_LoQ = Qualification_LOQ.SelectedValue;
            string usrInput_Uni = Qualification_UNI.Text;
            string usrInput_Year = Qualification_Year.Text;
            string usrInput_SAoE = Qualification_SAoE.Text;
            string usrInput_Name = Specialization_Consultancy.Text;
            string usrInput_Qualification = Qualification_Qualification.Text;
            SPListItem lic = QualificationList.AddItem();
            lic["Level_x0020_of_x0020_Qualificati"] = usrInput_LoQ;
            lic["Title"] = usrInput_Uni;
            lic["Year"] = usrInput_Year;
            lic["w91i"] = usrInput_SAoE;
            lic["Name"] = usrInput_Name;
            lic["Qualifications"] = usrInput_Qualification;
            lic["Staff_x0020_No"] = staffid.Value;
            if (Qualification_Attachment.HasFile)
            {
                lic.Attachments.Add(Qualification_Attachment.FileName, Qualification_Attachment.FileBytes);
            }
            lic.Update();
            QualificationDataLoad();
            RSFCurrVal.Text = 3.ToString();
        }

        protected void PublicationSbmt_Click(object sender, EventArgs e)
        {
            SPList PublicationList = SPContext.Current.Web.Lists[PublicationListName];
            string usrInput_URLPub = Publication_URLPub.Text;
            string usrInput_Name = Publication_Name.Text;
            SPListItem lic = PublicationList.AddItem();
            lic["Description"] = usrInput_URLPub;
            lic["Name"] = usrInput_Name;
            lic["Title"] = staffid.Value;
            if (Publication_Attachment.HasFile)
            {
                lic.Attachments.Add(Publication_Attachment.FileName, Publication_Attachment.FileBytes);
            }
            lic.Update();
            PublicationDataLoad();
            RSFCurrVal.Text = 4.ToString();
        }

        protected void SupervisionBtn_Click(object sender, EventArgs e)
        {
            SPList SupervisionList = SPContext.Current.Web.Lists[SupervisionListName];
            string usrInput_StudName = Supervision_StudName.Text;
            string usrInput_MetrixId = Supervision_MatrixID.Text;
            string usrInput_EnrollYear = Supervision_EnrollYear.Text;
            string usrInput_LoQ = Supervision_LoQ.Text;
            string usrInput_Status = Supervision_Status.Text;
            string usrInput_Remarks = Supervision_Remarks.Text;
            string usrInput_StudentGrade = Supervision_StudentGrade.Text;
            string usrInput_StudentEnroll = Supervision_StudentEnroll.Text;
            SPListItem lic = SupervisionList.AddItem();
            lic["Title"] = usrInput_StudName;
            lic["l79i"] = usrInput_MetrixId;
            lic["Enroll_x0020_Year"] = usrInput_EnrollYear;
            lic["Level_x0020_of_x0020_Qualificati"] = usrInput_LoQ;
            lic["Status"] = usrInput_Status;
            lic["Remarks"] = usrInput_Remarks;
            lic["Student_x0020_Grade"] = usrInput_StudentGrade;
            lic["Student_x0020_Enroll"] = usrInput_StudentEnroll;
            lic["Staff_x0020_No"] = staffid.Value;
            if (Supervision_Attachment.HasFile)
            {
                lic.Attachments.Add(Supervision_Attachment.FileName, Supervision_Attachment.FileBytes);
            }
            lic.Update();
            SupervisionDataLoad();
            RSFCurrVal.Text = 5.ToString();
        }

        protected void Rsearch_SbmtBtn_Click(object sender, EventArgs e)
        {
            SPList ResearchList = SPContext.Current.Web.Lists[ResearchListName];
            string usrInput_RTitle = Research_RTitle.Text;
            string usrInput_LoI = Research_LoI.Text;
            string usrInput_FundedOrNot = Research_FundedorNot.SelectedValue;
            string usrInput_FundAmount = Research_FundAmount.Text;
            string usrInput_ToFA = Research_FundAgencies.Text;
            string usrInput_From = Research_From.Text;
            string usrInput_To = Research_To.Text;
            string usrInput_MajorOutput = Research_MajorOutput.Text;
            string usrInput_OtherOutput = Research_OtherOutput.Text;
            string usrInput_Remarks = Research_Remarks.Text;
            string usrInput_Pos = Research_Position.Text;
            string usrInput_PublishingLevel = Research_PublishingLevel.Text;
            string usrInput_PublishingType = Research_PublishingType.Text;
            string usrInput_OPT = Research_OtherPublishingType.Text;
            string userInput_SD = Research_SourceDetail.Text;
            string userInput_Amount = Research_Amount.Text;
            string userInput_FA = Research_FundAgencies.Text;
            SPListItem lic = ResearchList.AddItem();
            lic["Research_x0020_Title"] = usrInput_RTitle;
            lic["Level_x0020_of_x0020_Involvement"] = usrInput_LoI;
            lic["Funded_x002f__x0020_Non_x0020_Fu"] = usrInput_FundedOrNot;
            lic["Funded_x0020_Amount_x0020__x0028"] = usrInput_FundAmount;
            lic["Type_x0020_of_x0020_Funding_x002"] = usrInput_ToFA;
            lic["From_x0020__x0028_Year_x0029_"] = usrInput_From;
            lic["To_x0020__x0028_Year_x0029_"] = usrInput_To;
            lic["Major_x0020_Output"] = usrInput_MajorOutput;
            lic["Other_x0020_Output"] = usrInput_OtherOutput;
            lic["Remarks"] = usrInput_Remarks;
            lic["Position"] = usrInput_Pos;
            lic["Publishing_x0020_Level"] = usrInput_PublishingLevel;
            lic["Publishing_x0020_Type"] = usrInput_PublishingType;
            lic["Other_x0020_Publishing_x0020_Typ"] = usrInput_OPT;
            lic["Source_x0020_Detail"] = userInput_SD;
            lic["Amount"] = userInput_Amount;
            lic["Funding_x0020_Agencies"] = userInput_FA;
            lic["Title"] = staffid.Value;
            if (Research_Attachment.HasFile)
            {
                lic.Attachments.Add(Research_Attachment.FileName, Research_Attachment.FileBytes);
            }
            lic.Update();
            ResearchDataLoad();
            RSFCurrVal.Text = 6.ToString();
        }

        protected void Recognition_Btn_Click(object sender, EventArgs e)
        {
            SPList RecognitionList = SPContext.Current.Web.Lists[RecognitionListName];
            string usrInput_RT = Recognition_Title.Text;
            string usrInput_RL = Recognition_Leve.SelectedValue;
            string usrInput_RType = Recognition_Type.SelectedValue;
            string usrInput_OB = Recognition_OrgBod.Text;
            string usrInput_From = Recognition_From.Text;
            string usrInput_To = Recognition_To.Text;
            string usrInput_RDetails = Recognition_Details.Text;
            string usrInput_ORDetails = Recognition_ORD.Text;
            string usrInput_RFW = Recogntion_FromWho.Text;
            SPListItem lic = RecognitionList.AddItem();
            lic["Recognition_x0020_Title"] = usrInput_RT;
            lic["Level"] = usrInput_RL;
            lic["Recognition_x0020_Type"] = usrInput_RType;
            lic["Organiser_x002f_Bodies"] = usrInput_OB;
            lic["From_x0020__x0028_Year_x0029_"] = usrInput_From;
            lic["To_x0020__x0028_Year_x0029_"] = usrInput_To;
            lic["Recognition_x0020_Details"] = usrInput_RDetails;
            lic["Other_x0020_Recognition_x0020_Ty"] = usrInput_ORDetails;
            lic["Recognition_x0020_From"] = usrInput_RFW;
            lic["Title"] = staffid.Value;
            if (Recognition_Attachment.HasFile)
            {
                lic.Attachments.Add(Recognition_Attachment.FileName, Recognition_Attachment.FileBytes);
            }
            lic.Update();
            RecognitionDataLoad();
            RSFCurrVal.Text = 7.ToString();
        }

        protected void MembershipBtn_Click(object sender, EventArgs e)
        {
            SPList MembershipList = SPContext.Current.Web.Lists[MembershipListName];
            string usrInput_MNum = Membership_Number.Text;
            string usrInput_MLevel = Membership_Level.SelectedValue;
            string usrInput_MPos = Membership_Position.Text;
            string usrInput_MPBA = Membership_PBA.Text;
            string usrInput_From = Membership_From.Text;
            string usrInput_To = Membership_To.Text;
            string usrInput_MCat = Membership_Category.Text;
            SPListItem lic = MembershipList.AddItem();
            lic["Membership_x0020_Number"] = usrInput_MNum;
            lic["Level"] = usrInput_MLevel;
            lic["Position"] = usrInput_MPos;
            lic["Profesional_x0020_Bodies_x002f_A"] = usrInput_MPBA;
            lic["From_x0020__x0028_Year_x0029_"] = usrInput_From;
            lic["To_x0020__x0028_Year_x0029_"] = usrInput_To;
            lic["Membership_x0020_Category"] = usrInput_MCat;
            lic["Title"] = staffid.Value;
            if (Membership_Attachment.HasFile)
            {
                lic.Attachments.Add(Membership_Attachment.FileName, Membership_Attachment.FileBytes);
            }
            lic.Update();
            MembershipDataLoad();
            RSFCurrVal.Text = 8.ToString();
        }

        protected void SDP_Btn_Click(object sender, EventArgs e)
        {
            SPList SDPList = SPContext.Current.Web.Lists[SDPListName];
            string usrInput_Duration = SDP_Duration.Text;
            string usrInput_Date = SDP_StartDate.Text;
            string usrInput_Location = SDP_Location.Text;
            SPListItem lic = SDPList.AddItem();
            lic["Duration"] = usrInput_Duration;
            lic["Start_x0020_Date"] = usrInput_Date;
            lic["tbir"] = usrInput_Location;
            lic["Title"] = staffid.Value;
            if (SDP_Attachment.HasFile)
            {
                lic.Attachments.Add(SDP_Attachment.FileName, SDP_Attachment.FileBytes);
            }
            lic.Update();
            SDPDataLoad();
            RSFCurrVal.Text = 10.ToString();
        }

        protected void CAButton_Click(object sender, EventArgs e)
        {
            SPList CAList = SPContext.Current.Web.Lists[CourseAttendedListName];
            string usrInput_CN = CA_CN.Text;
            DateTime usrInput_SD = new DateTime();
            DateTime.TryParse(CA_StartDate.Text,out usrInput_SD);
            string usrInput_Programme = CA_Programme.Text;
            string usrInput_Year = CA_Year.Text;
            string usrInput_Duration = CA_Duration.Text;
            SPListItem lic = CAList.AddItem();
            lic["Course_x0020_Name"] = usrInput_CN;
            lic["Start_x0020_Date"] = usrInput_CN;
            lic["Programme"] = usrInput_Programme;
            lic["Year"] = usrInput_Year;
            lic["Duration"] = usrInput_Duration;
            lic["Title"] = staffid.Value;
            if (CA_Attachment.HasFile)
            {
                lic.Attachments.Add(CA_Attachment.FileName, CA_Attachment.FileBytes);
            }
            lic.Update();
            CourseAttendedDataLoad();
            RSFCurrVal.Text = 11.ToString();
        }

        protected void WEButton_Click(object sender, EventArgs e)
        {
            SPList WEList = SPContext.Current.Web.Lists[WorkExperienceListName];
            string usrInput_CN = WE_CN.Text;
            string usrInput_WE = WE_WE.Text;
            string usrInput_From = WE_From.Text;
            string usrInput_To = WE_To.Text;
            string usrInput_Position = WE_Position.Text;
            SPListItem lic = WEList.AddItem();
            lic["pldr"] = usrInput_CN;
            lic["Work_x0020_Experience"] = usrInput_WE;
            lic["From_x0020__x0028_Year_x0029_"] = usrInput_From;
            lic["To_x0020__x0028_Year_x0029_"] = usrInput_To;
            lic["Position"] = usrInput_Position;
            lic["Title"] = staffid.Value;
            if (WE_Attachment.HasFile)
            {
                lic.Attachments.Add(WE_Attachment.FileName, WE_Attachment.FileBytes);
            }
            lic.Update();
            WorkingExperienceDataLoad();
            RSFCurrVal.Text = 12.ToString();
        }
        
        protected void TEButton_Click(object sender, EventArgs e)
        {
            SPList TEList = SPContext.Current.Web.Lists[TEListName];
            string usrInput_CN = TE_CN.Text;
            string usrInput_Programme = TE_Programme.Text;
            string usrInput_TE_YoS = TE_YoS.Text;
            SPListItem lic = TEList.AddItem();
            lic["Title"] = usrInput_CN;
            lic["Programme"] = usrInput_Programme;
            lic["Year_x0020_of_x0020_Standing"] = usrInput_TE_YoS;
            SPFieldUserValue userValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.LoginName);
            lic["Username"] = userValue;
            if (TE_Attachment.HasFile)
            {
                lic.Attachments.Add(TE_Attachment.FileName, TE_Attachment.FileBytes);
            }
            lic.Update();
            TeachingExperienceDataLoad();
            RSFCurrVal.Text = 13.ToString();
        }

        #endregion

        #region Edit_Function

        private void EditButton(string ListName, string ItemID, string Section)
        {
            SPContext.Current.Web.AllowUnsafeUpdates = true;
            if (ListName == SpacializationListName)
            {
                RSFCurrValEditForm.Text = 2.ToString();
                RSFCurrVal.Text = 2.ToString();
                SpecializationEditFrom(ItemID);
            }
            else if (ListName == QualificationListName)
            {
                RSFCurrValEditForm.Text = 3.ToString();
                RSFCurrVal.Text = 3.ToString();
                QualificationEditForm(ItemID);
            }
            else if (ListName == PublicationListName)
            {
                RSFCurrValEditForm.Text = 4.ToString();
                RSFCurrVal.Text = 4.ToString();
                PublicationEditForm(ItemID);
            }
            else if (ListName == SupervisionListName)
            {
                RSFCurrValEditForm.Text = 5.ToString();
                RSFCurrVal.Text = 5.ToString();
                SupervisionEditForm(ItemID);
            }
            else if (ListName == ResearchListName)
            {
                RSFCurrValEditForm.Text = 6.ToString();
                RSFCurrVal.Text = 6.ToString();
                ResearchEditForm(ItemID);
            }
            else if (ListName == RecognitionListName)
            {
                RSFCurrValEditForm.Text = 7.ToString();
                RSFCurrVal.Text = 7.ToString();
                RecognitionEditForm(ItemID);
            }
            else if (ListName == MembershipListName)
            {
                RSFCurrValEditForm.Text = 8.ToString();
                RSFCurrVal.Text = 8.ToString();
                MembershipEditForm(ItemID);
            }
            else if (ListName == SDPListName)
            {
                RSFCurrValEditForm.Text = 10.ToString();
                RSFCurrVal.Text = 10.ToString();
                SDPEditForm(ItemID);
            }
            else if (ListName == CourseAttendedListName)
            {
                RSFCurrValEditForm.Text = 11.ToString();
                RSFCurrVal.Text = 11.ToString();
                CAEditForm(ItemID);
            }
            else if (ListName == WorkExperienceListName)
            {
                RSFCurrValEditForm.Text = 12.ToString();
                RSFCurrVal.Text = 12.ToString();
                WEEditForm(ItemID);
            }
            else if (ListName == TEListName)
            {
                RSFCurrValEditForm.Text = 13.ToString();
                RSFCurrVal.Text = 13.ToString();
                TEEditForm(ItemID);
            }
            SPContext.Current.Web.AllowUnsafeUpdates = false;
        }

        private void SpecializationEditFrom(string ItemID)
        {
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"<FieldRef Name='e6jh' /><FieldRef Name='j16h' />"
            };

            SPListItem Item = SPContext.Current.Web.Lists[SpacializationListName].GetItems(q)[0];

            Spacialization_Area.Text = WebUtility.HtmlDecode(Regex.Replace(Item["e6jh"].ToString(), "<[^>]*(>|$)", string.Empty));
            Specialization_Consultancy.Text = (string)Item["j16h"];
            Spacialization_ID.Visible = true;
            Spacialization_ID.Value = Item.ID.ToString();
            Specialization_Submit.EnableViewState = true;
            Specialization_Submit.Visible = false;
            Specialization_SubmitEdit.EnableViewState = true;
            Specialization_SubmitEdit.Visible = true;
        }

        private void QualificationEditForm(string ItemID)
        {
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Level_x0020_of_x0020_Qualificati' />
                  <FieldRef Name='Title' />
                  <FieldRef Name='Year' />
                  <FieldRef Name='w91i' />
                  <FieldRef Name='Staff_x0020_No' />
                  <FieldRef Name='Qualifications' />
                  <FieldRef Name='Name' />
                  <FieldRef Name='Username' />
                "
            };
            SPListItemCollection qLIC = SPContext.Current.Web.Lists[QualificationListName].GetItems(q);
            SPListItem Item = qLIC[0];
            Qualification_LOQ.ClearSelection();
            Qualification_LOQ.Items.FindByValue((string)Item["Level_x0020_of_x0020_Qualificati"]);
            Qualification_UNI.Text = (string)Item["Title"];
            Qualification_Year.Text = (string)Item["Year"];
            Qualification_SAoE.Text = (string)Item["w91i"];
            Qualification_Name.Text = (string)Item["Name"];
            Qualification_Qualification.Text = (string)Item["Qualifications"];
            Qualification_ID.Visible = true;
            Qualification_ID.Value = Item.ID.ToString();
            QualificationBtn.EnableViewState = true;
            QualificationBtn.Visible = false;
            QualificationBtnEdit.EnableViewState = true;
            QualificationBtnEdit.Visible = true;
        }

        private void PublicationEditForm(string ItemID)
        {
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Description' /><FieldRef Name='Name' />"
            };
            SPListItem Item = SPContext.Current.Web.Lists[PublicationListName].GetItems(q)[0];            
            Publication_URLPub.Text = WebUtility.HtmlDecode(Regex.Replace(Item["Description"].ToString(), "<[^>]*(>|$)", string.Empty));
            Publication_Name.Text = Item["Name"].ToString();
            Publication_ID.Visible = true;
            Publication_ID.Value = Item.ID.ToString();
            PublicationSbmt.EnableViewState = true;
            PublicationSbmt.Visible = false;
            PublicationEdit.EnableViewState = true;
            PublicationEdit.Visible = true;
        }

        private void SupervisionEditForm(string ItemID)
        {
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Title' />
                  <FieldRef Name='l79i' />
                  <FieldRef Name='Enroll_x0020_Year' />
                  <FieldRef Name='Level_x0020_of_x0020_Qualificati' />
                  <FieldRef Name='Status' />
                  <FieldRef Name='Remarks' />
                  <FieldRef Name='Student_x0020_Grade' />
                  <FieldRef Name='Student_x0020_Enroll' />
                "
            };

            SPListItem Item = SPContext.Current.Web.Lists[SupervisionListName].GetItems(q)[0];
            Supervision_StudName.Text = (string)Item["Title"];
            Supervision_MatrixID.Text = (string)Item["l79i"];
            Supervision_EnrollYear.Text = (string)Item["Enroll_x0020_Year"];
            Supervision_LoQ.Text = (string)Item["Level_x0020_of_x0020_Qualificati"];
            Supervision_Status.Text = (string)Item["Status"];            
            Supervision_Remarks.Text = WebUtility.HtmlDecode(Regex.Replace(Item["Remarks"].ToString(), "<[^>]*(>|$)", string.Empty));
            Supervision_StudentGrade.Text = (string)Item["Student_x0020_Grade"];
            Supervision_StudentEnroll.Text = (string)Item["Student_x0020_Enroll"];
            Supervision_ID.Visible = true;
            Supervision_ID.Value = Item.ID.ToString();
            SupervisionBtn.EnableViewState = true;
            SupervisionBtn.Visible = false;
            SupervisionEdit.EnableViewState = true;
            SupervisionEdit.Visible = true;

        }

        private void ResearchEditForm(string ItemID)
        {
            SPList list = SPContext.Current.Web.Lists[ResearchListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Research_x0020_Title' />
                  <FieldRef Name='Level_x0020_of_x0020_Involvement' />
                  <FieldRef Name='Funded_x002f__x0020_Non_x0020_Fu' />
                  <FieldRef Name='Funded_x0020_Amount_x0020__x0028' />
                  <FieldRef Name='Type_x0020_of_x0020_Funding_x002' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Major_x0020_Output' />
                  <FieldRef Name='Other_x0020_Output' />
                  <FieldRef Name='Remarks' />
                  <FieldRef Name='Position' />
                  <FieldRef Name='Publishing_x0020_Level' />
                  <FieldRef Name='Publishing_x0020_Type' />
                  <FieldRef Name='Other_x0020_Publishing_x0020_Typ' />
                  <FieldRef Name='Source_x0020_Detail' />
                  <FieldRef Name='Amount' />
                  <FieldRef Name='Funding_x0020_Agencies' />
                "
            };
            var Item = list.GetItems(q)[0];

            Research_RTitle.Text = (string)Item["Research_x0020_Title"];
            Research_LoI.Text = (string)Item["Level_x0020_of_x0020_Involvement"];
            Research_FundedorNot.Items.FindByValue((string)Item["Funded_x002f__x0020_Non_x0020_Fu"]);
            Research_FundAmount.Text = (string)Item["Funded_x0020_Amount_x0020__x0028"];
            Research_FundAgencies.Text = (string)Item["Type_x0020_of_x0020_Funding_x002"];
            Research_From.Text = (string)Item["From_x0020__x0028_Year_x0029_"];
            Research_To.Text = (string)Item["To_x0020__x0028_Year_x0029_"];
            Research_MajorOutput.Text = (string)Item["Major_x0020_Output"];
            Research_OtherOutput.Text = (string)Item["Other_x0020_Output"];
            Research_Remarks.Text = (string)Item["Remarks"];
            Research_Position.Text = (string)Item["Position"];
            Research_PublishingLevel.Text = (string)Item["Publishing_x0020_Level"];
            Research_PublishingType.Text = (string)Item["Publishing_x0020_Type"];
            Research_OtherPublishingType.Text = (string)Item["Other_x0020_Publishing_x0020_Typ"];
            Research_SourceDetail.Text = (string)Item["Source_x0020_Detail"];
            Research_Amount.Text = (string)Item["Amount"];
            Research_FundAgencies.Text = (string)Item["Funding_x0020_Agencies"];

            RsearchID.Visible = true;
            RsearchID.Value = Item.ID.ToString();
            Rsearch_SbmtBtn.EnableViewState = true;
            Rsearch_SbmtBtn.Visible = false;
            Rsearch_Edit.EnableViewState = true;
            Rsearch_Edit.Visible = true;
        }

        private void RecognitionEditForm(string ItemID)
        {
            SPList list = SPContext.Current.Web.Lists[RecognitionListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Recognition_x0020_Title' />
                  <FieldRef Name='Level' />
                  <FieldRef Name='Recognition_x0020_Type' />
                  <FieldRef Name='Organiser_x002f_Bodies' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Other_x0020_Recognition_x0020_Ty' />
                  <FieldRef Name='Recognition_x0020_From' />
                  <FieldRef Name='Recognition_x0020_Details' />
                "
            };
            var Item = list.GetItems(q)[0];            
            Recognition_Title.Text = WebUtility.HtmlDecode(Regex.Replace(Item["Recognition_x0020_Title"].ToString(), "<[^>]*(>|$)", string.Empty));
            Recognition_Leve.Items.FindByValue((string)Item["Level"]);
            Recognition_Type.Items.FindByValue((string)Item["Recognition_x0020_Type"]);
            Recognition_OrgBod.Text = (string)Item["Organiser_x002f_Bodies"];
            Recognition_From.Text = (string)Item["From_x0020__x0028_Year_x0029_"];
            Recognition_To.Text = (string)Item["To_x0020__x0028_Year_x0029_"];
            Recognition_Details.Text = (string)Item["Recognition_x0020_Details"];
            Recognition_ORD.Text = (string)Item["Other_x0020_Recognition_x0020_Ty"];
            Recogntion_FromWho.Text = (string)Item["Recognition_x0020_From"];

            RecognitionID.Visible = true;
            RecognitionID.Value = Item.ID.ToString();
            Recognition_Edit_Btn.EnableViewState = true;
            Recognition_Edit_Btn.Visible = true;
            Recognition_Btn.EnableViewState = true;
            Recognition_Btn.Visible = false;
        }

        private void MembershipEditForm(string ItemID)
        {
            SPList list = SPContext.Current.Web.Lists[MembershipListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Membership_x0020_Number' />
                  <FieldRef Name='Level' />
                  <FieldRef Name='Position' />
                  <FieldRef Name='Profesional_x0020_Bodies_x002f_A' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Membership_x0020_Category' />
                "
            };
            SPListItem Item = list.GetItems(q)[0];

            Membership_Number.Text = (string) Item["Membership_x0020_Number"];
            Membership_Level.Items.FindByValue((string) Item["Level"]);
            Membership_Position.Text = (string) Item["Position"];
            Membership_PBA.Text = (string) Item["Profesional_x0020_Bodies_x002f_A"];
            Membership_From.Text = (string) Item["From_x0020__x0028_Year_x0029_"];
            Membership_To.Text = (string) Item["To_x0020__x0028_Year_x0029_"];
            Membership_Category.Text = (string) Item["Membership_x0020_Category"];

            MembershipID.Visible = true;
            MembershipID.Value = Item.ID.ToString();
            MembershipEditBtn.EnableViewState = true;
            MembershipEditBtn.Visible = true;
            MembershipBtn.EnableViewState = true;
            MembershipBtn.Visible = false;
        }

        private void SDPEditForm(string ItemID)
        {
            SPList list = SPContext.Current.Web.Lists[SDPListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Duration' />
                  <FieldRef Name='Start_x0020_Date' />
                  <FieldRef Name='tbir' />
                "
            };
            SPListItem Item = list.GetItems(q)[0];
            SDP_Duration.Text = (string)Item["Duration"];
            SDP_StartDate.Text = (string)Item["Start_x0020_Date"];
            SDP_Location.Text = (string)Item["tbir"];

            SDP_ID.Visible = true;
            SDP_ID.Value = Item.ID.ToString();
            SDP_Edit_Btn.EnableViewState = true;
            SDP_Edit_Btn.Visible = true;
            SDP_Btn.EnableViewState = true;
            SDP_Btn.Visible = false;
        }

        private void CAEditForm(string ItemID)
        {
            SPList list = SPContext.Current.Web.Lists[CourseAttendedListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Course_x0020_Name' />
                  <FieldRef Name='Start_x0020_Date' />
                  <FieldRef Name='Programme' />
                  <FieldRef Name='Year' />
                  <FieldRef Name='Duration' />
                "
            };
            SPListItem Item = list.GetItems(q)[0];
            
            CA_CN.Text = WebUtility.HtmlDecode(Regex.Replace(Item["Course_x0020_Name"].ToString(), "<[^>]*(>|$)", string.Empty));
            CA_StartDate.Text = Item["Start_x0020_Date"] != null ? ((DateTime)Item["Start_x0020_Date"]).ToString("yyyy-MM-dd") : string.Empty;
            CA_Programme.Text = (string)Item["Programme"];
            CA_Year.Text = (string)Item["Year"];
            CA_Duration.Text = (string)Item["Duration"];

            CA_ID.Visible = true;
            CA_ID.Value = Item.ID.ToString();
            CAEditButton.EnableViewState = true;
            CAEditButton.Visible = true;
            CAButton.EnableViewState = true;
            CAButton.Visible = false;
        }

        private void WEEditForm(string ItemID)
        {
            SPList list = SPContext.Current.Web.Lists[WorkExperienceListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='pldr' />
                  <FieldRef Name='Work_x0020_Experience' />
                  <FieldRef Name='From_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='To_x0020__x0028_Year_x0029_' />
                  <FieldRef Name='Position' />
                "
            };
            SPListItem Item = list.GetItems(q)[0];
            WE_CN.Text = (string)Item["pldr"];            
            WE_WE.Text = WebUtility.HtmlDecode(Regex.Replace(Item["Work_x0020_Experience"].ToString(), "<[^>]*(>|$)", string.Empty));
            WE_From.Text = (string)Item["From_x0020__x0028_Year_x0029_"];
            WE_To.Text = (string)Item["To_x0020__x0028_Year_x0029_"];
            WE_Position.Text = (string)Item["Position"];

            WE_ID.Visible = true;
            WE_ID.Value = Item.ID.ToString();
            WEEditButton.EnableViewState = true;
            WEEditButton.Visible = true;
            WEButton.EnableViewState = true;
            WEButton.Visible = false;
        }

        private void TEEditForm(string ItemID)
        {
            SPList list = SPContext.Current.Web.Lists[TEListName];
            var q = new SPQuery()
            {
                Query = $"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>{ItemID}</Value></Eq></Where>",
                ViewFields = @"
                  <FieldRef Name='Title' />
                  <FieldRef Name='Programme' />
                  <FieldRef Name='Year_x0020_of_x0020_Standing' />
                "
            };
            var Item = list.GetItems(q)[0];
            TE_CN.Text = (string)Item["Title"];
            TE_Programme.Text = (string)Item["Programme"];
            TE_YoS.Text = (string)Item["Year_x0020_of_x0020_Standing"];

            TE_ID.Visible = true;
            TE_ID.Value = Item.ID.ToString();
            TEEditButton.EnableViewState = true;
            TEEditButton.Visible = true;
            TEButton.EnableViewState = true;
            TEButton.Visible = false;
        }

        protected void Specialization_SubmitEdit_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[SpacializationListName].GetItemById(Int32.Parse(Spacialization_ID.Value));
            Item["e6jh"] = Spacialization_Area.Text;
            Item["j16h"] = Specialization_Consultancy.Text;
            if(Specialization_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach(string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach(string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(Specialization_Attachment.FileName, Specialization_Attachment.FileBytes);
            }
            Item.Update();
            Spacialization_Area.Text = "";
            Specialization_Consultancy.Text = "";
            RSFCurrVal.Text = 2.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 2.ToString();
            SpecializationDataLoad();
            Spacialization_ID.Visible = false;
            Specialization_Submit.EnableViewState = true;
            Specialization_Submit.Visible = true;
            Specialization_SubmitEdit.EnableViewState = true;
            Specialization_SubmitEdit.Visible = false;
        }

        protected void QualificationBtnEdit_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[QualificationListName].GetItemById(Int32.Parse(Qualification_ID.Value));
            Item["Level_x0020_of_x0020_Qualificati"] = Qualification_LOQ.SelectedValue;
            Item["Title"] = Qualification_UNI.Text;
            Item["Year"] = Qualification_Year.Text;
            Item["w91i"] = Qualification_SAoE.Text;
            Item["Name"] = Qualification_Name.Text;
            Item["Qualifications"] = Qualification_Qualification.Text;
            if (Qualification_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(Qualification_Attachment.FileName, Qualification_Attachment.FileBytes);
            }
            Item.Update();
            RSFCurrVal.Text = 3.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 3.ToString();
            Qualification_LOQ.ClearSelection();
            Qualification_UNI.Text = "";
            Qualification_Year.Text = "";
            Qualification_SAoE.Text = "";
            Qualification_Name.Text = "";
            Qualification_Qualification.Text = "";
            Qualification_ID.Visible = false;
            Qualification_ID.Value = "";
            QualificationBtn.EnableViewState = true;
            QualificationBtn.Visible = true;
            QualificationBtnEdit.EnableViewState = true;
            QualificationBtnEdit.Visible = false;
            QualificationDataLoad();
        }

        protected void PublicationEdit_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[PublicationListName].GetItemById(Int32.Parse(Publication_ID.Value));
            Item["Description"] = Publication_URLPub.Text;
            Item["Name"] = Publication_Name.Text;
            if (Publication_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(Publication_Attachment.FileName, Publication_Attachment.FileBytes);
            }
            Item.Update();
            Publication_URLPub.Text = "";
            Publication_Name.Text = "";
            Publication_ID.Visible = false;
            Publication_ID.Value = "";
            PublicationSbmt.EnableViewState = true;
            PublicationSbmt.Visible = true;
            PublicationEdit.EnableViewState = true;
            PublicationEdit.Visible = false;

            RSFCurrVal.Text = 4.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 4.ToString();
            PublicationDataLoad();
        }

        protected void SupervisionEdit_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[SupervisionListName].GetItemById(Int32.Parse(Supervision_ID.Value));
            Item["Title"] = Supervision_StudName.Text;
            Item["l79i"] = Supervision_MatrixID.Text;
            Item["Enroll_x0020_Year"] = Supervision_EnrollYear.Text;
            Item["Level_x0020_of_x0020_Qualificati"] = Supervision_LoQ.Text;
            Item["Status"] = Supervision_Status.Text;
            Item["Remarks"] = Supervision_Remarks.Text;
            Item["Student_x0020_Grade"] = Supervision_StudentGrade.Text;
            Item["Student_x0020_Enroll"] = Supervision_StudentEnroll.Text;
            if (Supervision_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(Supervision_Attachment.FileName, Supervision_Attachment.FileBytes);
            }
            Item.Update();

            Supervision_StudName.Text = "";
            Supervision_MatrixID.Text = "";
            Supervision_EnrollYear.Text = "";
            Supervision_LoQ.Text = "";
            Supervision_Status.Text = "";
            Supervision_Remarks.Text = "";
            Supervision_StudentGrade.Text = "";
            Supervision_StudentEnroll.Text = "";

            Supervision_ID.Visible = false;
            Supervision_ID.Value = "";
            SupervisionBtn.EnableViewState = true;
            SupervisionBtn.Visible = true;
            SupervisionEdit.EnableViewState = true;
            SupervisionEdit.Visible = false;

            RSFCurrVal.Text = 5.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 5.ToString();
            SupervisionDataLoad();
        }

        protected void Rsearch_Edit_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[ResearchListName].GetItemById(int.Parse(RsearchID.Value));

            Item["Research_x0020_Title"] = Research_RTitle.Text;
            Item["Level_x0020_of_x0020_Involvement"] = Research_LoI.Text;
            Item["Funded_x002f__x0020_Non_x0020_Fu"] = Research_FundedorNot.SelectedValue;
            Item["Funded_x0020_Amount_x0020__x0028"] = Research_FundAmount.Text;
            Item["Type_x0020_of_x0020_Funding_x002"] = Research_FundAgencies.Text;
            Item["From_x0020__x0028_Year_x0029_"] = Research_From.Text;
            Item["To_x0020__x0028_Year_x0029_"] = Research_To.Text;
            Item["Major_x0020_Output"] = Research_MajorOutput.Text;
            Item["Other_x0020_Output"] = Research_OtherOutput.Text;
            Item["Remarks"] = Research_Remarks.Text;
            Item["Position"] = Research_Position.Text;
            Item["Publishing_x0020_Level"] = Research_PublishingLevel.Text;
            Item["Publishing_x0020_Type"] = Research_PublishingType.Text;
            Item["Other_x0020_Publishing_x0020_Typ"] = Research_OtherPublishingType.Text;
            Item["Source_x0020_Detail"] = Research_SourceDetail.Text;
            Item["Amount"] = Research_Amount.Text;
            Item["Funding_x0020_Agencies"] = Research_FundAgencies.Text;
            if (Research_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(Research_Attachment.FileName, Research_Attachment.FileBytes);
            }
            Item.Update();

            Research_RTitle.Text = string.Empty;
            Research_LoI.Text = string.Empty;
            Research_FundedorNot.ClearSelection();
            Research_FundAmount.Text = string.Empty;
            Research_FundAgencies.Text = string.Empty;
            Research_From.Text = string.Empty;
            Research_To.Text = string.Empty;
            Research_MajorOutput.Text = string.Empty;
            Research_OtherOutput.Text = string.Empty;
            Research_Remarks.Text = string.Empty;
            Research_Position.Text = string.Empty;
            Research_PublishingLevel.Text = string.Empty;
            Research_PublishingType.Text = string.Empty;
            Research_OtherPublishingType.Text = string.Empty;
            Research_SourceDetail.Text = string.Empty;
            Research_Amount.Text = string.Empty;
            Research_FundAgencies.Text = string.Empty;

            RSFCurrVal.Text = 6.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 6.ToString();
            ResearchDataLoad();
        }

        protected void Recognition_Edit_Btn_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[RecognitionListName].GetItemById(int.Parse(RecognitionID.Value));
            Item["Recognition_x0020_Title"] = Recognition_Title.Text;
            Item["Level"] = Recognition_Leve.SelectedValue;
            Item["Recognition_x0020_Type"] = Recognition_Type.SelectedValue;
            Item["Organiser_x002f_Bodies"] = Recognition_OrgBod.Text;
            Item["From_x0020__x0028_Year_x0029_"] = Recognition_From.Text;
            Item["To_x0020__x0028_Year_x0029_"] = Recognition_To.Text;
            Item["Recognition_x0020_Details"] = Recognition_Details.Text;
            Item["Other_x0020_Recognition_x0020_Ty"] = Recognition_ORD.Text;
            Item["Recognition_x0020_From"] = Recogntion_FromWho.Text;
            if (Recognition_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(Recognition_Attachment.FileName, Recognition_Attachment.FileBytes);
            }
            Item.Update();

            Recognition_Title.Text = string.Empty;
            Recognition_Leve.SelectedValue = string.Empty;
            Recognition_Type.SelectedValue = string.Empty;
            Recognition_OrgBod.Text = string.Empty;
            Recognition_From.Text = string.Empty;
            Recognition_To.Text = string.Empty;
            Recognition_Details.Text = string.Empty;
            Recognition_ORD.Text = string.Empty;
            Recogntion_FromWho.Text = string.Empty;

            RecognitionID.Visible = false;
            Recognition_Edit_Btn.EnableViewState = true;
            Recognition_Edit_Btn.Visible = false;
            Recognition_Btn.EnableViewState = true;
            Recognition_Btn.Visible = true;

            RSFCurrVal.Text = 7.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 7.ToString();
            RecognitionDataLoad();
        }

        protected void MembershipEditBtn_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[MembershipListName].GetItemById(int.Parse(MembershipID.Value));

            Item["Membership_x0020_Number"] = Membership_Number.Text;
            Item["Level"] = Membership_Level.SelectedValue;
            Item["Position"] = Membership_Position.Text;
            Item["Profesional_x0020_Bodies_x002f_A"] = Membership_PBA.Text;
            Item["From_x0020__x0028_Year_x0029_"] = Membership_From.Text;
            Item["To_x0020__x0028_Year_x0029_"] = Membership_To.Text;
            Item["Membership_x0020_Category"] = Membership_Category.Text;
            if (Membership_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(Membership_Attachment.FileName, Membership_Attachment.FileBytes);
            }
            Item.Update();

            Membership_Number.Text= string.Empty;
            Membership_Level.SelectedValue= string.Empty;
            Membership_Position.Text= string.Empty;
            Membership_PBA.Text= string.Empty;
            Membership_From.Text= string.Empty;
            Membership_To.Text= string.Empty;
            Membership_Category.Text= string.Empty;

            MembershipID.Visible = false;
            MembershipID.Value = string.Empty;
            MembershipEditBtn.EnableViewState = true;
            MembershipEditBtn.Visible = false;
            MembershipBtn.EnableViewState = true;
            MembershipBtn.Visible = true;

            RSFCurrVal.Text = 8.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 8.ToString();
            MembershipDataLoad();
        }

        protected void SDP_Edit_Btn_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[SDPListName].GetItemById(int.Parse(SDP_ID.Value));
            Item["Duration"] = SDP_Duration.Text;
            Item["Start_x0020_Date"] = SDP_StartDate.Text;
            Item["tbir"] = SDP_Location.Text;
            if (SDP_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(SDP_Attachment.FileName, SDP_Attachment.FileBytes);
            }
            Item.Update();

            SDP_Duration.Text = string.Empty;
            SDP_StartDate.Text = string.Empty;
            SDP_Location.Text = string.Empty;

            SDP_ID.Visible = true;
            SDP_ID.Value = string.Empty;
            SDP_Edit_Btn.EnableViewState = true;
            SDP_Edit_Btn.Visible = false;
            SDP_Btn.EnableViewState = true;
            SDP_Btn.Visible = true;

            RSFCurrVal.Text = 10.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 10.ToString();
            SDPDataLoad();
        }

        protected void CAEditButton_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[CourseAttendedListName].GetItemById(int.Parse(CA_ID.Value));
            Item["Course_x0020_Name"] = CA_CN.Text;
            Item["Start_x0020_Date"] = DateTime.Parse(CA_StartDate.Text);
            Item["Programme"] = CA_Programme.Text;
            Item["Year"] = CA_Year.Text;
            Item["Duration"] = CA_Duration.Text;
            if (CA_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(CA_Attachment.FileName, CA_Attachment.FileBytes);
            }
            Item.Update();

            CA_CN.Text = string.Empty;
            CA_StartDate.Text = string.Empty;
            CA_Programme.Text = string.Empty;
            CA_Year.Text = string.Empty;
            CA_Duration.Text = string.Empty;

            CA_ID.Visible = true;
            CA_ID.Value = Item.ID.ToString();
            CAEditButton.EnableViewState = true;
            CAEditButton.Visible = false;
            CAButton.EnableViewState = true;
            CAButton.Visible = true;

            RSFCurrVal.Text = 11.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 11.ToString();
            CourseAttendedDataLoad();
        }

        protected void WEEditButton_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[WorkExperienceListName].GetItemById(int.Parse(WE_ID.Value));
            
            Item["pldr"] = WE_CN.Text;
            Item["Work_x0020_Experience"] = WE_WE.Text;
            Item["From_x0020__x0028_Year_x0029_"] = WE_From.Text;
            Item["To_x0020__x0028_Year_x0029_"] = WE_To.Text;
            Item["Position"] = WE_Position.Text;
            if (WE_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(WE_Attachment.FileName, WE_Attachment.FileBytes);
            }
            Item.Update();

            WE_CN.Text = string.Empty;
            WE_WE.Text = string.Empty;
            WE_From.Text = string.Empty;
            WE_To.Text = string.Empty;
            WE_Position.Text = string.Empty;

            WE_ID.Visible = false;
            WE_ID.Value = string.Empty;
            WEEditButton.EnableViewState = true;
            WEEditButton.Visible = false;
            WEButton.EnableViewState = true;
            WEButton.Visible = true;

            RSFCurrVal.Text = 12.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 12.ToString();
            WorkingExperienceDataLoad();
        }

        protected void TEEditButton_Click(object sender, EventArgs e)
        {
            SPListItem Item = SPContext.Current.Web.Lists[TEListName].GetItemById(int.Parse(TE_ID.Value));

            Item["Title"] = TE_CN.Text;
            Item["Programme"] = TE_Programme.Text;
            Item["Year_x0020_of_x0020_Standing"] = TE_YoS.Text;
            if (TE_Attachment.HasFile)
            {
                List<string> FileNames = new List<string>();
                foreach (string Names in Item.Attachments)
                {
                    FileNames.Add(Names);
                }
                foreach (string FName in FileNames)
                {
                    Item.Attachments.DeleteNow(FName);
                }
                Item.Attachments.Add(TE_Attachment.FileName, TE_Attachment.FileBytes);
            }
            Item.Update();

            TE_CN.Text = string.Empty;
            TE_Programme.Text = string.Empty;
            TE_YoS.Text = string.Empty;

            TE_ID.Visible = false;
            TE_ID.Value = string.Empty;
            TEEditButton.EnableViewState = true;
            TEEditButton.Visible = false;
            TEButton.EnableViewState = true;
            TEButton.Visible = true;

            RSFCurrVal.Text = 13.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 13.ToString();
            TeachingExperienceDataLoad();
        }
        #endregion

        #region Cancel_Function
        protected void Specialization_CancelForm_Click(object sender, EventArgs e)
        {
            Spacialization_Area.Text = "";
            Specialization_Consultancy.Text = "";
            RSFCurrVal.Text = 2.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 2.ToString();

            Spacialization_ID.Visible = true;
            Spacialization_ID.Value = string.Empty;
            Specialization_Submit.EnableViewState = true;
            Specialization_Submit.Visible = true;
            Specialization_SubmitEdit.EnableViewState = true;
            Specialization_SubmitEdit.Visible = false;
        }

        protected void Qualification_Cancel_Click(object sender, EventArgs e)
        {
            RSFCurrVal.Text = 3.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 3.ToString();
            Qualification_LOQ.ClearSelection();
            Qualification_UNI.Text = "";
            Qualification_Year.Text = "";
            Qualification_SAoE.Text = "";
            Qualification_Name.Text = "";
            Qualification_Qualification.Text = "";

            Qualification_ID.Visible = false;
            Qualification_ID.Value = "";
            QualificationBtn.EnableViewState = true;
            QualificationBtn.Visible = true;
            QualificationBtnEdit.EnableViewState = true;
            QualificationBtnEdit.Visible = false;
        }

        protected void Publication_Cancel_Click(object sender, EventArgs e)
        {
            Publication_URLPub.Text = "";
            Publication_Name.Text = "";
            Publication_ID.Visible = false;
            Publication_ID.Value = "";
            PublicationSbmt.EnableViewState = true;
            PublicationSbmt.Visible = true;
            PublicationEdit.EnableViewState = true;
            PublicationEdit.Visible = false;

            RSFCurrVal.Text = 4.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 4.ToString();
        }

        protected void Supervision_Cancel_Click(object sender, EventArgs e)
        {
            Supervision_StudName.Text = "";
            Supervision_MatrixID.Text = "";
            Supervision_EnrollYear.Text = "";
            Supervision_LoQ.Text = "";
            Supervision_Status.Text = "";
            Supervision_Remarks.Text = "";
            Supervision_StudentGrade.Text = "";
            Supervision_StudentEnroll.Text = "";

            RSFCurrVal.Text = 5.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 5.ToString();

            Supervision_ID.Visible = false;
            Supervision_ID.Value = "";
            SupervisionBtn.EnableViewState = true;
            SupervisionBtn.Visible = true;
            SupervisionEdit.EnableViewState = true;
            SupervisionEdit.Visible = false;
        }

        protected void Research_Cancel_Click(object sender, EventArgs e)
        {
            Research_RTitle.Text = string.Empty;
            Research_LoI.Text = string.Empty;
            Research_FundedorNot.ClearSelection();
            Research_FundAmount.Text = string.Empty;
            Research_FundAgencies.Text = string.Empty;
            Research_From.Text = string.Empty;
            Research_To.Text = string.Empty;
            Research_MajorOutput.Text = string.Empty;
            Research_OtherOutput.Text = string.Empty;
            Research_Remarks.Text = string.Empty;
            Research_Position.Text = string.Empty;
            Research_PublishingLevel.Text = string.Empty;
            Research_PublishingType.Text = string.Empty;
            Research_OtherPublishingType.Text = string.Empty;
            Research_SourceDetail.Text = string.Empty;
            Research_Amount.Text = string.Empty;
            Research_FundAgencies.Text = string.Empty;

            RSFCurrVal.Text = 6.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 6.ToString();
        }

        protected void Recignition_Cancel_Click(object sender, EventArgs e)
        {
            Recognition_Title.Text = string.Empty;
            Recognition_Leve.SelectedValue = string.Empty;
            Recognition_Type.SelectedValue = string.Empty;
            Recognition_OrgBod.Text = string.Empty;
            Recognition_From.Text = string.Empty;
            Recognition_To.Text = string.Empty;
            Recognition_Details.Text = string.Empty;
            Recognition_ORD.Text = string.Empty;
            Recogntion_FromWho.Text = string.Empty;

            RecognitionID.Visible = false;
            Recognition_Edit_Btn.EnableViewState = true;
            Recognition_Edit_Btn.Visible = false;
            Recognition_Btn.EnableViewState = true;
            Recognition_Btn.Visible = false;

            RSFCurrVal.Text = 7.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 7.ToString();
        }

        protected void Membership_Cancel_Click(object sender, EventArgs e)
        {
            Membership_Number.Text = string.Empty;
            Membership_Level.SelectedValue = string.Empty;
            Membership_Position.Text = string.Empty;
            Membership_PBA.Text = string.Empty;
            Membership_From.Text = string.Empty;
            Membership_To.Text = string.Empty;
            Membership_Category.Text = string.Empty;

            MembershipID.Visible = false;
            MembershipID.Value = string.Empty;
            MembershipEditBtn.EnableViewState = true;
            MembershipEditBtn.Visible = false;
            MembershipBtn.EnableViewState = true;
            MembershipBtn.Visible = true;

            RSFCurrVal.Text = 8.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 8.ToString();
        }

        protected void SDP_Cancel_Click(object sender, EventArgs e)
        {
            SDP_Duration.Text = string.Empty;
            SDP_StartDate.Text = string.Empty;
            SDP_Location.Text = string.Empty;

            SDP_ID.Visible = true;
            SDP_ID.Value = string.Empty;
            SDP_Edit_Btn.EnableViewState = true;
            SDP_Edit_Btn.Visible = true;
            SDP_Btn.EnableViewState = true;
            SDP_Btn.Visible = false;

            RSFCurrVal.Text = 10.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 10.ToString();
        }

        protected void CA_Cancel_Click(object sender, EventArgs e)
        {
            CA_CN.Text = string.Empty;
            CA_StartDate.Text = string.Empty;
            CA_Programme.Text = string.Empty;
            CA_Year.Text = string.Empty;
            CA_Duration.Text = string.Empty;

            CA_ID.Visible = true;
            CA_ID.Value = string.Empty;
            CAEditButton.EnableViewState = true;
            CAEditButton.Visible = false;
            CAButton.EnableViewState = true;
            CAButton.Visible = true;

            RSFCurrVal.Text = 11.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 11.ToString();
        }

        protected void WE_Cancel_Click(object sender, EventArgs e)
        {
            WE_CN.Text = string.Empty;
            WE_WE.Text = string.Empty;
            WE_From.Text = string.Empty;
            WE_To.Text = string.Empty;
            WE_Position.Text = string.Empty;

            WE_ID.Visible = false;
            WE_ID.Value = string.Empty;
            WEEditButton.EnableViewState = true;
            WEEditButton.Visible = false;
            WEButton.EnableViewState = true;
            WEButton.Visible = true;

            RSFCurrVal.Text = 12.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 12.ToString();
        }

        protected void TE_Cancel_Click(object sender, EventArgs e)
        {
            TE_CN.Text = string.Empty;
            TE_Programme.Text = string.Empty;
            TE_YoS.Text = string.Empty;

            TE_ID.Visible = false;
            TE_ID.Value = string.Empty;
            TEEditButton.EnableViewState = true;
            TEEditButton.Visible = false;
            TEButton.EnableViewState = true;
            TEButton.Visible = true;

            RSFCurrVal.Text = 13.ToString();
            RSFCurrValEditForm.Text = "";
            RSFCurrValDeleteForm.Text = 13.ToString();
        }
        #endregion

        protected void SDP_StartDate_DataBinding(object sender, EventArgs e)
        {
            SDP_StartDate.Text = DateTime.Parse(SDP_StartDate.Text).ToString("dd-MMM-yyyy");
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            if(UploadPhoto.HasFile)
            {
                using (SPWeb Web = SPContext.Current.Site.OpenWeb())
                {
                    Web.AllowUnsafeUpdates = true;
                    
                    //Validate If got existing image
                    SPUser ForPhoto = Web.EnsureUser(UserEmail.Value);
                    SPQuery Query = new SPQuery()
                    {
                        Query = $"<Where><Eq><FieldRef Name='Username' /><Value Type='User'>{ForPhoto.Name}</Value></Eq></Where>",
                        ViewFields = "<FieldRef Name='Title' />"
                    };
                    SPListItemCollection Images = Web.Lists[PersonalPhotoListName].GetItems(Query);
                    if(Images.Count > 0)
                    {
                        List<int> ids = new List<int>();
                        foreach(SPListItem Img in Images)
                        {
                            ids.Add(Img.ID);
                            Web.GetFile(Img["Title"].ToString()).Delete();
                        }
                        foreach (int Id in ids)
                        {
                            Images.DeleteItemById(Id);
                        }
                    }
                    SPFile Files = Web.GetFolder("PublishingImages/CV Photo").Files.Add(UploadPhoto.FileName, UploadPhoto.FileBytes, true);
                    SPListItem NewItem = Web.Lists[PersonalPhotoListName].AddItem();
                    NewItem["Title"] = Files.ServerRelativeUrl;
                    NewItem["Username"] = Web.EnsureUser(UserEmail.Value);
                    NewItem.Update();
                    uploadimage = true;
                    Web.AllowUnsafeUpdates = false;
                }
                Page.Response.RedirectPermanent(Page.Request.RawUrl);
            }
        }
    }
}

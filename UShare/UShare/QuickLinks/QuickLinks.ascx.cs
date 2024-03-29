﻿using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UShare.QuickLinks
{
    [ToolboxItemAttribute(false)]
    public partial class QuickLinks : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        [WebBrowsable(true), WebDisplayName("Source List Name"), WebDescription("Source List Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListName { get; set; }

        [WebBrowsable(true), WebDisplayName("Source List Category Name"), WebDescription("Source List Category Name"), Personalizable(PersonalizationScope.Shared)]
        public string ListCategoryName { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Source List Category Name"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }

        public QuickLinks()
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
                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                StringBuilder stringBuilder3 = new StringBuilder();
                StringBuilder stringBuilder4 = new StringBuilder();

                //Grabing the Category
                string _tempCategory, CurrentCategory = "";
                bool _sec1, _sec2, _sec3, _sec4;
                _sec1 = _sec2 = _sec3 = _sec4 = false;


                SPList list = Web.Lists[new Guid("297d6a4e-40ae-4aa6-8239-2ab99256f6cf")];
                var query = new SPQuery()
                {
                    Query = @"<Where><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></Where><OrderBy><FieldRef Name='Sequence' Ascending='True' /></OrderBy>"
                };

                var r = list.GetItems(query);
                foreach(SPListItem cItem in r)
                {
                    var q = new SPQuery()
                    {
                        Query = $"<Where><And><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq><Eq><FieldRef Name='Category' /><Value Type='Lookup'>{cItem["Title"].ToString()}</Value></Eq></And></Where><OrderBy><FieldRef Name='Sequence' Ascending='True' /></OrderBy>",
                        ViewFields = @"  <FieldRef Name='URL' /><FieldRef Name='Comments' /><FieldRef Name='Category' /><FieldRef Name='Category_x003a_Section' />"
                    };
                    SPListItemCollection Items = Web.Lists[ListName].GetItems(q);
                    foreach (SPListItem Item in Items)
                    {
                        q = new SPQuery()
                        {
                            Query = $"<Where><And><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq><Eq><FieldRef Name='Category' /><Value Type='Lookup'>{cItem}</Value></Eq></And></Where><OrderBy><FieldRef Name='Category_x003a_Sequence' Ascending='True' /><FieldRef Name='URL' Ascending='True' /></OrderBy>",
                            ViewFields = @"  <FieldRef Name='URL' /><FieldRef Name='Comments' /><FieldRef Name='Category' /><FieldRef Name='Category_x003a_Section' />"
                        };
                        StringBuilder stringBuilder = new StringBuilder();
                        _tempCategory = Item[Helper.QLCategoryField].ToString().Contains("#") ? Item[Helper.QLCategoryField].ToString().Split('#')[1] : Item[Helper.QLCategoryField].ToString();
                        int sec = Convert.ToInt32(Convert.ToDouble(Item[Helper.QLSectionField].ToString().Contains("#") ? Item[Helper.QLSectionField].ToString().Split('#')[1] : Item["Category_x003a_Section"].ToString()));
                        switch (sec)
                        {
                            case 1: stringBuilder = stringBuilder1; _sec1 = true; break;
                            case 2: stringBuilder = stringBuilder2; _sec2 = true; break;
                            case 3: stringBuilder = stringBuilder3; _sec3 = true; break;
                            case 4: stringBuilder = stringBuilder4; _sec4 = true; break;
                        };
                        if (_tempCategory != CurrentCategory)
                        {
                            if (CurrentCategory != "")
                            {
                                stringBuilder.Append("</ul>");
                            }
                            CurrentCategory = _tempCategory;
                            stringBuilder.Append($"<div class='link-category'>{CurrentCategory}</div>");
                            stringBuilder.Append("<ul>");
                        }
                        SPFieldUrlValue Url = new SPFieldUrlValue(Item[Helper.QLUrlField].ToString());
                        stringBuilder.Append($"<li><a href = '{Url.Url}' >{Url.Description}</a></li>");
                    }
                }
                QuickLink1.Text = stringBuilder1.ToString();
                QuickLink2.Text = stringBuilder2.ToString();
                QuickLink3.Text = stringBuilder3.ToString();
                QuickLink4.Text = stringBuilder4.ToString();
            }
            catch (Exception ex)
            {
                QuickLink1.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

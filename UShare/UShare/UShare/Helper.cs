﻿using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShare
{
    public class Helper
    {
        //Upcoming Event (WebPart)
        public static string UPQuery { get { return @"<OrderBy><FieldRef Name='Expires' Ascending='True' /></OrderBy>"; } }
        public static string UPTitleField { get { return "Title"; } }
        public static string UPExpiresField { get { return "Expires"; } }
        public static string UPBodyField { get { return "Body"; } }

        //The Latest
        public static string TLQuery { get { return @"<OrderBy><FieldRef Name='Expires' Ascending='True' /></OrderBy>"; } }
        public static string TLTitleField { get { return "Picture"; } }
        public static string TLPictureField { get { return "Picture"; } }

        //Quick Link
        public static string QLQuery { get { return @"<Where><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></Where><OrderBy><FieldRef Name='Category_x003a_Sequence' Ascending='True' /><FieldRef Name='URL' Ascending='True' /></OrderBy>"; } }
        public static string QLViewField { get { return @"  <FieldRef Name='URL' /><FieldRef Name='Comments' /><FieldRef Name='Category' /><FieldRef Name='Category_x003a_Section' />"; } }
        public static string QLCategoryField { get { return "Category"; } }
        public static string QLSectionField { get { return "Category_x003a_Section"; } }
        public static string QLUrlField { get { return "URL"; } }

        //Mega Dropdown
        public static string MDQuery { get { return @"<Where><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></Where><OrderBy><FieldRef Name='Category_x003a_Sequence' Ascending='True' /><FieldRef Name='URL' Ascending='True' /></OrderBy>"; } }
        public static string MDViewField { get { return @"<FieldRef Name='URL' /><FieldRef Name='Category' /><FieldRef Name='Category_x003a_Section' />"; } }
        public static string MDSectionField { get { return "Category_x003a_Section"; } }
        public static string MDCategoryField { get { return "Category"; } }
        public static string MDUrlField { get { return "URL"; } }

        //Audience Pathaway
        public static string APQuery { get { return @"<OrderBy><FieldRef Name='URL' Ascending='True' /></OrderBy>"; } }
        public static string APViewField { get { return @"<FieldRef Name='URL' />"; } }
        public static string APUrlField { get { return "URL"; } }

        //Ushare Navigation
        public static string DepartmentQuery(int id)
        {
            return $"<Where><Eq><FieldRef Name='Division_x003a_ID' /><Value Type='Lookup'>{id}</Value></Eq></Where>";
        }
        public static string DepartmentViewField { get { return "<FieldRef Name='URL' />"; } }
        public static string UnitsQuery(int id)
        {
            return $"<Where><Eq><FieldRef Name='Department_x003a_ID' /><Value Type='Lookup'>{id}</Value></Eq></Where>";
        }
        public static string UnitsViewField { get { return "<FieldRef Name='URL' />"; } }
        public static string DepartURL { get { return "URL"; } }
        public static string UnitURL { get { return "URL"; } }

        public static string NoImageURL { get { return SPContext.Current.Web.Url + "/SiteAssets/No_Image_Available.jpg"; } }
        private static SPDiagnosticsService spd = SPDiagnosticsService.Local;
        public static SPListItemCollection GetListItems(string ListName, string Query, uint RowLimit = 5)
        {
            try
            {
                SPQuery query = new SPQuery()
                {
                    ViewXml = Query,
                    RowLimit = RowLimit
                };
                return SPContext.Current.Web.Lists[ListName].GetItems(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Log(string Message)
        {
            //SPDiagnosticsCategory spc = new SPDiagnosticsCategory("ZEN_WebPart", TraceSeverity.High, EventSeverity.Error);
            //spd.WriteTrace(0, spc, TraceSeverity.High, Message, null);
        }
    }
}

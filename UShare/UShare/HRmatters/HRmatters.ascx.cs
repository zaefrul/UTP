﻿using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace UShare.HRmatters
{
    [ToolboxItemAttribute(false)]
    public partial class HRmatters : WebPart
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

        [WebBrowsable(true), WebDisplayName("Item Lists URL"), WebDescription("Component Row Limit"), Personalizable(PersonalizationScope.Shared)]
        public string ListsURL { get; set; }

        [WebBrowsable(true), WebDisplayName("Item Detail URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string DetailURL { get; set; }

        [WebBrowsable(true), WebDisplayName("Metadata site URL"), WebDescription("Item Detail URL"), Personalizable(PersonalizationScope.Shared)]
        public string SiteCollection { get; set; }
        public HRmatters()
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
                btnMore.Text = $"<a href='{ListsURL}'><section class='sec-hrmatters-btn'>Read More <i class='fas fa-angle-right fa-more-btn'></i></section></a>";

                SPWeb Web = new SPSite(SiteCollection).OpenWeb();
                SPListItemCollection sPListItem = Web.Lists[ListName].GetItems(new SPQuery() { Query = Helper.TLQuery, RowLimit = 5 });
                StringBuilder stringBuilder = new StringBuilder();
                foreach (SPListItem Item in sPListItem)
                {
                    stringBuilder.Append("<section class=\"sec-hrmatters-row\">");
                    DateTime Expires = DateTime.Parse(Item["Created"].ToString());
                    stringBuilder.Append($"<section class=\"sec-hrmatters-date\"><div class=\"day\">{Expires.ToString("dd")}</div><div class=\"month\">{Expires.ToString("MMM")}</div></section>");
                    stringBuilder.Append("<section class=\"sec-hrmatters-name\">");
                    stringBuilder.Append($"<a href='{string.Format("{0}?hrid={1}", DetailURL, Item.ID)}' class=''>{Item["Title"].ToString()}</a>");
                    stringBuilder.Append("</section>");
                    stringBuilder.Append("</section>");
                }
                ltrTheLatest.Text = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                ltrTheLatest.Text = ex.Message;
                //Helper.Log(ex.Message);
            }
        }
    }
}

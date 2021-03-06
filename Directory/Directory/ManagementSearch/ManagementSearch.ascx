﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManagementSearch.ascx.cs" Inherits="Directory.ManagementSearch.ManagementSearch" %>
<section class="directory-form">

    <div class="information">
        <div class="icon"><i class="fas fa-user-tie"></i></div>
        <div class="title">Management Search</div>
    </div>


    <div class="form">

        <div class="item-row">
            <div class="item-input col10">
                <label for="InputName">Name</label>
                <asp:TextBox ID="Name" runat="server" CssClass="form-control" placeholder="Enter name"></asp:TextBox>
            </div>
        </div>


        <div class="item-row">
            <div class="item-input col5">
                <label for="InputPos">Position</label>
                <asp:DropDownList ID="Position" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="item-input col5">
                <label for="InputDept">Department</label>
                <asp:DropDownList ID="Department" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
    </div>


</section>



<section class="directory-result">
    <asp:Literal ID="Results" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="Details" runat="server" Visible="false"></asp:Literal>
</section>

<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Directory.ascx.cs" Inherits="Directory.Directory.Directory" %>

<section class="form">
    <section class="form-title">
        Directory Search
    </section>
    <section class="form-row ">
        <section class="form-label">
            Name
        </section>
        <section class="form-field">
            <asp:TextBox ID="Name" runat="server" CssClass="form-control"></asp:TextBox>
        </section>
    </section>

    <section class="form-row">
        <section class="form-label">
            Position
        </section>
        <section class="form-field">
            <asp:DropDownList ID="Position" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </section>
    </section>

    <section class="form-row">
        <section class="form-label">
            Department
        </section>
        <section class="form-field">
            <asp:DropDownList ID="Department" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </section>
    </section>

    <section class="form-row">
        <section class="form-label">
            Nationality
        </section>
        <section class="form-field">
            <asp:DropDownList ID="Nationality" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </section>
    </section>

    <section class="form-row">
        <section class="form-label">
            Area of Expertise
        </section>
        <section class="form-field">
            <asp:TextBox ID="AOExperties" runat="server" CssClass="form-control"></asp:TextBox>
        </section>
    </section>

    <section class="form-submit">
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="form-submit"  />
    </section>
</section>


<asp:Literal ID="Results" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="Details" runat="server" Visible="false"></asp:Literal>
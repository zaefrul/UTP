<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MegaDropdown.ascx.cs" Inherits="UShare.MegaDropdown.MegaDropdown" %>
<%--<link href="../_layouts/15/UShare/style_ushare.css" rel="stylesheet" />--%>
<section class="mega-sec-one">
    <section class="mega-content" runat="server" id="mega_division">
    </section>
</section>

<section class="mega-sec-two" runat="server" id="mega_academic">
</section>

<section class="mega-sec-three" runat="server" id="mega_Units">
</section>

<script type="text/JavaScript">
    var currentdiv = "";
    var currentdepartment = "";

    function divisionBtn(el) {
        var newSelection = document.getElementById(el);
        var currentSelection = document.getElementById(currentdiv);
        var currentDepartmentSelection = document.getElementById(currentdepartment);

        if (currentSelection != null) { currentSelection.style.display = "none"; }
        if (currentDepartmentSelection != null) { currentDepartmentSelection.style.display = "none"; }
        newSelection.style.display = "table";
        currentdiv = el;
    }
    function departmentBtn(name) {
        var newSelection = document.getElementById(name);
        var currentSelection = document.getElementById(currentdepartment);
        var currentDepartmentSelection = document.getElementById(currentdepartment);
        if (currentSelection != null) { currentSelection.style.display = "none"; }
        if (currentDepartmentSelection != null) { currentDepartmentSelection.style.display = "none"; }
        newSelection.style.display = "table";
        currentdepartment = name;
    }

    /*selection active*/
    var divBtns = document.getElementsByClassName("btnDivNav");
    for (var i = 0; i < divBtns.length; i++) {
        divBtns[i].addEventListener("click", function () {
            var currentDiv = document.getElementsByClassName("navDivActive ");
            if (currentDiv.length > 0) {
                currentDiv[0].className = currentDiv[0].className.replace(" navDivActive ", "");
            }
            this.className += " navDivActive ";
        });
    }


    var deptBtns = document.getElementsByClassName("btnDeptNav");
    for (var j = 0; j < deptBtns.length; j++) {
        deptBtns[j].addEventListener("click", function () {
            var currentDept = document.getElementsByClassName("navDeptActive ");
            if (currentDept.length > 0) {
                currentDept[0].className = currentDept[0].className.replace(" navDeptActive ", "");
            }
            this.className += " navDeptActive ";
        });
    }
</script>

<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Administration.ascx.cs" Inherits="Directory.Administration.Administration" %>
<%--<link href="../_layouts/15/Directory/directoryForm.css" rel="stylesheet" />--%>
<!--navigation pane--->

    
    <div class="directoryMobileBtn" onclick="directoryFormBtnFunction()">
        <i class="fas fa-bars directoryMobileBtnIcon"></i>
    </div>

<section id="dirFormNav" class="leftPane">

    <div class="profileColumn">
        <asp:Literal runat="server" ID="MiniProfile"></asp:Literal>
        <asp:HiddenField runat="server" ID="staffid" />
        <input type="hidden" id="RSFValue" value="<asp:literal runat='server' id='RSFCurrVal' />" />
        <input type="hidden" id="RSFValueEditForm" value="<asp:literal runat='server' id='RSFCurrValEditForm' />" />
        <input type="hidden" id="RSFValueDeletForm" value="<asp:literal runat='server' id='RSFCurrValDeleteForm' />" />
    </div>

    <div class="leftPaneMenu">
        <div id="LPF1" onclick="myFormFunction(1);directoryFormBtnFunction()" class="topicNav topicNavSelected">Profile</div>
        <div id="LPF2" onclick="myFormFunction(2);directoryFormBtnFunction()" class="topicNav">Specialization</div>
        <%--<asp:LinkButton runat="server" ID="SpecializationBtn" OnClick="SpecializationBtn_Click" OnClientClick="myFormFunction(2)"><div id="LPF2" onclick="myFormFunction(2)" class="topicNav">Specialization</div></asp:LinkButton>--%>
        <div id="LPF3" onclick="myFormFunction(3);directoryFormBtnFunction()" class="topicNav">Qualification</div>
        <div id="LPF4" onclick="myFormFunction(4);directoryFormBtnFunction()" class="topicNav">Publication</div>
        <div id="LPF5" onclick="myFormFunction(5);directoryFormBtnFunction()" class="topicNav">Supervision</div>
        <div id="LPF6" onclick="myFormFunction(6);directoryFormBtnFunction()" class="topicNav">Research</div>
        <div id="LPF7" onclick="myFormFunction(7);directoryFormBtnFunction()" class="topicNav">Recognition</div>
        <div id="LPF8" onclick="myFormFunction(8);directoryFormBtnFunction()" class="topicNav">Membership</div>
        <!--<div onclick="myFormFunction(9)" class="topicNav">PreviewCV</div>-->
        <div id="LPF9" style="display:none" onclick="myFormFunction(9);directoryFormBtnFunction()" class="topicNav">MOR</div>
        <div id="LPF10" onclick="myFormFunction(10);directoryFormBtnFunction()" class="topicNav">SDP</div>
        <div id="LPF11" onclick="myFormFunction(11);directoryFormBtnFunction()" class="topicNav">Course Attended</div>
        <div id="LPF12" onclick="myFormFunction(12);directoryFormBtnFunction()" class="topicNav">Working Experience</div>
        <div id="LPF13" onclick="myFormFunction(13);directoryFormBtnFunction()" class="topicNav">Teaching Experience</div>
    </div>
</section>

<section class="rightPane">

    <!--Profile form-->
    <div id="RPF1" class="topicCategory">
        <div class="topicTitle">Profile</div>

        <div class="listView">
            <asp:Literal runat="server" ID="PDetail"></asp:Literal>
            <div class="item-input">
                <label>Upload Photo</label>
                <asp:HiddenField runat="server" ID="UserEmail" />
                <asp:UpdatePanel runat="server" ID="UploadPhotoUP">
                    <ContentTemplate>
                        <asp:FileUpload ID="UploadPhoto" runat="server" CssClass="form-control" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ProfileSubmit" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="item-input">
                <asp:Button CssClass="btn btn-primary" runat="server" OnClick="Unnamed_Click1" ID="ProfileSubmit" Text="Submit" />
            </div>
        </div>
    </div>

    <!--Specialization form-->
    <div id="RPF2" class="topicCategory formHidden">
        <div class="topicTitle">Specialization</div>
        <div id="RPL2" class="listView">
            <asp:Literal runat="server" ID="SDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(2)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV2" class="formView formHidden">
            <div class="formRow">
                <asp:HiddenField runat="server" ID="Spacialization_ID" Visible="false" />
                <div class="item-input ">
                    <label for="InputName">Area</label>
                    <asp:TextBox ID="Spacialization_Area" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <%--<textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>--%>
                </div>

                <div class="item-input ">
                    <label for="InputName">Consultancy</label>
                    <%--<input type="text" class="form-control" id="InputName" placeholder="Enter consultancy">--%>
                    <asp:TextBox runat="server" ID="Specialization_Consultancy" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel_Specialization" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="Specialization_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="Specialization_Submit" />
                           <asp:PostBackTrigger ControlID="Specialization_SubmitEdit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="formRow">
                <asp:Button Text="Submit" runat="server" ID="Specialization_Submit" OnClick="Specialization_Submit_Click" CssClass="btn btn-primary" />
                <asp:Button Text="Submit" Visible="false" runat="server" ID="Specialization_SubmitEdit" OnClick="Specialization_SubmitEdit_Click" CssClass="btn btn-success" />
                <asp:Button Text="Cancel" runat="server" ID="Specialization_CancelForm" CssClass="btn btn-danger" OnClick="Specialization_CancelForm_Click" />
                <%--<button type="submit" class="btn btn-primary">Submit</button>--%>

            </div>
        </div>
    </div>

    <!--Qualification form-->
    <div id="RPF3" class="topicCategory formHidden">
        <div class="topicTitle">Qualification</div>

        <div id="RPL3" class="listView">
            <asp:Literal runat="server" ID="QDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(3)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV3" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Level of Qualification</label>
                    <asp:DropDownList CssClass="form-control" runat="server" ID="Qualification_LOQ">
                        <asp:ListItem Value="N/A">N/A</asp:ListItem>
                        <asp:ListItem Value="Diploma">Diploma</asp:ListItem>
                        <asp:ListItem Value="Degree">Degree</asp:ListItem>
                        <asp:ListItem Value="Master">Master</asp:ListItem>
                        <asp:ListItem Value="PhD">PhD</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">University</label>
                    <%--<input type="text" class="form-control" id="InputName" placeholder="Enter University">--%>
                    <asp:TextBox runat="server" ID="Qualification_UNI" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Year</label>
                    <asp:TextBox runat="server" ID="Qualification_Year" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Specialization or Area of Expertise</label>
                    <%--<input type="text" class="form-control" id="InputName" placeholder="Enter Specialization or Area of Expertise">--%>
                    <asp:TextBox runat="server" ID="Qualification_SAoE" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Name</label>
                    <%--<input type="text" class="form-control" id="InputName" placeholder="Enter Name">--%>
                    <asp:TextBox runat="server" ID="Qualification_Name" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Qualification</label>
                    <%--<input type="text" class="form-control" id="InputName" placeholder="Enter Qualification">--%>
                    <asp:TextBox runat="server" ID="Qualification_Qualification" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="Qualification_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="QualificationBtn" />
                           <asp:PostBackTrigger ControlID="QualificationBtnEdit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="Qualification_ID" Visible="false" />
                <asp:Button Text="Submit" runat="server" ID="QualificationBtn" OnClick="QualificationBtn_Click" CssClass="btn btn-primary" />
                <asp:Button Text="Submit" Visible="false" runat="server" ID="QualificationBtnEdit" OnClick="QualificationBtnEdit_Click" CssClass="btn btn-success" />
                <asp:Button Text="Cancel" runat="server" ID="Qualification_Cancel" CssClass="btn btn-danger" OnClick="Qualification_Cancel_Click" />

            </div>
        </div>

    </div>

    <!--Publication form-->
    <div id="RPF4" class="topicCategory formHidden">
        <div class="topicTitle">Publication</div>

        <div id="RPL4" class="listView">
            <asp:Literal runat="server" ID="PubDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(4)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV4" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">URL Publication</label>
                    <asp:TextBox runat="server" ID="Publication_URLPub" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Name</label>
                    <asp:TextBox runat="server" ID="Publication_Name" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="Publication_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="PublicationSbmt" />
                           <asp:PostBackTrigger ControlID="PublicationEdit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>                
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="Publication_ID" Visible="false" />
                <asp:Button Text="Submit" Visible="false" runat="server" ID="PublicationEdit" CssClass="btn btn-success" OnClick="PublicationEdit_Click" />
                <asp:Button Text="Submit" runat="server" ID="PublicationSbmt" CssClass="btn btn-primary" OnClick="PublicationSbmt_Click" />
                <asp:Button Text="Cancel" runat="server" ID="Publication_Cancel" CssClass="btn btn-danger" OnClick="Publication_Cancel_Click" />
            </div>
        </div>
    </div>

    <!--Supervision form-->
    <div id="RPF5" class="topicCategory formHidden">
        <div class="topicTitle">Supervision</div>

        <div id="RPL5" class="listView">
            <asp:Literal runat="server" ID="SuperDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(5)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV5" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Student Name</label>
                    <asp:TextBox runat="server" ID="Supervision_StudName" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Metric ID</label>
                    <asp:TextBox runat="server" ID="Supervision_MatrixID" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Enroll Year</label>
                    <asp:TextBox runat="server" ID="Supervision_EnrollYear" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Level of Qualification</label>
                    <asp:ListBox runat="server" CssClass="form-control" ID="Supervision_LoQ">
                        <asp:ListItem Value="SPM">SPM</asp:ListItem>
                        <asp:ListItem Value="Diploma">Diploma</asp:ListItem>
                        <asp:ListItem Value="Degree">Degree</asp:ListItem>
                        <asp:ListItem Value="Master">Master</asp:ListItem>
                        <asp:ListItem Value="PhD">PhD</asp:ListItem>
                    </asp:ListBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Status</label>
                    <asp:ListBox runat="server" CssClass="form-control" ID="Supervision_Status">
                        <asp:ListItem Value="SPM">Withdraw</asp:ListItem>
                        <asp:ListItem Value="Diploma">Graduated</asp:ListItem>
                    </asp:ListBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Remarks</label>
                    <asp:TextBox runat="server" ID="Supervision_Remarks" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Student Grade</label>
                    <asp:TextBox runat="server" ID="Supervision_StudentGrade" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Student Enroll</label>
                    <asp:TextBox runat="server" ID="Supervision_StudentEnroll" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="Supervision_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="SupervisionBtn" />
                           <asp:PostBackTrigger ControlID="SupervisionEdit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>      
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="Supervision_ID" Visible="false" />
                <asp:Button Text="Submit" runat="server" ID="SupervisionEdit" CssClass="btn btn-success" OnClick="SupervisionEdit_Click" Visible="false" />
                <asp:Button Text="Submit" runat="server" ID="SupervisionBtn" CssClass="btn btn-primary" OnClick="SupervisionBtn_Click" />
                <asp:Button Text="Cancel" runat="server" ID="Supervision_Cancel" CssClass="btn btn-danger" OnClick="Supervision_Cancel_Click" />

            </div>
        </div>
    </div>

    <!--Research form-->
    <div id="RPF6" class="topicCategory formHidden">
        <div class="topicTitle">Research</div>

        <div id="RPL6" class="listView">
            <asp:Literal runat="server" ID="ResearchDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(6)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV6" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Research Title</label>
                    <asp:TextBox runat="server" ID="Research_RTitle" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Level of Involvement</label>
                    <asp:DropDownList runat="server" ID="Research_LoI" CssClass="form-control">
                        <asp:ListItem Value="Member">Member</asp:ListItem>
                        <asp:ListItem Value="Member">Leader</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">Funded/Non Funded</label>
                    <div class="radioRow">
                        <%--<input class="" type="radio" name="exampleRadios" id="exampleRadios1" value="option1" checked>
                        <label class="form-check-label" for="exampleRadios1">
                            Funded
                        </label>

                        <input class="" type="radio" name="exampleRadios" id="exampleRadios2" value="option2" checked>
                        <label class="form-check-label" for="exampleRadios1">
                            Non-Funded
                        </label>--%>
                        <asp:RadioButtonList CssClass="radioRow" runat="server" ID="Research_FundedorNot">
                            <asp:ListItem Text="Funded" Value="Funded"></asp:ListItem>
                            <asp:ListItem Text="Non-Funded" Value="Non-Funded"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <div class="item-input ">
                    <label for="InputName">Funded Amount (RM)</label>
                    <asp:TextBox runat="server" ID="Research_FundAmount" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Type of Funding Agencies</label>
                    <asp:DropDownList runat="server" ID="Research_FundAgencies" CssClass="form-control">
                        <asp:ListItem Value="None">Select Type of Funding Agencies</asp:ListItem>
                        <asp:ListItem Value="None">None</asp:ListItem>
                        <asp:ListItem Value="Government">Government</asp:ListItem>
                        <asp:ListItem Value="Private">Private</asp:ListItem>
                        <asp:ListItem Value="Internal">Internal</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">From (Year)</label>
                    <asp:TextBox runat="server" ID="Research_From" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">To (Year)</label>
                    <asp:TextBox runat="server" ID="Research_To" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Major Output</label>
                    <asp:DropDownList runat="server" ID="Research_MajorOutput" CssClass="form-control">
                        <asp:ListItem Value="None">Select Major Output</asp:ListItem>
                        <asp:ListItem Value="Prototype">Prototype</asp:ListItem>
                        <asp:ListItem Value="Others">Others</asp:ListItem>
                        <asp:ListItem Value="Product Commercialized">Product Commercialized</asp:ListItem>
                        <asp:ListItem Value="IP">IP</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">Other Output</label>
                    <asp:TextBox runat="server" ID="Research_OtherOutput" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Remarks</label>
                    <asp:TextBox runat="server" ID="Research_Remarks" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Position</label>
                    <asp:TextBox runat="server" ID="Research_Position" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Publishing Level</label>
                    <asp:TextBox runat="server" ID="Research_PublishingLevel" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Publishing Type</label>
                    <asp:TextBox runat="server" ID="Research_PublishingType" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Other Publishing Type</label>
                    <asp:TextBox runat="server" ID="Research_OtherPublishingType" CssClass="form-control"></asp:TextBox>

                </div>

                <div class="item-input ">
                    <label for="InputName">Source Detail</label>
                    <asp:TextBox runat="server" ID="Research_SourceDetail" CssClass="form-control"></asp:TextBox>

                </div>

                <div class="item-input ">
                    <label for="InputName">Amount</label>
                    <asp:TextBox runat="server" ID="Research_Amount" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Funding Agencies</label>
                    <asp:TextBox runat="server" ID="Research_FundingAgencies" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="Research_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="Rsearch_SbmtBtn" />
                           <asp:PostBackTrigger ControlID="Rsearch_Edit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div> 
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="RsearchID" Visible="false" />;
                <asp:Button Text="Submit" CssClass="btn btn-success" runat="server" Visible="false" ID="Rsearch_Edit" OnClick="Rsearch_Edit_Click" />
                <asp:Button Text="Submit" CssClass="btn btn-primary" runat="server" ID="Rsearch_SbmtBtn" OnClick="Rsearch_SbmtBtn_Click" />
                <asp:Button Text="Cancel" runat="server" ID="Research_Cancel" CssClass="btn btn-danger" OnClick="Research_Cancel_Click" />
            </div>
        </div>
    </div>

    <!--Recognition form-->
    <div id="RPF7" class="topicCategory formHidden">
        <div class="topicTitle">Recognition</div>

        <div id="RPL7" class="listView">
            <asp:Literal runat="server" ID="RecognitionDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(7)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV7" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Recognition Title</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="Recognition_Title" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Level</label>
                    <asp:DropDownList runat="server" ID="Recognition_Leve" CssClass="form-control">
                        <asp:ListItem Text="" Value="Select Recogntion Level"></asp:ListItem>
                        <asp:ListItem Text="International" Value="International"></asp:ListItem>
                        <asp:ListItem Text="National" Value="National"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">Recognition Type</label>
                    <asp:DropDownList runat="server" ID="Recognition_Type" CssClass="form-control">
                        <asp:ListItem Text="" Value="Select Recogntion Type"></asp:ListItem>
                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                        <asp:ListItem Text="Awarded" Value="Awarded"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">Organizer/Bodies</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="Recognition_OrgBod"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">From (Year)</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="Recognition_From" TextMode="Number"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">To (Year)</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="Recognition_To" TextMode="Number"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Recognition Details</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="Recognition_Details"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Other Recognition Details</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="Recognition_ORD"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Recognition From</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="Recogntion_FromWho"></asp:TextBox>
                </div>
                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="Recognition_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="Recognition_Btn" />
                           <asp:PostBackTrigger ControlID="Recognition_Edit_Btn" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div> 
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="RecognitionID" Visible="false" />
                <asp:Button runat="server" Visible="false" ID="Recognition_Edit_Btn" CssClass="btn btn-success" Text="Submit" OnClick="Recognition_Edit_Btn_Click" />
                <asp:Button runat="server" ID="Recognition_Btn" CssClass="btn btn-primary" Text="Submit" OnClick="Recognition_Btn_Click" />
                <asp:Button Text="Cancel" runat="server" ID="Recignition_Cancel" CssClass="btn btn-danger" OnClick="Recignition_Cancel_Click" />

            </div>
        </div>
    </div>

    <!--Membership form-->
    <div id="RPF8" class="topicCategory formHidden">
        <div class="topicTitle">Membership</div>

        <div id="RPL8" class="listView">
            <asp:Literal runat="server" ID="MembershipDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(8)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV8" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Membership Number</label>
                    <asp:TextBox runat="server" ID="Membership_Number" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Level</label>
                    <asp:DropDownList runat="server" ID="Membership_Level" CssClass="form-control">
                        <asp:ListItem Text="Select Membership Level" Value=""></asp:ListItem>
                        <asp:ListItem Text="International" Value="International"></asp:ListItem>
                        <asp:ListItem Text="National" Value="National"></asp:ListItem>
                        <asp:ListItem Text="State" Value="State"></asp:ListItem>
                        <asp:ListItem Text="District" Value="District"></asp:ListItem>
                        <asp:ListItem Text="Local" Value="Local"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">Position</label>
                    <asp:TextBox runat="server" ID="Membership_Position" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Professional Bodies/Association</label>
                    <asp:TextBox runat="server" ID="Membership_PBA" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">From (Year)</label>
                    <asp:TextBox runat="server" ID="Membership_From" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">To (Year)</label>
                    <asp:TextBox runat="server" ID="Membership_To" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="item-input ">
                    <label for="InputName">Membership Category</label>
                    <asp:TextBox runat="server" ID="Membership_Category" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="Membership_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="MembershipBtn" />
                           <asp:PostBackTrigger ControlID="MembershipEditBtn" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="MembershipID" Visible="false" />
                <asp:Button runat="server" ID="MembershipEditBtn" CssClass="btn btn-success" Text="Submit" Visible="false" OnClick="MembershipEditBtn_Click" />
                <asp:Button runat="server" ID="MembershipBtn" CssClass="btn btn-primary" Text="Submit" OnClick="MembershipBtn_Click" />
                <asp:Button Text="Cancel" runat="server" ID="Membership_Cancel" CssClass="btn btn-danger" OnClick="Membership_Cancel_Click" />
            </div>
        </div>
    </div>

    <!--PreviewCV form-->
    <!--<div id="RPF9" class=""></div>-->

    <!--MOR form-->
    <div id="RPF9" class="topicCategory formHidden">
        <div class="topicTitle">MOR</div>

        <div id="RPL9" class="listView">
            <table class="table table-striped">
                <thead class="">
                    <tr>
                        <th scope="col" style="width: 50px;">No</th>
                        <th scope="col">Membership of MOR</th>
                        <th scope="col">MOR Details</th>
                        <th scope="col">Area of Expertise</th>
                        <th scope="col" style="width: 120px;">Option</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>Membership of MOR 1</td>
                        <td>MOR Details 1</td>
                        <td>Area of Expertise 1</td>
                        <td>Delete | Edit</td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td>Membership of MOR 2</td>
                        <td>MOR Details 2</td>
                        <td>Area of Expertise 2</td>
                        <td>Delete | Edit</td>
                    </tr>
                </tbody>
            </table>

            <div class="formRow">

                <button type="submit" onclick="myFormAddFunction(9)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV9" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Membership of MOR</label>
                    <select class="form-control" id="InputNat">
                        <option>Select Membership of MOR</option>
                        <option>2</option>
                        <option>3</option>
                        <option>4</option>
                        <option>5</option>
                    </select>
                </div>

                <div class="item-input ">
                    <label for="InputName">MOR Details</label>
                    <input type="text" class="form-control" id="InputName" placeholder="Enter MOR Details">
                </div>

                <div class="item-input ">
                    <label for="InputName">Area of Expertise</label>
                    <input type="text" class="form-control" id="InputName" placeholder="Enter Area of Expertise">
                </div>

            </div>

            <div class="formRow">

                <button type="submit" class="btn btn-primary">Submit</button>
                <button type="submit" onclick="myFormCancelFunction(9)" class="btn btn-danger">Cancel</button>

            </div>
        </div>
    </div>

    <!--SDP form-->
    <div id="RPF10" class="topicCategory formHidden">
        <div class="topicTitle">SDP</div>

        <div id="RPL10" class="listView">
            <asp:Literal runat="server" ID="SDPDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(10)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV10" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Duration</label>
                    <asp:TextBox runat="server" ID="SDP_Duration" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Start Date</label>
                    <asp:TextBox runat="server" ID="SDP_StartDate" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Location</label>
                    <asp:TextBox runat="server" ID="SDP_Location" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="SDP_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="SDP_Btn" />
                           <asp:PostBackTrigger ControlID="SDP_Edit_Btn" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="SDP_ID" Visible="false" />
                <asp:Button runat="server" ID="SDP_Edit_Btn" Visible="false" Text="Submit" CssClass="btn btn-success" OnClick="SDP_Edit_Btn_Click" />
                <asp:Button runat="server" ID="SDP_Btn" CssClass="btn btn-primary" Text="Submit" OnClick="SDP_Btn_Click" />
                <asp:Button Text="Cancel" runat="server" ID="SDP_Cancel" CssClass="btn btn-danger" OnClick="SDP_Cancel_Click" />
                

            </div>
        </div>
    </div>

    <!--Course Attended form-->
    <div id="RPF11" class="topicCategory formHidden">
        <div class="topicTitle">Course Attended</div>

        <div id="RPL11" class="listView">
            <asp:Literal runat="server" ID="CADetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(11)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV11" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Course Name</label>
                    <asp:TextBox runat="server" ID="CA_CN" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Start Date</label>
                    <asp:TextBox runat="server" ID="CA_StartDate" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Programme</label>
                    <asp:TextBox runat="server" ID="CA_Programme" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Year</label>
                    <asp:TextBox runat="server" ID="CA_Year" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Duration</label>
                    <asp:TextBox runat="server" ID="CA_Duration" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="CA_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="CAButton" />
                           <asp:PostBackTrigger ControlID="CAEditButton" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="CA_ID" Visible="false" />
                <asp:Button runat="server" ID="CAEditButton" Visible="false" Text="Submit" CssClass="btn btn-success" OnClick="CAEditButton_Click" />
                <asp:Button runat="server" ID="CAButton" CssClass="btn btn-primary" Text="Submit" OnClick="CAButton_Click" />
                <asp:Button Text="Cancel" runat="server" ID="CA_Cancel" CssClass="btn btn-danger" OnClick="CA_Cancel_Click" />

            </div>
        </div>
    </div>

    <!--Working Experience form-->
    <div id="RPF12" class="topicCategory formHidden">
        <div class="topicTitle">Working Experience Form</div>

        <div id="RPL12" class="listView">
            <asp:Literal runat="server" ID="WEDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(12)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV12" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Company Name</label>
                    <asp:TextBox runat="server" ID="WE_CN" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Work Experience</label>
                    <asp:TextBox runat="server" ID="WE_WE" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">From (Year)</label>
                    <asp:TextBox runat="server" ID="WE_From" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">To (Year)</label>
                    <asp:TextBox runat="server" ID="WE_To" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Position</label>
                    <asp:TextBox runat="server" ID="WE_Position" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="WE_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="WEButton" />
                           <asp:PostBackTrigger ControlID="WEEditButton" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="WE_ID" Visible="false" />
                <asp:Button runat="server" ID="WEEditButton" CssClass="btn btn-success" Visible="false" Text="Submit" OnClick="WEEditButton_Click" />
                <asp:Button runat="server" ID="WEButton" CssClass="btn btn-primary" Text="Submit" OnClick="WEButton_Click" />
                <asp:Button Text="Cancel" runat="server" ID="WE_Cancel" CssClass="btn btn-danger" OnClick="WE_Cancel_Click" />

            </div>
        </div>
    </div>

    <!--Teaching Experience form-->
    <div id="RPF13" class="topicCategory formHidden">
        <div class="topicTitle">Teaching Experience</div>

        <div id="RPL13" class="listView">
            <asp:Literal runat="server" ID="TEDetail"></asp:Literal>

            <div class="formRow">

                <button type="button" onclick="myFormAddFunction(13)" class="btn btn-success">Add New</button>

            </div>
        </div>

        <div id="RPFV13" class="formView formHidden">
            <div class="formRow">

                <div class="item-input ">
                    <label for="InputName">Course Name</label>
                    <asp:TextBox runat="server" ID="TE_CN" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="item-input ">
                    <label for="InputName">Programme</label>
                    <asp:DropDownList runat="server" ID="TE_Programme" CssClass="form-control">
                        <asp:ListItem Text="Not Applicable" Value="Not Applicable" />
                        <asp:ListItem Text="Foundation" Value="Foundation" />
                        <asp:ListItem Text="Applied Chemistry" Value="Applied Chemistry" />
                        <asp:ListItem Text="Applied Physics" Value="Applied Physics" />
                        <asp:ListItem Text="Chemical Engineering" Value="Chemical Engineering" />
                        <asp:ListItem Text="Civil Engineering" Value="Civil Engineering" />
                        <asp:ListItem Text="Electrical & Electronics Engineering" Value="Electrical & Electronics Engineering" />
                        <asp:ListItem Text="Mechanical Engineering" Value="Mechanical Engineering" />
                        <asp:ListItem Text="Petroleum Engineering" Value="Petroleum Engineering" />
                        <asp:ListItem Text="Geoscience" Value="Geoscience" />
                        <asp:ListItem Text="Business Information System" Value="Business Information System" />
                        <asp:ListItem Text="Information & Communication Technology" Value="Information & Communication Technology" />
                        <asp:ListItem Text="MSc - Petroleum Engineering" Value="MSc - Petroleum Engineering" />
                        <asp:ListItem Text="MSc - Petroleum Geoscience" Value="MSc - Petroleum Geoscience" />
                        <asp:ListItem Text="MSc - Process Integration" Value="MSc - Process Integration" />
                        <asp:ListItem Text="MBA in Energy Management" Value="MBA in Energy Management" />
                        <asp:ListItem Text="Master of Philosophy" Value="Master of Philosophy" />
                        <asp:ListItem Text="Doctor of Philosophy" Value="Doctor of Philosophy" />
                    </asp:DropDownList>
                </div>

                <div class="item-input ">
                    <label for="InputName">Year of Standing</label>
                    <asp:DropDownList runat="server" ID="TE_YoS" CssClass="form-control">
                        <asp:ListItem Text="Not Applicable" Value="Not Applicable" />
                        <asp:ListItem Text="Foundation" Value="Foundation" />
                        <asp:ListItem Text="Foundation Sem 1" Value="Foundation Sem 1" />
                        <asp:ListItem Text="Foundation Sem 2" Value="Foundation Sem 2" />
                        <asp:ListItem Text="Year 1 Sem 1" Value="Year 1 Sem 1" />
                        <asp:ListItem Text="Year 1 Sem 2" Value="Year 1 Sem 2" />
                        <asp:ListItem Text="Year 1 Sem 3" Value="Year 1 Sem 3" />
                        <asp:ListItem Text="Year 2 Sem 1" Value="Year 2 Sem 1" />
                        <asp:ListItem Text="Year 2 Sem 2" Value="Year 2 Sem 2" />
                        <asp:ListItem Text="Year 3 Sem 1" Value="Year 3 Sem 1" />
                        <asp:ListItem Text="Year 3 Sem 2" Value="Year 3 Sem 2" />
                        <asp:ListItem Text="Year 4 Sem 1" Value="Year 4 Sem 1" />
                        <asp:ListItem Text="Year 4 Sem 2" Value="Year 3 Sem 2" />
                    </asp:DropDownList>
                </div>

                <div class="item-input">
                    <label for="InputName">Attachment</label>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload runat="server" ID="TE_Attachment" CssClass="form-control" />
                        </ContentTemplate>
                        <Triggers>
                           <asp:PostBackTrigger ControlID="TEButton" />
                           <asp:PostBackTrigger ControlID="TEEditButton" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="formRow">
                <asp:HiddenField runat="server" ID="TE_ID" Visible="false" />
                <asp:Button runat="server" ID="TEEditButton" CssClass="btn btn-primary" Text="Submit" Visible="false" OnClick="TEEditButton_Click" />
                <asp:Button runat="server" ID="TEButton" CssClass="btn btn-primary" Text="Submit" OnClick="TEButton_Click" />
                <asp:Button Text="Cancel" runat="server" ID="TE_Cancel" CssClass="btn btn-danger" OnClick="TE_Cancel_Click" />

            </div>
        </div>
    </div>


</section>

<script>
    if (!isNaN(document.getElementById("RSFValue").value)) {
        myFormFunction(document.getElementById("RSFValue").value);
    }

    function myFormFunction(rpfNo) {
        var rpf = "RPF" + rpfNo;
        var lpf = "LPF" + rpfNo;
        var y = document.getElementById(lpf);
        var x = document.getElementById(rpf);


        var i;
        for (i = 1; i < 14; i++) {

            var cr = "RPF" + i;
            var lr = "LPF" + i;
            var formList = document.getElementById(cr);
            var buttonList = document.getElementById(lr);
            formList.style.display = "none";
            buttonList.classList.remove("topicNavSelected");

        }

        if (x.style.display === "none") {
            x.style.display = "block";
            y.classList.add("topicNavSelected");
        }

    }
    function onclickdelete(event) {
        if (confirm("Are you sure to delete this record?")) {
            var form = document.createElement("form");
            var element1 = document.createElement("input"); 
            var element2 = document.createElement("input");  
            var element3 = document.createElement("input");  
            var element4 = document.createElement("input");  
            form.method = "POST";
            form.name = "Delete";
            var information = event.id.split('_');
            element1.value = information[0];
            element1.name="listname";
            form.appendChild(element1);      
            element2.value=information[1];
            element2.name = "itemid";
            form.appendChild(element2);
            element3.value=information[2];
            element3.name = "section";
            form.appendChild(element3);
            element4.value="Delete";
            element4.name = "action";
            form.appendChild(element4);
            document.body.appendChild(form);
            form.submit();
        }
    }
    function onEditClick(event) {
        var form = document.createElement("form");
        var element1 = document.createElement("input"); 
        var element2 = document.createElement("input");  
        var element3 = document.createElement("input");
        form.method = "POST";
        form.name = "Edit";
        var information = event.id.split('_');
        element1.value = information[0];
        element1.name="listname";
        form.appendChild(element1);      
        element2.value=information[1];
        element2.name = "itemid";
        form.appendChild(element2);
        element3.value=information[2];
        element3.name = "section";
        form.appendChild(element3);
        document.body.appendChild(form);
        form.submit();
    }
</script>

<script>
    //alert(document.getElementById("RSFValueEditForm").value)
    if (!isNaN(document.getElementById("RSFValueEditForm").value)) {
        myFormAddFunction(document.getElementById("RSFValueEditForm").value)
    }
    function myFormAddFunction(rplNo) {
        var rpl = "RPL" + rplNo;
        var rpfv = "RPFV" + rplNo;
        var y = document.getElementById(rpl);
        var x = document.getElementById(rpfv);

        y.style.display = "none";
        x.style.display = "block";
        document.getElementById("RSFValueEditForm").value = "";
    }
</script>

<script>
    if (!isNaN(document.getElementById("RSFValueDeleteForm").value)) {
        myFormCancelFunction(document.getElementById("RSFValueDeleteForm").value)
    }
    function myFormCancelFunction(rplNo) {
        var rpl = "RPL" + rplNo;
        var rpfv = "RPFV" + rplNo;
        var y = document.getElementById(rpl);
        var x = document.getElementById(rpfv);

        x.style.display = "none";
        y.style.display = "block";
        document.getElementById("RSFValueDeleteForm").value = "";
    }
</script>

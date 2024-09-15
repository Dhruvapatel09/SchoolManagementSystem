<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendance.aspx.cs" Inherits="SchoolManagementSyatem.Admin.EmployeeAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Image/bg4.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <div class="ml-auto text-right">
                <asp:ScriptManager runat="server"></asp:ScriptManager>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Timer runat="server" OnTick="Timer1_Tick" interval="1000"></asp:Timer>
                        <asp:Label runat="server" ID="lblTime" Font-Bold="true"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <h3 class="text-center">Teacher's Attendance</h3>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12 ">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-borderd"  EmptyDataText="No Record to Display!" >
                        <columns>
                         
                            <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="RadioButton1" runat="server" Text="Present" Checked="true" GroupName="attendance" CssClass="form-check-input"/>
                                    </div>
                                   <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="RadioButton2" runat="server" Text="Absent"  GroupName="attendance"  CssClass="form-check-input"/>
                                    </div>
                                    </ItemTemplate>
                                   
                               <%-- <itemtemplate>
                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                </itemtemplate>--%>
                                <itemstyle horizontalalign="Center" />
                            </asp:TemplateField>
                           
                        </columns>
                        <headerstyle backcolor="#5558C9" forecolor="White" />
                    </asp:GridView>
                </div>
            </div>
                <div class="row mb-3 mr-lg-5 ml-lg-5 ">
                    <div class="col-md-16 col-lg-4 col-xl-3 col-md-offset-2 mb-3">
                        <asp:Button ID="btnMarkAttendance" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Mark Attendance" OnClick="btnMarkAttendanced_Click" />
                    </div>
                </div>

           
        </div>
</asp:Content>

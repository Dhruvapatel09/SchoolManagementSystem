﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="EmpAttendanceDetails.aspx.cs" Inherits="SchoolManagementSyatem.Admin.EmpAttendanceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Image/bg4.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h3 class="text-center">Teacher Attendance Deatils</h3>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="ddlClass">Teacher</label>
                    <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Teacher is required"
                        ControlToValidate="ddlTeacher" Display="Dynamic" ForeColor="Red"
                        InitialValue="Select Teacher" SetFocusOnError="True">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtMonth">Month</label>
                    <asp:TextBox ID="txtMonth" CssClass="form-control" runat="server" TextMode="Month" required></asp:TextBox>
                </div>

            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <asp:Button ID="btnCheckAttendance" runat="server" CssClass="btn btn-primary btn-block" Text="Check Attendance" OnClick="btnCheckAttendance_Click" />
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-8">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display!" AutoGenerateColumns="False"
                        AllowPaging="true" PageSize="8" >
                        <Columns>
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="label1" Text='<%# Boolean.Parse(Eval("Status").ToString()) ? "Present" : "Absent" %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Date" HeaderText="Date">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                           

                        </Columns>
                        <HeaderStyle BackColor="#5558C9" ForeColor="White" />
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

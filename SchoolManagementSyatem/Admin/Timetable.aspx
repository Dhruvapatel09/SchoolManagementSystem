<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="Timetable.aspx.cs" Inherits="SchoolManagementSyatem.Admin.Timetable" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:GridView ID="GridViewTimetable" runat="server" AutoGenerateColumns="false" CssClass="timetable-grid">
            <Columns>
                <asp:BoundField DataField="DayName" HeaderText="Day" />
                <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                <asp:BoundField DataField="ClassName" HeaderText="Class" />
                <asp:BoundField DataField="Name" HeaderText="Teacher" />
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

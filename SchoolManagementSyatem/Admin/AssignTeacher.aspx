<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AssignTeacher.aspx.cs" Inherits="SchoolManagementSyatem.Admin.AssignTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Assign Teacher</h2>
    <div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <br />
        <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>

        <asp:DropDownList ID="ddlTimeSlots" runat="server" CssClass="form-control">
            <asp:ListItem Text="09:00 - 10:00" Value="1"></asp:ListItem>
            <asp:ListItem Text="10:00 - 11:00" Value="2"></asp:ListItem>
            <asp:ListItem Text="11:00 - 12:00" Value="3"></asp:ListItem>
        </asp:DropDownList>

        <asp:DropDownList ID="ddlDaysOfWeek" runat="server" CssClass="form-control">
            <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
            <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
            <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
            <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
            <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
        </asp:DropDownList>

        <asp:TextBox ID="txtRoomId" runat="server" CssClass="form-control" placeholder="Enter Room ID"></asp:TextBox>
        <asp:Button ID="btnAssign" runat="server" Text="Assign Teacher" OnClick="btnAssign_Click" />
        <asp:Label ID="lblNoTimetable" runat="server" Text="No timetable found for this class." Visible="false"></asp:Label>
        <asp:PlaceHolder ID="timetablePlaceholder" runat="server"></asp:PlaceHolder>
         <asp:Button ID="btnViewTimetable" runat="server" Text="View Timetable" CssClass="btn btn-primary" OnClick="btnViewTimetable_Click" />
<asp:Table ID="timetableTable" runat="server" CssClass="timetable-table"></asp:Table>
    </div>
</asp:Content>

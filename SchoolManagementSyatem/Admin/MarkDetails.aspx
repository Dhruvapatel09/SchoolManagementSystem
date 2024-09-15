<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="MarkDetails.aspx.cs" Inherits="SchoolManagementSyatem.Admin.MarksDetails" %>

<%@ Register Src="~/MarksDetailUserConreol.ascx" TagPreFix="uc" TagName="MarksDetail"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:MarksDetail runat="server" ID="MarksDetail1"></uc:MarksDetail>
   
</asp:Content>

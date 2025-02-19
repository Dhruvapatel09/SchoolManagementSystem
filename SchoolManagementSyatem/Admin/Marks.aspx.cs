﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSyatem.Models.CommonFn;

namespace SchoolManagementSyatem.Admin
{
    public partial class Marks : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetMarks();
            }
        }
        private void GetClass()
        {
            DataTable dataTable = fn.Fetch("select * from Class");
            ddlClass.DataSource = dataTable;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");
        }
        private void GetMarks()
        {
            DataTable dt = fn.Fetch(@"Select ROW_NUMBER() OVER(ORDER BY(SELECT 1)) AS [Sr.No],e.ExamId,e.ClassId,c.ClassName, e.SubjectId,s.SubjectName,e.RollNo,e.TotalMarks,e.OutOfMarks from Exam e
INNER JOIN Class c on c.ClassId= e.ClassId
INNER JOIN Subject s on s.SubjectId= e.SubjectId");
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;
            DataTable dt = fn.Fetch("SELECT * FROM Subject where ClassId='" + classId + "' ");
            ddlSubject.DataSource = dt;
            ddlSubject.DataTextField = "SubjectName";
            ddlSubject.DataValueField = "SubjectId";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "Select Subject");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                string rollNo = txtRoll.Text.Trim();
                string studMarks = txtStudMarks.Text.Trim();
                string outOfMarks = txtOutOfMarks.Text.Trim();
                DataTable dttbl = fn.Fetch("SELECT StudentId FROM Student WHERE ClassId='" + classId +
              "' and RollNo='" + rollNo + "'");

                if (dttbl.Rows.Count > 0)
                {
                    DataTable dt = fn.Fetch("SELECT * FROM Exam WHERE ClassId='" + classId +
                  "' and SubjectId='" + subjectId + "'and RollNo='" + rollNo + "'");

                    if (dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Exam Values('" + classId + "','" + subjectId + "','" + rollNo + "','" + studMarks + "','" + outOfMarks + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted Successfully";
                        lblMsg.CssClass = "alert alert-success";
                        ddlClass.SelectedIndex = 0;
                        ddlSubject.SelectedIndex = 0;
                        txtRoll.Text = string.Empty;
                        txtStudMarks.Text = string.Empty;
                        txtOutOfMarks.Text = string.Empty;

                        GetMarks();
                    }
                    else
                    {
                        lblMsg.Text = "Entered <b>Data</b> already exists !";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Text = "Entered Roll number<b>"+rollNo+"</b> does not exists for selected class !";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetMarks();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetMarks();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int examId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
                string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;
                string rollNo = (row.FindControl("txtRollNoGv") as TextBox).Text;
                string studMarks = (row.FindControl("txtStudMarksGv") as TextBox).Text;
                string outOfMarks = (row.FindControl("txtOutOfMarksGv") as TextBox).Text;
                

                // Update query 
                fn.Query(@"UPDATE Exam SET ClassId='" + classId + "', SubjectId='" + subjectId + "', Rollno='" + rollNo + "',TotalMarks='"+studMarks+"',OutOfMarks='"+outOfMarks+"' WHERE ExamId='" + examId + "'");

                lblMsg.Text = "Record updated Successfully";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetMarks();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClassGv");
                    DropDownList ddlSubject = (DropDownList)e.Row.FindControl("ddlSubjectGv");
                    DataTable dt = fn.Fetch("Select * from Subject where ClassId='"   +ddlClass.SelectedValue+"' ");
                    ddlSubject.DataSource = dt;
                    ddlSubject.DataTextField = "SubjectName";
                    ddlSubject.DataValueField = "SubjectId";
                    ddlSubject.DataBind();
                    ddlSubject.Items.Insert(0, "Select Subject");

                    // Set the selected value based on the data item
                    string selectedSubject = DataBinder.Eval(e.Row.DataItem, "SubjectName").ToString();
                    ddlSubject.Items.FindByText(selectedSubject).Selected = true;
                }
            }
        }

        protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlClassSelected=(DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
            if (row != null)
            {
                if ((row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlSubjectGv = (DropDownList)row.FindControl("ddlSubjectGv");
                    DataTable dt = fn.Fetch("SELECT * FROM Subject where ClassId='" + ddlClassSelected.SelectedValue + "' ");
                    ddlSubject.DataSource = dt;
                    ddlSubject.DataTextField = "SubjectName";
                    ddlSubject.DataValueField = "SubjectId";
                    ddlSubject.DataBind();
                }
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetMarks();
        }
    }
}
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSyatem.Models.CommonFn;
using System.Dynamic;

namespace SchoolManagementSyatem.Teacher
{
    public partial class StudentAttendanceDetails : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetSubjects();
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
        private void GetSubjects()
        {
            // Fetch subjects from the database
            DataTable dataTable = fn.Fetch("SELECT * FROM Subject");

            // Bind the DataTable to the DropDownList
            ddlSubject.DataSource = dataTable;
            ddlSubject.DataTextField = "SubjectName";
            ddlSubject.DataValueField = "SubjectId";
            ddlSubject.DataBind();

            // Insert a default "Select Subject" item at the beginning
            ddlSubject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Subject", ""));

        }

        protected void btnCheckAttendance_Click(object sender, EventArgs e)
        {
            // Get the selected date from the txtMonth TextBox
            DateTime date;
            if (!DateTime.TryParse(txtMonth.Text, out date))
            {
                // Handle invalid date input
                // Optionally, you can display an error message or take other actions
                return;
            }

            // Check if a class is selected
            if (ddlClass.SelectedValue == "Select Class")
            {
                // Display a message indicating that a class must be selected
                lblMsg.Text = "Please select a class.";
                lblMsg.CssClass = "alert alert-danger";
                return;
            }

            // Fetch attendance records based on the selected class and subject
            DataTable dt;
            if (ddlSubject.SelectedValue == "Select Subject")
            {
                // Attendance records for all subjects in the selected class
                dt = fn.Fetch(@"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS [Sr.No], s.Name, sa.Status, sa.Date     
                       FROM StufentAttendence sa
                       INNER JOIN Student s ON s.RollNo = sa.RollNo 
                       WHERE sa.ClassId = '" + ddlClass.SelectedValue + "' AND sa.Date >= '" + date.ToString("yyyy-MM-01") + "' AND sa.Date < '" + date.AddMonths(1).ToString("yyyy-MM-01") + "'");
            }
            else
            {
                // Attendance records for a specific subject in the selected class
                dt = fn.Fetch(@"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS [Sr.No], s.Name, sa.Status, sa.Date     
                       FROM StufentAttendence sa
                       INNER JOIN Student s ON s.RollNo = sa.RollNo 
                       WHERE sa.ClassId = '" + ddlClass.SelectedValue + "' AND sa.SubjectId = '" + ddlSubject.SelectedValue + "' AND sa.Date >= '" + date.ToString("yyyy-MM-01") + "' AND sa.Date < '" + date.AddMonths(1).ToString("yyyy-MM-01") + "'");
            }

            // Bind the fetched data to the GridView
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
       

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
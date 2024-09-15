using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSyatem.Models.CommonFn;

namespace SchoolManagementSyatem
{
    public partial class StudentAttendanceUC : System.Web.UI.UserControl
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();

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

        protected void btnCheckAttendance_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DateTime date = Convert.ToDateTime(txtMonth.Text);

            if (ddlSubject.SelectedValue == "Select Subject")
            {
                dt = fn.Fetch(@"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS [Sr.No], s.Name, sa.Status, sa.Date     
                 FROM StufentAttendence sa
                 INNER JOIN Student s ON s.RollNo = sa.RollNo 
                 WHERE sa.ClassId = '" + ddlClass.SelectedValue + "' AND " +
                         "sa.RollNo = '" + txtRollNO.Text.Trim() + "' AND DATEPART(yy, sa.Date) = '" + date.Year + "' AND DATEPART(M, sa.Date) = '" + date.Month + "' AND sa.Status = 1");
            }
            else
            {
                dt = fn.Fetch(@"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS [Sr.No], s.Name, sa.Status, sa.Date     
                 FROM StufentAttendence sa
                 INNER JOIN Student s ON s.RollNo = sa.RollNo 
                 WHERE sa.ClassId = '" + ddlClass.SelectedValue + "' AND " +
                        "sa.RollNo = '" + txtRollNO.Text.Trim() + "' AND sa.SubjectId = '" + ddlSubject.SelectedValue + "' AND DATEPART(M, sa.Date) = '" + date.Year + "' AND DATEPART(M, sa.Date) = '" + date.Month + "' AND sa.Status = 1");
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}
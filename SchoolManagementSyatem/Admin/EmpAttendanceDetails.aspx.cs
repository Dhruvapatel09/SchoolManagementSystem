using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSyatem.Models.CommonFn;

namespace SchoolManagementSyatem.Admin
{
    public partial class EmpAttendanceDetails : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTeacher();
            }
        }
        private void GetTeacher()
        {
            DataTable dt = fn.Fetch("SELECT * FROM Teacher");
            ddlTeacher.DataSource = dt;
            ddlTeacher.DataTextField = "Name";
            ddlTeacher.DataValueField = "TeacherId";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, "Select Teacher");
        }
        protected void btnCheckAttendance_Click(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(txtMonth.Text);


            //DataTable dt = fn.Fetch(@"SELECT Row_Number() over(Order by (SELECT 1)) AS [Sr.No], t.Name,ta.status,ta.Date 
            //                         from TeacherAttendance ta
            //                         inner join Teacher t on t.TeacherId=ta.TeacherId 
            //                         WHERE DATEPART(yy, Date) = '" + date.Year + "'and" + " DATEPART(M, Date) = '" + date.Month +
            //                         "'and ta.Status=1 and ta.TeacherId='" + ddlTeacher.SelectedValue + "' ");
            DataTable dt = fn.Fetch(@"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS [Sr.No], t.Name, ta.Status, ta.Date     
                         FROM TeacherAttendence ta
                         INNER JOIN Teacher t ON t.TeacherId = ta.TeacherId 
                         WHERE DATEPART(yy, Date) = '" + date.Year + @"' 
                         AND DATEPART(M, Date) = '" + date.Month + @"' 
                         AND ta.Status = 1 
                         AND ta.TeacherId = '" + ddlTeacher.SelectedValue + "'");

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

    }
}
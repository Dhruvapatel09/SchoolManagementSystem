using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSyatem.Models.CommonFn;

namespace SchoolManagementSyatem.Admin
{
    public partial class TeacherSubject : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetTeacher();
                GetTeacherSubject();
            }
        }
        private void GetClass()
        {
            DataTable dt = fn.Fetch("SELECT * FROM Class");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");
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

        private void GetTeacherSubject()
        {
            DataTable dt = fn.Fetch(@"SELECT Row_Number() over(Order by (SELECT 1)) AS [Sr.No],ts.Id,ts.ClassId,c.ClassName,ts.SubjectId,s.SubjectName,ts.TeacherId,t.Name from TeacherSubject ts inner join Class c on ts.ClassId=c.ClassId inner join Subject s on ts.SubjectId=s.SubjectId inner join Teacher t on t.TeacherId=ts.TeacherId
");
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classId=ddlClass.SelectedValue;
            DataTable dt = fn.Fetch("SELECT * FROM Subject where ClassId='"+classId+"' ");
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
                // Get selected class, subject, and teacher
                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                string teacherId = ddlTeacher.SelectedValue;
                
                // Check if the assignment already exists
                bool assignmentExists = CheckIfAssignmentExists(classId, subjectId, teacherId);

                if (assignmentExists)
                {
                    // If assignment exists, display error message
                    lblMsg.Text = "Entered Teacher Subject already exists for this class and subject!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    // If assignment doesn't exist, proceed with inserting the new assignment
                    InsertAssignmentRecord(classId, subjectId, teacherId);
                    lblMsg.Text = "Inserted Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    ResetDropdowns();
                    GetTeacherSubject(); // Refresh teacher subject list
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        // Function to check if the assignment already exists
        private bool CheckIfAssignmentExists(string classId, string subjectId, string teacherId)
        {
            // Query the database to retrieve existing records
            DataTable dt = fn.Fetch("SELECT * FROM TeacherSubject WHERE ClassId='" + classId + "' AND SubjectId='" + subjectId + "' AND TeacherId='" + teacherId + "'");

            // If no records are found, return false indicating assignment doesn't exist
            return dt.Rows.Count > 0;
        }

        // Function to insert the new assignment record
        private void InsertAssignmentRecord(string classId, string subjectId, string teacherId)
        {
            // Insert the record into the database
            string query = "INSERT INTO TeacherSubject (ClassId, SubjectId, TeacherId) VALUES ('" + classId + "', '" + subjectId + "', '" + teacherId + "')";
            fn.Query(query);
        }

        // Function to reset dropdowns after successful insertion
        private void ResetDropdowns()
        {
            ddlClass.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetTeacherSubject();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTeacherSubject();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int teachersubjectId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from TeacherSubject where Id='" + teachersubjectId + "'");
                lblMsg.Text = "Teacher subject deleted Successfuly";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeacherSubject();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alret('" + ex.Message + "');</script>");

            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int teachersubjectId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGv")).SelectedValue;
                string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGv")).SelectedValue;

                string teacherId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlTeacherGv")).SelectedValue;

                fn.Query(@"UPDATE TeacherSubject SET ClassId='" + classId + "', SubjectId='" + subjectId + "', TeacherId='" + teacherId + "' WHERE Id='" + teachersubjectId + "'");
                lblMsg.Text = "Record updated Successfuly";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeacherSubject();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTeacherSubject();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void ddlClassGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlClassSelected = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
            if(row!=null)
            {
                if((row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlSubjectGv= (DropDownList)row.FindControl("ddlSubjectGv");
                    DataTable dt = fn.Fetch("select * from Subject where ClassId='" + ddlClassSelected.SelectedValue+"' ");
                    ddlSubjectGv.DataSource = dt;
                    ddlSubjectGv.DataTextField = "SubjectName";
                    ddlSubjectGv.DataValueField = "SubjectId";
                    ddlSubjectGv.DataBind();

                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType== DataControlRowType.DataRow)
            {
                if((e.Row.RowState & DataControlRowState.Edit)>0)
                {
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClassGv");
                    DropDownList ddlSubject = (DropDownList)e.Row.FindControl("ddlSubjectGv");
                        DataTable dt = fn.Fetch("select * from Subject where ClassId='" + ddlClass.SelectedValue + "' ");
                    ddlSubject.DataSource = dt;
                    ddlSubject.DataTextField = "SubjectName";
                    ddlSubject.DataValueField = "SubjectId";
                    ddlSubject.DataBind();
                    ddlSubject.Items.Insert(0, "Select Subject");
                    string teacherSubjectId = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                    DataTable dataTable = fn.Fetch(@"SELECT ts.Id,ts.ClassId,ts.SubjectId,s.SubjectName from TeacherSubject ts
                    inner join Subject s on ts.SubjectId=s.SubjectId where ts.Id='"+ teacherSubjectId + "'");
                    ddlSubject.SelectedValue = dataTable.Rows[0]["SubjectId"].ToString();
                }
            }
        }
    }
}
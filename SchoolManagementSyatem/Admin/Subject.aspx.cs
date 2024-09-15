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
    public partial class Subject : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetSubject();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlClass.SelectedItem != null)
                {
                    string classVal = ddlClass.SelectedItem.Text;
                    DataTable dt = fn.Fetch("SELECT * FROM Subject WHERE ClassId='" + ddlClass.SelectedItem.Value +
                  "' AND SubjectName='" + txtSubject.Text.Trim() + "'");

                    if (dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Subject Values('" + ddlClass.SelectedItem.Value + "','" + txtSubject.Text.Trim() + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted Successfully";
                        lblMsg.CssClass = "alert alert-success";
                        ddlClass.SelectedIndex = 0;
                        txtSubject.Text = string.Empty;
                        GetSubject();
                    }
                    else
                    {
                        lblMsg.Text = "Entered Subject already exists for <b>'" + classVal + "'</b>!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select a class.";
                    lblMsg.CssClass = "alert alert-warning";
                }
                GetSubject();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void GetSubject()
        {
            DataTable dt = fn.Fetch("SELECT Row_Number() over(Order by (SELECT 1)) AS [Sr.No],s.SubjectId,s.ClassId,c.ClassName,s.SubjectName from Subject s inner join Class c on c.ClassId=s.ClassId");
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetSubject();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetSubject();
        }

       

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetSubject();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
          {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int subjId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("DropDownList1")).SelectedValue;
                string subjName = (row.FindControl("TextBox1") as TextBox).Text;
                fn.Query("UPDATE Subject SET ClassId='" + classId + "', SubjectName='" + subjName + "' WHERE SubjectId='" + subjId + "'");
                lblMsg.Text = "Subject updated Successfully";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetSubject();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}
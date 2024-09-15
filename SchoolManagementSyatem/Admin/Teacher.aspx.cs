using System;
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
    public partial class Teacher : System.Web.UI.Page
    {
        Commonfnx fn=new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetTeachers();
            }
        }

        private void GetTeachers()
        {
            DataTable dt = fn.Fetch(@"Select ROW_NUMBER() OVER(ORDER BY(SELECT 1)) AS [Sr.No],[TeacherId],[Name],DOB, Gender,Mobile,Email,[Address] from Teacher ");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(ddlGender.SelectedValue!="0")
                {
                    string email=txtEmail.Text.Trim();
                    DataTable dt = fn.Fetch("Select * from Teacher where Email='" + email + "'");
                    if(dt.Rows.Count==0 )
                    {
                        string query="insert into Teacher values('"+txtName.Text.Trim()+"','"+txtDoB.Text.Trim()+"','"+ddlGender.SelectedValue+"','"+txtMobile.Text.Trim()+"','"+txtEmail.Text.Trim()+"','"+txtAddress.Text.Trim()+"')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted Successfully";
                        lblMsg.CssClass = "alert alert-success";
                        ddlGender.SelectedIndex = 0;
                        txtName.Text = string.Empty;
                        txtDoB.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        GetTeachers();
                    }
                    else
                    {
                        lblMsg.Text = "Entered  <b>'" + email + "'</b> already exists!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Text = "Gender is required!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch(Exception ex) 
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetTeachers();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTeachers() ;
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTeachers();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from Teacher where TeacherId='" + teacherId + "'");
                lblMsg.Text = "Teacher deleted Successfuly";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeachers();
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
                int teacherId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                string name = (row.FindControl("txtName") as TextBox).Text;
                string mobile = (row.FindControl("txtMobile") as TextBox).Text;
               
                string address = (row.FindControl("txtAddress") as TextBox).Text;

                // Validation: Ensure name contains only characters and spaces
                if (!Regex.IsMatch(name, "^[a-zA-Z ]+$"))
                {
                    throw new Exception("Name should contain only characters and spaces.");
                }

                // Validation: Ensure mobile number has 10 digits only
                if (!Regex.IsMatch(mobile, @"^\d{10}$"))
                {
                    throw new Exception("Mobile number should be 10 digits.");
                }

                // Update query
                fn.Query("UPDATE Teacher SET Name='" + name.Trim() + "', Mobile='" + mobile.Trim() + "', Address='" + address.Trim() + "' WHERE TeacherId='" + teacherId + "'");

                lblMsg.Text = "Teacher updated Successfully";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeachers();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}
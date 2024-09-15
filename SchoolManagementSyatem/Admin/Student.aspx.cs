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
    public partial class Student : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetStudents();
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
       
        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGender.SelectedValue != "0")
                {
                    string roll = txtRollNum.Text.Trim();
                    DataTable dt = fn.Fetch("SELECT * FROM Student WHERE ClassId='" + ddlClass.SelectedValue + "' AND RollNo='" + roll + "'");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Student (Name, DOB, Gender, Mobile, RollNo, Address, ClassId, ClassName) VALUES " +
                                       "('" + txtName.Text.Trim() + "', '" + txtDOB.Text.Trim() + "', '" + ddlGender.SelectedValue + "', " +
                                       "'" + txtContact.Text.Trim() + "', '" + txtRollNum.Text.Trim() + "', '" + txtAddress.Text.Trim() + "', " +
                                       "'" + ddlClass.SelectedValue + "', (SELECT ClassName FROM Class WHERE ClassId='" + ddlClass.SelectedValue + "'))";
                        fn.Query(query);
                        lblMsg.Text = "Inserted Successfully";
                        lblMsg.CssClass = "alert alert-success";
                        ddlGender.SelectedIndex = 0;
                        txtName.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        txtContact.Text = string.Empty;
                        txtRollNum.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        ddlClass.SelectedIndex = 0;
                        GetStudents();
                    }
                    else
                    {
                        lblMsg.Text = "Entered Roll no.<b>'" + roll + "'</b> already exists for selected Class!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Text = "Gender is required!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void GetStudents()
        {
            DataTable dt = fn.Fetch(@"SELECT ROW_NUMBER() OVER(ORDER BY(SELECT 1)) AS [Sr.No], 
                                      s.StudentId, 
                                      s.[Name], 
                                      s.DOB, 
                                      s.Gender, 
                                      s.Mobile, 
                                      s.RollNo, 
                                      s.[Address], 
                                      c.ClassId, 
                                      c.ClassName 
                               FROM Student s 
                               INNER JOIN Class c ON c.ClassId = s.ClassId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex=e.NewEditIndex;
            GetStudents();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int studentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                string name = (row.FindControl("txtName") as TextBox).Text;
                string rollNo = (row.FindControl("txtRollNum") as TextBox).Text;
                string address = (row.FindControl("txtAddress") as TextBox).Text;
                

                // Validation: Ensure name contains only characters and spaces
                //if (!Regex.IsMatch(name, "^[a-zA-Z ]+$"))
                //{
                //    throw new Exception("Name should contain only characters and spaces.");
                //}

                // Update query
                fn.Query(@"UPDATE Student SET Name='" + name.Trim() + "', Address='" + address.Trim() + "', Rollno='" + rollNo.Trim() + "' WHERE StudentId='" + studentId + "'");


                lblMsg.Text = "Student updated Successfully";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetStudents();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex= -1;
            GetStudents();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex=e.NewPageIndex;
            GetStudents();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlClass = e.Row.FindControl("ddlClass") as DropDownList;
                    DataTable dt = fn.Fetch("select * from Class");
                    ddlClass.DataSource = dt;
                    ddlClass.DataTextField = "ClassName";
                    ddlClass.DataValueField = "ClassId";
                    ddlClass.DataBind();
                    ddlClass.Items.Insert(0, "Select Class");

                    // Set the selected value based on the data item
                    string selectedClass = DataBinder.Eval(e.Row.DataItem, "ClassName").ToString();
                   ddlClass.Items.FindByText(selectedClass).Selected = true;
                
            }
        }



    }
}
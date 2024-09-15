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
    public partial class Login : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnTeacherLogin_Click(object sender, EventArgs e)
        {

            string email = inputEmail.Value.Trim();
            string password = inputPassword.Value.Trim();
            DataTable dt = fn.Fetch("select * from TeacherRegistration where Email='" + email + "' and Password='" + password + "' ");

            if (dt.Rows.Count > 0)
            {
                Session["teacher"] = email;
                Response.Redirect("Teacher/TeacherHome.aspx");
            }
            else
            {
                lblMsg.Text = "Login Failed!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }
    }
}
using System;

namespace SchoolManagementSyatem
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AdminRegistration_Click(object sender, EventArgs e)
        {
            // Redirect to Admin signup page
            Response.Redirect("AdminRegistration.aspx");
        }

        protected void TeacherRegistration_Click(object sender, EventArgs e)
        {
            // Redirect to Teacher signup page
            Response.Redirect("TeacherRegistration.aspx");
        }

        protected void AdminLogin_Click(object sender, EventArgs e)
        {
            // Redirect to New User registration page
            Response.Redirect("AdminLogin.aspx");
        }
        protected void TeacherLogin_Click(object sender, EventArgs e)
        {
            // Redirect to New User registration page
            Response.Redirect("TeacherLogin.aspx");
        }
    }
}

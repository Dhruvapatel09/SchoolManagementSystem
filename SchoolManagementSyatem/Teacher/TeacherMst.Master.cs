using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagementSyatem.Teacher
{
    public partial class TeacherMst : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            // Perform logout actions if needed
            // Redirect to the homepage
            Response.Redirect("~/HomePage.aspx");
        }
    }
}
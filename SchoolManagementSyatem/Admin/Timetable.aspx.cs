using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagementSyatem.Admin
{
    public partial class Timetable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTimetable();
            }
        }

        private void BindTimetable()
        {
            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString;

            // SQL query to select timetable data
            string query = "SELECT DaysOfWeek.DayName, TimeSlots.StartTime, TimeSlots.EndTime, Class.ClassName, Teacher.Name, Subject.SubjectName " +
                "FROM Timetable " +
                "INNER JOIN DaysOfWeek ON Timetable.DayOfWeekId = DaysOfWeek.DayOfWeekId " +
                "INNER JOIN TimeSlots ON Timetable.TimeSlotId = TimeSlots.TimeSlotId " +
                "INNER JOIN Class ON Timetable.ClassId = Class.ClassId " +
                "INNER JOIN Teacher ON Timetable.TeacherId = Teacher.TeacherId " +
                "INNER JOIN Subject ON Timetable.SubjectId = Subject.SubjectId";


            // Create connection and command objects
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open connection
                    connection.Open();

                    // Create a data adapter to fill a DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable timetableData = new DataTable();

                    // Fill the DataTable with data from the query
                    adapter.Fill(timetableData);

                    // Bind the DataTable to the GridView
                    GridViewTimetable.DataSource = timetableData;
                    GridViewTimetable.DataBind();
                }
            }
        }
    
}
}
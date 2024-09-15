using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace SchoolManagementSyatem.Admin
{
    public partial class AssignTeacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate dropdowns on first load
                PopulateDropdowns();
            }
        }

        //protected void btnAssign_Click(object sender, EventArgs e)
        //{
        //    int classId = Convert.ToInt32(ddlClass.SelectedValue);
        //    int subjectId = Convert.ToInt32(ddlSubject.SelectedValue);
        //    int dayOfWeekId = Convert.ToInt32(ddlDaysOfWeek.SelectedValue);
        //    int timeSlotId = Convert.ToInt32(ddlTimeSlots.SelectedValue);
        //    int roomId = Convert.ToInt32(txtRoomId.Text);
        //    int teacherId = GetTeacherId(); // Assuming you have a method to get the teacher's ID

        //    if (AssignTeacherToTimetable(classId, subjectId, dayOfWeekId, timeSlotId, teacherId,roomId))
        //    {
        //        lblMessage.Text = "Teacher assigned successfully.";
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Failed to assign teacher.";
        //    }
        //}

        private void PopulateDropdowns()
        {
            // Populate dropdowns with data from the database
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Populate ddlClasses
                SqlDataAdapter adapterClasses = new SqlDataAdapter("SELECT ClassId, ClassName FROM Class", connection);
                DataTable dtClasses = new DataTable();
                adapterClasses.Fill(dtClasses);
                ddlClass.DataSource = dtClasses;
                ddlClass.DataTextField = "ClassName";
                ddlClass.DataValueField = "ClassId";
                ddlClass.DataBind();

                // Populate ddlSubjects
                SqlDataAdapter adapterSubjects = new SqlDataAdapter("SELECT SubjectId, SubjectName FROM Subject", connection);
                DataTable dtSubjects = new DataTable();
                adapterSubjects.Fill(dtSubjects);
                ddlSubject.DataSource = dtSubjects;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();

                // Populate ddlDaysOfWeek
                SqlDataAdapter adapterDaysOfWeek = new SqlDataAdapter("SELECT DayOfWeekId, DayName FROM DaysOfWeek", connection);
                DataTable dtDaysOfWeek = new DataTable();
                adapterDaysOfWeek.Fill(dtDaysOfWeek);
                ddlDaysOfWeek.DataSource = dtDaysOfWeek;
                ddlDaysOfWeek.DataTextField = "DayName";
                ddlDaysOfWeek.DataValueField = "DayOfWeekId";
                ddlDaysOfWeek.DataBind();

                // Populate ddlTimeSlots
                SqlDataAdapter adapterTimeSlots = new SqlDataAdapter("SELECT TimeSlotId, CONCAT(StartTime, ' - ', EndTime) AS TimeSlot FROM TimeSlots", connection);
                DataTable dtTimeSlots = new DataTable();
                adapterTimeSlots.Fill(dtTimeSlots);
                ddlTimeSlots.DataSource = dtTimeSlots;
                ddlTimeSlots.DataTextField = "TimeSlot";
                ddlTimeSlots.DataValueField = "TimeSlotId";
                ddlTimeSlots.DataBind();
            }
        }

        private bool AssignTeacherToTimetable(int classId, int subjectId, int dayOfWeekId, int timeSlotId, int teacherId, int roomId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Timetable (ClassId, SubjectId, DayOfWeekId, TimeSlotId, TeacherId, RoomId) VALUES (@ClassId, @SubjectId, @DayOfWeekId, @TimeSlotId, @TeacherId, @RoomId);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassId", classId);
                        command.Parameters.AddWithValue("@SubjectId", subjectId);
                        command.Parameters.AddWithValue("@DayOfWeekId", dayOfWeekId);
                        command.Parameters.AddWithValue("@TimeSlotId", timeSlotId);
                        command.Parameters.AddWithValue("@TeacherId", teacherId);
                        command.Parameters.AddWithValue("@RoomId", roomId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        
    }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            // Get the selected values from dropdown lists
            int classId = Convert.ToInt32(ddlClass.SelectedValue);
            int subjectId = Convert.ToInt32(ddlSubject.SelectedValue);
            int dayOfWeekId = Convert.ToInt32(ddlDaysOfWeek.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlots.SelectedValue);
            int teacherId = GetTeacherId(); // Assuming you have a method to get the teacher's ID
            int roomId = Convert.ToInt32(txtRoomId.Text);

            // Insert data into the Timetable table
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString;
            string query = "INSERT INTO Timetable (TimeSlotId, DayOfWeekId, ClassId, RoomId, TeacherId, SubjectId) VALUES (@TimeSlotId, @DayOfWeekId, @ClassId, @RoomId, @TeacherId, @SubjectId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TimeSlotId", timeSlotId);
                    command.Parameters.AddWithValue("@DayOfWeekId", dayOfWeekId);
                    command.Parameters.AddWithValue("@ClassId", classId);
                    // You need to set the RoomId, TeacherId, and SubjectId based on your logic
                    command.Parameters.AddWithValue("@RoomId", roomId);
                    command.Parameters.AddWithValue("@TeacherId", teacherId);
                    command.Parameters.AddWithValue("@SubjectId", subjectId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Timetable entry successfully added
                        lblMessage.Text = "Timetable entry added successfully.";
                    }
                    else
                    {
                        // Failed to add timetable entry
                        lblMessage.Text = "Failed to add timetable entry.";
                    }
                }
            }
        }

        protected void btnViewTimetable_Click(object sender, EventArgs e)
        {
            // Get the selected class ID
            int classId = Convert.ToInt32(ddlClass.SelectedValue);

            // Call a method to retrieve and display the timetable for the selected class
            DisplayTimetable(classId);
        }
        private void DisplayTimetable(int classId)
        {
            // SQL query to retrieve timetable data with names instead of IDs
            string query = @"
        SELECT 
            d.DayName AS DayOfWeek, 
            CONCAT(ts.StartTime, ' - ', ts.EndTime) AS TimeSlot,
            s.SubjectName AS Subject,
            t.Name AS Teacher
        FROM 
            Timetable tt
            INNER JOIN DaysOfWeek d ON tt.DayOfWeekId = d.DayOfWeekId
            INNER JOIN TimeSlots ts ON tt.TimeSlotId = ts.TimeSlotId
            INNER JOIN Subject s ON tt.SubjectId = s.SubjectId
            INNER JOIN Teacher t ON tt.TeacherId = t.TeacherId
        WHERE 
            tt.ClassId = @ClassId
    ";

            // Set up SQL connection and command
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@ClassId", classId);

                    // Open connection and execute query
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Create a new table to display the timetable data
                    Table timetableTable = new Table();
                    timetableTable.CssClass = "timetable-table";

                    // Create a header row for the table
                    TableRow headerRow = new TableRow();
                    headerRow.CssClass = "timetable_simple_rowheader";

                    TableCell dayHeaderCell = new TableCell();
                    dayHeaderCell.Text = "Day";
                    dayHeaderCell.CssClass = "timetable_simple_rowheader_inner";
                    headerRow.Cells.Add(dayHeaderCell);

                    TableCell timeHeaderCell = new TableCell();
                    timeHeaderCell.Text = "Time";
                    timeHeaderCell.CssClass = "timetable_simple_rowheader_inner";
                    headerRow.Cells.Add(timeHeaderCell);

                    TableCell subjectHeaderCell = new TableCell();
                    subjectHeaderCell.Text = "Subject";
                    subjectHeaderCell.CssClass = "timetable_simple_rowheader_inner";
                    headerRow.Cells.Add(subjectHeaderCell);

                    TableCell teacherHeaderCell = new TableCell();
                    teacherHeaderCell.Text = "Teacher";
                    teacherHeaderCell.CssClass = "timetable_simple_rowheader_inner";
                    headerRow.Cells.Add(teacherHeaderCell);

                    timetableTable.Rows.Add(headerRow);

                    // Loop through the result set and append timetable data to the table
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Create a new row for each timetable entry
                            TableRow dataRow = new TableRow();

                            TableCell dayDataCell = new TableCell();
                            dayDataCell.Text = reader["DayOfWeek"].ToString();
                            dataRow.Cells.Add(dayDataCell);

                            TableCell timeDataCell = new TableCell();
                            timeDataCell.Text = reader["TimeSlot"].ToString();
                            dataRow.Cells.Add(timeDataCell);

                            TableCell subjectDataCell = new TableCell();
                            subjectDataCell.Text = reader["Subject"].ToString();
                            dataRow.Cells.Add(subjectDataCell);

                            TableCell teacherDataCell = new TableCell();
                            teacherDataCell.Text = reader["Teacher"].ToString();
                            dataRow.Cells.Add(teacherDataCell);

                            timetableTable.Rows.Add(dataRow);
                        }
                    }
                    else
                    {
                        // Add a row to indicate that there is no timetable data for the selected class
                        TableRow noDataRow = new TableRow();
                        noDataRow.CssClass = "timetable_simple_row";

                        TableCell noDataCell = new TableCell();
                        noDataCell.Text = "No timetable found for this class.";
                        noDataCell.ColumnSpan = 4;
                        noDataRow.Cells.Add(noDataCell);

                        timetableTable.Rows.Add(noDataRow);
                    }

                    // Close the data reader
                    reader.Close();
                }
            }

            // Add the table to the page
            timetablePlaceholder.Controls.Add(timetableTable);
        }

        // Method to get the teacher's ID (you need to implement this)
        private int GetTeacherId()
        {
            //  
            // Example: return (int)Session["TeacherId"];
            return 1; // Placeholder value, replace with actual logic
        }
    }
}
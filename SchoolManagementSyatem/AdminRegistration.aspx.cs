using System;
using System.Data;
using System.Data.SqlClient;
using SchoolManagementSyatem.Models;
using static SchoolManagementSyatem.Models.CommonFn;

namespace SchoolManagementSyatem
{
    public partial class AdminRegistration : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Value.Trim();
            string email = txtEmail.Value.Trim();
            string password = txtPassword.Value.Trim();
            string confirmPassword = txtConfirmPassword.Value.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "All fields are required";
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text = "Passwords do not match";
                return;
            }

            // Check if email already exists
            if (IsEmailExists(email))
            {
                lblMessage.Text = "Email already registered";
                return;
            }

            // Save admin's information to the database
            try
            {
                // Assuming you have a connection string named "ConnectionString" in your web.config file
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString;


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Example query to insert the admin's information into the Admins table
                    string query = "INSERT INTO AdminRegistration (Name, Email, Password, PasswordConfirmed) VALUES (@Name, @Email, @Password, @PasswordConfirmed)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@PasswordConfirmed", confirmPassword);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }
                }

                // Redirect the admin to a confirmation page or login page
                Response.Redirect("AdminLogin.aspx");
            }
            catch (Exception ex)
            {
                // Handle the exception
                // You can display an error message or log the exception
                lblMessage.Text = "An error occurred while registering";
            }
        }

        private bool IsEmailExists(string email)
        {
            // Check if email already exists in the database
            // Implement your logic to query the database and check if the email exists
            // Return true if email exists, false otherwise

            // Example pseudo code:
            // SqlConnection connection = new SqlConnection(connectionString);
            // SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Admins WHERE Email = @Email", connection);
            // command.Parameters.AddWithValue("@Email", email);
            // int count = (int)command.ExecuteScalar();
            // return count > 0;

            // Replace this with your actual implementation
            return false;
        }
    }
}

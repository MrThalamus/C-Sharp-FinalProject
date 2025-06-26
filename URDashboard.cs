using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;


namespace FinalProject
{
    public partial class URDashboard : Form
    {
        string urid;
        public URDashboard()
        {
            InitializeComponent();
            

        }
        public URDashboard(string id)
        {
            InitializeComponent();
            this.urid = id;
            panel3.Visible = false;
            panel4.Visible = false;
            button6.Visible = false;
            panel11.Visible = false;
            panel7.Visible = false;
            panel6.Visible = false;

            panel6.Height = button4.Height;
            panel6.Location = new Point(panel6.Location.X, button4.Location.Y);
            panel6.Visible = true;

            this.Location = new Point(0,0);
            this.Size = new Size(902, 506);
            panel5.Location = new Point(154, 62);
            panel5.Size = new Size(734, 404);
            panel5.Visible = true;
            panel3.Size=panel5.Size;
            panel3.Location = new Point(154, 62);
            panel4.Size=panel5.Size;
            panel4.Location = new Point(154, 62);
            panel7.Size=panel5.Size;
            panel7.Location = new Point(154, 62);
            //panel6.Size=panel5.Size;
            
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";

            // Query to fetch all details for the given ID
            string query3 = "SELECT Name FROM URDetails WHERE ID = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    // Add the ID parameter
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        connection.Open();

                        // Execute the query
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the details into respective controls or variables
                                linkLabel1.Text = reader["Name"].ToString();
                            }
                            else
                            {
                                // Handle the case where no record is found for the given ID
                                MessageBox.Show("No record found for the provided ID.", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that may occur
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

            panel8.Size = new Size(180, 3);
            panel9.Size = new Size(180, 3);
            panel10.Size = new Size(180, 3);

            panel12.Size = new Size(180, 3);
            panel13.Size = new Size(180, 3);
            panel14.Size = new Size(180, 3);
            panel15.Size = new Size(180, 3);
            panel16.Size = new Size(180, 3);
            panel17.Size = new Size(180, 3);
            panel18.Size = new Size(180, 3);
            panel19.Size = new Size(180, 3);


            panel8.Size = new Size(180, 3);
            panel4.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            button6.Visible = false;

            panel6.Height = button1.Height;
            panel6.Location = new Point(panel6.Location.X, button1.Location.Y);
            panel6.Visible = true;

            //panel3.Location = new Point(200, 67);
            //panel3.Size = new Size(685, 410);
            panel3.Visible = true;


        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;

            panel5.Visible = true;
        }
        private void LoadDataIntoGrid(string urid)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //COLUMN RESIZE IN GRIDVIEW
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // Automatically adjust row height based on content
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // Optional: Allow column resizing for better UX
            dataGridView1.AllowUserToResizeColumns = true;

            //LoadDataIntoGrid("20");
            panel3.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            button6.Visible = false;

            panel6.Height = button2.Height;
            panel6.Location = new Point(panel6.Location.X, button2.Location.Y);
            panel6.Visible = true;

            //panel4.Location = new Point(200, 67);
            //panel4.Size = new Size(685, 410);
            panel4.Visible = true;
            //null search box
            textBox12.Text = string.Empty;

            // CONNECTION STRING
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            // Create the SQL query with a WHERE clause
            string query = "SELECT * FROM URDashboard WHERE URID = @URID";

            // Establish the connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create the SqlCommand
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter to the command
                    command.Parameters.AddWithValue("@URID", urid);

                    // Use SqlDataAdapter to fetch data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    // Fill data into a DataTable
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind DataTable to DataGridView
                    dataGridView1.DataSource = dataTable;

                    // (Optional) Adjust column headers
                    dataGridView1.AutoResizeColumns();
                }
            }


        }
        private void nullAddInfo(bool value)
        {
            // Reset text fields
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;

            // Reset numeric fields to "0"
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;

            // Reset radio buttons (unselect all)
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            // Clear ComboBox selection (if it contains items)
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = -1;  // No item selected
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            //DATABASE CONNECTION
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            //VARIABLE
            string courseName = textBox1.Text.Trim();
            string universityName = textBox2.Text.Trim();
            string country = textBox3.Text.Trim();
            string courseCode = textBox4.Text.Trim();
            string degreeType = comboBox1.Text.Trim();
            string courseDuration = textBox5.Text.Trim();
            string studyMode = radioButton1.Checked ? radioButton1.Text :
            radioButton2.Checked ? radioButton2.Text :
                radioButton3.Checked ? radioButton3.Text : null;
            string sat = textBox6.Text.Trim();
            string ielts = textBox7.Text.Trim();
            string gre = textBox8.Text.Trim();
            string tutionFee = textBox9.Text.Trim();
            string maxScholarship = textBox10.Text.Trim();
            string intake = textBox11.Text.Trim();
            String applicationDeadline = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //string URID = "20";


            //HANDLE EMPTY SUBMISSION
            if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(courseDuration) ||
                string.IsNullOrWhiteSpace(intake) || string.IsNullOrWhiteSpace(applicationDeadline) ||
                string.IsNullOrWhiteSpace(tutionFee) || string.IsNullOrWhiteSpace(studyMode) ||
                string.IsNullOrWhiteSpace(courseCode) || string.IsNullOrWhiteSpace(degreeType) ||
                string.IsNullOrWhiteSpace(ielts) || string.IsNullOrWhiteSpace(gre) ||
                string.IsNullOrWhiteSpace(sat) || string.IsNullOrWhiteSpace(maxScholarship) ||
                string.IsNullOrWhiteSpace(universityName) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(urid))
            {
                MessageBox.Show("All fields must be filled out!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //nullAddInfo(true);
                return;
            }

            //HANDLE INVALID INPUT

            //QUERY
            string query = "INSERT INTO [FINAL(DB)].[dbo].[URDashboard] (CourseName, UniversityName, Country, " +
                "CourseCode, DegreeType, CourseDuration, StudyMode, SAT, IELTS, GRE, TutionFee, MaxScholarship," +
                " Intake, ApplicationDeadline, URID) VALUES (@CourseName, @UniversityName, @Country, @CourseCode, @DegreeType," +
                " @CourseDuration, @StudyMode, @SAT, @IELTS, @GRE, @TutionFee, @MaxScholarship, @Intake, @ApplicationDeadline, @URID)";

            //COMMAND
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //OPEN CONNECTION
                    connection.Open();
                    //PARAMETERS
                    command.Parameters.AddWithValue("@CourseName", courseName);
                    command.Parameters.AddWithValue("@UniversityName", universityName);
                    command.Parameters.AddWithValue("@Country", country);
                    command.Parameters.AddWithValue("@CourseCode", courseCode);
                    command.Parameters.Add("@DegreeType", SqlDbType.VarChar).Value = degreeType;
                    command.Parameters.AddWithValue("@CourseDuration", courseDuration);
                    command.Parameters.AddWithValue("@StudyMode", studyMode);
                    command.Parameters.AddWithValue("@SAT", sat);
                    command.Parameters.AddWithValue("@IELTS", ielts);
                    command.Parameters.AddWithValue("@GRE", gre);
                    command.Parameters.AddWithValue("@TutionFee", tutionFee);
                    command.Parameters.AddWithValue("@MaxScholarship", maxScholarship);
                    command.Parameters.AddWithValue("@Intake", intake);
                    command.Parameters.Add("@ApplicationDeadline", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
                    command.Parameters.AddWithValue("@URID", urid);

                    //EXECUTE
                    string queryCheck = "SELECT COUNT(*) FROM URDashboard WHERE CourseCode = @CourseCode";

                    SqlCommand checkCommand = new SqlCommand(queryCheck, connection);
                    checkCommand.Parameters.AddWithValue("@CourseCode", courseCode);


                    int exists = (int)checkCommand.ExecuteScalar();
                    //DUPLICATE CHECK
                    if (exists > 0)
                    {
                        MessageBox.Show("A record with the same CourseCode already exists." +
                            " Please use a unique Course code or update the course.", "Duplicate Key Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nullAddInfo(true);

                        return;
                    }

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to add the new course details?",
                                      "Confirmation",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

                        // Check the user's response
                        if (result == DialogResult.Yes)
                        {
                            // Show success message if the user chooses "Yes"
                            MessageBox.Show("New course details added successfully!",
                                            "Successful",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Show unsuccessful message if the user chooses "No"
                            MessageBox.Show("The new course details were not added.",
                                            "Unsuccessful",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }

                        nullAddInfo(true);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        nullAddInfo(true);
                    }

                    //CLOSE CONNECTION
                    connection.Close();
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel5.Visible = true;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button7_Click(object sender, EventArgs e)
        {
            string courseCode = textBox12.Text.Trim();
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            string query = "SELECT * FROM URDashboard WHERE CourseCode = @CourseCode";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //pass query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter to the command
                    command.Parameters.AddWithValue("@CourseCode", courseCode);

                    // Use SqlDataAdapter to fetch data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    // Fill data into a DataTable
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind DataTable to DataGridView
                    dataGridView1.DataSource = dataTable;

                    // (Optional) Adjust column headers
                    dataGridView1.AutoResizeColumns();
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //NewLogin login = new NewLogin();
            //this.Hide();
            //login.Show();
        }

        private void UpdateDatabase()
        {
            // Validate courseCode, if needed
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Prepare the data adapter
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM URDashboard", connection);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                    // Ensure the DataGridView is bound to a DataTable
                    if (dataGridView1.DataSource is DataTable dataTable)
                    {
                        if (dataTable.GetChanges() != null)
                        {
                            // Update the database
                            adapter.Update(dataTable);
                            MessageBox.Show("Data updated successfully!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No changes to update!","Return",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Data source is not a valid DataTable.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected Error: {ex.Message}");
                }
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {

            UpdateDatabase();
        }
        private void DeleteSelectedRow()
        {
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Prepare the data adapter
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM URDashboard", connection);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                    // Ensure the DataGridView is bound to a DataTable
                    if (dataGridView1.DataSource is DataTable dataTable)
                    {
                        // Check if a row is selected
                        if (dataGridView1.SelectedRows.Count > 0)
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?",
                            "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                                {
                                    if (!selectedRow.IsNewRow) // Prevent deleting the new row placeholder
                                    {
                                        dataGridView1.Rows.Remove(selectedRow); // Remove from DataGridView
                                    }
                                }

                                // Update the database
                                adapter.Update(dataTable);
                                MessageBox.Show("Selected row(s) deleted successfully!","Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Row deletion canceled.","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No row selected. Please select a row to delete!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data source is not a valid DataTable!","Invalid",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected Error: {ex.Message}");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DeleteSelectedRow();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            button6.Visible = false;


            panel6.Height = button4.Height;
            panel6.Location = new Point(panel6.Location.X, button4.Location.Y);
            panel6.Visible = true;

            //panel5.Location = new Point(200, 67);
            //panel5.Size = new Size(685, 410);
            panel5.Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

            button6.Visible = true;


            panel6.Height = button3.Height;
            panel6.Location = new Point(panel6.Location.X, button3.Location.Y);
            panel6.Visible = true;

            //panel7.Location = new Point(200, 67);
            //panel7.Size = new Size(685, 410);
            panel7.Visible = true;

            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            // SQL Query
            string query = @"
        SELECT
            ApplicationStatus.ApplicationStatus,
            ApplicationStatus.ID,
            ApplicationStatus.CourseName,
            ApplicationStatus.CourseCode,
            ApplicationStatus.UniversityName,
            ApplicationStatus.Country,
            ApplicationStatus.CourseDuration,
            ApplicationStatus.DegreeType,
            ApplicationStatus.IELTS,
            ApplicationStatus.GRE,
            ApplicationStatus.SAT,
            ApplicationStatus.TutionFee,
            ApplicationStatus.MaxScholarship,
            ApplicationStatus.Intake,
            ApplicationStatus.ApplicationDeadline,
            ApplicationStatus.StudyMode
            
        FROM 
            ApplicationStatus
        INNER JOIN 
            URDashboard ON ApplicationStatus.CourseCode = URDashboard.CourseCode
        WHERE 
            URDashboard.URID = @URID;";

            // Execute the query and fill the DataGridView
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter for URID
                    cmd.Parameters.AddWithValue("@URID", urid);

                    // Open connection
                    conn.Open();

                    // Data Adapter and DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind DataTable to DataGridView
                        dataGridView2.DataSource = dataTable;
                    }
                }
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void URDashborad_Load(object sender, EventArgs e)
        {

        }

        //application status
        private void UpdateDatabaseApplication()
        {
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //MessageBox.Show("Connection opened successfully."); // Debugging line

                    // Get the selected row from DataGridView
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        // Get the DataGridView row and the corresponding DataRow from the DataTable
                        int rowIndex = dataGridView2.SelectedRows[0].Index;
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                        // Get the new status value from the DataGridView cell (ApplicationStatus column)
                        string newStatus = selectedRow.Cells["ApplicationStatus"].Value.ToString(); // Assuming "ApplicationStatus" is the column name

                        // Get the user ID and course identifier (CourseID or CourseName)
                        int userId = Convert.ToInt32(selectedRow.Cells["ID"].Value); // Assuming "ID" is the column name
                        string courseCode = selectedRow.Cells["CourseCode"].Value.ToString(); // Assuming "CourseCode" is the column name

                        // Debugging: Display the values
                        MessageBox.Show($"Updating Status for User ID: {userId}, Course: {courseCode}, New Status: {newStatus}");

                        // Manually create the UPDATE SQL command to update the status for the specific course and user
                        string updateQuery = "UPDATE ApplicationStatus SET ApplicationStatus = @ApplicationStatus WHERE ID = @ID AND CourseCode = @CourseCode";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            // Define the parameters for the query
                            command.Parameters.AddWithValue("@ApplicationStatus", "Accepted");
                            command.Parameters.AddWithValue("@ID", userId); // Update only the specific user with the given ID
                            command.Parameters.AddWithValue("@CourseCode", courseCode); // Ensure the course is also matched for the update

                            // Execute the command
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Status updated successfully for the selected user and course!","Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No rows were updated. Ensure the user and course exist.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a row to update!","Request",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected Error: {ex.Message}");
                }
            }



        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            UpdateDatabaseApplication();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
           //panel7.Visible = true;
            int ID = int.Parse(urid);
            



            if (!int.TryParse(textBox13.Text, out ID))
            {
                panel11.Visible = false; 
                MessageBox.Show("Please enter a valid numeric ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT ID, Name, Nationality, Gender, Email, Age FROM [dbo].[StudentDetails] WHERE ID = @ID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Only show panel11 if the ID is found
                                while (reader.Read())
                                {
                                    label29.Text = reader["ID"].ToString();
                                    label20.Text = reader["Name"].ToString();
                                    label21.Text = reader["Nationality"].ToString();
                                    label23.Text = reader["Gender"].ToString();
                                    label25.Text = reader["Email"].ToString();
                                    label27.Text = reader["Age"].ToString();
                                }

                                panel7.Visible = true;
                                panel11.Size = new Size(430, 403);
                                panel11.Location = new Point(472, 59);
                                MessageBox.Show("Data found for the given ID.", "Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                panel11.Visible = true; // Only set visible here
                                textBox13.Text = "";
                            }
                            else
                            {
                                panel11.Visible = false;
                                MessageBox.Show("No data found for the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox13.Text = "";

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel11.Visible = false; // Ensure panel is hidden in case of an error
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel11.Visible = false;
            panel7.Visible = true;  
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //MessageBox.Show("Connection opened successfully."); // Debugging line

                    // Get the selected row from DataGridView
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        // Get the DataGridView row and the corresponding DataRow from the DataTable
                        int rowIndex = dataGridView2.SelectedRows[0].Index;
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                        // Get the new status value from the DataGridView cell (ApplicationStatus column)
                        string newStatus = selectedRow.Cells["ApplicationStatus"].Value.ToString(); // Assuming "ApplicationStatus" is the column name

                        // Get the user ID and course identifier (CourseID or CourseName)
                        int userId = Convert.ToInt32(selectedRow.Cells["ID"].Value); // Assuming "ID" is the column name
                        string courseCode = selectedRow.Cells["CourseCode"].Value.ToString(); // Assuming "CourseCode" is the column name

                        // Debugging: Display the values
                        MessageBox.Show($"Updating Status for User ID: {userId}, Course: {courseCode}, New Status: {newStatus}");

                        // Manually create the UPDATE SQL command to update the status for the specific course and user
                        string updateQuery = "UPDATE ApplicationStatus SET ApplicationStatus = @ApplicationStatus WHERE ID = @ID AND CourseCode = @CourseCode";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            // Define the parameters for the query
                            command.Parameters.AddWithValue("@ApplicationStatus", "Rejected");
                            command.Parameters.AddWithValue("@ID", userId); // Update only the specific user with the given ID
                            command.Parameters.AddWithValue("@CourseCode", courseCode); // Ensure the course is also matched for the update

                            // Execute the command
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Status updated successfully for the selected user and course!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No rows were updated. Ensure the user and course exist.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a row to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected Error: {ex.Message}");
                }
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            
        }



        private void button13_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

            button6.Visible = true;


            panel6.Height = button3.Height;
            panel6.Location = new Point(panel6.Location.X, button3.Location.Y);
            panel6.Visible = true;

            //panel7.Location = new Point(200, 67);
            //panel7.Size = new Size(685, 410);
            panel7.Visible = true;

            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            // SQL Query
            string query = @"
        SELECT
            ApplicationStatus.ApplicationStatus,
            ApplicationStatus.ID,
            ApplicationStatus.CourseName,
            ApplicationStatus.CourseCode,
            ApplicationStatus.UniversityName,
            ApplicationStatus.Country,
            ApplicationStatus.CourseDuration,
            ApplicationStatus.DegreeType,
            ApplicationStatus.IELTS,
            ApplicationStatus.GRE,
            ApplicationStatus.SAT,
            ApplicationStatus.TutionFee,
            ApplicationStatus.MaxScholarship,
            ApplicationStatus.Intake,
            ApplicationStatus.ApplicationDeadline,
            ApplicationStatus.StudyMode
            
        FROM 
            ApplicationStatus
        INNER JOIN 
            URDashboard ON ApplicationStatus.CourseCode = URDashboard.CourseCode
        WHERE 
            URDashboard.URID = @URID;";

            // Execute the query and fill the DataGridView
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter for URID
                    cmd.Parameters.AddWithValue("@URID", urid);

                    // Open connection
                    conn.Open();

                    // Data Adapter and DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind DataTable to DataGridView
                        dataGridView2.DataSource = dataTable;
                    }
                }
            }
        }

        private void textBox13_Enter(object sender, EventArgs e)
        {
            
            if (textBox13.Text == "Search ID")
            {
                textBox13.Text = "";
                //textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                textBox13.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                textBox13.Text = "Search ID";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                textBox13.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            NewLogin login = new NewLogin();
            this.Hide();
            login.Show();

        }
    }
}


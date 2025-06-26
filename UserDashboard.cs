using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace FinalProject
{
    public partial class UserDashboard : Form
    {
        private System.Windows.Forms.Timer displayTimer;
        public string id;

        public UserDashboard(string id)
        {
            InitializeComponent();
            this.id = id;
            //panel visibility
            panel2.Visible = false;
            dataGridView1.Size = new Size(658, 421);
            dataGridView1.Location=new Point(-25, 67);
            panel1.Location = new Point(-7, 0);
            panel1.Size = new Size(894,67);
            panel3.Location = new Point(635, 67);
            panel3.Size = new Size(250, 421);
            panel3.Visible = true;
            this.Location = new Point(0, 0);
            this.Size = new Size(900,530);

            label8.Text = "Welcome";
            label8.Visible = true; // Make sure the label is visible initially
            label3.Visible = false;

            // Initialize the timer
            displayTimer = new System.Windows.Forms.Timer();
            displayTimer.Interval = 2000; // Set timer interval to 5 seconds
            displayTimer.Tick += DisplayTimer_Tick; // Subscribe to the Tick event
            displayTimer.Start(); // Start the timer


            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";

            // Query to fetch all details for the given ID
            string query3 = "SELECT Name FROM StudentDetails WHERE ID = @Id";

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


            LoadData();

        }

        public void LoadData()
        {

            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            string query = "SELECT * FROM URDashboard";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        //Hide the URID column
                        if (dataGridView1.Columns.Contains("URID"))
                        {
                            dataGridView1.Columns["URID"].Visible = false;
                        }

                        // Adjust column widths dynamically
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        // Enable header text wrapping
                        dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;


                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("An error occurred: " + ex.Message);
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            label8.Visible = false; // Hide the label
            displayTimer.Stop(); // Stop the timer as it's no longer needed
            label3.Visible = true; // Show the second label
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string courseName = textBox1.Text.Trim();
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            string query = "SELECT * FROM URDashboard WHERE CourseName LIKE @SearchText + '%'";

            //empty handler
            if (string.IsNullOrEmpty(courseName))
            {
                MessageBox.Show("Please enter a course name to search.", "Suggest", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter to prevent SQL injection
                        command.Parameters.AddWithValue("@SearchText", courseName);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        //Hide the URID column
                        if (dataGridView1.Columns.Contains("URID"))
                        {
                            dataGridView1.Columns["URID"].Visible = false;
                        }

                        // Adjust column widths dynamically
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        // Enable header text wrapping
                        dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;


                    }
                }
                catch (Exception ex)
                {
                   // MessageBox.Show("An error occurred: " + ex.Message);
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LoadData();
            textBox1.Text = string.Empty;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //NewLogin login = new NewLogin();
            //login.Show();
            //this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void UserDashborad_Load(object sender, EventArgs e)
        {

        }

        private void UserDashborad_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_FINAL_DB_DataSet7.ApplicationStatus' table. You can move, or remove it, as needed.
            //this.applicationStatusTableAdapter.Fill(this._FINAL_DB_DataSet7.ApplicationStatus);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //panel visibility
            panel3.Visible = false;

            panel2.Location = new Point(635, 67);
            panel2.Size = new Size(250, 421);
            panel2.Visible = true;

            //refresh data
            LoadData();
            textBox1.Text = string.Empty;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //panel visibility
            panel2.Visible = false;

            //panel3.Location = new Point(644, 67);
            //panel3.Size = new Size(244, 421);
            panel3.Visible = true;

            comboBox1.Text = null;
            textBox4.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            // Operation
            string country = comboBox1.Text.Trim();
            string courseName = textBox3.Text.Trim();
            string ieltsScore = textBox4.Text.Trim();
            string tuitionFee = textBox2.Text.Trim();

            // Check empty
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(courseName) ||
                string.IsNullOrEmpty(ieltsScore) || string.IsNullOrEmpty(tuitionFee))
            {
                MessageBox.Show("Please Fill out all the fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            

            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            string query = @"SELECT * FROM URDashboard 
                 WHERE Country = @Country 
                 AND CourseName = @CourseName
                 AND IELTS <= @IELTS
                 AND TutionFee <= @TutionFee";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();

                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Country", country);
                        command.Parameters.AddWithValue("@CourseName", courseName);
                        command.Parameters.AddWithValue("@IELTS", ieltsScore);
                        command.Parameters.AddWithValue("@TutionFee", tuitionFee);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Check if any rows are returned
                            if (dataTable.Rows.Count > 0)
                            {
                                dataGridView1.DataSource = dataTable;
                            }
                            else
                            {
                                dataGridView1.DataSource = null; // No data matched the query
                                MessageBox.Show("No matching records found.");
                            }
                        }

                        // Hide the URID column
                        if (dataGridView1.Columns.Contains("URID"))
                        {
                            dataGridView1.Columns["URID"].Visible = false;
                        }

                        // Adjust column widths dynamically
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        // Enable header text wrapping
                        dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertDataIntoDatabase();
            LoadData();
            textBox1.Text = null;
        }
        private void InsertDataIntoDatabase()
        {
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";
            // Check if there is at least one selected row
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Capture data from DataGridView
                string courseName = row.Cells["CourseName"].Value?.ToString();
                string courseCode = row.Cells["CourseCode"].Value?.ToString();
                string universityName = row.Cells["UniversityName"].Value?.ToString();
                string country = row.Cells["Country"].Value?.ToString();
                string courseDuration = row.Cells["CourseDuration"].Value?.ToString();
                string degreeType = row.Cells["DegreeType"].Value?.ToString();
                string ielts = Convert.ToString(row.Cells["IELTS"].Value);
                string gre = row.Cells["GRE"].Value?.ToString();
                string sat = row.Cells["SAT"].Value?.ToString();
                string tuitionFee = Convert.ToString(row.Cells["TutionFee"].Value);
                string maxScholarship = Convert.ToString(row.Cells["MaxScholarship"].Value);
                string intake = row.Cells["Intake"].Value?.ToString();
                string applicationDeadline = row.Cells["ApplicationDeadline"].Value?.ToString();
                string studyMode = row.Cells["StudyMode"].Value?.ToString();
                string applicationStatus = "Pending";
                

                // SQL INSERT Statement
                string query = @"
            INSERT INTO ApplicationStatus 
            (ApplicationStatus, CourseName, CourseCode, UniversityName, Country, CourseDuration, DegreeType, IELTS, GRE, SAT, TutionFee, MaxScholarship, Intake, ApplicationDeadline, StudyMode, ID)
            VALUES
            (@ApplicationStatus, @CourseName, @CourseCode, @UniversityName, @Country, @CourseDuration, @DegreeType, @IELTS, @GRE, @SAT, @TutionFee, @MaxScholarship, @Intake, @ApplicationDeadline, @StudyMode, @ID)";

                // Create connection and command
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                        command.Parameters.AddWithValue("@CourseName", courseName);
                        command.Parameters.AddWithValue("@CourseCode", courseCode);
                        command.Parameters.AddWithValue("@UniversityName", universityName);
                        command.Parameters.AddWithValue("@Country", country);
                        command.Parameters.AddWithValue("@CourseDuration", courseDuration);
                        command.Parameters.AddWithValue("@DegreeType", degreeType);
                        command.Parameters.AddWithValue("@IELTS", ielts);
                        command.Parameters.AddWithValue("@GRE", gre);
                        command.Parameters.AddWithValue("@SAT", sat);
                        command.Parameters.AddWithValue("@TutionFee", tuitionFee);
                        command.Parameters.AddWithValue("@MaxScholarship", maxScholarship);
                        command.Parameters.AddWithValue("@Intake", intake);
                        command.Parameters.AddWithValue("@ApplicationDeadline", applicationDeadline);
                        command.Parameters.AddWithValue("@StudyMode", studyMode);
                        command.Parameters.AddWithValue("@ID", id);

                        //try
                        //{
                            // Open the connection and execute the query
                            conn.Open();
                            int result = command.ExecuteNonQuery();

                            // Check result
                            if (result > 0)
                            {
                                MessageBox.Show("Application successfull.","Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           // MessageBox.Show("No matching records found!", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                            else
                            {
                                MessageBox.Show("Data insertion failed.","Unsuccessful",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("No row selected.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {


            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";
            string query = "SELECT * FROM ApplicationStatus WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter to the SQL command
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the query results
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Adjust the DisplayIndex for ApplicationStatus if the column exists
                        if (dataGridView1.Columns.Contains("ApplicationStatus"))
                        {
                            dataGridView1.Columns["ApplicationStatus"].DisplayIndex = 0;
                        }
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertDataIntoDatabase();
            LoadData();
            comboBox1.Text = null;
            textBox4.Text = null;
            textBox2.Text= null;
            textBox3.Text= null;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            NewLogin login = new NewLogin();
            login.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

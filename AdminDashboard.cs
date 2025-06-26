using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
//using Microsoft.Data.SqlClient;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace FinalProject
{
    public partial class AdminDashboard : Form
    {
        private int id;

        public AdminDashboard()
        {
            InitializeComponent();
        }
        public AdminDashboard(string id)
        {
            InitializeComponent();
            this.id = int.Parse(id);

            // Connection string to your database
            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";

            // Query to fetch all details for the given ID
            string query3 = "SELECT Name FROM AdminDetails WHERE ID = @Id";

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
                                linkLabel6.Text = reader["Name"].ToString();
                            }
                            else
                            {
                                // Handle the case where no record is found for the given ID
                                MessageBox.Show("No record found for this provided ID.", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


            this.Size = new Size(986, 523);
            this.Location = new Point(0,0);
            panel1.Size = new Size(850, 400);
            panel2.Size = panel1.Size;
            panel3.Size = panel1.Size;
            panel4.Size = panel1.Size;
            panel5.Size = new Size(850, 400);
            panel5.Location = new Point(154, 81);
            panel7.Size = new Size(760, 400);
            panel8.Size = panel1.Size;
            panel1.Location = new Point(160, 80);
            panel2.Location = panel1.Location;
            panel3.Location = panel1.Location;
            panel4.Location = panel1.Location;
            //panel5.Location = panel1.Location;
            panel7.Location = new Point(154, 81);
            panel8.Location = panel1.Location;
            label24.Size = new Size(324,315);
            label24.Location = new Point(350,111);
            label25.Size = new Size(194, 227);
            label25.Location = new Point(509, 85);
            label26.Size = new Size(160,160);
            label26.Location = new Point(512,102);
            //panel5.Location = new Point(200,200);

            //panel1.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            //label24.Visible = false;
            label24.Visible = true;

            dataGridView2.DataSource = null;
            dataGridView1.DataSource = null;
            //just a comment
            //rimal

            //string query = "SELECT COUNT(*) FROM section WHERE Id = @Id AND Name = @Name";
            string query = "SELECT COUNT(*) FROM StudentDetails";


            using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@ID", id);
                    //command.Parameters.AddWithValue("@Password", pass);

                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count >= 0)
                    {
                        linkLabel1.Text = count.ToString();
                        

                    }
                    else
                    {

                        //MessageBox.Show("Invalid Id or Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection.Close();
                }

            }

            string query1 = "SELECT COUNT(*) FROM URDetails";


            using (SqlConnection connection1 = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            {
                using (SqlCommand command = new SqlCommand(query1, connection1))
                {
                    //command.Parameters.AddWithValue("@ID", id);
                    //command.Parameters.AddWithValue("@Password", pass);

                    connection1.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count >= 0)
                    {
                        linkLabel2.Text = count.ToString();
                        

                    }
                    else
                    {

                        //MessageBox.Show("Invalid Id or Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection1.Close();
                }

            }

        }



        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            

            // Assuming the ID input is from a TextBox
            string idInput = textBox1.Text;  // TextBox for ID input
            int id;

            // Try to parse the ID to an integer
            if (int.TryParse(idInput, out id))
            {
                // Proceed with the search if the ID is valid
                SearchById(id);
            }
            else
            {
                // Show a message if the ID is not valid
                MessageBox.Show("Invalid ID.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = null;
            }
        }

        private void SearchById(int id)
        {
            using (SqlConnection connection1 = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            {
                // Query to get the data
                string query1 = "SELECT ID, Name, Nationality, Gender, Email, Age FROM StudentDetails WHERE ID = @id";

                // Create the command and add the parameter
                SqlCommand command = new SqlCommand(query1, connection1);
                command.Parameters.AddWithValue("@id", id); // Use the ID parameter in the query

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection1.Open();

                    // Execute the query and fill the DataTable
                    adapter.Fill(dataTable);

                    // Check if any rows were returned
                    if (dataTable.Rows.Count > 0)
                    {
                        // Data found, show success message and bind to DataGridView
                        MessageBox.Show("ID found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = dataTable;
                        textBox1.Text = null;

                    }
                    else
                    {
                        // No rows found, show error message
                        MessageBox.Show("Invalid ID.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = null;

                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("Error: " + ex.Message);
                    textBox1.Text = null;

                }
                finally
                {
                    // Ensure the connection is closed
                    connection1.Close();
                }
            }





        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.studentDetailsTableAdapter.Fill(this._FINAL_DB_DataSet.StudentDetails);


            try
            {
                // Define the query to fetch all records
                string query = "SELECT ID, Name, Nationality, Gender, Email, Age FROM StudentDetails";

                using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        connection.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Optional: Clear any text in the search box
                        textBox1.Text = string.Empty;

                        // MessageBox.Show("Data refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing data! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

            {
                // Ensure a row is selected in the DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the ID of the selected row
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    string id = dataGridView1.Rows[selectedRowIndex].Cells["iDDataGridViewTextBoxColumn"].Value.ToString();

                    // Confirm the delete action
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Delete the record from the database
                        using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                        {
                            string query = "DELETE FROM StudentDetails WHERE ID = @id";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", id);

                            try
                            {
                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Remove the row from the DataGridView
                                    dataGridView1.Rows.RemoveAt(selectedRowIndex);
                                }
                                else
                                {
                                    MessageBox.Show("No record found to delete!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {

            try
            {
                // Define the query to fetch all records
                string query = "SELECT ID, Name, Nationality, Gender, Email, Age FROM StudentDetails";

                using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Create an adapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Open the connection
                        connection.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Update the DataGridView with the new data
                        dataGridView1.DataSource = dataTable;

                        // Update the total count in the linkLabel
                        linkLabel1.Text = dataTable.Rows.Count.ToString();

                        // Clear the search box for a refreshed view
                        textBox1.Text = string.Empty;

                        // Show a success message
                        MessageBox.Show("Data refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message in case of failure
                MessageBox.Show("Failed to  refreshing data " + ex.Message, "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }




            
        }

        private void button9_Click(object sender, EventArgs e)
        {





            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Extract values from the selected row
                string id = selectedRow.Cells["iDDataGridViewTextBoxColumn"].Value.ToString(); // Replace with your column name
                string name = selectedRow.Cells["nameDataGridViewTextBoxColumn"].Value.ToString();
                string nationality = selectedRow.Cells["nationalityDataGridViewTextBoxColumn"].Value.ToString();
                string gender = selectedRow.Cells["genderDataGridViewTextBoxColumn"].Value.ToString();
                string email = selectedRow.Cells["emailDataGridViewTextBoxColumn"].Value.ToString();
                int age = int.Parse(selectedRow.Cells["ageDataGridViewTextBoxColumn"].Value.ToString());

                // Show Yes/No dialog box for confirmation
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                // If user clicks Yes
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the database
                    using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                    {
                        string query = @"UPDATE StudentDetails 
                                 SET Name = @name, 
                                     Nationality = @nationality, 
                                     Gender = @gender, 
                                     Email = @email, 
                                     Age = @age 
                                 WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@nationality", nationality);
                        command.Parameters.AddWithValue("@gender", gender);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@age", age);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data Updated successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Data Update Failed!", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    // If user clicks No, do nothing
                    MessageBox.Show("Update Cancelled!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Update!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }




        private void button5_Click(object sender, EventArgs e)
        {
            panel6.Location = new Point(panel6.Location.X, button5.Location.Y);

            //panel6.Visible = false;
            panel6.Height = button5.Height;
            panel6.Visible = true;
            //user1.BringToFront();
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            //panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            panel1.Visible = true;
            //label24.Visible = true;


            //panel2.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel6.Height = button4.Height;
            panel6.Location = new Point(panel6.Location.X, button4.Location.Y);
            panel6.Visible = true;

            //ur1.BringToFront();
            panel1.Visible = false;
            //panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            //panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            panel2.Visible = true;


            //panel2.BringToFront();
            //panel2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel6.Height = button2.Height;
            panel6.Location = new Point(panel6.Location.X, button2.Location.Y);
            panel6.Visible = true;

            dataGridView4.DataSource = null;
            string query = "SELECT COUNT(*) FROM URDummy";


            using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    

                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count >= 0)
                    {
                        linkLabel4.Text = count.ToString();
                        

                    }
                    else
                    {

                        //MessageBox.Show("Invalid Id or Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection.Close();
                }

            }


            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            //panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            panel3.Visible = true;

        }

        private void ur1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string idInput1 = textBox2.Text;  // TextBox for ID input
            int id1;

            // Try to parse the ID to an integer
            if (int.TryParse(idInput1, out id1))
            {
                // Proceed with the search if the ID is valid
                SearchById1(id1);
            }
            else
            {
                // Show a message if the ID is not validif (int.TryParse(idInput, out int id))

                MessageBox.Show("Invalid ID!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = null;
            }
        }

        private void SearchById1(int id1)
        {
            using (SqlConnection connection1 = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            {
                // Query to get the data
                string query1 = "SELECT ID, Name, Nationality, Gender, Email, UniversityName, EIIN FROM URDetails WHERE ID = @id";

                // Create the command and add the parameter
                SqlCommand command = new SqlCommand(query1, connection1);
                command.Parameters.AddWithValue("@id", id1); // Use the ID parameter in the query

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection1.Open();

                    // Execute the query and fill the DataTable
                    adapter.Fill(dataTable);

                    // Check if any rows were returned
                    if (dataTable.Rows.Count > 0)
                    {
                        // Data found, show success message and bind to DataGridView
                        MessageBox.Show("ID found!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView2.DataSource = dataTable;
                        textBox2.Text = null;

                    }
                    else
                    {
                        // No rows found, show error message
                        MessageBox.Show("Invalid ID!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox2.Text = null;

                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("Error: " + ex.Message);
                    textBox2.Text = null;

                }
                finally
                {
                    // Ensure the connection is closed
                    connection1.Close();
                }
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void AdminDashboard_Load_1(object sender, EventArgs e)
        {
            

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Define the query to fetch all records
                string query1 = "SELECT ID, Name, Nationality, Gender, Email, UniversityName, EIIN FROM URDetails";

                using (SqlConnection connection1 = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query1, connection1))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        connection1.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView2.DataSource = dataTable;

                        // Optional: Clear any text in the search box
                        textBox2.Text = string.Empty;

                        // MessageBox.Show("Data refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to  refreshing data " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the query to fetch all records
                string query = "SELECT ID, Name, Nationality, Gender, Email, UniversityName, EIIN FROM URDetails";

                using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Create an adapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Open the connection
                        connection.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Update the DataGridView with the new data
                        dataGridView2.DataSource = dataTable;

                        // Update the total count in the linkLabel
                        linkLabel2.Text = dataTable.Rows.Count.ToString();

                        // Clear the search box for a refreshed view
                        textBox2.Text = string.Empty;

                        // Show a success message
                        MessageBox.Show("Data refreshed successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message in case of failure
                MessageBox.Show("Failed to refreshing data!" + ex.Message, "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Extract values from the selected row
                string id = selectedRow.Cells["iDDataGridViewTextBoxColumn1"].Value.ToString(); // Replace with your column name
                string name = selectedRow.Cells["nameDataGridViewTextBoxColumn1"].Value.ToString();
                string nationality = selectedRow.Cells["nationalityDataGridViewTextBoxColumn1"].Value.ToString();
                string gender = selectedRow.Cells["genderDataGridViewTextBoxColumn1"].Value.ToString();
                string email = selectedRow.Cells["emailDataGridViewTextBoxColumn1"].Value.ToString();
                string universityname = selectedRow.Cells["universityNameDataGridViewTextBoxColumn"].Value.ToString();
                int eiin = int.Parse(selectedRow.Cells["eIINDataGridViewTextBoxColumn"].Value.ToString());

                // Show Yes/No dialog box for confirmation
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If user clicks Yes
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the database
                    using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                    {
                        string query = @"UPDATE URDetails SET Name = @name, Nationality = @nationality, Gender = @gender, Email = @email,UniversityName=@universityname,EIIN=@eiin WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@nationality", nationality);
                        command.Parameters.AddWithValue("@gender", gender);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@universityname", universityname);
                        command.Parameters.AddWithValue("@eiin", eiin);



                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data Updated successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Data Update Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    // If user clicks No, do nothing
                    MessageBox.Show("Update Cancelled!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Update!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            {
                // Ensure a row is selected in the DataGridView
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    // Get the ID of the selected row
                    int selectedRowIndex = dataGridView2.SelectedRows[0].Index;
                    string id = dataGridView2.Rows[selectedRowIndex].Cells["iDDataGridViewTextBoxColumn1"].Value.ToString();

                    // Confirm the delete action
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Delete the record from the database
                        using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                        {
                            string query = "DELETE FROM URDetails WHERE ID = @id";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", id);

                            try
                            {
                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record deleted successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Remove the row from the DataGridView
                                    dataGridView1.Rows.RemoveAt(selectedRowIndex);
                                }
                                else
                                {
                                    MessageBox.Show("No record found to delete!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel6.Height = button1.Height;
            panel6.Location = new Point(panel6.Location.X, button1.Location.Y);
            panel6.Visible = true;

            dataGridView3.DataSource = null;
            string query = "SELECT COUNT(*) FROM AdminDetails";


            using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@ID", id);
                    //command.Parameters.AddWithValue("@Password", pass);

                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count >= 0)
                    {
                        linkLabel3.Text = count.ToString();
                        //MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Hide();
                        //URDashboard urd = new URDashboard(id);
                        //urd.Show();

                    }
                    else
                    {

                        //MessageBox.Show("Invalid Id or Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection.Close();
                }

            }



            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel5.Visible = false;
            //panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            panel4.Visible = true;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            label24.Visible = false;
            label26.Visible = false;

            panel5.Visible = true;
            label25.Visible = true;
            try
            {
                // Define the query to get the max value
                string query = "SELECT ISNULL(MAX(ID), 0) AS MaxValue FROM AdminDetails"; // Adjust table and column names

                // Connection string (adjust accordingly)
                string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and get the max value
                        object result = command.ExecuteScalar();
                        int maxValue = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                        // Display max value + 1 on the label
                        label14.Text = $"{maxValue + 1}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching max value!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string gender;
        private void button21_Click(object sender, EventArgs e)
        {

            string name = textBox4.Text;
            string country = textBox5.Text;
            string email = textBox6.Text;
            string address = textBox7.Text;
            string contactNumber = textBox8.Text;
            string password = textBox9.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(contactNumber) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all the fields!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";

                string query = "INSERT INTO AdminDetails (Name, Address, Email, Country, Gender, ContactNumber, Password) VALUES (@Name, @Address, @Email, @Country, @Gender, @ContactNumber, @Password)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //command.Parameters.AddWithValue("@ID", "2");
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Country", country);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        command.Parameters.AddWithValue("@Password", password);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            textBox4.Text = null;
                            textBox5.Text = null;
                            textBox6.Text = null;
                            textBox7.Text = null;
                            textBox8.Text = null;
                            textBox9.Text = null;
                            radioButton1.Checked = false;
                            radioButton2.Checked = false;
                            panel5.Visible = false;
                            label25.Visible = false;
                            panel4.Visible = true;


                        }
                        else
                        {
                            MessageBox.Show("Failed to create the profile. Please try again.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

                // Extract values from the selected row
                string id = selectedRow.Cells["iDDataGridViewTextBoxColumn2"].Value.ToString(); // Replace with your column name
                string universityname = selectedRow.Cells["UniversityName"].Value.ToString();
                string eiin = selectedRow.Cells["EIIN"].Value.ToString();
                string name = selectedRow.Cells["nameDataGridViewTextBoxColumn2"].Value.ToString();
                string nationality = selectedRow.Cells["nationalityDataGridViewTextBoxColumn2"].Value.ToString();
                string gender = selectedRow.Cells["genderDataGridViewTextBoxColumn2"].Value.ToString();
                string email = selectedRow.Cells["emailDataGridViewTextBoxColumn2"].Value.ToString();
                //string regdate = selectedRow.Cells["regDateDataGridViewTextBoxColumn"].Value.ToString();
                string pass = selectedRow.Cells["Password"].Value.ToString();

                // Show Yes/No dialog box for confirmation
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to Add this record?", "Confirm Add", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                // If user clicks Yes
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the database
                    using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                    {
                        string query = "INSERT INTO URDetails (ID, UniversityName, EIIN, Name, Nationality, Gender, Email, Password) " + "VALUES (@ID, @UniversityName, @EIIN, @Name, @Nationality, @Gender, @Email, @Password)";


                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@UniversityName", universityname);
                        command.Parameters.AddWithValue("@EIIN", eiin);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Nationality", nationality);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Email", email);
                        //DateTime registrationDate = DateTime.Parse(regdate);
                        //command.Parameters.AddWithValue("@RegistrationDate", registrationDate);
                        command.Parameters.AddWithValue("@Password", pass);



                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Accepted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                string deleteQuery = "DELETE FROM URDummy WHERE ID = @ID";
                                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                                deleteCommand.Parameters.AddWithValue("@ID", id);

                                int deleteRowsAffected = deleteCommand.ExecuteNonQuery();

                                if (deleteRowsAffected > 0)
                                {
                                    //MessageBox.Show("Data deleted from URDummy table!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    //MessageBox.Show("Failed to delete data from URDummy table!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    // If user clicks No, do nothing
                    MessageBox.Show("Cancelled!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Accept!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

                // Extract values from the selected row
                string id = selectedRow.Cells["iDDataGridViewTextBoxColumn2"].Value.ToString(); // Replace with your column name
                string universityname = selectedRow.Cells["UniversityName"].Value.ToString();
                string eiin = selectedRow.Cells["EIIN"].Value.ToString();
                string name = selectedRow.Cells["nameDataGridViewTextBoxColumn2"].Value.ToString();
                string nationality = selectedRow.Cells["nationalityDataGridViewTextBoxColumn2"].Value.ToString();
                string gender = selectedRow.Cells["genderDataGridViewTextBoxColumn2"].Value.ToString();
                string email = selectedRow.Cells["emailDataGridViewTextBoxColumn2"].Value.ToString();
                //string regdate = selectedRow.Cells["regDateDataGridViewTextBoxColumn"].Value.ToString();
                string pass = selectedRow.Cells["Password"].Value.ToString();

                // Show Yes/No dialog box for confirmation
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to Add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If user clicks Yes
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the database
                    using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                    {

                        try
                        {
                            connection.Open();

                            string deleteQuery = "DELETE FROM URDummy WHERE ID = @ID";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                            deleteCommand.Parameters.AddWithValue("@ID", id);

                            int deleteRowsAffected = deleteCommand.ExecuteNonQuery();

                            if (deleteRowsAffected > 0)
                            {
                                MessageBox.Show("Rejected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete data from URDummy table!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    // If user clicks No, do nothing
                    MessageBox.Show("Cancelled!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Accept!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the query to fetch all records
                string query = "SELECT * FROM URDummy";


                using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Create an adapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Open the connection
                        connection.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Update the DataGridView with the new data
                        dataGridView4.DataSource = dataTable;

                        // Update the total count in the linkLabel
                        linkLabel4.Text = dataTable.Rows.Count.ToString();

                        // Clear the search box for a refreshed view
                        //textBox2.Text = string.Empty;

                        // Show a success message
                        MessageBox.Show("Application refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message in case of failure
                MessageBox.Show("Failed refreshing data!" + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Define the query to fetch all records
                string query1 = "SELECT * FROM URDummy";

                using (SqlConnection connection1 = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query1, connection1))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        connection1.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView4.DataSource = dataTable;

                        // Optional: Clear any text in the search box
                        //textBox2.Text = string.Empty;

                        // MessageBox.Show("Data refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed refreshing data!" + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                // Define the query to fetch all records
                string query1 = "SELECT * FROM AdminDetails";

                using (SqlConnection connection1 = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query1, connection1))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        connection1.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView3.DataSource = dataTable;
                        dataGridView3.Visible = true;

                        // Optional: Clear any text in the search box
                        textBox3.Text = string.Empty;

                        // MessageBox.Show("Data refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed refreshing data!" + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                //dataGridViewTextBoxColumn1
                //dataGridViewTextBoxColumn2
                //dataGridViewTextBoxColumn4
                //Country
                //Address
                //dataGridViewTextBoxColumn5
                //ContactNumber


                // Get the selected row
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

                // Extract values from the selected row
                string id = selectedRow.Cells["dataGridViewTextBoxColumn1"].Value.ToString(); // Replace with your column name
                string name = selectedRow.Cells["dataGridViewTextBoxColumn2"].Value.ToString();
                string gender = selectedRow.Cells["dataGridViewTextBoxColumn4"].Value.ToString();
                string country = selectedRow.Cells["Country"].Value.ToString();
                string address = selectedRow.Cells["Address"].Value.ToString();
                string email = selectedRow.Cells["dataGridViewTextBoxColumn5"].Value.ToString();
                string contactnumber = selectedRow.Cells["ContactNumber"].Value.ToString();

                // Show Yes/No dialog box for confirmation
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If user clicks Yes
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the database
                    using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                    {
                        string query = @"UPDATE AdminDetails SET Name = @name, Gender = @gender, Country = @country, Email = @email,Address=@address,ContactNumber=@contactnumber WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@gender", gender);
                        command.Parameters.AddWithValue("@country", country);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@address", address);
                        command.Parameters.AddWithValue("@contactnumber", contactnumber);
                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Data Update Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    // If user clicks No, do nothing
                    MessageBox.Show("Update Cancelled!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Update!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Get the ID of the selected row
                int selectedRowIndex = dataGridView3.SelectedRows[0].Index;
                string id = dataGridView3.Rows[selectedRowIndex].Cells["dataGridViewTextBoxColumn1"].Value.ToString();

                // Confirm the delete action
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Delete the record from the database
                    using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                    {
                        string query = "DELETE FROM AdminDetails WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", id);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Remove the row from the DataGridView
                                dataGridView1.Rows.RemoveAt(selectedRowIndex);
                            }
                            else
                            {
                                MessageBox.Show("No record found to delete!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the query to fetch all records
                string query = "SELECT * FROM AdminDetails";

                using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Create an adapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Open the connection
                        connection.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Update the DataGridView with the new data
                        dataGridView3.DataSource = dataTable;

                        // Update the total count in the linkLabel
                        linkLabel3.Text = dataTable.Rows.Count.ToString();

                        // Clear the search box for a refreshed view
                        textBox3.Text = string.Empty;

                        // Show a success message
                        MessageBox.Show("Data refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message in case of failure
                MessageBox.Show("Failed to refreshing data " + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string idInput1 = textBox3.Text;  // TextBox for ID input
            int id2;

            // Try to parse the ID to an integer
            if (int.TryParse(idInput1, out id2))
            {
                // Proceed with the search if the ID is valid
                SearchById2(id2);
            }
            else
            {
                // Show a message if the ID is not validif (int.TryParse(idInput, out int id))

                MessageBox.Show("Invalid ID!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = null;
            }
        }
        private void SearchById2(int id2)
        {
            using (SqlConnection connection1 = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            {
                // Query to get the data
                string query1 = "SELECT * FROM AdminDetails WHERE ID = @id";

                // Create the command and add the parameter
                SqlCommand command = new SqlCommand(query1, connection1);
                command.Parameters.AddWithValue("@id", id2); // Use the ID parameter in the query

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection1.Open();

                    // Execute the query and fill the DataTable
                    adapter.Fill(dataTable);

                    // Check if any rows were returned
                    if (dataTable.Rows.Count > 0)
                    {
                        // Data found, show success message and bind to DataGridView
                        MessageBox.Show("ID found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView3.DataSource = dataTable;
                        textBox3.Text = null;

                    }
                    else
                    {
                        // No rows found, show error message
                        MessageBox.Show("Invalid ID!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox3.Text = null;

                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("Error: " + ex.Message);
                    textBox3.Text = null;

                }
                finally
                {
                    // Ensure the connection is closed
                    connection1.Close();
                }
            }

        }

        private void button25_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            panel4.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel6.Height = button7.Height;
            panel6.Location = new Point(panel6.Location.X, button7.Location.Y);
            panel6.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            panel4.Visible = false;
            panel8.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel6.Height = button6.Height;
            panel6.Location = new Point(panel6.Location.X, button6.Location.Y);
            panel6.Visible = true;



        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel8.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            panel7.Visible = true;
            label26.Visible = true;


            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";
            string query = "SELECT ID,Name, Address, Email, Country, Gender, ContactNumber, Password FROM [dbo].[AdminDetails] WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox10.Text = reader["ID"].ToString();
                            textBox11.Text = reader["Name"].ToString();
                            textBox12.Text = reader["Address"].ToString();
                            textBox13.Text = reader["Email"].ToString();
                            textBox14.Text = reader["Country"].ToString();
                            string gender = reader["Gender"].ToString(); // Example for dropdown
                            textBox15.Text = reader["ContactNumber"].ToString();
                            string pass = reader["Password"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given ID!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
            }

            panel7.Visible = true;
            label26.Visible = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {


            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True";
            string query = "UPDATE AdminDetails SET Name = @Name, Address = @Address, Email = @Email, Country = @Country, ContactNumber = @ContactNumber  WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", textBox11.Text);
                    command.Parameters.AddWithValue("@Address", textBox12.Text);
                    command.Parameters.AddWithValue("@Email", textBox13.Text);
                    command.Parameters.AddWithValue("@Country", textBox14.Text);
                    command.Parameters.AddWithValue("@ContactNumber", textBox15.Text);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panel7.Visible = false;
                        label24.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("Update failed! Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            label24.Visible = true;

            //AdminDashboard
        }

        private void button27_Click(object sender, EventArgs e)
        {
            panel6.Height = button27.Height;
            panel6.Location = new Point(panel6.Location.X, button27.Location.Y - 1);
            panel6.Visible = true;

            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            panel4.Visible = false;
            panel8.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label24.Visible = true;

            panel6.Visible = true;

        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //NewLogin login = new NewLogin();
            //this.Visible = false;
            //login.Visible = true;
        }

        private void AdminDashboard_Load_2(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView5.SelectedRows[0];

                // Extract values from the selected row
                string coursename = selectedRow.Cells["courseNameDataGridViewTextBoxColumn"].Value.ToString(); // Replace with your column name
                string coursecode = selectedRow.Cells["courseCodeDataGridViewTextBoxColumn"].Value.ToString();
                string uniname = selectedRow.Cells["universityNameDataGridViewTextBoxColumn1"].Value.ToString();
                string country = selectedRow.Cells["countryDataGridViewTextBoxColumn"].Value.ToString();
                string courseduration = selectedRow.Cells["courseDurationDataGridViewTextBoxColumn"].Value.ToString();
                string degreetype = selectedRow.Cells["degreeTypeDataGridViewTextBoxColumn"].Value.ToString();
                string ielts = selectedRow.Cells["iELTSDataGridViewTextBoxColumn"].Value.ToString();
                string gre = selectedRow.Cells["gREDataGridViewTextBoxColumn"].Value.ToString();
                string sat = selectedRow.Cells["sATDataGridViewTextBoxColumn"].Value.ToString();
                string tutionfees = selectedRow.Cells["tutionFeeDataGridViewTextBoxColumn"].Value.ToString();
                string maxscholarship = selectedRow.Cells["maxScholarshipDataGridViewTextBoxColumn"].Value.ToString();
                string intake = selectedRow.Cells["intakeDataGridViewTextBoxColumn"].Value.ToString();
                string deadline = selectedRow.Cells["applicationDeadlineDataGridViewTextBoxColumn"].Value.ToString();
                string studymode = selectedRow.Cells["studyModeDataGridViewTextBoxColumn"].Value.ToString();
                string applyid = selectedRow.Cells["iDDataGridViewTextBoxColumn3"].Value.ToString();


                // Show Yes/No dialog box for confirmation
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                // If user clicks Yes
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the database
                    using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                    {
                        string query = @"UPDATE ApplicationStatus 
                                 SET ID=@ID, StudyMode=@StudyMode, ApplicationDeadline=@ApplicationDeadline, 
                                 Intake=@Intake, MaxScholarship=@MaxScholarship, TutionFee=@TutionFee, SAT=@SAT, 
                                 GRE=@GRE, IELTS=@IELTS, DegreeType=@DegreeType, CourseDuration=@CourseDuration, 
                                 Country=@Country, UniversityName=@UniversityName, CourseCode=@CourseCode, 
                                 CourseName=@CourseName
                                 WHERE CourseCode = @Coursecode";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", applyid);
                        command.Parameters.AddWithValue("@StudyMode", studymode);
                        command.Parameters.AddWithValue("@ApplicationDeadline", deadline);
                        command.Parameters.AddWithValue("@Intake", intake);
                        command.Parameters.AddWithValue("@MaxScholarship", maxscholarship);
                        command.Parameters.AddWithValue("@TutionFee", tutionfees);
                        command.Parameters.AddWithValue("@SAT", sat);
                        command.Parameters.AddWithValue("@GRE", gre);
                        command.Parameters.AddWithValue("@IELTS", ielts);
                        command.Parameters.AddWithValue("@DegreeType", degreetype);
                        command.Parameters.AddWithValue("@CourseDuration", courseduration);
                        command.Parameters.AddWithValue("@Country", country);
                        command.Parameters.AddWithValue("@UniversityName", uniname);
                        command.Parameters.AddWithValue("@CourseCode", coursecode);
                        command.Parameters.AddWithValue("@CourseName", coursename);
                        command.Parameters.AddWithValue("@ApplicationStatus", coursecode);
                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Data Update Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    // If user clicks No, do nothing
                    MessageBox.Show("Update Cancelled!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Update!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
            {
                // Ensure a row is selected in the DataGridView
                if (dataGridView5.SelectedRows.Count > 0)
                {
                    // Get the ID of the selected row
                    int selectedRowIndex = dataGridView5.SelectedRows[0].Index;
                    string coursecode = dataGridView5.Rows[selectedRowIndex].Cells["courseCodeDataGridViewTextBoxColumn"].Value.ToString();

                    // Confirm the delete action
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Delete the record from the database
                        using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                        {
                            string query = "DELETE FROM ApplicationStatus WHERE CourseCode=@coursecode";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@coursecode", coursecode);

                            try
                            {
                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Course deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                }
                                else
                                {
                                    MessageBox.Show("No record found to delete!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the query to fetch all records
                string query = "SELECT * FROM ApplicationStatus";


                using (SqlConnection connection = new SqlConnection("Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Create an adapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Open the connection
                        connection.Open();

                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);

                        // Update the DataGridView with the new data
                        dataGridView5.DataSource = dataTable;

                        // Update the total count in the linkLabel
                        //linkLabel4.Text = dataTable.Rows.Count.ToString();

                        // Clear the search box for a refreshed view
                        //textBox2.Text = string.Empty;

                        // Show a success message
                        MessageBox.Show("Application refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message in case of failure
                MessageBox.Show("Failed to refreshing data!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
            NewLogin login = new NewLogin();
            this.Visible = false;
            login.Visible = true;
        }
    }
}


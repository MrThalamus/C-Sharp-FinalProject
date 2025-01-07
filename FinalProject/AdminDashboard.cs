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

namespace FinalProject
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            //panel1.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;


            //string query = "SELECT COUNT(*) FROM section WHERE Id = @Id AND Name = @Name";
            string query = "SELECT COUNT(*) FROM StudentDetails";


            using (SqlConnection connection = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
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
        }



        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            //    {
            //        // Assuming the ID input is from a TextBox
            //        string idInput = textBox1.Text;  // TextBox for ID input
            //        int id;

            //        // Try to parse the ID to an integer
            //        if (int.TryParse(idInput, out id))
            //        {
            //            // Proceed with the search if the ID is valid
            //            SearchById(id);
            //        }
            //        else
            //        {
            //            // Show a message if the ID is not valid
            //            MessageBox.Show("Please enter a valid ID.");
            //        }
            //    }


            //    //int id = int.Parse(textBox1.Text);
            //    private void SearchById(int id) { 
            //    using (SqlConnection connection1 = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            //    {
            //        // Query excluding the Password column
            //        string query1 = "SELECT ID, Name, Nationality, Gender, Email, Age FROM StudentDetails WHERE ID=@id";

            //        // Create the command and add the parameter
            //        SqlCommand command = new SqlCommand(query1, connection1);
            //        command.Parameters.AddWithValue("@id", id); // Replace 'yourIdValue' with the actual value for ID


            //        connection1.Open();
            //        var count1 = command.ExecuteScalar();
            //        int count2 = (count1 != DBNull.Value) ? Convert.ToInt32(count1) : 0;

            //        if (count2 > 0)
            //        {

            //            MessageBox.Show("ID found!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            //this.Hide();
            //            //URDashboard urd = new URDashboard(id);
            //            //urd.Show();


            //            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //            DataTable dataTable = new DataTable();
            //            adapter.Fill(dataTable);

            //            // Bind the DataTable to the DataGridView

            //            dataGridView1.DataSource = dataTable;


            //        }
            //        else
            //        {

            //            MessageBox.Show("Invalid Id.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }


            //        // Use SqlDataAdapter to execute the query
            //        connection1.Close();
            //    }
            //}




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
            using (SqlConnection connection1 = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
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

                using (SqlConnection connection = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
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
                 MessageBox.Show("Error refreshing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        // Delete the record from the database
                        using (SqlConnection connection = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
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
                                    MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Remove the row from the DataGridView
                                    dataGridView1.Rows.RemoveAt(selectedRowIndex);
                                }
                                else
                                {
                                    MessageBox.Show("No record found to delete.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Please select a row to delete.", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {

            try
            {
                // Define the query to fetch all records
                string query = "SELECT ID, Name, Nationality, Gender, Email, Age FROM StudentDetails";

                using (SqlConnection connection = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
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
                MessageBox.Show("Error refreshing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            //if (textBox1.Text == null)
            //{
            //    this.studentDetailsTableAdapter.Fill(this._FINAL_DB_DataSet.StudentDetails);
            //}




            //string query = "SELECT COUNT(*) FROM StudentDetails";


            //using (SqlConnection connection = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            //{
            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        //command.Parameters.AddWithValue("@ID", id);
            //        //command.Parameters.AddWithValue("@Password", pass);

            //        connection.Open();

            //        int count = (int)command.ExecuteScalar();

            //        if (count >= 0)
            //        {
            //            linkLabel1.Text = count.ToString();
            //            this.studentDetailsTableAdapter.Fill(this._FINAL_DB_DataSet.StudentDetails);
            //            //MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            //this.Hide();
            //            //URDashboard urd = new URDashboard(id);
            //            //urd.Show();

            //        }
            //        else
            //        {

            //            //MessageBox.Show("Invalid Id or Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        connection.Close();
            //    }
            //}
        }

        private void button9_Click(object sender, EventArgs e)
        {

            //        // Check if a row is selected
            //        if (dataGridView1.SelectedRows.Count > 0)
            //        {
            //            // Get the selected row
            //            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            //            // Extract values from the selected row
            //            string id = selectedRow.Cells["iDDataGridViewTextBoxColumn"].Value.ToString(); // Replace with your column name
            //            string name = selectedRow.Cells["nameDataGridViewTextBoxColumn"].Value.ToString();
            //            string nationality = selectedRow.Cells["nationalityDataGridViewTextBoxColumn"].Value.ToString();
            //            string gender = selectedRow.Cells["genderDataGridViewTextBoxColumn"].Value.ToString();
            //            string email = selectedRow.Cells["emailDataGridViewTextBoxColumn"].Value.ToString();
            //            int age = int.Parse(selectedRow.Cells["ageDataGridViewTextBoxColumn"].Value.ToString());

            //            // Update the database
            //            using (SqlConnection connection = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
            //            {
            //                string query = @"UPDATE StudentDetails 
            //                     SET Name = @name, 
            //                         Nationality = @nationality, 
            //                         Gender = @gender, 
            //                         Email = @email, 
            //                         Age = @age 
            //                     WHERE ID = @id";

            //                SqlCommand command = new SqlCommand(query, connection);
            //                command.Parameters.AddWithValue("@id", id);
            //                command.Parameters.AddWithValue("@name", name);
            //                command.Parameters.AddWithValue("@nationality", nationality);
            //                command.Parameters.AddWithValue("@gender", gender);
            //                command.Parameters.AddWithValue("@email", email);
            //                command.Parameters.AddWithValue("@age", age);

            //                try
            //                {
            //                    connection.Open();
            //                    int rowsAffected = command.ExecuteNonQuery();

            //                    if (rowsAffected > 0)
            //                    {
            //                        MessageBox.Show("Record updated successfully!");
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show("Update failed. Record not found.");
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show("Error: " + ex.Message);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please select a row to update.");
            //        }


            //}





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
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo);

                // If user clicks Yes
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the database
                    using (SqlConnection connection = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
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
                                MessageBox.Show("Data Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Data Update Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Update Cancelled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Update!", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }




        private void button5_Click(object sender, EventArgs e)
        {
            //user1.BringToFront();
            panel2.Visible = false;
            panel1.Visible = true;


            //panel2.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ur1.BringToFront();
            panel1.Visible = false;
            panel2.Visible = true;


            //panel2.BringToFront();
            //panel2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //panel2.Visible = true;
            //panel1.Visible = false;

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

                MessageBox.Show("Invalid ID.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = null;
            }
        }

        private void SearchById1(int id1)
        {
            using (SqlConnection connection1 = new SqlConnection("Data Source=RIMAL\\SQLEXPRESS;Initial Catalog=FINAL(DB);Integrated Security=True;TrustServerCertificate=True"))
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
                        MessageBox.Show("ID found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView2.DataSource = dataTable;
                        textBox2.Text = null;

                    }
                    else
                    {
                        // No rows found, show error message
                        MessageBox.Show("Invalid ID.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
    }


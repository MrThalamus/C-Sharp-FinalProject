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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FinalProject
{
    public partial class Forget : Form
    {
        int idfound = 0;
        int updatepass = 0;
        //private List<string> countryList;
        //private List<string> UniversityList;
        string email;
        string id;
        public Forget()
        {
            InitializeComponent();
            this.Size = new Size(298, 195);
            this.Location=new Point(0, 0);
            panel1.Visible = false;
            panel4.Visible = false;
            panel2.Visible = true;
            panel3.Visible = true;
            panel1.Size = new Size(282, 156);
            panel1.Location = new Point(-1, 0);
            panel4.Size = new Size(190, 3);
            panel4.Location = new Point(43, 70);
            panel2.Size = new Size(190, 3);
            panel2.Location = new Point(60, 83);
            panel3.Size = new Size(190, 3);
            panel3.Location = new Point(60, 48);

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "your ID")
            {
                textBox1.Text = "";
                //textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                textBox1.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "your ID";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                textBox1.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Type Your Email")
            {
                textBox2.Text = "";
                //textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                textBox2.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Type Your Email";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                textBox2.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //panel1.Visible = true;
            id = textBox1.Text.Trim();
            email = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter ID & Email both",
                    "Validation error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(id, out int numericId))
            {
                MessageBox.Show("ID must be a valid numeric value.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /*if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }*/


            string connectionString1 = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            string query1 = "SELECT COUNT(*) FROM StudentDetails WHERE ID = @ID and Email=@Email";

            using (SqlConnection connection1 = new SqlConnection(connectionString1))
            {
                using (SqlCommand command = new SqlCommand(query1, connection1))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Email", email);
                    //command.Parameters.AddWithValue("@Password", pass);

                    connection1.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        idfound = 1;

                        //this.Close();
                        label2.Visible = false;
                        label3.Visible = false;
                        panel2.Visible = false;
                        panel3.Visible = false;
                        panel1.Visible = true;
                        panel4.Visible = true;
                        //this.Close();
                        //panel3.Visible = false;
                        //panel5.Visible = false;
                        //panel6.Visible = false;
                        //panel7.Visible = false;
                        //panel8.Visible = false;
                        //panel4.Visible = true;
                        //panel1.Visible = true;
                        //panel9.Visible = true;

                    }
                    else
                    {
                        idfound = 0;
                        //MessageBox.Show("Invalid Id or Email", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection1.Close();

                }

                string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

                //string query = "SELECT COUNT(*) FROM section WHERE Id = @Id AND Name = @Name";
                string query = "SELECT COUNT(*) FROM URDetails WHERE ID = @Id and Email=@Email";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Email", email);

                        //command.Parameters.AddWithValue("@Password", pass);

                        connection.Open();

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            idfound = 1;
                            //this.Close();
                            label2.Visible=false;
                            label3.Visible=false;
                            panel2.Visible=false;
                            panel3.Visible=false;
                            panel1.Visible = true;
                            panel4.Visible = true;
                            //this.Close();
                            //panel3.Visible = false;
                            //panel5.Visible = false;
                            //panel6.Visible = false;
                            //panel7.Visible = false;
                            //panel8.Visible = false;
                            //panel4.Visible = true;
                            //panel1.Visible = true;
                            //panel9.Visible = true;

                        }
                        else
                        {

                            //MessageBox.Show("Invalid Id or Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        connection.Close();

                    }




                }

            }
            if (idfound == 1)
            {
                MessageBox.Show("Account Found", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                idfound = 0;
                textBox1.Text = null;
                textBox2.Text = null;
            }
            else
            {
                MessageBox.Show("Account Not Found.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = null;
                textBox2.Text = null;
            }


        }

       

        private void Forget_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string id = textBox3.Text.Trim();
            //string email = textBox4.Text.Trim();
            string pass = textBox3.Text.Trim();

            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            //string query = "INSERT INTO UR WHERE ID =label.text (Name, Country, Email, Gender, EIIN, Password) VALUES (@Name, @Country, @Email, @Gender, @EIIN, @Password)";
            string query = "UPDATE StudentDetails SET  Password = @Password WHERE ID = @ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Password", pass);
                    command.Parameters.AddWithValue("@ID", id);
                    //command.Parameters.AddWithValue("@Email", email);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        updatepass = 1;
                        //MessageBox.Show("Password Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //panel2.Visible = false;
                    }
                    else
                    {
                        //MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            string connectionString1 = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            //string query = "INSERT INTO UR WHERE ID =label.text (Name, Country, Email, Gender, EIIN, Password) VALUES (@Name, @Country, @Email, @Gender, @EIIN, @Password)";
            string query1 = "UPDATE URDetails SET  Password = @Password WHERE ID = @ID ";
            using (SqlConnection connection1 = new SqlConnection(connectionString1))
            {
                using (SqlCommand command = new SqlCommand(query1, connection1))
                {

                    command.Parameters.AddWithValue("@Password", pass);
                    command.Parameters.AddWithValue("@ID", id);
                    //command.Parameters.AddWithValue("@Email", email);


                    connection1.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        updatepass = 1;
                        //MessageBox.Show("Password Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //panel2.Visible = false;
                    }
                    else
                    {
                        //MessageBox.Show("Failed to Update Password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (updatepass == 1)
            {
                MessageBox.Show("Password Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updatepass = 0;

                //NewLogin newLogin = new NewLogin();
                this.Hide();
                panel1.Visible = false;
                panel4.Visible = false;
                //newLogin.Show();
                //panel2.Visible = false;
                //panel3.Visible = false;
                //panel4.Visible = false;
                //panel5.Visible = false;
                //panel6.Visible = false;
                //panel7.Visible = false;
                //panel8.Visible = false;
                //panel9.Visible = true;
                //panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("Failed to Update Password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updatepass = 0;
                textBox3.Text = null;
            }
        }
    }
}

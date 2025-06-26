using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FinalProject
{
    public partial class NewLogin : Form
    {
        int idfound = 0;
        int updatepass = 0;
        string email;
        string id;
        private List<string> countryList;
        private List<string> UniversityList;


        public NewLogin()
        {
            InitializeComponent();
            InitializeCountryList();
            InitializeUniversityList();


            button2.Visible = false;
            button3.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            //NewLogin.Size = new Size(834, 442);
            //NewLogin.Location = new Point(0, 0);
            this.Size = new Size(850, 480);
            this.Location = new Point(0, 0);
            panel1.Size = new Size(834, 442);
            panel1.Location = new Point(0, 0);
            panel2.Size = new Size(834, 55);
            panel2.Location = new Point(0, 1);
            panel3.Size = panel1.Size;
            panel3.Location=panel1.Location;
            panel4.Size = panel2.Size;
            panel4.Location=panel2.Location;

            // Enable auto-complete in ComboBox
            comboBoxNationality.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // Set the data source for auto-complete
            AutoCompleteStringCollection autoCompleteData = new AutoCompleteStringCollection();
            autoCompleteData.AddRange(countryList.ToArray());
            //autoCompleteData.AddRange(UniversityList.ToArray());
            comboBoxNationality.AutoCompleteCustomSource = autoCompleteData;
            comboBox1.AutoCompleteCustomSource = autoCompleteData;

            AutoCompleteStringCollection autoCompleteData1 = new AutoCompleteStringCollection();
            autoCompleteData1.AddRange(UniversityList.ToArray());
            comboBox2.AutoCompleteCustomSource = autoCompleteData1;

            // Add event handler for ComboBox item selection
            comboBoxNationality.SelectedIndexChanged += comboBoxNationality_SelectedIndexChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            // Add event handler for dropdown opening to show all countries
            comboBoxNationality.DropDown += comboBox1_DropDown;
            comboBox1.DropDown += comboBox1_DropDown;
            comboBox2.DropDown += comboBox1_DropDown1;
        }

        private void InitializeCountryList()
        {
            countryList = new List<string>
            {
                "Afghanistan", "Albania", "Algeria", "Andorra", "Angola",
                "Argentina", "Armenia", "Australia", "Austria", "Azerbaijan",
                "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus",
                "Belgium", "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia",
                "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso",
                "Burundi", "Cambodia", "Cameroon", "Canada", "Chad",
                "Chile", "China", "Colombia", "Comoros", "Congo", "Croatia",
                "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti",
                "Dominica", "Dominican Republic", "Ecuador", "Egypt",
                "El Salvador", "Estonia", "Eswatini", "Ethiopia", "Fiji",
                "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany",
                "Ghana", "Greece", "Grenada", "Guatemala", "Guinea", "Guyana",
                "Haiti", "Honduras", "Hungary", "Iceland", "India", "Indonesia",
                "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica",
                "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait",
                "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia",
                "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Madagascar",
                "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Mauritania",
                "Mauritius", "Mexico", "Monaco", "Mongolia", "Montenegro",
                "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru",
                "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger",
                "Nigeria", "North Korea", "North Macedonia", "Norway", "Oman",
                "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay",
                "Peru", "Philippines", "Poland", "Portugal", "Qatar", "Romania",
                "Russia", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia",
                "Saint Vincent", "Samoa", "San Marino", "Saudi Arabia",
                "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore",
                "Slovakia", "Slovenia", "Solomon Islands", "Somalia",
                "South Africa", "South Korea", "Spain", "Sri Lanka",
                "Sudan", "Suriname", "Sweden", "Switzerland", "Syria",
                "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Togo",
                "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey",
                "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates",
                "United Kingdom", "United States", "Uruguay", "Uzbekistan",
                "Vanuatu", "Vatican City", "Venezuela", "Vietnam",
                "Yemen", "Zambia", "Zimbabwe"
            };


        }

        private void InitializeUniversityList()
        {
            UniversityList = new List<string>
            {
                "University of Dhaka (DU)","Bangladesh University of Engineering and Technology (BUET)", "Jahangirnagar University (JU)", "Rajshahi University (RU)", "Chittagong University (CU)", "Khulna University (KU)",
                "Shahjalal University of Science and Technology (SUST)", "Bangladesh Agricultural University (BAU)", "Bangabandhu Sheikh Mujibur Rahman Agricultural University (BSMRAU)", "Bangabandhu Sheikh Mujib Medical University (BSMMU)",
                "Patuakhali Science and Technology University (PSTU)", "Sylhet Agricultural University (SAU)", "Islamic University (IU)", "Hajee Mohammad Danesh Science and Technology University (HSTU)", "Jagannath University (JNU)",
                "Noakhali Science and Technology University (NSTU)", "Jessore University of Science and Technology (JUST)", "Begum Rokeya University, Rangpur (BRUR)", "Barisal University (BU)", "Sheikh Hasina University", "North South University (NSU)",
                "BRAC University (BRACU)", "Independent University, Bangladesh (IUB)", "American International University-Bangladesh (AIUB)", "East West University (EWU)", "University of Asia Pacific (UAP)", "Daffodil International University (DIU)",
                "United International University (UIU)", "Stamford University Bangladesh", "Ahsanullah University of Science and Technology (AUST)", "Bangladesh University of Business and Technology (BUBT)", "Southeast University", "International University of Business Agriculture and Technology (IUBAT)",
                "Prime University", "University of Liberal Arts Bangladesh (ULAB)", "Green University of Bangladesh", "World University of Bangladesh", "Northern University Bangladesh", "Eastern University", "Dhaka International University",
                "Islamic University of Technology (IUT)", "Asian University for Women (AUW)","University of California, Berkeley (UC Berkeley)", "University of California, Los Angeles (UCLA)", "University of Michigan, Ann Arbor", "University of Virginia (UVA)",
                "University of North Carolina at Chapel Hill (UNC)", "University of Florida (UF)", "University of Texas at Austin", "University of Washington, Seattle", "University of Wisconsin-Madison", "Ohio State University", "Harvard University", "Stanford University",
                "Massachusetts Institute of Technology (MIT)", "California Institute of Technology (Caltech)", "Princeton University", "Yale University", "University of Chicago", "Columbia University", "Duke University", "Northwestern University", "New York University (NYU)",
                "Carnegie Mellon University", "Brown University", "Cornell University", "Johns Hopkins University", "Vanderbilt University", "Emory University", "University of Southern California (USC)", "Rice University", "Boston University","University of Oxford",
                "University of Cambridge", "Imperial College London", "London School of Economics and Political Science (LSE)", "University College London (UCL)", "King's College London", "University of Manchester", "University of Bristol", "University of Birmingham",
                "University of Warwick", "University of Nottingham", "Durham University", "University of Leeds", "University of Sheffield", "University of Southampton", "University of Edinburgh", "University of Glasgow", "University of St Andrews", "University of Aberdeen",
                "University of Dundee", "Cardiff University", "Swansea University", "Bangor University", "Aberystwyth University", "Queen’s University Belfast", "Ulster University", "University of Toronto", "McMaster University", "University of Ottawa", "Western University",
                "University of Waterloo", "York University", "Queen's University", "Ryerson University", "University of British Columbia (UBC)", "Simon Fraser University", "University of Victoria", "Thompson Rivers University", "McGill University", "University of Montreal",
                "Laval University", "Concordia University", "Université du Québec à Montréal (UQAM)", "University of Alberta", "University of Calgary", "University of Lethbridge", "University of Manitoba", "University of Winnipeg", "University of Saskatchewan", "University of Regina",
                "Dalhousie University", "Saint Mary's University", "University of New Brunswick", "Mount Allison University", "Memorial University of Newfoundland", "University of Prince Edward Island", "Yukon University", "Australian National University", "University of Canberra",
                "Australian Catholic University", "Charles Sturt University", "Macquarie University", "Southern Cross University", "University of New England", "University of New South Wales", "University of Newcastle", "University of Sydney", "University of Technology Sydney",
                "Western Sydney University", "University of Wollongong", "Charles Darwin University", "Bond University", "Central Queensland University", "Griffith University", "James Cook University", "Queensland University of Technology", "University of Queensland",
                "University of Southern Queensland", "University of the Sunshine Coast", "Flinders University", "University of Adelaide", "University of South Australia", "University of Tasmania", "Deakin University", "Federation University Australia", "La Trobe University",
                "Monash University", "RMIT University", "Swinburne University of Technology", "University of Divinity", "University of Melbourne", "Victoria University", "Curtin University", "Edith Cowan University", "Murdoch University", "University of Notre Dame Australia",
                "University of Western Australia", "Ludwig Maximilian University of Munich (LMU Munich)", "University of Heidelberg", "Technical University of Munich (TUM)", "University of Freiburg", "University of Mannheim", "University of Göttingen", "University of Stuttgart",
                "University of Leipzig", "University of Berlin (Humboldt University of Berlin)", "University of Hamburg", "University of Düsseldorf", "University of Frankfurt (Goethe University Frankfurt)", "University of Würzburg", "Technical University of Berlin (TU Berlin)",
                "Technical University of Darmstadt", "Technical University of Dresden", "Technical University of Kaiserslautern", "Technical University of Braunschweig", "University of Bremen", "University of Augsburg", "University of Konstanz", "University of Karlsruhe",
                "University of Paris (Sorbonne University)", "University of Lyon", "University of Bordeaux", "University of Toulouse", "University of Paris-Saclay", "University of Grenoble Alpes", "University of Aix-Marseille", "University of Montpellier", "University of Lille",
                "University of Strasbourg", "University of Nice Sophia Antipolis", "University of Rennes 1", "University of Nantes", "University of Burgundy (Dijon)", "University of Paris 1 Panthéon-Sorbonne", "École Normale Supérieure (ENS Paris)", "École Polytechnique",
                "HEC Paris (Hautes Études Commerciales)", "Sciences Po Paris (Institute of Political Studies)", "École des Mines ParisTech", "ESSEC Business School", "École Centrale Paris", "Université Pierre et Marie Curie (UPMC)", "INSA Lyon (National Institute of Applied Sciences)",
                "École Centrale de Lyon", "Université de Technologie de Compiègne (UTC)", "Tsinghua University (Beijing)", "Peking University (Beijing)", "Fudan University (Shanghai)", "Shanghai Jiao Tong University (Shanghai)", "Zhejiang University (Hangzhou)",
                "University of Science and Technology of China (Hefei)", "Beijing Normal University (Beijing)", "Nanjing University (Nanjing)", "Xi'an Jiaotong University (Xi'an)", "Sun Yat-sen University (Guangzhou)", "Shanghai University (Shanghai)", "Sichuan University (Chengdu)",
                "Tongji University (Shanghai)", "Harbin Institute of Technology (Harbin)", "Ningbo University (Ningbo)", "Shandong University (Jinan)", "Jilin University (Changchun)", "Renmin University of China (Beijing)", "Beijing Institute of Technology (Beijing)", "Chongqing University (Chongqing)",
                "University of Barcelona", "Autonomous University of Barcelona", "Complutense University of Madrid", "University of Madrid (Universidad Politécnica de Madrid)", "University of Valencia", "University of Seville", "University of Granada", "University of Zaragoza", "University of Salamanca",
                "University of Alicante", "University of Murcia", "University of the Basque Country", "University of Navarre", "University of La Laguna (Tenerife)", "University of Santiago de Compostela", "IE University (Madrid)", "Universidad Pontificia Comillas (Madrid)", "Universidad de Navarra (Pamplona)",
                "Universidad CEU San Pablo (Madrid)", "Universidad Internacional de La Rioja", "Polytechnic University of Catalonia", "Polytechnic University of Valencia", "Polytechnic University of Madrid", "Universidad Politécnica de Cartagena", "University of Delhi", "Jawaharlal Nehru University (JNU)",
                "Banaras Hindu University (BHU)", "University of Hyderabad", "Jamia Millia Islamia", "Aligarh Muslim University (AMU)", "University of Calcutta", "University of Mumbai", "University of Madras", "University of Rajasthan", "University of Pune", "Bangalore University", "Lucknow University",
                "Jadavpur University", "Visva-Bharati University", "Punjab University", "Osmania University", "Maharshi Dayanand University", "University of Kerala", "Andhra University", "Dr. B.R. Ambedkar University (Agra)", "Kakatiya University", "Mumbai University", "IIT Bombay", "IIT Delhi", "IIT Kanpur",
                "IIT Kharagpur", "IIT Madras", "IIT Roorkee", "IIT Guwahati", "IIT BHU Varanasi", "IIT Gandhinagar", "IIT Ropar", "IIM Ahmedabad", "IIM Bangalore", "IIM Calcutta", "IIM Lucknow", "IIM Kozhikode", "IIM Indore", "IIM Shillong", "Tata Institute of Social Sciences (TISS)",
                "Indian Statistical Institute (ISI)", "National Law University (NLU)", "Indian Institute of Science (IISc)", "National Institute of Design (NID)", "Bogazici University (Istanbul)", "Middle East Technical University (METU)", "Istanbul Technical University (ITU)",
                "Ankara University (Ankara)", "Hacettepe University (Ankara)", "Istanbul University (Istanbul)", "Koç University (Istanbul)", "Sabancı University (Istanbul)", "Ege University (Izmir)", "Gazi University (Ankara)", "Yildiz Technical University (Istanbul)", "Izmir Institute of Technology (IZTECH)",
                "Bilkent University (Ankara)", "Kadir Has University (Istanbul)", "University of Tokyo", "Kyoto University", "Osaka University", "Keio University", "Wako University", "Tohoku University", "Nagoya University", "Hokkaido University", "Kinki University", "Hitotsubashi University", "Chiba University",
                "Kyushu University", "Chuo University", "Shibaura Institute of Technology", "University of Tsukuba", "University of Helsinki", "Aalto University", "University of Turku", "University of Eastern Finland", "University of Oulu", "University of Tampere", "University of Lapland", "University of Jyväskylä",
                "Hanken School of Economics", "Lappeenranta-Lahti University of Technology (LUT)", "University of Vaasa", "Åbo Akademi University", "Tampere University of Technology", "Satakunta University of Applied Sciences", "Finnish Academy of Fine Arts", "Metropolia University of Applied Sciences"

            };
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter your ID or Email")
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
                textBox1.Text = "Enter your ID or Email";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                textBox1.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //button2.Visible = true;
            //button3.Visible=true;

            if(button2.Visible && button3.Visible)
            {
                button2.Visible = false;
                button3.Visible = false;

            }
            else
            {
                button2.Visible = true;
                button3.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            Forget forget = new Forget();
            forget.Show();
            
        }

        private void NewLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim(); // User can input ID, Email
            string pass = textBox2.Text.Trim();

            // Check correctness of provided info for login
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please enter ID or Email & Password",
                    "Validation Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query for URDummy
                string query1 = @"
            SELECT COUNT(*)
            FROM URDetails
            WHERE 
                (CAST(ID AS NVARCHAR) = @Input OR Email = @Input )
                AND Password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password";

                SqlCommand command1 = new SqlCommand(query1, connection);
                command1.Parameters.AddWithValue("@Input", input);
                command1.Parameters.AddWithValue("@Password", pass);

                int count1 = (int)command1.ExecuteScalar();

                // Query for StudentDetails
                string query2 = @"
            SELECT COUNT(*)
            FROM StudentDetails
            WHERE 
                (CAST(ID AS NVARCHAR) = @Input OR Email = @Input)
                AND Password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password";

                SqlCommand command2 = new SqlCommand(query2, connection);
                command2.Parameters.AddWithValue("@Input", input);
                command2.Parameters.AddWithValue("@Password", pass);

                int count2 = (int)command2.ExecuteScalar();

                string query3 = @"
            SELECT COUNT(*)
            FROM AdminDetails
            WHERE 
                (CAST(ID AS VARCHAR) = @Input OR Email = @Input )
                AND Password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password";

                SqlCommand command3 = new SqlCommand(query3, connection);
                command3.Parameters.AddWithValue("@Input", input);
                command3.Parameters.AddWithValue("@Password", pass);

                int count3 = (int)command3.ExecuteScalar();

                if (count1 > 0)
                {
                    //MessageBox.Show("Login successful as UR!");
                    MessageBox.Show("Login successful as University Representative!",
                    "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    URDashboard ad = new URDashboard(input);
                    ad.Show();
                }
                else if (count2 > 0)
                {
                    //MessageBox.Show("Login successful as Student!");
                    MessageBox.Show("Login successful as Student!",
                    "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    UserDashboard ad1 = new UserDashboard(input);
                    ad1.Show();
                }
                else if (count3 > 0)
                {
                    //MessageBox.Show("Login successful as Admin!");
                    MessageBox.Show("Login successful as Admin!",
                    "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    AdminDashboard ad1 = new AdminDashboard(input);
                    ad1.Show();
                }
                else
                {
                    // MessageBox.Show("Invalid ID/Email  or Password!");
                    MessageBox.Show("Invalid ID/Email  or Password!",
                     "Validation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = null;
                    textBox2.Text = null;
                }
                connection.Close();
            }
        

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            //if (textBox2.Text == " Password*")
            //{
            //    textBox2.Clear();
            //    textBox2.UseSystemPasswordChar = true;

            //}
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == " Password*")
            {
                textBox2.Text = "";
                // textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                textBox2.ForeColor = Color.Black; // Set text color for input
            }
            //textBox2.UseSystemPasswordChar = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = " Password*";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                textBox2.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            //if (textBox2.Text == " Password*")
            //{
            //    textBox2.Clear();
            //    textBox2.UseSystemPasswordChar = true;

            //}
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            //if (textBox2.Text == "")
            //{
            //    textBox2.Text = " Password*";
            //    //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
            //    //textBox2.ForeColor = Color.DimGray; // Set text color for input
            //    textBox2.UseSystemPasswordChar = false;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //panel1.Visible = true;
            //panel2.Visible = true;

            try
            {
                // Define the query to get the max value
                string query = "SELECT ISNULL(MAX(ID), 0) AS MaxValue FROM StudentDetails"; // Adjust table and column names

                // Connection string (adjust accordingly)
                string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and get the max value
                        object result = command.ExecuteScalar();
                        int maxValue = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                        // Calculate the new ID
                        int newID = maxValue + 1;

                        // Get the current date
                        DateTime currentDate = DateTime.Now;


                        // Display the new ID and date on the form
                        label18.Text = newID.ToString();
                        label19.Text = currentDate.ToString("dd-MMMM-yyyy HH:mm:ss");

                        // Hide other panels and show the relevant ones
                        //panel1.Visible = false;
                        //panel3.Visible = false;
                        //panel4.Visible = false;
                        //panel5.Visible = false;
                        //panel7.Visible = false;
                        //panel8.Visible = false;
                        //panel9.Visible = false;
                        //panel6.Visible = true;
                        //panel2.Visible = true;
                        panel3.Visible = false;
                        panel4.Visible = false;
                        panel1.Visible = true;
                        panel2.Visible = true;



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxNationality_Enter(object sender, EventArgs e)
        {
            if (comboBoxNationality.Text == "What's your country?")
            {
                comboBoxNationality.Text = "";
                //textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                comboBoxNationality.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void comboBoxNationality_Leave(object sender, EventArgs e)
        {
            if (comboBoxNationality.Text == "")
            {
                comboBoxNationality.Text = "What's your country?";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                comboBoxNationality.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Someone@example.com")
            {
                textBox4.Text = "";
                //textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                textBox4.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Someone@example.com";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                textBox4.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";
            string id = label18.Text.Trim();
            string name = textBox3.Text.Trim();
            string nationality = comboBoxNationality.Text.Trim();
            string gender = radioButton1.Checked ? radioButton1.Text :
            radioButton2.Checked ? radioButton2.Text : "None selected";
            string email = textBox4.Text.Trim();
            string age = textBox5.Text.Trim();
            string password = textBox6.Text.Trim();
            //string nationality = comboBox1.SelectedItem?.ToString();
            string cp = textBox7.Text.Trim();
            string regdate = label19.Text.Trim();
            //MessageBox.Show(applicationDeadline);
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) ||
               string.IsNullOrWhiteSpace(nationality) || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(age) ||
              string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(cp) || string.IsNullOrWhiteSpace(regdate))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(age, out int parsedAge))
            {
                MessageBox.Show("Age must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != cp)
            {
                MessageBox.Show("Please enter a valid Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO StudentDetails (Name,Nationality,Gender,Email,Age,Password) VALUES (@Name,@Nationality,@Gender,@Email,@Age,@Password)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Nationality", nationality);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Password", password);
                    //command.Parameters.AddWithValue("@RegDate", regdate);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Hide();
                        //Form1 f1 = new Form1();
                        //f1.Show();
                        panel1.Visible = false;
                        panel2.Visible = false;
                        button2.Visible = false;
                        button3.Visible = false;
                        panel3.Visible = false;
                        panel4.Visible = false;

                        //panel3.Visible = false;
                        //panel4.Visible = false;
                        //panel2.Visible = false;
                        //panel5.Visible = false;
                        //panel6.Visible = false;
                        //panel7.Visible = false;
                        //panel8.Visible = false;
                        //panel1.Visible = true;
                        //panel9.Visible = true;

                        textBox3.Text = null;
                        comboBoxNationality.Text = null;
                        textBox4.Text = null;
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        textBox5.Text = null;
                        textBox6.Text = null;
                        textBox7.Text = null;

                    }
                    else
                    {
                        MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void comboBoxNationality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBoxNationality.Items.Clear();
            comboBox1.Items.Clear();
            comboBoxNationality.Items.AddRange(countryList.ToArray());
            comboBox1.Items.AddRange(countryList.ToArray());
        }

        private void comboBox1_DropDown1(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(UniversityList.ToArray());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible=false;
            panel2.Visible=false;
            button2.Visible=false;
            button3.Visible=false;
            panel3.Visible = false;
            panel4.Visible = false;

            textBox3.Text = null;
            //comboBoxNationality.Text = null;
            //textBox4.Text = null;
            radioButton1.Checked =false;
            radioButton2.Checked = false;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the query to get the max value
                string query = "SELECT ISNULL(MAX(ID), 0) AS MaxValue FROM URDetails"; // Adjust table and column names

                // Connection string (adjust accordingly)
                string connectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and get the max value
                        object result = command.ExecuteScalar();
                        int maxValue = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                        // Calculate the new ID
                        int newID = maxValue + 1;

                        // Get the current date
                        DateTime currentDate = DateTime.Now;

                        // Display the new ID and date on the form
                        label32.Text = newID.ToString();
                        label33.Text = currentDate.ToString("dd-MMMM-yyyy HH:mm:ss");

                        // Hide other panels and show the relevant ones
                        panel1.Visible = false;
                       
                        panel2.Visible = false;
                        //panel9.Visible = false;
                        panel3.Visible = true;
                        panel4.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "  What's your country?")
            {
                comboBox1.Text = ""; // Clear placeholder text
                comboBox1.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Text = "  What's your country?"; // Clear placeholder text
                comboBox1.ForeColor = Color.Gray; // Set text color for input
            }
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == " Someone@example.com")
            {
                textBox9.Text = "";
                //textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                textBox9.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = " Someone@example.com";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                textBox9.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            if (comboBox2.Text == " Your university?")
            {
                comboBox2.Text = "";
                //textBox2.UseSystemPasswordChar = true;// Clear placeholder text
                comboBox2.ForeColor = Color.Black; // Set text color for input
            }
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                comboBox2.Text = " Your university?";
                //textBox2.UseSystemPasswordChar = false;// Clear placeholder text
                comboBox2.ForeColor = Color.DimGray; // Set text color for input
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            textBox8.Text = null;
           // comboBox1.Text = null;
            //textBox9.Text = null;
            //comboBox2.Text = null;
            textBox10.Text = null;
            textBox11.Text = null;
            textBox12.Text = null;
            textBox13.Text = null;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=SAIKOT;Initial Catalog=FINAL(DB);Integrated Security=True";
            string id = label32.Text.Trim();
            string name = textBox8.Text.Trim();
            string nationality = comboBox1.Text.Trim();
            string gender = radioButton3.Checked ? radioButton3.Text :
            radioButton4.Checked ? radioButton4.Text : "None selected";
            string email = textBox9.Text.Trim();
            string uniname = comboBox2.Text.Trim();
            string eiin = textBox10.Text.Trim();
            //string nationality = comboBox1.SelectedItem?.ToString();
            string password = textBox11.Text.Trim();
            string cp = textBox12.Text.Trim();
            string regdate = label33.Text.Trim();
            string urspecial = textBox13.Text.Trim();

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) ||
              string.IsNullOrWhiteSpace(nationality) || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(uniname) ||
              string.IsNullOrWhiteSpace(eiin) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(cp) ||
              string.IsNullOrWhiteSpace(regdate))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != cp)
            {
                MessageBox.Show("Please enter a valid Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO URDummy (ID,Name,Nationality,Gender,Email,UniversityName,EIIN,Password,RegDate,URspecial) VALUES (@ID,@Name,@Nationality,@Gender,@Email,@UniversityName,@EIIN,@Password,@RegDate,@URspecial)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Nationality", nationality);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@UniversityName", uniname);
                    command.Parameters.AddWithValue("@EIIN", eiin);
                    command.Parameters.AddWithValue("@Password", password);

                    command.Parameters.AddWithValue("@RegDate", regdate);
                    command.Parameters.AddWithValue("@URspecial", urspecial);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        panel1.Visible = false;
                        panel2.Visible = false;
                        button2.Visible = false;
                        button3.Visible = false;
                        panel3.Visible = false;
                        panel4.Visible = false;

                        textBox8.Text = null;
                        comboBox1.Text = null;
                        textBox9.Text = null;
                        comboBox2.Text = null;
                        textBox10.Text = null;
                        textBox11.Text = null;
                        textBox12.Text = null;
                        textBox13.Text = null;
                        radioButton3.Checked = false;
                        radioButton4.Checked = false;

                    }
                    else
                    {
                        MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    }
}

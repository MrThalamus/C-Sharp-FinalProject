using FinalProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(826, 450);
            this.Location = new Point(0, 0);
            progressBar1.Size = new Size(400, 15);
            progressBar1.Location = new Point(200, 370);
            label1.Size = new Size(39, 14);
            label1.Location = new Point(600, 370);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Enabled = true;

            if (progressBar1.Value + 3 >= 100)
            {
                progressBar1.Value = 100;
            }
            else
            {
                progressBar1.Increment(3);
            }

            label1.Text = progressBar1.Value.ToString() + "%";
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;

                Application.DoEvents();

                NewLogin login = new NewLogin();
                login.Show();
                this.Hide();
            }
        

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            // progressBar1.ForeColor = Color.SkyBlue;

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }
    }
}

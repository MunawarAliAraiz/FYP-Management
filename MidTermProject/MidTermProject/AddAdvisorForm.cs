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

namespace MidTermProject
{
    public partial class AddAdvisorForm : Form
    {
        public AddAdvisorForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private bool isValidAdv()
        {
            if (guna2TextBox1.Text == string.Empty)
            {
                MessageBox.Show("First Name is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox3.Text == string.Empty)
            {
                MessageBox.Show("Last Name is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox2.Text == string.Empty)
            {
                MessageBox.Show("Contact is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox2.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Contact Should be Number Only");
                    return false;
                }
            }
            if (guna2TextBox4.Text == string.Empty)
            {
                MessageBox.Show("Email is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Select Designation", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox5.Text == string.Empty)
            {
                MessageBox.Show("Salary is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox5.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Salary Should be Number Only");
                    return false;
                }
            }
            if (!guna2RadioButton1.Checked && !guna2RadioButton2.Checked)
            {
                MessageBox.Show("Select Gender", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private int getDesignation(String x)
        {
            if (x == "Professor")
                return 6;
            else if (x == "Associate Professor")
                return 7;
            else if (x == "Assisstant Professor")
                return 8;
            else if (x == "Lecturer")
                return 9;
            else if (x == "Industry Professional")
                return 10;
            return -1;
        }

        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void addAdvBtn_Click_1(object sender, EventArgs e)
        {
            if (isValidAdv())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Person values (@FirstName,@LastName, @Contact, @Email, @DateOfBirth, @Gender)", con);
                cmd.Parameters.AddWithValue("@FirstName", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", guna2TextBox3.Text);
                cmd.Parameters.AddWithValue("@Contact", guna2TextBox2.Text);
                cmd.Parameters.AddWithValue("@Email", guna2TextBox4.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", guna2DateTimePicker1.Text);
                if (guna2RadioButton1.Checked)
                    cmd.Parameters.AddWithValue("@Gender", 1);
                else
                    cmd.Parameters.AddWithValue("@Gender", 2);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(Id) from  Person", con);
                SqlDataReader Sdr = cmd1.ExecuteReader();
                Sdr.Read();
                int a = Sdr.GetInt32(0);
                Sdr.Close();
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("Insert into Advisor values (@Id, @Designation, @Salary)", con);
                cmd2.Parameters.AddWithValue("@Id", a);
                cmd2.Parameters.AddWithValue("@Designation", getDesignation(guna2ComboBox1.SelectedItem.ToString()));
                cmd2.Parameters.AddWithValue("@Salary", guna2TextBox5.Text);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
        }
    }
}

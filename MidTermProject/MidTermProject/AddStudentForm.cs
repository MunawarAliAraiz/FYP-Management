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
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddStudentBtn_Click(object sender, EventArgs e)
        {
            if(isValidStd())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Person values (@FirstName,@LastName, @Contact, @Email, @DateOfBirth, @Gender)", con); 
                cmd.Parameters.AddWithValue("@FirstName", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", guna2TextBox3.Text);
                cmd.Parameters.AddWithValue("@Contact", guna2TextBox2.Text);
                cmd.Parameters.AddWithValue("@Email", guna2TextBox5.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Text);
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
                SqlCommand cmd2 = new SqlCommand("Insert into Student values (@Id, @RegistrationNo)", con);
                cmd2.Parameters.AddWithValue("@Id", a);
                cmd2.Parameters.AddWithValue("@RegistrationNo", guna2TextBox4.Text);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
        }

        private bool isValidStd()
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
                MessageBox.Show("Registration No is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox4.Text != string.Empty)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select RegistrationNo from Student", con);
                SqlDataReader Sdr = cmd.ExecuteReader();
                while (Sdr.Read())
                {
                    for (int i = 0; i < Sdr.FieldCount; i++)
                    {
                        if (Sdr.GetString(i) == guna2TextBox4.Text)
                        {
                            MessageBox.Show("Registration Number is Already Present", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Sdr.Close();
                            return false;
                        }
                    }
                }
                Sdr.Close();
            }
            if (guna2TextBox5.Text == string.Empty)
            {
                MessageBox.Show("Name is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!guna2RadioButton1.Checked && !guna2RadioButton2.Checked)
            {
                MessageBox.Show("Select Gender", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}

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
    public partial class UpdateStudentForm : Form
    {
        public UpdateStudentForm()
        {
            InitializeComponent();
            showStdRegNo();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if (isValidStd())
            {
                if (guna2ComboBox1.SelectedItem.ToString() != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Email = @Email, DateOfBirth = @DateOfBirth, Gender = @Gender FROM Person AS P, Student AS S WHERE P.Id = S.Id AND S.RegistrationNo = @RegistrationNo", con);
                    cmd.Parameters.AddWithValue("@FirstName", guna2TextBox3.Text);
                    cmd.Parameters.AddWithValue("@LastName", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Contact", guna2TextBox4.Text);
                    cmd.Parameters.AddWithValue("@Email", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", guna2DateTimePicker1.Text);
                    if (guna2RadioButton1.Checked)
                        cmd.Parameters.AddWithValue("@Gender", 1);
                    else
                        cmd.Parameters.AddWithValue("@Gender", 2);
                    cmd.Parameters.AddWithValue("@RegistrationNo", guna2ComboBox1.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");
                    showStdRegNo();
                    clearForm();
                }
            }
        }

        private bool isValidStd()
        {
            if (guna2TextBox3.Text == string.Empty)
            {
                MessageBox.Show("First Name is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox2.Text == string.Empty)
            {
                MessageBox.Show("Last Name is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox4.Text == string.Empty)
            {
                MessageBox.Show("Contact is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox4.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Contact Should be Number Only");
                    return false;
                }
            }
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please Select Registration No.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox5.Text == string.Empty)
            {
                MessageBox.Show("Email is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!guna2RadioButton1.Checked && !guna2RadioButton2.Checked)
            {
                MessageBox.Show("Select Gender", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void showStdRegNo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT RegistrationNo FROM Student", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox1.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox1.Items.Add(Sdr.GetString(i));
                }
            }
            Sdr.Close();
        }

        private void clearForm()
        {
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2DateTimePicker1.Text = DateTime.Now.ToString();
            guna2RadioButton1.Controls.Clear();
            guna2RadioButton2.Controls.Clear();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender FROM Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE S.RegistrationNo = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            if (Sdr.HasRows)
            {
                while (Sdr.Read())
                {
                    guna2TextBox3.Text = Sdr[0].ToString();
                    guna2TextBox2.Text = Sdr[1].ToString();
                    guna2TextBox4.Text = Sdr[2].ToString();
                    guna2TextBox5.Text = Sdr[3].ToString();
                    guna2DateTimePicker1.Text = Sdr[4].ToString();
                    String a = Sdr[5].ToString();
                    if (a == "Male")
                        guna2RadioButton1.Select();
                    else if (a == "Female")
                        guna2RadioButton2.Select();
                }
            }
            Sdr.Close();
        }
    }

}

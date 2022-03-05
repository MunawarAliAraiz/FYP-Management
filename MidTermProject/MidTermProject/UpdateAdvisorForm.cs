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
    public partial class UpdateAdvisorForm : Form
    {
        public UpdateAdvisorForm()
        {
            InitializeComponent();
            showAdvId();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if (isValidAdv())
            {
                if (guna2ComboBox1.SelectedItem.ToString() != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Email = @Email, DateOfBirth = @DateOfBirth, Gender = @Gender FROM Person AS P, Advisor AS A WHERE P.Id = A.Id AND A.Id = @Id", con);
                    SqlCommand cmd1 = new SqlCommand("UPDATE Advisor SET Designation = @Designation, Salary = @Salary FROM Person AS P, Advisor AS A WHERE P.Id = A.Id AND A.Id = @Id", con);
                    cmd.Parameters.AddWithValue("@FirstName", guna2TextBox3.Text);
                    cmd.Parameters.AddWithValue("@LastName", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Contact", guna2TextBox4.Text);
                    cmd.Parameters.AddWithValue("@Email", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", guna2DateTimePicker1.Text);
                    if (guna2RadioButton1.Checked)
                        cmd.Parameters.AddWithValue("@Gender", 1);
                    else
                        cmd.Parameters.AddWithValue("@Gender", 2);
                    cmd1.Parameters.AddWithValue("@Designation", getDesignation(guna2ComboBox2.SelectedItem.ToString()));
                    cmd1.Parameters.AddWithValue("@Salary", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@Id", guna2ComboBox1.SelectedItem.ToString());
                    cmd1.Parameters.AddWithValue("@Id", guna2ComboBox1.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");
                    showAdvId();
                    clearForm();
                }
            }
        }

        private bool isValidAdv()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select Advisor ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
            if (guna2TextBox5.Text == string.Empty)
            {
                MessageBox.Show("Email is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Select Designation", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox1.Text == string.Empty)
            {
                MessageBox.Show("Salary is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox1.Text);
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

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender Where A.Id = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
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
                    else if(a == "Female")
                        guna2RadioButton2.Select();
                    guna2ComboBox2.Text = Sdr[6].ToString();
                    guna2TextBox1.Text = Sdr[7].ToString();
                }
            }
            Sdr.Close();
        }

        private void showAdvId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Advisor", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox1.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox1.Items.Add(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
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

        private void clearForm()
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2DateTimePicker1.Text = DateTime.Now.ToString();
            guna2RadioButton1.Controls.Clear();
            guna2RadioButton2.Controls.Clear();
            guna2ComboBox2.ResetText();

        }
    }
}

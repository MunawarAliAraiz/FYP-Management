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
    public partial class DeleteAdvisorForm : Form
    {
        public DeleteAdvisorForm()
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
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select Advisor ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE Advisor WHERE Id = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
                SqlCommand cmd1 = new SqlCommand("DELETE Person WHERE Id = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                showAdvId();
                clearForm();
            }
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
                    else if (a == "Female")
                        guna2RadioButton2.Select();
                    guna2ComboBox2.Text = Sdr[6].ToString();
                    guna2TextBox1.Text = Sdr[7].ToString();
                }
            }
            Sdr.Close();
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

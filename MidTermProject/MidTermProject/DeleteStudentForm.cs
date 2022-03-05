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
    public partial class DeleteStudentForm : Form
    {
        public DeleteStudentForm()
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

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select Advisor ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT S.Id from  Person AS P JOIN Student AS S ON S.Id = P.Id AND S.RegistrationNo = '"+ guna2ComboBox1.SelectedItem.ToString()+"'", con);
                SqlDataReader Sdr = cmd.ExecuteReader();
                Sdr.Read();
                int a = Sdr.GetInt32(0);
                Sdr.Close();
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("DELETE Student WHERE RegistrationNo = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
                SqlCommand cmd2 = new SqlCommand("DELETE Person WHERE Id = '" + a.ToString() + "'", con);
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                showStdRegNo();
                clearForm();
            }
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
    }
}

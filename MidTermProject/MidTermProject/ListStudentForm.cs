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
    public partial class ListStudentForm : Form
    {
        public ListStudentForm()
        {
            InitializeComponent();
            ShowStudentData();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        void ShowStudentData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            studentGrid.DataSource = dt;
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (guna2ComboBox3.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE S.RegistrationNo LIKE '%" + guna2TextBox7.Text + "%' OR P.FirstName LIKE '%" + guna2TextBox7.Text + "%' OR P.LastName LIKE '%" + guna2TextBox7.Text + "%' OR P.Contact LIKE '%" + guna2TextBox7.Text + "%' OR P.Email LIKE '%" + guna2TextBox7.Text + "%' OR P.DateOfBirth LIKE '%" + guna2TextBox7.Text + "%' OR L.Value LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE S.RegistrationNo LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE P.FirstName LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE P.LastName LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 4)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE P.Contact LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 5)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE P.Email LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 6)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE P.DateOfBirth LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 7)
            {
                SqlCommand cmd = new SqlCommand("Select S.RegistrationNo,P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L.Value AS Gender from Student AS S INNER JOIN Person AS P ON P.Id = S.Id INNER JOIN Lookup AS L ON L.Id = P.Gender WHERE L.Value LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentGrid.DataSource = dt;
            }
        }
    }
}

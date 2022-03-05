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
    public partial class ListAdvisorForm : Form
    {
        public ListAdvisorForm()
        {
            InitializeComponent();
            ShowAdvisorData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        void ShowAdvisorData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            advisorGrid.DataSource = dt;
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (guna2ComboBox3.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE A.Id LIKE '%" + guna2TextBox7.Text + "%' OR P.FirstName LIKE '%" + guna2TextBox7.Text + "%' OR P.LastName LIKE '%" + guna2TextBox7.Text + "%' OR P.Contact LIKE '%" + guna2TextBox7.Text + "%' OR P.Email LIKE '%" + guna2TextBox7.Text + "%' OR P.DateOfBirth LIKE '%" + guna2TextBox7.Text + "%' OR L2.Value LIKE '%" + guna2TextBox7.Text + "%' OR L1.Value LIKE '%" + guna2TextBox7.Text + "%' OR A.Salary LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE A.Id LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE P.FirstName LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE P.LastName LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 4)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE P.Contact LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 5)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE P.Email LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 6)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE P.DateOfBirth LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 7)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE L2.Value LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 8)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE L1.Value LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 9)
            {
                SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender WHERE A.Salary LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
        }
    }
}

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
    public partial class AssignAdvisorForm : Form
    {
        public AssignAdvisorForm()
        {
            InitializeComponent();
            ShowAdvisorData();
            ProAdvId();
            AdvId();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ShowAdvisorData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            advisorGrid.DataSource = dt;
        }

        private void ProAdvId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Project", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox5.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox5.Items.Add(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
            SqlCommand cmd1 = new SqlCommand("SELECT ProjectId FROM ProjectAdvisor", con);
            SqlDataReader Sdr1 = cmd1.ExecuteReader();
            while (Sdr1.Read())
            {
                for (int i = 0; i < Sdr1.FieldCount; i++)
                {
                    guna2ComboBox5.Items.Remove(Sdr1.GetInt32(i));
                }
            }
            Sdr1.Close();
            SqlCommand cmd3 = new SqlCommand("SELECT AdvisorId FROM ProjectAdvisor", con);
            SqlDataReader Sdr3 = cmd3.ExecuteReader();
            guna2ComboBox7.Items.Clear();
            while (Sdr3.Read())
            {
                for (int i = 0; i < Sdr3.FieldCount; i++)
                {
                    guna2ComboBox7.Items.Add(Sdr3.GetInt32(i));
                }
            }
            Sdr3.Close();
        }

        private void AdvId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Advisor", con);
            SqlDataReader Sdr2 = cmd2.ExecuteReader();
            guna2ComboBox1.Items.Clear();
            while (Sdr2.Read())
            {
                for (int i = 0; i < Sdr2.FieldCount; i++)
                {
                    guna2ComboBox1.Items.Add(Sdr2.GetInt32(i));
                }
            }
            Sdr2.Close();
        }

        private void guna2ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT ProjectId FROM ProjectAdvisor WHERE AdvisorId = '" + guna2ComboBox7.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox2.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox2.Items.Add(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
            guna2ComboBox2.Enabled = true;
            guna2ComboBox6.Enabled = true;
            guna2Button3.Enabled = true;
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Project", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox5.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox5.Items.Add(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
            SqlCommand cmd1 = new SqlCommand("SELECT ProjectId FROM ProjectAdvisor WHERE AdvisorId = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr1 = cmd1.ExecuteReader();
            while (Sdr1.Read())
            {
                for (int i = 0; i < Sdr1.FieldCount; i++)
                {
                    guna2ComboBox5.Items.Remove(Sdr1.GetInt32(i));
                }
            }
            Sdr1.Close();
            guna2ComboBox5.Enabled = true;
            guna2ComboBox4.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (isValidAddEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into ProjectAdvisor values (@AdvisorId, @ProjectId, @AdvisorRole, @AssignmentDate)", con);
                cmd.Parameters.AddWithValue("@AdvisorId", guna2ComboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ProjectId", guna2ComboBox5.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@AdvisorRole", getAdvRole(guna2ComboBox4.SelectedItem.ToString()));
                cmd.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Assigned");
                ShowAdvisorData();
                ProAdvId();
                AdvId();
                clearForm();
            }
        }

        private int getAdvRole(String a)
        {
            if (a == "Main Advisor")
            {
                return 11;
            }    
            else if (a == "Co-Advisror")
            {
                return 12;
            }
            else if (a == "Industry Advisor")
            {
                return 14;
            }
            return -1;
        }

        private String getAdvRole1(int a)
        {
            if (a == 11)
            {
                return "Main Advisor";
            }
            else if (a == 12)
            {
                return "Co-Advisror";
            }
            else if (a == 14)
            {
                return "Industry Advisor";
            }
            return "";
        }

        private void clearForm()
        {
            guna2ComboBox5.ResetText();
            guna2ComboBox1.ResetText();
            guna2ComboBox2.ResetText();
            guna2ComboBox7.ResetText();
            guna2ComboBox4.ResetText();
            guna2ComboBox6.ResetText();
        }

        private bool isValidAddEva()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select Advisor ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox5.SelectedItem == null)
            {
                MessageBox.Show("Select Project ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox4.SelectedItem == null)
            {
                MessageBox.Show("Select Advisor Role", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool isValidDelEva()
        {
            if (guna2ComboBox7.SelectedItem == null)
            {
                MessageBox.Show("Select Advisor ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox2.SelectedItem == null)
            {
                MessageBox.Show("Select Project ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox6.SelectedItem == null)
            {
                MessageBox.Show("Select Advisor Role", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (isValidDelEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE Projectadvisor WHERE AdvisorId = '" + guna2ComboBox7.SelectedItem.ToString() + "' AND ProjectId = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                ShowAdvisorData();
                ProAdvId();
                AdvId();
                clearForm();
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("SELECT AdvisorRole FROM ProjectAdvisor WHERE AdvisorId = '" + guna2ComboBox7.SelectedItem.ToString() + "' AND ProjectId = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr1 = cmd1.ExecuteReader();
            guna2ComboBox6.Items.Clear();
            while (Sdr1.Read())
            {
                for (int i = 0; i < Sdr1.FieldCount; i++)
                {
                    guna2ComboBox6.SelectedIndex  = guna2ComboBox6.Items.IndexOf(getAdvRole1((Sdr1.GetInt32(i))));
                }
            }
            Sdr1.Close();
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (guna2ComboBox3.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id WHERE P.Id LIKE '%" + guna2TextBox7.Text + "%' OR P.Title LIKE '%" + guna2TextBox7.Text + "%' OR PA.AdvisorId LIKE '%" + guna2TextBox7.Text + "%' OR per.FirstName LIKE '%" + guna2TextBox7.Text + "%' OR per.LastName LIKE '%" + guna2TextBox7.Text + "%' OR L.Value LIKE '%" + guna2TextBox7.Text + "%' OR PA.AssignmentDate LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id WHERE P.Id LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id WHERE P.Title LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id WHERE PA.AdvisorId LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 4)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id WHERE per.FirstName LIKE '%" + guna2TextBox7.Text + "%' OR per.LastName LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 5)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id WHERE L.Value LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 6)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, PA.AdvisorId, per.FirstName+''+per.LastName AS Name, L.Value AS 'Advisor Role',PA.AssignmentDate FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = PA.ProjectId JOIN Person AS per ON PA.AdvisorId = per.Id JOIN Lookup AS L ON PA.AdvisorRole = L.Id WHERE PA.AssignmentDate LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                advisorGrid.DataSource = dt;
            }
        }
    }
}

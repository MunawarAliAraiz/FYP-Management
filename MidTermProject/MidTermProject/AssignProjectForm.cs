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
    public partial class AssignProjectForm : Form
    {
        public AssignProjectForm()
        {
            InitializeComponent();
            ShowProjectData();
            GrpProId();
            GrpId();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        void ShowProjectData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Id AS ProjectID, P.Title, GP.GroupId,GP.AssignmentDate FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            projectGrid.DataSource = dt;
        }

        private void GrpProId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM [Group]", con);
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
            SqlCommand cmd1 = new SqlCommand("SELECT GroupId FROM GroupProject", con);
            SqlDataReader Sdr1 = cmd1.ExecuteReader();
            while (Sdr1.Read())
            {
                for (int i = 0; i < Sdr1.FieldCount; i++)
                {
                    guna2ComboBox1.Items.Remove(Sdr1.GetInt32(i));
                }
            }
            Sdr1.Close();
            SqlCommand cmd3 = new SqlCommand("SELECT Id FROM Project", con);
            SqlDataReader Sdr3 = cmd3.ExecuteReader();
            guna2ComboBox5.Items.Clear();
            guna2ComboBox2.Items.Clear();
            while (Sdr3.Read())
            {
                for (int i = 0; i < Sdr3.FieldCount; i++)
                {
                    guna2ComboBox5.Items.Add(Sdr3.GetInt32(i));
                    guna2ComboBox2.Items.Add(Sdr3.GetInt32(i));
                }
            }
            Sdr3.Close();
        }

        private void GrpId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("SELECT GroupId FROM GroupProject", con);
            SqlDataReader Sdr2 = cmd2.ExecuteReader();
            guna2ComboBox7.Items.Clear();
            while (Sdr2.Read())
            {
                for (int i = 0; i < Sdr2.FieldCount; i++)
                {
                    guna2ComboBox7.Items.Add(Sdr2.GetInt32(i));
                }
            }
            Sdr2.Close();
        }

        private void ProId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT ProjectId FROM GroupProject WHERE GroupId = '"+ guna2ComboBox7.SelectedItem.ToString()+ "'", con);
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
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBox5.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void guna2ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProId();
            guna2ComboBox2.Enabled = true;
            guna2Button3.Enabled = true;
        }

        private void clearForm()
        { 
            guna2ComboBox5.ResetText();
            guna2ComboBox1.ResetText();
            guna2ComboBox2.ResetText();
            guna2ComboBox7.ResetText();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (isValidAddEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into GroupProject values (@ProjectId, @GroupId, @AssignmentDate)", con);
                cmd.Parameters.AddWithValue("@ProjectId", guna2ComboBox5.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@GroupId", guna2ComboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Assigned");
                ShowProjectData();
                clearForm();
                GrpProId();
                GrpId();
            }
        }

        private bool isValidAddEva()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select Group ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox5.SelectedItem == null)
            {
                MessageBox.Show("Select Project ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool isValidDelEva()
        {
            if (guna2ComboBox7.SelectedItem == null)
            {
                MessageBox.Show("Select Group ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox2.SelectedItem == null)
            {
                MessageBox.Show("Select Project ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (isValidDelEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE GroupProject WHERE GroupId = '" + guna2ComboBox7.SelectedItem.ToString() + "' AND ProjectId = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                ShowProjectData();
                clearForm();
                GrpProId();
                GrpId();
            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (guna2ComboBox3.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, GP.GroupId,GP.AssignmentDate FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId WHERE P.Id LIKE '%" + guna2TextBox7.Text + "%' OR P.Title LIKE '%" + guna2TextBox7.Text + "%' OR GP.GroupId LIKE '%" + guna2TextBox7.Text + "%' OR GP.AssignmentDate LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, GP.GroupId,GP.AssignmentDate FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId WHERE P.Id LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, GP.GroupId,GP.AssignmentDate FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId WHERE P.Title LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, GP.GroupId,GP.AssignmentDate FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId WHERE GP.GroupId LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 4)
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id, P.Title, GP.GroupId,GP.AssignmentDate FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId WHERE GP.AssignmentDate LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
        }
    }
}

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
    public partial class ProjectForm : Form
    {
        public ProjectForm()
        {
            InitializeComponent();
            ShowProjectData();
            showProId();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        void ShowProjectData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * From Project", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            projectGrid.DataSource = dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Add Project
        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if (isValidAddPro())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Project values (@Description, @Title)", con);
                cmd.Parameters.AddWithValue("@Description", guna2TextBox3.Text);
                cmd.Parameters.AddWithValue("@Title", guna2TextBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
                ShowProjectData();
                clearForm();
                showProId();
            }
        }

        //Validation for Add Project
        private bool isValidAddPro()
        {
            if (guna2TextBox1.Text == string.Empty)
            {
                MessageBox.Show("Title is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox3.Text == string.Empty)
            {
                MessageBox.Show("Description is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //Validation for Delete Project
        private bool isValidDelPro()
        {
            if (guna2ComboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please Select Project ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox6.Text == string.Empty)
            {
                MessageBox.Show("Title is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox5.Text == string.Empty)
            {
                MessageBox.Show("Description is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //Validation for Update Project
        private bool isValidUpdPro()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please Select Project ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox4.Text == string.Empty)
            {
                MessageBox.Show("Title is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox2.Text == string.Empty)
            {
                MessageBox.Show("Description is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void showProId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Project", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox1.Items.Clear();
            guna2ComboBox2.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox1.Items.Add(Sdr.GetInt32(i));
                    guna2ComboBox2.Items.Add(Sdr.GetInt32(i));
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
            guna2TextBox6.Text = "";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (isValidUpdPro())
            {
                if (guna2ComboBox1.SelectedItem.ToString() != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Project SET Description = @Description, Title = @Title WHERE Id = '"+guna2ComboBox1.SelectedItem.ToString()+"'", con);
                    cmd.Parameters.AddWithValue("@Description", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Title", guna2TextBox4.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");
                    ShowProjectData();
                    clearForm();
                    showProId();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (isValidDelPro())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE Project WHERE Id = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                ShowProjectData();
                clearForm();
                showProId();

            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Title, Description FROM Project WHERE Id = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            if (Sdr.HasRows)
            {
                while (Sdr.Read())
                {
                    guna2TextBox4.Text = Sdr[0].ToString();
                    guna2TextBox2.Text = Sdr[1].ToString();
                }
            }
            Sdr.Close();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Title, Description FROM Project WHERE Id = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            if (Sdr.HasRows)
            {
                while (Sdr.Read())
                {
                    guna2TextBox6.Text = Sdr[0].ToString();
                    guna2TextBox5.Text = Sdr[1].ToString();
                }
            }
            Sdr.Close();
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            
            if (guna2ComboBox3.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Select * From Project WHERE ID LIKE '%"+guna2TextBox7.Text+"%' OR Title LIKE '%"+ guna2TextBox7 .Text+ "%' OR Description LIKE '%"+ guna2TextBox7.Text+ "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
            else if(guna2ComboBox3.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("Select * From Project WHERE ID LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("Select * From Project WHERE Title LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("Select * From Project WHERE Description LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                projectGrid.DataSource = dt;
            }
        }
    }
    
}

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
    public partial class ManageEvaluationForm : Form
    {
        public ManageEvaluationForm()
        {
            InitializeComponent();
            ShowEvaluationData();
            showEvaId();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ShowEvaluationData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * From Evaluation", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            evaluationGrid.DataSource = dt;
        }

        private void showEvaId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Evaluation", con);
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

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if (isValidAddEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Evaluation values (@Name, @TotalMarks, @TotalWeightage)", con);
                cmd.Parameters.AddWithValue("@Name", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", guna2TextBox2.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", guna2TextBox3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Added");
                ShowEvaluationData();
                clearForm();
                showEvaId();
            }
        }

        //Validation for Add Evaluation
        private bool isValidAddEva()
        {
            if (guna2TextBox1.Text == string.Empty)
            {
                MessageBox.Show("Name is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox2.Text == string.Empty)
            {
                MessageBox.Show("Total Marks is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox2.Text);
                }
                catch(Exception)
                {
                    MessageBox.Show("Total Marks Should be Number Only");
                    return false;
                }
            }
            if (guna2TextBox3.Text == string.Empty)
            {
                MessageBox.Show("Weightage is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox3.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Weightage Should be Number Only");
                    return false;
                }
            }
            return true;
        }

        private void clearForm()
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
            guna2TextBox8.Text = "";
            guna2TextBox9.Text = "";
            guna2TextBox10.Text = "";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (isValidUpdEva())
            {
                if (guna2ComboBox1.SelectedItem.ToString() != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET Name = @Name, TotalMarks = @TotalMarks, TotalWeightage = @TotalWeightage WHERE Id = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@Name", guna2TextBox4.Text);
                    cmd.Parameters.AddWithValue("@TotalMarks", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@TotalWeightage", guna2TextBox6.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");
                    ShowEvaluationData();
                    clearForm();
                    showEvaId();
                }
            }
        }

        //Validation for Update Evaluation
        private bool isValidUpdEva()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please Select Evaluation ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox4.Text == string.Empty)
            {
                MessageBox.Show("Name is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox5.Text == string.Empty)
            {
                MessageBox.Show("Total Marks is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox5.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Total Marks Should be Number Only");
                    return false;
                }
            }
            if (guna2TextBox6.Text == string.Empty)
            {
                MessageBox.Show("Weightage is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(guna2TextBox6.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Weightage Should be Number Only");
                    return false;
                }
            }
            return true;
        }

        private bool isValidDelEva()
        {
            if (guna2ComboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please Select Evaluation ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (isValidDelEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE Evaluation WHERE Id = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                ShowEvaluationData();
                clearForm();
                showEvaId();

            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (guna2ComboBox3.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Select * From Evaluation WHERE Id LIKE '%" + guna2TextBox7.Text + "%' OR Name LIKE '%" + guna2TextBox7.Text + "%' OR TotalMarks LIKE '%" + guna2TextBox7.Text + "%' OR TotalWeightage LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("Select * From Evaluation WHERE Id LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("Select * From Evaluation WHERE Name LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("Select * From Evaluation WHERE TotalMarks LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid.DataSource = dt;
            }
            else if (guna2ComboBox3.SelectedIndex == 4)
            {
                SqlCommand cmd = new SqlCommand("Select * From Evaluation WHERE TotalWeightage LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid.DataSource = dt;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Name, TotalMarks, TotalWeightage FROM Evaluation WHERE Id = '" + guna2ComboBox1.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            if (Sdr.HasRows)
            {
                while (Sdr.Read())
                {
                    guna2TextBox4.Text = Sdr[0].ToString();
                    guna2TextBox5.Text = Sdr[1].ToString();
                    guna2TextBox6.Text = Sdr[2].ToString();
                }
            }
            Sdr.Close();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Name, TotalMarks, TotalWeightage FROM Evaluation WHERE Id = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            if (Sdr.HasRows)
            {
                while (Sdr.Read())
                {
                    guna2TextBox8.Text = Sdr[0].ToString();
                    guna2TextBox9.Text = Sdr[1].ToString();
                    guna2TextBox10.Text = Sdr[2].ToString();
                }
            }
            Sdr.Close();
        }
    }
}

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
    public partial class MarkEvaluationForm : Form
    {
        public MarkEvaluationForm()
        {
            InitializeComponent();
            ShowEvaluationData();
            showGrpEvaId();
            showGrpId();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ShowEvaluationData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * FROM Evaluation", con);
            SqlCommand cmd1 = new SqlCommand("Select * FROM GroupEvaluation", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            evaluationGrid.DataSource = dt;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            evaluationGrid2.DataSource = dt1;
        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if (isValidAddEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into GroupEvaluation values (@GroupId, @EvaluationId, @ObtainedMarks, @EvaluationDate)", con);
                cmd.Parameters.AddWithValue("@GroupId", guna2ComboBox4.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EvaluationId", guna2ComboBox5.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ObtainedMarks", guna2TextBox3.Text);
                cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Today.ToShortDateString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Marked");
                ShowEvaluationData();
                clearForm();
                showGrpEvaId();
            }
        }

        private void showGrpEvaId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT(GroupId) FROM GroupEvaluation", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox1.Items.Clear();
            guna2ComboBox2.Items.Clear();
            guna2ComboBox5.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox1.Items.Add(Sdr.GetInt32(i));
                    guna2ComboBox2.Items.Add(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
            
            SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Evaluation", con);
            SqlDataReader Sdr2 = cmd2.ExecuteReader();
            while (Sdr2.Read())
            {
                for (int i = 0; i < Sdr2.FieldCount; i++)
                {
                    guna2ComboBox5.Items.Add(Sdr2.GetInt32(i));
                }
            }
            Sdr2.Close();
        }

        private void showGrpId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("SELECT Id FROM [Group]", con);
            SqlDataReader Sdr1 = cmd1.ExecuteReader();
            guna2ComboBox4.Items.Clear();
            while (Sdr1.Read())
            {
                for (int i = 0; i < Sdr1.FieldCount; i++)
                {
                    guna2ComboBox4.Items.Add(Sdr1.GetInt32(i));
                }
            }
            Sdr1.Close();
        }

        private void showEvaIdUpd()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT EvaluationId FROM GroupEvaluation WHERE GroupId = '"+guna2ComboBox1.SelectedItem.ToString()+"'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox6.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox6.Items.Add(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
        }

        private void showEvaIdDel()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT EvaluationId FROM GroupEvaluation WHERE GroupId = '" + guna2ComboBox2.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            guna2ComboBox7.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox7.Items.Add(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
        }

        private void clearForm()
        {
            guna2TextBox3.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox9.Text = "";
            guna2ComboBox4.ResetText();
            guna2ComboBox5.ResetText();
            guna2ComboBox1.ResetText();
            guna2ComboBox6.ResetText();
            guna2ComboBox2.ResetText();
            guna2ComboBox7.ResetText();
        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            showGrpEvaId();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT EvaluationId FROM GroupEvaluation WHERE GroupId = '"+guna2ComboBox4.SelectedItem.ToString()+"'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    guna2ComboBox5.Items.Remove(Sdr.GetInt32(i));
                }
            }
            Sdr.Close();
            guna2ComboBox5.Enabled = true;
            guna2TextBox3.Enabled = true;
            guna2Button24.Enabled = true;
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showEvaIdUpd();
            guna2ComboBox6.Enabled = true;
            guna2TextBox5.Enabled = true;
            guna2Button2.Enabled = true;
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            showEvaIdDel();
            guna2ComboBox7.Enabled = true;
            guna2TextBox9.Enabled = true;
            guna2Button3.Enabled = true;
        }

        //Validation for Mark Evaluation
        private bool isValidAddEva()
        {
            if (guna2ComboBox4.SelectedItem == null)
            {
                MessageBox.Show("Select Group ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox5.SelectedItem == null)
            {
                MessageBox.Show("Select Evaluation ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox3.Text == string.Empty)
            {
                MessageBox.Show("Obtained Marks is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Obtained Marks Should be Number Only");
                    return false;
                }
            }
            return true;
        }

        //Validation for Update Evaluation
        private bool isValidUpdEva()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select Group ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox6.SelectedItem == null)
            {
                MessageBox.Show("Select Evaluation ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox5.Text == string.Empty)
            {
                MessageBox.Show("Obtained Marks is Empty", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Obtained Marks Should be Number Only");
                    return false;
                }
            }
            return true;
        }

        //Validation for Delete Evaluation
        private bool isValidDelEva()
        {
            if (guna2ComboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please Select Group ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2ComboBox7.SelectedItem == null)
            {
                MessageBox.Show("Please Select Evaluation ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (isValidUpdEva())
            {
                if (guna2ComboBox1.SelectedItem.ToString() != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE GroupEvaluation SET GroupId = @GroupId, EvaluationId = @EvaluationId, ObtainedMarks = @ObtainedMarks, EvaluationDate = @EvaluationDate WHERE GroupId = '" + guna2ComboBox1.SelectedItem.ToString() + "' AND EvaluationId = '"+ guna2ComboBox6.SelectedItem.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@GroupId", guna2ComboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EvaluationId", guna2ComboBox6.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ObtainedMarks", guna2TextBox5.Text);
                    cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Today.ToShortDateString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");
                    ShowEvaluationData();
                    clearForm();
                    showGrpEvaId();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (isValidDelEva())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE GroupEvaluation WHERE GroupId = '" + guna2ComboBox2.SelectedItem.ToString() + "' AND EvaluationId = '"+ guna2ComboBox7.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                ShowEvaluationData();
                clearForm();
                showGrpEvaId();

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
            if (guna2ComboBox8.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Select * From GroupEvaluation WHERE GroupId LIKE '%" + guna2TextBox7.Text + "%' OR EvaluationId LIKE '%" + guna2TextBox7.Text + "%' OR ObtainedMarks LIKE '%" + guna2TextBox7.Text + "%' OR EvaluationDate LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid2.DataSource = dt;
            }
            else if (guna2ComboBox8.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("Select * From GroupEvaluation WHERE GroupId LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid2.DataSource = dt;
            }
            else if (guna2ComboBox8.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("Select * From GroupEvaluation WHERE EvaluationId LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid2.DataSource = dt;
            }
            else if (guna2ComboBox8.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("Select * From GroupEvaluation WHERE ObtainedMarks LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid2.DataSource = dt;
            }
            else if (guna2ComboBox8.SelectedIndex == 4)
            {
                SqlCommand cmd = new SqlCommand("Select * From GroupEvaluation WHERE EvaluationDate LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                evaluationGrid2.DataSource = dt;
            }
        }
    }
}

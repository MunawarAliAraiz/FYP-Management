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
    public partial class CreateGroup : Form
    {
        public CreateGroup()
        {
            InitializeComponent();
            ShowGroupData();
            hideMembersText();
            showStdRegNo();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hideMembersText()
        {
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            guna2ComboBox2.Visible = false;
            guna2ComboBox3.Visible = false;
            guna2ComboBox4.Visible = false;
            guna2ComboBox5.Visible = false;
            guna2ComboBox6.Visible = false;
            guna2ComboBox7.Visible = false;
            guna2ComboBox8.Visible = false;
            guna2ComboBox9.Visible = false;
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hideMembersText();
            if (guna2ComboBox1.SelectedIndex == 0)
            {
                label3.Visible = true;
                label4.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                guna2ComboBox2.Visible = true;
                guna2ComboBox3.Visible = true;
                guna2ComboBox6.Visible = true;
                guna2ComboBox7.Visible = true;
            }
            else if (guna2ComboBox1.SelectedIndex == 1)
            {
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                guna2ComboBox2.Visible = true;
                guna2ComboBox3.Visible = true;
                guna2ComboBox4.Visible = true;
                guna2ComboBox6.Visible = true;
                guna2ComboBox7.Visible = true;
                guna2ComboBox8.Visible = true;
            }
            else if (guna2ComboBox1.SelectedIndex == 2)
            {
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                guna2ComboBox2.Visible = true;
                guna2ComboBox3.Visible = true;
                guna2ComboBox4.Visible = true;
                guna2ComboBox5.Visible = true;
                guna2ComboBox6.Visible = true;
                guna2ComboBox7.Visible = true;
                guna2ComboBox8.Visible = true;
                guna2ComboBox9.Visible = true;
            }
        }

        private void showStdRegNo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId WHERE G.GroupId IS NULL", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    if(!guna2ComboBox2.Items.Contains(Sdr.GetString(i)))
                    {
                        guna2ComboBox2.Items.Add(Sdr.GetString(i));
                    }
                    if (!guna2ComboBox3.Items.Contains(Sdr.GetString(i)))
                    {
                        guna2ComboBox3.Items.Add(Sdr.GetString(i));
                    }
                    if (!guna2ComboBox4.Items.Contains(Sdr.GetString(i)))
                    {
                        guna2ComboBox4.Items.Add(Sdr.GetString(i));
                    }
                    if (!guna2ComboBox5.Items.Contains(Sdr.GetString(i)))
                    {
                        guna2ComboBox5.Items.Add(Sdr.GetString(i));
                    }
                }
            }
            Sdr.Close();
        }

        private void validateGroup()
        {
            showStdRegNo();

            //Removing Selected Members of ComboBox 2 From Other ComboBoxes
            guna2ComboBox3.Items.Remove(guna2ComboBox2.SelectedItem);
            guna2ComboBox4.Items.Remove(guna2ComboBox2.SelectedItem);
            guna2ComboBox5.Items.Remove(guna2ComboBox2.SelectedItem);

            //Removing Selected Members of ComboBox 3 From Other ComboBoxes
            guna2ComboBox2.Items.Remove(guna2ComboBox3.SelectedItem);
            guna2ComboBox4.Items.Remove(guna2ComboBox3.SelectedItem);
            guna2ComboBox5.Items.Remove(guna2ComboBox3.SelectedItem);

            //Removing Selected Members of ComboBox 4 From Other ComboBoxes
            if (guna2ComboBox1.SelectedIndex == 1)
            {
                guna2ComboBox2.Items.Remove(guna2ComboBox4.SelectedItem);
                guna2ComboBox3.Items.Remove(guna2ComboBox4.SelectedItem);
                guna2ComboBox5.Items.Remove(guna2ComboBox4.SelectedItem);
            }

            //Removing Selected Members of ComboBox 5 & 4 From Other ComboBoxes
            if (guna2ComboBox1.SelectedIndex == 2)
            {
                guna2ComboBox2.Items.Remove(guna2ComboBox4.SelectedItem);
                guna2ComboBox3.Items.Remove(guna2ComboBox4.SelectedItem);
                guna2ComboBox5.Items.Remove(guna2ComboBox4.SelectedItem);
                guna2ComboBox2.Items.Remove(guna2ComboBox5.SelectedItem);
                guna2ComboBox3.Items.Remove(guna2ComboBox5.SelectedItem);
                guna2ComboBox4.Items.Remove(guna2ComboBox5.SelectedItem);
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateGroup();
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateGroup();
        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateGroup();
        }

        private void guna2ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateGroup();
        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if (isValidGrp())
            {
                //Create Group
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into [Group] values (@Created_On)", con);
                cmd.Parameters.AddWithValue("@Created_On", DateTime.Today.ToShortDateString());
                cmd.ExecuteNonQuery();

                //Get ID From Group Which is Created
                SqlCommand cmdId = new SqlCommand("SELECT MAX(Id) from  [Group]", con);
                SqlDataReader Sdr = cmdId.ExecuteReader();
                Sdr.Read();
                int ID = Sdr.GetInt32(0);
                Sdr.Close();
                cmdId.ExecuteNonQuery();

                //Adding Members to the Group

                //Member 1
                SqlCommand cmd1 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                cmd1.Parameters.AddWithValue("@GroupId", ID);
                cmd1.Parameters.AddWithValue("@StudentId", getStudentId(guna2ComboBox2.SelectedItem.ToString()));
                cmd1.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox6.SelectedItem.ToString()));
                cmd1.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                cmd1.ExecuteNonQuery();

                //Member 2
                SqlCommand cmd2 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                cmd2.Parameters.AddWithValue("@GroupId", ID);
                cmd2.Parameters.AddWithValue("@StudentId", getStudentId(guna2ComboBox3.SelectedItem.ToString()));
                cmd2.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox7.SelectedItem.ToString()));
                cmd2.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                cmd2.ExecuteNonQuery();

                if (guna2ComboBox1.SelectedIndex == 1)
                {
                    //Member 3
                    SqlCommand cmd3 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                    cmd3.Parameters.AddWithValue("@GroupId", ID);
                    cmd3.Parameters.AddWithValue("@StudentId", getStudentId(guna2ComboBox4.SelectedItem.ToString()));
                    cmd3.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox8.SelectedItem.ToString()));
                    cmd3.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                    cmd3.ExecuteNonQuery();
                }
                if (guna2ComboBox1.SelectedIndex == 2)
                {
                    //Member 3
                    SqlCommand cmd3 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                    cmd3.Parameters.AddWithValue("@GroupId", ID);
                    cmd3.Parameters.AddWithValue("@StudentId", getStudentId(guna2ComboBox4.SelectedItem.ToString()));
                    cmd3.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox8.SelectedItem.ToString()));
                    cmd3.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                    cmd3.ExecuteNonQuery();

                    //Member 4
                    SqlCommand cmd4 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                    cmd4.Parameters.AddWithValue("@GroupId", ID);
                    cmd4.Parameters.AddWithValue("@StudentId", getStudentId(guna2ComboBox5.SelectedItem.ToString()));
                    cmd4.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox9.SelectedItem.ToString()));
                    cmd4.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                    cmd4.ExecuteNonQuery();
                }               
                MessageBox.Show("Successfully Created");
                ShowGroupData();
                hideMembersText();
                showStdRegNo();
                clearForm();
            }
        }

        private void clearForm()
        {
            guna2ComboBox4.ResetText();
            guna2ComboBox5.ResetText();
            guna2ComboBox1.ResetText();
            guna2ComboBox2.ResetText();
            guna2ComboBox3.ResetText();
        }

        private int getStatus(String a)
        {
            if (a == "Active")
                return 3;
            else
                return 4;
        }

        private int getStudentId(String a)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmdId = new SqlCommand("SELECT Id FROM  Student WHERE RegistrationNo = '"+a+"'", con);
            SqlDataReader Sdr = cmdId.ExecuteReader();
            Sdr.Read();
            int ID = Sdr.GetInt32(0);
            Sdr.Close();
            cmdId.ExecuteNonQuery();
            return ID;
        }

        private bool isValidGrp()
        {
            if(guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please Select No. of Members", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (guna2ComboBox1.SelectedIndex == 0)
            {
                if (guna2ComboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 1 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (guna2ComboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 2 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            else if (guna2ComboBox1.SelectedIndex == 1)
            {
                if (guna2ComboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 1 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (guna2ComboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 2 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (guna2ComboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 3 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            else if (guna2ComboBox1.SelectedIndex == 2)
            {
                if (guna2ComboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 1 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (guna2ComboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 2 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (guna2ComboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 3 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (guna2ComboBox5.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 4 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            return true;
        }

        void ShowGroupData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo,P.FirstName+' '+P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id ORDER BY G.GroupId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GroupGrid.DataSource = dt;
        }

        private void guna2ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (guna2ComboBox10.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo, P.FirstName + ' ' + P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id WHERE S.RegistrationNo LIKE '%" + guna2TextBox7.Text + "%' OR P.FirstName LIKE '%" + guna2TextBox7.Text + "%' OR P.LastName LIKE '%" + guna2TextBox7.Text + "%' OR S.Id LIKE '%" + guna2TextBox7.Text + "%' OR G.GroupId LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GroupGrid.DataSource = dt;
            }
            else if (guna2ComboBox10.SelectedIndex == 1)
            {
                SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo, P.FirstName + ' ' + P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id WHERE S.RegistrationNo LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GroupGrid.DataSource = dt;
            }
            else if (guna2ComboBox10.SelectedIndex == 2)
            {
                SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo, P.FirstName + ' ' + P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id WHERE P.FirstName LIKE '%" + guna2TextBox7.Text + "%' OR P.LastName LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GroupGrid.DataSource = dt;
            }
            else if (guna2ComboBox10.SelectedIndex == 3)
            {
                SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo, P.FirstName + ' ' + P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id WHERE S.Id LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GroupGrid.DataSource = dt;
            }
            else if (guna2ComboBox10.SelectedIndex == 4)
            {
                SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo, P.FirstName + ' ' + P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id WHERE G.GroupId LIKE '%" + guna2TextBox7.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GroupGrid.DataSource = dt;
            }
        }
    }
}

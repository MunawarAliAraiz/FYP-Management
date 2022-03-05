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
    public partial class EditGroupForm : Form
    {
        public EditGroupForm()
        {
            InitializeComponent();
            showGroupId();
            hideMembersText();
            ShowGroupData();
        }

        private void showGroupId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id FROM [Group]", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    comboBox1.Items.Add(Sdr.GetInt32(i).ToString());
                }
            }
            Sdr.Close();
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
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            guna2ComboBox6.Visible = false;
            guna2ComboBox7.Visible = false;
            guna2ComboBox8.Visible = false;
            guna2ComboBox9.Visible = false;
            guna2Button3.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button24.Visible = false;
            guna2Button2.Visible = false;
        }

        void ShowGroupData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo,P.FirstName+' '+P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GroupGrid.DataSource = dt;
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

        private void showStdRegNo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId WHERE G.GroupId IS NULL OR G.GroupID = '"+comboBox1.SelectedItem.ToString()+"'", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    if (!comboBox2.Items.Contains(Sdr.GetString(i)))
                    {
                        comboBox2.Items.Add(Sdr.GetString(i));
                    }
                    if (!comboBox3.Items.Contains(Sdr.GetString(i)))
                    {
                        comboBox3.Items.Add(Sdr.GetString(i));
                    }
                    if (!comboBox4.Items.Contains(Sdr.GetString(i)))
                    {
                        comboBox4.Items.Add(Sdr.GetString(i));
                    }
                    if (!comboBox5.Items.Contains(Sdr.GetString(i)))
                    {
                        comboBox5.Items.Add(Sdr.GetString(i));
                    }
                }
            }
            Sdr.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private String getStatus(int a)
        {
            if (a == 3)
                return "Active";
            else
                return "InActive";
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fun();
        }

        private void validateGroup(int c)
        {
            //Removing Selected Members of ComboBox 2 From Other ComboBoxes
            comboBox3.Items.Remove(comboBox2.SelectedItem);
            comboBox4.Items.Remove(comboBox2.SelectedItem);
            comboBox5.Items.Remove(comboBox2.SelectedItem);

            //Removing Selected Members of ComboBox 3 From Other ComboBoxes
            comboBox2.Items.Remove(comboBox3.SelectedItem);
            comboBox4.Items.Remove(comboBox3.SelectedItem);
            comboBox5.Items.Remove(comboBox3.SelectedItem);

            //Removing Selected Members of ComboBox 4 From Other ComboBoxes
            if (c == 3)
            {
                comboBox2.Items.Remove(comboBox4.SelectedItem);
                comboBox3.Items.Remove(comboBox4.SelectedItem);
                comboBox5.Items.Remove(comboBox4.SelectedItem);
            }

            //Removing Selected Members of ComboBox 5 & 4 From Other ComboBoxes
            if (c == 4)
            {
                comboBox2.Items.Remove(comboBox4.SelectedItem);
                comboBox3.Items.Remove(comboBox4.SelectedItem);
                comboBox5.Items.Remove(comboBox4.SelectedItem);
                comboBox2.Items.Remove(comboBox5.SelectedItem);
                comboBox3.Items.Remove(comboBox5.SelectedItem);
                comboBox4.Items.Remove(comboBox5.SelectedItem);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            label5.Visible = false;
            label9.Visible = false;
            comboBox4.Visible = false;
            guna2ComboBox8.Visible = false;
            guna2Button5.Visible = true;
            guna2Button3.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            label10.Visible = false;
            comboBox5.Visible = false;
            guna2ComboBox9.Visible = false;
            guna2Button5.Visible = true;
            guna2Button4.Visible = false;
        }

        private void fun()
        {
            showStdRegNo();
            hideMembersText();
            guna2Button2.Visible = true;
            guna2Button24.Visible = true;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("SELECT S.RegistrationNo FROM Student AS S JOIN GroupStudent AS G ON G.StudentId = S.Id WHERE G.GroupId = '" + comboBox1.SelectedItem.ToString() + "'", con);
            SqlCommand cmd2 = new SqlCommand("SELECT G.Status FROM Student AS S JOIN GroupStudent AS G ON G.StudentId = S.Id WHERE G.GroupId = '" + comboBox1.SelectedItem.ToString() + "'", con);
            SqlDataReader Sdr2 = cmd2.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(Sdr2);
            int count = dt.Rows.Count;
            if (count == 2)
            {
                string[] arr = new string[2];
                label3.Visible = true;
                label4.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                guna2ComboBox6.Visible = true;
                guna2ComboBox7.Visible = true;
                guna2Button5.Visible = true;
                SqlDataReader Sdr = cmd1.ExecuteReader();
                int i = 0;
                while (Sdr.Read())
                {
                    arr[i] = Sdr.GetString(0);
                    i++;
                }
                Sdr.Close();
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(arr[0]);
                comboBox3.SelectedIndex = comboBox3.Items.IndexOf(arr[1]);
                validateGroup(count);
            }
            else if (count == 3)
            {
                String[] arr = new String[3];
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
                guna2ComboBox6.Visible = true;
                guna2ComboBox7.Visible = true;
                guna2ComboBox8.Visible = true;
                guna2Button3.Visible = true;
                guna2Button5.Visible = true;
                SqlDataReader Sdr = cmd1.ExecuteReader();
                int i = 0;
                while (Sdr.Read())
                {
                    arr[i] = Sdr.GetString(0);
                    i++;
                }
                Sdr.Close();
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(arr[0]);
                comboBox3.SelectedIndex = comboBox3.Items.IndexOf(arr[1]);
                comboBox4.SelectedIndex = comboBox4.Items.IndexOf(arr[2]);
                validateGroup(count);
            }
            else if (count == 4)
            {
                string[] arr = new string[4];
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
                comboBox5.Visible = true;
                guna2ComboBox6.Visible = true;
                guna2ComboBox7.Visible = true;
                guna2ComboBox8.Visible = true;
                guna2ComboBox9.Visible = true;
                guna2Button3.Visible = true;
                guna2Button4.Visible = true;
                SqlDataReader Sdr = cmd1.ExecuteReader();
                int i = 0;
                while (Sdr.Read())
                {
                    arr[i] = Sdr.GetString(0);
                    i++;
                }
                Sdr.Close();
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(arr[0]);
                comboBox3.SelectedIndex = comboBox3.Items.IndexOf(arr[1]);
                comboBox4.SelectedIndex = comboBox4.Items.IndexOf(arr[2]);
                comboBox5.SelectedIndex = comboBox5.Items.IndexOf(arr[3]);
                validateGroup(count);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (guna2Button3.Visible != true)
            {
                label5.Visible = true;
                label9.Visible = true;
                comboBox4.Visible = true;
                comboBox4.ResetText();
                guna2ComboBox8.Visible = true;
                guna2Button3.Visible = true;
            }
            else if(guna2Button4.Visible != true)
            {
                label6.Visible = true;
                label10.Visible = true;
                comboBox5.Visible = true;
                comboBox5.ResetText();
                guna2ComboBox9.Visible = true;
                guna2Button4.Visible = true;
            }
            if(guna2Button3.Visible == true && guna2Button4.Visible == true)
            {
                guna2Button5.Visible = false;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            fun();
            guna2Button3.Enabled = false;
            guna2Button4.Enabled = false;
            guna2Button5.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            guna2ComboBox6.Enabled = false;
            guna2ComboBox7.Enabled = false;
            guna2ComboBox8.Enabled = false;
            guna2ComboBox9.Enabled = false;
            DialogResult dr = MessageBox.Show("Are You Sure You want to Delete this Group?", "Confirmation", MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE GroupStudent WHERE GroupId = '" + comboBox1.SelectedItem.ToString() + "'", con);
                SqlCommand cmd1 = new SqlCommand("DELETE [Group] WHERE Id = '" + comboBox1.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                showGroupId();
                hideMembersText();
                ShowGroupData();
                MessageBox.Show("Successfully Deleted");
            }
            else if (dr == DialogResult.No)
            {
                guna2Button3.Enabled = true;
                guna2Button4.Enabled = true;
                guna2Button5.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                guna2ComboBox6.Enabled = true;
                guna2ComboBox7.Enabled = true;
                guna2ComboBox8.Enabled = true;
                guna2ComboBox9.Enabled = true;
            }
        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            if(isValidGrp())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE GroupStudent WHERE GroupId = '" + comboBox1.SelectedItem.ToString() + "'", con);
                cmd.ExecuteNonQuery();

                int ID = int.Parse(comboBox1.SelectedItem.ToString());

                //Adding Members to the Group

                if (comboBox2.Visible == true && comboBox3.Visible == true)
                {
                    //Member 1
                    SqlCommand cmd1 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                    cmd1.Parameters.AddWithValue("@GroupId", ID);
                    cmd1.Parameters.AddWithValue("@StudentId", getStudentId(comboBox2.SelectedItem.ToString()));
                    cmd1.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox6.SelectedItem.ToString()));
                    cmd1.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Member 1 Added");

                    //Member 2
                    SqlCommand cmd2 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                    cmd2.Parameters.AddWithValue("@GroupId", ID);
                    cmd2.Parameters.AddWithValue("@StudentId", getStudentId(comboBox3.SelectedItem.ToString()));
                    cmd2.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox7.SelectedItem.ToString()));
                    cmd2.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Member 2 Added");
                }

                if (comboBox4.Visible == true)
                {
                    //Member 3
                    SqlCommand cmd3 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                    cmd3.Parameters.AddWithValue("@GroupId", ID);
                    cmd3.Parameters.AddWithValue("@StudentId", getStudentId(comboBox4.SelectedItem.ToString()));
                    cmd3.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox8.SelectedItem.ToString()));
                    cmd3.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Member 3 Added");
                }
                if (comboBox5.Visible == true)
                {
                    //Member 4
                    SqlCommand cmd4 = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId, @Status, @AssignmentDate)", con);
                    cmd4.Parameters.AddWithValue("@GroupId", ID);
                    cmd4.Parameters.AddWithValue("@StudentId", getStudentId(comboBox5.SelectedItem.ToString()));
                    cmd4.Parameters.AddWithValue("@Status", getStatus(guna2ComboBox9.SelectedItem.ToString()));
                    cmd4.Parameters.AddWithValue("@AssignmentDate", DateTime.Today.ToShortDateString());
                    cmd4.ExecuteNonQuery();
                    MessageBox.Show("Member 4 Added");
                }
                MessageBox.Show("Successfully Updated");
                showGroupId();
                hideMembersText();
                ShowGroupData();
            }
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
            SqlCommand cmdId = new SqlCommand("SELECT Id FROM  Student WHERE RegistrationNo = '" + a + "'", con);
            SqlDataReader Sdr = cmdId.ExecuteReader();
            Sdr.Read();
            int ID = Sdr.GetInt32(0);
            Sdr.Close();
            cmdId.ExecuteNonQuery();
            return ID;
        }

        private bool isValidGrp()
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please Select Group ID", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBox2.Visible == true && comboBox3.Visible == true)
            {
                if (comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 1 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 2 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            else if (comboBox2.Visible == true && comboBox3.Visible == true && comboBox4.Visible == true)
            {
                if (comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 1 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 2 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (comboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 3 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            else if (comboBox2.Visible == true && comboBox3.Visible == true && comboBox4.Visible == true && comboBox5.Visible == true)
            {
                if (comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 1 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 2 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (comboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 3 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (comboBox5.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Member 4 Reg No", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}

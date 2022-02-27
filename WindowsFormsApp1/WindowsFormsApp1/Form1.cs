using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void customizeDesign()
        {
            stdSubmenu.Visible = false;
            advSubmenu.Visible = false;
            proSubmenu.Visible = false;
            grpSubmenu.Visible = false;
            assignProjectSubmenu.Visible = false;
            assignAdvisorSubmenu.Visible = false;
            manEvaSubmenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if(stdSubmenu.Visible == true)
            {
                stdSubmenu.Visible = false;
            }
            if (advSubmenu.Visible == true)
            {
                advSubmenu.Visible = false;
            }
            if (proSubmenu.Visible == true)
            {
                proSubmenu.Visible = false;
            }
            if (grpSubmenu.Visible == true)
            {
                grpSubmenu.Visible = false;
            }
            if (assignProjectSubmenu.Visible == true)
            {
                assignProjectSubmenu.Visible = false;
            }
            if (assignAdvisorSubmenu.Visible == true)
            {
                assignAdvisorSubmenu.Visible = false;
            }
            if (manEvaSubmenu.Visible == true)
            {
                manEvaSubmenu.Visible = false;
            }
        }

        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void manageStudentBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(stdSubmenu);
        }

        private void manageAdvisorBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(advSubmenu);
        }

        private void manageProjectsBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(proSubmenu);
        }

        private void createGroupBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(grpSubmenu);
        }

        private void assignProjectBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(assignProjectSubmenu);
        }

        private void assignAdvisorBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(assignAdvisorSubmenu);
        }

        private void manageEvaluationBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(manEvaSubmenu);
        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private Form activeForm = null;
        private void openInnerForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            multiFormPanel.Controls.Add(childForm);
            multiFormPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void addStdSubmenu_Click(object sender, EventArgs e)
        {
            openInnerForm(new AddStudentForm());
        }

        private void updateStdSubmenu_Click(object sender, EventArgs e)
        {
            openInnerForm(new UpdateStudentForm());
        }

        private void deleteStdSubmenu_Click(object sender, EventArgs e)
        {
            openInnerForm(new DeleteStudentForm());
        }

        private void listStdSubmenu_Click(object sender, EventArgs e)
        {
            openInnerForm(new ListStudentForm());
        }
    }
}

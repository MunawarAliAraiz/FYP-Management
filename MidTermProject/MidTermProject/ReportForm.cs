using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidTermProject
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            hideButton();
        }

        private void hideButton()
        {
            guna2Button2.Visible = false;
            guna2Button5.Visible = false;
            guna2Button7.Visible = false;
            guna2Button9.Visible = false;
            guna2Button11.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Report1()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Title, PR.FirstName+' '+PR.LastName AS 'Advisor', L.Value AS 'Advisor Role' FROM Project AS P RIGHT OUTER JOIN ProjectAdvisor AS PA ON PA.ProjectId = P.Id JOIN Person AS PR ON PR.Id = PA.AdvisorId JOIN Lookup AS L ON L.Id = PA.AdvisorRole", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            reportGrid.DataSource = dt;
            guna2Button2.Visible = true;
        }

        void Report2()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT PR.FirstName+' '+PR.LastName AS 'Student Name',S.RegistrationNo,GS.GroupId,GE.EvaluationId,E.Name,E.TotalMarks,GE.ObtainedMarks,E.TotalWeightage FROM GroupStudent AS GS JOIN GroupEvaluation AS GE ON GE.GroupId = GS.GroupId JOIN Evaluation AS E ON GE.EvaluationId = E.Id JOIN Student AS S ON S.Id = GS.StudentId JOIN Person AS PR ON PR.Id = GS.StudentId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            reportGrid.DataSource = dt;
            guna2Button5.Visible = true;
        }

        void Report3()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT  S.RegistrationNo,P.FirstName+' '+P.LastName AS Name, S.Id AS 'Student-ID', G.GroupId FROM Student AS S LEFT OUTER JOIN GroupStudent AS G ON S.Id = G.StudentId JOIN Person AS P ON S.Id = P.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            reportGrid.DataSource = dt;
            guna2Button7.Visible = true;
        }

        void Report4()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select A.Id, P.FirstName,P.LastName,P.Contact,P.Email,P.DateOfBirth,L2.Value AS Gender,L1.Value AS Designation,A.Salary from Advisor AS A INNER JOIN Person AS P ON P.Id = A.Id INNER JOIN Lookup AS L1 ON L1.Id = A.Designation INNER JOIN Lookup AS L2 ON L2.Id = P.Gender", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            reportGrid.DataSource = dt;
            guna2Button9.Visible = true;
        }

        void Report5()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * From Evaluation", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            reportGrid.DataSource = dt;
            guna2Button11.Visible = true;
        }

        void Report1Print()
        {
            guna2Button2.Visible = false;
            pdfReport(reportGrid, "report1", "List of Projects Along with Advisory Board");
        }

        void Report2Print()
        {
            guna2Button5.Visible = false;
            pdfReport(reportGrid, "report2", "Marks Sheet of Each Student with against Each Evaluation");
        }

        void Report3Print()
        {
            guna2Button7.Visible = false;
            pdfReport(reportGrid, "report3", "List of All the Students Along with Group");
        }

        void Report4Print()
        {
            guna2Button9.Visible = false;
            pdfReport(reportGrid, "report4", "List of Advisors with Details");
        }

        void Report5Print()
        {
            guna2Button11.Visible = false;
            pdfReport(reportGrid, "report5", "List of All the Assesments");
        }

        private void pdfReport(DataGridView dataGridView1,String filename, String header)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = filename+".pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            //Font
                            System.Drawing.Font fontH1 = new System.Drawing.Font("Currier", 16);

                            //Header Section Format
                            PdfPTable pdfHeader = new PdfPTable(1); //1 for count of Columns
                            pdfHeader.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfHeader.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                            pdfHeader.DefaultCell.BorderWidth = 0;
                            pdfHeader.DefaultCell.PaddingBottom = 20;
                            pdfHeader.WidthPercentage = 100;

                            //Heading
                            Chunk c1 = new Chunk(header, FontFactory.GetFont("Times New Roman"));
                            c1.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                            c1.Font.SetStyle(1);
                            c1.Font.Size = 19;
                            Phrase p1 = new Phrase();
                            p1.Add(c1);
                            pdfHeader.AddCell(p1);
                            

                            //Table Section
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 5;
                            pdfTable.DefaultCell.BorderColor = new iTextSharp.text.BaseColor(162, 75, 64);
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            pdfTable.WidthPercentage = 100;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                Chunk c2 = new Chunk(column.HeaderText, FontFactory.GetFont("Times New Roman"));
                                c2.Font.Color = new iTextSharp.text.BaseColor(255,255,255);
                                c2.Font.SetStyle(0);
                                c2.Font.Size = 9;
                                Phrase p2 = new Phrase();
                                p2.Add(c2);
                                PdfPCell cell = new PdfPCell(p2);
                                cell.BackgroundColor = new BaseColor(33, 29, 85);
                                cell.Padding = 5;
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    Chunk c2 = new Chunk(cell.Value.ToString(), FontFactory.GetFont("Times New Roman"));
                                    c2.Font.Color = new iTextSharp.text.BaseColor(255, 255, 255);
                                    c2.Font.SetStyle(0);
                                    c2.Font.Size = 9;
                                    Phrase p2 = new Phrase();
                                    p2.Add(c2);
                                    PdfPCell cell1 = new PdfPCell(p2);
                                    cell1.BackgroundColor = new BaseColor(66, 58, 170);
                                    cell1.Padding = 3;
                                    pdfTable.AddCell(cell1);
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfHeader);
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Report Created Successfully!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Report1();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Report1Print();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Report2();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Report3();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Report4();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Report5();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Report2Print();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Report3Print();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            Report4Print();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            Report5Print();
        }
    }
}

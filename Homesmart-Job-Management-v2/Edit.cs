using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Homesmart_Job_Management_v2
{
    public partial class Edit : Form
    {
        int ID;

        public Edit(int CustomerID)
        {
            InitializeComponent();

            ID = CustomerID;
        }

        //Run on form load
        private void Edit_Load(object sender, EventArgs e)
        {
            int jobs = CountNumJobs();
            CreateJobs(jobs);
        }

        //Create a new job tab
        private void CreateTab()
        {
            tabControl1.TabPages.Add($"Job {tabControl1.TabPages.Count + 1}");

            AddJobDetailTitles();
        }

        //Loop number of jobs for the customer
        private void CreateJobs(int jobs)
        {
            for (int i = 0; i < jobs; i++)
            {
                CreateTab();
            }
        }

        //Count number of jobs for the customer
        private int CountNumJobs()
        {
            int jobs = 0;

            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                string query = "SELECT COUNT(JobID) AS 'Number of Jobs' " +
                                "FROM Job " +
                                "WHERE CustomerID = @CustomerID;";

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@CustomerID", ID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobs = reader.GetInt32(0);
                }
                reader.Close();
            }
            else
            {
                DialogResult result = MessageBox.Show("Server not found. Contact Admin", "Error", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    CountNumJobs();
                }
                else
                {
                    Close();
                }
            }

            dbConnection.CloseConnection();

            return jobs;
        }

        //Count number of active tabs
        private int CountNumTabs()
        {
            int tabs = tabControl1.TabPages.Count;
            return tabs;
        }

        //On add tab button press 
        private void BtnAddTab_Click(object sender, EventArgs e)
        {
            CreateTab();
        }

        //Create job detail elements
        private void AddJobDetailTitles()
        {

            var controlsInfo = new List<(Type, string, string, int, Point)>
            {
                (typeof(Label), "lblJobDetails", "Job Details", 16, new Point(10, 10)),
                (typeof(Label), "lblPaintColour", "Paint Colour", 10, new Point(10, 30)),
                (typeof(Label), "lblSurface", "Surface", 10, new Point(10, 50)),
                (typeof(Label), "lblArea", "Area", 10, new Point(10, 70)),
                (typeof(Label), "lblSupplier", "Supplier", 10, new Point(10, 90)),
                (typeof(Label), "lblValue", "Value", 10, new Point(10, 110))
            };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                foreach (var (controlType, name, text, fontSize, position) in controlsInfo)
                {
                    Control newControl = (Control)Activator.CreateInstance(controlType);
                    newControl.Name = name;
                    newControl.Text = text;
                    newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                    newControl.Location = position;

                    tabPage.Controls.Add(newControl);
                }
            }
        }
    }
}

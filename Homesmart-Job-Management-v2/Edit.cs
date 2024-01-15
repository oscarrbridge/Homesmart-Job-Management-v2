using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Data;
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

        private void Edit_Load(object sender, EventArgs e)
        {
            int jobs = GetNumJobs();
            CreateJobs(jobs);
        }


        private void CreateTab()
        {
            tabControl1.TabPages.Add($"Job {tabControl1.TabPages.Count + 1}");
        }

        private void CreateJobs(int jobs)
        {
            for (int i = 0; i < jobs; i++)
            {
                CreateTab();
            }
        }

        private int GetNumJobs()
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
                    GetNumJobs();
                }
                else
                {
                    Close();
                }
            }

            dbConnection.CloseConnection();

            return jobs;
        }

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            CreateTab();
        }
    }
}

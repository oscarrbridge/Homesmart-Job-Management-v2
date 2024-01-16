using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
            AddPaintDetailTitles();
            AddInternalChargeTitles();
            AddQuoteTitles();
            AddInvoiceTitles();
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

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(Label), "lblSalesPerson",   "Sales Person", 10, new Point(10, 10), new Size(104, 16)),
                (typeof(Label), "lblQuoteDetails",  "Details",      10, new Point(134, 10), new Size(104, 16)),
                (typeof(Label), "lblQuoteOwner",    "Quote Owner",  10, new Point(263, 10), new Size(104, 16)),
                (typeof(Label), "lblQuoteNumber",   "Quote Number", 10, new Point(385, 10), new Size(104, 16)),
                (typeof(Label), "lblQuoteValue",    "Value",        10, new Point(633, 10), new Size(104, 16))
            };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                {
                    Control newControl = (Control)Activator.CreateInstance(controlType);
                    newControl.Name = name;
                    newControl.Text = text;
                    newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                    newControl.Location = position;
                    newControl.Size = size;

                    tabPage.Controls.Add(newControl);
                }
            }
        }

        //Create paint detail elements
        private void AddPaintDetailTitles()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(Label), "lblPaintColour",   "Paint Colour", 10, new Point(134, 83), new Size(104, 16)),
                (typeof(Label), "lblSurface",       "Surface",      10, new Point(258, 83), new Size(104, 16)),
                (typeof(Label), "lblArea",          "Area",         10, new Point(387, 83), new Size(104, 16)),
                (typeof(Label), "lblSupplier",      "Supplier",     10, new Point(509, 83), new Size(104, 16)),
                (typeof(Label), "lblValue",         "Value",        10, new Point(633, 83), new Size(104, 16))
            };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                {
                    Control newControl = (Control)Activator.CreateInstance(controlType);
                    newControl.Name = name;
                    newControl.Text = text;
                    newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                    newControl.Location = position;
                    newControl.Size = size;

                    tabPage.Controls.Add(newControl);
                }
            }
        }

        //Create internal charges elements
        private void AddInternalChargeTitles()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(Label), "lblInternalCharges",   "Internal Charges",         16, new Point(10, 156), new Size(175, 30)),
                (typeof(Label), "lblInternalSupplier",  "Supplier / Contractor",    10, new Point(10, 193), new Size(140, 20)),
                (typeof(Label), "lblInternalCompany",   "Internal Company",         10, new Point(168, 193), new Size(140, 20)),
                (typeof(Label), "lblType",              "Type of Charge",           10, new Point(326, 193), new Size(140, 20)),
                (typeof(Label), "lblValue",             "Value",                    10, new Point(633, 193), new Size(140, 20))
            };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                {
                    Control newControl = (Control)Activator.CreateInstance(controlType);
                    newControl.Name = name;
                    newControl.Text = text;
                    newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                    newControl.Location = position;
                    newControl.Size = size;

                    tabPage.Controls.Add(newControl);
                }
            }
        }

        //Create quote elements
        private void AddQuoteTitles()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(Label), "lblQuotes",        "Quotes",               16, new Point(10, 261), new Size(175, 30)),
                (typeof(Label), "lblQuoteSupplier", "Supplier / Contractor",10, new Point(10, 304), new Size(140, 20)),
                (typeof(Label), "lblQuoteDate",     "Date",                 10, new Point(168, 304), new Size(140, 20)),
                (typeof(Label), "lblQuoteReference","Reference",            10, new Point(326, 304), new Size(140, 20)),
                (typeof(Label), "lblQuoteValue",    "Value",                10, new Point(633, 304), new Size(140, 20))
            };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                {
                    Control newControl = (Control)Activator.CreateInstance(controlType);
                    newControl.Name = name;
                    newControl.Text = text;
                    newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                    newControl.Location = position;
                    newControl.Size = size;

                    tabPage.Controls.Add(newControl);
                }
            }
        }

        //Create quote elements
        private void AddInvoiceTitles()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(Label), "lblInvoice",        "Quotes",                16, new Point(10, 371), new Size(175, 30)),
                (typeof(Label), "lblInvoiceSupplier", "Supplier / Contractor",10, new Point(10, 413), new Size(140, 20)),
                (typeof(Label), "lblInvoiceDate",     "Date",                 10, new Point(168, 413), new Size(140, 20)),
                (typeof(Label), "lblInvoiceReference","Reference",            10, new Point(326, 413), new Size(140, 20)),
                (typeof(Label), "lblInvoiceValue",    "Value",                10, new Point(633, 413), new Size(140, 20))
            };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                {
                    Control newControl = (Control)Activator.CreateInstance(controlType);
                    newControl.Name = name;
                    newControl.Text = text;
                    newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                    newControl.Location = position;
                    newControl.Size = size;

                    tabPage.Controls.Add(newControl);
                }
            }
        }
    }
}

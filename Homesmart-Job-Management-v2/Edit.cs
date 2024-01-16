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
        }

        //Loop number of jobs for the customer
        private void CreateJobs(int jobs)
        {
            for (int i = 0; i < jobs; i++)
            {
                CreateTab();
            }

            AddJobDetailTitles();
            AddPaintDetailTitles();
            AddInternalChargeTitles();
            AddQuoteTitles();
            AddInvoiceTitles();

            AddPaintDetailInputs();
            AddJobDetailInputs();
            AddInternalChargeInputs();
            AddQuoteInputs();
            AddInvoiceInputs();
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
                (typeof(Label), "lblValue",         "Value",        10, new Point(633, 83), new Size(104, 16)),
                (typeof(Button), "btnAddDetail",    "+",             8, new Point(717, 63), new Size(20, 20))
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

                    if (newControl is Button button)
                    {
                        //button.Click += Button_Click;
                    }

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
                (typeof(Label), "lblType",              "Type",                     10, new Point(326, 193), new Size(140, 20)),
                (typeof(Label), "lblValue",             "Value",                    10, new Point(633, 193), new Size(140, 20)),
                (typeof(Button), "btnAddCharge",        "+",                         8, new Point(717, 173), new Size(20, 20))
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

                    if (newControl is Button button)
                    {
                        //button.Click += Button_Click;
                    }

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
                (typeof(Label), "lblQuoteValue",    "Value",                10, new Point(633, 304), new Size(140, 20)),
                (typeof(Button), "btnAddQuote",     "+",                     8, new Point(717, 284), new Size(20, 20))
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

                    if (newControl is Button button)
                    {
                        //button.Click += Button_Click;
                    }

                    tabPage.Controls.Add(newControl);
                }
            }
        }

        //Create quote elements
        private void AddInvoiceTitles()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(Label), "lblInvoice",        "Invoices",              16, new Point(10, 371), new Size(175, 30)),
                (typeof(Label), "lblInvoiceSupplier", "Supplier / Contractor",10, new Point(10, 413), new Size(140, 20)),
                (typeof(Label), "lblInvoiceDate",     "Date",                 10, new Point(168, 413), new Size(140, 20)),
                (typeof(Label), "lblInvoiceReference","Reference",            10, new Point(326, 413), new Size(140, 20)),
                (typeof(Label), "lblInvoiceValue",    "Value",                10, new Point(633, 413), new Size(140, 20)),
                (typeof(Button), "btnAddInvoice",     "+",                     8, new Point(717, 393), new Size(20, 20))
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

                    if (newControl is Button button)
                    {
                        //button.Click += Button_Click;
                    }

                    tabPage.Controls.Add(newControl);
                }
            }
        }



        //Create job detail elements
        private void AddJobDetailInputs()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(TextBox),       "txtSalesPerson",   "", 10, new Point(10, 38), new Size(104, 16)),
                (typeof(TextBox),       "txtQuoteDetails",  "", 10, new Point(134, 38), new Size(104, 16)),
                (typeof(ComboBox),      "txtQuoteOwner",    "", 10, new Point(263, 38), new Size(104, 16)),
                (typeof(TextBox),       "txtQuoteNumber",   "", 10, new Point(385, 38), new Size(104, 16)),
                (typeof(NumericUpDown), "txtQuoteValue",    "", 10, new Point(633, 38), new Size(104, 16))
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
        private void AddPaintDetailInputs()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(TextBox),       "txtPaintColour",   "",     10, new Point(134, 106), new Size(104, 16)),
                (typeof(TextBox),       "txtSurface",       "",     10, new Point(258, 106), new Size(104, 16)),
                (typeof(TextBox),       "txtArea",          "",     10, new Point(387, 106), new Size(104, 16)),
                (typeof(ComboBox),      "txtSupplier",      "",     10, new Point(509, 106), new Size(104, 16)),
                (typeof(NumericUpDown), "txtValue",         "",     10, new Point(633, 106), new Size(104, 16)),
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
        private void AddInternalChargeInputs()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(ComboBox),      "txtInternalSupplier",  "",    10, new Point(10,  225), new Size(140, 20)),
                (typeof(ComboBox),      "txtInternalCompany",   "",    10, new Point(168, 225), new Size(140, 20)),
                (typeof(TextBox),       "txtType",              "",    10, new Point(326, 225), new Size(140, 20)),
                (typeof(NumericUpDown), "txtValue",             "",    10, new Point(633, 225), new Size(100, 20))
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
        private void AddQuoteInputs()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(ComboBox),      "txtQuoteSupplier", "", 10, new Point(10, 337), new Size(140, 20)),
                (typeof(ComboBox),      "txtQuoteDate",     "", 10, new Point(168, 337), new Size(140, 20)),
                (typeof(TextBox),       "txtQuoteReference","", 10, new Point(326, 337), new Size(140, 20)),
                (typeof(NumericUpDown), "txtQuoteValue",    "", 10, new Point(633, 337), new Size(100, 20))
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
        private void AddInvoiceInputs()
        {

            var controlsInfo = new List<(Type, string, string, int, Point, Size)>
            {
                (typeof(ComboBox),      "txtInvoiceSupplier", "",   10, new Point(10,  445), new Size(140, 20)),
                (typeof(ComboBox),      "txtInvoiceDate",     "",   10, new Point(168, 445), new Size(140, 20)),
                (typeof(TextBox),       "txtInvoiceReference","",   10, new Point(326, 445), new Size(140, 20)),
                (typeof(NumericUpDown), "txtInvoiceValue",    "",   10, new Point(633, 445), new Size(100, 20)),
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

        /*
         *Dynamically create rows below titles
         *Start importing the information to the form
        */
    }
}

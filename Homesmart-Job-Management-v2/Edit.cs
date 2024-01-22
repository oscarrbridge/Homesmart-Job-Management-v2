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
        public Edit(int CustomerID)
        {
            InitializeComponent();

            //ID = CustomerID;
            lblCustomerID.Value = CustomerID;
        }

        //Run on form load
        private void Edit_Load(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(JobID) AS 'Number of Jobs' " +
                           "FROM Job " +
                           "WHERE CustomerID = @CustomerID;";

            int jobs = CountNumJobs(query);

            PopulateCustomerDetails();

            CreateJobPages(jobs);
        }

        //Create a new job tab
        private void CreateTab()
        {
            tabControl1.TabPages.Add($"Job {tabControl1.TabPages.Count + 1}");  
        }

        //Loop number of jobs for the customer
        private void CreateJobPages(int jobs)
        {
            for (int i = 0; i < jobs; i++)
            {
                CreateTab();
            }

            AddJobDetailTitles();
            PopulatePages();
        }

        private void PopulatePages()
        {
            AddJobDetailTitles();
            //AddPaintDetailTitles();
            //AddInternalChargeTitles();
            //AddQuoteTitles();
            //AddInvoiceTitles();

            AddJobDetailInputs();
            AddPaintDetailInputs();
            AddInternalChargeInputs();
            AddQuoteInputs();
            AddInvoiceInputs();
        }

        //Count number of jobs for the customer
        private int CountNumJobs(string query)
        {
            int jobs = 0;

            NumericUpDown lblJobID = new NumericUpDown();

            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@CustomerID", lblCustomerID.Value);
                cmd.Parameters.AddWithValue("@JobID", lblJobID.Value);
                
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
                    CountNumJobs(query);
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
            //CreateJobPages(1);
        }

        private List<int> GetJobIDs()
        {
            List<int> jobIDs = new List<int>();

            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                string query = "SELECT JobID " +
                               "FROM Job " +
                               "WHERE CustomerID = @CustomerID;";

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@CustomerID", lblCustomerID.Value);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobIDs.Add(reader.GetInt32(0));
                }
                reader.Close();
            }
            else
            {
                DialogResult result = MessageBox.Show("Server not found. Contact Admin", "Error", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    return GetJobIDs();
                }
                else
                {
                    Close();
                }
            }

            return jobIDs;
        }



        //Create job detail elements
        private void AddJobDetailTitles()
        {
            List<int> jobIDs = GetJobIDs(); // Call the function to get the list of JobIDs

            int tabIndex = 0;

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    //Job Details
                    (typeof(Label), "lblSalesPerson",   "Sales Person", 10, new Point(10, 10), new Size(104, 16)),
                    (typeof(Label), "lblQuoteDetails",  "Details",      10, new Point(134, 10), new Size(104, 16)),
                    (typeof(Label), "lblQuoteOwner",    "Quote Owner",  10, new Point(263, 10), new Size(104, 16)),
                    (typeof(Label), "lblQuoteNumber",   "Quote Number", 10, new Point(385, 10), new Size(104, 16)),
                    (typeof(Label), "lblQuoteValue",    "Value",        10, new Point(633, 10), new Size(104, 16)),
                    (typeof(NumericUpDown), "lblJobID", jobIDs[tabIndex].ToString(), 10, new Point(633, 572), new Size(104, 16)), // Fill in the JobID for this tab
                
                    //Paint Details
                    (typeof(Label), "lblPaintColour",   "Paint Colour", 10, new Point(134, 83), new Size(104, 16)),
                    (typeof(Label), "lblSurface",       "Surface",      10, new Point(258, 83), new Size(104, 16)),
                    (typeof(Label), "lblArea",          "Area",         10, new Point(387, 83), new Size(104, 16)),
                    (typeof(Label), "lblSupplier",      "Supplier",     10, new Point(509, 83), new Size(104, 16)),
                    (typeof(Label), "lblValue",         "Value",        10, new Point(633, 83), new Size(104, 16)),
                    (typeof(Button), "btnAddDetail",    "+",             8, new Point(717, 63), new Size(20, 20)),

                    //Internal Charges
                    (typeof(Label), "lblInternalCharges",   "Internal Charges",         16, new Point(10, 156), new Size(175, 30)),
                    (typeof(Label), "lblInternalSupplier",  "Supplier / Contractor",    10, new Point(10, 193), new Size(140, 20)),
                    (typeof(Label), "lblInternalCompany",   "Internal Company",         10, new Point(168, 193), new Size(140, 20)),
                    (typeof(Label), "lblType",              "Type",                     10, new Point(326, 193), new Size(140, 20)),
                    (typeof(Label), "lblValue",             "Value",                    10, new Point(633, 193), new Size(140, 20)),
                    (typeof(Button), "btnAddCharge",        "+",                         8, new Point(717, 173), new Size(20, 20)),

                    //Quotes
                    (typeof(Label), "lblQuotes",        "Quotes",               16, new Point(10, 261), new Size(175, 30)),
                    (typeof(Label), "lblQuoteSupplier", "Supplier / Contractor",10, new Point(10, 304), new Size(140, 20)),
                    (typeof(Label), "lblQuoteDate",     "Date",                 10, new Point(168, 304), new Size(140, 20)),
                    (typeof(Label), "lblQuoteReference","Reference",            10, new Point(326, 304), new Size(140, 20)),
                    (typeof(Label), "lblQuoteValue",    "Value",                10, new Point(633, 304), new Size(140, 20)),
                    (typeof(Button), "btnAddQuote",     "+",                     8, new Point(717, 284), new Size(20, 20)),

                    //Invoices
                    (typeof(Label), "lblInvoice",        "Invoices",              16, new Point(10, 371), new Size(175, 30)),
                    (typeof(Label), "lblInvoiceSupplier", "Supplier / Contractor",10, new Point(10, 413), new Size(140, 20)),
                    (typeof(Label), "lblInvoiceDate",     "Date",                 10, new Point(168, 413), new Size(140, 20)),
                    (typeof(Label), "lblInvoiceReference","Reference",            10, new Point(326, 413), new Size(140, 20)),
                    (typeof(Label), "lblInvoiceValue",    "Value",                10, new Point(633, 413), new Size(140, 20)),
                    (typeof(Button), "btnAddInvoice",     "+",                     8, new Point(717, 393), new Size(20, 20))
                };

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

                tabIndex++; // Move to the next JobID for the next tab
            }
        }

        /*
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

        */


        //Query SQL server
        private List<string> GetDetails(string jobID, string query)
        {
            List<string> jobDetails = new List<string>();

            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("@JobID", jobID);
                cmd.Parameters.AddWithValue("@CustomerID", jobID);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!reader.IsDBNull(i))
                        {
                            jobDetails.Add(reader.GetString(i));
                        }
                        else
                        {
                            jobDetails.Add("");
                        }
                    }

                }
                reader.Close();
            }
            else
            {
                DialogResult result = MessageBox.Show("Server not found. Contact Admin", "Error", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    return GetDetails(jobID, query);
                }
                else
                {
                    Close();
                }
            }

            return jobDetails;
        }

        private List<List<string>> GetAllDetails(string jobID, string query)
        {
            List<List<string>> allJobDetails = new List<List<string>>();

            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("@JobID", jobID);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<string> jobDetails = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!reader.IsDBNull(i))
                        {
                            jobDetails.Add(reader.GetString(i));
                        }
                        else
                        {
                            jobDetails.Add("");
                        }
                    }
                    allJobDetails.Add(jobDetails);
                }
                reader.Close();
            }
            else
            {
                DialogResult result = MessageBox.Show("Server not found. Contact Admin", "Error", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    return GetAllDetails(jobID, query);
                }
                else
                {
                    Close();
                }
            }

            return allJobDetails;
        }



        //Create job detail elements
        private void AddJobDetailInputs()
        {
            int tabIndex = 0;

            string query = "SELECT SalesPerson, Details, QuoteOwner, QuoteNumber, QuoteValue " +
               "FROM Job " +
               "WHERE JobID = @JobID;";

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<string> jobDetails = GetDetails(tabPage.Controls["lblJobID"].Text, query);

                var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    (typeof(TextBox),       "txtSalesPerson",   jobDetails[0], 10, new Point(10, 38), new Size(104, 16)),
                    (typeof(TextBox),       "txtQuoteDetails",  jobDetails[1], 10, new Point(134, 38), new Size(104, 16)),
                    (typeof(ComboBox),      "txtQuoteOwner",    jobDetails[2], 10, new Point(263, 38), new Size(104, 16)),
                    (typeof(TextBox),       "txtQuoteNumber",   jobDetails[3], 10, new Point(385, 38), new Size(104, 16)),
                    (typeof(NumericUpDown), "txtQuoteValue",    jobDetails[4], 10, new Point(633, 38), new Size(104, 16))
                };

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

                tabIndex++; // Move to the next JobID for the next tab
            }
        }

        //Create paint detail elements
        private void AddPaintDetailInputs()
        {
            int tabIndex = 0;

            string query = "SELECT PaintColour, Surface, Area, Supplier, Value " +
               "FROM AdditionalJobInfo " +
               "WHERE JobID = @JobID;";

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<List<string>> allPaintDetails = GetAllDetails(tabPage.Controls["lblJobID"].Text, query);

                int yOffset = 0;
                foreach (List<string> paintDetails in allPaintDetails)
                {
                    if (paintDetails.Count >= 5)
                    {
                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    (typeof(TextBox),       "txtPaintColour",   paintDetails[0], 10, new Point(134, 106 + yOffset), new Size(104, 16)),
                    (typeof(TextBox),       "txtSurface",       paintDetails[1], 10, new Point(258, 106 + yOffset), new Size(104, 16)),
                    (typeof(TextBox),       "txtArea",          paintDetails[2], 10, new Point(387, 106 + yOffset), new Size(104, 16)),
                    (typeof(ComboBox),      "txtSupplier",      paintDetails[3], 10, new Point(509, 106 + yOffset), new Size(104, 16)),
                    (typeof(NumericUpDown), "txtValue",         paintDetails[4], 10, new Point(633, 106 + yOffset), new Size(104, 16)),
                };

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

                        yOffset += 20; // Adjust this value as needed to space out the rows properly
                    }
                }

                tabIndex++; // Move to the next JobID for the next tab
            }
        }


        //Create internal charges elements
        private void AddInternalChargeInputs()
        {
            int tabIndex = 0;

            string query = "SELECT SupplierContractorID, CompanyName, Type, Value " +
               "FROM InterCompanyCharge " +
               "WHERE JobID = @JobID;";

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<List<string>> allInternalChargeDetails = GetAllDetails(tabPage.Controls["lblJobID"].Text, query);

                int yOffset = 0;
                foreach (List<string> internalChargeDetails in allInternalChargeDetails)
                {
                    if (internalChargeDetails.Count >= 4)
                    {
                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    (typeof(ComboBox),      "txtInternalSupplier",  internalChargeDetails[0], 10, new Point(10,  225 + yOffset), new Size(140, 20)),
                    (typeof(ComboBox),      "txtInternalCompany",   internalChargeDetails[1], 10, new Point(168, 225 + yOffset), new Size(140, 20)),
                    (typeof(TextBox),       "txtType",              internalChargeDetails[2], 10, new Point(326, 225 + yOffset), new Size(140, 20)),
                    (typeof(NumericUpDown), "txtValue",             internalChargeDetails[3], 10, new Point(633, 225 + yOffset), new Size(100, 20)),
                };

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

                        yOffset += 20; // Adjust this value as needed to space out the rows properly
                    }
                }

                tabIndex++; // Move to the next JobID for the next tab
            }
        }


        //Create quote elements
        private void AddQuoteInputs()
        {
            int tabIndex = 0;

            string query = "SELECT SupplierContractorID, QuoteDate, QuoteReference, QuoteValue " +
               "FROM Quotes " +
               "WHERE JobID = @JobID;";

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<List<string>> allQuoteDetails = GetAllDetails(tabPage.Controls["lblJobID"].Text, query);

                int yOffset = 0;
                foreach (List<string> quoteDetails in allQuoteDetails)
                {
                    if (quoteDetails.Count >= 4)
                    {
                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    (typeof(ComboBox),      "txtQuoteSupplier",  quoteDetails[0], 10, new Point(10, 337 + yOffset), new Size(140, 20)),
                    (typeof(ComboBox),      "txtQuoteDate",      quoteDetails[1], 10, new Point(168, 337 + yOffset), new Size(140, 20)),
                    (typeof(TextBox),       "txtQuoteReference", quoteDetails[2], 10, new Point(326, 337 + yOffset), new Size(140, 20)),
                    (typeof(NumericUpDown), "txtQuoteValue",     quoteDetails[3], 10, new Point(633, 337 + yOffset), new Size(100, 20)),
                };

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

                        yOffset += 20; // Adjust this value as needed to space out the rows properly
                    }
                }

                tabIndex++; // Move to the next JobID for the next tab
            }
        }


        //Create quote elements
        private void AddInvoiceInputs()
        {
            int tabIndex = 0;

            string query = "SELECT SupplierContractorID, InvoiceDate, InvoiceReference, InvoiceValue " +
               "FROM Invoices " +
               "WHERE JobID = @JobID;";

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<List<string>> allInvoiceDetails = GetAllDetails(tabPage.Controls["lblJobID"].Text, query);

                int yOffset = 0;
                foreach (List<string> invoiceDetails in allInvoiceDetails)
                {
                    if (invoiceDetails.Count >= 4)
                    {
                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    (typeof(ComboBox),      "txtInvoiceSupplier",  invoiceDetails[0], 10, new Point(10,  445 + yOffset), new Size(140, 20)),
                    (typeof(ComboBox),      "txtInvoiceDate",      invoiceDetails[1], 10, new Point(168, 445 + yOffset), new Size(140, 20)),
                    (typeof(TextBox),       "txtInvoiceReference", invoiceDetails[2], 10, new Point(326, 445 + yOffset), new Size(140, 20)),
                    (typeof(NumericUpDown), "txtInvoiceValue",     invoiceDetails[3], 10, new Point(633, 445 + yOffset), new Size(100, 20)),
                };

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

                        yOffset += 30; // Adjust this value as needed to space out the rows properly
                    }
                }

                tabIndex++; // Move to the next JobID for the next tab
            }
        }



        private void PopulateCustomerDetails()
        {
            string query = "SELECT CustomerName, CustomerAddress, CustomerEmail " +
               "FROM CustomerInfo " +
               "WHERE CustomerID = @CustomerID;";

            List<string> customerDetails = GetDetails(this.Controls["lblCustomerID"].Text, query);

            this.Controls["txtCustomerName"].Text = customerDetails[0];
            this.Controls["txtCustomerAddress"].Text = customerDetails[1];
            this.Controls["txtCustomerEmail"].Text = customerDetails[2];
        }



        /*
         *Dynamically create rows below titles
         *Start importing the information to the form
        */
    }
}

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
        int DropDownHeight = 30; // How much should each item drop

        int InputOffset = 30; // How low below title

        private Dictionary<TabPage, int> paintOffsets = new Dictionary<TabPage, int>();
        private Dictionary<TabPage, int> internalOffsets = new Dictionary<TabPage, int>();
        private Dictionary<TabPage, int> quoteOffsets = new Dictionary<TabPage, int>();
        private Dictionary<TabPage, int> invoiceOffsets = new Dictionary<TabPage, int>();

        int lblJobDetailsY = 10;
        int lblPaintDetailsY = 83;
        int lblInternalChargesY = 173;
        int lblQuotesY = 263;
        int lblInvoicesY = 353;

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

        void InitializeOffsets()
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                // Initialize the offset for this tab if it hasn't been set yet
                if (!paintOffsets.ContainsKey(tabPage))
                {
                    //paintOffsets[tabPage] = 5;
                    paintOffsets[tabPage] = 0;
                }
                if (!internalOffsets.ContainsKey(tabPage))
                {
                    //internalOffsets[tabPage] = 40;
                    internalOffsets[tabPage] = 0;
                }
                if (!quoteOffsets.ContainsKey(tabPage))
                {
                    //quoteOffsets[tabPage] = 70;
                    quoteOffsets[tabPage] = 0;
                }
                if (!invoiceOffsets.ContainsKey(tabPage))
                {
                    //invoiceOffsets[tabPage] = 100;
                    invoiceOffsets[tabPage] = 0;
                }
            }
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

            PopulatePages();
        }

        private void PopulatePages()
        {
            AddJobDetailTitles();

            InitializeOffsets();

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

        private void Button_Click(object sender, EventArgs e)
        {
            // Get the parent control (e.g., a Panel or Form) that contains the controls you want to move
            Control parentControl = ((Control)sender).Parent;

            // Call MoveControlsDown with the parent control's Controls collection
            MoveControlsDown(parentControl.Controls, 106, 20);
        }

        //Create job detail elements
        private void AddJobDetailTitles()
        {
            List<int> jobIDs = GetJobIDs(); // Call the function to get the list of JobIDs

            int tabIndex = 0;

            foreach (TabPage tabPage in tabControl1.TabPages)
            {

                Panel panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.Name = "panel" + tabIndex;

                var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    //Job Details
                    (typeof(Label), "lblSalesPerson",   "Sales Person", 10, new Point(10,  lblJobDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblQuoteDetails",  "Details",      10, new Point(134, lblJobDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblQuoteOwner",    "Quote Owner",  10, new Point(263, lblJobDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblQuoteNumber",   "Quote Number", 10, new Point(385, lblJobDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblQuoteValue",    "Value",        10, new Point(633, lblJobDetailsY), new Size(104, 16)),
                    (typeof(NumericUpDown), "lblJobID", jobIDs[tabIndex].ToString(), 10, new Point(633, 572), new Size(104, 16)), // Fill in the JobID for this tab
               
                    //Paint Details
                    (typeof(Button), "btnAddDetail",    "+",             8, new Point(717, lblPaintDetailsY), new Size(20, 20)),
                    (typeof(Label), "lblPaintColour",   "Paint Colour", 10, new Point(134, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblSurface",       "Surface",      10, new Point(263, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblArea",          "Area",         10, new Point(387, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblSupplier",      "Supplier",     10, new Point(509, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblValue",         "Value",        10, new Point(633, lblPaintDetailsY), new Size(104, 16)),

                    //Internal Charges
                    (typeof(Button), "btnAddCharge",        "+",                         8, new Point(717, lblInternalChargesY), new Size(20,  20)),
                    (typeof(Label), "lblInternalCharges",   "Internal Charges",         16, new Point(10,  lblInternalChargesY - 30), new Size(175, 30)),
                    (typeof(Label), "lblInternalSupplier",  "Supplier / Contractor",    10, new Point(10,  lblInternalChargesY), new Size(140, 16)),
                    (typeof(Label), "lblInternalCompany",   "Internal Company",         10, new Point(168, lblInternalChargesY), new Size(140, 16)),
                    (typeof(Label), "lblType",              "Type",                     10, new Point(326, lblInternalChargesY), new Size(140, 16)),
                    (typeof(Label), "lblValue",             "Value",                    10, new Point(633, lblInternalChargesY), new Size(140, 16)),

                    //Quotes
                    (typeof(Button), "btnAddQuote",     "+",                     8, new Point(717, lblQuotesY), new Size(20,  20)),
                    (typeof(Label), "lblQuotes",        "Quotes",               16, new Point(10,  lblQuotesY - 30), new Size(175,  30)),
                    (typeof(Label), "lblQuoteSupplier", "Supplier / Contractor",10, new Point(10,  lblQuotesY), new Size(140,  16)),
                    (typeof(Label), "lblQuoteDate",     "Date",                 10, new Point(168, lblQuotesY), new Size(140, 16)),
                    (typeof(Label), "lblQuoteReference","Reference",            10, new Point(326, lblQuotesY), new Size(140, 16)),
                    (typeof(Label), "lblQuoteValue",    "Value",                10, new Point(633, lblQuotesY), new Size(140, 16)),

                    //Invoices
                    (typeof(Button), "btnAddInvoice",     "+",                     8, new Point(717, lblInvoicesY), new Size(20,  20)),
                    (typeof(Label), "lblInvoice",        "Invoices",              16, new Point(10,  lblInvoicesY - 30), new Size(175,  30)),
                    (typeof(Label), "lblInvoiceSupplier", "Supplier / Contractor",10, new Point(10,  lblInvoicesY), new Size(140,  16)),
                    (typeof(Label), "lblInvoiceDate",     "Date",                 10, new Point(168, lblInvoicesY), new Size(140, 16)),
                    (typeof(Label), "lblInvoiceReference","Reference",            10, new Point(326, lblInvoicesY), new Size(140, 16)),
                    (typeof(Label), "lblInvoiceValue",    "Value",                10, new Point(633, lblInvoicesY), new Size(140, 16)),
                };

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
                        button.Click += Button_Click;
                    }
                    else if (newControl is DateTimePicker dateTimePicker)
                    {
                        dateTimePicker.Format = DateTimePickerFormat.Custom;
                        dateTimePicker.CustomFormat = "dd/MM/yy";
                    }



                    //tabPage.Controls.Add(panel);

                    tabPage.Controls.Add(newControl);
                }

                tabIndex++; // Move to the next JobID for the next tab
            }
        }


        //Query SQL server
        private List<List<string>> GetAllDetails(string jobID, string query)
        {
            List<List<string>> allJobDetails = new List<List<string>>();

            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("@JobID", jobID);
                cmd.Parameters.AddWithValue("@CustomerID", lblCustomerID.Value);

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

        //Move controls down
        private void MoveControlsDown(Control.ControlCollection controls, int startingY, int offset)
        {
            foreach (Control control in controls)
            {
                if (control.Location.Y > startingY)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + offset);
                }
            }
        }



        private void AddInputs(string query, List<(Type, string, int, Point, Size)> controlsInfo, TabPage tabPage, int controlsY, Dictionary<TabPage, int> offsets, string passType)
        {
            //tabPage = tabControl1.SelectedTab;

            // Initialize the offset for this tab if it hasn't been set yet
            if (!offsets.ContainsKey(tabPage))
            {
                offsets[tabPage] = 0;
            }

            List<List<string>> allDetails = GetAllDetails(tabPage.Controls["lblJobID"].Text, query);


            foreach (List<string> details in allDetails)
            {
                if (details.Count >= controlsInfo.Count)
                {   
                    for (int i = 0; i < controlsInfo.Count; i++)
                    {
                        var (controlType, name, fontSize, position, size) = controlsInfo[i];
                        string text = details[i];

                        Control newControl = (Control)Activator.CreateInstance(controlType);
                        newControl.Name = name;
                        newControl.Text = text;
                        newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                        newControl.Location = new Point(position.X, position.Y + offsets[tabPage]); // Update the Y position based on the offset
                        newControl.Size = size;

                        tabPage.Controls.Add(newControl);
                    }
                }
                // Move all controls below the new Y-position down
                MoveControlsDown(tabPage.Controls, controlsY + offsets[tabPage], DropDownHeight);
                
                if(passType == "paint")
                {
                    paintOffsets[tabPage] += DropDownHeight;
                    internalOffsets[tabPage] += DropDownHeight;
                    quoteOffsets[tabPage] += DropDownHeight;
                    invoiceOffsets[tabPage] += DropDownHeight;
                }
                else if (passType == "internal")
                {
                    internalOffsets[tabPage] += DropDownHeight;
                    quoteOffsets[tabPage] += DropDownHeight;
                    invoiceOffsets[tabPage] += DropDownHeight;
                }
                else if (passType == "quote")
                {
                    quoteOffsets[tabPage] += DropDownHeight;
                    invoiceOffsets[tabPage] += DropDownHeight;
                }
                else if (passType == "invoice")
                {
                    invoiceOffsets[tabPage] += DropDownHeight;
                }
            }
        }






        //Create job detail elements
        private void AddJobDetailInputs()
        {
            int tabIndex = 0;

            string query = "SELECT SalesPerson, Details, QuoteOwner, QuoteNumber, QuoteValue " +
               "FROM Job " +
               "WHERE JobID = @JobID;";

            int yPos = lblJobDetailsY + InputOffset;

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<List<string>> jobDetails = GetAllDetails(tabPage.Controls["lblJobID"].Text, query);

                var controlsInfo = new List<(Type, string, string, int, Point, Size)>
        {
            (typeof(TextBox),       "txtSalesPerson",   jobDetails[0][0], 8, new Point(10,  yPos), new Size(104, 16)),
            (typeof(TextBox),       "txtQuoteDetails",  jobDetails[0][1], 8, new Point(134, yPos), new Size(104, 16)),
            (typeof(ComboBox),      "txtQuoteOwner",    jobDetails[0][2], 8, new Point(263, yPos), new Size(104, 16)),
            (typeof(TextBox),       "txtQuoteNumber",   jobDetails[0][3], 8, new Point(385, yPos), new Size(104, 16)),
            (typeof(NumericUpDown), "txtQuoteValue",    jobDetails[0][4], 8, new Point(633, yPos), new Size(104, 16))
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
            string query = "SELECT PaintColour, Surface, Area, Supplier, Value " +
               "FROM AdditionalJobInfo " +
               "WHERE JobID = @JobID;";

            int yPos = lblPaintDetailsY + InputOffset;

            var controlsInfo = new List<(Type, string, int, Point, Size)>
    {
        (typeof(TextBox),       "txtPaintColour",   8, new Point(134, yPos), new Size(104, 16)),
        (typeof(TextBox),       "txtSurface",       8, new Point(263, yPos), new Size(104, 16)),
        (typeof(TextBox),       "txtArea",          8, new Point(387, yPos), new Size(104, 16)),
        (typeof(ComboBox),      "txtSupplier",      8, new Point(509, yPos), new Size(104, 16)),
        (typeof(NumericUpDown), "txtValue",         8, new Point(633, yPos), new Size(104, 16)),
    };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                AddInputs(query, controlsInfo, tabPage, yPos, paintOffsets, "paint");
            }
        }

        //Create internal charges elements
        private void AddInternalChargeInputs()
        {
            string query = "SELECT SupplierContractorID, CompanyName, Type, Value " +
               "FROM InterCompanyCharge " +
               "WHERE JobID = @JobID;";

            int yPos = lblInternalChargesY + InputOffset;

            var controlsInfo = new List<(Type, string, int, Point, Size)>
    {
        (typeof(ComboBox),      "txtInternalSupplier",  8, new Point(10,  yPos), new Size(140, 20)),
        (typeof(ComboBox),      "txtInternalCompany",   8, new Point(168, yPos), new Size(140, 20)),
        (typeof(TextBox),       "txtType",              8, new Point(326, yPos), new Size(140, 20)),
        (typeof(NumericUpDown), "txtValue",             8, new Point(633, yPos), new Size(100, 20)),
    };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                AddInputs(query, controlsInfo, tabPage, yPos, internalOffsets, "internal");
            }
        }

        //Create quote elements
        private void AddQuoteInputs()
        {
            string query = "SELECT SupplierContractorID, QuoteDate, QuoteReference, QuoteValue " +
               "FROM Quotes " +
               "WHERE JobID = @JobID;";

            int yPos = lblQuotesY + InputOffset;

            var controlsInfo = new List<(Type, string, int, Point, Size)>
    {
        (typeof(ComboBox),      "txtQuoteSupplier",  8, new Point(10,  yPos), new Size(140, 20)),
        (typeof(ComboBox),      "txtQuoteDate",      8, new Point(168, yPos), new Size(140, 20)),
        (typeof(TextBox),       "txtQuoteReference", 8, new Point(326, yPos), new Size(140, 20)),
        (typeof(NumericUpDown), "txtQuoteValue",     8, new Point(633, yPos), new Size(100, 20)),
    };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                AddInputs(query, controlsInfo, tabPage, yPos, quoteOffsets, "quote");
            }
        }

        //Create invoice elements
        private void AddInvoiceInputs()
        {
            string query = "SELECT SupplierContractorID, InvoiceDate, InvoiceReference, InvoiceValue " +
               "FROM Invoices " +
               "WHERE JobID = @JobID;";

            int yPos = lblInvoicesY + InputOffset;

            var controlsInfo = new List<(Type, string, int, Point, Size)>
    {
        (typeof(ComboBox),      "txtInvoiceSupplier",  8, new Point(10,  yPos), new Size(140, 20)),
        (typeof(ComboBox),      "txtInvoiceDate",      8, new Point(168, yPos), new Size(140, 20)),
        (typeof(TextBox),       "txtInvoiceReference", 8, new Point(326, yPos), new Size(140, 20)),
        (typeof(NumericUpDown), "txtInvoiceValue",     8, new Point(633, yPos), new Size(100, 20)),
    };

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                AddInputs(query, controlsInfo, tabPage, yPos, invoiceOffsets, "invoice");
            }
        }





        private void PopulateCustomerDetails()
        {
            string query = "SELECT CustomerName, CustomerAddress, CustomerEmail " +
               "FROM CustomerInfo " +
               "WHERE CustomerID = @CustomerID;";

            List<List<string>> customerDetails = GetAllDetails(this.Controls["lblCustomerID"].Text, query);

            this.Controls["txtCustomerName"].Text = customerDetails[0][0];
            this.Controls["txtCustomerAddress"].Text = customerDetails[0][1];
            this.Controls["txtCustomerEmail"].Text = customerDetails[0][2];
        }


    }
}

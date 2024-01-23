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
        int PaintOffset = 0;
        int InternalOffset = 0;
        int QuoteOffset = 0;
        int InvoiceOffset = 0;

        int lblJobDetailsY = 10;
        int lblPaintDetailsY = 83;
        int lblInternalChargesY = 193;
        int lblQuotesY = 304;
        int lblInvoicesY = 413;

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
                    (typeof(Label), "lblPaintColour",   "Paint Colour", 10, new Point(134, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblSurface",       "Surface",      10, new Point(263, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblArea",          "Area",         10, new Point(387, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblSupplier",      "Supplier",     10, new Point(509, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Label), "lblValue",         "Value",        10, new Point(633, lblPaintDetailsY), new Size(104, 16)),
                    (typeof(Button), "btnAddDetail",    "+",             8, new Point(717, 63), new Size(20, 20)),

                    //Internal Charges
                    (typeof(Label), "lblInternalCharges",   "Internal Charges",         16, new Point(10,  lblInternalChargesY - 30), new Size(175, 30)),
                    (typeof(Label), "lblInternalSupplier",  "Supplier / Contractor",    10, new Point(10,  lblInternalChargesY), new Size(140, 16)),
                    (typeof(Label), "lblInternalCompany",   "Internal Company",         10, new Point(168, lblInternalChargesY), new Size(140, 16)),
                    (typeof(Label), "lblType",              "Type",                     10, new Point(326, lblInternalChargesY), new Size(140, 16)),
                    (typeof(Label), "lblValue",             "Value",                    10, new Point(633, lblInternalChargesY), new Size(140, 16)),
                    (typeof(Button), "btnAddCharge",        "+",                         8, new Point(717, 173), new Size(20,  20)),

                    //Quotes
                    (typeof(Label), "lblQuotes",        "Quotes",               16, new Point(10,  lblQuotesY - 30), new Size(175,  30)),
                    (typeof(Label), "lblQuoteSupplier", "Supplier / Contractor",10, new Point(10,  lblQuotesY), new Size(140,  16)),
                    (typeof(Label), "lblQuoteDate",     "Date",                 10, new Point(168, lblQuotesY), new Size(140, 16)),
                    (typeof(Label), "lblQuoteReference","Reference",            10, new Point(326, lblQuotesY), new Size(140, 16)),
                    (typeof(Label), "lblQuoteValue",    "Value",                10, new Point(633, lblQuotesY), new Size(140, 16)),
                    (typeof(Button), "btnAddQuote",     "+",                     8, new Point(717, 284), new Size(20,  20)),

                    //Invoices
                    (typeof(Label), "lblInvoice",        "Invoices",              16, new Point(10,  lblInvoicesY -30), new Size(175,  30)),
                    (typeof(Label), "lblInvoiceSupplier", "Supplier / Contractor",10, new Point(10,  lblInvoicesY), new Size(140,  16)),
                    (typeof(Label), "lblInvoiceDate",     "Date",                 10, new Point(168, lblInvoicesY), new Size(140, 16)),
                    (typeof(Label), "lblInvoiceReference","Reference",            10, new Point(326, lblInvoicesY), new Size(140, 16)),
                    (typeof(Label), "lblInvoiceValue",    "Value",                10, new Point(633, lblInvoicesY), new Size(140, 16)),
                    (typeof(Button), "btnAddInvoice",     "+",                     8, new Point(717, 393), new Size(20,  20))
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

        //Move controls down
        private void MoveControlsDown(Control.ControlCollection controls, int startingY, int offset)
        {
            foreach (Control control in controls)
            {
                if (control.Location.Y >= startingY)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + offset);
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

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<string> jobDetails = GetDetails(tabPage.Controls["lblJobID"].Text, query);

                var controlsInfo = new List<(Type, string, string, int, Point, Size)>
                {
                    (typeof(TextBox),       "txtSalesPerson",   jobDetails[0], 8, new Point(10,  (lblJobDetailsY + 20)), new Size(104, 16)),
                    (typeof(TextBox),       "txtQuoteDetails",  jobDetails[1], 8, new Point(134, (lblJobDetailsY + 20)), new Size(104, 16)),
                    (typeof(ComboBox),      "txtQuoteOwner",    jobDetails[2], 8, new Point(263, (lblJobDetailsY + 20)), new Size(104, 16)),
                    (typeof(TextBox),       "txtQuoteNumber",   jobDetails[3], 8, new Point(385, (lblJobDetailsY + 20)), new Size(104, 16)),
                    (typeof(NumericUpDown), "txtQuoteValue",    jobDetails[4], 8, new Point(633, (lblJobDetailsY + 20)), new Size(104, 16))
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

        int DropDownHeight = 30; // Assuming each control has a height of 20

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

                foreach (List<string> paintDetails in allPaintDetails)
                {
                    if (paintDetails.Count >= 5)
                    {
                        // Move all controls below the new Y-position down
                        MoveControlsDown(tabPage.Controls, 106 + PaintOffset, DropDownHeight);

                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
{
    (typeof(TextBox),       "txtPaintColour",   paintDetails[0], 8, new Point(134, (lblPaintDetailsY + 20) + PaintOffset), new Size(104, 16)),
    (typeof(TextBox),       "txtSurface",       paintDetails[1], 8, new Point(258, (lblPaintDetailsY + 20) + PaintOffset), new Size(104, 16)),
    (typeof(TextBox),       "txtArea",          paintDetails[2], 8, new Point(387, (lblPaintDetailsY + 20) + PaintOffset), new Size(104, 16)),
    (typeof(ComboBox),      "txtSupplier",      paintDetails[3], 8, new Point(509, (lblPaintDetailsY + 20) + PaintOffset), new Size(104, 16)),
    (typeof(NumericUpDown), "txtValue",         paintDetails[4], 8, new Point(633, (lblPaintDetailsY + 20) + PaintOffset), new Size(104, 16)),
};

                        Control lastControl = null;
                        foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                        {
                            Control newControl = (Control)Activator.CreateInstance(controlType);
                            newControl.Name = name;
                            newControl.Text = text;
                            newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                            newControl.Location = position;
                            newControl.Size = size;

                            tabPage.Controls.Add(newControl);
                            lastControl = newControl;
                        }
                    }
                    PaintOffset += 20;
                    InternalOffset += 20;
                    QuoteOffset += 20;
                    InvoiceOffset += 20;
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

                foreach (List<string> internalChargeDetails in allInternalChargeDetails)
                {
                    if (internalChargeDetails.Count >= 4)
                    {

                        // Move all controls below the new Y-position down
                        MoveControlsDown(tabPage.Controls, 225 + InternalOffset, DropDownHeight);

                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
        {
            (typeof(ComboBox),      "txtInternalSupplier",  internalChargeDetails[0], 8, new Point(10,  (lblInternalChargesY + 20) + InternalOffset), new Size(140, 20)),
            (typeof(ComboBox),      "txtInternalCompany",   internalChargeDetails[1], 8, new Point(168, (lblInternalChargesY + 20) + InternalOffset), new Size(140, 20)),
            (typeof(TextBox),       "txtType",              internalChargeDetails[2], 8, new Point(326, (lblInternalChargesY + 20) + InternalOffset), new Size(140, 20)),
            (typeof(NumericUpDown), "txtValue",             internalChargeDetails[3], 8, new Point(633, (lblInternalChargesY + 20) + InternalOffset), new Size(100, 20)),
        };

                        Control lastControl = null;
                        foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                        {
                            Control newControl = (Control)Activator.CreateInstance(controlType);
                            newControl.Name = name;
                            newControl.Text = text;
                            newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                            newControl.Location = position;
                            newControl.Size = size;

                            tabPage.Controls.Add(newControl);
                            lastControl = newControl;
                        }
                    }
                    InternalOffset += 20;
                    QuoteOffset += 20;
                    InvoiceOffset += 20;
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

                foreach (List<string> quoteDetails in allQuoteDetails)
                {
                    if (quoteDetails.Count >= 4)
                    {

                        // Move all controls below the new Y-position down
                        MoveControlsDown(tabPage.Controls, 337 + QuoteOffset, DropDownHeight);

                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
{
    (typeof(ComboBox),      "txtQuoteSupplier",  quoteDetails[0], 8, new Point(10,  (lblQuotesY + 20) + QuoteOffset), new Size(140, 20)),
    (typeof(ComboBox),      "txtQuoteDate",      quoteDetails[1], 8, new Point(168, (lblQuotesY + 20) + QuoteOffset), new Size(140, 20)),
    (typeof(TextBox),       "txtQuoteReference", quoteDetails[2], 8, new Point(326, (lblQuotesY + 20) + QuoteOffset), new Size(140, 20)),
    (typeof(NumericUpDown), "txtQuoteValue",     quoteDetails[3], 8, new Point(633, (lblQuotesY + 20) + QuoteOffset), new Size(100, 20)),
};

                        Control lastControl = null;
                        foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                        {
                            Control newControl = (Control)Activator.CreateInstance(controlType);
                            newControl.Name = name;
                            newControl.Text = text;
                            newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                            newControl.Location = position;
                            newControl.Size = size;

                            tabPage.Controls.Add(newControl);
                            lastControl = newControl;
                        }
                    }
                    QuoteOffset += 20;
                    InvoiceOffset += 20;
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

                foreach (List<string> invoiceDetails in allInvoiceDetails)
                {
                    if (invoiceDetails.Count >= 4)
                    {

                        // Move all controls below the new Y-position down
                        MoveControlsDown(tabPage.Controls, 445 + InvoiceOffset, DropDownHeight);

                        var controlsInfo = new List<(Type, string, string, int, Point, Size)>
{
    (typeof(ComboBox),      "txtInvoiceSupplier",  invoiceDetails[0], 8, new Point(10,  (lblInvoicesY + 20) + InvoiceOffset), new Size(140, 20)),
    (typeof(ComboBox),      "txtInvoiceDate",      invoiceDetails[1], 8, new Point(168, (lblInvoicesY + 20) + InvoiceOffset), new Size(140, 20)),
    (typeof(TextBox),       "txtInvoiceReference", invoiceDetails[2], 8, new Point(326, (lblInvoicesY + 20) + InvoiceOffset), new Size(140, 20)),
    (typeof(NumericUpDown), "txtInvoiceValue",     invoiceDetails[3], 8, new Point(633, (lblInvoicesY + 20) + InvoiceOffset), new Size(100, 20)),
};

                        Control lastControl = null;
                        foreach (var (controlType, name, text, fontSize, position, size) in controlsInfo)
                        {
                            Control newControl = (Control)Activator.CreateInstance(controlType);
                            newControl.Name = name;
                            newControl.Text = text;
                            newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                            newControl.Location = position;
                            newControl.Size = size;

                            tabPage.Controls.Add(newControl);
                            lastControl = newControl;
                        }
                    }
                    InvoiceOffset += 20;
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

    }
}

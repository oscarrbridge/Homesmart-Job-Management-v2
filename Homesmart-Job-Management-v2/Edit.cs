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
        int DropDownHeight = 30; // Assuming each control has a height of 20

        int InputOffset = 20;

        int PaintOffset = 0;
        int InternalOffset = 0;
        int QuoteOffset = 0;
        int InvoiceOffset = 0;

        int lblJobDetailsY = 10;
        int lblPaintDetailsY = 83;
        int lblInternalChargesY = 163;
        int lblQuotesY = 243;
        int lblInvoicesY = 323;

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

        private void AddInputs(string query, List<(Type, string, int, Point, Size)> controlsInfo, ref int offset, int controlsY)
        {
            int tabIndex = 0;

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                List<List<string>> allDetails = GetAllDetails(tabPage.Controls["lblJobID"].Text, query);

                foreach (List<string> details in allDetails)
                {
                    if (details.Count >= controlsInfo.Count)
                    {
                        // Move all controls below the new Y-position down
                        MoveControlsDown(tabPage.Controls, controlsY + offset, DropDownHeight);

                        Control lastControl = null;
                        for (int i = 0; i < controlsInfo.Count; i++)
                        {
                            var (controlType, name, fontSize, position, size) = controlsInfo[i];
                            string text = details[i];

                            Control newControl = (Control)Activator.CreateInstance(controlType);
                            newControl.Name = name;
                            newControl.Text = text;
                            newControl.Font = new Font(newControl.Font.FontFamily, fontSize);
                            newControl.Location = new Point(position.X, position.Y + offset); // Update the Y position based on the offset
                            newControl.Size = size;

                            tabPage.Controls.Add(newControl);
                            lastControl = newControl;
                        }
                        offset += DropDownHeight; // Move this line inside the inner loop
                    }
                }

                tabIndex++; // Move to the next JobID for the next tab
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
                    (typeof(TextBox),       "txtSalesPerson",   jobDetails[0], 8, new Point(10,  (lblJobDetailsY + InputOffset)), new Size(104, 16)),
                    (typeof(TextBox),       "txtQuoteDetails",  jobDetails[1], 8, new Point(134, (lblJobDetailsY + InputOffset)), new Size(104, 16)),
                    (typeof(ComboBox),      "txtQuoteOwner",    jobDetails[2], 8, new Point(263, (lblJobDetailsY + InputOffset)), new Size(104, 16)),
                    (typeof(TextBox),       "txtQuoteNumber",   jobDetails[3], 8, new Point(385, (lblJobDetailsY + InputOffset)), new Size(104, 16)),
                    (typeof(NumericUpDown), "txtQuoteValue",    jobDetails[4], 8, new Point(633, (lblJobDetailsY + InputOffset)), new Size(104, 16))
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

            var controlsInfo = new List<(Type, string, int, Point, Size)>
            {
                (typeof(TextBox),       "txtPaintColour",   8, new Point(134, (lblPaintDetailsY + InputOffset)), new Size(104, 16)),
                (typeof(TextBox),       "txtSurface",       8, new Point(263, (lblPaintDetailsY + InputOffset)), new Size(104, 16)),
                (typeof(TextBox),       "txtArea",          8, new Point(387, (lblPaintDetailsY + InputOffset)), new Size(104, 16)),
                (typeof(ComboBox),      "txtSupplier",      8, new Point(509, (lblPaintDetailsY + InputOffset)), new Size(104, 16)),
                (typeof(NumericUpDown), "txtValue",         8, new Point(633, (lblPaintDetailsY + InputOffset)), new Size(104, 16)),
            };

            PaintOffset += 5; // Increase the initial offset
            AddInputs(query, controlsInfo, ref PaintOffset, lblPaintDetailsY);

            PaintOffset += DropDownHeight;
            InternalOffset += DropDownHeight;
            QuoteOffset += DropDownHeight;
            InvoiceOffset += DropDownHeight;
        }





        //Create internal charges elements
        private void AddInternalChargeInputs()
        {
            string query = "SELECT SupplierContractorID, CompanyName, Type, Value " +
               "FROM InterCompanyCharge " +
               "WHERE JobID = @JobID;";

            var controlsInfo = new List<(Type, string, int, Point, Size)>
            {
                (typeof(ComboBox),      "txtInternalSupplier",  8, new Point(10,  (lblInternalChargesY + InputOffset)), new Size(140, 20)),
                (typeof(ComboBox),      "txtInternalCompany",   8, new Point(168, (lblInternalChargesY + InputOffset)), new Size(140, 20)),
                (typeof(TextBox),       "txtType",              8, new Point(326, (lblInternalChargesY + InputOffset)), new Size(140, 20)),
                (typeof(NumericUpDown), "txtValue",             8, new Point(633, (lblInternalChargesY + InputOffset)), new Size(100, 20)),
            };

            InternalOffset += 40;
            AddInputs(query, controlsInfo, ref InternalOffset, lblInternalChargesY);

            InternalOffset += DropDownHeight;
            QuoteOffset += DropDownHeight;
            InvoiceOffset += DropDownHeight;
        }


        //Create quote elements
        private void AddQuoteInputs()
        {
            string query = "SELECT SupplierContractorID, QuoteDate, QuoteReference, QuoteValue " +
               "FROM Quotes " +
               "WHERE JobID = @JobID;";

            var controlsInfo = new List<(Type, string, int, Point, Size)>
            {
                (typeof(ComboBox),      "txtQuoteSupplier",  8, new Point(10,  (lblQuotesY + InputOffset)), new Size(140, 20)),
                (typeof(ComboBox),      "txtQuoteDate",      8, new Point(168, (lblQuotesY + InputOffset)), new Size(140, 20)),
                (typeof(TextBox),       "txtQuoteReference", 8, new Point(326, (lblQuotesY + InputOffset)), new Size(140, 20)),
                (typeof(NumericUpDown), "txtQuoteValue",     8, new Point(633, (lblQuotesY + InputOffset)), new Size(100, 20)),
            };

            QuoteOffset += 70;
            AddInputs(query, controlsInfo, ref QuoteOffset, lblQuotesY);

            QuoteOffset += DropDownHeight;
            InvoiceOffset += DropDownHeight;
        }


        //Create invoice elements
        private void AddInvoiceInputs()
        {
            string query = "SELECT SupplierContractorID, InvoiceDate, InvoiceReference, InvoiceValue " +
               "FROM Invoices " +
               "WHERE JobID = @JobID;";
        
            var controlsInfo = new List<(Type, string, int, Point, Size)>
            {
                (typeof(ComboBox),      "txtInvoiceSupplier",  8, new Point(10,  (lblInvoicesY + InputOffset)), new Size(140, 20)),
                (typeof(ComboBox),      "txtInvoiceDate",      8, new Point(168, (lblInvoicesY + InputOffset)), new Size(140, 20)),
                (typeof(TextBox),       "txtInvoiceReference", 8, new Point(326, (lblInvoicesY + InputOffset)), new Size(140, 20)),
                (typeof(NumericUpDown), "txtInvoiceValue",     8, new Point(633, (lblInvoicesY + InputOffset)), new Size(100, 20)),
            };

            InvoiceOffset += 100;
            AddInputs(query, controlsInfo, ref InvoiceOffset, lblInvoicesY);
        
            InvoiceOffset += DropDownHeight;
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

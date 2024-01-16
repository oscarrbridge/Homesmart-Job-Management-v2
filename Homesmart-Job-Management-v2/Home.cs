using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Homesmart_Job_Management_v2
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            Search();
            SetColumnWidth();
        }

        //Set widths of columns
        private void SetColumnWidth()
        {
            dataJobs.Columns[1].Width = 40;
            dataJobs.Columns[2].Width = 160;
            dataJobs.Columns[3].Width = 205;
            dataJobs.Columns[4].Width = 205;
        }

        //Search database for specified name / address
        private void Search()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                string query = "";

                if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT CustomerInfo.CustomerID AS 'ID', " +
                            "CustomerInfo.CustomerName AS 'Customer Name', " +
                            "CustomerInfo.CustomerAddress AS 'Customer Address', " +
                            "CustomerInfo.CustomerEmail AS 'Customer Email' " +
                            "FROM CustomerInfo " +
                            "WHERE CustomerInfo.CustomerName LIKE @CustomerName " +
                            "AND CustomerInfo.CustomerAddress LIKE @CustomerAddress";
                }
                else if (boxCustomerName.Text.Length > 0)
                {
                    query = "SELECT CustomerInfo.CustomerID AS 'ID', " +
                            "CustomerInfo.CustomerName AS 'Customer Name', " +
                            "CustomerInfo.CustomerAddress AS 'Customer Address', " +
                            "CustomerInfo.CustomerEmail AS 'Customer Email' " +
                            "FROM CustomerInfo " +
                            "WHERE CustomerInfo.CustomerName LIKE @CustomerName";
                }
                else if (boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT CustomerInfo.CustomerID AS 'ID', " +
                            "CustomerInfo.CustomerName AS 'Customer Name', " +
                            "CustomerInfo.CustomerAddress AS 'Customer Address', " +
                            "CustomerInfo.CustomerEmail AS 'Customer Email' " +
                            "FROM CustomerInfo " +
                            "WHERE CustomerInfo.CustomerAddress LIKE @CustomerAddress;";
                }
                else if (boxCustomerName.Text.Length == 0 && boxCustomerAddress.Text.Length == 0)
                {
                    query = "SELECT CustomerInfo.CustomerID AS 'ID', " +
                            "CustomerInfo.CustomerName AS 'Customer Name', " +
                            "CustomerInfo.CustomerAddress AS 'Customer Address', " +
                            "CustomerInfo.CustomerEmail AS 'Customer Email' " +
                            "FROM CustomerInfo;";
                }

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@CustomerName", "%" + boxCustomerName.Text + "%");
                cmd.Parameters.AddWithValue("@CustomerAddress", "%" + boxCustomerAddress.Text + "%");

                DataTable dt = new DataTable();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                dataJobs.DataSource = dt;
            }
            else
            {
                DialogResult result = MessageBox.Show("Server not found. Contact Admin", "Error", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    Search();
                }
                else
                {
                    Close();
                }
            }

            dbConnection.CloseConnection();
        }

        //Check for text in name / address field
        private void CheckSearchConditions()
        {
            if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
            {
                btnSubmitNew.Enabled = true;
            }
            else
            {
                btnSubmitNew.Enabled = false;
            }
        }

        //On search button press
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        //On edit customer name text changed
        private void BoxCustomerName_TextChanged(object sender, EventArgs e)
        {
            CheckSearchConditions();
        }

        //On edit customer address text changed
        private void BoxCustomerAddress_TextChanged(object sender, EventArgs e)
        {
            CheckSearchConditions();
        }

        //On reset button pressed
        private void BtnResetSearch_Click(object sender, EventArgs e)
        {
            boxCustomerName.Text = "";
            boxCustomerAddress.Text = "";
            Search();
        }

        //On search button pressed
        private void BtnSubmitNew_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Is the information correct: " +
                                                        $"\nCustomer Name: {boxCustomerName.Text}" +
                                                        $"\nCustomer Address: {boxCustomerAddress.Text}",
                                                        "Confirmation", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                DatabaseConnection dbConnection = new DatabaseConnection();

                if (dbConnection.OpenConnection() == true)
                {
                    string query = "INSERT INTO CustomerInfo (CustomerName, CustomerAddress) " +
                                   "VALUES (@CustomerName, @CustomerAddress);";

                    MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                    cmd.Parameters.AddWithValue("@CustomerName", boxCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CustomerAddress", boxCustomerAddress.Text);

                    cmd.ExecuteNonQuery();

                    dbConnection.CloseConnection();
                }
                Search();
            }
        }

        //On edit button pressed
        private void DataJobs_ClickEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) ;

            // If the 'Edit' button was clicked.
            else if (dataJobs.Columns[e.ColumnIndex].Name == "btnEdit" && e.RowIndex >= 0)
            {
                // Get the job ID from the selected row.
                int ID = Convert.ToInt32(dataJobs.Rows[e.RowIndex].Cells["ID"].Value);

                Edit edit = new Edit(ID);
                edit.Show();
            }
        }
    }
}

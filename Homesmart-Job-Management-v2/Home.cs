using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homesmart_Job_Management_v2
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            search();
            setColumnWidth();
        }

        private void setColumnWidth()
        {
            dataJobs.Columns[1].Width = 170;
            dataJobs.Columns[2].Width = 220;
            dataJobs.Columns[3].Width = 220;
        }

        private void search()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            if (dbConnection.OpenConnection() == true)
            {
                string query = "";

                if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT CustomerInfo.CustomerName AS 'Customer Name', " +
                            "CustomerInfo.CustomerAddress AS 'Customer Address', " +
                            "CustomerInfo.CustomerEmail AS 'Customer Email' " +
                            "FROM CustomerInfo " +
                            "WHERE CustomerInfo.CustomerName LIKE @CustomerName " +
                            "AND CustomerInfo.CustomerAddress LIKE @CustomerAddress";
                }
                else if (boxCustomerName.Text.Length > 0)
                {
                    query = "SELECT CustomerInfo.CustomerName AS 'Customer Name', " +
                            "CustomerInfo.CustomerAddress AS 'Customer Address', " +
                            "CustomerInfo.CustomerEmail AS 'Customer Email' " +
                            "FROM CustomerInfo " +
                            "WHERE CustomerInfo.CustomerName LIKE @CustomerName";
                }
                else if (boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT CustomerInfo.CustomerName AS 'Customer Name', " +
                        "CustomerInfo.CustomerAddress AS 'Customer Address', " +
                        "CustomerInfo.CustomerEmail AS 'Customer Email' " +
                        "FROM CustomerInfo " +
                        "WHERE CustomerInfo.CustomerAddress LIKE @CustomerAddress;";
                }
                else if (boxCustomerName.Text.Length == 0 && boxCustomerAddress.Text.Length == 0)
                {
                    query = "SELECT CustomerInfo.CustomerName AS 'Customer Name', " +
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
                    search();
                }
                else
                {
                    Close();
                }
            }

            dbConnection.CloseConnection();
        }

        private void checkSearchConditions()
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void boxCustomerName_TextChanged(object sender, EventArgs e)
        {
            checkSearchConditions();
        }

        private void boxCustomerAddress_TextChanged(object sender, EventArgs e)
        {
            checkSearchConditions();
        }

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            boxCustomerName.Text = "";
            boxCustomerAddress.Text = "";
            search();
        }

        private void btnSubmitNew_Click(object sender, EventArgs e)
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
                search();
            }
        }
    }
}

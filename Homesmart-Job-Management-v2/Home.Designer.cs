namespace Homesmart_Job_Management_v2
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabJobs = new System.Windows.Forms.TabPage();
            this.dataFollowUp = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnResetSearch = new System.Windows.Forms.Button();
            this.btnSubmitNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtCustomerAddress = new System.Windows.Forms.Label();
            this.boxCustomerAddress = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.Label();
            this.boxCustomerName = new System.Windows.Forms.TextBox();
            this.dataJobs = new System.Windows.Forms.DataGridView();
            this.btnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabSuppliers = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabJobs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataFollowUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataJobs)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLogo
            // 
            this.imgLogo.Image = global::Homesmart_Job_Management_v2.Properties.Resources.logo_plaster_home_specialists_w_;
            this.imgLogo.Location = new System.Drawing.Point(6, 24);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(300, 48);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgLogo.TabIndex = 5;
            this.imgLogo.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabJobs);
            this.tabControl1.Controls.Add(this.tabSuppliers);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1160, 637);
            this.tabControl1.TabIndex = 6;
            // 
            // tabJobs
            // 
            this.tabJobs.Controls.Add(this.dataFollowUp);
            this.tabJobs.Controls.Add(this.imgLogo);
            this.tabJobs.Controls.Add(this.btnResetSearch);
            this.tabJobs.Controls.Add(this.btnSubmitNew);
            this.tabJobs.Controls.Add(this.btnSearch);
            this.tabJobs.Controls.Add(this.txtCustomerAddress);
            this.tabJobs.Controls.Add(this.boxCustomerAddress);
            this.tabJobs.Controls.Add(this.txtCustomerName);
            this.tabJobs.Controls.Add(this.boxCustomerName);
            this.tabJobs.Controls.Add(this.dataJobs);
            this.tabJobs.Location = new System.Drawing.Point(4, 22);
            this.tabJobs.Name = "tabJobs";
            this.tabJobs.Padding = new System.Windows.Forms.Padding(3);
            this.tabJobs.Size = new System.Drawing.Size(1152, 611);
            this.tabJobs.TabIndex = 0;
            this.tabJobs.Text = "Jobs";
            this.tabJobs.UseVisualStyleBackColor = true;
            // 
            // dataFollowUp
            // 
            this.dataFollowUp.AllowUserToAddRows = false;
            this.dataFollowUp.AllowUserToDeleteRows = false;
            this.dataFollowUp.AllowUserToResizeColumns = false;
            this.dataFollowUp.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataFollowUp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataFollowUp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataFollowUp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataFollowUp.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataFollowUp.Location = new System.Drawing.Point(687, 96);
            this.dataFollowUp.MultiSelect = false;
            this.dataFollowUp.Name = "dataFollowUp";
            this.dataFollowUp.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataFollowUp.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataFollowUp.RowHeadersVisible = false;
            this.dataFollowUp.Size = new System.Drawing.Size(459, 509);
            this.dataFollowUp.TabIndex = 23;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "Edit";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Text = "Edit";
            this.dataGridViewButtonColumn1.ToolTipText = "Edit";
            this.dataGridViewButtonColumn1.Width = 40;
            // 
            // btnResetSearch
            // 
            this.btnResetSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSearch.Location = new System.Drawing.Point(1111, 25);
            this.btnResetSearch.Name = "btnResetSearch";
            this.btnResetSearch.Size = new System.Drawing.Size(25, 26);
            this.btnResetSearch.TabIndex = 20;
            this.btnResetSearch.Text = "↻";
            this.btnResetSearch.UseVisualStyleBackColor = true;
            this.btnResetSearch.Click += new System.EventHandler(this.BtnResetSearch_Click);
            // 
            // btnSubmitNew
            // 
            this.btnSubmitNew.Enabled = false;
            this.btnSubmitNew.Location = new System.Drawing.Point(990, 52);
            this.btnSubmitNew.Name = "btnSubmitNew";
            this.btnSubmitNew.Size = new System.Drawing.Size(115, 26);
            this.btnSubmitNew.TabIndex = 19;
            this.btnSubmitNew.Text = "Create New";
            this.btnSubmitNew.UseVisualStyleBackColor = true;
            this.btnSubmitNew.Click += new System.EventHandler(this.BtnSubmitNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(990, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(115, 26);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.AutoSize = true;
            this.txtCustomerAddress.Location = new System.Drawing.Point(684, 59);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(92, 13);
            this.txtCustomerAddress.TabIndex = 22;
            this.txtCustomerAddress.Text = "Customer Address";
            // 
            // boxCustomerAddress
            // 
            this.boxCustomerAddress.Location = new System.Drawing.Point(782, 56);
            this.boxCustomerAddress.Name = "boxCustomerAddress";
            this.boxCustomerAddress.Size = new System.Drawing.Size(171, 20);
            this.boxCustomerAddress.TabIndex = 17;
            this.boxCustomerAddress.TextChanged += new System.EventHandler(this.BoxCustomerAddress_TextChanged);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.AutoSize = true;
            this.txtCustomerName.Location = new System.Drawing.Point(684, 32);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(82, 13);
            this.txtCustomerName.TabIndex = 21;
            this.txtCustomerName.Text = "Customer Name";
            // 
            // boxCustomerName
            // 
            this.boxCustomerName.Location = new System.Drawing.Point(782, 29);
            this.boxCustomerName.Name = "boxCustomerName";
            this.boxCustomerName.Size = new System.Drawing.Size(171, 20);
            this.boxCustomerName.TabIndex = 16;
            this.boxCustomerName.TextChanged += new System.EventHandler(this.BoxCustomerName_TextChanged);
            // 
            // dataJobs
            // 
            this.dataJobs.AllowUserToDeleteRows = false;
            this.dataJobs.AllowUserToOrderColumns = true;
            this.dataJobs.AllowUserToResizeColumns = false;
            this.dataJobs.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataJobs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataJobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnEdit});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataJobs.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataJobs.Location = new System.Drawing.Point(6, 96);
            this.dataJobs.MultiSelect = false;
            this.dataJobs.Name = "dataJobs";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataJobs.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataJobs.RowHeadersVisible = false;
            this.dataJobs.Size = new System.Drawing.Size(671, 509);
            this.dataJobs.TabIndex = 8;
            this.dataJobs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataJobs_ClickEdit);
            // 
            // btnEdit
            // 
            this.btnEdit.HeaderText = "Edit";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Text = "Edit";
            this.btnEdit.ToolTipText = "Edit";
            this.btnEdit.Width = 40;
            // 
            // tabSuppliers
            // 
            this.tabSuppliers.Location = new System.Drawing.Point(4, 22);
            this.tabSuppliers.Name = "tabSuppliers";
            this.tabSuppliers.Padding = new System.Windows.Forms.Padding(3);
            this.tabSuppliers.Size = new System.Drawing.Size(1152, 611);
            this.tabSuppliers.TabIndex = 1;
            this.tabSuppliers.Text = "Suppliers";
            this.tabSuppliers.UseVisualStyleBackColor = true;
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHome";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.FrmHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabJobs.ResumeLayout(false);
            this.tabJobs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataFollowUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataJobs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabJobs;
        private System.Windows.Forms.TabPage tabSuppliers;
        private System.Windows.Forms.DataGridView dataJobs;
        private System.Windows.Forms.DataGridViewButtonColumn btnEdit;
        private System.Windows.Forms.Button btnResetSearch;
        private System.Windows.Forms.Button btnSubmitNew;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label txtCustomerAddress;
        private System.Windows.Forms.TextBox boxCustomerAddress;
        private System.Windows.Forms.Label txtCustomerName;
        private System.Windows.Forms.TextBox boxCustomerName;
        private System.Windows.Forms.DataGridView dataFollowUp;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
    }
}


namespace Homesmart_Job_Management_v2
{
    partial class Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edit));
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.lblCustomerInfo = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtCustomerAddress = new System.Windows.Forms.TextBox();
            this.txtCustomerEmail = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerAddress = new System.Windows.Forms.Label();
            this.lblCustomerEmail = new System.Windows.Forms.Label();
            this.btnAddTab = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLogo
            // 
            this.imgLogo.Image = global::Homesmart_Job_Management_v2.Properties.Resources.logo_plaster_home_specialists_w_;
            this.imgLogo.Location = new System.Drawing.Point(12, 12);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(300, 48);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgLogo.TabIndex = 6;
            this.imgLogo.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(12, 165);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(660, 634);
            this.tabControl1.TabIndex = 7;
            // 
            // lblCustomerInfo
            // 
            this.lblCustomerInfo.AutoSize = true;
            this.lblCustomerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerInfo.Location = new System.Drawing.Point(12, 74);
            this.lblCustomerInfo.Name = "lblCustomerInfo";
            this.lblCustomerInfo.Size = new System.Drawing.Size(145, 25);
            this.lblCustomerInfo.TabIndex = 8;
            this.lblCustomerInfo.Text = "Customer Info";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(17, 118);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(160, 20);
            this.txtCustomerName.TabIndex = 9;
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.Location = new System.Drawing.Point(193, 118);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerAddress.TabIndex = 10;
            // 
            // txtCustomerEmail
            // 
            this.txtCustomerEmail.Location = new System.Drawing.Point(409, 118);
            this.txtCustomerEmail.Name = "txtCustomerEmail";
            this.txtCustomerEmail.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerEmail.TabIndex = 11;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.Location = new System.Drawing.Point(14, 99);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(104, 16);
            this.lblCustomerName.TabIndex = 12;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.AutoSize = true;
            this.lblCustomerAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerAddress.Location = new System.Drawing.Point(190, 99);
            this.lblCustomerAddress.Name = "lblCustomerAddress";
            this.lblCustomerAddress.Size = new System.Drawing.Size(118, 16);
            this.lblCustomerAddress.TabIndex = 13;
            this.lblCustomerAddress.Text = "Customer Address";
            // 
            // lblCustomerEmail
            // 
            this.lblCustomerEmail.AutoSize = true;
            this.lblCustomerEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerEmail.Location = new System.Drawing.Point(406, 99);
            this.lblCustomerEmail.Name = "lblCustomerEmail";
            this.lblCustomerEmail.Size = new System.Drawing.Size(101, 16);
            this.lblCustomerEmail.TabIndex = 14;
            this.lblCustomerEmail.Text = "Customer Email";
            // 
            // btnAddTab
            // 
            this.btnAddTab.Location = new System.Drawing.Point(650, 167);
            this.btnAddTab.Name = "btnAddTab";
            this.btnAddTab.Size = new System.Drawing.Size(20, 20);
            this.btnAddTab.TabIndex = 47;
            this.btnAddTab.Text = "+";
            this.btnAddTab.UseVisualStyleBackColor = true;
            this.btnAddTab.Click += new System.EventHandler(this.BtnAddTab_Click);
            // 
            // Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 811);
            this.Controls.Add(this.btnAddTab);
            this.Controls.Add(this.lblCustomerEmail);
            this.Controls.Add(this.lblCustomerAddress);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.txtCustomerEmail);
            this.Controls.Add(this.txtCustomerAddress);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.lblCustomerInfo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.imgLogo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Edit";
            this.Text = "Edit";
            this.Load += new System.EventHandler(this.Edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lblCustomerInfo;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtCustomerAddress;
        private System.Windows.Forms.TextBox txtCustomerEmail;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerAddress;
        private System.Windows.Forms.Label lblCustomerEmail;
        private System.Windows.Forms.Button btnAddTab;
    }
}
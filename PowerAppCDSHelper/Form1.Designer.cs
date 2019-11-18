namespace PowerAppCDSHelper
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Merge = new System.Windows.Forms.Button();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lst_projectDest = new System.Windows.Forms.ListBox();
            this.lst_projectSource = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Merge
            // 
            this.Merge.Location = new System.Drawing.Point(259, 354);
            this.Merge.Name = "Merge";
            this.Merge.Size = new System.Drawing.Size(75, 23);
            this.Merge.TabIndex = 0;
            this.Merge.Text = "btn_merge";
            this.Merge.UseVisualStyleBackColor = true;
            this.Merge.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(67, 26);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(207, 20);
            this.txt_pass.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_status);
            this.groupBox1.Controls.Add(this.btn_login);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_pass);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 92);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Access Credential ";
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(210, 59);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 23);
            this.btn_login.TabIndex = 5;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(8, 59);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(57, 13);
            this.lbl_status.TabIndex = 0;
            this.lbl_status.Text = "[lbl_status]";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lst_projectDest);
            this.groupBox3.Controls.Add(this.lst_projectSource);
            this.groupBox3.Controls.Add(this.Merge);
            this.groupBox3.Location = new System.Drawing.Point(12, 110);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(604, 409);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Projects";
            // 
            // lst_projectDest
            // 
            this.lst_projectDest.FormattingEnabled = true;
            this.lst_projectDest.Location = new System.Drawing.Point(308, 19);
            this.lst_projectDest.Name = "lst_projectDest";
            this.lst_projectDest.Size = new System.Drawing.Size(274, 329);
            this.lst_projectDest.TabIndex = 1;
            // 
            // lst_projectSource
            // 
            this.lst_projectSource.FormattingEnabled = true;
            this.lst_projectSource.Location = new System.Drawing.Point(11, 19);
            this.lst_projectSource.Name = "lst_projectSource";
            this.lst_projectSource.Size = new System.Drawing.Size(274, 329);
            this.lst_projectSource.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 529);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PowerApps CDS Helper";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Merge;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lst_projectSource;
        private System.Windows.Forms.ListBox lst_projectDest;
    }
}


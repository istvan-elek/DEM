namespace DCAnalyser
{
    partial class frmSelectGeneration
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCompare = new System.Windows.Forms.ComboBox();
            this.cmbGenerationNumber = new System.Windows.Forms.ComboBox();
            this.bttnOK = new System.Windows.Forms.Button();
            this.bttnClose = new System.Windows.Forms.Button();
            this.chbDisplayData = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Give a generation ";
            // 
            // cmbCompare
            // 
            this.cmbCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompare.FormattingEnabled = true;
            this.cmbCompare.Items.AddRange(new object[] {
            "=",
            "!=",
            "<",
            "<=",
            ">",
            ">="});
            this.cmbCompare.Location = new System.Drawing.Point(206, 6);
            this.cmbCompare.Name = "cmbCompare";
            this.cmbCompare.Size = new System.Drawing.Size(45, 21);
            this.cmbCompare.TabIndex = 1;
            this.cmbCompare.Visible = false;
            this.cmbCompare.SelectedIndexChanged += new System.EventHandler(this.cmbCompare_SelectedIndexChanged);
            // 
            // cmbGenerationNumber
            // 
            this.cmbGenerationNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenerationNumber.FormattingEnabled = true;
            this.cmbGenerationNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmbGenerationNumber.Location = new System.Drawing.Point(113, 6);
            this.cmbGenerationNumber.Name = "cmbGenerationNumber";
            this.cmbGenerationNumber.Size = new System.Drawing.Size(56, 21);
            this.cmbGenerationNumber.TabIndex = 2;
            this.cmbGenerationNumber.SelectedIndexChanged += new System.EventHandler(this.cmbGenerationNumber_SelectedIndexChanged);
            // 
            // bttnOK
            // 
            this.bttnOK.Location = new System.Drawing.Point(175, 59);
            this.bttnOK.Name = "bttnOK";
            this.bttnOK.Size = new System.Drawing.Size(75, 23);
            this.bttnOK.TabIndex = 5;
            this.bttnOK.Text = "OK";
            this.bttnOK.UseVisualStyleBackColor = true;
            this.bttnOK.Click += new System.EventHandler(this.bttnOK_Click);
            // 
            // bttnClose
            // 
            this.bttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnClose.Location = new System.Drawing.Point(12, 59);
            this.bttnClose.Name = "bttnClose";
            this.bttnClose.Size = new System.Drawing.Size(75, 23);
            this.bttnClose.TabIndex = 6;
            this.bttnClose.Text = "Close";
            this.bttnClose.UseVisualStyleBackColor = true;
            this.bttnClose.Click += new System.EventHandler(this.bttnClose_Click);
            // 
            // chbDisplayData
            // 
            this.chbDisplayData.AutoSize = true;
            this.chbDisplayData.Location = new System.Drawing.Point(15, 30);
            this.chbDisplayData.Name = "chbDisplayData";
            this.chbDisplayData.Size = new System.Drawing.Size(127, 17);
            this.chbDisplayData.TabIndex = 7;
            this.chbDisplayData.Text = "Show result in a table";
            this.chbDisplayData.UseVisualStyleBackColor = true;
            this.chbDisplayData.Visible = false;
            this.chbDisplayData.CheckedChanged += new System.EventHandler(this.chbDisplayData_CheckedChanged);
            // 
            // frmSelectGeneration
            // 
            this.AcceptButton = this.bttnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnClose;
            this.ClientSize = new System.Drawing.Size(263, 94);
            this.Controls.Add(this.chbDisplayData);
            this.Controls.Add(this.bttnClose);
            this.Controls.Add(this.bttnOK);
            this.Controls.Add(this.cmbGenerationNumber);
            this.Controls.Add(this.cmbCompare);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSelectGeneration";
            this.Text = "Select a generation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCompare;
        private System.Windows.Forms.ComboBox cmbGenerationNumber;
        private System.Windows.Forms.Button bttnOK;
        private System.Windows.Forms.Button bttnClose;
        private System.Windows.Forms.CheckBox chbDisplayData;
    }
}
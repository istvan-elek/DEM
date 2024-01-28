namespace DCMaster
{
    partial class frmConfig
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
            this.bttnSave = new System.Windows.Forms.Button();
            this.bttnQuit = new System.Windows.Forms.Button();
            this.tbConfig = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bttnSave
            // 
            this.bttnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnSave.Location = new System.Drawing.Point(146, 133);
            this.bttnSave.Name = "bttnSave";
            this.bttnSave.Size = new System.Drawing.Size(92, 26);
            this.bttnSave.TabIndex = 0;
            this.bttnSave.Text = "Save changes";
            this.bttnSave.UseVisualStyleBackColor = true;
            this.bttnSave.Click += new System.EventHandler(this.bttnSave_Click);
            // 
            // bttnQuit
            // 
            this.bttnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnQuit.Location = new System.Drawing.Point(12, 133);
            this.bttnQuit.Name = "bttnQuit";
            this.bttnQuit.Size = new System.Drawing.Size(104, 26);
            this.bttnQuit.TabIndex = 1;
            this.bttnQuit.Text = "Quit without save";
            this.bttnQuit.UseVisualStyleBackColor = true;
            this.bttnQuit.Click += new System.EventHandler(this.bttnQuit_Click);
            // 
            // tbConfig
            // 
            this.tbConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConfig.Location = new System.Drawing.Point(12, 12);
            this.tbConfig.Multiline = true;
            this.tbConfig.Name = "tbConfig";
            this.tbConfig.Size = new System.Drawing.Size(228, 107);
            this.tbConfig.TabIndex = 2;
            // 
            // frmConfig
            // 
            this.AcceptButton = this.bttnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnQuit;
            this.ClientSize = new System.Drawing.Size(252, 168);
            this.Controls.Add(this.tbConfig);
            this.Controls.Add(this.bttnQuit);
            this.Controls.Add(this.bttnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.Text = "Config file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnSave;
        private System.Windows.Forms.Button bttnQuit;
        private System.Windows.Forms.TextBox tbConfig;
    }
}
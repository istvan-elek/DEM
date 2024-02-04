namespace DCAnalyser
{
    partial class frmCompareImprints
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
            this.bttnCompare = new System.Windows.Forms.Button();
            this.bttnLoad1 = new System.Windows.Forms.Button();
            this.bttnLoad2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bttnCompare
            // 
            this.bttnCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bttnCompare.Location = new System.Drawing.Point(138, 12);
            this.bttnCompare.Name = "bttnCompare";
            this.bttnCompare.Size = new System.Drawing.Size(75, 61);
            this.bttnCompare.TabIndex = 4;
            this.bttnCompare.Text = "Compare";
            this.bttnCompare.UseVisualStyleBackColor = true;
            this.bttnCompare.Click += new System.EventHandler(this.bttnCompare_Click);
            // 
            // bttnLoad1
            // 
            this.bttnLoad1.Location = new System.Drawing.Point(12, 12);
            this.bttnLoad1.Name = "bttnLoad1";
            this.bttnLoad1.Size = new System.Drawing.Size(111, 23);
            this.bttnLoad1.TabIndex = 5;
            this.bttnLoad1.Text = "Load#1 database";
            this.bttnLoad1.UseVisualStyleBackColor = true;
            this.bttnLoad1.Click += new System.EventHandler(this.bttnLoadDB_Click);
            // 
            // bttnLoad2
            // 
            this.bttnLoad2.Location = new System.Drawing.Point(12, 50);
            this.bttnLoad2.Name = "bttnLoad2";
            this.bttnLoad2.Size = new System.Drawing.Size(111, 23);
            this.bttnLoad2.TabIndex = 6;
            this.bttnLoad2.Text = "Load#2 database";
            this.bttnLoad2.UseVisualStyleBackColor = true;
            this.bttnLoad2.Click += new System.EventHandler(this.bttnLoadDB_Click);
            // 
            // frmCompareImprints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 83);
            this.Controls.Add(this.bttnLoad2);
            this.Controls.Add(this.bttnLoad1);
            this.Controls.Add(this.bttnCompare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCompareImprints";
            this.Text = "Compare";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCompareImprints_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bttnCompare;
        private System.Windows.Forms.Button bttnLoad1;
        private System.Windows.Forms.Button bttnLoad2;
    }
}
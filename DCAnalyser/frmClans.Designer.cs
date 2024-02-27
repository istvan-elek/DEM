namespace DCAnalyser
{
    partial class frmClans
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
            this.hScrollB = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // hScrollB
            // 
            this.hScrollB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollB.Location = new System.Drawing.Point(0, 486);
            this.hScrollB.Name = "hScrollB";
            this.hScrollB.Size = new System.Drawing.Size(793, 17);
            this.hScrollB.TabIndex = 0;
            this.hScrollB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollB_Scroll);
            // 
            // frmClans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 503);
            this.Controls.Add(this.hScrollB);
            this.Name = "frmClans";
            this.Text = "Godfathers and their members\' count";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmClans_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollB;
    }
}
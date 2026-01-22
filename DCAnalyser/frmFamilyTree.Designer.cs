namespace DCAnalyser
{
    partial class frmFamilyTree
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
            this.vScrollB = new System.Windows.Forms.VScrollBar();
            this.hScrollB = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // vScrollB
            // 
            this.vScrollB.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollB.Location = new System.Drawing.Point(771, 0);
            this.vScrollB.Name = "vScrollB";
            this.vScrollB.Size = new System.Drawing.Size(17, 538);
            this.vScrollB.TabIndex = 0;
            this.vScrollB.Visible = false;
            // 
            // hScrollB
            // 
            this.hScrollB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollB.Location = new System.Drawing.Point(0, 538);
            this.hScrollB.Name = "hScrollB";
            this.hScrollB.Size = new System.Drawing.Size(788, 17);
            this.hScrollB.TabIndex = 1;
            this.hScrollB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollB_Scroll);
            // 
            // frmFamilyTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(788, 555);
            this.Controls.Add(this.vScrollB);
            this.Controls.Add(this.hScrollB);
            this.Name = "frmFamilyTree";
            this.Text = "Family tree";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawParents);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmFamilyTree_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmFamilyTree_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollB;
        private System.Windows.Forms.HScrollBar hScrollB;
    }
}
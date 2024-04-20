namespace DC
{
    partial class frmGuide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGuide));
            this.webBrowserGuide = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabUsersGuide = new System.Windows.Forms.TabPage();
            this.tabTheory = new System.Windows.Forms.TabPage();
            this.webBrowserTheory = new System.Windows.Forms.WebBrowser();
            this.tabControl1.SuspendLayout();
            this.tabUsersGuide.SuspendLayout();
            this.tabTheory.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowserGuide
            // 
            this.webBrowserGuide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserGuide.Location = new System.Drawing.Point(3, 3);
            this.webBrowserGuide.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserGuide.Name = "webBrowserGuide";
            this.webBrowserGuide.Size = new System.Drawing.Size(750, 544);
            this.webBrowserGuide.TabIndex = 0;
            this.webBrowserGuide.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabUsersGuide);
            this.tabControl1.Controls.Add(this.tabTheory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(764, 576);
            this.tabControl1.TabIndex = 1;
            // 
            // tabUsersGuide
            // 
            this.tabUsersGuide.Controls.Add(this.webBrowserGuide);
            this.tabUsersGuide.Location = new System.Drawing.Point(4, 22);
            this.tabUsersGuide.Name = "tabUsersGuide";
            this.tabUsersGuide.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsersGuide.Size = new System.Drawing.Size(756, 550);
            this.tabUsersGuide.TabIndex = 0;
            this.tabUsersGuide.Text = "Users\' Guide";
            this.tabUsersGuide.UseVisualStyleBackColor = true;
            // 
            // tabTheory
            // 
            this.tabTheory.Controls.Add(this.webBrowserTheory);
            this.tabTheory.Location = new System.Drawing.Point(4, 22);
            this.tabTheory.Name = "tabTheory";
            this.tabTheory.Padding = new System.Windows.Forms.Padding(3);
            this.tabTheory.Size = new System.Drawing.Size(756, 550);
            this.tabTheory.TabIndex = 1;
            this.tabTheory.Text = "Theory";
            this.tabTheory.UseVisualStyleBackColor = true;
            // 
            // webBrowserTheory
            // 
            this.webBrowserTheory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserTheory.Location = new System.Drawing.Point(3, 3);
            this.webBrowserTheory.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserTheory.Name = "webBrowserTheory";
            this.webBrowserTheory.Size = new System.Drawing.Size(750, 544);
            this.webBrowserTheory.TabIndex = 1;
            this.webBrowserTheory.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // frmGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 576);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGuide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guide";
            this.tabControl1.ResumeLayout(false);
            this.tabUsersGuide.ResumeLayout(false);
            this.tabTheory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserGuide;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabUsersGuide;
        private System.Windows.Forms.TabPage tabTheory;
        private System.Windows.Forms.WebBrowser webBrowserTheory;
    }
}
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTheory = new System.Windows.Forms.TabPage();
            this.tabUsersGuide = new System.Windows.Forms.TabPage();
            this.webBrowserGuide = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webBrowserTheory = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.tabControl1.SuspendLayout();
            this.tabTheory.SuspendLayout();
            this.tabUsersGuide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowserGuide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowserTheory)).BeginInit();
            this.SuspendLayout();
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
            // webBrowserGuide
            // 
            this.webBrowserGuide.AllowExternalDrop = true;
            this.webBrowserGuide.CreationProperties = null;
            this.webBrowserGuide.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webBrowserGuide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserGuide.Location = new System.Drawing.Point(3, 3);
            this.webBrowserGuide.Name = "webBrowserGuide";
            this.webBrowserGuide.Size = new System.Drawing.Size(750, 544);
            this.webBrowserGuide.TabIndex = 0;
            this.webBrowserGuide.ZoomFactor = 1D;
            // 
            // webBrowserTheory
            // 
            this.webBrowserTheory.AllowExternalDrop = true;
            this.webBrowserTheory.CreationProperties = null;
            this.webBrowserTheory.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webBrowserTheory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserTheory.Location = new System.Drawing.Point(3, 3);
            this.webBrowserTheory.Name = "webBrowserTheory";
            this.webBrowserTheory.Size = new System.Drawing.Size(750, 544);
            this.webBrowserTheory.TabIndex = 0;
            this.webBrowserTheory.ZoomFactor = 1D;
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
            this.tabTheory.ResumeLayout(false);
            this.tabUsersGuide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webBrowserGuide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowserTheory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTheory;
        private System.Windows.Forms.TabPage tabUsersGuide;
        private Microsoft.Web.WebView2.WinForms.WebView2 webBrowserGuide;
        private Microsoft.Web.WebView2.WinForms.WebView2 webBrowserTheory;
    }
}
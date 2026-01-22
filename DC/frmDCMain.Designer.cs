namespace DC
{
    partial class frmDCMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDCMain));
            this.bttnAnalyser = new System.Windows.Forms.Button();
            this.bttnMaster = new System.Windows.Forms.Button();
            this.bttnDemo = new System.Windows.Forms.Button();
            this.bttnGuide = new System.Windows.Forms.Button();
            this.bttnAbout = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.bttnMinimize = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bttnAnalyser
            // 
            this.bttnAnalyser.Image = ((System.Drawing.Image)(resources.GetObject("bttnAnalyser.Image")));
            this.bttnAnalyser.Location = new System.Drawing.Point(20, 223);
            this.bttnAnalyser.Name = "bttnAnalyser";
            this.bttnAnalyser.Size = new System.Drawing.Size(85, 91);
            this.bttnAnalyser.TabIndex = 0;
            this.bttnAnalyser.Text = "DC analyser";
            this.bttnAnalyser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnAnalyser.UseVisualStyleBackColor = true;
            this.bttnAnalyser.Click += new System.EventHandler(this.bttnAnalyser_Click);
            // 
            // bttnMaster
            // 
            this.bttnMaster.Image = ((System.Drawing.Image)(resources.GetObject("bttnMaster.Image")));
            this.bttnMaster.Location = new System.Drawing.Point(20, 126);
            this.bttnMaster.Name = "bttnMaster";
            this.bttnMaster.Size = new System.Drawing.Size(85, 91);
            this.bttnMaster.TabIndex = 1;
            this.bttnMaster.Text = "DC master";
            this.bttnMaster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnMaster.UseVisualStyleBackColor = true;
            this.bttnMaster.Click += new System.EventHandler(this.bttnMaster_Click);
            // 
            // bttnDemo
            // 
            this.bttnDemo.Image = ((System.Drawing.Image)(resources.GetObject("bttnDemo.Image")));
            this.bttnDemo.Location = new System.Drawing.Point(20, 29);
            this.bttnDemo.Name = "bttnDemo";
            this.bttnDemo.Size = new System.Drawing.Size(85, 91);
            this.bttnDemo.TabIndex = 2;
            this.bttnDemo.Text = "DC demo";
            this.bttnDemo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnDemo.UseVisualStyleBackColor = true;
            this.bttnDemo.Click += new System.EventHandler(this.bttnDemo_Click);
            // 
            // bttnGuide
            // 
            this.bttnGuide.Image = ((System.Drawing.Image)(resources.GetObject("bttnGuide.Image")));
            this.bttnGuide.Location = new System.Drawing.Point(20, 320);
            this.bttnGuide.Name = "bttnGuide";
            this.bttnGuide.Size = new System.Drawing.Size(85, 95);
            this.bttnGuide.TabIndex = 3;
            this.bttnGuide.Text = "Guide";
            this.bttnGuide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnGuide.UseVisualStyleBackColor = true;
            this.bttnGuide.Click += new System.EventHandler(this.bttnGuide_Click);
            // 
            // bttnAbout
            // 
            this.bttnAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bttnAbout.Image = ((System.Drawing.Image)(resources.GetObject("bttnAbout.Image")));
            this.bttnAbout.Location = new System.Drawing.Point(20, 425);
            this.bttnAbout.Name = "bttnAbout";
            this.bttnAbout.Size = new System.Drawing.Size(85, 76);
            this.bttnAbout.TabIndex = 4;
            this.bttnAbout.Text = "Info about DC";
            this.bttnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnAbout.UseVisualStyleBackColor = true;
            this.bttnAbout.Click += new System.EventHandler(this.bttnAbout_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.bttnMinimize});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(125, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "DEM centre";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel2.Text = "     ";
            // 
            // bttnMinimize
            // 
            this.bttnMinimize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("bttnMinimize.Image")));
            this.bttnMinimize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttnMinimize.Name = "bttnMinimize";
            this.bttnMinimize.Size = new System.Drawing.Size(24, 24);
            this.bttnMinimize.Text = "toolStripButton1";
            this.bttnMinimize.ToolTipText = "Minimize";
            this.bttnMinimize.Visible = false;
            this.bttnMinimize.Click += new System.EventHandler(this.bttnMinimize_Click);
            this.bttnMinimize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bttnMinimize_MouseMove);
            // 
            // frmDCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(125, 511);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.bttnAbout);
            this.Controls.Add(this.bttnGuide);
            this.Controls.Add(this.bttnDemo);
            this.Controls.Add(this.bttnMaster);
            this.Controls.Add(this.bttnAnalyser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 50);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDCMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDCMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnAnalyser;
        private System.Windows.Forms.Button bttnMaster;
        private System.Windows.Forms.Button bttnDemo;
        private System.Windows.Forms.Button bttnGuide;
        private System.Windows.Forms.Button bttnAbout;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton bttnMinimize;
    }
}


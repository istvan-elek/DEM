
namespace DCMaster
{
    partial class frmPicture2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPicture2));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbtnHand = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPan = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomFull = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslblZoomRate = new System.Windows.Forms.ToolStripLabel();
            this.tstbZoomRate = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnChangeColor = new System.Windows.Forms.ToolStripButton();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnZoomIn,
            this.tsbtnZoomOut,
            this.tsbtnHand,
            this.tsbtnPan,
            this.tsbtnZoomFull,
            this.toolStripSeparator1,
            this.tslblZoomRate,
            this.tstbZoomRate,
            this.tsbtnChangeColor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(505, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnZoomIn
            // 
            this.tsbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomIn.Image")));
            this.tsbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomIn.Name = "tsbtnZoomIn";
            this.tsbtnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomIn.Text = "Zoom in";
            this.tsbtnZoomIn.Click += new System.EventHandler(this.tsbtnZoomIn_Click);
            // 
            // tsbtnZoomOut
            // 
            this.tsbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomOut.Image")));
            this.tsbtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomOut.Name = "tsbtnZoomOut";
            this.tsbtnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomOut.Text = "Zoom out";
            this.tsbtnZoomOut.Click += new System.EventHandler(this.tsbtnZoomOut_Click);
            // 
            // tsbtnHand
            // 
            this.tsbtnHand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnHand.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnHand.Image")));
            this.tsbtnHand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHand.Name = "tsbtnHand";
            this.tsbtnHand.Size = new System.Drawing.Size(23, 22);
            this.tsbtnHand.Text = "Scroll image";
            this.tsbtnHand.Click += new System.EventHandler(this.tsbtnHand_Click);
            // 
            // tsbtnPan
            // 
            this.tsbtnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPan.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPan.Image")));
            this.tsbtnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPan.Name = "tsbtnPan";
            this.tsbtnPan.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPan.Text = "Pan image";
            this.tsbtnPan.Click += new System.EventHandler(this.tsbtnPan_Click);
            // 
            // tsbtnZoomFull
            // 
            this.tsbtnZoomFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomFull.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomFull.Image")));
            this.tsbtnZoomFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomFull.Name = "tsbtnZoomFull";
            this.tsbtnZoomFull.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomFull.Text = "Zoom to full extent";
            this.tsbtnZoomFull.Click += new System.EventHandler(this.tsbtnZoomFull_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslblZoomRate
            // 
            this.tslblZoomRate.Name = "tslblZoomRate";
            this.tslblZoomRate.Size = new System.Drawing.Size(63, 22);
            this.tslblZoomRate.Text = "zoom rate:";
            // 
            // tstbZoomRate
            // 
            this.tstbZoomRate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbZoomRate.Name = "tstbZoomRate";
            this.tstbZoomRate.Size = new System.Drawing.Size(40, 25);
            this.tstbZoomRate.Text = "10";
            this.tstbZoomRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstbZoomRate_KeyDown);
            // 
            // tsbtnChangeColor
            // 
            this.tsbtnChangeColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnChangeColor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnChangeColor.Image")));
            this.tsbtnChangeColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnChangeColor.Name = "tsbtnChangeColor";
            this.tsbtnChangeColor.Size = new System.Drawing.Size(23, 22);
            this.tsbtnChangeColor.Text = "Change to Green";
            this.tsbtnChangeColor.Visible = false;
            this.tsbtnChangeColor.Click += new System.EventHandler(this.tsbtnChangeColor_Click);
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.Color.Black;
            this.picBox.Location = new System.Drawing.Point(3, 3);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(500, 500);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseMove);
            this.picBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 503);
            this.panel1.TabIndex = 2;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // frmPicture2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 528);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPicture2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPicture2";
            this.TopMost = true;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnZoomIn;
        private System.Windows.Forms.ToolStripButton tsbtnZoomOut;
        private System.Windows.Forms.ToolStripButton tsbtnZoomFull;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tsbtnPan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslblZoomRate;
        private System.Windows.Forms.ToolStripTextBox tstbZoomRate;
        private System.Windows.Forms.ToolStripButton tsbtnHand;
        private System.Windows.Forms.ToolStripButton tsbtnChangeColor;
    }
}
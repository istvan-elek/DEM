namespace DCAnalyser
{
    partial class frmSql
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSql));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvSql = new System.Windows.Forms.DataGridView();
            this.bnSql = new System.Windows.Forms.BindingNavigator(this.components);
            this.bsSql = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSql = new System.Windows.Forms.TextBox();
            this.cmbSql = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bttnLoadSavedSql = new System.Windows.Forms.Button();
            this.bttnSaveSqlCommand = new System.Windows.Forms.Button();
            this.menuClans = new System.Windows.Forms.MenuStrip();
            this.showWorkersTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showParentsGraphicallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showClansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largestClansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSql)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnSql)).BeginInit();
            this.bnSql.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSql)).BeginInit();
            this.menuClans.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvSql);
            this.panel1.Controls.Add(this.bnSql);
            this.panel1.Location = new System.Drawing.Point(0, 166);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 431);
            this.panel1.TabIndex = 0;
            // 
            // dgvSql
            // 
            this.dgvSql.AllowUserToAddRows = false;
            this.dgvSql.AllowUserToDeleteRows = false;
            this.dgvSql.AllowUserToResizeRows = false;
            this.dgvSql.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSql.Location = new System.Drawing.Point(0, 25);
            this.dgvSql.Name = "dgvSql";
            this.dgvSql.Size = new System.Drawing.Size(702, 406);
            this.dgvSql.TabIndex = 0;
            this.dgvSql.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSql_CellMouseDown);
            // 
            // bnSql
            // 
            this.bnSql.AddNewItem = null;
            this.bnSql.BindingSource = this.bsSql;
            this.bnSql.CountItem = this.bindingNavigatorCountItem;
            this.bnSql.DeleteItem = null;
            this.bnSql.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bnSql.Location = new System.Drawing.Point(0, 0);
            this.bnSql.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnSql.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnSql.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnSql.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnSql.Name = "bnSql";
            this.bnSql.PositionItem = this.bindingNavigatorPositionItem;
            this.bnSql.Size = new System.Drawing.Size(702, 25);
            this.bnSql.TabIndex = 6;
            this.bnSql.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sql command --->";
            // 
            // tbSql
            // 
            this.tbSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSql.Location = new System.Drawing.Point(111, 6);
            this.tbSql.Multiline = true;
            this.tbSql.Name = "tbSql";
            this.tbSql.Size = new System.Drawing.Size(580, 98);
            this.tbSql.TabIndex = 2;
            this.tbSql.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSql_KeyDown);
            // 
            // cmbSql
            // 
            this.cmbSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSql.FormattingEnabled = true;
            this.cmbSql.Location = new System.Drawing.Point(139, 110);
            this.cmbSql.Name = "cmbSql";
            this.cmbSql.Size = new System.Drawing.Size(552, 21);
            this.cmbSql.TabIndex = 3;
            this.cmbSql.SelectedIndexChanged += new System.EventHandler(this.cmbSql_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Previous SQL commands:";
            // 
            // bttnLoadSavedSql
            // 
            this.bttnLoadSavedSql.Location = new System.Drawing.Point(7, 42);
            this.bttnLoadSavedSql.Name = "bttnLoadSavedSql";
            this.bttnLoadSavedSql.Size = new System.Drawing.Size(98, 23);
            this.bttnLoadSavedSql.TabIndex = 6;
            this.bttnLoadSavedSql.Text = "Load saved Sql";
            this.bttnLoadSavedSql.UseVisualStyleBackColor = true;
            this.bttnLoadSavedSql.Click += new System.EventHandler(this.bttnLoadSavedSql_Click);
            // 
            // bttnSaveSqlCommand
            // 
            this.bttnSaveSqlCommand.Location = new System.Drawing.Point(7, 71);
            this.bttnSaveSqlCommand.Name = "bttnSaveSqlCommand";
            this.bttnSaveSqlCommand.Size = new System.Drawing.Size(98, 23);
            this.bttnSaveSqlCommand.TabIndex = 7;
            this.bttnSaveSqlCommand.Text = "Save command";
            this.bttnSaveSqlCommand.UseVisualStyleBackColor = true;
            this.bttnSaveSqlCommand.Click += new System.EventHandler(this.bttnSaveSqlCommand_Click);
            // 
            // menuClans
            // 
            this.menuClans.Dock = System.Windows.Forms.DockStyle.None;
            this.menuClans.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWorkersTreeToolStripMenuItem,
            this.showParentsGraphicallyToolStripMenuItem,
            this.showClansToolStripMenuItem});
            this.menuClans.Location = new System.Drawing.Point(0, 134);
            this.menuClans.Name = "menuClans";
            this.menuClans.Size = new System.Drawing.Size(355, 24);
            this.menuClans.TabIndex = 9;
            this.menuClans.Text = "menuStrip1";
            // 
            // showWorkersTreeToolStripMenuItem
            // 
            this.showWorkersTreeToolStripMenuItem.Name = "showWorkersTreeToolStripMenuItem";
            this.showWorkersTreeToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.showWorkersTreeToolStripMenuItem.Text = "Show workers\' tree";
            this.showWorkersTreeToolStripMenuItem.Click += new System.EventHandler(this.showWorkersTreeToolStripMenuItem_Click);
            // 
            // showParentsGraphicallyToolStripMenuItem
            // 
            this.showParentsGraphicallyToolStripMenuItem.Name = "showParentsGraphicallyToolStripMenuItem";
            this.showParentsGraphicallyToolStripMenuItem.Size = new System.Drawing.Size(151, 20);
            this.showParentsGraphicallyToolStripMenuItem.Text = "Show parents graphically";
            this.showParentsGraphicallyToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuShowGraphically_Click);
            // 
            // showClansToolStripMenuItem
            // 
            this.showClansToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.largestClansToolStripMenuItem});
            this.showClansToolStripMenuItem.Name = "showClansToolStripMenuItem";
            this.showClansToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.showClansToolStripMenuItem.Text = "Show clans";
            // 
            // largestClansToolStripMenuItem
            // 
            this.largestClansToolStripMenuItem.Name = "largestClansToolStripMenuItem";
            this.largestClansToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.largestClansToolStripMenuItem.Text = "Largest clans";
            this.largestClansToolStripMenuItem.Click += new System.EventHandler(this.largestClansToolStripMenuItem_Click);
            // 
            // frmSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 598);
            this.Controls.Add(this.bttnSaveSqlCommand);
            this.Controls.Add(this.bttnLoadSavedSql);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSql);
            this.Controls.Add(this.tbSql);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuClans);
            this.MainMenuStrip = this.menuClans;
            this.Name = "frmSql";
            this.Text = "Sql window";
            this.Load += new System.EventHandler(this.frmSql_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSql)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnSql)).EndInit();
            this.bnSql.ResumeLayout(false);
            this.bnSql.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSql)).EndInit();
            this.menuClans.ResumeLayout(false);
            this.menuClans.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSql;
        private System.Windows.Forms.ComboBox cmbSql;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingNavigator bnSql;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Button bttnLoadSavedSql;
        private System.Windows.Forms.Button bttnSaveSqlCommand;
        public System.Windows.Forms.BindingSource bsSql;
        public System.Windows.Forms.DataGridView dgvSql;
        private System.Windows.Forms.MenuStrip menuClans;
        private System.Windows.Forms.ToolStripMenuItem showClansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largestClansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showWorkersTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showParentsGraphicallyToolStripMenuItem;
    }
}
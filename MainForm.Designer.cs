namespace NeoFlyExport
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sfdGPX = new System.Windows.Forms.SaveFileDialog();
            this.toolStripBottom = new System.Windows.Forms.ToolStrip();
            this.tsbExportGPX = new System.Windows.Forms.ToolStripButton();
            this.tsddbExportExternalViewer = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectNone = new System.Windows.Forms.ToolStripButton();
            this.tspbLoad = new System.Windows.Forms.ToolStripProgressBar();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.toolStripBottom.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // sfdGPX
            // 
            this.sfdGPX.DefaultExt = "gpx";
            this.sfdGPX.Filter = "GPX files|*.gpx";
            // 
            // toolStripBottom
            // 
            this.toolStripBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripBottom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripBottom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExportGPX,
            this.tsddbExportExternalViewer,
            this.tsbSettings});
            this.toolStripBottom.Location = new System.Drawing.Point(0, 670);
            this.toolStripBottom.Name = "toolStripBottom";
            this.toolStripBottom.Padding = new System.Windows.Forms.Padding(5);
            this.toolStripBottom.Size = new System.Drawing.Size(602, 34);
            this.toolStripBottom.TabIndex = 4;
            this.toolStripBottom.Text = "toolStrip1";
            // 
            // tsbExportGPX
            // 
            this.tsbExportGPX.Image = global::NeoFlyExport.Properties.Resources.export_16;
            this.tsbExportGPX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportGPX.Name = "tsbExportGPX";
            this.tsbExportGPX.Size = new System.Drawing.Size(131, 21);
            this.tsbExportGPX.Text = "Export to GPX file";
            this.tsbExportGPX.Click += new System.EventHandler(this.tsbExportGPX_Click);
            // 
            // tsddbExportExternalViewer
            // 
            this.tsddbExportExternalViewer.Image = global::NeoFlyExport.Properties.Resources.route_16;
            this.tsddbExportExternalViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbExportExternalViewer.Name = "tsddbExportExternalViewer";
            this.tsddbExportExternalViewer.Size = new System.Drawing.Size(185, 21);
            this.tsddbExportExternalViewer.Text = "Export to external viewer";
            this.tsddbExportExternalViewer.Visible = false;
            this.tsddbExportExternalViewer.ButtonClick += new System.EventHandler(this.tsddbExportExternalViewer_ButtonClick);
            this.tsddbExportExternalViewer.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbExportExternalViewer_DropDownItemClicked);
            // 
            // tsbSettings
            // 
            this.tsbSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSettings.Image = global::NeoFlyExport.Properties.Resources.settings_16;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(74, 21);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // toolStripTop
            // 
            this.toolStripTop.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelectAll,
            this.tsbSelectNone,
            this.tspbLoad});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(5);
            this.toolStripTop.Size = new System.Drawing.Size(602, 34);
            this.toolStripTop.TabIndex = 5;
            // 
            // tsbSelectAll
            // 
            this.tsbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectAll.Name = "tsbSelectAll";
            this.tsbSelectAll.Size = new System.Drawing.Size(63, 21);
            this.tsbSelectAll.Text = "Select all";
            this.tsbSelectAll.Click += new System.EventHandler(this.tsbSelectAll_Click);
            // 
            // tsbSelectNone
            // 
            this.tsbSelectNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSelectNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectNone.Name = "tsbSelectNone";
            this.tsbSelectNone.Size = new System.Drawing.Size(79, 21);
            this.tsbSelectNone.Text = "Select none";
            this.tsbSelectNone.Click += new System.EventHandler(this.tsbSelectNone_Click);
            // 
            // tspbLoad
            // 
            this.tspbLoad.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspbLoad.Name = "tspbLoad";
            this.tspbLoad.Size = new System.Drawing.Size(200, 21);
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Location = new System.Drawing.Point(0, 34);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowTemplate.Height = 25;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(602, 636);
            this.dgvLog.TabIndex = 7;
            this.dgvLog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLog_CellContentClick);
            this.dgvLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLog_CellDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(602, 704);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.toolStripTop);
            this.Controls.Add(this.toolStripBottom);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "NeoFlyExport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStripBottom.ResumeLayout(false);
            this.toolStripBottom.PerformLayout();
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog sfdGPX;
        private System.Windows.Forms.ToolStrip toolStripBottom;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripButton tsbSelectAll;
        private System.Windows.Forms.ToolStripButton tsbSelectNone;
        private System.Windows.Forms.ToolStripProgressBar tspbLoad;
        private System.Windows.Forms.ToolStripButton tsbExportGPX;
        private System.Windows.Forms.ToolStripSplitButton tsddbExportExternalViewer;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.DataGridView dgvLog;
    }
}


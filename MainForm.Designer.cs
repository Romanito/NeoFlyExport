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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.btnExportGPX = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfdGPX
            // 
            this.sfdGPX.DefaultExt = "gpx";
            this.sfdGPX.Filter = "GPX files|*.gpx";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.dgvLog, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnExportGPX, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(498, 575);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Location = new System.Drawing.Point(3, 34);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowTemplate.Height = 25;
            this.dgvLog.Size = new System.Drawing.Size(492, 501);
            this.dgvLog.TabIndex = 1;
            // 
            // btnExportGPX
            // 
            this.btnExportGPX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportGPX.Enabled = false;
            this.btnExportGPX.Location = new System.Drawing.Point(3, 542);
            this.btnExportGPX.Name = "btnExportGPX";
            this.btnExportGPX.Size = new System.Drawing.Size(134, 30);
            this.btnExportGPX.TabIndex = 2;
            this.btnExportGPX.Text = "Export to GPX file";
            this.btnExportGPX.UseVisualStyleBackColor = true;
            this.btnExportGPX.Click += new System.EventHandler(this.btnExportGPX_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectNone);
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 31);
            this.panel1.TabIndex = 3;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(3, 5);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(84, 5);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(75, 23);
            this.btnSelectNone.TabIndex = 5;
            this.btnSelectNone.Text = "Select none";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 575);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "NeoFlyExport";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog sfdGPX;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Button btnExportGPX;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnSelectAll;
    }
}



namespace NeoFlyExport
{
    partial class SettingsForm
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
            this.pButtons = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbExternalViewers = new System.Windows.Forms.GroupBox();
            this.lblExternalViewers = new System.Windows.Forms.Label();
            this.txtExternalViewers = new System.Windows.Forms.TextBox();
            this.lblDoubleClick = new System.Windows.Forms.Label();
            this.pButtons.SuspendLayout();
            this.gbExternalViewers.SuspendLayout();
            this.SuspendLayout();
            // 
            // pButtons
            // 
            this.pButtons.Controls.Add(this.btnOK);
            this.pButtons.Controls.Add(this.btnCancel);
            this.pButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pButtons.Location = new System.Drawing.Point(10, 373);
            this.pButtons.Name = "pButtons";
            this.pButtons.Size = new System.Drawing.Size(478, 40);
            this.pButtons.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(319, 13);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(400, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbExternalViewers
            // 
            this.gbExternalViewers.Controls.Add(this.lblDoubleClick);
            this.gbExternalViewers.Controls.Add(this.lblExternalViewers);
            this.gbExternalViewers.Controls.Add(this.txtExternalViewers);
            this.gbExternalViewers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbExternalViewers.Location = new System.Drawing.Point(10, 10);
            this.gbExternalViewers.Margin = new System.Windows.Forms.Padding(10);
            this.gbExternalViewers.Name = "gbExternalViewers";
            this.gbExternalViewers.Size = new System.Drawing.Size(478, 363);
            this.gbExternalViewers.TabIndex = 1;
            this.gbExternalViewers.TabStop = false;
            this.gbExternalViewers.Text = " External viewers ";
            // 
            // lblExternalViewers
            // 
            this.lblExternalViewers.AutoSize = true;
            this.lblExternalViewers.Location = new System.Drawing.Point(13, 22);
            this.lblExternalViewers.Name = "lblExternalViewers";
            this.lblExternalViewers.Size = new System.Drawing.Size(270, 39);
            this.lblExternalViewers.TabIndex = 1;
            this.lblExternalViewers.Text = "List of programs used to view exported GPX files.\r\nEnter the full path to the pro" +
    "gram\'s executable file.\r\nFirst one in the list is the default viewer.";
            // 
            // txtExternalViewers
            // 
            this.txtExternalViewers.AcceptsReturn = true;
            this.txtExternalViewers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExternalViewers.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExternalViewers.Location = new System.Drawing.Point(12, 73);
            this.txtExternalViewers.Multiline = true;
            this.txtExternalViewers.Name = "txtExternalViewers";
            this.txtExternalViewers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtExternalViewers.Size = new System.Drawing.Size(451, 259);
            this.txtExternalViewers.TabIndex = 0;
            this.txtExternalViewers.WordWrap = false;
            // 
            // lblDoubleClick
            // 
            this.lblDoubleClick.AutoSize = true;
            this.lblDoubleClick.Location = new System.Drawing.Point(13, 335);
            this.lblDoubleClick.Name = "lblDoubleClick";
            this.lblDoubleClick.Size = new System.Drawing.Size(391, 13);
            this.lblDoubleClick.TabIndex = 2;
            this.lblDoubleClick.Text = "Double-click a flight on the main screen to open it with the default viewer.";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(498, 423);
            this.Controls.Add(this.gbExternalViewers);
            this.Controls.Add(this.pButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.pButtons.ResumeLayout(false);
            this.gbExternalViewers.ResumeLayout(false);
            this.gbExternalViewers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pButtons;
        private System.Windows.Forms.GroupBox gbExternalViewers;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtExternalViewers;
        private System.Windows.Forms.Label lblExternalViewers;
        private System.Windows.Forms.Label lblDoubleClick;
    }
}
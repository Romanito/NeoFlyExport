using NeoFlyExport.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace NeoFlyExport
{
    public partial class MainForm : Form
    {
        private NeoFlyData neoFlyData = new NeoFlyData();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadData();
            BindSettings();
        }

        // Loads data from the database and binds it to the datagrid
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                tspbLoad.Visible = true;
                Enabled = false;

                neoFlyData.LoadStarted += NeoFlyData_LoadStarted;
                neoFlyData.LoadProgress += NeoFlyData_LoadProgress;

                neoFlyData.Load();

                dgvLog.DataSource = neoFlyData.TableLog;
                dgvLog.Columns["Id"].Visible = false;
                foreach (DataGridViewColumn col in dgvLog.Columns)
                {
                    col.ReadOnly = col.Name != "Export";
                }
                dgvLog.AutoResizeColumns();
                Enabled = true;
            }
            finally
            {
                tspbLoad.Visible = false;
                Cursor = Cursors.Default;
            }
        }

        private void NeoFlyData_LoadStarted(object sender, NeoFlyDataLoadEventArgs e)
        {
            tspbLoad.Maximum = e.RowCount;
        }

        private void NeoFlyData_LoadProgress(object sender, NeoFlyDataLoadEventArgs e)
        {
            tspbLoad.Value = e.CurrentRow;
            tspbLoad.Value = e.CurrentRow - 1; // Dirty trick to make the progress bar reach 100% https://stackoverflow.com/a/5332770
            Application.DoEvents();
        }

        private void tsbExportGPX_Click(object sender, EventArgs e)
        {
            if (sfdGPX.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    neoFlyData.ExportToGPXFile(sfdGPX.FileName);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }

        }

        private void tsbSelectAll_Click(object sender, EventArgs e)
        {
            neoFlyData.SelectAllFlights(true);
        }

        private void tsbSelectNone_Click(object sender, EventArgs e)
        {
            neoFlyData.SelectAllFlights(false);
        }

        private void dgvLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Allows checking a range of checkboxes with a shift-click
            if (e.ColumnIndex == dgvLog.Columns["Export"].Index)
            {
                object value = dgvLog.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                foreach (DataGridViewCell cell in dgvLog.SelectedCells)
                {
                    if (cell.ColumnIndex == e.ColumnIndex)
                        cell.Value = value;
                }
            }

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void tsddbExportExternalViewer_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                // Exports the current selection to a temporary file and opens it in an external viewer
                Cursor = Cursors.WaitCursor;
                neoFlyData.ExportToExternalViewer();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void dgvLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Exports the current flight to a temporary file and opens it in an external viewer
                Cursor = Cursors.WaitCursor;
                int logId = (int)dgvLog.Rows[e.RowIndex].Cells["Id"].Value;
                neoFlyData.ExportToExternalViewer(logId);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
            BindSettings();
        }

        private void BindSettings()
        {
            tsddbExportExternalViewer.Visible = Settings.Default.ExternalViewers.Count > 0;
            tsddbExportExternalViewer.DropDownItems.Clear();
            foreach (var viewer in Settings.Default.ExternalViewers)
            {
                tsddbExportExternalViewer.DropDownItems.Add(new ToolStripButton()
                {
                    Text = Path.GetFileNameWithoutExtension(viewer),
                    DisplayStyle = ToolStripItemDisplayStyle.Text,
                    ToolTipText = viewer
                });
            }
        }

        private void tsddbExportExternalViewer_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                // Exports the current selection to a temporary file and opens it in an external viewer
                Cursor = Cursors.WaitCursor;
                neoFlyData.ExportToExternalViewer(0, e.ClickedItem.ToolTipText);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}

using System;
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
        }

        // Loads data from the database and binds it to the datagrid
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                pbLoad.Visible = true;
                tableLayoutPanel.Enabled = false;

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
                tableLayoutPanel.Enabled = true;
            }
            finally
            {
                pbLoad.Visible = false;
                Cursor = Cursors.Default;
            }
        }

        private void NeoFlyData_LoadStarted(object sender, NeoFlyDataLoadEventArgs e)
        {
            pbLoad.Maximum = e.RowCount;
        }

        private void NeoFlyData_LoadProgress(object sender, NeoFlyDataLoadEventArgs e)
        {
            pbLoad.Value = e.CurrentRow;
            pbLoad.Value = e.CurrentRow - 1; // Dirty trick to make the progress bar reach 100% https://stackoverflow.com/a/5332770
            Application.DoEvents();
        }

        private void btnExportGPX_Click(object sender, EventArgs e)
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

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            neoFlyData.SelectAllFlights(true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
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
    }
}

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                neoFlyData.Load();
                dgvLog.DataSource = neoFlyData.TableLog;
                dgvLog.Columns["Id"].Visible = false;
                foreach (DataGridViewColumn col in dgvLog.Columns)
                {
                    col.ReadOnly = col.Name != "Export";
                }
                dgvLog.AutoResizeColumns();
                btnExportGPX.Enabled = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
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
    }
}

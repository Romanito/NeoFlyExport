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
                dgvLog.Columns["FromLon"].Visible = false;
                dgvLog.Columns["FromLat"].Visible = false;
                dgvLog.Columns["ToLon"].Visible = false;
                dgvLog.Columns["ToLat"].Visible = false;
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
    }
}

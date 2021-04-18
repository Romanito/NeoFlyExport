using NeoFlyExport.Properties;
using System;
using System.Windows.Forms;

namespace NeoFlyExport
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            txtExternalViewers.Lines = Settings.Default.ExternalViewers.ToArray();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.ExternalViewers.Clear();
            foreach (string line in txtExternalViewers.Lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Settings.Default.ExternalViewers.Add(line.Trim());
                }
            }
        }
    }
}

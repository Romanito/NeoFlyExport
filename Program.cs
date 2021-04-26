using NeoFlyExport.Properties;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NeoFlyExport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (CommandLine.Arguments.Count == 0)
            {
                SearchExternalViewers();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                try
                {
                    NeoFlyData neoFlyData = new NeoFlyData();
                    neoFlyData.Load();
                    neoFlyData.ExportToGPXFile(CommandLine.Arguments);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "NeoFlyExport error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Search for installed GPX viewers and add them automatically 
        /// </summary>
        static void SearchExternalViewers()
        {
            string[] regKeys = new string[] {
                @"HKEY_CLASSES_ROOT\Applications\GPXSee.exe\shell\open\command",
                @"HKEY_CLASSES_ROOT\Viking File\shell\edit\command"
            };

            foreach (string regKey in regKeys)
            {
                // Registry command
                string commandLine = (string)Microsoft.Win32.Registry.GetValue(regKey, "", null);
                if (commandLine == null)
                    continue;

                // Exe file
                commandLine = commandLine.Substring(0, commandLine.IndexOf(".exe", StringComparison.InvariantCultureIgnoreCase) + 4).Trim('"');
                if (!File.Exists(commandLine))
                    continue;

                // Add the viewer to user settings
                if (!Settings.Default.ExternalViewers.Contains(commandLine, StringComparer.InvariantCultureIgnoreCase))
                    Settings.Default.ExternalViewers.Add(commandLine);
            }
        }
    }
}

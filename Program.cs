using System;
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
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length <= 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                NeoFlyData neoFlyData = new NeoFlyData();
                neoFlyData.Load();
                neoFlyData.ExportToGPXFile(args[1]);
            }
        }
    }
}

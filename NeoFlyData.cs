using Microsoft.Data.Sqlite;
using NeoFlyExport.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace NeoFlyExport
{
    public class NeoFlyData
    {
        private DataSet DataSetNeoFlyData;
        public DataTable TableLog { get; private set; }
        private DataTable TableTrajectoryLog;
        private DataRelation RelationLoTrajectoryLog;
        private string tempFileName;

        public event NeoFlyDataLoadEventHandler LoadStarted;
        public event NeoFlyDataLoadEventHandler LoadProgress;
        public event NeoFlyDataLoadEventHandler LoadEnded;

        public NeoFlyData()
        {
        }

        ~NeoFlyData()
        {
            if (File.Exists(tempFileName))
                File.Delete(tempFileName);
        }

        /// <summary>
        /// Loads database data into in-memory objects
        /// </summary>
        public void Load()
        {
            // NeoFly database file path
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\NeoFly\common.db";
            if (!File.Exists(dbPath))
                throw new FileNotFoundException(dbPath);

            DataSetNeoFlyData = new DataSet();

            // In-memory log table structure
            TableLog = DataSetNeoFlyData.Tables.Add("log");
            TableLog.Columns.Add("Export", typeof(bool));
            TableLog.Columns.Add("Id", typeof(int));
            TableLog.Columns.Add("Date", typeof(DateTime));
            TableLog.Columns.Add("From", typeof(string));
            TableLog.Columns.Add("To", typeof(string));

            // In-memory trajectory log table structure
            TableTrajectoryLog = DataSetNeoFlyData.Tables.Add("trajectoryLog");
            TableTrajectoryLog.Columns.Add("Id", typeof(int));
            TableTrajectoryLog.Columns.Add("FlightId", typeof(int));
            TableTrajectoryLog.Columns.Add("Lon", typeof(double));
            TableTrajectoryLog.Columns.Add("Lat", typeof(double));
            TableTrajectoryLog.Columns.Add("Altitude", typeof(int));

            // Relation between flight and flight trajectory
            RelationLoTrajectoryLog = DataSetNeoFlyData.Relations.Add(TableLog.Columns["Id"], TableTrajectoryLog.Columns["FlightId"]);

            // Db connection
            using (var connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();

                // Airport command
                var commandAirport = connection.CreateCommand();
                commandAirport.CommandText = "SELECT lonx, laty, altitude FROM airport WHERE ident = $ident";
                commandAirport.Parameters.Add("$ident", SqliteType.Text);
                commandAirport.Prepare();

                // Trajectory log command
                var commandTrajectoryLog = connection.CreateCommand();
                commandTrajectoryLog.CommandText = "SELECT id, location, altitude FROM trajectoryLog WHERE flightId = $flightId ORDER BY id";
                commandTrajectoryLog.Parameters.Add("$flightId", SqliteType.Integer);
                commandTrajectoryLog.Prepare();

                // Log command
                var commandLog = connection.CreateCommand();
                commandLog.CommandText = "SELECT COUNT(*) FROM log";
                int logCount = Convert.ToInt32(commandLog.ExecuteScalar());
                int currentLog = 0;
                LoadStarted?.Invoke(this, new NeoFlyDataLoadEventArgs() { RowCount = logCount });

                commandLog = connection.CreateCommand();
                commandLog.CommandText = "SELECT id, date, fp FROM log ORDER BY date";
                using (var readerLog = commandLog.ExecuteReader())
                {
                    while (readerLog.Read())
                    {
                        try
                        {

                            // Flight
                            var row = TableLog.NewRow();
                            row["Id"] = readerLog.GetInt32(0);
                            row["Date"] = readerLog.GetDateTime(1);
                            string[] fp = readerLog.GetString(2).Split('>');
                            row["From"] = fp[0];
                            row["To"] = fp[1];
                            row["Export"] = true;
                            TableLog.Rows.Add(row);

                            // Trajectory
                            bool trajectoryData = false;
                            commandTrajectoryLog.Parameters["$flightId"].Value = row["Id"];
                            using (var readerTrajectoryLog = commandTrajectoryLog.ExecuteReader())
                            {
                                while (readerTrajectoryLog.Read())
                                {
                                    var trajRow = TableTrajectoryLog.NewRow();
                                    trajRow["Id"] = readerTrajectoryLog.GetInt32(0);
                                    trajRow["FlightId"] = row["Id"];

                                    // The location field may be corrupt, in that case the row is skipped
                                    string[] location = readerTrajectoryLog.GetString(1).Split(',');
                                    if (location.Length != 2)
                                        continue;

                                    // The latitude value is checked
                                    double coordValue;
                                    if (!double.TryParse(location[0], NumberStyles.Float, CultureInfo.InvariantCulture, out coordValue))
                                        continue;
                                    trajRow["Lat"] = coordValue;

                                    // The longitude value is checked
                                    if (!double.TryParse(location[1], NumberStyles.Float, CultureInfo.InvariantCulture, out coordValue))
                                        continue;
                                    trajRow["Lon"] = coordValue;

                                    // Altitude
                                    trajRow["Altitude"] = readerTrajectoryLog.GetInt32(2);

                                    // We skip the row is the coordinates are "0,0"
                                    if ((double)trajRow["Lat"] == 0 && (double)trajRow["Lon"] == 0)
                                        continue;

                                    TableTrajectoryLog.Rows.Add(trajRow);
                                    trajectoryData = true;
                                }
                            }

                            // If no trajectory data is available (old NeoFly version), we add two waypoints: departure and arrival
                            if (!trajectoryData)
                            {
                                foreach (string airportId in fp)
                                {
                                    commandAirport.Parameters["$ident"].Value = airportId;
                                    using (var readerFrom = commandAirport.ExecuteReader())
                                    {
                                        if (readerFrom.Read())
                                        {
                                            var trajRow = TableTrajectoryLog.NewRow();
                                            trajRow["FlightId"] = row["Id"];
                                            trajRow["Lon"] = readerFrom.GetDouble(0);
                                            trajRow["Lat"] = readerFrom.GetDouble(1);
                                            trajRow["Altitude"] = readerFrom.GetInt32(2);
                                            TableTrajectoryLog.Rows.Add(trajRow);
                                        }
                                    }
                                }
                            }

                        }
                        finally
                        {
                            LoadProgress?.Invoke(this, new NeoFlyDataLoadEventArgs() { CurrentRow = ++currentLog });
                        }
                    }
                }
                LoadEnded?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Exports in-memory data to a GPX file
        /// </summary>
        /// <param name="options">Export options in key-value pairs</param>
        public void ExportToGPXFile(Dictionary<string, string> options)
        {
            if (!options.ContainsKey("file"))
            {
                throw new ExportOptionException("Provide a file name to export to.\nEx: NeoFlyExport.exe --file=neofly.gpx");
            }

            const double footMeterRatio = 0.3048;

            // Segment list
            List<trkType> trks = new List<trkType>();

            // Rows to be exported
            IEnumerable<DataRow> exportRows;

            // If only the file name is provided, we export all selected rows
            if (options.Count == 1)
                exportRows = TableLog.AsEnumerable().Where(row => row.Field<bool>("Export"));

            // If a log ID is provided, we export only this one
            else if (options.ContainsKey("logid"))
                exportRows = TableLog.AsEnumerable().Where(row => row.Field<int>("Id").ToString() == options["logid"]);

            // Other filters are processed
            else
            {
                exportRows = TableLog.AsEnumerable();

                // dateFrom: Exports all rows after this date
                if (options.ContainsKey("datefrom"))
                {
                    if (!DateTime.TryParse(options["datefrom"], out DateTime dateFrom))
                        throw new ExportOptionException($"Incorrect dateFrom value: {options["datefrom"]}.");
                    exportRows = exportRows.Where(row => row.Field<DateTime>("Date").Date >= dateFrom.Date);
                }

                // dateTo: Exports all rows before this date
                if (options.ContainsKey("dateto"))
                {
                    if (!DateTime.TryParse(options["dateto"], out DateTime dateTo))
                        throw new ExportOptionException($"Incorrect dateTo value: {options["dateto"]}.");
                    exportRows = exportRows.Where(row => row.Field<DateTime>("Date").Date <= dateTo.Date);
                }
            }

            foreach (DataRow row in exportRows)
            {
                // Trajectory waypoints
                DataRow[] trajectoryWaypoints = row.GetChildRows(RelationLoTrajectoryLog);
                if (trajectoryWaypoints.Length < 1)
                    continue;

                // Waypoint list
                List<wptType> wpts = new List<wptType>();
                foreach (DataRow dataRow in trajectoryWaypoints)
                {
                    wpts.Add(new wptType()
                    {
                        lon = Convert.ToDecimal(dataRow["Lon"]),
                        lat = Convert.ToDecimal(dataRow["Lat"]),
                        ele = Convert.ToDecimal((int)dataRow["Altitude"] / footMeterRatio),
                        eleSpecified = true
                    });
                }

                // Labels and time for departure and arrival
                wpts[0].name = (string)row["From"];
                wpts[0].time = row.Field<DateTime>("Date").ToUniversalTime();
                wpts[0].timeSpecified = true;
                wpts[wpts.Count - 1].name = (string)row["To"];

                // Track
                trkType trk = new trkType()
                {
                    name = $"{row["From"]}>{row["To"]}",
                    trkseg = new trksegType[1]
                };
                trk.trkseg[0] = new trksegType() { trkpt = wpts.ToArray() };
                trks.Add(trk);
            }

            // GPX object
            gpxType gpxData = new gpxType()
            {
                trk = trks.ToArray(),
                creator = "NeoFlyExport " + typeof(NeoFlyData).Assembly.GetName().Version.ToString()
            };

            // Write to xml file
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(gpxType));
            using (var swFile = new StreamWriter(options["file"]))
                xmlSerializer.Serialize(swFile, gpxData);
        }

        /// <summary>
        /// Exports in-memory data to a GPX file
        /// </summary>
        /// <param name="gpxFilePath">Exported file path</param>
        /// <param name="logId">Log ID to be exported. If omitted, all checked rows are exported.</param>
        public void ExportToGPXFile(string gpxFilePath, int logId = 0)
        {
            Dictionary<string, string> options = new Dictionary<string, string>();
            options.Add("file", gpxFilePath);
            if (logId != 0)
                options.Add("logid", logId.ToString());
            ExportToGPXFile(options);
        }

        // Exports the current selection to a temporary file and opens it in an external viewer
        public void ExportToExternalViewer(int logId = 0, string commandLine = null)
        {
            // If no external viewer is provided, use the default one
            if (commandLine == null)
                commandLine = Settings.Default.DefaultExternalViewer;

            // If no external viewer is set, exception
            if (commandLine == null)
                throw new NoExternalViewerException();

            tempFileName = Path.Combine(Path.GetTempPath(), "NeoFlyExport.gpx");
            ExportToGPXFile(tempFileName, logId);
            Process.Start(commandLine, tempFileName);
        }

        // Select all or none flight for export
        public void SelectAllFlights(bool selected)
        {
            foreach (DataRow row in TableLog.Rows)
            {
                row["Export"] = selected;
            }
        }

        // Returns true if at least one flight has been selected for export
        public bool HasSelectedFlights => TableLog.AsEnumerable().Any(row => row.Field<bool>("Export"));
    }

    public class NeoFlyDataLoadEventArgs : EventArgs
    {
        public int CurrentRow { get; set; }
        public int RowCount { get; set; }
    }

    public delegate void NeoFlyDataLoadEventHandler(object sender, NeoFlyDataLoadEventArgs e);

    public class NoExternalViewerException : Exception { }
    public class ExportOptionException : Exception
    {
        public ExportOptionException(string message) : base(message) { }
    }
}

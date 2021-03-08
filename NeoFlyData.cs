using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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

        public event NeoFlyDataLoadEventHandler LoadStarted;
        public event NeoFlyDataLoadEventHandler LoadProgress;
        public event NeoFlyDataLoadEventHandler LoadEnded;

        public NeoFlyData()
        {
        }

        // Loads database data into in-memory objects
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

            // Relation between flight and flight trajectory
            RelationLoTrajectoryLog = DataSetNeoFlyData.Relations.Add(TableLog.Columns["Id"], TableTrajectoryLog.Columns["FlightId"]);

            // Db connection
            using (var connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();

                // Airport command
                var commandAirport = connection.CreateCommand();
                commandAirport.CommandText = "SELECT lonx, laty FROM airport WHERE ident = $ident";
                commandAirport.Parameters.Add("$ident", SqliteType.Text);
                commandAirport.Prepare();

                // Trajectory log command
                var commandTrajectoryLog = connection.CreateCommand();
                commandTrajectoryLog.CommandText = "SELECT id, location FROM trajectoryLog WHERE flightId = $flightId ORDER BY id";
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
                    //CultureInfo ciEnUs = new CultureInfo()
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
                                    if (!double.TryParse(location[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out coordValue))
                                        continue;
                                    trajRow["Lat"] = coordValue;

                                    // The longitude value is checked
                                    if (!double.TryParse(location[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out coordValue))
                                        continue;
                                    trajRow["Lon"] = coordValue;

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

        // Exports in-memory data to a GPX fiile
        public void ExportToGPXFile(string gpxFilePath)
        {
            // Segment list
            List<trkType> trks = new List<trkType>();

            foreach (DataRow row in TableLog.AsEnumerable().Where(row => row.Field<bool>("Export")))
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
                        lat = Convert.ToDecimal(dataRow["Lat"])
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
                    name = row["From"] + ">" + row["To"],
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
            using (var swFile = new StreamWriter(gpxFilePath))
                xmlSerializer.Serialize(swFile, gpxData);
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
}

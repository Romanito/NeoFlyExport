using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;

namespace NeoFlyExport
{
    public class NeoFlyData
    {
        public DataTable TableLog { get; private set; }

        public NeoFlyData()
        {
        }

        public void Load()
        {
            // NeoFly database file path
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\NeoFly\common.db";
            if (!File.Exists(dbPath))
                throw new FileNotFoundException(dbPath);

            // In-memory table structure
            TableLog = new DataTable();
            TableLog.Columns.Add("Date", typeof(DateTime));
            TableLog.Columns.Add("From", typeof(string));
            TableLog.Columns.Add("To", typeof(string));
            TableLog.Columns.Add("FromLon", typeof(double));
            TableLog.Columns.Add("FromLat", typeof(double));
            TableLog.Columns.Add("ToLon", typeof(double));
            TableLog.Columns.Add("ToLat", typeof(double));

            // Db connection
            using (var connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();

                // Airport command
                var commandAirport = connection.CreateCommand();
                commandAirport.CommandText = "SELECT lonx, laty FROM airport WHERE ident = $ident";
                commandAirport.Parameters.Add("$ident", SqliteType.Text);

                // Log command
                var commandLog = connection.CreateCommand();
                commandLog.CommandText = "SELECT date, fp FROM log ORDER BY date";
                using (var readerLog = commandLog.ExecuteReader())
                {
                    while (readerLog.Read())
                    {
                        var row = TableLog.NewRow();
                        row["Date"] = readerLog.GetDateTime(0);
                        string[] fp = readerLog.GetString(1).Split('>');

                        // Departure airport
                        row["From"] = fp[0];
                        commandAirport.Parameters["$ident"].Value = fp[0];
                        using (var readerFrom = commandAirport.ExecuteReader())
                        {
                            if (readerFrom.Read())
                            {
                                row["FromLon"] = readerFrom.GetDouble(0);
                                row["FromLat"] = readerFrom.GetDouble(1);
                            }
                        }

                        // Arrival airport
                        row["To"] = fp[1];
                        commandAirport.Parameters["$ident"].Value = fp[1];
                        using (var readerTo = commandAirport.ExecuteReader())
                        {
                            if (readerTo.Read())
                            {
                                row["ToLon"] = readerTo.GetDouble(0);
                                row["ToLat"] = readerTo.GetDouble(1);
                            }
                        }

                        TableLog.Rows.Add(row);
                    }
                }
            }
        }

        public void ExportToGPXFile(string gpxFilePath)
        {
            // Segment list
            List<trkType> trks = new List<trkType>();

            foreach (DataRow row in TableLog.Rows)
            {
                // Waypoint list
                List<wptType> wpts = new List<wptType>();

                // Departure waypoint
                wpts.Add(new wptType()
                {
                    name = (string)row["From"],
                    lon = Convert.ToDecimal(row["FromLon"]),
                    lat = Convert.ToDecimal(row["FromLat"]),
                    time = (DateTime)row["Date"]
                });

                // Arrival waypoint
                wpts.Add(new wptType()
                {
                    name = (string)row["To"],
                    lon = Convert.ToDecimal(row["ToLon"]),
                    lat = Convert.ToDecimal(row["ToLat"])
                });

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
    }
}

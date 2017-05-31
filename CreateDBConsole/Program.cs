using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using DAL;
using Lab7Sqlite;

namespace CreateDBConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello SQLite-net Data!");

            // We're using a file in Assets instead of the one defined above
            //string dbPath = Directory.GetCurrentDirectory ();
            string dbPath = @"../../../Lab7Sqlite/Assets/tidesDB.db3";
            var db = new SQLiteConnection(dbPath);

            // Create a Stocks table
            if (db.CreateTable<TideDataObject>() == 0)
            {
                // A table already exixts, delete any data it contains
                db.DeleteAll<TideDataObject>();
            }

            //AddStocksToDb(db, "GOOG", "Google", "GoogleStocks.csv");
            //AddStocksToDb(db, "EA", "Electronic Arts", "EAStocks.csv");
            //AddStocksToDb(db, "SNE", "Sony", "SonyStocks.csv");
            AddTideObjectToDb(db, "Florence", "FlorenceStation_annual.txt");
            AddTideObjectToDb(db, "Coos Bay", "CoosBayStation_annual.txt");
            AddTideObjectToDb(db, "Newport", "NewportStation_annual.txt");
        }
        private static void AddTideObjectToDb(SQLiteConnection db, string station, string file)
        {
            // parse the tides txt file
            const int NUMBER_OF_FIELDS = 6;    // The text file will have 6 fields, The first is the date, the last is the tide level
            TextParser parser = new TextParser("\t", NUMBER_OF_FIELDS);     // instantiate our general purpose text file parser object.
            List<string[]> stringArrays;    // The parser generates a List of string arrays. Each array represents one line of the text file.
            stringArrays = parser.ParseText(File.Open(@"../../../CreateDBConsole/Assets/" + file, FileMode.Open));     // Open the file as a stream and parse all the text

            // Don't use the first array, it's a header
            stringArrays.RemoveAt(0);


            //// Copy the List of strings into our Database
            int pk = 0;
            foreach (string[] tideInfo in stringArrays)
            {
                var date = Convert.ToDateTime(DateTime.ParseExact(tideInfo[0].ToString(), "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None));
                pk += db.Insert(new TideDataObject()
                {
                    Station = station,
                    Date = date,
                    Day = tideInfo[1],
                    Time = tideInfo[2],
                    PredFt = tideInfo[3],
                    PredCm = tideInfo[4],
                    Level = tideInfo[5],
                });
                // Give an update every 100 rows
                if (pk % 100 == 0)
                    Console.WriteLine("{0} {1} {2} {3} {4} rows inserted", pk, station, tideInfo[0], tideInfo[1], tideInfo[2], tideInfo[4]);
            }
            
        }
    }
}

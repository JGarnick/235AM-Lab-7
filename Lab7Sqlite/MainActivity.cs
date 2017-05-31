using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using System.IO;
using System.Linq;
using DAL;

namespace Lab7Sqlite
{
    [Activity(Label = "Lab7Sqlite", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            /* ------ copy and open the dB file using the SQLite-Net ORM ------ */

            string dbPath = "";
            SQLiteConnection db = null;

            // Get the path to the database that was deployed in Assets
            dbPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "tidesDB.db3");

            // It seems you can read a file in Assets, but not write to it
            // so we'll copy our file to a read/write location
            using (Stream inStream = Assets.Open("tidesDB.db3"))
            using (Stream outStream = File.Create(dbPath))
                inStream.CopyTo(outStream);

            // Open the database
            db = new SQLiteConnection(dbPath);

            /* ------ Spinner initialization ------ */

            // Initialize the adapter for the spinner with tide date
            var distinctStation = db.Table<TideDataObject>().GroupBy(t => t.Station).Select(t => t.First());
            var stationNames = distinctStation.Select(t => t.Station).ToList();
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, stationNames);

            var stationSpinner = FindViewById<Spinner>(Resource.Id.stationSpinner);
            stationSpinner.Adapter = adapter;

            // Event handler for selected spinner item
            string selectedStation = "";
            stationSpinner.ItemSelected += delegate (object sender, AdapterView.ItemSelectedEventArgs e) {
                Spinner spinner = (Spinner)sender;
                selectedStation = (string)spinner.GetItemAtPosition(e.Position);
            };

            /* ------- DatePicker initialization ------- */

            var tideDatePicker = FindViewById<DatePicker>(Resource.Id.tideDatePicker);

            TideDataObject firstDateTide =
                db.Get<TideDataObject>((from t in db.Table<TideDataObject>() select t).Min(t => t.ID));
            DateTime firstDate = firstDateTide.DateActual;
            tideDatePicker.DateTime = firstDate;

            /* ------- Query for selected tide date -------- */

            Button listViewButton = FindViewById<Button>(Resource.Id.listViewButton);
            //ListView tidesListView = FindViewById<ListView>(Resource.Id.tidesListView);
            listViewButton.Click += delegate
            {
                DateTime endDate = tideDatePicker.DateTime;
                DateTime startDate = tideDatePicker.DateTime;

                Helper.DB = db;
                var listView = new Intent(this, typeof(ListViewActivity));
                listView.PutExtra("StartDate", startDate.ToString());
                listView.PutExtra("EndDate", endDate.ToString());
                listView.PutExtra("Station", selectedStation);
                StartActivity(listView);
            };
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DAL;

namespace Lab7Sqlite
{
    [Activity(Label = "ListViewActivity")]
    public class ListViewActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string selectedStation = (string)Intent.Extras.Get("Station");
            string endDateStr = (string)Intent.Extras.Get("EndDate");
            string startDateStr = (string)Intent.Extras.Get("StartDate");
            DateTime endDate = Convert.ToDateTime(endDateStr);
            DateTime startDate = Convert.ToDateTime(startDateStr);
            
            var db = Helper.DB;

            var tides = (from t in db.Table<TideDataObject>()
                         where (t.Station == selectedStation)
                             && (t.DateActual <= endDate)
                             && (t.DateActual >= startDate)
                         select t).ToList();

            ListAdapter = new TideDataAdapter(this, tides);
            
        }
    }
}
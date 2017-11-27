
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
using DSoft.UI.Grid;
using FoodieVisionClient.Data.Grid;

namespace FoodieVisionClient
{
    [Activity(Label = "TimeLineActivity")]
    public class TimeLineActivity : DSGridViewActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //set that data source and the table name
            DataSource = new ExampleDataSet(this);
            TableName = "DT1";
        }
    }
}

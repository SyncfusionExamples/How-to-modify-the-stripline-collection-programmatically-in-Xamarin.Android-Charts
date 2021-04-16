using System;
using System.Collections.ObjectModel;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Charts;

namespace ChartControl
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : AppCompatActivity
    {
        SfChart chart;
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);
            LinearLayout linearLayout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            };


            #region Chart 
            chart = new SfChart(this)
            {
                PrimaryAxis = GetCategoryAxis(),
                SecondaryAxis = GetSecondaryAxis(),
            };

            chart.SetPadding(20, 50, 20, 0);
            chart.Series.Add(new LineSeries() { ItemsSource = new ViewModel().Date, XBindingPath = "Month", YBindingPath = "Temperature", Color = Android.Graphics.Color.Red });
            #endregion

            #region Spinner
            Spinner spinner = new Spinner(this)
            {
                Adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.action_array, Android.Resource.Layout.SimpleSpinnerItem),
            };
            spinner.SetSelection(3);
            spinner.ItemSelected += Spinner_ItemSelected;
            #endregion

            #region TextView
            TextView textView = new TextView(this)
            {
                Text = "Choose Your Action",
            };
            textView.SetPadding(5, 20, 5, 0);
            #endregion

            linearLayout.AddView(textView);
            linearLayout.AddView(spinner);
            linearLayout.AddView(chart);

            SetContentView(linearLayout);

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var index = (sender as Spinner).SelectedItemPosition;
            if (index == 0)
            {
                NumericalStripLine stripLine = new NumericalStripLine()
                {
                    Start = 16,
                    Width = 7,
                    Text = "Low Temperature",
                    FillColor = Android.Graphics.Color.Rgb(66, 197, 245)
                };

                stripLine.LabelStyle.TextColor = Android.Graphics.Color.White;

                (chart.SecondaryAxis as NumericalAxis).StripLines.Insert(0, stripLine);
            }
            else if (index == 1)
            {
                (chart.SecondaryAxis as NumericalAxis).StripLines.Remove((chart.SecondaryAxis as NumericalAxis).StripLines[0]);
            }
            else if (index == 2)
            {
                if ((chart.SecondaryAxis as NumericalAxis).StripLines.Count > 0)
                {
                    (chart.SecondaryAxis as NumericalAxis).StripLines.RemoveAt(0);
                }
            }
            else if (index == 3)
            {
                NumericalStripLine stripLine = new NumericalStripLine()
                {
                    Start = 28,
                    Width = 7,
                    Text = "High Temperature",
                    FillColor = Android.Graphics.Color.Rgb(6, 104, 140),
                };
                stripLine.LabelStyle.TextColor = Android.Graphics.Color.White;

                NumericalStripLine stripLine1 = new NumericalStripLine()
                {
                    Start = 16,
                    Width = 7,
                    Text = "Low Temperature",
                    FillColor = Android.Graphics.Color.Rgb(66, 197, 245)
                };
                stripLine1.LabelStyle.TextColor = Android.Graphics.Color.White;

                (chart.SecondaryAxis as NumericalAxis).StripLines.Add(stripLine);
                (chart.SecondaryAxis as NumericalAxis).StripLines.Add(stripLine1);
            }
            else if (index == 4)
            {
                (chart.SecondaryAxis as NumericalAxis).StripLines.Clear();
            }
        }

        private NumericalAxis GetSecondaryAxis()
        {
            NumericalAxis numericalAxis = new NumericalAxis()
            {
                ShowMajorGridLines = false,
                ShowMinorGridLines = false
            };
            numericalAxis.StripLines.Add(new NumericalStripLine()
            {
                Start = 16,
                Width = 4,
                Text = "Low Temperature",
                FillColor = Android.Graphics.Color.Rgb(66, 197, 245)
            });
            numericalAxis.LabelStyle.LabelFormat = "#'C'";
            numericalAxis.LabelStyle.TextColor = Android.Graphics.Color.Black;
            return numericalAxis;
        }

        private CategoryAxis GetCategoryAxis()
        {
            CategoryAxis categoryAxis = new CategoryAxis()
            {
                ShowMajorGridLines = false,
                LabelPlacement = LabelPlacement.BetweenTicks
            };
            categoryAxis.LabelStyle.TextColor = Android.Graphics.Color.Black;
            return categoryAxis;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}

    public class Model
    {
        public string Month { get; set; }

        public double Temperature { get; set; }
    }

    public class ViewModel
    {
        public ObservableCollection<Model> Date { get; set; }

        public ViewModel()
        {
            Date = new ObservableCollection<Model>()
            {
                new Model(){Month = "Jan", Temperature = 28},
                new Model(){Month = "Feb", Temperature = 26},
                new Model(){Month = "Mar", Temperature = 33},
                new Model(){Month = "Apr", Temperature = 35 },
                new Model(){Month = "May", Temperature = 28},
                new Model(){Month = "Jun", Temperature = 29},
                new Model(){Month = "Jul", Temperature = 32},
                new Model(){Month = "Aug", Temperature = 25},
                new Model(){Month = "Sep", Temperature = 30},
                new Model(){Month = "Oct", Temperature = 20},
                new Model(){Month = "Nov", Temperature = 13},
                new Model(){Month = "Dec", Temperature = 15},
                    
            };
        }
    }
}

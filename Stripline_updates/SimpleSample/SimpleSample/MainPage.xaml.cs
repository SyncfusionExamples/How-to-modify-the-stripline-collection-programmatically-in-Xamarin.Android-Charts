using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Syncfusion.SfChart.XForms;

namespace SimpleSample
{
    public partial class MainPage : ContentPage
    {
        StackLayout stackLayout;
        ViewModel viewModel;
        NumericalStripLine stripLine;
        NumericalAxis numericalAxis;
        public MainPage()
        {
            InitializeComponent();
            stackLayout = new StackLayout();
            viewModel = new ViewModel();
            this.BindingContext = viewModel;
            Picker picker = new Picker();
            picker.TitleColor = Color.Black;
            picker.Title = "Remove or Modify Striplines";
            picker.SelectedIndexChanged += picker_SelectedIndexChanged;
            picker.TextColor = Color.Red;
            picker.ItemsSource = new String[] { "Insert Striplines", "Remove Striplines", "RemoveAt Striplines", "Add Striplines", "Clear Striplines" };
            SfChart chart = new SfChart();
            chart.Title.Text = "Temperature Flow";
            chart.HorizontalOptions = LayoutOptions.FillAndExpand;
            chart.VerticalOptions = LayoutOptions.FillAndExpand;
            CategoryAxis categoryAxis = new CategoryAxis();
            categoryAxis.Title.Text = "Days";
            chart.PrimaryAxis = categoryAxis;

            //Initializing secondary Axis
            numericalAxis = new NumericalAxis();
            numericalAxis.Title.Text = "Temperature in Celsius";
            numericalAxis.Name = "numericalAxis";
            numericalAxis.LabelStyle.LabelFormat = "#'°C'";
            stripLine = new NumericalStripLine();
            stripLine.Start = 23;
            stripLine.Width = 5;
            stripLine.Text = "High Temperature";
            stripLine.FillColor = Color.LightGreen;
            numericalAxis.StripLines.Add(stripLine);
            chart.SecondaryAxis = numericalAxis;

            //Initializing column series
            SplineSeries series = new  SplineSeries();
            series.ItemsSource = viewModel.Data;
            series.XBindingPath = "XValue";
            series.YBindingPath = "YValue";
            series.Color = Color.Maroon;
            series.DataMarker = new ChartDataMarker();
            series.DataMarker.ShowMarker = true;
            chart.Series.Add(series);
            stackLayout.Children.Add(picker);
            stackLayout.Children.Add(chart);
            this.Content = stackLayout;
        }

        
        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = (sender as Picker).SelectedIndex;
            if (index == 0)
            {
                NumericalStripLine stripLine = new NumericalStripLine();
                
                stripLine.Start = 18;

                stripLine.Width = 5;
                
                stripLine.Text = "Low Temperature";

                stripLine.FillColor = Color.CornflowerBlue;
                
                numericalAxis.StripLines.Insert(1,stripLine);
            }
            else if (index == 1)
            {
                numericalAxis.StripLines.Remove(stripLine);
            }
            else if (index == 2)
            {
                numericalAxis.StripLines.RemoveAt(0);
            }
            else if (index == 3)
            {
                NumericalStripLine stripLine = new NumericalStripLine();

                stripLine.Start = 23;

                stripLine.Width = 5;

                stripLine.Text = "High Temperature";

                stripLine.FillColor = Color.LightGreen;

                numericalAxis.StripLines.Add(stripLine);

                NumericalStripLine stripLine1 = new NumericalStripLine();

                stripLine1.Start = 18;

                stripLine1.Width = 5;

                stripLine1.Text = "Low Temperature";

                stripLine1.FillColor = Color.CornflowerBlue;

                numericalAxis.StripLines.Add(stripLine1);
            }
            else if (index == 4)
            {
                numericalAxis.StripLines.Clear();
            }
        }

    }

    public class ViewModel
    {
        public ObservableCollection<Model> Data { get; set; }

        public ViewModel()
        {
            Data = new ObservableCollection<Model>()
            {
                new Model("Sun", 19, 20),
                new Model("Mon", 18.3, 20),
                new Model("Tue", 19.2, 20),
                new Model("Wed", 22, 20),
                new Model("Thur", 25, 20),
                new Model("Fri", 27.3, 20),
                new Model("Sat", 26, 20),
            };
        }
    }

    public class Model
    {
        public string XValue { get; set; }
        public double YValue { get; set; }

        private double Size;

        public Model(string xValue, double yValue, double size)
        {
            this.XValue = xValue;
            this.YValue = yValue;
            this.Size = size;
        }
    }
}

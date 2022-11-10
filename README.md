# How to modify the stripline collection programmatically in Xamarin.Android Charts (SfChart)

[Xamarin.Android Chart](https://help.syncfusion.com/xamarin-android/sfchart/getting-started) allows to shade the different ranges in the plot area using [StripLines](https://help.syncfusion.com/xamarin-android/sfchart/striplines) support.

The [Stripline](https://help.syncfusion.com/cr/xamarin-android/Com.Syncfusion.Charts.NumericalStripLine.html) is classified into [NumericalStripLine](https://help.syncfusion.com/cr/xamarin-android/Com.Syncfusion.Charts.NumericalStripLine.html) and [DateTimeStripLine](https://help.syncfusion.com/cr/xamarin-android/Com.Syncfusion.Charts.DateTimeStripLine.html) based on the type of input, you can proceed to draw the Striplines. The [NumericalStripLine](https://help.syncfusion.com/cr/xamarin-android/Com.Syncfusion.Charts.NumericalStripLine.html) is used to draw striplines for numerical values and the following code sample explains how to draw the [NumericalStripLines](https://help.syncfusion.com/cr/xamarin-android/Com.Syncfusion.Charts.NumericalStripLine.html) and modify the stripline collection programmatically.

```
LinearLayout linearLayout = new LinearLayout(this)
{
    Orientation = Orientation.Vertical,
    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
};

chart = new SfChart(this)
{
    PrimaryAxis = GetCategoryAxis(),
    SecondaryAxis = GetSecondaryAxis(),
};

chart.SetPadding(20, 50, 20, 0);
chart.Series.Add( new LineSeries()
{ 
    ItemsSource = new ViewModel().Date, 
    XBindingPath = "Month", 
    YBindingPath = "Temperature", 
    Color = Android.Graphics.Color.Red 
});

Spinner spinner = new Spinner(this)
{
    Adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.action_array, Android.Resource.Layout.SimpleSpinnerItem),
};
spinner.SetSelection(3);
spinner.ItemSelected += Spinner_ItemSelected;
…

linearLayout.AddView(spinner);
linearLayout.AddView(chart);

SetContentView(linearLayout);
```

```
private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
{
    var index = (sender as Spinner).SelectedItemPosition;
    if (index == 0)
    {
        NumericalStripLine stripLine = new NumericalStripLine()
        {
            Start = 16,
            Width = 3,
            Text = "Low Temperature",
            FillColor = Android.Graphics.Color.Rgb(66, 197, 245)
        };

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
            Width = 3,
            Text = "High Temperature",
            FillColor = Android.Graphics.Color.Rgb(6, 104, 140)
        };
        NumericalStripLine stripLine1 = new NumericalStripLine()
        {
            Start = 16,
            Width = 3,
            Text = "Low Temperature",
            FillColor = Android.Graphics.Color.Rgb(66, 197, 245)
        };

        (chart.SecondaryAxis as NumericalAxis).StripLines.Add(stripLine);
        (chart.SecondaryAxis as NumericalAxis).StripLines.Add(stripLine1);
    }
    else if (index == 4)
    {
        (chart.SecondaryAxis as NumericalAxis).StripLines.Clear();
    }
}
```

![Modify the StripLine in Xamarin.Android Charts](https://github.com/SyncfusionExamples/How-to-modify-the-stripline-collection-programmatically-in-Xamarin.Android-Charts/blob/main/Xamarin.Android-Chart-StripLine.gif)

### See also

[How to customize strip line label in Xamarin.Android Chart](https://help.syncfusion.com/xamarin-android/sfchart/striplines#customize-text)

[How to draw the strip lines repeatedly at the regular intervals](https://help.syncfusion.com/xamarin-android/sfchart/striplines#strip-line-recurrence)

[How to draw a strip line without stretch with its associated axis](https://help.syncfusion.com/xamarin-android/sfchart/striplines#segmented-strip-line)

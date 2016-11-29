using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WTBoxPannel.Panels;

namespace WTBoxPannel
{
    [TemplatePart(Name = "ChartPanel", Type = typeof(ChartPanel))]
    [TemplatePart(Name = "GridPanel", Type = typeof(GridPanel))]
    [TemplatePart(Name = "SeriesPanel", Type =typeof(SeriesPanel))]

    public class WTChart : Control
    {
        private ChartPanel chartPanel;
        private GridPanel gridPanel;
        private SeriesPanel seriesPanel;
        private ChartModelCollection chartModelCollection = new ChartModelCollection();
        internal List<KeyValuePair<int, int>> collection;
        private LineSeries lineSeriesValues;

        public WTChart()
        {
        
        }

        protected override void OnApplyTemplate()
        {
            chartPanel = GetTemplateChild("ChartPanel") as ChartPanel;
            gridPanel = GetTemplateChild("GridPanel") as GridPanel;
            seriesPanel = GetTemplateChild("SeriesPanel") as SeriesPanel;
            base.OnApplyTemplate();
            ApplyDataSource();
            ApplyModelCollection();
            ApplySettings();
        }

        public Series Series
        {
            get { return (Series)GetValue(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Series.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeriesProperty =
            DependencyProperty.Register(nameof(Series), typeof(Series), typeof(WTChart), new PropertyMetadata(null, OnSeriesValueChanged));

        private static void OnSeriesValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = d as WTChart;
            chart.lineSeriesValues =(LineSeries) e.NewValue;
        }

        private void ApplySettings()
        {
            gridPanel.Visibility = Series.ShowGridLines ? Visibility.Visible : Visibility.Collapsed;
            chartPanel.collection = collection;
            gridPanel.collection = collection;
            seriesPanel.collection = collection;
        }

        private void ApplyModelCollection()
        {
            chartPanel.chartModelCollection = chartModelCollection;
            gridPanel.chartModelCollection = chartModelCollection;
            seriesPanel.chartModelCollection = chartModelCollection;
            seriesPanel.lineSeries = lineSeriesValues;
            lineSeriesValues.lineSeries = lineSeriesValues;
        }

        private void ApplyDataSource()
        {
            if (seriesPanel.canvas.Children.Count > 0)
                seriesPanel.canvas.Children.Clear();
            if (Series.DataCollection != null)
            { 
                collection = Series.DataCollection;
            }
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var customChart = (WTChart)dependencyObject;
            //customChart.UpdateLayout();
        }
    }
}

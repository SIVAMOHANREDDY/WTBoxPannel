using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WTBoxPannel.Panels;

namespace WTBoxPannel
{
    [TemplatePart(Name = "ChartPanel", Type = typeof(BoxPanel))]
    [TemplatePart(Name = "GridPanel", Type = typeof(GridPanel))]
    [TemplatePart(Name = "SeriesPanel", Type =typeof(SeriesPanel))]

    public class WTChart : Control
    {
        private BoxPanel chartPanel;
        private GridPanel gridPanel;
        private SeriesPanel seriesPanel;
        public static double Chartheight { get; set; }
        public static double Chartwidth { get; set; }

        public WTChart()
        {

        }
        //public IList<object> Datasource
        //{
        //    get { return (IList<object>)GetValue(DatasourceProperty); }
        //    set { SetValue(DatasourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DatasourceProperty =
        //    DependencyProperty.Register("Datasource", typeof(IList<object>), typeof(WTChart), new PropertyMetadata(new List<string>()));
        
        protected override void OnApplyTemplate()
        {
            chartPanel = GetTemplateChild("ChartPanel") as BoxPanel;
            Chartheight = chartPanel.Height;
            Chartwidth = chartPanel.Width;
            gridPanel = GetTemplateChild("GridPanel") as GridPanel;
            seriesPanel = GetTemplateChild("SeriesPanel") as SeriesPanel;
            //chartPanel.Itemsource = Datasource;
            base.OnApplyTemplate();
        }

        //private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        //{
        //    var customChart = (WTChart)dependencyObject;
        //    customChart.UpdateLayout();
        //}
    }
}

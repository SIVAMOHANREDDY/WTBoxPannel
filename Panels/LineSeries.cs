using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using WTBoxPannel.Panels;

namespace WTBoxPannel
{
    public class LineSeries : Series
    {
        public ObservableCollection<Point> LinePoints { get; set; }
        internal LineSeries lineSeries;

        public LineSeries()
        {
            LinePoints = new ObservableCollection<Point>();
        }

        public Brush LineColor
        {
            get { return (Brush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty,value); }
        }

        // Using a DependencyProperty as the backing store for LineColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register(nameof(LineColor), typeof(Brush), typeof(LineSeries), new PropertyMetadata(new SolidColorBrush(Colors.Black), onColorChanged));

        private static void onColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Handle runtime property changes.
            //TODO: Runtime line color changes not yet implemented.
           var  lineSeriess = d as LineSeries;
            //lineSeriess.lineSeries.LineColor = e.NewValue as Brush;
        }

        public int LineThickness
        {
            get { return (int)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineThicknessProperty =
            DependencyProperty.Register(nameof(LineThickness), typeof(int), typeof(LineSeries), new PropertyMetadata(1,OnThicknessChanged));

        private static void OnThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lineSeries = d as LineSeries;
            lineSeries.LineThickness = (int)e.NewValue;
        }

        public LinePatternEnum LinePattern
        {
            get { return (LinePatternEnum)GetValue(LinePatternProperty); }
            set { SetValue(LinePatternProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinePattern.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinePatternProperty =
            DependencyProperty.Register(nameof(LinePattern), typeof(LinePatternEnum), typeof(LineSeries), 
                new PropertyMetadata(LinePatternEnum.Solid, OnPatternChanged));

        private static void OnPatternChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lineSeries = d as LineSeries;
            var newValue = (LinePatternEnum)e.NewValue;
        }

        private string seriesName="Default";

        public string SeriesName
        {
            get { return seriesName; }
            set { seriesName= value; }
        }

        private DoubleCollection lineDashPattern;

        public DoubleCollection LineDashPattern
        {
            get { return lineDashPattern; }
            set { lineDashPattern = value; }
        }

        public void SetLinePattern(LinePatternEnum linePattern )
        {
            switch (linePattern)
            {
                case LinePatternEnum.Dash:
                    LineDashPattern = new DoubleCollection() { 4, 3 };
                    break;
                case LinePatternEnum.Dot:
                    LineDashPattern = new DoubleCollection() { 1, 2 };
                    break;
                case LinePatternEnum.DashDot:
                    LineDashPattern = new DoubleCollection() { 4, 2, 1, 2 };
                    break;
                case LinePatternEnum.PipeLine:
                    LineDashPattern = new DoubleCollection() { };
                    break;
            }
        }
    }

    public enum LinePatternEnum
    {
        Solid = 1,
        Dash = 2,
        Dot = 3,
        DashDot = 4,
        PipeLine=5,

    }
}

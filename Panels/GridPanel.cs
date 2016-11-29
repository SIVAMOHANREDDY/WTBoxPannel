using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace WTBoxPannel.Panels
{
    public class GridPanel: Panel
    {
        private Canvas canvas;
        const double margin = 20;
        private bool isInArrange;

        List<int> keylist = new List<int>();
        List<int> valueList = new List<int>();

        internal List<KeyValuePair<int, int>> collection;
        internal ChartModelCollection chartModelCollection;


        public GridPanel()
        {
            canvas = new Canvas();
            canvas.Width = this.Width;
            canvas.Height = this.Height;
            canvas.Background = new SolidColorBrush(Colors.Black);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new Size(0, 0);

            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
                size.Width = Math.Max(size.Width, child.DesiredSize.Width);
                size.Height = Math.Max(size.Height, child.DesiredSize.Height);
                isInArrange = false;
            }

            size.Width = double.IsPositiveInfinity(availableSize.Width) ?
               size.Width : availableSize.Width;

            size.Height = double.IsPositiveInfinity(availableSize.Height) ?
               size.Height : availableSize.Height;

            return size;
        }
    
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count <= 0)
            {
                this.Children.Add(canvas);
            }
            if (!isInArrange)
            {
                isInArrange = true;
                if (canvas.Children.Count > 0)
                    canvas.Children.Clear();

                chartModelCollection.Xstep = (finalSize.Width - 40) / collection.Count;
                chartModelCollection.Ystep = (finalSize.Height - 40) / collection.Count;

                LineForXaxisGeometry(finalSize);
                LineForYaxisGeometry(finalSize);
            }
            return finalSize;
        }

        private void LineForYaxisGeometry(Size finalSize)
        {
            GeometryGroup yaxis_geometry = new GeometryGroup();
            chartModelCollection.Ymin = margin;
            for (double y = chartModelCollection.Ymin; y <= finalSize.Height; y += chartModelCollection.Ystep)
            {
                LineGeometry myLineGeometry5 = new LineGeometry();
                myLineGeometry5.StartPoint = new Point(XAxisScalePoint(finalSize), y);
                myLineGeometry5.EndPoint = new Point(XAxisScalePoint(finalSize) + (finalSize.Width - 40), y);
                yaxis_geometry.Children.Add(myLineGeometry5);
            }
            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = new SolidColorBrush(Colors.Red);
            yaxis_path.Data = yaxis_geometry;
            canvas.Children.Add(yaxis_path);
        }

        private void LineForXaxisGeometry(Size finalSize)
        {
            GeometryGroup xaxis_geometry = new GeometryGroup();
            chartModelCollection.Xmin = margin;

            for (double x = chartModelCollection.Xmin;
                 x <= finalSize.Width; x += chartModelCollection.Xstep)
            {
                LineGeometry myLineGeometry12 = new LineGeometry();
                myLineGeometry12.StartPoint = new Point(x, YAxisScalePoint(finalSize));
                myLineGeometry12.EndPoint = new Point(x, YAxisScalePoint(finalSize) - forWindowSize(finalSize) + 10);
                xaxis_geometry.Children.Add(myLineGeometry12);
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);

            xaxis_path.Data = xaxis_geometry;
            canvas.Children.Add(xaxis_path);
        }

        private double windowWidth(Size finalSize)
        {
            return finalSize.Width;
        }

        private double forWindowSize(Size finalSize)
        {
            return finalSize.Height-margin;
        }

        public double YAxisScalePoint(Size availablsize)
        {
            return availablsize.Height - margin;
        }

        public double XAxisScalePoint(Size availablsize)
        {
            return 20;
        }

        private void AddChart()
        {
            ObservableCollection<LineSeries> dc = new ObservableCollection<LineSeries>();
            var ds = new LineSeries();
            ds.LineColor = new SolidColorBrush(Colors.Blue);
            ds.LineThickness = 2;
            ds.LinePattern = LinePatternEnum.Solid;
            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x);
                ds.LinePoints.Add(new Point(x, y));
            }
            dc.Add(ds);
            ds = new LineSeries();
            ds.LineColor = new SolidColorBrush(Colors.Red);
            ds.LineThickness = 2;
            ds.LinePattern = LinePatternEnum.Dash;
            //ds.SetLinePattern();
            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LinePoints.Add(new Point(x, y));
            }
            dc.Add(ds);
            SetLines(dc);
        }

        private void SetLines(ObservableCollection<LineSeries> dc)
        {
            if (dc.Count <= 0)
                return;

            int i = 0;
            foreach (var ds in dc)
            {
                PointCollection pts = new PointCollection();

                if (ds.SeriesName == "Default")
                    ds.SeriesName = "LineSeries" + i.ToString();
                //ds.SetLinePattern();
                for (int j = 1; j < 50; j++)
                {
                    var pt = ds.LinePoints[j];
                    pts.Add(pt);
                }

                Polyline line = new Polyline();
                line.Points = pts;
                line.Stroke = ds.LineColor;
                line.StrokeThickness = ds.LineThickness;
                line.StrokeDashArray = ds.LineDashPattern;
                canvas.Children.Add(line);
                i++;
            }
        }

        private object NormalizePoint(Point pt)
        {
            if (Double.IsNaN(canvas.Width) || canvas.Width <= 0)
                canvas.Width = 270;
            if (Double.IsNaN(canvas.Height) || canvas.Height <= 0)
                canvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - 20) * canvas.Width / (canvas.Width - 20);
            result.Y = canvas.Height - (pt.Y - 20) * canvas.Height / (canvas.Height - 20);
            return result;
        }
    }
}

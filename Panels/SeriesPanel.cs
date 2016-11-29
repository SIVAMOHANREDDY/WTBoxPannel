using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace WTBoxPannel.Panels
{
    public class SeriesPanel : Panel
    {
        public Canvas canvas;
        internal ChartModelCollection chartModelCollection;
        internal List<KeyValuePair<int, int>> collection;
        internal LineSeries lineSeries;
        private PlotPanel plotPanel;

        private bool isInArrange;
        int maxValueFromKey;
        int maxValueFromValue;
        List<int> keylist = new List<int>();
        List<int> valueList = new List<int>();
        PointCollection pointCollection;
        Ellipse myEllipse;

        public SeriesPanel()
        {
            canvas = new Canvas();
            plotPanel = new PlotPanel();
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
                this.Children.Add(plotPanel);
            }
            if (!isInArrange)
            {
                isInArrange = true;
                if (canvas.Children.Count > 0)
                    canvas.Children.Clear();

               // SetLines(collection, finalSize);
                AddChart(finalSize,collection);
            }
            return finalSize;
        }

        public void SetLines(List<KeyValuePair<int, int>> collection,Size finalSize)
        {
            chartModelCollection.Xstep = (finalSize.Width - 40) / collection.Count;
            chartModelCollection.Ystep = (finalSize.Height - 40) / collection.Count;

            var maxValueFromHeight = ValueList(finalSize);
            var maxValueFromWidth = KeyList(finalSize);

            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            pointCollection = new PointCollection();
            var myValueOfKey = collection;

            foreach (var item in myValueOfKey)
            {
                var point = (new Point(((((finalSize.Width - 40) / maxValueFromWidth) * item.Key) + 20),
                                    (finalSize.Height - 20) - (((finalSize.Height - 40) / maxValueFromHeight) * item.Value)));
                var myEllipse = CreateEllipse(point);
                var myTextBlock = createTextBlock(point);
                canvas.Children.Add(myEllipse);
                pointCollection.Add(point);
                //canvas.Children.Add(myTextBlock);
            }
            // Create a polyline
            Polyline yellowPolyline = new Polyline();
            yellowPolyline.Stroke = blackBrush;
            yellowPolyline.StrokeThickness = 2;
            yellowPolyline.Points = pointCollection;
            // Add polyline to the page
            canvas.Children.Add(yellowPolyline);
        }

        private TextBlock createTextBlock(Point point)
        {
            var myTextBlock = new TextBlock();
            myTextBlock.Text = "siva";
            myTextBlock.Foreground = new SolidColorBrush(Colors.AliceBlue);
           
            return myTextBlock;
        }

        private Ellipse CreateEllipse(Point point)
        {
            myEllipse = new Ellipse();
            myEllipse.Fill = new SolidColorBrush(Colors.Black);
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = new SolidColorBrush(Colors.White);
            myEllipse.Width = 10;
            myEllipse.Height = 10;
            Canvas.SetTop(myEllipse, point.Y - 4);
            Canvas.SetLeft(myEllipse, point.X - 6);
            return myEllipse;
        }

        private double ValueList(Size finalSize)
        {
            var myValueOfKey = collection;

            foreach (var item in myValueOfKey)
            {
                valueList.Add(item.Value);
            }
            maxValueFromKey = valueList.Max();
            return maxValueFromKey;
        }

        private double KeyList(Size finalSize)
        {
            var myValueOfKey = collection;

            foreach (var item in myValueOfKey)
            {
                keylist.Add(item.Key);
            }
            maxValueFromValue = keylist.Max();
            return maxValueFromValue;
        }

      //ObservableCollection<Point>  LinePoints = new ObservableCollection<Point>();

        private void AddChart(Size finalSize, List<KeyValuePair<int,int>> collection)
        {
            var maxValueFromHeight = ValueList(finalSize);
            var maxValueFromWidth = KeyList(finalSize);
            ObservableCollection<LineSeries> dc = new ObservableCollection<LineSeries>();
            PointCollection pointCollection = new PointCollection();

            foreach (var item in collection)
            {
                chartModelCollection.Xstep = (finalSize.Width - 40) / collection.Count;
                chartModelCollection.Ystep = (finalSize.Height - 40) / collection.Count;

                var point = (new Point(((((finalSize.Width - 40) / maxValueFromWidth) * item.Key) + 20),
                                   (finalSize.Height - 20) - (((finalSize.Height - 40) / maxValueFromHeight) * item.Value)));

                pointCollection.Add(point);
                var myEllipse = CreateEllipse(point);
                canvas.Children.Add(myEllipse);
            }
            lineSeries.SetLinePattern(lineSeries.LinePattern);
            Polyline line = new Polyline();
            line.Points = pointCollection;
            line.Stroke = lineSeries.LineColor;
            line.StrokeThickness = lineSeries.LineThickness;
            line.StrokeDashArray = lineSeries.LineDashPattern;
            line.StrokeDashCap=PenLineCap.Square;
            canvas.Children.Add(line);

            dc.Add(lineSeries);

            //SetLines(dc);
        }

        //private void SetLines(ObservableCollection<LineSeries> dc)
        //{
        //    if (dc.Count <= 0)
        //        return;
        //    int i = 0;
        //    foreach (var ds in dc)
        //    {
        //        //for (int j = 0; j < collection.Count; j++)
        //        //{
        //        //    var pt = LinePoints[j];
        //        //    pointCollection.Add(pt);
        //        //}
        //        i++;
        //    }
        //}
    }
}

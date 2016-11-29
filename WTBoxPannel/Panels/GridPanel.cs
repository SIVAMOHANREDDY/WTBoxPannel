using System;
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
        public static Canvas canvas;
        const double margin = 10;
        const double step = 25;
        private double xmax;
        private double xmin;
        private double ymax;
        private double ymin;

        public GridPanel()
        {
            canvas = new Canvas();
            canvas.Width = this.Width;
            canvas.Height = this.Height;
            canvas.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            xmin = margin;
            xmax = canvas.Width - margin;
            ymin = margin;
            ymax = canvas.Height - margin;
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
        private bool isInArrange;
        private Point Point1;
        private Point Point2;
        private Point Point3;
        private Point Point4;
        private Point Point5;

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
                LineForXaxisGeometry(finalSize);
                LineForYaxisGeometry(finalSize);
                // Make some data sets.
                // Create a blue and a black Brush
                SolidColorBrush blackBrush = new SolidColorBrush();
                blackBrush.Color = Colors.Black;

                // Create a polyline
                Polyline yellowPolyline = new Polyline();
                yellowPolyline.Stroke = blackBrush;
                yellowPolyline.StrokeThickness = 4;

                canvas.Height = WTChart.Chartheight;
                canvas.Width = WTChart.Chartwidth;

                //Create a collection of points for a polyline
                if (double.IsNaN(canvas.Height) && double.IsNaN(canvas.Width))
                {
                     Point1 = new Point(20, finalSize.Height - 20);
                    Point2 = new Point(finalSize.Width - 300, finalSize.Height - 150);
                     Point3 = new Point(finalSize.Width - 200, finalSize.Height - 250);
                     Point4 = new Point(finalSize.Width - 100, finalSize.Height - 350);
                     Point5 = new Point(finalSize.Width - 50, finalSize.Height - 250);
                }
                else
                {
                    Point1 = new Point(20, canvas.Height - 20);
                    Point2 = new Point(canvas.Width - 300, canvas.Height - 150);
                    Point3 = new Point(canvas.Width - 200, canvas.Height - 250);
                    Point4 = new Point(canvas.Width - 100, canvas.Height - 350);
                    Point5 = new Point(canvas.Width - 50, canvas.Height - 250);
                }
                GeometryGroup geometryGroup = new GeometryGroup();
                PointCollection polygonPoints = new PointCollection();
                polygonPoints.Add(Point1);
                polygonPoints.Add(Point2);
                polygonPoints.Add(Point3);
                polygonPoints.Add(Point4);
                polygonPoints.Add(Point5);
                // Set Polyline.Points properties
                yellowPolyline.Points = polygonPoints;
                // Add polyline to the page
                canvas.Children.Add(yellowPolyline);

                //TextBlock txt = new TextBlock();
                //txt.Text = "text";
                //txt.Arrange(new Rect(50, 10, 200, 200));
                //txt.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));


                //canvas.Children.Add(txt);
            }
            return finalSize;
        }

        private void LineForYaxisGeometry(Size finalSize)
        {
            GeometryGroup yaxis_geometry = new GeometryGroup();
            LineGeometry myLineGeometry3 = new LineGeometry();

            xmin = margin;
            xmax = finalSize.Width;
            myLineGeometry3.StartPoint = new Point(20, 0);
            myLineGeometry3.EndPoint = new Point(20, finalSize.Height);
            myLineGeometry3.Transform = new TranslateTransform() { X = myLineGeometry3.StartPoint.Y, Y = myLineGeometry3.EndPoint.X };
            yaxis_geometry.Children.Add(myLineGeometry3);

            for (double y = step; y <= finalSize.Height - step; y += step)
            {
                LineGeometry myLineGeometry5 = new LineGeometry();
                myLineGeometry5.StartPoint = new Point(XAxisScalePoint(finalSize) + windowWidth(finalSize) - 20, y - 5);
                myLineGeometry5.EndPoint = new Point(XAxisScalePoint(finalSize), y - 5);

                yaxis_geometry.Children.Add(myLineGeometry5);
            }
            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            yaxis_path.Data = yaxis_geometry;
            canvas.Children.Add(yaxis_path);
        }

        private void LineForXaxisGeometry(Size finalSize)
        {
            GeometryGroup xaxis_geometry = new GeometryGroup();
            LineGeometry myLineGeometry = new LineGeometry();
            ymax = margin;
            myLineGeometry.StartPoint = new Point(0, 0);
            myLineGeometry.EndPoint = new Point(finalSize.Width, 0);
            myLineGeometry.Transform = new TranslateTransform() { X = myLineGeometry.StartPoint.Y, Y = finalSize.Height - 20 };

            //< Matrix OffsetX = "10" OffsetY = "20" M11 = "5" M12 = "5" />

            xaxis_geometry.Children.Add(myLineGeometry);
            for (double x = xmin + step;
                x <= finalSize.Width - step; x += step)
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
            return finalSize.Height;
        }

        public double YAxisScalePoint(Size availablsize)
        {
            return availablsize.Height - margin;
        }

        public double XAxisScalePoint(Size availablsize)
        {
            return 20;
        }
    }
}

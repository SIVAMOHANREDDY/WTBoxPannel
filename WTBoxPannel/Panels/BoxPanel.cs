using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.Reflection;
using System.Linq;

namespace WTBoxPannel
{
    internal class BoxPanel : Panel
    {
        public static Canvas canvas;
        const double margin = 10;
        const double step = 25;
        private double xmax;
        private double xmin;
        private double ymax;
        private double ymin;

        internal IList<object> Itemsource { get; set; }

        public BoxPanel()
        {
            canvas = new Canvas();
            canvas.Height = this.Width;
            canvas.Width = this.Width;
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

            }
            return finalSize;
        }

        private void LineForYaxisGeometry(Size finalSize)
        {
            GeometryGroup yaxis_geometry = new GeometryGroup();
            LineGeometry myLineGeometry3 = new LineGeometry();

            xmin = margin;
            myLineGeometry3.StartPoint = new Point(20, 0);
            myLineGeometry3.EndPoint = new Point(20, finalSize.Height);
            //myLineGeometry3.Transform = new TranslateTransform() { X = myLineGeometry3.StartPoint.Y, Y = myLineGeometry3.EndPoint.X };
            yaxis_geometry.Children.Add(myLineGeometry3);

            //Itemsource.GetType().GetProperty("");
            //foreach (var prop in props)
            //{

            //}

            //var props = Itemsource.GetType().GetProperties(BindingFlags.Public);
            //foreach (var prop in props)
            //{
  
            //}


            for (double y = step; y <= finalSize.Height - step; y += step)
            {
                LineGeometry myLineGeometry4 = new LineGeometry();
                myLineGeometry4.StartPoint = new Point(XAxisScalePoint(finalSize), y);
                myLineGeometry4.EndPoint = new Point(XAxisScalePoint(finalSize) - margin, y);
                myLineGeometry4.Transform = new TranslateTransform() { X = myLineGeometry3.StartPoint.Y, Y = myLineGeometry3.EndPoint.X };
                yaxis_geometry.Children.Add(myLineGeometry4);
                ////======
                //LineGeometry myLineGeometry5 = new LineGeometry();
                //myLineGeometry5.StartPoint = new Point(XAxisScalePoint(finalSize)+windowWidth(finalSize), y-margin);
                //myLineGeometry5.EndPoint = new Point(XAxisScalePoint(finalSize), y);

                //yaxis_geometry.Children.Add(myLineGeometry5);

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
                LineGeometry myLineGeometry2 = new LineGeometry();
                myLineGeometry2.StartPoint = new Point(x, YAxisScalePoint(finalSize) - margin);
                myLineGeometry2.EndPoint = new Point(x, YAxisScalePoint(finalSize));

                myLineGeometry2.Transform = new TranslateTransform() { X = myLineGeometry.StartPoint.Y, Y = myLineGeometry.StartPoint.X };
                xaxis_geometry.Children.Add(myLineGeometry2);
                ////====
                //LineGeometry myLineGeometry12 = new LineGeometry();
                //myLineGeometry12.StartPoint = new Point(x, YAxisScalePoint(finalSize));
                //myLineGeometry12.EndPoint = new Point(x, YAxisScalePoint(finalSize)-forWindowSize(finalSize));
                //xaxis_geometry.Children.Add(myLineGeometry12);
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

using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace WTBoxPannel
{
    internal class ChartPanel : Panel, IDisposable
    {
        private Canvas canvas;
        private Grid grid;
        public TextBlock text1;
        const double margin = 10;
        const double totalMargin = 40;
        internal IList<KeyValuePair<int,int>> collection;
        internal ChartModelCollection chartModelCollection;
        private bool isInArrange;


        public ChartPanel()
        {
            grid = new Grid();
            grid.Margin = new Thickness(10);
            ColumnDefinition gridColumnDefinition1 = new ColumnDefinition();
            gridColumnDefinition1.Width = new GridLength(0.5,GridUnitType.Auto);
            ColumnDefinition gridColumnDefinition2 = new ColumnDefinition();
            gridColumnDefinition2.Width = new GridLength(0.5, GridUnitType.Star);

            grid.ColumnDefinitions.Add(gridColumnDefinition1);
            grid.ColumnDefinitions.Add(gridColumnDefinition2);
            RowDefinition gridRowDefinition1 = new RowDefinition();
            gridRowDefinition1.Height = new GridLength(0.5, GridUnitType.Auto);
            RowDefinition gridRowDefinition2 = new RowDefinition();
            gridRowDefinition2.Height = new GridLength(0.5, GridUnitType.Star);
            RowDefinition gridRowDefinition3 = new RowDefinition();
            gridRowDefinition3.Height = new GridLength(0.5, GridUnitType.Auto);

            grid.RowDefinitions.Add(gridRowDefinition1);
            grid.RowDefinitions.Add(gridRowDefinition2);
            grid.RowDefinitions.Add(gridRowDefinition3);
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
                this.Children.Add(grid);
            }
            if (!isInArrange)
            {
                isInArrange = true;
                if (grid.Children.Count > 0)
                    grid.Children.Clear();

                chartModelCollection.Xstep = (finalSize.Width - totalMargin) / collection.Count;
                chartModelCollection.Ystep = (finalSize.Height - totalMargin) / collection.Count;
                LineForXaxisGeometry(finalSize);
                LineForYaxisGeometry(finalSize);
            }
            return finalSize;
        }
  
        private void LineForYaxisGeometry(Size finalSize)
        {
            GeometryGroup yaxis_geometry = new GeometryGroup();
            LineGeometry myLineGeometry3 = new LineGeometry();
            myLineGeometry3.StartPoint = new Point(20, 0);
            myLineGeometry3.EndPoint = new Point(20, finalSize.Height-30);
            myLineGeometry3.Transform = new TranslateTransform() { X = myLineGeometry3.StartPoint.Y, Y = myLineGeometry3.EndPoint.X };
            yaxis_geometry.Children.Add(myLineGeometry3);

            chartModelCollection.Ymin = 0;
            chartModelCollection.Xmin = 0;

            for (double y = chartModelCollection.Ymin; y <= finalSize.Height; y+=chartModelCollection.Ystep)
                {
                    LineGeometry myLineGeometry4 = new LineGeometry();
                    myLineGeometry4.StartPoint = new Point(XAxisScalePoint(finalSize), y);
                    myLineGeometry4.EndPoint = new Point(XAxisScalePoint(finalSize) - margin, y);
                    myLineGeometry4.Transform = new TranslateTransform() { X = myLineGeometry3.StartPoint.Y, Y = myLineGeometry3.EndPoint.X };
                    yaxis_geometry.Children.Add(myLineGeometry4);
                }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = new SolidColorBrush(Colors.Red);
            yaxis_path.Data = yaxis_geometry;
            canvas.Children.Add(yaxis_path);
        }

        private void LineForXaxisGeometry(Size finalSize)
        {
            CreateTextBlock();
            CreateCanvas();
            GeometryGroup xaxis_geometry = new GeometryGroup();
            LineGeometry myLineGeometry = new LineGeometry();
            chartModelCollection.Xmin = margin;
            myLineGeometry.StartPoint = new Point(0, 0);
            myLineGeometry.EndPoint = new Point(finalSize.Width, 0);
            myLineGeometry.Transform = new TranslateTransform() { X = myLineGeometry.StartPoint.Y, Y = finalSize.Height - 20 };

            xaxis_geometry.Children.Add(myLineGeometry);
            for (double x = chartModelCollection.Xmin+margin;
                 x <= finalSize.Width; x += chartModelCollection.Xstep)
            {
                LineGeometry myLineGeometry2 = new LineGeometry();
                myLineGeometry2.StartPoint = new Point(x, YAxisScalePoint(finalSize) - margin);
                myLineGeometry2.EndPoint = new Point(x, YAxisScalePoint(finalSize));
                myLineGeometry2.Transform = new TranslateTransform() { X = myLineGeometry.StartPoint.Y, Y = myLineGeometry.StartPoint.X };
                xaxis_geometry.Children.Add(myLineGeometry2);
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = new SolidColorBrush(Colors.Red);
            xaxis_path.Data = xaxis_geometry;
            canvas.Children.Add(xaxis_path);
        }

        private void CreateTextBlock()
        {
            text1 = new TextBlock();
            text1.Margin = new Thickness(2);
            text1.RenderTransformOrigin = new Point(0.5, 0.5);
            text1.FontSize = 14;
            text1.HorizontalAlignment = HorizontalAlignment.Stretch;
            text1.VerticalAlignment = VerticalAlignment.Stretch;
            text1.TextAlignment = TextAlignment.Center;
            text1.Text = "Title";
            Grid.SetRow(text1, 0);
            Grid.SetColumn(text1, 1);
            text1.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            grid.Children.Add(text1);
        }

        private void CreateCanvas()
        {
            canvas = new Canvas();
            canvas.Height = this.Width;
            canvas.Width = this.Width;
            canvas.Background = new SolidColorBrush(Colors.Black);
            Grid.SetColumn(canvas, 1);
            Grid.SetRow(canvas, 1);
            grid.Children.Add(canvas);
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

        public void Dispose()
        {
            if (collection != null)
            {
                collection = null;
            }
        }
    }
}

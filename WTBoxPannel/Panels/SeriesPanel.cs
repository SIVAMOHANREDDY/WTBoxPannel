using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WTBoxPannel.Panels
{
    public class SeriesPanel : Panel
    {
        public static Canvas canvas;
        const double margin = 10;
        const double step = 25;
        private double xmax;
        private double xmin;
        private double ymax;
        private double ymin;

        public SeriesPanel()
        {
            canvas = new Canvas();
            canvas.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            canvas.HorizontalAlignment = HorizontalAlignment.Center;
            canvas.VerticalAlignment = VerticalAlignment.Center;
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
               // LineForYaxisGeometry(finalSize);
            }
            return finalSize;
        }
        Point location = new Point(0,0);

        private void LineForXaxisGeometry(Size finalSize)
        {
            TextBlock text = new TextBlock();
            text.FontSize = 12;
            text.Text = "siva";
            Canvas.SetTop(text, 200);
            Canvas.SetLeft(text, 100);
            canvas.Children.Add(text);

            text.Measure(new Size(20, 0));
            double x = location.X;

            HorizontalAlignment halign = HorizontalAlignment.Right;
            VerticalAlignment valign = VerticalAlignment.Bottom;
            if (halign == HorizontalAlignment.Center)
                x -= text.DesiredSize.Width / 2;
            else if (halign == HorizontalAlignment.Right)
                x -= text.DesiredSize.Width;
            Canvas.SetLeft(text, x);

            double y = location.Y;
            if (valign == VerticalAlignment.Center)
                y -= text.DesiredSize.Height / 2;
            else if (valign == VerticalAlignment.Bottom)
                y -= text.DesiredSize.Height;
            Canvas.SetTop(text, y);

        }
    }
}

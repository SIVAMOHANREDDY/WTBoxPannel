using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WTBoxPannel.Panels
{
    public class PlotPanel : Panel
    {
        private PointCollection pointCollection;

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new Size(0, 0);

            foreach (var child in Children)
            {
                child.Measure(availableSize);
            }

            size.Width = double.IsPositiveInfinity(availableSize.Width) ?
               size.Width : availableSize.Width;

            size.Height = double.IsPositiveInfinity(availableSize.Height) ?
               size.Height : availableSize.Height;

            return size;
        }

        public void Arrange(Size finalSize, PointCollection collection)
        {
            pointCollection = collection;
            ArrangeOverride(finalSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Children.Count > 0)
            {
                foreach (var item in Children)
                {
                    // Arrangement needs to happen.
                    // Measurement needs to be checked.
                    item.Arrange(new Rect(pointCollection.First().X, pointCollection.First().Y, finalSize.Width, finalSize.Height));
                    break;
                }
            }
            return finalSize;
        }
    }
}

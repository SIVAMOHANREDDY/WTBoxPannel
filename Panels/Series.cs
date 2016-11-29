using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace WTBoxPannel.Panels
{
    public abstract class Series : FrameworkElement
    {
        public Series()
        {
            
        }

        public List<KeyValuePair<int,int>> DataCollection
        {
            get { return (List<KeyValuePair<int,int>>)GetValue(DataCollectionProperty); }
            set { SetValue(DataCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataCollectionProperty =
            DependencyProperty.Register(nameof(DataCollection), typeof(List<KeyValuePair<int,int>>), typeof(Series), new PropertyMetadata(null, new PropertyChangedCallback(OnDataChanged)));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var series = d as Series;
            var dc = e.NewValue as List<KeyValuePair<int, int>>;
            if (dc != null)
            {

            }
         }

        public int BindingPathX
        {
            get { return (int)GetValue(BindingPathXProperty); }
            set { SetValue(BindingPathXProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BindingPathX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingPathXProperty =
            DependencyProperty.Register(nameof(BindingPathX), typeof(int), typeof(Series), new PropertyMetadata(0));

        public int BindingPathY
        {
            get { return (int)GetValue(BindingPathYProperty); }
            set { SetValue(BindingPathYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindingPathY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingPathYProperty =
            DependencyProperty.Register(nameof(BindingPathY), typeof(int), typeof(Series), new PropertyMetadata(0));

        public bool ShowGridLines
        {
            get { return (bool)GetValue(ShowGridLinesProperty); }
            set { SetValue(ShowGridLinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowGridLines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowGridLinesProperty =
            DependencyProperty.Register(nameof(ShowGridLines), typeof(bool), typeof(Series), new PropertyMetadata(true, OnShowGridLinesChanged));

        private static void OnShowGridLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}

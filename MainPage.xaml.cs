using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WTBoxPannel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       public List<KeyValuePair<int, int>> DataCollection=new List<KeyValuePair<int, int>>();
        public MainPage()
        {
            this.InitializeComponent();
            InitializeSettings();
            InitializeDatasource();
            DataContext = this;
        }

        private void InitializeDatasource()
        {
            DataCollection.Add(new KeyValuePair<int, int>(1000, 2000));
            DataCollection.Add(new KeyValuePair<int, int>(1200, 1000));
            DataCollection.Add(new KeyValuePair<int, int>(3000, 3000));
            DataCollection.Add(new KeyValuePair<int, int>(4000, 5000));
            DataCollection.Add(new KeyValuePair<int, int>(2000, 4000));
        }

        private void InitializeSettings()
        {
            this.LineColorCbx.ItemsSource = new List<SolidColorBrush>() { new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Green) };
            this.LineColorCbx.SelectionChanged += LineColorCbx_SelectionChanged;
        }

        private void LineColorCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChartLineSeries.LineColor = e.AddedItems[0] as SolidColorBrush;
        }
    }
}

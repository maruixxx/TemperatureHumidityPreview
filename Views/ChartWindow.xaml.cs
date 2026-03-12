using System.Windows;
using TemperatureHumidityPreview.ViewModels;

namespace TemperatureHumidityPreview.Views
{
    public partial class ChartWindow : Window
    {
        public ChartWindow(ChartViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
// Assuming we are adding a RelayCommand for the OpenChartCommand

public ICommand OpenChartCommand { get; private set; }

public MainViewModel()
{
    OpenChartCommand = new RelayCommand(OpenChart);
}

private void OpenChart()
{
    // Logic to open the chart window
    // Example: ChartWindow chartWindow = new ChartWindow();
    // chartWindow.Show();
}
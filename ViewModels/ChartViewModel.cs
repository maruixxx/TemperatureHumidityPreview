using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using TemperatureHumidityPreview.Models;

namespace TemperatureHumidityPreview.ViewModels {
    public class ChartViewModel : INotifyPropertyChanged {
        private PlotModel _plotModel;

        public PlotModel PlotModel {
            get => _plotModel;
            set {
                if (_plotModel != value) {
                    _plotModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public ChartViewModel(IEnumerable<SensorData> sensorData) {
            InitializeChart(sensorData);
        }

        private void InitializeChart(IEnumerable<SensorData> sensorData) {
            var plotModel = new PlotModel {
                Title = "温湿度曲线",
                Background = OxyColors.White,
                TitleFontSize = 16,
                Padding = new OxyThickness(60, 10, 20, 60)
            };

            var xAxis = new DateTimeAxis {
                Position = AxisPosition.Bottom,
                Title = "时间",
                StringFormat = "HH:mm:ss",
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
            };
            plotModel.Axes.Add(xAxis);

            var yAxisTemperature = new LinearAxis {
                Position = AxisPosition.Left,
                Title = "温度 (°C)",
                Foreground = OxyColors.Red,
                TitleColor = OxyColors.Red,
                TextColor = OxyColors.Red,
                Key = "TemperatureAxis"
            };
            plotModel.Axes.Add(yAxisTemperature);

            var yAxisHumidity = new LinearAxis {
                Position = AxisPosition.Right,
                Title = "湿度 (%)",
                Foreground = OxyColors.Blue,
                TitleColor = OxyColors.Blue,
                TextColor = OxyColors.Blue,
                Maximum = 100,
                Minimum = 0,
                Key = "HumidityAxis"
            };
            plotModel.Axes.Add(yAxisHumidity);

            var temperatureSeries = new LineSeries {
                Title = "温度",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                YAxisKey = "TemperatureAxis",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            var humiditySeries = new LineSeries {
                Title = "湿度",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                YAxisKey = "HumidityAxis",
                MarkerType = MarkerType.Square,
                MarkerSize = 3
            };

            foreach (var data in sensorData) {
                double timeValue = OxyPlot.Axes.DateTimeAxis.ToDouble(data.Timestamp);
                temperatureSeries.Points.Add(new DataPoint(timeValue, data.Temperature));
                humiditySeries.Points.Add(new DataPoint(timeValue, data.Humidity));
            }

            plotModel.Series.Add(temperatureSeries);
            plotModel.Series.Add(humiditySeries);
            plotModel.LegendPosition = LegendPosition.TopLeft;
            plotModel.LegendBackground = OxyColor.FromArgb(200, 255, 255, 255);
            PlotModel = plotModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
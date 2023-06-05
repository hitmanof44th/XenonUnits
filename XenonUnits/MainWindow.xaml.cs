using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace XenonUnits
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const double MaxValue = 8000.0;
        private const double GaugeRadius = 150.0;
        private const double NeedleLength = 130.0;
        private const double NeedleWidth = 8.0;

        private double currentValue = 0.0;
        private double targetValue = 0.0;
        private double stepSize = 10.0;

        public MainWindow()
        {
            InitializeComponent();
            CreateGauge();
            StartAnimation();
        }


        private void CreateGauge()
        {
            gaugeCanvas.Width = 300;
            gaugeCanvas.Height = 400;
            background.Width = GaugeRadius * 2;
            background.Height = GaugeRadius * 2;
            Canvas.SetLeft(background, (gaugeCanvas.Width - background.Width) / 2);
            Canvas.SetTop(background, (gaugeCanvas.Height - background.Height) / 2);

            needle.X1 = gaugeCanvas.Width / 2;
            needle.Y1 = gaugeCanvas.Height / 2;
            needle.X2 = gaugeCanvas.Width / 2;
            needle.Y2 = gaugeCanvas.Height / 2 - NeedleLength;
            needle.StrokeThickness = NeedleWidth;

            Canvas.SetLeft(unitTextBlock, (gaugeCanvas.Width - unitTextBlock.ActualWidth) / 2);
            Canvas.SetTop(unitTextBlock, gaugeCanvas.Height - unitTextBlock.ActualHeight - 10);

            Canvas.SetLeft(minValueTextBlock, 10);
            Canvas.SetTop(minValueTextBlock, gaugeCanvas.Height / 2 - 15);

            Canvas.SetRight(maxValueTextBlock, 10);
            Canvas.SetTop(maxValueTextBlock, gaugeCanvas.Height / 2 - 15);
        }

        private void StartAnimation()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            timer.Tick += UpdateValue;
            timer.Start();
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            // Simulate value changes
            if (Math.Abs(currentValue - targetValue) < stepSize)
            {
                targetValue = new Random().NextDouble() * MaxValue;
            }
            else if (currentValue < targetValue)
            {
                currentValue += stepSize;
            }
            else if (currentValue > targetValue)
            {
                currentValue -= stepSize;
            }

            // Update the needle rotation angle
            var angle = currentValue / MaxValue * 180.0 - 90.0;
            var radian = angle * Math.PI / 180.0;
            var centerX = needle.X1 + NeedleLength * Math.Cos(radian);
            var centerY = needle.Y1 + NeedleLength * Math.Sin(radian);
            needle.X2 = centerX;
            needle.Y2 = centerY;
        }
    }
}

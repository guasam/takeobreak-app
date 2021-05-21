using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace takeobreak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan time;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContentGrid.Focus();
        }

        private void DurationNumberValidation(object sender, TextCompositionEventArgs e)
        {
            var length = e.Text.Length + DurationNumberTextBox.Text.Length;
            var selected = DurationNumberTextBox.SelectedText.Length;
            var regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text) || length > 4;

            // Prevent 0 at first place
            if (e.Handled == false && int.Parse(e.Text) == 0)
            {
                if (length <= 1 || selected > 0)
                    e.Handled = true;
            }

        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            var color = (Color) ColorConverter.ConvertFromString("#FF7FFF8B");
            MessageTextBlock.Foreground = new SolidColorBrush(color);

            var duration = int.Parse(DurationNumberTextBox.Text);
            var period = DurationTag();

            // Create Timespam based on duration
            time = DurationPeriod(duration, period);

            // Initial update of text
            MessageTextBlock.Text = "Next take-o-break in " + time.ToString("c");

            // Dispatcher Timer
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                MessageTextBlock.Text = "Next take-o-break in " + time.ToString("c");

                if (time == TimeSpan.Zero)
                {
                    timer.Stop();

                    Console.WriteLine("Timer Finished");
                }

                // Tick the time for a second
                time = time.Add(TimeSpan.FromSeconds(-1));
                
            }, Application.Current.Dispatcher);

        }

        private string DurationTag()
        {
            var item = Duration_ComboBox.SelectedItem as ComboBoxItem;
            return item.Tag == null ? "" : item.Tag.ToString();
        }

        private TimeSpan DurationPeriod(double duration, string period)
        {
            TimeSpan span = TimeSpan.Zero;

            if (period == "seconds")
            {
                span =  TimeSpan.FromSeconds(duration);
            }
            else if (period == "minutes")
            {
                span = TimeSpan.FromMinutes(duration);
            }
            else if (period == "hours")
            {
                span = TimeSpan.FromHours(duration);
            }

            return span;
        }
    }
}

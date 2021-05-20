using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace takeobreak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || length > 4;

            // PRevent 0 at first place
            if (e.Handled == false && int.Parse(e.Text) <= 0 && length == 1)
            {
                e.Handled = true;
            }
        }

        private void Duration_Changed(object sender, SelectionChangedEventArgs e)
        {
            var item = Duration_ComboBox.SelectedItem as ComboBoxItem;
            
            if (item.Tag != null)
            {
                MessageTextBlock.Text = item.Tag.ToString();
            }
        }
    }
}

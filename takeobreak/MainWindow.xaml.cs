using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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
        }
    }
}

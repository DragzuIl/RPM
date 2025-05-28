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

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTheme("Themes/LightTheme.xaml");
        }

        private void LoadTheme(string themePath)
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            var themeDict = new ResourceDictionary
            {
                Source = new System.Uri(themePath, System.UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(themeDict);
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            LoadTheme("Themes/LightTheme.xaml");
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            LoadTheme("Themes/DarkTheme.xaml");
        }
        private bool isDefaultFont = true;

        private void ToggleFont_Click(object sender, RoutedEventArgs e)
        {
            string font = isDefaultFont ? "Comic Sans MS" : "Segoe UI";

            Application.Current.Resources["AppFont"] = new FontFamily(font);
            isDefaultFont = !isDefaultFont;
        }
        private bool isLargeFont = false;

        private void ToggleFontSize_Click(object sender, RoutedEventArgs e)
        {
            double newSize = isLargeFont ? 14 : 20;

            Application.Current.Resources["AppFontSize"] = newSize;
            isLargeFont = !isLargeFont;
        }
    }
}

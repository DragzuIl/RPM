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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Person> people = new List<Person>
            {
                new Person { Name = "Иван", Age = 25, Email = "ivan@example.com" },
                new Person { Name = "Мария", Age = 30, Email = "maria@example.com" },
                new Person { Name = "Пётр", Age = 22, Email = "petr@example.com" }
            };

            dataGrid.ItemsSource = people;
        }
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> people;
        public MainWindow()
        {
            InitializeComponent();

            people = new ObservableCollection<Person>
            {
                new Person { Name = "Иван", Age = 25, Email = "ivan@example.com" },
                new Person { Name = "Мария", Age = 30, Email = "maria@example.com" },
                new Person { Name = "Пётр", Age = 22, Email = "petr@example.com" }
            };

            dataGrid.ItemsSource = people;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newPerson = new Person { Name = "Новый", Age = 0, Email = "email@example.com" };
            people.Add(newPerson);
            dataGrid.SelectedItem = newPerson;
            dataGrid.ScrollIntoView(newPerson);

            dataGrid.CurrentCell = new DataGridCellInfo(newPerson, dataGrid.Columns[0]);
            dataGrid.BeginEdit();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                dataGrid.BeginEdit();
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Person selectedPerson)
            {
                if (MessageBox.Show($"Удалить {selectedPerson.Name}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    people.Remove(selectedPerson);
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SelectAll();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPeople = dataGrid.SelectedItems.Cast<Person>().ToList();

            if (selectedPeople.Count == 0)
            {
                MessageBox.Show("Выберите одну или несколько строк для удаления.");
                return;
            }

            if (MessageBox.Show($"Удалить {selectedPeople.Count} записей?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var person in selectedPeople)
                {
                    people.Remove(person);
                }
            }
        }
    }
    public class Person : INotifyPropertyChanged
    {
        private string name;
        private int age;
        private string email;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged("Name"); }
        }

        public int Age
        {
            get => age;
            set { age = value; OnPropertyChanged("Age"); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged("Email"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

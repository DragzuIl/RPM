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
            LoadTree();
        }
        public class Node
        {
            public string Name { get; set; }
            public List<Node> Children { get; set; } = new List<Node>();

            public override string ToString()
            {
                return Name;
            }
        }
        private void LoadTree()
        {
            var rootNodes = new List<Node>
            {
                new Node
                {
                    Name = "Фрукты",
                    Children = new List<Node>
                    {
                        new Node { Name = "Яблоко" },
                        new Node { Name = "Банан" },
                        new Node { Name = "Цитрусовые", Children = new List<Node>
                            {
                                new Node { Name = "Апельсин" },
                                new Node { Name = "Лимон" }
                            }
                        }
                    }
                },
                new Node
                {
                    Name = "Овощи",
                    Children = new List<Node>
                    {
                        new Node { Name = "Морковь" },
                        new Node { Name = "Картофель" }
                    }
                }
            };

            foreach (var node in rootNodes)
            {
                MyTreeView.Items.Add(CreateTreeViewItem(node));
            }
        }
        private TreeViewItem CreateTreeViewItem(Node node)
        {
            var item = new TreeViewItem { Header = node.Name, Tag = node };

            foreach (var child in node.Children)
            {
                item.Items.Add(CreateTreeViewItem(child));
            }

            return item;
        }
        private void MyTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (MyTreeView.SelectedItem is TreeViewItem selectedItem)
            {
                var node = selectedItem.Tag as Node;

                if (node != null)
                {
                    if (node.Children.Count > 0)
                    {
                        selectedItem.IsExpanded = !selectedItem.IsExpanded;
                    }
                    else
                    {
                        MessageBox.Show($"Вы выбрали листовой узел: {node.Name}", "Информация");
                    }
                }
            }
        }
    }
}

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

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Node
        {
            public string Name { get; set; }
            public List<Node> Children { get; set; } = new List<Node>();

            public override string ToString() => Name;
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadTree();
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
                        new Node
                        {
                            Name = "Цитрусовые",
                            Children = new List<Node>
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
                if (node != null && node.Children.Count > 0)
                {
                    selectedItem.IsExpanded = !selectedItem.IsExpanded;
                }
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newNodeName = "Новый элемент(не знаю как изменить название)";

            if (MyTreeView.SelectedItem is TreeViewItem selectedItem)
            {
                var parentNode = selectedItem.Tag as Node;

                if (parentNode != null)
                {
                    var newNode = new Node { Name = newNodeName };
                    parentNode.Children.Add(newNode);

                    var newTreeViewItem = CreateTreeViewItem(newNode);
                    selectedItem.Items.Add(newTreeViewItem);
                    selectedItem.IsExpanded = true;
                }
            }
            else
            {
                var rootNode = new Node { Name = newNodeName };
                MyTreeView.Items.Add(CreateTreeViewItem(rootNode));
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyTreeView.SelectedItem is TreeViewItem selectedItem)
            {
                var parent = GetParent(selectedItem);

                if (selectedItem.Tag is Node node)
                {
                    if (parent != null)
                    {
                        if (parent.Tag is Node parentNode)
                        {
                            parentNode.Children.Remove(node);
                        }
                        parent.Items.Remove(selectedItem);
                    }
                    else
                    {
                        MyTreeView.Items.Remove(selectedItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления", "Внимание");
            }
        }

        private TreeViewItem GetParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);

            while (parent != null && !(parent is TreeViewItem))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeViewItem;
        }
    }
}

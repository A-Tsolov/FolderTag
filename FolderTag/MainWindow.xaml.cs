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
using System.IO;
using System.Windows.Forms;

namespace FolderTag
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseDirectory(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    path = fbd.SelectedPath;
                }
            }
            ListDirectory(DirectoryTree, path);
        }

        private void ListDirectory(System.Windows.Controls.TreeView treeView, string path)
        {
            treeView.Items.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            TreeViewItem root = CreateDirectoryNode(rootDirectoryInfo);
            root.IsExpanded = true;
            treeView.Items.Add(root);
        }

        private static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryinfo)
        {
            TreeViewItem node = new TreeViewItem();
            foreach (var directory in directoryinfo.GetDirectories())
            {
                node.Items.Add(CreateDirectoryNode(directory));
            }
            foreach (var file in directoryinfo.GetFiles())
            {
                node.Items.Add(new TreeViewItem() { Header = file });
            }
            node.Header = directoryinfo.ToString();
            return node;
        }

        private void AddEntry(object sender, RoutedEventArgs e)
        {
            string tagsStr = TagBox.Text;
            List<string> tags = tagsStr.Split(' ').ToList();
            TreeViewItem selectedNode = DirectoryTree.SelectedItem as TreeViewItem;
            string path = selectedNode.Header.ToString();
            Node node = new Node(tags, Int32.Parse(RatingBox.Text), path);
            Results.Items.Add(node.ToString());
            DataGrid1.Items.Add(node.ToString());
        }


    }
}

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
        private string pathRoot;


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
                    pathRoot = fbd.SelectedPath;
                }
            }
            ListDirectory(DirectoryTree, pathRoot);
        }

        // Populate TreeView
        private void ListDirectory(System.Windows.Controls.TreeView treeView, string path)
        {
            treeView.Items.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            TreeViewItem root = CreateDirectoryNode(rootDirectoryInfo);
            root.IsExpanded = true;
            treeView.Items.Add(root);
            PopulateResult();
        }

        // Populate TreeView
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

        // Add Entry
        private void AddEntry(object sender, RoutedEventArgs e)
        {
            string tagsStr = TagBox.Text;
            List<string> tags = tagsStr.Split(' ').ToList();
            TreeViewItem selectedNode = DirectoryTree.SelectedItem as TreeViewItem;
            int rating = Int32.Parse(RatingBox.Text);
            string path = getPath(selectedNode);
            Node node = Constructor.createNode(tags,rating,path,"File");
            if (node != null)
            {
                Results.Items.Add(node);
            }
            
        }

        // Returns the path of the selected node
        private string getPath(TreeViewItem item)
        {
            string path = "";
            string filename = item.Header.ToString();
            TreeViewItem currentNode = item;
            while (currentNode != null)
            {
                path = "\\" + currentNode.Header.ToString() + path;
                currentNode = currentNode.Parent as TreeViewItem;
            }
            return path.TrimStart('\\');
        }

        private void PopulateResult()
        {
            List<Node> entries = Constructor.GetEntries();
            foreach(Node node in entries)
            {
                Results.Items.Add(node.GetPath());
            }
        }

        private void AddEntryToTrees(Node node)
        {
            Results.Items.Add(node.GetPath());
            ResultsFirstPage.Items.Add(node);
        }

    }
}

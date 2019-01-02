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
using System.Data;

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
            if (pathRoot != null){
                ListDirectory(DirectoryTree, pathRoot);
            }
            
        }

        // Populate TreeView
        private void ListDirectory(System.Windows.Controls.TreeView treeView, string path)
        {
            treeView.Items.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            TreeViewItem root = CreateDirectoryNode(rootDirectoryInfo);
            root.IsExpanded = true;
            treeView.Items.Add(root);
            //PopulateResult();
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
            List<string> tags = FormTagList(tagsStr);
            TreeViewItem selectedNode = DirectoryTree.SelectedItem as TreeViewItem;
            if (selectedNode == null){
                System.Windows.MessageBox.Show("Select a file");
                return;
            }
            int rating = 0;
            try
            {
                rating = Int32.Parse(RatingBox.Text);
            }
            catch (System.FormatException)
            {
                System.Windows.MessageBox.Show("Enter a score");
                return;
            }
            string path = getPath(selectedNode);

            Node node = null;
            try
            {
                node = Constructor.createNode(tags, rating, path, "File");
            }
            catch (FileNotFoundException)
            {
                System.Windows.MessageBox.Show("Does not work with folders");
            }

            if (node != null)
            {

                //Results.Items.Add(node);
                ShowTags();
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

        //private void PopulateResult()
        //{
        //    List<Node> entries = Constructor.GetEntries();
        //    foreach (Node node in entries)
        //    {
        //        Results.Items.Add(node.GetPath());
        //    }
        //}

        private void AddEntryToTrees(Node node)
        {
            //Results.Items.Add(node.GetPath());
            TagsFirstPage.Items.Add(node);
        }

        // Take string of tags from the textbox and returns a list of strings 
        private List<string> FormTagList(string tagsStr)
        {
            List<string> tags = tagsStr.Split(' ').ToList();
            return tags;
        }

        //private void UpdateTags(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{

        //}



        private void ShowTags()
        {
            TagsFirstPage.Items.Clear();
            TreeViewItem selectedNode = DirectoryTree.SelectedItem as TreeViewItem;
            if (selectedNode == null)
            {
                return;
            }
            string path = getPath(selectedNode);
            Node node = null;
            if (System.IO.Directory.Exists(path)) {
                node = Constructor.ReturnFolderWithPath(path);
            }
            if (System.IO.File.Exists(path)){
                long size = new System.IO.FileInfo(path).Length;
                node = Constructor.ReturnNodeWithSize(size);
            }
            
            if (node != null)
            {
                List<string> tags = node.GetTags();
                foreach (string tag in tags)
                {
                    TagsFirstPage.Items.Add(tag);
                }
            }
        }



        private void PrintTags(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ShowTags();
        }

        private void RemoveTag(object sender, RoutedEventArgs e)
        {
            var selectedObject = TagsFirstPage.SelectedItem;
            if (selectedObject == null)
            {
                return;
            }
            string selectedTag = selectedObject.ToString();
            TreeViewItem selectedNode = DirectoryTree.SelectedItem as TreeViewItem;
            string path = getPath(selectedNode);
            long size = new System.IO.FileInfo(path).Length;
            Node node = Constructor.ReturnNodeWithSize(size);
            if (node != null)
            {
                node.RemoveTag(selectedTag);
            }
            ShowTags();
        }

        private void fillData()
        {
            List<Node> entries = search();
            foreach (Node entry in entries)
            {
                entriesGrid.Items.Add(entry);
            }
        }

        private void load_entries(object sender, RoutedEventArgs e)
        {
            entriesGrid.Items.Clear();
            fillData();
        }

        private List<Node> search()
        {
            List<string> tagsInclude = FormTagList(TagsToInclude.Text);
            List<string> tagsExclude = FormTagList(TagsToExclude.Text);

            List<Node> entries = Constructor.GetEntries();
            List<Node> searchResult = new List<Node>();
            foreach (Node entry in entries)
            {
                searchResult.Add(entry);
                foreach (string tag in tagsInclude)
                {
                    if ((TagsToInclude.Text != "") && !(entry.GetTags().Contains(tag)))
                    {
                        searchResult.Remove(entry);
                    }
                }
                foreach (string tag in tagsExclude)
                {
                    if ((TagsToExclude.Text != "") && entry.GetTags().Contains(tag))
                    {
                        searchResult.Remove(entry);
                    }
                }
            }
            return searchResult;
        }
    }
}
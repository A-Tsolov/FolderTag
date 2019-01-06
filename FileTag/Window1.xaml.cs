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
using System.Windows.Shapes;

namespace FolderTag
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string path;
        public Window1(string path)
        {
            this.path = path;
            InitializeComponent();
        }

        private void UpdateTags(object sender, RoutedEventArgs e)
        {
            string newTagsString = TagsEditField.Text;
            List<string> newTags = newTagsString.Split(' ').ToList();
            Node entry = Constructor.ReturnNodeFromPath(this.path);
            entry.SetTags(newTags);
            ((MainWindow)this.Owner).FillData();

            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

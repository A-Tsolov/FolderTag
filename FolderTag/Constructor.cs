using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FolderTag
{
    static class Constructor
    {
        static private List<Node> entries = new List<Node>();

        public static Node createNode(List<string> tags, int rating, string path, string type)
        {
            if (type.Equals("Folder"))
            {
                Node node = new Folder(tags, rating, path);
                if (NodePresent(node))
                {
                    return null;
                }
                entries.Add(node);
                return node;
            }
            else if (type.Equals("File"))
            {
                long size = new System.IO.FileInfo(path).Length;
                MessageBox.Show(size.ToString());
                Node node = new File(tags, rating, path, size);
                if (NodePresent(node))
                {
                    return null;
                }
                entries.Add(node);
                return node;
            }
            else
            {
                throw new ArgumentException("Invalid type");
            }
        }

        // Check if file is present
        private static bool FilePresent(File file)
        {
            foreach (File entry in entries)
            {
                if (file.GetSize() == entry.GetSize())
                {
                    return true;
                }
            }
            return false;
        }

        // Check if folder is present
        private static bool FolderPresent(Folder folder)
        {
            foreach (Node entry in entries)
            {
                if (entry.GetPath().Equals(folder.GetPath()))
                {
                    return true;
                }
            }
            return false;
        }

        // Check if node is present
        public static bool NodePresent(Node node)
        {
            if (node is File)
            {
                File nodeFile = (File)node;
                return FilePresent(nodeFile);
            }
            else if (node is Folder)
            {
                Folder folder = (Folder)node;
                return FolderPresent(folder);
            }
            else
            {
                throw new Exception("Invalid type");
            }
        }

    public static List<Node> GetEntries()
        {
            return entries;
        }

    }
}

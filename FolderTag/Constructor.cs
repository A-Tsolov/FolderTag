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
                entries.Add(node);
                return node;
            }
            else if (type.Equals("File"))
            {
                Node node = new File(tags, rating, path);
                entries.Add(node);
                return node;
            }
            else
            {
                throw new ArgumentException("Invalid type");
            }
        }



        // Check if file is present
        public static bool filePresent(Node node)
        {
            if (node is File)
            {
                File nodeFile = (File)node;
                foreach (File file in entries)
                {
                    if(nodeFile.GetSize() == file.GetSize())
                    {
                        return true;
                    }
                }
            }
            if (node is Folder)
            {
                node = (Folder)node;
                foreach (Node file in entries)
                {
                    if (node.GetPath().Equals(file.GetPath()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    public static List<Node> GetEntries()
        {
            return entries;
        }

    }
}

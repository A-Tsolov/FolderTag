using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FolderTag
{
    class Constructor
    {
        static private List<Node> entries;

        public void createNode(List<string> tags, int rating, string path, string type)
        {
            if (type.Equals("Folder"))
            {
                Node node = new Folder(tags, rating, path);
                entries.Add(node);
            }
            else if (type.Equals("File"))
            {
                Node node = new File(tags, rating, path);
                entries.Add(node);
            }
            else
            {
                throw new ArgumentException("Invalid type");
            }
        }

        public bool filePresent(Node node)
        {
            if (node is File)
            {
                node = (File)node;
                foreach (Node file in entries)
                {
                    if(node.GetSize() == file.GetSize())
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

        public bool checkFileSize(Node file)
        {
            return (file.size)
        }

        //public static Node CreateFolder(List<string> tags, int rating, string path)
        //{
        //    Node folder = new Node(tags, rating, path);
        //    return folder;
        //}

        //public static Node CreateFile(List<string> tags, int rating, string path)
        //{
        //    Node file = new Node(tags, rating, path);
        //    return file;
        //}
    }
}

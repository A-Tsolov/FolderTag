using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTag
{
    abstract class Node
    {
        private List<string> tags;
        private int rating;
        private string path;

        public Node(List<string> tags, int rating, string path)
        {
            this.tags = tags;
            this.rating = rating;
            this.path = path;
        }

        public string GetPath()
        {
            return this.path;
        }

        public override string ToString()
        {
            return path;
        }
    }
}

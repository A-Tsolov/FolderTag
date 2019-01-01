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

        public int GetRating()
        {
            return this.rating;
        }

        public string GetPath()
        {
            return this.path;
        }

        public List<string> GetTags()
        {
            return tags;
        }

        public void SetTags(List<string> tags)
        {
            this.tags = tags;
        }

        public override string ToString()
        {
            string tagsString = String.Join(" ", tags);
            return tagsString + rating.ToString() + path;
        }

        public void AddTag(string ta)
        {

        }

        public void RemoveTag(string tag)
        {
            tags.Remove(tag);
        }
    }
}

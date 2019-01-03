using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTag
{
    [Serializable]
    class File: Node
    {
        private long size;

        public File(List<string> tags, int rating, string path, long size) : base(tags, rating, path)
        {
            this.size = size;
        }

        public long GetSize()
        {
            return this.size;
        }


    }
}

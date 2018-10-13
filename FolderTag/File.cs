using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTag
{
    class File: Node
    {
        private ulong size;

        public File(List<string> tags, int rating, string path) : base(tags, rating, path)
        {
        }

        public ulong GetSize()
        {
            return this.size;
        }
    }
}

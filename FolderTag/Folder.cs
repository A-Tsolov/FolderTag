using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderTag
{
    class Folder: Node
    {

        public Folder(List<string> tags, int rating, string path) : base(tags, rating, path)
        {
        }

    }
}

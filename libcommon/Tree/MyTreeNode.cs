using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JedinejProjektCoUdelam
{
    public class MyTreeNode
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public MyTreeNode Parent { get; set; }

        public int Depth
        {
            get
            {
                int depth = 0;
                var node = this;
                while ((node = node.Parent) != null)
                {
                    depth++;
                }
                return depth;
            }
        }

        public MyTreeNode(int id, string name, int left, int right)
        {
            this.ID = id;
            this.Name = name;
            this.Left = left;
            this.Right = right;
        }

    }
}

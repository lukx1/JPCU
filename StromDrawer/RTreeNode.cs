using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StromDrawer
{
    public class RTreeNode
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public RTreeNode Parent { get; set; }
        public IEnumerable<RTreeNode> Children { get; set; } = new List<RTreeNode>();

        public Point Location { get; set; }
        public Size Size { get; set; }

        public bool Drawn { get; set; } = false;

        public bool IsEmpty() => Children.Count() == 0;

        public int ZIndex { get; set; } = 0;

        public bool ShouldDraw { get; set; } = true;

        public bool Contains(Point p)
        {
            return new Rectangle(Location, Size).Contains(p);
        }

        public bool Collapsed { get; set; } = false;

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

        public RTreeNode(int id, string name, int left, int right)
        {
            this.ID = id;
            this.Name = name;
            this.Left = left;
            this.Right = right;
        }
    }
}

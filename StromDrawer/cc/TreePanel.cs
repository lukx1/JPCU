using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StromDrawer.cc
{
    public class TreePanel : Panel
    {
        public List<MyTreeNodeElem> Nodes => new List<MyTreeNodeElem>();

        public IEnumerable<MyTreeNodeElem> NodesToDraw
        {
            get
            {
                return Nodes.Where(
                    r => FindNodeWithID(r.ParentID) == null ? true : FindNodeWithID(r.ParentID).Collapsed);
            }
            
        }
        public void AddNode(MyTreeNodeElem node)
        {
            Nodes.Add(node);
        }

        private const int LEFT_OFFSET = -2;

        public MyTreeNodeElem FindNodeWithID(int? id)
        {
            return Nodes.Where(r => r.ID == id).FirstOrDefault();
        }
        public int GetDepth(MyTreeNodeElem e)
        {
            int jumps = 0;
            var node = e;
            while(node.ParentID != null)
            {
                node = Nodes.Where(r => r.ID == node.ParentID).FirstOrDefault();
                jumps++;
            }
            return jumps;
        }

        public Point CenterOfNode(MyTreeNodeElem e)
        {
            return new Point(e.Location.X+ LEFT_OFFSET, e.Location.Y + e.Height/2);
        }

        private void DrawBack(Graphics g)
        {
            var firstNode = Nodes.Where(r => r.LeftVal == Nodes.Min(l => l.LeftVal)).FirstOrDefault();
            var start = CenterOfNode(firstNode);
            var finishNode = Nodes.Where(r => r.RightVal == Nodes.Max(l => l.RightVal)).FirstOrDefault();
            var finish = CenterOfNode(finishNode);
            g.DrawLine(Pens.Black, start, finish);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //DrawBack(e.Graphics);
           // DrawConnection(e.Graphics);
        }
    }
}

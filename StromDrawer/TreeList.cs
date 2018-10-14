using JedinejProjektCoUdelam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StromDrawer
{
    public partial class TreeList : PictureBox
    {
        public List<RTreeNode> Nodes { get; set; } = new List<RTreeNode>();
        public List<RTreeNode> DrawnNodes { get; private set; } = new List<RTreeNode>();
        public IEnumerable<RTreeNode> NodesToDraw => Nodes.Where(r => !r.Collapsed).OrderBy(r => r.Left);
        public int SPACING = 20;

        private RTreeNode ClickedNode { get; set; } = null;
        private Point LatchPoint { get; set; } = new Point();

        private bool LastClickDrag = false;

        private bool IsSoftRefresh = false;

        private List<RTreeNode> NodesMoved = new List<RTreeNode>();

        private RTreeNode NodeBeingMoved = null;

        public void AddNode(RTreeNode n)
        {
            Nodes.Add(n);
        }
        public TreeList()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
            InitializeComponent();

            MouseMove += MouseMoveCascade;
            MouseDown += MouseDownCascade;
            MouseUp += MouseUpCascade;
        }
        private void MouseUpOnElem(RTreeNode node, MouseEventArgs e)
        {
            if (!node.IsEmpty() && !LastClickDrag)
            {
                node.Collapsed = !node.Collapsed;
                HardRefresh();
                
            }
            
            LastClickDrag = false;
            NodeBeingMoved = null;
        }

        RTreeNode FindNodeClosestTo(RTreeNode fromNode,Point p, out bool inside)
        {
            RTreeNode best = null;
            double dist = 9999999;
            inside = false;
            foreach (var node in DrawnNodes.Where(r => r != fromNode && !fromNode.Children.Contains(r)))
            {
                if (node.Contains(p))
                {
                    inside = true;
                    return node;
                }
                var ptA = p;
                var ptB = node.Location + new Size(0, node.Size.Height);
                var rdist = Math.Sqrt((ptA.X - ptB.X) * (ptA.X - ptB.X) + (ptA.Y - ptB.Y) * (ptA.Y - ptB.Y));
                if (rdist < dist)
                {
                    dist = rdist;
                    best = node;
                }
            }
            /*for (int i = 0; i < DrawnNodes.Count; i++)
            {
                var node = DrawnNodes[i];
                if (node == fromNode)
                    continue;
                var ptA = p;
                var ptB = node.Location + new Size(0, node.Size.Height);
                var rdist = Math.Sqrt((ptA.X-ptB.X)* (ptA.X - ptB.X)+ (ptA.Y - ptB.Y)* (ptA.Y - ptB.Y));
                if(rdist < dist)
                {
                    dist = rdist;
                    best = node;
                }

                contPlace:
                continue;
            }*/
            return best;
        }

        private void DragStopped(MouseEventArgs e)
        {
            var topNode = NodesMoved.Where(r => r.Location.Y == NodesMoved.Min(l => l.Location.Y)).FirstOrDefault();
            var inside = false;
            var placeAfterNode = FindNodeClosestTo(topNode,e.Location,out inside);
            //MessageBox.Show(placeAfterNode.Name);

            NodesMoveTo(topNode, placeAfterNode, inside);

            HardRefresh();
            NodesMoved.Clear();
        }
        private void SoftRefresh()
        {
            IsSoftRefresh = true;
            Refresh();
        }

        private void HardRefresh()
        {
            IsSoftRefresh = false;
            Refresh();
        }

        private void MouseUpCascade(object sender, MouseEventArgs e)
        {
            if (NodesMoved.Count > 0)
            {
                DragStopped(e);               
                LastClickDrag = false;
                NodeBeingMoved = null;
                HardRefresh();
                return;
            }
            foreach (var item in DrawnNodes.OrderByDescending(r => r.ZIndex))
            {
                if (item.Contains(e.Location))
                {
                    MouseUpOnElem(item, e);
                    break;
                }
                
            }
            if (LastClickDrag)
            {
                LastClickDrag = false;
                NodeBeingMoved = null;
                HardRefresh();
            }
        }

        private void MouseDownOnElem(RTreeNode node, MouseEventArgs e)
        {
            ClickedNode = node;
            LatchPoint = e.Location;
        }

        private void MouseDownCascade(object sender, MouseEventArgs e)
        {
            foreach (var item in DrawnNodes.OrderByDescending(r => r.ZIndex))
            {
                if (item.Contains(e.Location))
                {
                    MouseDownOnElem(item, e);
                    break;
                }
            }
        }

        private bool IsDragValid(Size dragAmount)
        {

            var curLoc = this.Location;
            var afterMoveLoc = curLoc + dragAmount;
            return !(afterMoveLoc.X < 0 || afterMoveLoc.Y < 0 || afterMoveLoc.X + this.Width > Parent.Width || (afterMoveLoc.Y + this.Height) > Parent.Height);
        }

        private IEnumerable<RTreeNode> MoveChildren(RTreeNode topNode, Size size)
        {
            var res = new List<RTreeNode>();
            res.Add(topNode);
            topNode.ZIndex = DrawnNodes.Max(r => r.ZIndex)+1;
            topNode.Location += size;
            if (!topNode.Collapsed)
            {
                foreach (var node in topNode.Children.OrderBy(r => r.Left))
                {
                    res.AddRange(MoveChildren(node,size));
                }
            }
            return res;
        }

        
        private void MouseMoveOnElem(RTreeNode node, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                var moveAmount = new Size(e.X - LatchPoint.X, e.Y - LatchPoint.Y);
                if (IsDragValid(moveAmount))
                {
                    LatchPoint += moveAmount;
                    NodesMoved.Clear();
                    NodesMoved.AddRange(MoveChildren(node, moveAmount));
                    LastClickDrag = true;
                    NodeBeingMoved = node;
                    SoftRefresh();
                }
            }
        }

        private void MouseMoveCascade(object sender, MouseEventArgs e)
        {
            foreach (var item in DrawnNodes.OrderBy(r => r.ZIndex))
            {
                if (item.Contains(e.Location))
                {
                    MouseMoveOnElem(item, e);
                    break;
                }
            }
        }

        private void CalculateChildren(Graphics g, RTreeNode topNode, int depth, ref int y, bool DrawTop = false)
        {
            CalculateIndividual(g, topNode, depth, ref y);
            if (!topNode.Collapsed)
            {
                foreach (var node in topNode.Children.OrderBy(r => r.Left))
                {
                    CalculateChildren(g, node, depth + 1, ref y);
                }
            }
        }

        private void CalculateIndividual(Graphics g, RTreeNode node, int depth, ref int y)
        {

            y += SPACING;
            int x = 0 + node.Depth*8;
            var drawString = "[" + (node.Collapsed ? "+" : "-") + "]" + node.Name;
            node.Location = new Point(x, y);
            node.Size = new Size(300, DefaultFont.Height);
            g.DrawString(drawString, DefaultFont, Brushes.Black, x, y);
            DrawnNodes.Add(node);
        }

        private void Draw(Graphics g, RTreeNode node)
        {
            var drawString = "[" + (node.IsEmpty() ? " " : node.Collapsed ? "+" : "-") + "]" + node.Name;
            node.Drawn = true;
            g.FillRectangle(new SolidBrush(Color.FromArgb(Math.Min(218+10*node.Depth,255),Math.Min(165+30*node.Depth,255),Math.Min(32+ 50*node.Depth,255))), new Rectangle(node.Location, node.Size));
            g.DrawString(drawString, DefaultFont, Brushes.Black, node.Location.X, node.Location.Y);

        }

        private void MoveNode(RTreeNode movedNode, int newPos, RTreeNode movedAfterNode)
        {
            /*// calculate position adjustment variables
            int width = movedNode.Right - movedNode.Left + 1;
            int distance = newPos - movedNode.Left;
            int tmppos = movedNode.Left;

            // backwards movement must account for new space
            if (distance < 0)
            {
                distance -= width;
                tmppos += width;
            }

            foreach (var node in Nodes) // create new space for subtree
            {
                if (node.Left >= newPos)
                {
                    node.Left += width;
                }
                if (node.Right >= newPos)
                {
                    node.Right += width;
                }
            }

            foreach (var node in Nodes) // move subtree into new space
            {
                if(node.Left >= tmppos && node.Right < tmppos + width)
                {
                    node.Left += distance;
                    node.Right += distance;
                }
            }

            foreach (var node in Nodes) // Remove space
            {
                if(node.Left > movedNode.Right)
                {
                    node.Left -= width;
                }
                if (node.Right > movedNode.Right)
                {
                    node.Right -= width;
                }
            }

            foreach (var node in NodesMoved.Where(r=>r.Depth == NodesMoved.Min(l => l.Depth)))
            {
                node.Parent = movedAfterNode;
            }

            foreach (var elem in Nodes)
            {
                elem.Children = Nodes.Where(r => r.Parent == elem).ToList();
            }*/
        }

        private bool IsChildrensChildren(RTreeNode topNode,RTreeNode afterNode)
        {
            foreach (var node in afterNode.Children)
            {
                if (node == topNode)
                    return true;
                if (node.Children.Count() > 0)
                    return IsChildrensChildren(topNode, node);
            }
            return false;
        }

        private void NodesMoveTo(RTreeNode topNodeMoved,RTreeNode afterNode, bool MovedInside = true)
        {
            if (afterNode == null)
                return;
            //if (topNodeMoved.Parent == null && afterNode.Parent == null)
            //    return;
            if (IsChildrensChildren(afterNode,topNodeMoved))
                return;
            {
                if(topNodeMoved.Parent != null)
                {
                    var pc = new List<RTreeNode>(topNodeMoved.Parent.Children);
                    pc.Remove(topNodeMoved);
                    topNodeMoved.Parent.Children = pc;
                }
                else
                {
                    Nodes.Remove(topNodeMoved);
                }
            }

            if (MovedInside)
            {
                var newList = new List<RTreeNode>(afterNode.Children);
                newList.Add(topNodeMoved);
                afterNode.Children = newList;
                topNodeMoved.Parent = afterNode;
            }
            else
            {
                if (afterNode.Parent == null)
                {
                    Nodes.Add(topNodeMoved);
                    topNodeMoved.Parent = null;
                }
                else
                {
                    var newList = new List<RTreeNode>(afterNode.Parent.Children);
                    newList.Add(topNodeMoved);
                    afterNode.Parent.Children = newList;
                    topNodeMoved.Parent = afterNode.Parent;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            int y = 0;

            if (!IsSoftRefresh)
            {
                DrawnNodes.Clear();
                foreach (var node in Nodes)
                {
                    node.Drawn = false;
                    node.ZIndex = 0;
                }
                for (int i = 0; i < Nodes.Count(); i++)
                {
                    CalculateChildren(pe.Graphics, Nodes.ElementAt(i), 0, ref y, true);
                }
            }
            foreach (var item in DrawnNodes.OrderBy(r => r.ZIndex))
            {
                Draw(pe.Graphics, item);
            }
            if (LastClickDrag)
            {
                pe.Graphics.DrawLine(Pens.Red, new Point(0, LatchPoint.Y), new Point(this.Width, LatchPoint.Y));
            }
            IsSoftRefresh = false;
        }
    }
}

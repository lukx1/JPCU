using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StromDrawer.cc
{
    public partial class MyTreeNodeElem : Control
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public int LeftVal { get; set; }
        public int RightVal { get; set; }
        public int? ParentID { get; set; }
        public IEnumerable<MyTreeNodeElem> Children { get; set; }

        private bool MoveAction = false;

        public const int DEPTH_OFFSET = 6;
        private TreePanel RealParent => (TreePanel)Parent;

        private Point LatchPoint = new Point(0, 0);

        public bool Collapsed = false;

        protected bool HasElementsBefore()
        {
            return RealParent.Nodes.Any(r => r.LeftVal < this.LeftVal);
        }
        protected int Depth => RealParent.GetDepth(this);
        public MyTreeNodeElem()
        {
            InitializeComponent();
            MouseDown += (object o, MouseEventArgs e) => LatchPoint = new Point(e.X, e.Y);
            MouseUp += Clicked;
            MouseMove += DragController;
        }

        private void Clicked(object sender, MouseEventArgs e)
        {
            if (MoveAction)
            {
                MoveAction = false;
                return;
            }
            Collapsed = !Collapsed;
        }

        private bool IsDragValid(Size dragAmount)
        {

            var curLoc = this.Location;
            var afterMoveLoc = curLoc + dragAmount;
            return !(afterMoveLoc.X < 0 || afterMoveLoc.Y < 0 || afterMoveLoc.X + this.Width > Parent.Width || (afterMoveLoc.Y + this.Height) > Parent.Height);
        }
        private void DragController(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                var moveAmount = new Size(e.X - LatchPoint.X, e.Y - LatchPoint.Y);
                if (IsDragValid(moveAmount))
                {
                    this.Location += moveAmount;
                    MoveAction = true;
                }
            }
        }

        private void PaintConnection(Graphics g)
        {
            var centerNode = RealParent.CenterOfNode(this);
            g.DrawLine(Pens.Black, DisplayRectangle.Location + new Size(0,DisplayRectangle.Height/2), DisplayRectangle.Location + new Size(DEPTH_OFFSET*(Depth+1),DisplayRectangle.Height / 2));
        }

        private Rectangle GetPaintingRectangle()
        {
            int depth = Depth+1;
            var rect = new Rectangle(DisplayRectangle.X , DisplayRectangle.Y, DisplayRectangle.Width - DEPTH_OFFSET * depth, DisplayRectangle.Height);
            return rect;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(Brushes.PaleGoldenrod, DisplayRectangle);
            pe.Graphics.DrawString(Value, new Font(FontFamily.GenericMonospace,12,FontStyle.Regular), Brushes.Black,GetPaintingRectangle() );
            //PaintConnection(pe.Graphics);
        }
    }
}

using JedinejProjektCoUdelam;
using StromDrawer.cc;
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
    public partial class Form1 : Form
    {

        private void LoadFromFile(string file)
        {
            var x = new TreeExplorer();
            var tree = x.LoadTree(file);
            var tn = CreateNodes(tree);
            var topLevel = tn.Where(r => r.Depth == 0);
            treeList.Nodes.AddRange(topLevel);
        }

        private void Init()
        {
            LoadFromFile(@"\\kacenka.litv.sssvt.cz\StudentiPrenosne\JoskaLukas\PGR\tree.csv");
        }

        private void CrtNode(MyTree t)
        {
            var x = new TreeExplorer();
            var tree = x.LoadTree(@"X:\PGR\tree.csv");
            int i = 0;
            foreach (var node in tree.Nodes)
            {
                var tNode = new MyTreeNodeElem()
                {
                    ID = node.ID,
                    LeftVal = node.Left,
                    RightVal = node.Right,
                    Value = node.Name
                };
                tNode.Width = 300;
                tNode.Height = 16;
                tNode.Location = new Point(2, i++ * 16 + 2);
                treeList.Controls.Add(tNode
                );
            }
            treeList.Refresh();
        }


        private List<RTreeNode> CreateNodes(MyTree tree)
        {
            List<RTreeNode> elems = new List<RTreeNode>();
            
            int i = 0;
            foreach (var n in tree.Nodes)
            {
                var elem = new RTreeNode(n.ID,n.Name,n.Left,n.Right);
                elems.Add(elem);
                i++;
            }
            foreach (var elem in elems)
            {
                var tn = tree.Nodes.Where(r => r.ID == elem.ID).FirstOrDefault();
                elem.Parent = elems.Where(r => r.ID == (tn.Parent == null ? -1 : tn.Parent.ID)).FirstOrDefault();
            }
            foreach (var elem in elems)
            {
                elem.Children = elems.Where(r => r.Parent == elem).ToList();
            }
            return elems;
        }

        private void DrawNodes()
        {
            
        }

        public Form1()
        {
            InitializeComponent();
            Init();
        }
    }
}

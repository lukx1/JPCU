using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JedinejProjektCoUdelam
{
    public class MyTree
    {
        public IReadOnlyList<MyTreeNode> Nodes { get; private set; }

        public MyTreeNode GetNodeWithId(int? id)
        {
            return Nodes.Where(r => r.ID == id).FirstOrDefault();
        }

        public IEnumerable<MyTreeNode> GetChildren(MyTreeNode node)
        {
            return Nodes.Where(r => r.Parent == node);
        }

        public MyTree(IEnumerable<MyTreeNode> nodes)
        {
            Nodes = new List<MyTreeNode>(nodes);
        }


    }
}

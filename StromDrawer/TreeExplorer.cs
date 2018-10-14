using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JedinejProjektCoUdelam
{
    public class TreeExplorer
    {

        private NodeParent ParseCsvLine(string line)
        {
            string[] parts = line.Split(';');
            if (parts.Length < 5)
                throw new FileLoadException("Corrupted file");
            return new NodeParent()
            {
                Node = new MyTreeNode(
                int.Parse(parts[0]),
                parts[1],
                int.Parse(parts[2]),
                int.Parse(parts[3])),
                Parent = int.Parse(parts[4])
            }; 
        }

        private struct NodeParent
        {
            public MyTreeNode Node;
            public int Parent;
        }

        public static void PrintChildren(IEnumerable<MyTreeNode> nodes)
        {
            foreach (var node in nodes.OrderBy(r => r.Left))
            {
                var spaceBuilder = new StringBuilder();
                for (int i = 0; i < node.Depth; i++)
                {
                    spaceBuilder.Append("  ");
                }
                Console.WriteLine(spaceBuilder.ToString() + node.Name);
                spaceBuilder.Length = 0;
            }
        }

        private void LinkNodes(Dictionary<int, NodeParent> parentNodes)
        {
            foreach (var pn in parentNodes)
            {
                if (pn.Value.Parent == -1)
                {
                    continue;
                }
                pn.Value.Node.Parent = parentNodes[pn.Value.Parent].Node;
            }
        }


        public MyTree LoadTree(string path, bool skipFirst = true)
        {
            var idNodes = new Dictionary<int, NodeParent>();
            using(var reader = new StreamReader(path))
            {
                string line = null;
                while((line = reader.ReadLine()) != null)
                {
                    if (skipFirst)
                    {
                        skipFirst = false;
                        continue;
                    }
                    var np = ParseCsvLine(line);
                    idNodes.Add(np.Node.ID,np);
                }
            }
            LinkNodes(idNodes);
            var tree = new MyTree(idNodes.Values.Select(r => r.Node).ToList());
            return tree;
        }

    }
}

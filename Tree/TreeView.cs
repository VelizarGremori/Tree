using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class TreeView : ITreeView
    {
        public string GetTreeView(Node root)
        {
            var stringBuilder = new StringBuilder();

            var nodes = new Stack<Node>();
            FillStack(nodes, root);

            foreach(var node in nodes.Reverse()) 
            {
                stringBuilder.Append(Environment.NewLine);
                for (int i = 0; i < node.Depth; i++)
                    stringBuilder.Append("|    ");
                stringBuilder.Append("|――――");
                stringBuilder.Append(GetNodeDiscription(node));
            }         

            return stringBuilder.ToString();
        }

        private void FillStack(Stack<Node> nodes, Node node)
        {
            nodes.Push(node);
            if (node.Type == NodeType.Directory)
            {
                foreach (var child in GetNodeChilds(node))
                    FillStack(nodes, child);
            }
        }

        public virtual IEnumerable<Node> GetNodeChilds(Node node) 
        {
            return node.Childs;
        }

        public virtual string GetNodeDiscription(Node node)
        {
            return node.FileInfo.Name;
        }
    }
}

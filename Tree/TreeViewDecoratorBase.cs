using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public abstract class TreeViewDecoratorBase : TreeView
    {

        protected TreeView TreeView;

        protected TreeViewDecoratorBase(TreeView treeView) 
        {
            TreeView = treeView;
        }

        public override IEnumerable<Node> GetNodeChilds(Node node) =>
            TreeView.GetNodeChilds(node);

        public override string GetNodeDiscription(Node node) =>
            TreeView.GetNodeDiscription(node);

    }
}

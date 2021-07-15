using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    [CommandLineParameter("sortDirection", typeof(SortDirectoinDecoratorWrapper), false, "-r", "--reverse")]
    public class SortDirectionTreeViewDecorator : TreeViewDecoratorBase
    {

        public SortDirectionTreeViewDecorator(TreeView treeView) : base(treeView) { }

        public override IEnumerable<Node> GetNodeChilds(Node node)
        {
            return base.GetNodeChilds(node).Reverse();
        }
    }

    public class SortDirectoinDecoratorWrapper : IDecoratorWrapper
    {
        public TreeView Wrap(TreeView treeView, string param)
        {
            return new SortDirectionTreeViewDecorator(treeView);
        }
    }
}

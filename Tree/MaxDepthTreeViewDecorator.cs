using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    [CommandLineParameter("maxDepth", typeof(MaxDepthDecoratorWrapper), true, "-d", "--depth")]
    public class MaxDepthTreeViewDecorator : TreeViewDecoratorBase
    {
        private readonly int _maxDepth;
        public MaxDepthTreeViewDecorator(TreeView treeView, int maxDepth) : base(treeView)
        {
            _maxDepth = maxDepth;
        }

        public override IEnumerable<Node> GetNodeChilds(Node node)
        {
            if (node.Depth >= _maxDepth)
                return Enumerable.Empty<Node>();
            return base.GetNodeChilds(node);
        }
    }

    public class MaxDepthDecoratorWrapper : IDecoratorWrapper
    {
        public TreeView Wrap(TreeView treeView, string param)
        {
            var valid = int.TryParse(param.Split()[1], out var depth);
            if (valid)
            {
                return new MaxDepthTreeViewDecorator(treeView, depth);
            }
            else
            {
                throw new Exception("Invalid argument depth:" + param);
            }
        }
    }

}

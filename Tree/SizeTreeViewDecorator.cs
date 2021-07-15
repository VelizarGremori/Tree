using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    [CommandLineParameter("size", typeof(SizeDecoratorWrapper), false, "-s", "--size", "-h", "--human-readable")]
    public class SizeTreeViewDecorator : TreeViewDecoratorBase
    {
        private readonly bool _humanReadable;

        public SizeTreeViewDecorator(TreeView treeView, bool humanReadable = false) : base(treeView) 
        {
            _humanReadable = humanReadable;
        }

        public override string GetNodeDiscription(Node node)
        {
            var disc = base.GetNodeDiscription(node);
            if(node.Type == NodeType.File)
                disc += " (" + ToHumanReadable(node.FileInfo.Length) + ")";
            return disc;
        }

        private string ToHumanReadable(double length) 
        {
            if (!_humanReadable)
                return length + " byte";
            if (length == 0)
                return "empty";
            if (length < 1024)
                return length + " B";
            length /= 1024;
            if (length < 1024)
                return length.ToString("N2") + " KB";
            length /= 1024;
            if (length < 1024)
                return length.ToString("N2") + " MB";
            length /= 1024;
            return length.ToString("N2") + " GB";
        }

    }

    public class SizeDecoratorWrapper : IDecoratorWrapper
    {
        public TreeView Wrap(TreeView treeView, string param)
        {
            var humanize = param.Split()[0] == "-h" || param == "--human-readable";
            return new SizeTreeViewDecorator(treeView, humanize);
         }
    }
}

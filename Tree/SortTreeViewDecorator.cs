using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    [CommandLineParameter("sort", typeof(SortDecoratorWrapper), true, "--sort")]
    public class SortTreeViewDecorator : TreeViewDecoratorBase
    {
        private SortType _sortType;
        public SortTreeViewDecorator(TreeView treeView, SortType sortType) : base(treeView) 
        {
            _sortType = sortType;
        }

        public override IEnumerable<Node> GetNodeChilds(Node node)
        {
            IEnumerable<Node> childs;
            if (_sortType == SortType.Size)
            {
                var direct = base.GetNodeChilds(node).Where(_ => _.Type == NodeType.Directory);
                childs = direct.Concat(base.GetNodeChilds(node).Where(_ => _.Type == NodeType.File).OrderBy(_ => _.FileInfo?.Length));
            } 
            else if (_sortType == SortType.CreateTime) 
            {
                childs = base.GetNodeChilds(node).OrderBy(_ => _.FileInfo.CreationTime);
            }
            else if (_sortType == SortType.ChangeTime)
            {
                childs = base.GetNodeChilds(node).OrderBy(_ => _.FileInfo.LastWriteTime);
            }
            else
            {
                childs = base.GetNodeChilds(node).OrderBy(_ => _.FileInfo.Name);
            }

            return childs;
        }

    }

    public class SortDecoratorWrapper : IDecoratorWrapper
    {
        public TreeView Wrap(TreeView treeView, string param)
        {
            return new SortTreeViewDecorator(treeView, GetSortType(param.Split()[1]));
        }

        SortType GetSortType(string sort) 
        {
            if (sort == "size")
            {
                return SortType.Size;
            }
            else if (sort == "create")
            {
                return SortType.CreateTime;
            }
            else if (sort == "change")
            {
                return SortType.ChangeTime;
            }
            else if (sort == "abc")
            {
                return SortType.Abc;
            }
            throw new Exception("Invalid argument: sort " + sort);
        }
    }
}

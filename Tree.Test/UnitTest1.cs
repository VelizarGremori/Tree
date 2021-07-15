using NUnit.Framework;
using System;
using System.Reflection;

namespace Tree.Test
{
    public class Tests
    {
        public static readonly string Path = "D:\\Projects\\Tree\\Tree.Test\\Data";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleTest()
        {
            var expected = @"
|――――Data
|    |――――folder
|    |――――folder1
|    |    |――――test.txt
|    |――――folder2
|    |    |――――folder3
|    |    |――――test3.txt
|    |    |――――test4.txt
|    |――――test.txt
|    |――――test1.txt
|    |――――test2.txt";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(Array.Empty<string>());
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);

        }

        [Test]
        public void DepthTest()
        {
            var expected = @"
|――――Data
|    |――――folder
|    |――――folder1
|    |――――folder2
|    |――――test.txt
|    |――――test1.txt
|    |――――test2.txt";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new MaxDepthTreeViewDecorator(treeView, 1);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "-d", "1" });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }

        [Test]
        public void SizeTest()
        {
            var expected = @"
|――――Data
|    |――――folder
|    |――――folder1
|    |    |――――test.txt (0 byte)
|    |――――folder2
|    |    |――――folder3
|    |    |――――test3.txt (15 byte)
|    |    |――――test4.txt (64 byte)
|    |――――test.txt (0 byte)
|    |――――test1.txt (3341 byte)
|    |――――test2.txt (18304272 byte)";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new SizeTreeViewDecorator(treeView);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "--size" });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }

        [Test]
        public void HumanizedSizeTest()
        {
            var expected = @"
|――――Data
|    |――――folder
|    |――――folder1
|    |    |――――test.txt (empty)
|    |――――folder2
|    |    |――――folder3
|    |    |――――test3.txt (15 B)
|    |    |――――test4.txt (64 B)
|    |――――test.txt (empty)
|    |――――test1.txt (3,26 KB)
|    |――――test2.txt (17,46 MB)";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new SizeTreeViewDecorator(treeView, true);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "-h" });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }

        [Test]
        public void SizeSortTest()
        {
            var expected = @"
|――――Data
|    |――――folder
|    |――――folder1
|    |    |――――test.txt
|    |――――folder2
|    |    |――――folder3
|    |    |――――test3.txt
|    |    |――――test4.txt
|    |――――test.txt
|    |――――test1.txt
|    |――――test2.txt";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new SortTreeViewDecorator(treeView, SortType.Size);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "--sort", "size", });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }

        [Test]
        public void CreateSortTest()
        {
            var expected = @"
|――――Data
|    |――――test.txt
|    |――――test1.txt
|    |――――test2.txt
|    |――――folder
|    |――――folder1
|    |    |――――test.txt
|    |――――folder2
|    |    |――――test3.txt
|    |    |――――test4.txt
|    |    |――――folder3";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new SortTreeViewDecorator(treeView, SortType.CreateTime);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "--sort", "create", });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }

        [Test]
        public void ChangeSortTest()
        {
            var expected = @"
|――――Data
|    |――――test1.txt
|    |――――test2.txt
|    |――――folder
|    |――――test.txt
|    |――――folder1
|    |    |――――test.txt
|    |――――folder2
|    |    |――――folder3
|    |    |――――test4.txt
|    |    |――――test3.txt";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new SortTreeViewDecorator(treeView, SortType.ChangeTime);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "--sort", "change", });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }

            [Test]
        public void AbcSortTest()
        {
            var expected = @"
|――――Data
|    |――――folder
|    |――――folder1
|    |    |――――test.txt
|    |――――folder2
|    |    |――――folder3
|    |    |――――test3.txt
|    |    |――――test4.txt
|    |――――test.txt
|    |――――test1.txt
|    |――――test2.txt";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new SortTreeViewDecorator(treeView, SortType.Abc);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "--sort", "abc", });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }

        [Test]
        public void SizeSortReverseTest()
        {
            var expected = @"
|――――Data
|    |――――test2.txt
|    |――――test1.txt
|    |――――test.txt
|    |――――folder2
|    |    |――――test4.txt
|    |    |――――test3.txt
|    |    |――――folder3
|    |――――folder1
|    |    |――――test.txt
|    |――――folder";

            var root = new Node(Path, NodeType.Directory, 0);
            var treeView = new TreeView();
            treeView = new SortTreeViewDecorator(treeView, SortType.Size);
            treeView = new SortDirectionTreeViewDecorator(treeView);
            var treeString = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString);

            treeView = CommandLineParser.Parse(new string[] { "--sort", "size", "-r" });
            var treeString2 = treeView.GetTreeView(root);
            Assert.AreEqual(expected, treeString2);
        }
    }
}
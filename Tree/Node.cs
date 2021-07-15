using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class Node
    {
        public string Path { get; }

        public int Depth { get; }

        public NodeType Type { get; }

        private FileInfo _fileInfo;
        public FileInfo FileInfo 
        {
            get 
            {
                if (_fileInfo == null)
                    _fileInfo = new FileInfo(Path);
                return _fileInfo;
            }
        }


        public Node(string path, NodeType type, int depth)
        {
            Path = path;
            Type = type;
            Depth = depth;
        }

        public IEnumerable<Node> Childs 
        {
            get
            {
                var directoryPaths = Directory.GetDirectories(Path);
                var filePaths = Directory.GetFiles(Path);
                var nextDepth = Depth + 1;

                foreach (var childPach in directoryPaths)
                    yield return new Node(childPach, NodeType.Directory, nextDepth);

                foreach (var childPath in filePaths)
                    yield return new Node(childPath, NodeType.File, nextDepth);
            }
        }
    }
}

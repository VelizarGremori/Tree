using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class CommandLineParser
    {
        static Dictionary<string, CommandLineParameterAttribute> _commands = new Dictionary<string, CommandLineParameterAttribute>();

        static CommandLineParser() 
        {
            foreach (var type in Assembly.GetAssembly(typeof(CommandLineParser)).GetTypes().Where(_ => _.IsSubclassOf(typeof(TreeViewDecoratorBase))))  
            {
                var param = type.GetCustomAttribute<CommandLineParameterAttribute>();
                if (param == null)
                    continue;
                foreach(var p in param.Parameters)
                    _commands.Add(p, param);
            }
        }

        public static TreeView Parse(string[] args) 
        {
            var treeView = new TreeView();
            List<string> wrapped = new List<string>(); 
            for (int i = 0; i < args.Length; i++ )
            {
                var arg = args[i];
                if (_commands.ContainsKey(arg)) 
                {
                    var wrapperType = _commands[arg].Wrapper;
                    if (wrapped.Contains(wrapperType.FullName))
                        continue;
                    var hasParam = _commands[arg].HasParameter;
                    IDecoratorWrapper wrapper = (IDecoratorWrapper)wrapperType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    treeView = wrapper.Wrap(treeView, arg + (hasParam ? (" " + args[++i]) : ""));
                    wrapped.Add(wrapperType.FullName);
                }
            }
            return treeView;
        }
        
    }
}

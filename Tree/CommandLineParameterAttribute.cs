using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class CommandLineParameterAttribute : Attribute
    {
        public string[] Parameters { get; set; }
        public string CommandName { get; set; }
        public Type Wrapper { get; set; }

        public bool HasParameter { get; set; }

        public CommandLineParameterAttribute(string commandName, Type wrapper, bool hasParameter, params string[] parameters)
        {
            CommandName = commandName;
            Wrapper = wrapper;
            HasParameter = hasParameter;
            Parameters = parameters;
        }

    }
}

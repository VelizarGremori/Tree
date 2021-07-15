using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    /// <summary>
    /// Фабрика для содания декораторов 
    /// Реализация интерфейса должна содержать конструктор по умолчанию. Используется в рефлексии
    /// </summary>
    public interface IDecoratorWrapper
    {
        TreeView Wrap(TreeView treeView, string param);
    }
}

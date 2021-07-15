using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any(_ => _ == "-?" || _ == "--help"))
                PrintHelp();

            ITreeView treeView = null;
            try
            {
                treeView = CommandLineParser.Parse(args);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }
            var path = "D:\\Projects\\Tree\\Tree.Test\\Data";
            var node = new Node(path, NodeType.Directory, 0);

            Console.WriteLine(treeView.GetTreeView(node));
        }

        static void PrintHelp() 
        {
            Console.WriteLine("Консольная утилита для вывода содержимого текущего и вложенных каталогов в виде дерева");
            Console.WriteLine("Справка по использованию(--help или -?)");
            Console.WriteLine("Показывать размер объектов(-s или--size), в том числе удобном для восприятия виде(-h или --human-readable)");
            Console.WriteLine("Глубину вложенности (-d или --depth). Параметр: целое число - глубина вложенности");
            Console.WriteLine("Сортировка(--sort). Параметр: Типа сортировки: размер - size, дата создания - create, дата изменения - change, алфавитный - abc");
            Console.WriteLine("Обратный порядок сортировки (-r или --reverse)");
        }
    }
}

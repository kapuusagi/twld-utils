using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVUtils.JsonData;

/// <summary>
/// MVUtilsの動作を確認するためのツール。
/// </summary>
namespace MVUtils_tool
{
    class Program
    {
        /// <summary>
        /// プログラムのエントリポイント
        /// </summary>
        /// <param name="args">引数</param>
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                DumpDataProc(args[0]);
            }
        }

        /// <summary>
        /// データをダンプする。
        /// </summary>
        /// <param name="path"></param>
        private static void DumpDataProc(string path)
        {
            try
            {
                DataReader reader = new DataReader();
                object obj = reader.Read(path);
                Dumper.Dump(obj, Console.Out);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }


    }
}

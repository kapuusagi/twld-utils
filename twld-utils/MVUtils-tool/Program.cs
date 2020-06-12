using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVUtils;
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
        /// <param name="path">ファイルパス</param>
        private static void DumpDataProc(string path)
        {
            try
            {
                string fileName = System.IO.Path.GetFileName(path);
                if (fileName.Equals("Actors.json"))
                {
                    DumpActorsJson(path);
                }
                else if (fileName.Equals("Items.json"))
                {
                    DumpItemsJson(path);
                }
                else if (path.EndsWith(".json"))
                {
                    DumpGenericJsonData(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// アクターのデータファイルを解析してダンプする。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        private static void DumpActorsJson(string path)
        {
            List<DataActor> actors = DataActorListParser.Read(path);

            Console.WriteLine("[");
            for (int i = 0; i < actors.Count; i++) 
            {
                DataActor actor = actors[i];
                if (actor == null)
                {
                    Console.Write("null");
                }
                else
                {
                    Console.Write(actor.ToString());
                }
                Console.WriteLine(((i + 1) < actors.Count) ? "," : "");
            }
            Console.WriteLine("]");
        }

        private static void DumpItemsJson(string path)
        {
            List<DataItem> items = DataItemListParser.Read(path);
            Console.WriteLine("[");
            for (int i = 0; i < items.Count; i++)
            {
                DataItem item = items[i];
                if (item == null)
                {
                    Console.Write("null");
                }
                else
                {
                    Console.Write(item.ToString());
                }
                Console.WriteLine(((i + 1) < items.Count) ? "," : "");
            }
            Console.WriteLine("]");
        }

        /// <summary>
        /// 汎用Json記述データをダンプする。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        private static void DumpGenericJsonData(string path)
        {
            DataReader reader = new DataReader();
            object obj = reader.Read(path);
            Dumper.Dump(obj, Console.Out);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVUtils_tool
{
    internal static class Dumper
    {
        /// <summary>
        /// ダンプする。
        /// </summary>
        /// <param name="obj">出力するオブジェクト</param>
        /// <param name="output">出力先</param>
        public static void Dump(object obj, System.IO.Stream output)
        {
            System.IO.TextWriter writer = new System.IO.StreamWriter(output);
            Dump(obj, writer);
        }

        /// <summary>
        /// ダンプする。
        /// </summary>
        /// <param name="obj">ダンプするオブジェクト</param>
        /// <param name="writer">出力先</param>
        public static void Dump(object obj, System.IO.TextWriter writer)
        {
            MVUtils.JsonData.IndentWriter mw = new MVUtils.JsonData.IndentWriter(writer);
            DumpObject(mw, null, obj);
        }

        /// <summary>
        /// オブジェクトをダンプする。
        /// </summary>
        /// <param name="writer">出力先</param>
        /// <param name="paramName">パラメータ名</param>
        /// <param name="obj">オブジェクト</param>
        private static void DumpObject(MVUtils.JsonData.IndentWriter writer, string paramName, object obj)
        {
            if (obj == null)
            {
                if (string.IsNullOrEmpty(paramName))
                {
                    writer.WriteLine(null);
                }
                else
                {
                    writer.WriteLine($"{paramName} : null");
                }
            }
            else if (obj is List<object> array)
            {
                DumpArray(writer, paramName, array);
            }
            else if (obj is Dictionary<string,object> dictionary)
            {
                DumpDictionary(writer, paramName, dictionary);
            }
            else 
            {
                if (string.IsNullOrEmpty(paramName))
                {
                    writer.WriteLine(obj.ToString());
                }
                else
                {
                    writer.WriteLine($"{paramName} : {obj}");

                }
            }

        }

        /// <summary>
        /// 配列をダンプする。
        /// </summary>
        /// <param name="writer">出力先</param>
        /// <param name="array">配列</param>
        private static void DumpArray(MVUtils.JsonData.IndentWriter writer, string paramName, List<object> array)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                writer.WriteLine("[");
            }
            else
            {
                writer.WriteLine($"{paramName} : [");
            }
            writer.Indent += 2;
            for (int i = 0; i < array.Count; i++)
            {
                object obj = array[i];
                if (obj == null)
                {
                    writer.WriteLine("null,");
                }
                else if ((obj is List<object>) || (obj is Dictionary<string, object>))
                {
                    DumpObject(writer, null, obj);
                }
                else
                {
                    writer.WriteLine($"{obj},");
                }
            }
            writer.Indent -= 2;
            writer.WriteLine("]");
        }

        /// <summary>
        /// ディクショナリをダンプする。
        /// </summary>
        /// <param name="writer">出力先</param>
        /// <param name="dictionary">ディクショナリ</param>
        private static void DumpDictionary(MVUtils.JsonData.IndentWriter writer, string paramName, Dictionary<string, object> dictionary)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                writer.WriteLine("{");
            }
            else
            {
                writer.WriteLine($"{paramName} : {{");
            }
            writer.Indent += 2;
            foreach (var pair in dictionary)
            {
                string key = pair.Key;
                object obj = pair.Value;
                if (obj == null)
                {
                    writer.WriteLine($"{key}:null,");
                }
                else if ((obj is List<object>) || (obj is Dictionary<string, object>))
                {
                    DumpObject(writer, key, obj);
                }
                else
                {
                    writer.WriteLine($"{key}:{obj},");
                }
            }
            writer.Indent -= 2;
            writer.WriteLine("}");
        }
    }
}

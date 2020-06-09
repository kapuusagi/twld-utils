using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEditor
{
    internal class ProfileReader
    {
        public ProfileReader()
        {
        }

        public List<ActorProfileData> Read(string path)
        {
            List<ActorProfileData> list = new List<ActorProfileData>();
            var dir = System.IO.Path.GetDirectoryName(path);
            var actorsPath = System.IO.Path.Combine(dir, "Actors.json");

            if (System.IO.File.Exists(actorsPath))
            {
                // アクター一覧からデータを構築する。

            }


            //using (System.IO.StreamReader reader = new System.IO.StreamReader(new System.IO.FileStream(path, System.IO.FileMode.Open)))
            //{
            //}
            return list;
        }



    }
}

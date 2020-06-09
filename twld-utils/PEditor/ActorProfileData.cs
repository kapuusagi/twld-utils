using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEditor
{
    /// <summary>
    /// アクターのプロフィールデータ
    /// </summary>
    internal class ActorProfileData
    {

        /// <summary>
        /// 新しい空のプロフィールデータを作成する
        /// </summary>
        public ActorProfileData() : this(0, "")
        {
        }
        /// <summary>
        /// 新しい空のプロフィールデータを作成する。
        /// </summary>
        public ActorProfileData(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Name { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// データ
        /// </summary>
        public List<ProfileData> Data { get; private set; } = new List<ProfileData>();
    }
}

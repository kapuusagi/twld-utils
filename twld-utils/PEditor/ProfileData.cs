using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEditor
{
    /// <summary>
    /// 1つのプロフィールデータを表すモデル。
    /// </summary>
    internal class ProfileData
    {
        /// <summary>
        /// 表示条件
        /// </summary>
        public string Condition { get; set; } = "";

        /// <summary>
        /// 表示テキスト
        /// </summary>
        public string Text { get; set; } = "";
    }
}

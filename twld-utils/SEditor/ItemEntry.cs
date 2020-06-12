using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEditor
{
    /// <summary>
    /// 店売り品データエントリ
    /// </summary>
    internal class ItemEntry
    {
        /// <summary>
        /// 販売条件
        /// </summary>
        public string Condition { get; set; } = "";

        /// <summary>
        /// ID番号
        /// </summary>
        public int Id { get; set; } = 0;
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 種類
        /// </summary>
        public int Kind { get; set; } = 0;
        /// <summary>
        /// 最大入荷数
        /// </summary>
        public int MaxCount { get; set; } = 0;
        /// <summary>
        /// 最小入荷数
        /// </summary>
        public int MinCount { get; set; } = 0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEditor
{
    /// <summary>
    /// 店データを格納するためのモデル
    /// </summary>
    internal class DataShop
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; } = 0;
        /// <summary>
        /// 店の名前
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// レベル
        /// </summary>
        public int Level { get; set; } = 1;
        /// <summary>
        /// 入荷品リストと条件
        /// </summary>
        public List<ItemEntry> ItemList { get; private set; } = new List<ItemEntry>();
    }
}

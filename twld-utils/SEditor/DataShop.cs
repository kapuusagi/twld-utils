﻿using System;
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
        /// 購入価格レート
        /// </summary>
        public double BuyingPriceRate { get; set; } = 1.0;
        /// <summary>
        /// 販売価格レート
        /// </summary>
        public double SellingPriceRate { get; set; } = 0.5;

        /// <summary>
        /// 入荷品リストと条件
        /// </summary>
        public List<ItemEntry> ItemList { get; private set; } = new List<ItemEntry>();

        /// <summary>
        /// フィールドを設定する。
        /// </summary>
        /// <param name="paramName">パラメータ名</param>
        /// <param name="value">値</param>
        public void SetValue(string paramName, object value)
        {
            switch (paramName)
            {
                case "id":
                    Id = (int)((double)(value));
                    break;
                case "name":
                    Name = (string)(value);
                    break;
                case "level":
                    Level = (int)((double)(value));
                    break;
                case "itemList":
                    ItemList.Clear();
                    ItemList.AddRange((List<ItemEntry>)(value));
                    break;
                case "buyingPriceRate":
                    BuyingPriceRate = (double)(value);
                    break;
                case "sellingPriceRate":
                    SellingPriceRate = (double)(value);
                    break;

            }
        }

        /// <summary>
        /// このオブジェクトの文字列表現を得る。
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            MVUtils.JsonData.JObjectBuilder job = new MVUtils.JsonData.JObjectBuilder();
            job.Append("id", Id);
            job.Append("name", Name);
            job.Append("level", Level);
            job.Append("itemList", ItemList);
            job.Append("buyingPriceRate", BuyingPriceRate);
            job.Append("sellingPriceRate", SellingPriceRate);
            return job.ToString();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MVUtils
{
    public class DataItem 
    {
        public int Id { get; set; } = 0;
        public int AnimationId { get; set; } = 0;
        public Boolean Consumable { get; set; } = false;
        public DamageEffect Damage { get; private set; } = new DamageEffect();
        public string Description { get; set; } = string.Empty;
        public List<Effect> Effects { get; private set; } = new List<Effect>();
        public int HitType { get; set; } = 0;
        public int IconIndex { get; set; } = 0;
        public int ItemTypeId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int Occasion { get; set; } = 0;
        public int Price { get; set; } = 0;
        public int Repeats { get; set; } = 0;
        public int Scope { get; set; } = 0;
        public int Speed { get; set; } = 0;
        public int SuccessRate { get; set; } = 0;
        public int TpGain { get; set; } = 0;

        /// <summary>
        /// 値をセットする
        /// </summary>
        /// <param name="paramName">フィールド名</param>
        /// <param name="value">値</param>
        public void SetValue(string paramName, object value)
        {
            switch (paramName)
            {
                case "id":
                    Id = (int)((double)(value));
                    break;
                case "animationId":
                    AnimationId = (int)((double)(value));
                    break;
                case "consumable":
                    Consumable = (bool)(value);
                    break;
                case "damage":
                    Damage = (DamageEffect)(value);
                    break;
                case "description":
                    Description = (string)(value);
                    break;
                case "effects":
                    Effects.Clear();
                    Effects.AddRange((List<Effect>)(value));
                    break;
                case "hitType":
                    HitType = (int)((double)(value));
                    break;
                case "iconIndex":
                    IconIndex = (int)((double)(value));
                    break;
                case "itypeId":
                    ItemTypeId = (int)((double)(value));
                    break;
                case "name":
                    Name = (string)(value);
                    break;
                case "note":
                    Note = (string)(value);
                    break;
                case "occasion":
                    Occasion = (int)((double)(value));
                    break;
                case "price":
                    Price = (int)((double)(value));
                    break;
                case "repeats":
                    Repeats = (int)((double)(value));
                    break;
                case "scope":
                    Scope = (int)((double)(value));
                    break;
                case "speed":
                    Speed = (int)((double)(value));
                    break;
                case "successRate":
                    SuccessRate = (int)((double)(value));
                    break;
                case "tpGain":
                    TpGain = (int)((double)(value));
                    break;
            }
        }

        /// <summary>
        /// このオブジェクトの文字列表現を得る。
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            JsonData.JObjectBuilder job = new JsonData.JObjectBuilder();
            job.Append("id", Id);
            job.Append("animationId", AnimationId);
            job.Append("consumable", Consumable);
            job.Append("damage", Damage);
            job.Append("description", Description);
            job.Append("effects", Effects);
            job.Append("hitType", HitType);
            job.Append("iconIndex", IconIndex);
            job.Append("itypeId", ItemTypeId);
            job.Append("name", Name);
            job.Append("note", Note);
            job.Append("occasion", Occasion);
            job.Append("price", Price);
            job.Append("repeats", Repeats);
            job.Append("scope", Scope);
            job.Append("speed", Speed);
            job.Append("successRate", SuccessRate);
            job.Append("tpGain", TpGain);

            return job.ToString();
        }
    }
}

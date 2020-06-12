using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MVUtils
{
    public class DataWeapon : IItem
    {
        public int Id { get; set; } = 0;
        public int AnimationId { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public int EquipTypeId { get; set; } = 1;
        public List<Trait> Traits { get; private set; } = new List<Trait>();
        public int IconIndex { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int[] Params { get; private set; } = new int[8];
        public int Price { get; set; } = 0;
        public int WeaponTypeId { get; set; } = 0;

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
                case "description":
                    Description = (string)(value);
                    break;
                case "etypeId":
                    EquipTypeId = (int)((double)(value));
                    break;
                case "traits":
                    Traits.Clear();
                    Traits.AddRange((List<Trait>)(value));
                    break;
                case "iconIndex":
                    IconIndex = (int)((double)(value));
                    break;
                case "name":
                    Name = (string)(value);
                    break;
                case "note":
                    Note = (string)(value);
                    break;
                case "params":
                    {
                        List<int> array = (List<int>)(value);
                        for (int i = 0; (i < Params.Length) && (i < array.Count); i++)
                        {
                            Params[i] = array[i];
                        }
                    }
                    break;
                case "price":
                    Price = (int)((double)(value));
                    break;
                case "wtypeId":
                    WeaponTypeId = (int)((double)(value));
                    break;
            }
        }

        /// <summary>
        /// このオブジェクトの文字列表現を得る。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            JsonData.JObjectBuilder job = new JsonData.JObjectBuilder();
            job.Append("id", Id);
            job.Append("animationId", AnimationId);
            job.Append("description", Description);
            job.Append("etypeId", EquipTypeId);
            job.Append("traits", Traits);
            job.Append("iconIndex", IconIndex);
            job.Append("name", Name);
            job.Append("note", Note);
            job.Append("params", Params);
            job.Append("price", Price);
            job.Append("wtypeId", WeaponTypeId);

            return job.ToString();
        }
    }
}

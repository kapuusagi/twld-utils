using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QEditor
{
    internal class DataQuest
    {
        public int Id { get; set; } = 0;
        public int GuildRank { set; get; } = 0;
        public int GuildExp { set; get; } = 0;
        public string EntrustCondition { get; set; } = string.Empty;
        public int QuestType { get; set; } = 0;
        public int[] Achieve { get; private set; } = new int[2];
        public int RewardGold { get; set; } = 0;
        public List<RewardItem> RewardItems { get; private set; } = new List<RewardItem>();
        public string Name { get; set; } = string.Empty;
        public string AchieveMessage { get; set; } = string.Empty;
        public string RewardsMessage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public void SetValue(string paramName, object value)
        {
            switch (paramName)
            {
                case "id":
                    Id = (int)((double)(value));
                    break;
                case "guildRank":
                    GuildRank = (int)((double)(value));
                    break;
                case "guildExp":
                    GuildExp = (int)((double)(value));
                    break;
                case "entrustCondition":
                    EntrustCondition = (string)(value);
                    break;
                case "qtype":
                    QuestType = (int)((double)(value));
                    break;
                case "achieve":
                    {
                        List<int> array = (List<int>)(value);
                        for (int i = 0; i < 2; i++)
                        {
                            if (i < array.Count)
                            {
                                Achieve[i] = array[i];
                            }
                            else
                            {
                                Achieve[i] = 0;
                            }
                        }
                    }
                    break;
                case "rewardGold":
                    RewardGold = (int)((double)(value));
                    break;
                case "rewardItems":
                    RewardItems.Clear();
                    RewardItems.AddRange((List<RewardItem>)(value));
                    break;
                case "name":
                    Name = (string)(value);
                    break;
                case "achieveMsg":
                    AchieveMessage = (string)(value);
                    break;
                case "rewardMsg":
                    RewardsMessage = (string)(value);
                    break;
                case "description":
                    Description = (string)(value);
                    break;
            }
        }

        public override string ToString()
        {
            MVUtils.JsonData.JObjectBuilder job = new MVUtils.JsonData.JObjectBuilder();
            job.Append("id", Id);
            job.Append("guildRank", GuildRank);
            job.Append("guildExp", GuildExp);
            job.Append("entrustCondition", EntrustCondition);
            job.Append("qtype", QuestType);
            job.Append("achieve", Achieve);
            job.Append("rewardGold", RewardGold);
            job.Append("rewardItems", RewardItems);
            job.Append("name", Name);
            job.Append("achieveMsg", AchieveMessage);
            job.Append("rewardMsg", RewardsMessage);
            job.Append("description", Description);
            return job.ToString();
        }

    }
}

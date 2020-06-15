using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QEditor
{
    internal class RewardItem
    {
        public int Kind { get; set; } = 0;
        public int DataId { get; set; } = 0;
        public int Value { get; set; } = 0;

        public void SetValue(string paramName, object value)
        {
            switch (paramName)
            {
                case "kind":
                    Kind = (int)((double)(value));
                    break;
                case "dataId":
                    DataId = (int)((double)(value));
                    break;
                case "value":
                    Value = (int)((double)(value));
                    break;
            }
        }

        public override string ToString()
        {
            MVUtils.JsonData.JObjectBuilder job = new MVUtils.JsonData.JObjectBuilder();
            job.Append("kind", Kind);
            job.Append("dataId", DataId);
            job.Append("value", Value);

            return job.ToString();
        }
    }
}

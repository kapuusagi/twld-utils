using System;
using System.Collections.Generic;
using System.Text;

namespace MVUtils
{
    /// <summary>
    /// アイテムインタフェース
    /// </summary>
    public interface IItem : INamedObject
    {
        string Description { get; }
    }
}

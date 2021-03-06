﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MVUtils
{
    /// <summary>
    /// アイテムインタフェース
    /// </summary>
    public interface IItem : INamedObject
    {
        /// <summary>
        /// 説明
        /// </summary>
        string Description { get; }
    }
}

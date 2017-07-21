using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// EmailConfigInfo.cs :EmailConfigInfo 
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 20010-06-18


/// <summary>
/// 基本设置描述类, 加[Serializable]标记为可序列化
/// </summary>
[Serializable]
public class mapConfigInfo
{
    private decimal _Radius;

    /// <summary>
    ///搜索半径
    /// </summary>
    public decimal Radius
    {
        set { _Radius = value; }
        get { return _Radius; }
    }

}


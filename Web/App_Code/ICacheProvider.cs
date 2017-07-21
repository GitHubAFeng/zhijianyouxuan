using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Hangjing.Model;

// ICacheProvider.cs:缓存提供者接口  可以实现这个接口的方法来提供不同的缓存方式 留待以后扩展
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-07

public interface ICacheProvider
{

    DataTable GetTogoLocalInfo();
}


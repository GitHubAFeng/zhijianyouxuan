using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

/// <summary>
///SentGsm 的摘要说明
/// </summary>
public class SentGsm
{
    public SentGsm()
    {
        
    }

    /// <summary>
    ///  1表示成功
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="cont"></param>
    /// <returns>1表示成功，0表示失败</returns>
    public static int sendMsg(string mobile, string content)
    {
        Hangjing.WebCommon.SendMsg.send(mobile, content);
        return 1;

    }
}

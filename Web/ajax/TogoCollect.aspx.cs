using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

// TogoCollect.aspx:收藏ajax执行.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-01

public partial class Ajax_TogoCollect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Clear();

        //fuc 是执行类型 
        //         类型代码          参数
        //addfood 加入购物车        foodid togoid togoname
        //delfood 删除收藏          foodid 
        //addtogo 删除购物车餐品    togoid
        //deltogo 获取购物车        togoid
        //clearfood  清空餐品收藏  
        //cleartogo  情况餐馆收藏  

        string type = Request["fuc"];
        int userid = -1;

        if (UserHelp.IsLogin())
        {
            userid = Convert.ToInt32(UserHelp.GetUser().DataID.ToString());
        }
        else
        {
            Response.Write("-1");
            Response.End();
        }
        try
        {
            int ret = -1;

            switch (type)
            {
                case "addfood":
                    ret = AddFood(Convert.ToInt32(Request["foodid"]),Convert.ToInt32(Request["togoid"]),userid ,Request["bid"]);

                    Response.Write(ret.ToString());
                    break;
                case "delfood":
                    if (DelFood(Convert.ToInt32(Request["foodid"]), userid))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                case "addtogo":
                    ret = AddTogo(Convert.ToInt32(Request["togoid"]), userid  ,Request["bid"]);

                    Response.Write(ret.ToString());

                    break;
                case "deltogo":
                    if (DelTogo(Convert.ToInt32(Request["togoid"]),userid))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                case "clearfood":

                    if (ClearFood(userid))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                case "cleartogo":
                    if (ClearTogo(userid))
                    {
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write("0");
                    }
                    break;
                default:
                    Response.Write("-1");
                    break;
            }
        }
        catch
        {
            Response.Write("");
        }

        Response.End();
    }

    ETogoCollect bll = new ETogoCollect();

    /// <summary>
    /// 餐品收藏
    /// </summary>
    /// <returns>2 已经存在 1 添加成功 0 失败</returns>
    private int AddFood(int foodid,int togoid,int userid , string bid)
    {
        //判断是否已经存在
        if (bll.GetFoodCount("userid="+userid+" and foodid="+foodid+" and togoid="+togoid+"") > 0)
        {
            return 2;
        }

        ETogoFoodCollectInfo info = new ETogoFoodCollectInfo();
        info.ctime = DateTime.Now;
        info.togoid = togoid;
        info.userid = userid;
        info.foodid = foodid;
        info.inve1 = 0;
        info.inve2 = bid;

        if (bll.AddFood(info) > 0)
        {
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// 餐馆收藏
    /// </summary>
    /// <returns>2 已经存在 1 添加成功 0 失败</returns>
    private int AddTogo(int togoid, int userid , string bid)
    {
        //判断是否已经存在
        if (bll.GetTogoCount("userid=" + userid + " and togoid=" + togoid + "") > 0)
        {
            return 2;
        }

        ETogoCollectInfo info = new ETogoCollectInfo();
        info.ctime = DateTime.Now;
        info.togoid = togoid;
        info.userid = userid;
        info.inve1 = bid == "" ? 0 : Convert.ToInt32(bid);
        info.inve2 = bid;

        if (bll.AddTogo(info) > 0)
        {
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// 删除收藏的餐品
    /// </summary>
    /// <returns></returns>
    private bool DelFood(int foodid, int userid)
    {

        return false;
    }

    /// <summary>
    /// 删除收藏的餐馆
    /// </summary>
    /// <returns></returns>
    private bool DelTogo(int togoid, int userid)
    {

        return false;
    }

    /// <summary>
    /// 清空收藏的餐品
    /// </summary>
    /// <returns></returns>
    private bool ClearFood(int userid)
    {
        if (bll.ClearFood(userid) > 0)
        {
            return true;
        }
        return false;    
    }

    /// <summary>
    /// 清空收藏的餐馆
    /// </summary>
    /// <returns></returns>
    private bool ClearTogo(int userid)
    {
        if (bll.ClearTogo(userid) > 0)
        {
            return true;
        }
        return false;    
    }
}

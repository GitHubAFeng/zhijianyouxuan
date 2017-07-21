#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-6-6 20:58:56.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// 提交订单的地方绑定优惠券
/// </summary>
public partial class Ajax_bindmycardbindmyshopcard : System.Web.UI.Page
{
    ShopCard dal = new ShopCard();
    protected void Page_Load(object sender, EventArgs e)
    {
        string pwd = WebUtility.InputText(Request["pwd"]);
        Response.Clear();
        string ret = "{'code':'0','msg':''}";
        ECustomerInfo user = UserHelp.GetUser();
        if (user == null)
        {
            ret = "{'code':'-1','msg':'您还没有登录，请登录'}";
        }
        else
        {
            //根据编号和密码绑定
            string sql = "ckey = '" + pwd + "'";
            IList<ShopCardInfo> cardlist = dal.GetList(1, 1, sql, "cid", 1);
            if (cardlist.Count == 0)
            {
                ret = "{'code':'-2','msg':'优惠券券号有有误，请重新输入'}";
            }
            else
            {
                //判断有没有激活,有没有绑定
                if (cardlist[0].Inve2 == "0")
                {
                    ret = "{'code':'-3','msg':'此优惠券未激活，不能绑定'}";
                }
                else
                {
                    if (cardlist[0].State == 1)
                    {
                        ret = "{'code':'-4','msg':'此优惠券已经绑定'}";
                    }
                    else
                    {
                        sql = "update ShopCard set usergettime = '" + DateTime.Now.ToString() + "' , state =1 , userid = " + user.DataID + ",username = '" + user.Name + "' where CID in (" + cardlist[0].CID + ")";
                        if (WebUtility.excutesql(sql) > 0)
                        {
                            foreach (var item in cardlist)
                            {
 
                                item.isbuy = 0;
                            }
                            ret = "{'code':'200','msg':'绑定成功。','isbuy':'" + cardlist[0].isbuy + "','ckey':'" + cardlist[0].ckey + "','cardnum':'" + cardlist[0].cardnum + "','cid':'" + cardlist[0].CID + "','point':'" + cardlist[0].Point + "','usemsg':'" + cardlist[0].ReveVar1 + "','ReveInt':'" + cardlist[0].ReveInt1 + "','title':'" + cardlist[0].title + "','moneyline':'" + cardlist[0].moneyline + "'}";//正确

                        }
                        else
                        {
                            ret = "{'code':'500','msg':'服务器繁忙，请稍后再试。'}";

                        }
                    }
                }
            }

        }

        Response.Write(ret);
        Response.End();
    }
}

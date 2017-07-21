<%@ WebHandler Language="C#" Class="MyAjaxHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Reflection;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

using Newtonsoft.Json;
using Hangjing.Weixin;

/// <summary>
/// 处理一般ajax请求:注意，所有方法是不能有参数的
/// </summary>
public class MyAjaxHandler : HandlerBase, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 使用分享红包
    /// </summary>
    public string useSharepackage()
    {
        int shopid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["id"]));

        WebUtility.excutesql("UPDATE userpacket SET reveint = reveint-1 WHERE id = " + shopid);

        return "1";
    }


    /// <summary>
    /// 用户注册
    /// </summary>
    public string userreg()
    {
        ECustomer userBLL = new ECustomer();
        string tbphone = WebUtility.InputText(base._httpReuqest["tbmobile"]);
        string tbGsmCode = WebUtility.InputText(base._httpReuqest["tbGsmCode"]);//验证码
        string password = WebUtility.GetMd5(base._httpReuqest["TBpassword"]);


        #region 验证判断
        string state = SectionProxyData.GetSetValue(39);
        if (state == "1")//表示要验证    
        {
            string cookie_gsmcode = WebUtility.FixgetCookie("gsmcode");
            if (cookie_gsmcode == null || cookie_gsmcode != tbGsmCode.Trim())
            {
                rs.msg = "短信验证码错误";
                return PrintJson();
            }
        }

        string sql = "[Name] = '" + tbphone + "'";
        int count = userBLL.GetCount(sql);
        if (count > 0)
        {
            rs.msg = "该手机号已注册，请重新输入";
            return PrintJson();
        }

        sql = "Tell = '" + tbphone + "'";
        count = userBLL.GetCount(sql);
        if (count > 0)
        {
            rs.msg = "该手机号已注册，请重新输入";
            return PrintJson();
        }
        #endregion

        ECustomerInfo model = new ECustomerInfo();
        int point = Convert.ToInt32(SectionProxyData.GetSetValue(19));
        model.EMAIL = "";
        model.Name = tbphone;
        model.TrueName = "";
        model.Tell = tbphone;
        model.Password = password;
        model.RegTime = DateTime.Now;
        model.Point = point;
        model.IsActivate = -1;
        model.ActivateCode = WebUtility.RandStr(200);
        model.GroupID = 0;
        model.WebSite = OrderSource.weixin + "";
        model.RID = "0";
        model.Usermoney = 0;
        model.PhoneActivate = 0;
        model.Sex = "";
        model.MSN = "";

        if (userBLL.Add(model) > 0)
        {
            ECustomerInfo customer = userBLL.GetModelByNameAPassword(tbphone, model.Password);

            EPointRecordInfo info = new EPointRecordInfo();
            info.UserID = customer.DataID;
            info.Point = point;
            info.Event = "新注册用户,获得积分" + point + "个";
            info.Time = DateTime.Now;
            EPointRecord bll = new EPointRecord();
            bll.Add(info);

            rs.state = 1;

            int haspackage = Hangjing.WebCommon.sendPacket.packetHandle(customer);
            if (haspackage > 0)
            {
                rs.msg = "恭喜您注册成功，还获得了若干分享红包，分享红包自己也获取哦";
            }
            else
            {
                rs.msg = "恭喜您注册在成功，优惠多多，快去订餐吧";
            }
        }
        else
        {
            rs.msg = "服务器繁忙，请稍后再试";
        }

        return PrintJson();

    }

    /// <summary>
    /// 用户绑定优惠券
    /// </summary>
    public string userAddShopCard()
    {
        int userid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["hfuserid"]));
        string tbcardckey = WebUtility.InputText(base._httpReuqest["tbcardckey"]);
        string tbvcode = WebUtility.InputText(base._httpReuqest["tbvcode"]);

        apiResultInfo rs = new apiResultInfo();
        rs.msg = "";

        if (Context.Session["CheckCode"] == null || Context.Session["CheckCode"].ToString().ToLower() != tbvcode.ToLower())
        {
            rs.state = 0;
            rs.msg = "验证码错误，请重新输入";
        }
        else
        {
            rs = new ShopCard().userAddCard(tbcardckey, userid);
        }

        return JsonConvert.SerializeObject(rs); ;
    }



    /// <summary>
    /// 自动提现到微信余额（发红包）
    /// </summary>
    public string autodrawcash()
    {
        int userid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["hfuserid"]));
        decimal money = Convert.ToDecimal(WebUtility.InputText(base._httpReuqest["tbmoney1"]));

        apiResultInfo rs = new apiResultInfo();
        rs.msg = "";

        ECustomerInfo user = new ECustomer().GetModel(userid);
        if (user != null && user.Usermoney >= money)
        {
            int dataid = new ECustomer().addAccountMoney(userid, "余额提现到微信账户（发红包方式）", -money, 9);

            // todo 根据配置，调用接口发红包

            decimal reviewdoor = Convert.ToDecimal(SectionProxyData.GetSetValue(79).Trim());

            string isopen = SectionProxyData.GetSetValue(78).Trim();//自动审核

            Hangjing.AppLog.AppLog.Info("reviewdoor=" + reviewdoor + "\r\nisopen=" + isopen);



            if (money <= reviewdoor && isopen == "1")
            {
                int paystate = 0;
                WechatPay2User wp = new WechatPay2User(Context);
                bool success = wp.Pay(user.PayPWDQuestion, dataid, Math.Abs(money));
                if (!success)
                {
                    rs.state = 0;
                    rs.msg = "提交成功，但发微信红包失败，请联系管理员";

                    paystate = 20;

                }
                else
                {
                    rs.state = 1;
                    rs.msg = "提交成功，请注意查收微信红包";
                    paystate = 10;
                }

                WebUtility.excutesql("UPDATE dbo.UserAddMoneyLog SET State = " + paystate + " WHERE DataId = " + dataid);

            }
            else
            {
                rs.state = 1;
                rs.msg = "提交成功，请等待管理员审核";
            }
        }
        else
        {
            rs.state = 0;
            rs.msg = "系统繁忙，请稍后再试";

        }

        return JsonConvert.SerializeObject(rs); ;
    }


    /// <summary>
    /// 余额提现到银行
    /// </summary>
    public string drawcash2back()
    {
        int userid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["hfuserid"]));
        decimal money = Convert.ToDecimal(WebUtility.InputText(base._httpReuqest["tbmoney1"]));
        string bankname = WebUtility.InputText(base._httpReuqest["bankname"]);
        string revevar1 = WebUtility.InputText(base._httpReuqest["revevar1"]);
        string bankusername = WebUtility.InputText(base._httpReuqest["bankusername"]);

        userCashAcountInfo accountinfo = new userCashAcount().GetModelByUser(userid);
        accountinfo.bankname = bankname;
        accountinfo.revevar1 = revevar1;
        accountinfo.bankusername = bankusername;
        new userCashAcount().Add(accountinfo);

        apiResultInfo rs = new apiResultInfo();

        ECustomerInfo user = new ECustomer().GetModel(userid);
        if (user != null && user.Usermoney >= money)
        {
            new ECustomer().addAccountMoney(userid, "余额提现到银行(" + bankname + "," + revevar1 + "," + bankusername + ")", -money, 8);
            rs.state = 1;
        }
        else
        {
            rs.state = 0;
        }

        return JsonConvert.SerializeObject(rs); ;
    }


    /// <summary>
    /// 余额提现到微信
    /// </summary>
    public string drawcash2Wechat()
    {
        int userid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["hfuserid"]));
        decimal money = Convert.ToDecimal(WebUtility.InputText(base._httpReuqest["tbmoney2"]));
        string opuser = WebUtility.InputText(base._httpReuqest["opuser"]);

        userCashAcountInfo accountinfo = new userCashAcount().GetModelByUser(userid);
        accountinfo.opuser = opuser;
        new userCashAcount().Add(accountinfo);

        apiResultInfo rs = new apiResultInfo();

        ECustomerInfo user = new ECustomer().GetModel(userid);
        if (user != null && user.Usermoney >= money)
        {
            new ECustomer().addAccountMoney(userid, "余额提现到微信(" + opuser + ")", -money, 8);
            rs.state = 1;
        }
        else
        {
            rs.state = 0;
        }

        return JsonConvert.SerializeObject(rs); ;
    }

    /// <summary>
    /// 提现到支付宝
    /// </summary>
    public string drawcash2alipay()
    {
        int userid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["hfuserid"]));
        decimal money = Convert.ToDecimal(WebUtility.InputText(base._httpReuqest["tbmoney3"]));
        string aliaccount = WebUtility.InputText(base._httpReuqest["aliaccount"]);
        string aliname = WebUtility.InputText(base._httpReuqest["aliname"]);

        userCashAcountInfo accountinfo = new userCashAcount().GetModelByUser(userid);
        accountinfo.aliname = aliname;
        accountinfo.aliaccount = aliaccount;
        new userCashAcount().Add(accountinfo);

        apiResultInfo rs = new apiResultInfo();

        ECustomerInfo user = new ECustomer().GetModel(userid);
        if (user != null && user.Usermoney >= money)
        {
            new ECustomer().addAccountMoney(userid, "余额提现到支付宝(" + aliname + "," + aliaccount + ")", -money, 8);
            rs.state = 1;
        }
        else
        {
            rs.state = 0;
        }

        return JsonConvert.SerializeObject(rs); ;
    }




    /// <summary>
    /// 佣金提现到余额
    /// </summary>
    public string CashAdvance()
    {
        int userid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["hfuserid"]));
        decimal money = Convert.ToDecimal(WebUtility.InputText(base._httpReuqest["tbmoney"]));

        apiResultInfo rs = new UserDistributionLog().Distribute2Account(userid, money);

        return JsonConvert.SerializeObject(rs); ;
    }

    /// <summary>
    /// 获取更多商家
    /// </summary>
    public string loadshop()
    {
        string res = "";


        return res;
    }
    public string changeadd()
    {
        EAddress dal = new EAddress();
        int aid = Convert.ToInt32(base._httpReuqest["aid"]);
        int uid = Convert.ToInt32(base._httpReuqest["uid"]);
        int rs = dal.UpdateDefaut(aid, uid);
        return rs.ToString();
    }
    public int addtogo()
    {
        EAddress dal = new EAddress();
        ETogoCollect bll = new ETogoCollect();
        int togoid = Convert.ToInt32(base._httpReuqest["togoid"]);
        int userid = Convert.ToInt32(base._httpReuqest["userid"]);
        int bid = Convert.ToInt32(base._httpReuqest["bid"]);
        int msg = bll.TogoCollect(userid, togoid, bid);
        return msg;
    }
    public int delete()
    {
        EAddress dal = new EAddress();
        int aid = Convert.ToInt32(base._httpReuqest["aid"]);
        int msg = dal.DelEAdd(aid);
        return msg;
    }
}
<%@ WebHandler Language="C#" Class="uc" %>

using System;
using System.Web;
using DS.Web.UCenter;
using DS.Web.UCenter.Api;

using Hangjing.Common;
using DS.Web.UCenter.Client;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

public class uc : UcApiBase
{
    public override ApiReturn DeleteUser(System.Collections.Generic.IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public override ApiReturn RenameUser(int uid, string oldUserName, string newUserName)
    {
        throw new NotImplementedException();
    }

    public override UcTagReturns GetTag(string tagName)
    {
        throw new NotImplementedException();
    }

    public override ApiReturn SynLogin(int uid , string uname)
    {
        //获取用户名，密码
        //此处判断有没有此用户，通过用户名.
        //如果有就登录，如果没有用注册用户.
        ECustomerInfo usermodel = new ECustomer().GetModel(uname);
        if (usermodel != null)//有用户登录.
        {
            UserHelp.SetLogin(usermodel);
        }
        else
        {
            //添加用户
            ECustomerInfo model = new ECustomerInfo();
            int point = Convert.ToInt32(SectionProxyData.GetSetValue(19));
            model.EMAIL = "";
            model.Password = WebUtility.GetMd5("jjj123456");//默认值，到时登录时用到
            model.RegTime = DateTime.Now;
            model.Point = point;
            model.Name = uname;
            model.IsActivate = 1;
            model.ActivateCode = WebUtility.RandStr(200);
            model.GroupID = 0;
            model.WebSite = "";
            model.RID = "";
            model.TrueName = "";
            model.MSN = "";
            model.Sex = "";
            model.Tell = "";
            model.Phone = "";
            model.QQ = "";
            model.Usermoney = 0;
            model.UC_ID = uid;
            model.PayPassword = "";
            model.DataID = new ECustomer().Add(model);
            UserHelp.SetLogin(model);
        }
        return ApiReturn.Success;
    }

    public override ApiReturn SynLogout()
    {
        UserHelp.Logout();
        return ApiReturn.Success;
    }

    public override ApiReturn UpdatePw(string userName, string passWord)
    {
        HJlog.toLog("修改密码:" + userName + "," + passWord);
        return ApiReturn.Success;
    }

    public override ApiReturn UpdateBadWords(UcBadWords badWords)
    {
        throw new NotImplementedException();
    }

    public override ApiReturn UpdateHosts(UcHosts hosts)
    {
        throw new NotImplementedException();
    }

    public override ApiReturn UpdateApps(UcApps apps)
    {
        throw new NotImplementedException();
    }

    public override ApiReturn UpdateClient(UcClientSetting client)
    {
        throw new NotImplementedException();
    }

    public override ApiReturn UpdateCredit(int uid, int credit, int amount)
    {
        throw new NotImplementedException();
    }

    public override UcCreditSettingReturns GetCreditSettings()
    {
        throw new NotImplementedException();
    }

    public override ApiReturn GetCredit(int uid, int credit)
    {
        throw new NotImplementedException();
    }

    public override ApiReturn UpdateCreditSettings(UcCreditSettings creditSettings)
    {
        throw new NotImplementedException();
    }
}
#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-3-21 9:26:14.
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

public partial class qy_54tss_AreaAdmin_manage_citydetail : System.Web.UI.Page
{
    City dal = new City();
    protected CityInfo info
    {
        set
        {
            hfcid.Value = value.cid.ToString();
            tbcname.Text = value.cname;
            tbReveInt.Text = value.ReveInt.ToString();
            this.hfadddate.Value = value.adddate.ToString();
            tbReveVar.Text = value.ReveVar;
            ImgUrl1.Value = value.url;
            ImgUrl.Src = WebUtility.ShowPic(value.url);
        }
        get
        {
            CityInfo model = new CityInfo();
            model.cid = Convert.ToInt32(hfcid.Value);
            model.cname = WebUtility.InputText(tbcname.Text);
            model.site = "";
            model.url = ImgUrl1.Value;
            model.adddate = Convert.ToDateTime(hfadddate.Value);
            model.ReveInt = Convert.ToInt32(tbReveInt.Text);
            model.ReveVar = tbReveVar.Text.ToUpper();
            model.Lat = hidLat.Value;
            model.Lng = hidLng.Value;
            return model;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!IsPostBack)
        {
            this.pageType.Text = "添加城市站";
            int id = HjNetHelper.GetQueryInt("id", 0);
            if (id > 0)
            {
                this.pageType.Text = "编辑城市站";
                CityInfo model = dal.GetModel(id);
                info = model;
                hidLat.Value = model.Lat;
                hidLng.Value = model.Lng;

            }
            else
            {
                hfadddate.Value = DateTime.Now.ToString();
                hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
                hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;
            }
        }
    }


    /// <summary>
    ///保存信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        CityInfo model = info;
        int i = 0;
        if (Request["id"] != null)
        {
            //判断权限
            int _rs = WebUtility.AreaAdmin_checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);init();");
                return;
            }
            i = dal.Update(model);
        }
        else
        {
            //判断权限
            int _rs = WebUtility.AreaAdmin_checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);init();");
                return;
            }
            i = dal.Add(model);
            ClearControl(Page);
        }
        //////////////保存结束/////////////////////
        if (i > 0)
        {
            AlertScript.RegScript(this.Page, this, "tipsWindown('提示信息','text:保存成功!','250','150','true','2000','true','text');");
    
        }
        else
        {
            AlertScript.RegScript(this.Page, this, "tipsWindown('提示信息','text:保存失败!','250','150','true','2000','true','text');");
        }
        ImgUrl.Src = WebUtility.ShowPic(model.url);
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/city");
    }

    /// <summary>
    /// 清空控件
    /// </summary>
    /// <param name="page"></param>
    public void ClearControl(System.Web.UI.Control page)
    {
        int count = page.Controls.Count;
        for (int i = 0; i < count; i++)
        {
            foreach (System.Web.UI.Control con in page.Controls[i].Controls)
            {
                if (con.HasControls())
                {
                    ClearControl(con);
                }
                else
                {
                    if (con is TextBox)
                    {
                        (con as TextBox).Text = string.Empty;
                    }
                }
            }
        }
    }

}
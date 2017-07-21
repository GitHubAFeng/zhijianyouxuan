using System;
using System.Collections;
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

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class AreaAdmin_Shop_addFoodAttributes : System.Web.UI.Page
{
    FoodAttributes dal = new FoodAttributes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("fid")))
            {
                int fid = HjNetHelper.GetQueryInt("fid", 0);
                Foodinfo fooddal = new Foodinfo();
                FoodinfoInfo foodinfo = fooddal.GetModel(fid);
                if (foodinfo != null)
                {
                    this.Lbfoodname.Text = foodinfo.FoodName.ToString();
                }
                hffid.Value = fid.ToString();
                hftid.Value = HjNetHelper.GetQueryString("tid");
            }
            GetFoodAttributes();
        }
    }

    protected void GetFoodAttributes()
    {
        if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("id")))
        {
            int dataid = HjNetHelper.GetQueryInt("id", 0);
            {
                FoodAttributesInfo info = dal.GetModel(dataid);
                hfdid.Value = dataid.ToString();
                if (info != null)
                {
                    this.hidDataId.Value = HjNetHelper.GetQueryInt("id", 0).ToString();
                    this.tbtitle.Text = info.Title.ToString();
                    this.DropSelectType.SelectedValue = info.SelectType.ToString();
                    this.Lbfoodname.Text = info.Name.ToString();
                    this.tbAttributes.Text = info.Attributes.ToString();
                    WebUtility.SelectValue(ddlInve1, info.Inve1.ToString());

                    IList<ShopDataInfo> attritems = new List<ShopDataInfo>();
                    string[] items = info.Attributes.Split('#');
                    foreach (var item in items)
                    {
                        string [] msg = item.Split('?');
                        attritems.Add(new ShopDataInfo() { classname = msg[0],Pic = msg[1] });
                    }

                    WebUtility.BindRepeater(rptfood, attritems);

                }
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        int fid = Convert.ToInt32(hffid.Value);
        int tid = Convert.ToInt32(hftid.Value);
        int did = Convert.ToInt32(hfdid.Value);
        FoodAttributesInfo info = new FoodAttributesInfo();
        info.DataId = did;
        info.Title = this.tbtitle.Text;
        info.SelectType = Convert.ToInt32(this.DropSelectType.SelectedValue);
        info.FoodtId = fid;
        info.Inve1 = Convert.ToInt32(ddlInve1.SelectedValue);
        info.Inve2 = "";


        IList<ShopDataInfo> attritems = JsonConvert.DeserializeObject<IList<ShopDataInfo>>(hidStyle.Value);
        //去冰?2#少冰?3</font>"。每个子项以"#"分隔,名称与价格以"?"分隔

        string Attributes = "";
        foreach (var item in attritems)
        {
            Attributes += item.classname + "?"+item.Pic+"#";
        }
        //去除最后一个# 
        if (Attributes.Length > 1)
        {
            Attributes = WebUtility.dellast(Attributes, "#");
        }
        info.Attributes = Attributes;

        string url = "FoodDetail.aspx?id=" + fid + "&tid=" + tid;
        if (did != 0)
        {
            if (dal.Update(info) > 0)
            {
                new Foodinfo().UpdateFooodAttr(fid);
                AlertScript.RegScript(this, "alert('编辑成功');top.location.href  = top.location.href;");
            }
            else
            {
                AlertScript.RegScript(this, "alert('编辑失败');top.location.href  = top.location.href;");
            }
        }
        else
        {
            if (dal.Add(info) > 0)
            {
                new Foodinfo().UpdateFooodAttr(fid);
                AlertScript.RegScript(this, "alert('编辑成功');top.location.href  = top.location.href;");
            }
            else
            {
                AlertScript.RegScript(this, "alert('添加失败');top.location.href  = top.location.href;");
            }
        }

    }
}

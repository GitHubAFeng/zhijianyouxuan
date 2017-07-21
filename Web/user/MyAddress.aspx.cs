using System;
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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class UserHome_MyAddress : PageBase
{
    EAddress dal = new EAddress();
    City cdal = new City();
    private string SqlWhere
    {
        get
        {
            return ViewState["where"] == null ? "" : ViewState["where"].ToString();
        }
        set
        {
            ViewState["where"] = (object)value;
        }
    }

    public string ImagePath = WebUtility.GetMasterPicturePath();
    public EAddressInfo defautaddress = new EAddressInfo();
    public string username = "";
    public string useraddress = "";
    public string userphone = "";
    public string buildingname = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            string cityid = WebUtility.get_userCityid();

            hfcityname.Value = WebUtility.get_userCityName();

            //从提交订单的地方来添加地址
            if (HjNetHelper.GetQueryInt("tid", 0) > 0)
            {
                string mylat = WebUtility.FixgetCookie("mylat");
                string mylng = WebUtility.FixgetCookie("mylng");
                string searchk = HttpUtility.UrlDecode(WebUtility.FixgetCookie("search_key"));
                if (searchk != null && searchk != "")
                {
                    tbAddress.Value = searchk;
                }
                if (mylat != null)
                {
                    hidLat.Value = mylat;
                    hidLng.Value = mylng;
                }
            }

            if (Request.QueryString["id"] != null)
            {
                GetData();
            }
            else
            {
                hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
                hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;
                hidlocalflag.Value = "1";
            }
            SqlWhere = "userid=" + UserHelp.GetUser().DataID;
            GetFirstAddress();
            BindData();
        }
    }

    /// <summary>
    /// 修改地址
    /// </summary>
    public void GetData()
    {
        EAddressInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id", 0));
        if (model != null)
        {
            this.tbReceiveName.Text = model.Receiver;
            this.tbAddress.Value = model.Address;
            this.tbCellPhone.Text = model.Mobilephone;
            this.hdState.Value = Request.QueryString["id"].ToString();
            this.hfbid.Value = model.BuildingID.ToString();
            tbphone.Value = model.Phone;
            hidLat.Value = model.Lat;
            hidLng.Value = model.Lng;
        }
    }

    public void BindData()
    {
        IList<EAddressInfo> list = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "pri", 1);
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        IList<EAddressInfo> listtemp = list;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Pri == 1)
            {

            }
        }
        this.rtpAddressList.DataSource = list;
        this.rtpAddressList.DataBind();
        if (list.Count == 0)
        {
            this.my_address.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }
    }

    protected void rtpAddressList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int dataid = Convert.ToInt32(e.CommandArgument);
        int userid = UserHelp.GetUser().DataID;
        switch (e.CommandName)
        {
            case "del":
                if (dal.DelEAddress(Convert.ToInt32(e.CommandArgument)) == 1)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text'); initialize();");
                    GetFirstAddress();
                    BindData();

                }
                else
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text'); initialize();");
                }
                break;
            case "setdefaut":
                if (dal.UpdateDefaut(dataid, userid) > 0)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:设置成功!','250','150','true','1000','true','text'); initialize();");
                    BindData();
                    GetFirstAddress();
                    this.AspNetPager1.CurrentPageIndex = 1;
                }
                else
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:设置成功!','250','150','true','1000','true','text'); initialize();");
                }
                break;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        EBuilding Bdal = new EBuilding();
        string sql = "";

        EAddressInfo info = new EAddressInfo();
        info.Receiver = WebUtility.InputText(this.tbReceiveName.Text);
        info.Address = WebUtility.InputText(this.tbAddress.Value);
        info.BuildingID = Convert.ToInt32(hfbid.Value);
        info.Phone = WebUtility.InputText(tbphone.Value);
        info.Mobilephone = WebUtility.InputText(this.tbCellPhone.Text);
        info.UserID = UserHelp.GetUser().DataID;
        info.AddTime = DateTime.Now;
        info.Pri = 0;
        info.Lat = hidLat.Value;
        info.Lng = hidLng.Value;
        info.DataID = HjNetHelper.GetQueryInt("id", 0);
        if (info.DataID== 0)
        {
            int addrid = dal.Add(info);
            if (addrid > 0)
            {
                WebUtility.FixsetCookie("useraddr_id", addrid.ToString(), 30);
                if (HjNetHelper.GetQueryInt("tid", 0) > 0)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "alert('添加成功,点击确定继续点餐');gourl('../OrderDetail.aspx?id=" + HjNetHelper.GetQueryInt("tid", 0) + "')");
                    return;
                }
                BindData();
                GetFirstAddress();
                ClearText();

                if (Request["ReturnUrl"] != null)
                {
                    Response.Redirect(Request["ReturnUrl"]);
                }


                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:添加成功!','250','150','true','1000','true','text');initialize();");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:添加失败!','250','150','true','1000','true','text');initialize();");
            }
        }
        else
        {
            if (dal.Update(info) == 1)
            {
                BindData();
                GetFirstAddress();
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:修改成功!','250','150','true','1000','true','text');initialize();");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:修改失败!','250','150','true','1000','true','text');initialize();");
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
        GetFirstAddress();
    }


    void ClearText()
    {
        this.tbReceiveName.Text = "";
        this.tbAddress.Value = "";
        //this.tbaCode.Text = "";
        this.tbCellPhone.Text = "";
        this.tbphone.Value = "";
    }

    public string handlerdefaut(object x)
    {
        if (Convert.ToInt32(x) == 1)
        {
            return "默认地址";
        }
        else
        {
            if (Convert.ToInt32(x) == 0)
            {
                return "设为默认";
            }
            return "";
        }
    }

    /// <summary>
    /// 获得默认地址
    /// </summary>
    public void GetFirstAddress()
    {
        IList<EAddressInfo> list = dal.GetList(1, 1, SqlWhere, "pri", 1);
        if (list.Count != 0)
        {
            EAddressInfo model = list[0];
            this.my_address.Style.Add(HtmlTextWriterStyle.Display, "block");
            username = model.Receiver;
            useraddress = model.Address+model.Phone;
            userphone = model.Mobilephone;
            buildingname = model.BuildingName;
        }
        else
        {
            this.my_address.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

}

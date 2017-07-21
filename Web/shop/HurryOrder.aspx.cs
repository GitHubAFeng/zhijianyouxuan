using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

using System.Text.RegularExpressions;

public partial class shop_HurryOrder : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }
    /// <summary>
    /// 本页面参数集合.每个参数以下字母开头(参数个数是定)
    /// </summary>
    Dictionary<string, string> mypara
    {
        get { object o = ViewState["mypara"]; return (Dictionary<string, string>)o; }
        set { ViewState["mypara"] = value; }
    }

    /// <summary>
    /// 列表排序,默认排序
    /// </summary>
    private string sortword
    {
        get
        {
            object o = ViewState["sortword"];
            string temp = "OrderDateTime";//下单时间

            return (o == null) ? temp : o.ToString();
        }
        set
        {
            ViewState["sortword"] = value;
        }
    }

    /// <summary>
    /// 商家编号
    /// </summary>
    private int tid
    {
        get
        {
            object o = ViewState["tid"];
            return (o == null) ? 0 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["tid"] = value;
        }
    }

    /// <summary>
    /// 列表排序,1表示降序，0表示升序
    /// </summary>
    private int sortflag
    {
        get
        {
            object o = ViewState["sortflag"];
            return Convert.ToInt32(o);
        }
        set
        {
            ViewState["sortflag"] = value;
        }
    }
    hurryorder hal = new hurryorder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            sortflag = 1;
            string para = Request["para1"];
            PointsInfo shop = new Points().GetModel(UserHelp.GetUser_Togo().Unid);
            tid = shop.Unid;
            SqlWhere = " TogoId = " + tid + " and (paymode = 4 OR paystate = 1) ";

            SqlWhere += " and Custorder.orderid in(SELECT oid FROM hurryorder) ";

            if (para == null || para.IndexOf("oe") == -1)
            {
                string url = "HurryOrder.aspx?c=1";
                url += "&para1=oe1|";
                Response.Redirect(url);
            }

            setCurrentSort(para);

            BindData();
        }
    }
    Custorder dal = new Custorder();
    protected void BindData()
    {
        IList<CustorderInfo> list = new List<CustorderInfo>();
        list = dal.GetListFix(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, sortword, sortflag);
        foreach (var item in list)
        {
            item.Foodlist = new Foodlist().GetList(1000, 1, "orderid=" + item.orderid, "unid", 1);
        }
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptOrderList.DataSource = list;
        this.rptOrderList.DataBind();
        NoRecord();
    }
    private void NoRecord()
    {
        if (rptOrderList.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        AlertScript.RegScript(this.Page, UpdatePanel1, "showfoodinfo();");
        BindData();
    }

    public void setCurrentSort(string para)
    {
        mypara = new Dictionary<string, string>();
        string para1 = para;
        //保存每个条件的标签如:s,sp,l,a...
        string mykey = "";
        //保存每个条件的标值如:223,2,11_22
        string myvalue = "";
        //数字和'_'
        Regex rxnumber = new Regex(@"[0-9_]+");
        //字母
        Regex rxLetter = new Regex(@"[a-zA-Z]+");
        if (para1 != null && para1 != "")
        {
            para1 = WebUtility.dellast(para1, "|");
            string[] paraitems = para1.Split('|');
            foreach (var item in paraitems)
            {
                mykey = rxnumber.Replace(item, "");
                myvalue = rxLetter.Replace(item, "");
                if (myvalue != "")
                {
                    mypara.Add(mykey, myvalue);
                }

            }
        }

        //当前已经选择的条件的列表,前台用repeater绑定
        IList<string> selectedcondition = new List<string>();
        foreach (var item in mypara)
        {
            switch (item.Key)
            {
                case "oe"://催单或者退单状态
                    {
                        switch (item.Value)
                        {
                            case "1"://待处理
                                SqlWhere += " and Custorder.orderid in(SELECT oid FROM hurryorder where ReveInt=0) ";
                                break;
                            case "2"://已处理
                                SqlWhere += " and Custorder.orderid in(SELECT oid FROM hurryorder where ReveInt=1) ";
                                break;
                        }
                    }
                    break;
            }
        }
    }
    /// <summary>
    /// 各个排序Url
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    protected string getSortUrl(string tag, object id)
    {
        string url = "HurryOrder.aspx?c=1";

        url += "&para1=";

        foreach (var item in mypara)
        {
            if (item.Key != tag)
            {
                url += item.Key + item.Value + "|";
            }
        }
        if (id.ToString() != "")
        {
            url += tag + id + "|";
        }

        return url;
    }
    /// <summary>
    /// timeer event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        string sql = "   OrderStatus in (1,2,7) and   TogoId = " + tid + " and IsShopSet =0  ";
        int count = dal.GetCount(sql);
        if (count != 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(" + count + ");");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(0);");
        }
        BindData();

    }

}
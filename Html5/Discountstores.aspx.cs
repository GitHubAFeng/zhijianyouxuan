using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Html5
{
    public partial class Discountstores : System.Web.UI.Page
    {

        /// <summary>
        /// 列表排序,默认排序
        /// </summary>
        private string sortword
        {
            get
            {
                object o = ViewState["sortword"];
                string temp = "SortNum";//距离

                return (o == null) ? temp : o.ToString();
            }
            set
            {
                ViewState["sortword"] = value;
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
        Foodinfo dalfood = new Foodinfo();
        Points daltogo = new Points();
        string sqlWhere = " IsDelete=0 ";
        string strDistance = " distance <= Inve1 ";
        protected void Page_Load(object sender, EventArgs e)
        {

            string lat = WebUtility.FixgetCookie("mylat");
            string lng = WebUtility.FixgetCookie("mylng");
            sqlWhere = "  1=1 and IsDelete = 0 and Star = 1 and Status=1 and InUse = 'Y'";//已经审核的

            WebUtility.BindRepeater(rptppt, SectionProxyData.GetPPTList().Where(a => a.Reve2 == "2").ToList());
            int id = HjNetHelper.GetQueryInt("id", 0); //分类的id
            if (id > 0)
            {
                sqlWhere += " and category like  '%{" + id + "}%'";
            }

            sortflag = 1;

            setCurrentSort();
            WebUtility.BindRepeater(rpttogosortlist, SectionProxyData.GetSortList());
            if (lat == "" || lng == "" || lat == null || lng == null)
            {
                lat = "0";
                lng = "0";
                return;
            }

            //查询本页所有商家配送段记录，再根据此及距离，得到配送费，起送价
            
            IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1000, 1, sqlWhere, sortword, sortflag, lat, lng, strDistance);
            if (shoplist.Count > 0)
            {
                string shopids = "";
                foreach (var item in shoplist)
                {
                    shopids += item.Unid + ",";
                }
                shopids = WebUtility.dellast(shopids);
                string sql = " 1=1 and InUse = 'y' and special='1'";
                if (shopids != "")
                {
                    sql += " and  FPMaster in (" + shopids + ")";
                }
                IList<FoodinfoInfo> foodlist = dalfood.GetList(1000, 1, sql, "OrderNum", 1);
                WebUtility.BindRepeater(rptfoodlist, foodlist);
                back.HRef = "Togolist.aspx?islocal=1&address=" + WebUtility.FixgetCookie("address") + "&lat=" + lat + "&lng=" + lng;
            }
            
        }
        public void setCurrentSort()
        {
            mypara = new Dictionary<string, string>();
            string para1 = Request["para1"];
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
                para1 = WebUtility.dellast(para1, "/");
                string[] paraitems = para1.Split('/');
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

            foreach (var item in mypara)
            {
                switch (item.Key)
                {
                    case "od"://排序
                        {
                            switch (item.Value)
                            {
                                case "0"://默认
                                    sortword = "SortNum";
                                    sortflag = 1;
                                    break;
                                case "1"://销量
                                    sortword = "pop";
                                    sortflag = 1;
                                    break;
                                case "2"://最新
                                    sortword = "InTime";
                                    sortflag = 1;
                                    break;
                                case "3"://距离
                                    sortword = "Distance";
                                    sortflag = 0;
                                    break;
                            }
                            hfod.Value = item.Key + item.Value;
                        }
                        break;
                    case "s"://分类
                        {
                            sqlWhere += " and  charindex('{" + item.Value + "}',category)>=1 ";
                            //hfcursortid.Value = item.Value;
                        }
                        break;
                    case "a"://起送价
                        {
                            //hfcursendmoney.Value = item.Value;
                            sqlWhere += " and EXISTS (SELECT tid FROM dbo.shopdelivery WHERE tid = Points.Unid AND minmoney <= " + item.Value + ") ";
                        }
                        break;
                    case "l"://配送类型
                        {
                            sqlWhere += " and sentorg= 0 ";
                            //hfcursaleid.Value = item.Value;
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
            string url = "Discountstores.aspx?c=1";
            url += "&para1=";

            foreach (var item in mypara)
            {
                if (item.Key != tag)
                {
                    url += item.Key + item.Value + "/";
                }
            }
            if (id.ToString() != "")
            {
                url += tag + id + "/";
            }

            return url;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;


namespace Html5
{
    public partial class moneydetail : System.Web.UI.Page
    {
        msgpacketInfo model = new msgpacketInfo();
        msgpacket dal = new msgpacket();
        userpacketInfo info = new userpacketInfo();
        userpacket ual = new userpacket();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();

            string urlkey = Request["urlkey"];
            if (urlkey != "")
            {
                string sqlwhere = "1=1 and pid='" + urlkey + "'";

                if (user != null)
                {

                    sqlwhere += "  and ReveVar='" + user.Tell + "'";

                    int ret = checkphone(user.Tell, urlkey);
                    if (ret == 0)
                    {
                        hidtel.Value = user.Tell;
                    }

                }
                else
                {
                    string tel = HjNetHelper.GetQueryString("hasLogUpload");
                    if (tel != "")
                    {
                        hidtel.Value = tel;
                        sqlwhere += "  and ReveVar='" + tel + "'";
                    }


                }
                IList<msgpacketInfo> list = dal.GetList(1, 1, sqlwhere, "id", 1);
                if (list.Count > 0)
                {
                    WebUtility.BindRepeater(rptpromotion, list);
                }
                else
                {
                    hidtel.Value = "";
                }
                
            }

            //获取post信息，提交订单
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                add_Click();
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {
            string urlkey = Request["urlkey"];
            if (urlkey != "")
            {
                string tel = WebUtility.InputText(Request.Form["tbphone"]);
                int msg=  checkphone(tel, urlkey);
                Response.Redirect("moneydetail.aspx?urlkey=" + urlkey + "&hasLogUpload=" + tel+ "&msg="+msg);
            }
            else
            {
                Response.Redirect("moneydetail.aspx?urlkey=" + urlkey + "&msg=1");//抢光了
                return;
            }
           

        }
        protected int checkphone(string tel, string urlkey)
        {
            int ret = 0;
            string sql = "1=1 and pid='" + urlkey + "' and ReveVar='" + tel + "'";
            int count = dal.GetCount(sql);
            HJlog.toLog("sql=" + sql);
            HJlog.toLog("count=" + count);
            if (count > 0)
            {

            }
            else
            {
                info = ual.GetModel(urlkey);
                
                if (info != null)
                {
                    sql = "pid='" + info.revevar + "'";
                    count = dal.GetCount(sql);
                    HJlog.toLog("count1=" + count);
                    if (count >= info.reveint)
                    {
                        ret = 1;
                    }
                    else
                    {
                        model.pid = urlkey;

                        if (info.reveint1 == 0)
                        {
                            Random Rdm = new Random();
                            model.alltotal = Rdm.Next(1, Convert.ToInt32(info.money));
                        }
                        else
                        {
                            model.alltotal = info.money;
                        }

                        model.num = 0;
                        model.eachmoney = 0;
                        model.validitytime = DateTime.Now.AddDays(info.state);
                        model.moneyline = Convert.ToInt32(info.revevar1);
                        model.cmoney = 0;
                        model.starttime = DateTime.Now;
                        model.endtime = DateTime.Now;
                        model.ReveInt = 0;
                        model.ReveInt1 = 0;
                        model.ReveVar = tel;
                        model.ReveVar1 = "";
                        model.datetime1 = DateTime.Now;
                        model.datetime2 = DateTime.Now;

                        dal.Add(model);
                    }
                }

            }
            HJlog.toLog("ret=" + ret);
            return ret;
        }

    }
}
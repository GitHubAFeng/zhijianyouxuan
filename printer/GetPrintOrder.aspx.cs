// OrderInfo.aspx.cs :获取订单，更新订单，打印机设置同步页面
// CopyRight (c) 2010 HangJing Teconology. All Rights Reserved.
// wl@ihangjing.com
// 2009-03-25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using HJPrinter;

/// <summary>
/// 订单获取，更新页面。同时如果商家通过电脑修改了商家信息，该页面会把更新后的用户信息发送到打印机上
/// </summary>
public partial class GetPrintOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string CustID = Request.QueryString["id"];//对应
        string Page = Request.QueryString["p"];
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(CustID))
        {
            if (!string.IsNullOrEmpty(Page))
            {//获取订单
                int page = int.Parse(Page);
                HJCustomer customer = HJPrinters.GetCustomerWithCustID(CustID, page, false);
                if (customer != null)
                {

                    if (page == 1)
                    {//订单编号，订单下发时间、用户、用户电话、用户地址、商品条数记录

                        if (customer.Edit)
                        {//用户信息有更新
                            sb.Append("<phead>");
                            sb.Append(customer.CustName);
                            sb.Append("</phead>");
                            sb.Append("<ptimes>");
                            sb.Append(customer.PrintTimes.ToString());
                            sb.Append("</ptimes>");
                            sb.Append("<pend>");
                            sb.Append(customer.PrintEnd);
                            sb.Append("</pend>");
                        }

                        //订单时间
                        sb.Append("<time>");
                        sb.Append(Convert.ToDateTime(customer.OrdTime).ToString("yyyy-MM-dd HH:mm:ss"));
                        sb.Append("</time>");

                        sb.Append("<orderid>");
                        sb.Append(customer.OSID);
                        sb.Append("</orderid>");
                        //订单编号                     

                    }
                    sb.Append("<OdCont>");
                    if (customer.Orderstate == 5)
                    {
                        sb.Append("\r--------------------------------\r");
                        sb.Append("\r注意！订单编号为：" + customer.OSID.ToString() + "的订单已被用户取消！\r");
                    }
                    else
                    {

                        if (page == 1)
                        {
                            ///打印机每行只能打印32个字符
                            sb.Append("\r商家名称：" + customer.TogoName.Trim());
                            sb.Append("\r客户姓名：" + customer.UserName.Trim());
                            sb.Append("\r客户电话：" + customer.UserPhone.Trim());//+ "  客户性质：" /*+ customer.kind+ "\r");
                            sb.Append("\r地址：" + customer.UserAddress.Trim() + "");
                            sb.Append("\r--------------------------------\r");
                            sb.Append("  品名          数量  单价  小计\r");
                        }

                        HJGoods goods;
                        String name;
                        int le = customer.ArrayCount;
                        int lo;

                        if (customer.OSID.Substring(0, 1) != "r")
                        {
                            for (int i = 0; i < le && i < HJPrinters.nCount; i++)
                            {//生成订单
                                goods = customer.GetGoods();
                                string s = goods.Notes.Trim() == "" ? "" : "(" + goods.Notes + ")";
                                name = goods.Name + s;
                                lo = System.Text.Encoding.GetEncoding("GB2312").GetByteCount(name);
                                //lo = name.Length;
                                while (lo < 17)
                                {
                                    name += " ";
                                    lo++;
                                }
                                if (goods.Count != 0)
                                {
                                    sb.Append(name + goods.Count.ToString() + "   " + goods.Price.ToString("0.00") + " " + goods.Discount.ToString("0.00") + "\r\n");
                                }
                                else
                                {
                                    sb.Append("\r--------------------------------\r");

                                }
                            }

                        }
                        else
                        {
                            goods = customer.GetGoods();
                            if (goods.Count != 0)
                            {
                                sb.Append(goods.Name + "      " + goods.Notes + "\n" + "  总份数：" + goods.Count.ToString() + "份" + "   总金额：" + goods.Price.ToString() + "元" + "\r\n");
                            }
                        }
                    }

                    if (customer.End)
                    {//订单结束标志

                        if (customer.Orderstate != 5)
                        {
                            //sb.Append("\r消费方式：" + customer.eattype + "");
                            //sb.Append("\r支付方式：" + WebUtility.TurnPayModel(customer.paymodel) + "");

                            //string mypaystate = "";
                            //if (customer.paystate == "1")
                            //{
                            //    mypaystate= "已付款";
                            //}
                            //else
                            //{
                            //    mypaystate = "未付款";
                            //}

                            //sb.Append("\r支付状态：" + mypaystate + "");
                            ////sb.Append("\r合计：" + customer.AllPrice.ToString("0.00") + "元");

                            sb.Append("\r--------------------------------");
                            sb.Append("\r送餐费：" + customer.sendmoney.ToString("0.00") + "元");
                            sb.Append("\r合计：" + customer.AllPrice.ToString("0.00") + "元");
                            //sb.Append("\r送达时间:" +Convert.ToDateTime(customer.sendtime).AddMinutes(Convert.ToInt32(customer.senttime)).ToString());
                            sb.Append("\r送达时间:" + Convert.ToDateTime(customer.sendtime).ToShortTimeString());
                            sb.Append("\r备注:" + customer.remark + "");


                        }

                        sb.Append("</OdCont>");
                        sb.Append("<end></end>");
                    }
                    else
                    {//分页标志
                        sb.Append("</OdCont>");
                        sb.Append("<page></page>");
                    }
                }
                else
                {
                    sb.Append("<none></none>");
                }


            }
        }
        else
        {
            sb.Append("<error></error>");
        }

        Response.Write(sb.ToString());
        Response.End();
    }
}

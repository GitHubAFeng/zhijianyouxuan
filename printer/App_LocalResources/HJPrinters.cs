// HJPrinters.cs :打印机接口
// CopyRight (c) 2010 HangJing Teconology. All Rights Reserved.
// wlf@ihangjing.com
// 2009-03-25

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hangjing.SQLServerDAL;
using HJPrinter;
using Hangjing.Model;

using System.Configuration;
using Hangjing.Common;

/// <summary>
/// 打印机商家交互接口
/// </summary>
public class HJPrinters
{
    public HJPrinters() { }

    //public static int nCount = 3;//没此传送订单中最高的记录条数，该数据有打印机的内存决定

    public static int nCount = Convert.ToInt32(ConfigurationManager.AppSettings["NCount"]);

    /// <summary>
    /// 根据打印机背部编号获取加密KEY，
    /// </summary>
    /// <param name="sn1"> 打印机序列号1</param>
    /// <returns>商家加密KEY</returns>
    public static string GetPassKeyWithSN(string togoNo)
    {
        Togo dal = new Togo();

        return dal.GetPassKeyWithSN(togoNo);
    }

    /// <summary>
    /// 根据打印机序列号获取商家信息
    /// </summary>
    /// <param name="sn2">打印机序列号</param>
    /// <returns>商家类</returns>
    public static HJCustomer GetCustID(string togoSn)
    {
        Togo dal = new Togo();
        if (togoSn == "")
        {
            return null;
        }
        HJCustomer customer = null;
        TogoPrinterInfo model = dal.GetModelByTogoSN(togoSn);
        if (model != null)
        {
            customer = new HJCustomer();
            customer.CustID = model.TogoId + "";
            customer.CustName = model.PrintTop;
            customer.PrintTimes = model.PrintPage;
            customer.PrintEnd = model.PrintFoot;
        }
        return customer;
    }

    /// <summary>
    /// 根据商家ID获取加密KEY
    /// </summary>
    /// <param name="id">商家ID</param>
    /// <returns>加密KEY</returns>
    public static string GetPasskeyWithCustID(string togoNum)
    {
        Togo dal = new Togo();
        return dal.GetPasskeyWithCustID(togoNum);
    }

    /// <summary>
    /// 根据订单编号修改订单状态为已经处理状态
    /// 如果打印机超过一定的时间短，而此时订单又是新增订单或者订单处理中，那么此时订单将更新为订单处理失败
    /// </summary>
    /// <param name="orderId">订单编号</param>
    /// <returns>修改状态成功，返回false，返回true</returns>
    public static bool SetOSIDStata(string orderId)
    {
        Custorder dal = new Custorder();

        CustorderInfo model = dal.GetModel(orderId);
        if (model == null)
        {
            return false;
        }
        int state = model.OrderStatus;
        if (state != 5)
        {
            if (dal.UpdateOrderState(orderId) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (dal.UpdataState(orderId, 6) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 打印机修改了商家信息，要同步更新到数据库。
    /// 参数主要包括商家名称、打印联数、结尾信息
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public static bool SetCustomerInfoWithCustID(HJCustomer customer)
    {
        Togo dal = new Togo();

        TogoPrinterInfo model = new TogoPrinterInfo();
        model.TogoId = Convert.ToInt32(customer.CustID);
        model.PrintTop = customer.CustName;
        model.PrintPage = customer.PrintTimes;
        model.PrintFoot = customer.PrintEnd;

        if (dal.UpdatePrintInfo(model) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 更改（商家名，联数，打印结尾是否更改）状态。
    /// </summary>
    /// <param name="togoNum"></param>
    /// <param name="isUpdate"></param>
    public static void UpdateIsUpdate(string togoNum, int isUpdate)
    {
        Togo dal = new Togo();
        dal.UpdateIsUpdate(togoNum, isUpdate);
    }

    /// <summary>
    /// 获取订单里面的餐品
    /// </summary>
    /// <param name="togoNum">商家编号</param>
    /// <param name="Page">页面</param>
    /// <param name="reChange">修改订单状态</param>
    /// <returns></returns>
    public static HJCustomer GetCustomerWithCustID(string togoid, int Page, bool reChange)
    {
        Togo dal_togo = new Togo();
        TogoPrinter daltp = new TogoPrinter();
        Custorder dal_togoorder = new Custorder();
        Foodlist dal_orderfood = new Foodlist();
        Points dal_points=new Points();

        TogoPrinterInfo tpmodel = daltp.GetModel(Convert.ToInt32(togoid));
        if (tpmodel == null)
        {
            return null;
        }
        dal_togo.UpdateState(togoid, 1, DateTime.Now);
        CustorderInfo order = dal_togoorder.GetOldOrder(togoid);
       
        HJCustomer customer = new HJCustomer();
        if (order == null)
        {
            return null;
        }
        else
        {
            //IList<CustorderInfo> drink_list = dal_togoorder.GetListFix(1, 1, "TogoId = 1 and ReveVar1 ='" + order.ReveVar1 +"'", "OrderDateTime", 1);
            //PointsInfo togoinfo = dal_points.GetModel(order.TogoId);
            int foodCount = order.FoodCount;

            if (tpmodel != null)
            {
                //customer.senttime = togoinfo.senttime;
                customer.CustID = tpmodel.TogoId + "";
                customer.CustName = tpmodel.PrintTop;
                customer.OrdTime = order.OrderDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                customer.Edit = tpmodel.IsUpdate == 1 ? true : false;
                customer.OSID = order.orderid.ToString();
                customer.PrintEnd = tpmodel.PrintFoot;
                customer.PrintTimes = tpmodel.PrintPage;
                customer.Count = order.FoodCount;
                
                customer.UserName = order.OrderRcver;
                customer.UserPhone = order.OrderComm;
                customer.UserAddress = order.AddressText;
                customer.Orderstate = order.OrderStatus;
                customer.remark = order.OrderAttach;
                customer.sendtime = order.SendTime.ToString();
                customer.sendmoney = order.SendFee;
                customer.TogoName = dal_points.GetModel(order.TogoId).Name;

                customer.AllPrice = order.OrderSums;
                //if (drink_list != null && drink_list.Count > 0)
                //{
                //    customer.AllPrice = order.OrderSums + drink_list[0].OrderSums;
                //}
                //else
                //{
                //    customer.AllPrice = order.OrderSums;
                //}
            }


            IList<FoodlistInfo> orderFoodList = dal_orderfood.GetPageList(order.Unid, Page, nCount);
            for (int i = 0; i < orderFoodList.Count; i++)
            {
                customer.AddOrd(orderFoodList[i].FoodName, orderFoodList[i].Remark, orderFoodList[i].FCounts, orderFoodList[i].FoodPrice, orderFoodList[i].FoodPrice * orderFoodList[i].FCounts);
            }


            if (Page * nCount >= foodCount)
            {
                customer.End = true;
                if (reChange)
                {
                    dal_togoorder.UpdataState(order.Unid, 2);
                }
            }

        }
        return customer;
    }

}

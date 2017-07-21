using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

using Hangjing.Model;
using Hangjing.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Hangjing.SQLServerDAL;

namespace Hangjing.WebCommon
{
    /// <summary>
    /// 红包发放
    /// </summary>
    public class sendPacket
    {
        /// <summary>
        /// 提交订单发红包
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int packetHandle(CustorderInfo model)
        {
            ECustomer bll = new ECustomer();
            ECustomerInfo infos = new ECustomerInfo();

            packetconfig abl = new packetconfig();

            packetconfigInfo anfo = new packetconfigInfo();

            userpacket pack = new userpacket();
            userpacketInfo info = new userpacketInfo();

            int ret = 0;
            infos = bll.GetModel(model.UserId);
            if (infos != null)
            {
                anfo = abl.GetModel(2);

                if (anfo.isopen == 1)
                {
                    info.pid = "1";
                    info.userid = model.UserId;
                    info.exptime = DateTime.Now;
                    info.pulltime = DateTime.Now;
                    info.pulltel = infos.Tell;
                    info.reveint = anfo.reveint1;
                    info.reveint1 = anfo.reveint2;
                    info.money = anfo.distance;
                    info.revevar1 = anfo.revevar1;
                    info.state = Convert.ToInt32(anfo.revevar2);

                    info.moneyline = 0;
                    info.revevar = Guid.NewGuid().ToString();
                    info.datetime1 = DateTime.Now;
                    info.datetime2 = DateTime.Now;
                    ret = pack.Add(info);
                }
            }
            return ret;
        }

        /// <summary>
        /// 注册发红包
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int packetHandle(ECustomerInfo model)
        {
            packetconfig abl = new packetconfig();
            packetconfigInfo anfo = new packetconfigInfo();
            userpacket pack = new userpacket();
            userpacketInfo info = new userpacketInfo();

            int ret = 0;
            anfo = abl.GetModel(1);

            if (anfo.isopen == 1)
            {
                info.pid = "0";
                info.userid = model.DataID;
                info.exptime = DateTime.Now;
                info.pulltime = DateTime.Now;
                info.pulltel = model.Tell;
                info.reveint = anfo.reveint1;
                info.reveint1 = anfo.reveint2;
                info.money = anfo.distance;
                info.revevar1 = anfo.revevar1;
                info.state = Convert.ToInt32(anfo.revevar2);

                info.moneyline = 0;
                info.revevar = Guid.NewGuid().ToString();
                info.datetime1 = DateTime.Now;
                info.datetime2 = DateTime.Now;
                ret = pack.Add(info);
            }
            return ret;
        }
    }
}

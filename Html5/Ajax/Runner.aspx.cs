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
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

namespace Html5.Ajax
{
    public partial class Runner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Clear();
            string fuc = Request["fuc"];

            switch (fuc)
            {
                case "distance":
                    string dis = CalculateDistance(Convert.ToDouble(Request["flat"]), Convert.ToDouble(Request["flng"]), Convert.ToDouble(Request["tlat"]), Convert.ToDouble(Request["tlng"])).ToString("f1");
                    Response.Write(dis);
                    break;
                case "sendfee":
                    string fee = CalculateFee(Convert.ToDouble(Request["flat"]), Convert.ToDouble(Request["flng"]), Convert.ToDouble(Request["tlat"]), Convert.ToDouble(Request["tlng"])).ToString("f1");
                    Response.Write(fee);
                    break;
            }

            Response.End();

        }

        /// <summary>
        /// 配送距离的计算
        /// </summary>
        /// <param name="flat"></param>
        /// <param name="flng"></param>
        /// <param name="tlat"></param>
        /// <param name="tlng"></param>
        /// <returns></returns>
        protected double CalculateDistance(double flat, double flng, double tlat, double tlng)
        {
            double radLat1 = rad(flat);
            double radLat2 = rad(tlat);
            double a = radLat1 - radLat2;
            double b = rad(flng) - rad(tlng);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * 6378.137;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// 配送费的计算
        /// </summary>
        /// <param name="flat"></param>
        /// <param name="flng"></param>
        /// <param name="tlat"></param>
        /// <param name="tlng"></param>
        /// <returns></returns>
        protected double CalculateFee(double flat, double flng, double tlat, double tlng)
        {
            string s = CalculateDistance(flat, flng, tlat, tlng).ToString("f1");
            double fee = Convert.ToDouble(s) * Convert.ToInt32(SectionProxyData.GetSetValue(66));
            return fee;
        }

    }
}
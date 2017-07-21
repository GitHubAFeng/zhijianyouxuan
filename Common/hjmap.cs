#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-5-6 11:28:56.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections;
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
using System.Reflection;
using System.Text;

namespace Hangjing.Common
{
    /// <summary>
    ///   判断点是否在多边形内
    /// </summary>
    public class hjmap
    {
        /// <summary>
        /// 坐标点
        /// </summary>
        public struct XPoint
        {
            public double lat, lng;
            public XPoint(double _x, double _y)
            {
                lat = _x;
                lng = _y;
            }

            /// <summary>
            /// 判断传入的点是否和当前相同，相同返回true，否则返回false
            /// </summary>
            /// <param name="source">传入的点</param>
            /// <returns></returns>
            public bool equals(XPoint source)
            {
                if (source.lat == lat && source.lng == lng)
                {
                    return true;
                }
                return false;
            }
        };

        /// <summary>
        /// 判断一个点是否在一个多边形范围内
        /// </summary>
        /// <param name="_lat">纬度</param>
        /// <param name="_lng">经度</param>
        /// <param name="_polygon">多边形范围 以这样的形式 lat1,lng1|lat2,lng2....</param>
        /// <returns></returns>
        public static bool isPointInPolygon(string _lat, string _lng, string _polygon)
        {
            XPoint point = new XPoint();
            point.lat = Convert.ToDouble(_lat);
            point.lng = Convert.ToDouble(_lng);

            string Polygon = _polygon;
            string[] PolygonArray = Polygon.Split('|');
            XPoint[] points = new XPoint[PolygonArray.Length];

            for (int i = 0; i < PolygonArray.Length; i++)
            {
                string[] point_value = PolygonArray[i].Split(',');
                points[i] = new XPoint((Convert.ToDouble(point_value[0])), (Convert.ToDouble(point_value[1])));
            }

            bool re = false;

            var t = points;
            var h = t.Length;
            var n = true;
            var j = 0;
            var g = 2e-10;
            var s = t[0];
            var q = new XPoint();
            var o = point;
            var e = o;
            for (var f = 1; f <= h; ++f)
            {
                if (e.equals(s))
                {
                    return n;
                }
                q = t[f % h];
                if (e.lat < Math.Min(s.lat, q.lat) || e.lat > Math.Max(s.lat, q.lat))
                {
                    s = q;
                    continue;
                }
                if (e.lat > Math.Min(s.lat, q.lat) && e.lat < Math.Max(s.lat, q.lat))
                {
                    if (e.lng <= Math.Max(s.lng, q.lng))
                    {
                        if (s.lat == q.lat && e.lng >= Math.Min(s.lng, q.lng))
                        {
                            return n;
                        }
                        if (s.lng == q.lng)
                        {
                            if (s.lng == e.lng)
                            {
                                return n;
                            }
                            else
                            {
                                ++j;
                            }
                        }
                        else
                        {
                            var r = (e.lat - s.lat) * (q.lng - s.lng) / (q.lat - s.lat) + s.lng;
                            if (Math.Abs(e.lng - r) < g)
                            {
                                return n;
                            }
                            if (e.lng < r)
                            {
                                ++j;
                            }
                        }
                    }
                }
                else
                {
                    if (e.lat == q.lat && e.lng <= q.lng)
                    {
                        var m = t[(f + 1) % h];
                        if (e.lat >= Math.Min(s.lat, m.lat) && e.lat <= Math.Max(s.lat, m.lat))
                        {
                            ++j;
                        }
                        else
                        {
                            j += 2;
                        }
                    }
                }
                s = q;
            }
            if (j % 2 == 0)
            {
                re = false;
            }
            else
            {
                re = true;
            }

            return re;
        }
    }
}
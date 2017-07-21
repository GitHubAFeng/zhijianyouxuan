using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
///获取默认位置
/// </summary>
namespace Map
{
    public static class DefautLocal
    {
        public static localInfo getlocalInfo()
        {
            localInfo ll = new localInfo();
            ll.Lat = SectionProxyData.GetSetValue(4);
            ll.Lng = SectionProxyData.GetSetValue(5);

            return ll;
        }
    }

    //位置
    public class localInfo
    {
        private string m_lat;
        private string m_lng;

        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat
        {
            get
            {
                return this.m_lat;
            }

            set
            {
                this.m_lat = value;
            }
        }

        /// <summary>
        ///经度
        /// </summary>
        public string Lng
        {
            get
            {
                return this.m_lng;
            }

            set
            {
                this.m_lng = value;
            }
        }
    }

}
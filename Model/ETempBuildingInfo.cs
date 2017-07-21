using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    public class ETempBuildingInfo
    {
        private int m__dataid;
        private string m__name;
        private string m__firstl;
        private int m__sectionid;
        private string m_sectionanme;

        private int id;

        /// <summary>
        /// 获取或设置
        /// </summary>
        public int dataid
        {
            get
            {
                return this.m__dataid;
            }

            set
            {
                this.m__dataid = value;
            }
        }

        /// <summary>
        /// 获取或设置
        /// </summary>
        public string name
        {
            get
            {
                return this.m__name;
            }

            set
            {
                this.m__name = value;
            }
        }

        /// <summary>
        /// 获取或设置
        /// </summary>
        public string firstl
        {
            get
            {
                return this.m__firstl;
            }

            set
            {
                this.m__firstl = value;
            }
        }

        /// <summary>
        /// 获取或设置
        /// </summary>
        public int sectionid
        {
            get
            {
                return this.m__sectionid;
            }

            set
            {
                this.m__sectionid = value;
            }
        }

        /// <summary>
        /// 获取或设置
        /// </summary>
        public string Sectionanme
        {
            get
            {
                return this.m_sectionanme;
            }

            set
            {
                this.m_sectionanme = value;
            }
        }

        /// <summary>
        /// 编号从1开始
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}

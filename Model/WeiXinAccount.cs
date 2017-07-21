using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace Hangjing.Model
{
    ///<summary>
    ///平台微信号信息
    ///<summary>
    public class WeiXinAccountInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int dataId
        {
            get;
            set;
        }

        /// <summary>
        /// 商家编号
        /// </summary>
        public int shopid
        {
            get;
            set;
        }

        /// <summary>
        /// 微信公众平台登录名
        /// </summary>
        public string wxusername
        {
            get;
            set;
        }

        /// <summary>
        /// 微信公众平台密码
        /// </summary>
        public string wxpwd
        {
            get;
            set;
        }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId
        {
            get;
            set;
        }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret
        {
            get;
            set;
        }

        /// <summary>
        /// 默认回复内容
        /// </summary>
        public int reveint1
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展
        /// </summary>
        public int reveint2
        {
            get;
            set;
        }

        /// <summary>
        /// 默认回复信息
        /// </summary>
        public string revevar1
        {
            get;
            set;
        }

        /// <summary>
        /// 微信网址，
        /// </summary>
        public string revevar2
        {
            get;
            set;
        }

        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public string partnerid
        {
            get;
            set;
        }

        /// <summary>
        /// apikey,在微信商户中心生成
        /// </summary>
        public string apikey
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string revevar3
        {
            get;
            set;
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string revevar4
        {
            get;
            set;
        }        
    }
}


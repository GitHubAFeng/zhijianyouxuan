using Newtonsoft.Json;
/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2015-01-15 16:47:40.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hangjing.Weixin
{
    /// <summary>
    ///  微信菜单按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        click,

        /// <summary>
        /// Url
        /// </summary>
        view,

        /// <summary>
        /// 扫码推事件的事件推送
        /// </summary>
        scancode_push,

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框的事件推送
        /// </summary>
        scancode_waitmsg,

        /// <summary>
        /// 弹出系统拍照发图的事件推送
        /// </summary>
        pic_sysphoto,

        /// <summary>
        /// 弹出拍照或者相册发图的事件推送
        /// </summary>
        pic_photo_or_album,

        /// <summary>
        /// 弹出微信相册发图器的事件推送
        /// </summary>
        pic_weixin,

        /// <summary>
        /// 弹出地理位置选择器的事件推送
        /// </summary>
        location_select
    }

    /// <summary>
    /// 微信自定义菜单
    /// </summary>
    public class WeixinMenuInfo
    {
        /// <summary>
        /// 按钮描述，既按钮名字，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 按钮类型（click或view）
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }

        /// <summary>
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string key { get; set; }

        /// <summary>
        /// 网页链接，用户点击按钮可打开链接，不超过256字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }

        /// <summary>
        /// 子按钮数组，按钮个数应为2~5个
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<WeixinMenuInfo> sub_button { get; set; }

        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="buttonType">菜单按钮类型</param>
        /// <param name="value">按钮的键值（Click)，或者连接URL(View)</param>
        public WeixinMenuInfo(string name, ButtonType buttonType, string value)
        {
            this.name = name;
            this.type = buttonType.ToString();

            switch (buttonType)
            {
                case ButtonType.click:
                    this.key = value;
                    break;
                case ButtonType.view:
                    this.url = value;
                    break;
                case ButtonType.location_select:
                    this.key = value;
                    break;
                default:
                    this.key = value;
                    break;
            }

        }

        /// <summary>
        /// 参数化构造函数,用于构造子菜单
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="sub_button">子菜单集合</param>
        public WeixinMenuInfo(string name, IEnumerable<WeixinMenuInfo> sub_button)
        {
            this.name = name;
            this.sub_button = new List<WeixinMenuInfo>();
            this.sub_button.AddRange(sub_button);
        }
    }

    /// <summary>
    /// 菜单的Json字符串对象
    /// </summary>
    public class MenuJson
    {
        public List<WeixinMenuInfo> button { get; set; }

        public MenuJson()
        {
            button = new List<WeixinMenuInfo>();
        }
    }

    /// <summary>
    /// 菜单列表的Json对象
    /// </summary>
    public class MenuListJson
    {
        public MenuJson menu { get; set; }
    }

}

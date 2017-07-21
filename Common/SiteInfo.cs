using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// SiteInfo.cs :SiteInfo  整站的配置信息管理类
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 20010-06-19

namespace Hangjing.Common
{
    /// <summary>
    /// 基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class SiteInfo
    {
        // Initial Catalog必须放在 Data Source  User ID Password之后,数据库备份程序中对此有要求
        // 数据库连接串-格式(中文为用户需要修改的内容)：Data Source=数据库服务器地址;User ID=您的数据库用户名;Password=您的数据库用户密码;Initial Catalog=数据库名称;Pooling=true
        // private string _connectstring = @"Data Source=HANGJING011\HANGJING011;User ID=easyeat;Password=easyeat;Initial Catalog=EasyPublish;Pooling=true";
        private string _connectstring;

        //站点名称

        private string _sitename = "出版社";

        //keywords
        private string _keywords = "出版社 图书销售";

        //description
        private string _description = "出版社 图书 网上书店";

        private string _databaseType = "SqlServer";

        private string _databaseOwner = "dbo";

        private string _maxsystemlogsize = "2048";

        private string _copyright = "© 出版社 &nbsp;2009-2010,All Rights Reserved";

        private int _regctrl = 0;           //IP 注册间隔限制(小时)

        private string _ipregctrl = "";     //特殊 IP 注册限制

        private string _ipdenyaccess = "";  //IP禁止访问列表

        private string _ipaccess = "";      //IP访问列表

        private string _adminipaccess = ""; //管理员后台IP访问列表

        //图片水印相关
        private int m_watermarkstatus = 3;              //图片附件添加水印 0=不使用 1=左上 2=中上 3=右上 4=左中 ... 9=右下
        private int m_watermarktype = 0;                //图片附件添加何种水印 0=文字 1=图片
        private int m_watermarktransparency = 5;        //图片水印透明度 取值范围1--10 (10为不透明)
        private string m_watermarktext = "Dianyifen";   //图片附件添加文字水印的内容 {1}表示论坛标题 {2}表示论坛地址 {3}表示当前日期 {4}表示当前时间, 例如: {3} {4}上传于{1} {2}
        private string m_watermarkpic = "watermark.gif";//使用的水印图片的名称
        private string m_watermarkfontname = "Tahoma";  //图片附件添加文字水印的字体
        private int m_watermarkfontsize = 12;           //图片附件添加文字水印的大小(像素)
        private int m_showattachmentpath = 0;           //图片附件如果直接显示, 地址是否直接使用图片真实路径
        private int m_attachimgquality = 80;            //是否是高质量图片 取值范围0--100

        /// <summary>
        /// 数据库连接串
        /// 格式(中文为用户需要修改的内容)：
        ///    Data Source=数据库服务器地址;
        ///    User ID=您的数据库用户名;
        ///    Password=您的数据库用户密码;
        ///    Initial Catalog=数据库名称;Pooling=true
        /// </summary>
        public string ConnectString
        {
            get { return _connectstring; }
            set { _connectstring = value; }
        }

        public string SiteName
        {
            set { _sitename = value; }
            get { return _sitename; }
        }

        public string KeyWords
        {
            set { _keywords = value; }
            get { return _keywords; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        /// <summary>
        /// 系统数据库类型
        /// </summary>
        public string DatabaseType
        {
            set { _databaseType = value; }
            get { return _databaseType; }
        }

        /// <summary>
        /// 数据库所有者
        /// </summary>
        public string DatabaseOwner
        {
            set { _databaseOwner = value; }
            get { return _databaseOwner; }
        }

        /// <summary>
        /// 数据库中保存的最大系统日志的大小 单位kb
        /// </summary>
        public string MaxSystemLogSize
        {
            set { _maxsystemlogsize = value; }
            get { return _maxsystemlogsize; }
        }

        /// <summary>
        /// 网站版权信息
        /// </summary>
        public string CopyRight
        {
            set { _copyright = value; }
            get { return _copyright; }
        }

        /// <summary>
        /// IP 注册间隔限制(小时)
        /// </summary>
        public int Regctrl
        {
            get { return _regctrl; }
            set { _regctrl = value; }
        }

        /// <summary>
        /// 特殊 IP 注册限制
        /// </summary>
        public string Ipregctrl
        {
            get { return _ipregctrl; }
            set { _ipregctrl = value; }
        }

        /// <summary>
        /// IP禁止访问列表
        /// </summary>
        public string Ipdenyaccess
        {
            get { return _ipdenyaccess; }
            set { _ipdenyaccess = value; }
        }

        /// <summary>
        /// IP访问列表
        /// </summary>
        public string Ipaccess
        {
            get { return _ipaccess; }
            set { _ipaccess = value; }
        }

        /// <summary>
        /// 管理员后台IP访问列表
        /// </summary>
        public string Adminipaccess
        {
            get { return _adminipaccess; }
            set { _adminipaccess = value; }
        }

        /// <summary>
        /// 图片附件添加水印 0=不使用 1=左上 2=中上 3=右上 4=左中 ... 9=右下
        /// </summary>
        public int Watermarkstatus
        {
            get { return m_watermarkstatus; }
            set { m_watermarkstatus = value; }
        }

        /// <summary>
        /// 图片附件添加何种水印 0=文字 1=图片
        /// </summary>
        public int Watermarktype
        {
            get { return m_watermarktype; }
            set { m_watermarktype = value; }
        }

        /// <summary>
        /// 图片水印透明度 取值范围1--10 (10为不透明)
        /// </summary>
        public int Watermarktransparency
        {
            get { return m_watermarktransparency; }
            set { m_watermarktransparency = value; }
        }

        /// <summary>
        /// 图片附件添加文字水印的内容 {1}表示论坛标题 {2}表示论坛地址 {3}表示当前日期 {4}表示当前时间, 例如: {3} {4}上传于{1} {2}
        /// </summary>
        public string Watermarktext
        {
            get { return m_watermarktext; }
            set { m_watermarktext = value; }
        }

        /// <summary>
        /// 使用的水印图片的名称
        /// </summary>
        public string Watermarkpic
        {
            get { return m_watermarkpic; }
            set { m_watermarkpic = value; }
        }

        /// <summary>
        /// 图片附件添加文字水印的字体
        /// </summary>
        public string Watermarkfontname
        {
            get { return m_watermarkfontname; }
            set { m_watermarkfontname = value; }
        }

        /// <summary>
        /// 图片附件添加文字水印的大小(像素)
        /// </summary>
        public int Watermarkfontsize
        {
            get { return m_watermarkfontsize; }
            set { m_watermarkfontsize = value; }
        }

        /// <summary>
        /// 图片附件如果直接显示, 地址是否直接使用图片真实路径
        /// </summary>
        public int Showattachmentpath
        {
            get { return m_showattachmentpath; }
            set { m_showattachmentpath = value; }
        }

        /// <summary>
        /// 附件图片质量　取值范围 1是　0不是
        /// </summary>
        public int Attachimgquality
        {
            get { return m_attachimgquality; }
            set { m_attachimgquality = value; }
        }
    }
}

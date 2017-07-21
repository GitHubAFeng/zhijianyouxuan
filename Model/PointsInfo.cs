using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// 实体类points 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PointsInfo
    {
        private int _unid;
        private string _inuse;
        private string _id;
        private string _name;
        private string _comm;
        private int _ptype;
        private int _rcvtype;
        private string _posaddr;
        private string _posroom;
        private string _posattch;
        private string _namepy;
        private string _possrvad;
        private string _commperson;
        private DateTime _endtopam;
        private DateTime _stoppm;
        private decimal _sendlimit;
        private string _loginname;
        private string _password;
        private decimal _sendfee;
        private string _sn1;
        private string _sn2;
        private string _sn2key;
        private int _ptimes;
        private string _mgrcell;
        private string _phead;
        private string _pend;
        private string _opentime;
        private int _iscallcenter;
        private string _address;
        private string _introduce;
        private int _status;
        private string _outnitice;
        private DateTime _intime;
        private DateTime _time1start;
        private DateTime _time1end;
        private int _isdelete;
        private int _sortnum;
        private int _flavorgrade;
        private int _servicegrade;
        private int _speedgrade;
        private int _star;
        private string _category;
        private int _viewtimes;
        private int _senttime;
        private string _sentorg;
        private string _special;
        private int _reviewtimes;
        private decimal _money;
        private int _inve1;
        private int _menunum;
        private string _picture;
        private int _showpicture;
        private DateTime _foodupdatetime;
        private DateTime _time2start;
        private DateTime _time2end;
        private DateTime _bisnessstart;
        private DateTime _bisnessend;
        private int _point;
        private int _showlocal;
        private int _Grade;

        private int _isbisness;

        private int _pop;

        private string _email;

        private int _allcount;
        private decimal _allprice;

        private string _EBuilding;

        private DateTime _Opentimes1;
        private DateTime _Opentimes2;
        private DateTime _Closetimes1;
        private DateTime _Closetimes2;

        private DateTime _LastLoginDate;
        private int _cityid;
        private string _bigpicture;

        /// <summary>
        /// 商家横幅
        /// </summary>
        public string BigPicture
        {
            set { _bigpicture = value; }
            get { return _bigpicture; }
        }

        /// <summary>
        /// 距离
        /// </summary>
        public decimal Distance
        {
            set;
            get;
        }

        public int cityid
        {
            set { _cityid = value; }
            get { return _cityid; }
        }

        /// <summary>
        /// 打印机最后更新时间
        /// </summary>
        public DateTime LastLoginDate
        {
            set { _LastLoginDate = value; }
            get { return _LastLoginDate; }

        }

        /// <summary>
        /// 营业时间一开始
        /// </summary>
        public DateTime Opentimes1
        {
            set { _Opentimes1 = value; }
            get { return _Opentimes1; }

        }

        /// <summary>
        /// 营业时间一结束
        /// </summary>
        public DateTime Opentimes2
        {

            set { _Opentimes2 = value; }
            get { return _Opentimes2; }
        }

        /// <summary>
        /// 营业时间二开始
        /// </summary>
        public DateTime Closetimes1
        {
            set { _Closetimes1 = value; }
            get { return _Closetimes1; }
        }

        /// <summary>
        /// 营业时间二结束
        /// </summary>
        public DateTime Closetimes2
        {
            set { _Closetimes2 = value; }
            get { return _Closetimes2; }
        }

        /// <summary>
        /// 未用
        /// </summary>
        public string EBuilding
        {
            set { _EBuilding = value; }
            get { return _EBuilding; }
        }

        /// <summary>
        /// 是否营业通过营业时间判断的。1表示是，0表示没有营业
        /// </summary>
        public int isbisness
        {
            set { _isbisness = value; }
            get { return _isbisness; }
        }


        /// <summary>
        /// 销量
        /// </summary>
        public int pop
        {
            set { _pop = value; }
            get { return _pop; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unid
        {
            set { _unid = value; }
            get { return _unid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InUse
        {
            set { _inuse = value; }
            get { return _inuse; }
        }
        /// <summary>
        /// 是否热门： 0表示不是，1表示是
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comm
        {
            set { _comm = value; }
            get { return _comm; }
        }
        /// <summary>
        /// 促销类型：0表示无，10表示使用商家自己的促销，20表示使用平台促销
        /// </summary>
        public int PType
        {
            set { _ptype = value; }
            get { return _ptype; }
        }
        /// <summary>
        ///  是否自动接单=》 0：否,1:是
        /// </summary>
        public int RcvType
        {
            set { _rcvtype = value; }
            get { return _rcvtype; }
        }
        /// <summary>
        /// 微信公众号对应的openid(在微信里登录时保存)
        /// </summary>
        public string PosAddr
        {
            set { _posaddr = value; }
            get { return _posaddr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PosRoom
        {
            set { _posroom = value; }
            get { return _posroom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PosAttch
        {
            set { _posattch = value; }
            get { return _posattch; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NamePy
        {
            set { _namepy = value; }
            get { return _namepy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PosSrvAd
        {
            set { _possrvad = value; }
            get { return _possrvad; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CommPerson
        {
            set { _commperson = value; }
            get { return _commperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StopAM
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StopPM
        {
            set { _stoppm = value; }
            get { return _stoppm; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public decimal SendLimit
        {
            set { _sendlimit = value; }
            get { return _sendlimit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public decimal SendFee
        {
            set { _sendfee = value; }
            get { return _sendfee; }
        }
        /// <summary>
        /// 佣金类型：0表示是比例（商品金额），1表示按单收费
        /// </summary>
        public string SN1
        {
            set { _sn1 = value; }
            get { return _sn1; }
        }
        /// <summary>
        /// 分销百分比
        /// </summary>
        public string SN2
        {
            set { _sn2 = value; }
            get { return _sn2; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string Sn2Key
        {
            set { _sn2key = value; }
            get { return _sn2key; }
        }
        /// <summary>
        /// 未用（以前是免配送费条件）
        /// </summary>
        public int PTimes
        {
            set { _ptimes = value; }
            get { return _ptimes; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string MgrCell
        {
            set { _mgrcell = value; }
            get { return _mgrcell; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string PHead
        {
            set { _phead = value; }
            get { return _phead; }
        }
        /// <summary>
        /// 参与平台促销项目，以这样的形式保存：{1},{2}
        /// </summary>
        public string PEnd
        {
            set { _pend = value; }
            get { return _pend; }
        }

        /// <summary>
        /// 保存商家标签,以这样的形式保存：{1},{2}
        /// </summary>
        public string OpenTime
        {
            set { _opentime = value; }
            get { return _opentime; }
        }

        /// <summary>
        /// 商家类型=》  0：外卖商家，1：超市（一个城市只有一个超市）
        /// </summary>
        public int IsCallCenter
        {
            set { _iscallcenter = value; }
            get { return _iscallcenter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduce
        {
            set { _introduce = value; }
            get { return _introduce; }
        }
        /// <summary>
        /// 状态1:正常营业;0:暂停营业;-1休息
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string outnitice
        {
            set { _outnitice = value; }
            get { return _outnitice; }
        }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime InTime
        {
            set { _intime = value; }
            get { return _intime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time1Start
        {
            set { _time1start = value; }
            get { return _time1start; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time1End
        {
            set { _time1end = value; }
            get { return _time1end; }
        }
        /// <summary>
        /// 0表示正常，1表示删除了
        /// </summary>
        public int IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// 排序降序
        /// </summary>
        public int SortNum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
        }
        /// <summary>
        /// 口味打分，都是平均分
        /// </summary>
        public int FlavorGrade
        {
            set { _flavorgrade = value; }
            get { return _flavorgrade; }
        }
        /// <summary>
        /// 服务打分，都是平均分
        /// </summary>
        public int ServiceGrade
        {
            set { _servicegrade = value; }
            get { return _servicegrade; }
        }
        /// <summary>
        /// 速度打分，都是平均分
        /// </summary>
        public int SpeedGrade
        {
            set { _speedgrade = value; }
            get { return _speedgrade; }
        }
        /// <summary>
        /// 是否审核0表示，没有，1表示审核,2审核失败
        /// </summary>
        public int Star
        {
            set { _star = value; }
            get { return _star; }
        }
        /// <summary>
        /// 分类,用{1},{2}的形式保存
        /// </summary>
        public string category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewTimes
        {
            set { _viewtimes = value; }
            get { return _viewtimes; }
        }
        /// <summary>
        /// 送达时间
        /// </summary>
        public int senttime
        {
            set { _senttime = value; }
            get { return _senttime; }
        }
        /// <summary>
        /// 配送方式,0表示统一配送，1表示商家自送
        /// </summary>
        public string sentorg
        {
            set { _sentorg = value; }
            get { return _sentorg; }
        }
        /// <summary>
        /// 店铺活动
        /// </summary>
        public string special
        {
            set { _special = value; }
            get { return _special; }
        }
        /// <summary>
        /// 评论次数
        /// </summary>
        public int reviewtimes
        {
            set { _reviewtimes = value; }
            get { return _reviewtimes; }
        }
        /// <summary>
        /// 帐号
        /// </summary>
        public decimal money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 配送半径（公里），根据配送表 shopdelivery 中最大值生成
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int menunum
        {
            set { _menunum = value; }
            get { return _menunum; }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// 是否送饮料：0表示不送，1表示送
        /// </summary>
        public int showpicture
        {
            set { _showpicture = value; }
            get { return _showpicture; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime foodupdatetime
        {
            set { _foodupdatetime = value; }
            get { return _foodupdatetime; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime Time2Start
        {
            set { _time2start = value; }
            get { return _time2start; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime Time2End
        {
            set { _time2end = value; }
            get { return _time2end; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime bisnessStart
        {
            set { _bisnessstart = value; }
            get { return _bisnessstart; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime bisnessend
        {
            set { _bisnessend = value; }
            get { return _bisnessend; }
        }

        private DateTime _bisnessStart2;
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime bisnessStart2
        {
            set { _bisnessStart2 = value; }
            get { return _bisnessStart2; }
        }
        private DateTime _bisnessend2;
        /// <summary>
        /// 未用
        /// </summary>
        public DateTime bisnessend2
        {
            set { _bisnessend2 = value; }
            get { return _bisnessend2; }
        }
        /// <summary>
        ///  sn1为0，表示按比例，为1时按单收费
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 配送点编号：没有为0 => DeliverySite.DataId
        /// </summary>
        public int showlocal
        {
            set { _showlocal = value; }
            get { return _showlocal; }
        }

        /// <summary>
        /// 评价星级，根据评论的3项平均得分
        /// </summary>
        public int Grade
        {
            set { _Grade = value; }
            get { return _Grade; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int allcount
        {
            set { _allcount = value; }
            get { return _allcount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal allprice
        {
            set { _allprice = value; }
            get { return _allprice; }
        }

        private int _isonline;
        /// <summary>
        /// 是否营业。1表示是，0表示没有营业
        /// </summary>
        public int isonline
        {
            set { _isonline = value; }
            get { return _isonline; }
        }

        private string _lat;
        /// <summary>
        /// 店铺坐标 横坐标
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        private string _lng;
        /// <summary>
        /// 店铺坐标 纵坐标
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

        /// <summary>
        /// 商家营业时间串
        /// </summary>
        public string opentimestr
        {
            set;
            get;
        }

        /// <summary>
        /// 商家经营分类名称
        /// </summary>
        public string sortnames
        {
            get;
            set;
        }

        /// <summary>
        /// 是否收藏，0表示没有，1表示是
        /// </summary>
        public int iscollect
        {
            get;
            set;
        }


        /// <summary>
        /// 商家标签列表
        /// </summary>
        public IList<ShopFoodPictureInfo> pictags
        {
            get;
            set;
        }

        /// <summary>
        /// 商家促销标签
        /// </summary>
        public IList<ShopFoodPictureInfo> promotions
        {
            get;
            set;
        }


        /// <summary>
        /// 商品列表
        /// </summary>
        public IList<FoodinfoInfo> Foods
        {
            get;
            set;
        }

        /// <summary>
        /// 搜索类型  0表示没有使用搜索  1表示使用商家名称搜索的结果  2表示使用菜品名称搜索的结果 
        /// </summary>
        public int seekType
        {
            get;
            set;
        }
        /// <summary>
        /// 搜索的商家名称或菜品名称
        /// </summary>
        public string keyWord
        {
            get;
            set;
        }

        private string _licensePic;
        /// <summary>
        /// 营业执照
        /// </summary>
        public string licensePic
        {
            get { return _licensePic; }
            set { _licensePic = value; }
        }
        private int _isLicense;

        /// <summary>
        /// 是否显示营业执照(0：不显示,1:显示)
        /// </summary>
        public int isLicense
        {
            get { return _isLicense; }
            set { _isLicense = value; }
        }

        private string _cateringPic;
        /// <summary>
        /// 餐饮服务许可证
        /// </summary>
        public string cateringPic
        {
            get { return _cateringPic; }
            set { _cateringPic = value; }
        }

        private int _isCatering;
        /// <summary>
        /// 是否显示餐饮服务许可证(0：不显示,1:显示)
        /// </summary>
        public int isCatering
        {
            get { return _isCatering; }
            set { _isCatering = value; }
        }
        public string review
        {
            get;
            set;
        }
        /// <summary>
        /// 订单列表刷新时间（商户后台）
        /// </summary>
        public string RefreshTime
        {
            get;
            set;
        }


    }
}


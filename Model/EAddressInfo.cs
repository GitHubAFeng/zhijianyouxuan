/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : eaddress.cs
 * Function : 地址实现类
 * Created by jijunjian at 2010-7-26 16:53:41.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// 实体类EAddress 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class EAddressInfo
    {
        private int _dataid;
        private int _userid;
        private int _buildingid;
        private string _address;
        private int _pri;
        private DateTime _addtime;
        private string _phone;
        private string _mobilephone;
        private string _receiver;
        private string _buildingname;

        private string _gainTime;
        private int _paymode;
        private string _sendtime;

        private decimal _pointrat;
        private string _lat;
        private string _lng;

        private string _kefuid;
        private int _orderSorces;
        private decimal _senmoney;

        /// <summary>
        /// 订单来源
        /// </summary>
        public string Ordersource
        {
            set;
            get;
        }

        /// <summary>
        /// 手机提交的配送费
        /// </summary>
        public decimal senmoney
        {
            get { return _senmoney; }
            set { _senmoney = value; }
        }

        /// <summary>
        /// zfy 前台下单为空，客服下单为客服的名称
        /// </summary>
        public string kefuid
        {
            set { _kefuid = value; }
            get { return _kefuid; }
        }

        /// <summary>
        /// 保存地址的坐标lat
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        /// <summary>
        /// 保存地址的坐标lng
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int cityid
        {
            get;
            set;
        }

        /// <summary>
        /// 积分倍数 
        /// </summary>
        public decimal pointrat
        {
            get { return _pointrat; }
            set { _pointrat = value; }
        }

        private decimal _foodmoneydiscount;
        /// <summary>
        /// 菜品折扣 
        /// </summary>
        public decimal foodmoneydiscount
        {
            get { return _foodmoneydiscount; }
            set { _foodmoneydiscount = value; }
        }

        /// <summary>
        /// 送餐时间
        /// </summary>
        public string sendtime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 支付类型 （1支付宝/2银联/3账户余额/4货到付款/5微信支付）
        /// </summary>
        public int paymode
        {
            set { _paymode = value; }
            get { return _paymode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BuildingID
        {
            set { _buildingid = value; }
            get { return _buildingid; }
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
        /// 1表示此地址是默认地址.0表示非.
        /// </summary>
        public int Pri
        {
            set { _pri = value; }
            get { return _pri; }
        }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string Mobilephone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }

        /// <summary>
        /// 写字楼名称
        /// </summary>
        public string BuildingName
        {
            set
            {
                _buildingname = value;
            }
            get
            {
                return _buildingname;
            }
        }

        /// <summary>
        /// 送餐时间表
        /// </summary>
        public string GainTime
        {
            set
            {
                _gainTime = value;
            }
            get
            {
                return _gainTime;
            }
        }

        private int _reveint1;
        /// <summary>
        /// 就餐人数
        /// </summary>
        public int ReveInt1
        {
            get { return _reveint1; }
            set { _reveint1 = value; }
        }

        private int _reveint2;
        /// <summary>
        /// 就餐类型：0外卖，1堂食
        /// </summary>
        public int ReveInt2
        {
            get { return _reveint2; }
            set { _reveint2 = value; }
        }

        /// <summary>
        /// 订单来源：0 网站，6 客服
        /// </summary>
        public string fromweb
        {
            set;
            get;
        }

        /// <summary>
        /// 临时编号，对于未登录的使用，通常用guid
        /// </summary>
        public string tempcode
        {
            set;
            get;
        }

        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPassword
        {
            set;
            get;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set;
            get;
        }

        /// <summary>
        /// 用户纬度
        /// </summary>
        public string ulat
        {
            set;
            get;
        }

        /// <summary>
        /// 用户经度
        /// </summary>
        public string ulng
        {
            set;
            get;
        }

        /// <summary>
        /// android ,iso提交订单(商家列表)
        /// </summary>
        public IList<Hangjing.Model.ETogoShoppingCart> shoplist
        {
            set;
            get;
        }

        /// <summary>
        /// 优惠券json(可以有多个.本项目只能用一个)
        /// </summary>
        public string shopcardjson
        {
            set;
            get;
        }

        /// <summary>
        /// 是否使用优惠券,0表示未用,1表示在用
        /// </summary>
        public int isuercard
        {
            set;
            get;
        }

        /// <summary>
        /// 会员昵称
        /// </summary>
        public string CustomerName
        {
            set;
            get;
        }

        /// <summary>
        /// 用户，商家坐标。
        /// </summary>
        public string latlng
        {
            set;
            get;
        }

        /// <summary>
        /// 商家原价
        /// </summary>
        public decimal foodprice
        {
            set;
            get;
        }

        /// <summary>
        /// 红包
        /// </summary>
        public msgpacketInfo redpackage
        {
            set;
            get;
        }

        

        /// <summary>
        /// 订单参与的促销项目
        /// </summary>
        public IList<OrderPromotionInfo> Promotions
        {
            set;
            get;
        }


    }
}


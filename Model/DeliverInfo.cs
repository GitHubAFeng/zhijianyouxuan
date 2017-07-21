using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    //配送员（站）信息
    [Serializable]
    public class DeliverInfo
    {
        private int _dataid;
        private string _codeid;
        private string _name;
        private string _phone;
        private string _section;
        private int _status;
        private string _gpsimei;
        private int _ordernum;
        private string _username;
        private string _password;
        private int _inve1;
        private string _inve2;
        private DateTime _adddate;

        //坐标相关
        private string _lat;
        private string _lng;
        private DateTime _loacltime;

        private int _distance;

        private int _direction;

        private decimal _speed;

        private string _carstate;

        /// <summary>
        /// 车况 离线显示灰色、停车显示黄色、行驶显示红色 
        /// </summary>
        public string carstate
        {
            set { _carstate = value; }
            get { return _carstate; }
        }

        /// <summary>
        /// 航向（0-360）
        /// </summary>
        public int direction
        {
            set { _direction = value; }
            get { return _direction; }
        }

        /// <summary>
        /// 速度(公里/小时)
        /// </summary>
        public decimal speed
        {
            set { _speed = value; }
            get { return _speed; }
        }


        /// <summary>
        /// 商家到配送员距离(米)
        /// </summary>
        public int distance
        {
            set { _distance = value; }
            get { return _distance; }
        }


        /// <summary>
        /// 编号主键
        /// </summary>
        public int DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// 每单费用
        /// </summary>
        public string CodeId
        {
            set { _codeid = value; }
            get { return _codeid; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 电话 接收短信的号码
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        /// <summary>
        /// 每单提成
        /// </summary>
        public string Section
        {
            set { _section = value; }
            get { return _section; }
        }

        /// <summary>
        /// 状态 1空闲 0 离线 -1 请假  2 繁忙
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 群组编号
        /// </summary>
        public string GpsIMEI
        {
            set { _gpsimei = value; }
            get { return _gpsimei; }
        }

        /// <summary>
        /// 配送中订单个数.考虑是否要在这里保存?
        /// </summary>
        public int OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
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
        /// 城市编号
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }

        /// <summary>
        /// 车辆编号
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

        /// <summary>
        /// 经纬度之 维度：30.260683333333333  latitude 
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        /// <summary>
        ///  经纬度之 经度：120.16711333333333 longitude 
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

        /// <summary>
        /// 坐标时间（此坐标上传的时间）
        /// </summary>
        public DateTime LocalTime
        {
            set { _loacltime = value; }
            get { return _loacltime; }
        }

        /// <summary>
        /// 城市名称
        /// </string>
        public string cityname
        {
            get;
            set;
        }

        /// <summary>
        /// 群组名称
        /// </string>
        public string Groupname
        {
            get;
            set;
        }

        /// <summary>
        /// 审核情况 0审核通过 1未审核
        /// </summary>
        public int IsApproved
        {
            set;
            get;
        }

        /// <summary>
        /// 骑士身份证照片
        /// </summary>
        public string pic1
        {
            set;
            get;
        }


        /// <summary>
        /// 完成的订单
        /// </summary>
        public int completeorder
        {
            set;
            get;
        }

        /// <summary>
        /// 超时订单
        /// </summary>
        public int timeoutorder
        {
            set;
            get;
        }

        /// <summary>
        /// 记录数
        /// </summary>
        public int recordtcount
        {
            get;
            set;
        }
        /// <summary>
        /// 骑士账户余额
        /// </summary>
        public decimal havemoney
        {
            get;
            set;
        }
        /// <summary>
        /// 是否接单，1表示正常接单，0表示暂停接单
        /// </summary>
        public int IsWorking
        {
            set;
            get;
        }

    }
}

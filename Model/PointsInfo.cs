using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// ʵ����points ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �̼Һ��
        /// </summary>
        public string BigPicture
        {
            set { _bigpicture = value; }
            get { return _bigpicture; }
        }

        /// <summary>
        /// ����
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
        /// ��ӡ��������ʱ��
        /// </summary>
        public DateTime LastLoginDate
        {
            set { _LastLoginDate = value; }
            get { return _LastLoginDate; }

        }

        /// <summary>
        /// Ӫҵʱ��һ��ʼ
        /// </summary>
        public DateTime Opentimes1
        {
            set { _Opentimes1 = value; }
            get { return _Opentimes1; }

        }

        /// <summary>
        /// Ӫҵʱ��һ����
        /// </summary>
        public DateTime Opentimes2
        {

            set { _Opentimes2 = value; }
            get { return _Opentimes2; }
        }

        /// <summary>
        /// Ӫҵʱ�����ʼ
        /// </summary>
        public DateTime Closetimes1
        {
            set { _Closetimes1 = value; }
            get { return _Closetimes1; }
        }

        /// <summary>
        /// Ӫҵʱ�������
        /// </summary>
        public DateTime Closetimes2
        {
            set { _Closetimes2 = value; }
            get { return _Closetimes2; }
        }

        /// <summary>
        /// δ��
        /// </summary>
        public string EBuilding
        {
            set { _EBuilding = value; }
            get { return _EBuilding; }
        }

        /// <summary>
        /// �Ƿ�Ӫҵͨ��Ӫҵʱ���жϵġ�1��ʾ�ǣ�0��ʾû��Ӫҵ
        /// </summary>
        public int isbisness
        {
            set { _isbisness = value; }
            get { return _isbisness; }
        }


        /// <summary>
        /// ����
        /// </summary>
        public int pop
        {
            set { _pop = value; }
            get { return _pop; }
        }

        /// <summary>
        /// ����
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
        /// �Ƿ����ţ� 0��ʾ���ǣ�1��ʾ��
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
        /// �������ͣ�0��ʾ�ޣ�10��ʾʹ���̼��Լ��Ĵ�����20��ʾʹ��ƽ̨����
        /// </summary>
        public int PType
        {
            set { _ptype = value; }
            get { return _ptype; }
        }
        /// <summary>
        ///  �Ƿ��Զ��ӵ�=�� 0����,1:��
        /// </summary>
        public int RcvType
        {
            set { _rcvtype = value; }
            get { return _rcvtype; }
        }
        /// <summary>
        /// ΢�Ź��ںŶ�Ӧ��openid(��΢�����¼ʱ����)
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
        /// δ��
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
        /// δ��
        /// </summary>
        public decimal SendFee
        {
            set { _sendfee = value; }
            get { return _sendfee; }
        }
        /// <summary>
        /// Ӷ�����ͣ�0��ʾ�Ǳ�������Ʒ����1��ʾ�����շ�
        /// </summary>
        public string SN1
        {
            set { _sn1 = value; }
            get { return _sn1; }
        }
        /// <summary>
        /// �����ٷֱ�
        /// </summary>
        public string SN2
        {
            set { _sn2 = value; }
            get { return _sn2; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public string Sn2Key
        {
            set { _sn2key = value; }
            get { return _sn2key; }
        }
        /// <summary>
        /// δ�ã���ǰ�������ͷ�������
        /// </summary>
        public int PTimes
        {
            set { _ptimes = value; }
            get { return _ptimes; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public string MgrCell
        {
            set { _mgrcell = value; }
            get { return _mgrcell; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public string PHead
        {
            set { _phead = value; }
            get { return _phead; }
        }
        /// <summary>
        /// ����ƽ̨������Ŀ������������ʽ���棺{1},{2}
        /// </summary>
        public string PEnd
        {
            set { _pend = value; }
            get { return _pend; }
        }

        /// <summary>
        /// �����̼ұ�ǩ,����������ʽ���棺{1},{2}
        /// </summary>
        public string OpenTime
        {
            set { _opentime = value; }
            get { return _opentime; }
        }

        /// <summary>
        /// �̼�����=��  0�������̼ң�1�����У�һ������ֻ��һ�����У�
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
        /// ���
        /// </summary>
        public string Introduce
        {
            set { _introduce = value; }
            get { return _introduce; }
        }
        /// <summary>
        /// ״̬1:����Ӫҵ;0:��ͣӪҵ;-1��Ϣ
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public string outnitice
        {
            set { _outnitice = value; }
            get { return _outnitice; }
        }
        /// <summary>
        /// ����ʱ��
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
        /// 0��ʾ������1��ʾɾ����
        /// </summary>
        public int IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int SortNum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
        }
        /// <summary>
        /// ��ζ��֣�����ƽ����
        /// </summary>
        public int FlavorGrade
        {
            set { _flavorgrade = value; }
            get { return _flavorgrade; }
        }
        /// <summary>
        /// �����֣�����ƽ����
        /// </summary>
        public int ServiceGrade
        {
            set { _servicegrade = value; }
            get { return _servicegrade; }
        }
        /// <summary>
        /// �ٶȴ�֣�����ƽ����
        /// </summary>
        public int SpeedGrade
        {
            set { _speedgrade = value; }
            get { return _speedgrade; }
        }
        /// <summary>
        /// �Ƿ����0��ʾ��û�У�1��ʾ���,2���ʧ��
        /// </summary>
        public int Star
        {
            set { _star = value; }
            get { return _star; }
        }
        /// <summary>
        /// ����,��{1},{2}����ʽ����
        /// </summary>
        public string category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public int ViewTimes
        {
            set { _viewtimes = value; }
            get { return _viewtimes; }
        }
        /// <summary>
        /// �ʹ�ʱ��
        /// </summary>
        public int senttime
        {
            set { _senttime = value; }
            get { return _senttime; }
        }
        /// <summary>
        /// ���ͷ�ʽ,0��ʾͳһ���ͣ�1��ʾ�̼�����
        /// </summary>
        public string sentorg
        {
            set { _sentorg = value; }
            get { return _sentorg; }
        }
        /// <summary>
        /// ���̻
        /// </summary>
        public string special
        {
            set { _special = value; }
            get { return _special; }
        }
        /// <summary>
        /// ���۴���
        /// </summary>
        public int reviewtimes
        {
            set { _reviewtimes = value; }
            get { return _reviewtimes; }
        }
        /// <summary>
        /// �ʺ�
        /// </summary>
        public decimal money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// ���Ͱ뾶��������������ͱ� shopdelivery �����ֵ����
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public int menunum
        {
            set { _menunum = value; }
            get { return _menunum; }
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// �Ƿ������ϣ�0��ʾ���ͣ�1��ʾ��
        /// </summary>
        public int showpicture
        {
            set { _showpicture = value; }
            get { return _showpicture; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public DateTime foodupdatetime
        {
            set { _foodupdatetime = value; }
            get { return _foodupdatetime; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public DateTime Time2Start
        {
            set { _time2start = value; }
            get { return _time2start; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public DateTime Time2End
        {
            set { _time2end = value; }
            get { return _time2end; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public DateTime bisnessStart
        {
            set { _bisnessstart = value; }
            get { return _bisnessstart; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public DateTime bisnessend
        {
            set { _bisnessend = value; }
            get { return _bisnessend; }
        }

        private DateTime _bisnessStart2;
        /// <summary>
        /// δ��
        /// </summary>
        public DateTime bisnessStart2
        {
            set { _bisnessStart2 = value; }
            get { return _bisnessStart2; }
        }
        private DateTime _bisnessend2;
        /// <summary>
        /// δ��
        /// </summary>
        public DateTime bisnessend2
        {
            set { _bisnessend2 = value; }
            get { return _bisnessend2; }
        }
        /// <summary>
        ///  sn1Ϊ0����ʾ��������Ϊ1ʱ�����շ�
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// ���͵��ţ�û��Ϊ0 => DeliverySite.DataId
        /// </summary>
        public int showlocal
        {
            set { _showlocal = value; }
            get { return _showlocal; }
        }

        /// <summary>
        /// �����Ǽ����������۵�3��ƽ���÷�
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
        /// �Ƿ�Ӫҵ��1��ʾ�ǣ�0��ʾû��Ӫҵ
        /// </summary>
        public int isonline
        {
            set { _isonline = value; }
            get { return _isonline; }
        }

        private string _lat;
        /// <summary>
        /// �������� ������
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        private string _lng;
        /// <summary>
        /// �������� ������
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }

        /// <summary>
        /// �̼�Ӫҵʱ�䴮
        /// </summary>
        public string opentimestr
        {
            set;
            get;
        }

        /// <summary>
        /// �̼Ҿ�Ӫ��������
        /// </summary>
        public string sortnames
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ��ղأ�0��ʾû�У�1��ʾ��
        /// </summary>
        public int iscollect
        {
            get;
            set;
        }


        /// <summary>
        /// �̼ұ�ǩ�б�
        /// </summary>
        public IList<ShopFoodPictureInfo> pictags
        {
            get;
            set;
        }

        /// <summary>
        /// �̼Ҵ�����ǩ
        /// </summary>
        public IList<ShopFoodPictureInfo> promotions
        {
            get;
            set;
        }


        /// <summary>
        /// ��Ʒ�б�
        /// </summary>
        public IList<FoodinfoInfo> Foods
        {
            get;
            set;
        }

        /// <summary>
        /// ��������  0��ʾû��ʹ������  1��ʾʹ���̼����������Ľ��  2��ʾʹ�ò�Ʒ���������Ľ�� 
        /// </summary>
        public int seekType
        {
            get;
            set;
        }
        /// <summary>
        /// �������̼����ƻ��Ʒ����
        /// </summary>
        public string keyWord
        {
            get;
            set;
        }

        private string _licensePic;
        /// <summary>
        /// Ӫҵִ��
        /// </summary>
        public string licensePic
        {
            get { return _licensePic; }
            set { _licensePic = value; }
        }
        private int _isLicense;

        /// <summary>
        /// �Ƿ���ʾӪҵִ��(0������ʾ,1:��ʾ)
        /// </summary>
        public int isLicense
        {
            get { return _isLicense; }
            set { _isLicense = value; }
        }

        private string _cateringPic;
        /// <summary>
        /// �����������֤
        /// </summary>
        public string cateringPic
        {
            get { return _cateringPic; }
            set { _cateringPic = value; }
        }

        private int _isCatering;
        /// <summary>
        /// �Ƿ���ʾ�����������֤(0������ʾ,1:��ʾ)
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
        /// �����б�ˢ��ʱ�䣨�̻���̨��
        /// </summary>
        public string RefreshTime
        {
            get;
            set;
        }


    }
}


using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// ���ȶ���
    /// </summary>
    [Serializable]
    public class ExpressOrderInfo
    {
        private int _dataid;
        private string _orderid;
        private int _userid;
        private string _username;
        private string _tel;
        private string _senttime;
        private string _address;
        private int _state;
        private int _togoid;
        private DateTime _ordertime;
        private decimal _totalprice;
        private DateTime _setstatetime;
        private decimal _currentprice;

        private int _foodcount;
        private int _ordercount;
        private decimal _ordertotal;

        private string _togoname;
        private string _customername;

        private string _Remark;
        private string _oorderid;
        private int _callcount;
        private string _callmsg;
        private string _writer;

        //֧������
        private DateTime _paytime;
        private int _paystate;//0��ʾ֧����1�ɹ���-1ʧ��
        private decimal _paymoney;
        private int _paymode;
        private string payorderid;
        private decimal _sendmoney;

        private int _Inve1;
        private int _sid;
        private string _inve2;
        private int _bid;
        private int _cityid;
        private string _tempcode;
        private string strPrintEnd;
        private int _addpoint;

        //�����������ֶ�
        private int _ordersource;
        private int _isaddpoint;
        private int _sendtype;
        private string _ulat;
        private string _ulng;
        private string _shoplng;
        private string _shoplat;
        private string _sitelat;
        private string _sitelng;
        private int _ordertype;
        private int _noaccess;
        private int _iscancel;
        private int _validatecode;
        private int _reveint1;
        private int _reveint2;
        private string _revevar;
        private DateTime _revedate1;
        private DateTime _revedate2;
        private int _istimelimit;

        private string _payorderid;

        private string _servename;

        /// <summary>
        /// ������������
        /// </summary>		
        public string servename
        {
            get { return _servename; }
            set { _servename = value; }
        }


        private IList<FoodInOrderInfo> _foodlist;

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        public IList<FoodInOrderInfo> foodlist
        {
            get { return _foodlist; }
            set { _foodlist = value; }
        }

        /// <summary>
        /// ֧�����
        /// </summary>		

        public string PayOrderId
        {
            get { return _payorderid; }
            set { _payorderid = value; }
        }

        /// <summary>
        /// ��ӵĻ���
        /// </summary>
        public int addpoint
        {
            set { _addpoint = value; }
            get { return _addpoint; }
        }

        /// <summary>
        /// ��ӡ��β
        /// </summary>
        public string PrintEnd
        {
            get { return strPrintEnd; }
            set { strPrintEnd = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string tempcode
        {
            set { _tempcode = value; }
            get { return _tempcode; }
        }

        /// <summary>
        /// ���б��
        /// </summary>
        public int Cityid
        {
            set { _cityid = value; }
            get { return _cityid; }
        }

        /// <summary>
        /// Ⱥ�����
        /// </summary>
        public int sid
        {
            set { _sid = value; }
            get { return _sid; }
        }

        /// <summary>
        /// ����Ա���
        /// </summary>
        public int Inve1
        {
            set { _Inve1 = value; }
            get { return _Inve1; }
        }


        /// <summary>
        /// ��Ʒ
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }

        /// <summary>
        /// ���͵���
        /// </summary>
        public int bid
        {
            set { _bid = value; }
            get { return _bid; }
        }

        /// <summary>
        /// ���ͷ�
        /// </summary>
        public decimal sendmoney
        {
            set { _sendmoney = value; }
            get { return _sendmoney; }
        }


        /// <summary>
        /// ֧����ʽ 1:֧����֧�� 3:���֧�� 2:������֧�� 4:�ռ���֧��(��������) 5:�Ƹ�ͨ
        /// </summary>
        public int PayMode
        {
            set { _paymode = value; }
            get { return _paymode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime paytime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }

        /// <summary>
        /// 0��ʾδ֧����1�ɹ���-1ʧ��
        /// </summary>
        public int paystate
        {
            set { _paystate = value; }
            get { return _paystate; }
        }

        /// <summary>
        /// ֧����֧�����
        /// </summary>
        public decimal paymoney
        {
            set { _paymoney = value; }
            get { return _paymoney; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string writer
        {
            set { _writer = value; }
            get { return _writer; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }


        /// <summary>
        /// �����ֶ�
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// �û�ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// ������ϵ��
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// ������ϵ��ʽ
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }

        /// <summary>
        /// ȡ��ʱ��
        /// </summary>
        public string SentTime
        {
            set { _senttime = value; }
            get { return _senttime; }
        }

        /// <summary>
        ///������ϵ�˵�ַ
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        /// ����״̬�� 0 ���������ӵ���;1�ѵ���; 2ȡ����; 4 ������;3 �ɹ� ;5 ȡ��,6��ʧ��
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// �¶���ʱ��
        /// </summary>
        public DateTime orderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }

        /// <summary>
        /// �ܷ��� = ���ͷ�
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }

        /// <summary>
        /// ����ͳ����Ŀ
        /// </summary>
        public int OrderCount
        {
            get { return _ordercount; }
            set { _ordercount = value; }
        }

        /// <summary>
        /// �������ͳ��  �������ֶΣ�
        /// </summary>
        public Decimal OrderTotal
        {
            get { return _ordertotal; }
            set { _ordertotal = value; }
        }

        /// <summary>
        /// ��ȡ������ʱ�䲢���޸�״̬
        /// ������ɨ���ʱ�䣨�ж϶����Ƿ�ʧЧ��
        /// </summary>
        public DateTime SetStateTime
        {
            get { return _setstatetime; }
            set { _setstatetime = value; }
        }

        /// <summary>
        /// δ��
        /// </summary>
        public int TogoID
        {
            set
            {
                _togoid = value;
            }
            get
            {
                return _togoid;
            }
        }

        /// <summary>
        ///�û�����֧���Ľ��
        /// </summary>
        public decimal Currentprice
        {
            set
            {
                _currentprice = value;
            }
            get
            {
                return _currentprice;
            }
        }

        /// <summary>
        /// δ��
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// δ��
        /// </summary>
        public int FoodCount
        {
            get { return _foodcount; }
            set { _foodcount = value; }
        }

        /// <summary>
        /// �û���
        /// </summary>
        public string CustomerName
        {
            get { return _customername; }
            set { _customername = value; }
        }
        /// <summary>
        /// �ռ��˵�ַ
        /// </summary>
        public string Oorderid
        {
            set { _oorderid = value; }
            get { return _oorderid; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public int callcount
        {
            set { _callcount = value; }
            get { return _callcount; }
        }
        /// <summary>
        /// �ռ�������
        /// </summary>
        public string callmsg
        {
            set { _callmsg = value; }
            get { return _callmsg; }
        }

        /// <summary>
        /// ������Դ��0��web ;1:wap;2:android;3:ios;
        /// </summary>		

        public int ordersource
        {
            get { return _ordersource; }
            set { _ordersource = value; }
        }
        /// <summary>
        /// �Ƿ�ӻ��֡�0��ʾû�У�����0��ʾ�˶����ӵĻ���
        /// </summary>		
        public int isaddpoint
        {
            get { return _isaddpoint; }
            set { _isaddpoint = value; }
        }
        /// <summary>
        /// δ��
        /// </summary>		
        public int sendtype
        {
            get { return _sendtype; }
            set { _sendtype = value; }
        }
        /// <summary>
        /// �ռ���γ�� 
        /// </summary>		
        public string ulat
        {
            get { return _ulat; }
            set { _ulat = value; }
        }
        /// <summary>
        /// �ռ��˾���
        /// </summary>	
        public string ulng
        {
            get { return _ulng; }
            set { _ulng = value; }
        }
        /// <summary>
        /// ������γ�� 
        /// </summary>		
        public string shoplat
        {
            get { return _shoplat; }
            set { _shoplat = value; }
        }
        /// <summary>
        /// �����˾���
        /// </summary>		
        public string shoplng
        {
            get { return _shoplng; }
            set { _shoplng = value; }
        }
        /// <summary>
        /// ���͵�γ��
        /// </summary>		
        public string sitelat
        {
            get { return _sitelat; }
            set { _sitelat = value; }
        }
        /// <summary>
        /// ���͵㾭��
        /// </summary>		
        public string sitelng
        {
            get { return _sitelng; }
            set { _sitelng = value; }
        }
        /// <summary>
        /// δ��
        /// </summary>		
        public int ordertype
        {
            get { return _ordertype; }
            set { _ordertype = value; }
        }
        /// <summary>
        /// δ�� 
        /// </summary>		
        public int noaccess
        {
            get { return _noaccess; }
            set { _noaccess = value; }
        }
        /// <summary>
        /// δ��
        /// </summary>		
        public int validateCode
        {
            get { return _validatecode; }
            set { _validatecode = value; }
        }
        /// <summary>
        /// δ��
        /// </summary>	
        public int iscancel
        {
            get { return _iscancel; }
            set { _iscancel = value; }
        }
        /// <summary>
        /// �������ͱ��
        /// </summary>		
        public int ReveInt1
        {
            get { return _reveint1; }
            set { _reveint1 = value; }
        }
        /// <summary>
        /// �Ƿ񱻽ӵ��� 0δ���ӵ� 1�ѱ��ӵ�  5����(���ڽӵ���ť�ı仯)
        /// </summary>		
        public int ReveInt2
        {
            get { return _reveint2; }
            set { _reveint2 = value; }
        }
        /// <summary>
        /// �ռ�����ϵ��ʽ
        /// </summary>		  
        public string ReveVar
        {
            get { return _revevar; }
            set { _revevar = value; }
        }
        /// <summary>
        /// δ��
        /// </summary>		
        public DateTime ReveDate1
        {
            get { return _revedate1; }
            set { _revedate1 = value; }
        }
        /// <summary>
        /// δ��
        /// </summary>		
        public DateTime ReveDate2
        {
            get { return _revedate2; }
            set { _revedate2 = value; }
        }
        /// <summary>
        /// δ��
        /// </summary>		
        public int IsTimeLimit
        {
            get { return _istimelimit; }
            set { _istimelimit = value; }
        }

        private string _delivername;
        /// <summary>
        /// ����Ա���� ����ʱ������
        /// </summary>
        public string delivername
        {
            get { return _delivername; }
            set { _delivername = value; }
        }
        /// <summary>
        /// ����Ա�绰 ����ʱ������
        /// </summary>
        public string delivertel
        {
            get;
            set;
        }


        //���������������
        private OrderDeliverInfo _deliveinfo;

        public OrderDeliverInfo DeliveInfo
        {
            set { _deliveinfo = value; }
            get { return _deliveinfo; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string CityName
        {
            get;
            set;
        }

        /// <summary>
        /// ���֧���Ľ��
        /// </summary>
        public decimal acountpay
        {
            get;
            set;
        }


        /// <summary>
        /// �̼�id
        /// </summary>
        public int shopid
        {
            get;
            set;
        }
        /// <summary>
        /// ֧������
        /// </summary>
        public string PayPassword
        {
            get;
            set;
        }


    }
}


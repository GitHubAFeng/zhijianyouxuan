using System;
using System.Collections.Generic;

namespace Hangjing.Model
{
    /// <summary>
    /// ʵ����custorder ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    public class CustorderInfo
    {
        private int _unid;
        private string _inuse;
        private DateTime _orderdatetime;
        private int _orderchecker;
        private int _orderstatus;
        private string _orderrcver;
        private string _ordercomm;
        private string _orderaddress;
        private string _addresstext;
        private string _orderattach;
        private decimal _ordersums;
        private string _sender;
        private DateTime _sendtime;
        private string _callphoneno;
        private string _p2sign;
        private decimal _sendfee;
        private int _paymode;
        private DateTime _paytime;
        private decimal _paymoney;
        private int _paystate;
        private DateTime _setstatetime;
        private int _userid;
        private int _togoid;

        //�����ֶ���Ҫ���Ӳ�ѯ��ȡ
        private int _foodcount;
        private int _ordercount;
        private decimal _ordertotal;

        private string _togoname;
        private string _customername;
        private string _togotel;

        private string _fromweb;


        private int _oldstatus;
        private int _systemuser;
        private int _Commentstate;
        private string _FoodName;
        private string _writer;
        private string _TogoAddress;
        private decimal _oldprice;

        /// <summary>
        /// ��Ʒԭ�ܽ��
        /// </summary>
        public Decimal OldPrice
        {
            get { return _oldprice; }
            set { _oldprice = value; }
        }

        /// <summary>
        /// �̼ҵ�ַ
        /// </summary>
        public string TogoAddress
        {
            set { _TogoAddress = value; }
            get { return _TogoAddress; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string writer
        {
            set { _writer = value; }
            get { return _writer; }
        }

        public string FoodName
        {
            set { _FoodName = value; }
            get { return _FoodName; }
        }


        /// <summary>
        /// ��Ʒ�ۿۣ�100��ʾû���ۿۣ���Ա�еȼ������ۿۣ�88�ۣ�����88
        /// </summary>
        public int OldStatus
        {
            set { _oldstatus = value; }
            get { return _oldstatus; }
        }

        /// <summary>
        /// �����ö����Ĺ���Ա���
        /// </summary>
        public int SystemUserId
        {
            set { _systemuser = value; }
            get { return _systemuser; }
        }

        /// <summary>
        /// ������Դ��0 ��վ��6 �ͷ�
        /// </summary>
        public string fromweb
        {
            set { _fromweb = value; }
            get { return _fromweb; }
        }

        private string _orderid;
        /// <summary>
        /// ������� �������
        /// 2010-03-15 8:47 by jijunjian
        /// </summary>
        public string orderid
        {
            set { _orderid = value; }
            get { return _orderid; }
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
        /// �������ͳ��
        /// </summary>
        public Decimal OrderTotal
        {
            get { return _ordertotal; }
            set { _ordertotal = value; }
        }

        /// <summary>
        /// �̼�����
        /// 2010-03-15 8:47 by jijunjian
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }

        /// <summary>
        /// ���������Ĳ�Ʒ��Ŀ
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
        /// �̼ҵ绰����
        /// </summary>
        public string TogoTel
        {
            get { return _togotel; }
            set { _togotel = value; }
        }

        /// <summary>
        /// ������� 
        /// </summary>
        public int Unid
        {
            set { _unid = value; }
            get { return _unid; }
        }

        /// <summary>
        /// �Ƿ�ʹ�� Y/N
        /// </summary>
        public string InUse
        {
            set { _inuse = value; }
            get { return _inuse; }
        }

        /// <summary>
        /// �µ�ʱ��
        /// </summary>
        public DateTime OrderDateTime
        {
            set { _orderdatetime = value; }
            get { return _orderdatetime; }
        }

        /// <summary>
        /// �Ƿ�ȷ���ջ���0��ʾû�У�1��ʾ��
        /// </summary>
        public int OrderChecker
        {
            set { _orderchecker = value; }
            get { return _orderchecker; }
        }

        /// <summary>
        /// ����״̬�� 1:�ȴ����;2:���ͨ��;7:�Ѿ�����;3:����ɹ�;4:����ʧ��;5:����ȡ��;6:����ʧЧ;
        /// </summary>
        public int OrderStatus
        {
            set { _orderstatus = value; }
            get { return _orderstatus; }
        }

        /// <summary>
        /// �ղ��˳ƺ�
        /// </summary>
        public string OrderRcver
        {
            set { _orderrcver = value; }
            get { return _orderrcver; }
        }

        /// <summary>
        /// �ղ�����ϵ�绰
        /// </summary>
        public string OrderComm
        {
            set { _ordercomm = value; }
            get { return _ordercomm; }
        }

        /// <summary>
        /// ǰ̨�µ�Ϊ"" ���ͷ��µ�Ϊ�ͷ��û���
        /// </summary>
        public string OrderAddress
        {
            set { _orderaddress = value; }
            get { return _orderaddress; }
        }

        /// <summary>
        /// �ջ���ַ
        /// </summary>
        public string AddressText
        {
            set { _addresstext = value; }
            get { return _addresstext; }
        }

        /// <summary>
        ///  �̼Ҿܾ���������
        /// </summary>
        public string OrderAddrEx
        {
            set;
            get;
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string OrderAttach
        {
            set { _orderattach = value; }
            get { return _orderattach; }
        }

        /// <summary>
        /// �����ܽ��:��Ʒ����+���ͷ�
        /// </summary>
        public decimal OrderSums
        {
            set { _ordersums = value; }
            get { return _ordersums; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string Sender
        {
            set { _sender = value; }
            get { return _sender; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string CallPhoneNo
        {
            set { _callphoneno = value; }
            get { return _callphoneno; }
        }

        /// <summary>
        /// �����û�openid 
        /// </summary>
        public string P2Sign
        {
            set { _p2sign = value; }
            get { return _p2sign; }
        }

        /// <summary>
        /// ���ͷ�
        /// </summary>
        public decimal SendFee
        {
            set { _sendfee = value; }
            get { return _sendfee; }
        }

        /// <summary>
        /// ֧������ ��1֧����/2����/3�˻����/4��������/5΢��֧����
        /// </summary>
        public int paymode
        {
            set { _paymode = value; }
            get { return _paymode; }
        }

        /// <summary>
        /// ֧��ʱ��
        /// </summary>
        public DateTime paytime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }

        /// <summary>
        /// ֧�����
        /// </summary>
        public decimal paymoney
        {
            set { _paymoney = value; }
            get { return _paymoney; }
        }

        /// <summary>
        /// ֧�����  0 δ֧�� 1 �ɹ�
        /// </summary>
        public int paystate
        {
            set { _paystate = value; }
            get { return _paystate; }
        }

        /// <summary>
        /// ����״̬ʱ��
        /// </summary>
        public DateTime SetStateTime
        {
            set { _setstatetime = value; }
            get { return _setstatetime; }
        }

        /// <summary>
        /// ��Ա���
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// �̼ұ��
        /// </summary>
        public int TogoId
        {
            set { _togoid = value; }
            get { return _togoid; }
        }


        public int Commentstate
        {
            set { _Commentstate = value; }
            get { return _Commentstate; }
        }

        /// <summary>
        /// �̼��Ƿ�ȷ��(0δȷ�ϣ�1�Ѿ�ȷ�ϣ�2�ܾ�).,�����̼Ҹ�������ֶ�
        /// </summary>
        public int IsShopSet
        {
            set;
            get;
        }

        private int _deliversiteid;
        /// <summary>
        /// ��ӡ״̬��999��ʾû�д�ӡ���̼ҵģ� 0���ȴ���ӡ;1:�Ѵ�ӡ;2����ʧ��;3:�����ѷ���;-1:IP��ַ������;-2:�ؼ�����Ϊ�ջ�����ʽ����;-3:�ͻ����벻��ȷ;-4:��ȫУ���벻��ȷ;-5:����ʱ��ʧЧ;
        /// </summary>
        public int deliversiteid
        {
            get { return _deliversiteid; }
            set { _deliversiteid = value; }
        }

        private int _deliverheaderid;
        /// <summary>
        /// Ⱥ����
        /// </summary>
        public int deliverheaderid
        {
            get { return _deliverheaderid; }
            set { _deliverheaderid = value; }
        }

        private int _deliverid;
        /// <summary>
        /// ����Ա���
        /// </summary>
        public int deliverid
        {
            get { return _deliverid; }
            set { _deliverid = value; }
        }

        /// <summary>
        /// ����Ա�绰
        /// </summary>
        public string delivertel
        {
            get;
            set;
        }

        private int _sendstate;
        /// <summary>
        /// ����״̬��0��δ����,1ȡ���У�2�������У�3��������ɣ� 4������ʧ��
        /// </summary>
        public int sendstate
        {
            get { return _sendstate; }
            set { _sendstate = value; }
        }

        private int _reveint1;
        /// <summary>
        /// �Ͳ�����
        /// </summary>
        public int ReveInt1
        {
            get { return _reveint1; }
            set { _reveint1 = value; }
        }

        private int _reveint2;
        /// <summary>
        /// �Ͳ����ͣ�0������1��ʳ
        /// </summary>
        public int ReveInt2
        {
            get { return _reveint2; }
            set { _reveint2 = value; }
        }

        private string _revevar1;
        /// <summary>
        ///  ���ͷ�ʽ�������̼����õ�����  ���ͷ�ʽ,0��ʾͳһ���ͣ�1��ʾ�̼�����
        /// </summary>
        public string ReveVar1
        {
            get { return _revevar1; }
            set { _revevar1 = value; }
        }

        private string _revevar2;
        /// <summary>
        /// �̼��û���γ��
        /// </summary>
        public string ReveVar2
        {
            get { return _revevar2; }
            set { _revevar2 = value; }
        }

        private DateTime _revedate1;
        /// <summary>
        /// �̼ҽ��ջ��߾ܾ�ʱ��
        /// </summary>
        public DateTime ReveDate1
        {
            get { return _revedate1; }
            set { _revedate1 = value; }
        }

        private DateTime _revedate2;
        /// <summary>
        /// �Զ����ȷ���Ⱥʱ��ʱ��
        /// </summary>
        public DateTime ReveDate2
        {
            get { return _revedate2; }
            set { _revedate2 = value; }
        }


        //���������������
        private OrderDeliverInfo _deliveinfo;
        public OrderDeliverInfo DeliveInfo
        {
            set { _deliveinfo = value; }
            get { return _deliveinfo; }
        }

        /// <summary>
        /// ��ʾ�˶�����ʿҪ���̼ҵĽ��
        /// </summary>
        public decimal shopdiscountmoney
        {
            set;
            get;
        }

        /// <summary>
        /// �Ż�ȯ���
        /// </summary>
        public decimal cardpay
        {
            set;
            get;
        }

        /// <summary>
        /// ��ʱ��ţ�����δ��¼��ʹ�ã�ͨ����guid
        /// </summary>
        public string tempcode
        {
            set;
            get;
        }

        /// <summary>
        /// �����
        /// </summary>
        public decimal Packagefee
        {
            set;
            get;
        }

        /// <summary>
        /// ���б��
        /// </summary>
        public int cityid
        {
            get;
            set;
        }

        /// <summary>
        /// ����΢��ɨ��֧������
        /// </summary>
        public string PayOrderId
        {
            set;
            get;
        }
        /// <summary>
        /// �̼�ͼƬ
        /// </summary>
        public string TogoPic
        {
            set;
            get;
        }


        /// <summary>
        /// �����Żݽ��
        /// </summary>
        public decimal promotionmoney
        {
            set;
            get;
        }
        /// <summary>
        /// �̼����õ�������ʱ
        /// </summary>
        public int SentTime
        {
            set;
            get;
        }
        /// <summary>
        /// ����Աȡ��ʱ��
        /// </summary>
        public DateTime picktime
        {
            set;
            get;
        }
        /// <summary>
        /// ����Ա�Ͳ����ʱ�䣨�ʹ�ʱ�䣩
        /// </summary>
        public DateTime comtime
        {
            set;
            get;
        }

        /// <summary>
        /// Ҫ֧���Ľ�� = �ܽ��-�Ż�ȯ��-�����Ż�
        /// </summary>
        public decimal needpaymoney
        {
            set;
            get;
        }

        /// <summary>
        /// ��������Ĵ�����Ŀ
        /// </summary>
        public IList<OrderPromotionInfo> Promotions
        {
            set;
            get;
        }
        /// <summary>
        /// ��Ʒ�б�
        /// </summary>
        public IList<FoodlistInfo> Foodlist
        {
            set;
            get;
        }
        /// <summary>
        /// �̼Ҵ���״̬��ȡ���������룩0δ���� 1ͬ��2�ܾ�
        /// </summary>
        public int shopCancel
        {
            set;
            get;
        }
        /// <summary>
        /// �̼Ҿܾ�ȡ����������
        /// </summary>
        public string Cancelreason
        {
            set;
            get;
        }
        /// <summary>
        /// �ߵ�״̬��0 δ���� 1�Ѵ���
        /// </summary>
        public int hurhav
        {
            set;
            get;
        }
        /// <summary>
        /// �˵��������
        /// </summary>
        public int iscount
        {
            set;
            get;
        }

    }
}


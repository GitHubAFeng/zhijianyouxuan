/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : eaddress.cs
 * Function : ��ַʵ����
 * Created by jijunjian at 2010-7-26 16:53:41.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// ʵ����EAddress ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ������Դ
        /// </summary>
        public string Ordersource
        {
            set;
            get;
        }

        /// <summary>
        /// �ֻ��ύ�����ͷ�
        /// </summary>
        public decimal senmoney
        {
            get { return _senmoney; }
            set { _senmoney = value; }
        }

        /// <summary>
        /// zfy ǰ̨�µ�Ϊ�գ��ͷ��µ�Ϊ�ͷ�������
        /// </summary>
        public string kefuid
        {
            set { _kefuid = value; }
            get { return _kefuid; }
        }

        /// <summary>
        /// �����ַ������lat
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }

        /// <summary>
        /// �����ַ������lng
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
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
        /// ���ֱ��� 
        /// </summary>
        public decimal pointrat
        {
            get { return _pointrat; }
            set { _pointrat = value; }
        }

        private decimal _foodmoneydiscount;
        /// <summary>
        /// ��Ʒ�ۿ� 
        /// </summary>
        public decimal foodmoneydiscount
        {
            get { return _foodmoneydiscount; }
            set { _foodmoneydiscount = value; }
        }

        /// <summary>
        /// �Ͳ�ʱ��
        /// </summary>
        public string sendtime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
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
        /// 1��ʾ�˵�ַ��Ĭ�ϵ�ַ.0��ʾ��.
        /// </summary>
        public int Pri
        {
            set { _pri = value; }
            get { return _pri; }
        }
        /// <summary>
        /// �ղ�ʱ��
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// ���ƺ�
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// �û��ֻ�
        /// </summary>
        public string Mobilephone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// �ռ���
        /// </summary>
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }

        /// <summary>
        /// д��¥����
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
        /// �Ͳ�ʱ���
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

        /// <summary>
        /// ������Դ��0 ��վ��6 �ͷ�
        /// </summary>
        public string fromweb
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
        /// ֧������
        /// </summary>
        public string PayPassword
        {
            set;
            get;
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set;
            get;
        }

        /// <summary>
        /// �û�γ��
        /// </summary>
        public string ulat
        {
            set;
            get;
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public string ulng
        {
            set;
            get;
        }

        /// <summary>
        /// android ,iso�ύ����(�̼��б�)
        /// </summary>
        public IList<Hangjing.Model.ETogoShoppingCart> shoplist
        {
            set;
            get;
        }

        /// <summary>
        /// �Ż�ȯjson(�����ж��.����Ŀֻ����һ��)
        /// </summary>
        public string shopcardjson
        {
            set;
            get;
        }

        /// <summary>
        /// �Ƿ�ʹ���Ż�ȯ,0��ʾδ��,1��ʾ����
        /// </summary>
        public int isuercard
        {
            set;
            get;
        }

        /// <summary>
        /// ��Ա�ǳ�
        /// </summary>
        public string CustomerName
        {
            set;
            get;
        }

        /// <summary>
        /// �û����̼����ꡣ
        /// </summary>
        public string latlng
        {
            set;
            get;
        }

        /// <summary>
        /// �̼�ԭ��
        /// </summary>
        public decimal foodprice
        {
            set;
            get;
        }

        /// <summary>
        /// ���
        /// </summary>
        public msgpacketInfo redpackage
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


    }
}


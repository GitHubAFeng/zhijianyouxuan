using System;
namespace Hangjing.Model
{
    /// <summary>
    /// ʵ����EUser ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class ECustomerInfo
    {
        /// <summary>
        /// �������Ķ�ά��ͼƬ��ַ
        /// </summary>
        public string qrcodeurl
        {
            set;
            get;
        }



        private int _dataid;
        private string _name;
        private string _truename;
        private string _sex;
        private string _tell;
        private string _phone;
        private string _qq;
        private string _msn;
        private DateTime _regtime;
        private int _point;
        private string _picture;
        private string _state;
        private string _email;
        private string _password;
        private int _isavtivate;
        private string _avtivatecode;
        private string _website;
        private string _rid;
        private decimal _usermoney;
        private int _phoneActivate;


        private string _PayPassword;
        private string _PayPWDQuestion;
        private string _PayPWDAnswer;
        private string _lastordertime;

        private string _openid;
        private string _wtype;

        /// <summary>
        /// ��һ�ζ���ʱ��
        /// </summary>
        public string lastordertime
        {
            set { _lastordertime = value; }
            get { return _lastordertime; }
        }

        /// <summary>
        /// ֧������
        /// </summary>
        public string PayPassword
        {
            set { _PayPassword = value; }
            get { return _PayPassword; }
        }

        /// <summary>
        /// ΢��openid
        /// </summary>
        public string PayPWDQuestion
        {
            set { _PayPWDQuestion = value; }
            get { return _PayPWDQuestion; }
        }

        /// <summary>
        /// �Ƿ�Ϊ�����̣���ֵ10Ԫ���Ϊ����-�� 0�����ǣ�1����
        /// </summary>
        public string PayPWDAnswer
        {
            set { _PayPWDAnswer = value; }
            get { return _PayPWDAnswer; }
        }

        /// <summary>
        /// �ֻ���֤��Ĭ��Ϊ0����֤�ɹ�Ϊ1��
        /// </summary>
        public int PhoneActivate
        {
            get { return _phoneActivate; }
            set { _phoneActivate = value; }
        }
        /// <summary>
        /// �û��˻����
        /// </summary>
        public decimal Usermoney
        {
            get { return _usermoney; }
            set { _usermoney = value; }
        }

        /// <summary>
        ///�û����
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// �ǳ�
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// �Ա�
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// �ֻ�
        /// </summary>
        public string Tell
        {
            set { _tell = value; }
            get { return _tell; }
        }
        /// <summary>
        /// �绰
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// �����û�������
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// ע��ʱ��
        /// </summary>
        public DateTime RegTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        /// <summary>
        ///����
        /// </summary>
        public int Point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// ͷ��
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// �û�״̬-�� 0��������1��������
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// EMAIL
        /// </summary>
        public string EMAIL
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        /// is activate or not -1 = not activate / 1 = activate
        /// </summary>
        public int IsActivate
        {
            set
            {
                _isavtivate = value;
            }
            get
            {
                return _isavtivate;
            }
        }

        /// <summary>
        /// activate code 
        /// </summary>
        public string ActivateCode
        {
            set
            {
                _avtivatecode = value;
            }
            get
            {
                return _avtivatecode;
            }
        }

        /// <summary>
        /// �����˻����
        /// </summary>
        public decimal GroupID
        {
            set;
            get;
        }

        /// <summary>
        /// ���������루�����Ƿ����֣�
        /// </summary>
        public decimal distributemoney
        {
            set;
            get;
        }

        /// <summary>
        /// ��Դ����Ӧ OrderSource 
        /// </summary>
        public string WebSite
        {
            set
            {
                _website = value;
            }
            get
            {
                return _website;
            }
        }

        /// <summary>
        /// �Ƽ��˵ı��
        /// </summary>
        public string RID
        {
            set
            {
                _rid = value;
            }
            get
            {
                return _rid;
            }
        }

        /// <summary>
        /// �����ʺŵ�¼�������õ��ı��
        /// </summary>
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// ��¼���ĸ�������վ��qq ,���ˡ�����
        /// </summary>
        public string wtype
        {
            set { _wtype = value; }
            get { return _wtype; }
        }

        /// <summary>
        /// ��Ӧucenter�еı��
        /// </summary>
        public int UC_ID
        {
            set;
            get;
        }

        /// <summary>
        /// ����
        /// </summary>
        public int num { get; set; }

        /// <summary>
        /// �ܽ��
        /// </summary>
        public decimal orderSums { get; set; }
    }
}


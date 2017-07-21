using System;
namespace Hangjing.Model
{
    /// <summary>
    /// 实体类EUser 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ECustomerInfo
    {
        /// <summary>
        /// 带参数的二维码图片地址
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
        /// 上一次订餐时间
        /// </summary>
        public string lastordertime
        {
            set { _lastordertime = value; }
            get { return _lastordertime; }
        }

        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPassword
        {
            set { _PayPassword = value; }
            get { return _PayPassword; }
        }

        /// <summary>
        /// 微信openid
        /// </summary>
        public string PayPWDQuestion
        {
            set { _PayPWDQuestion = value; }
            get { return _PayPWDQuestion; }
        }

        /// <summary>
        /// 是否为分销商（充值10元后成为）：-》 0：不是，1：是
        /// </summary>
        public string PayPWDAnswer
        {
            set { _PayPWDAnswer = value; }
            get { return _PayPWDAnswer; }
        }

        /// <summary>
        /// 手机验证（默认为0，验证成功为1）
        /// </summary>
        public int PhoneActivate
        {
            get { return _phoneActivate; }
            set { _phoneActivate = value; }
        }
        /// <summary>
        /// 用户账户余额
        /// </summary>
        public decimal Usermoney
        {
            get { return _usermoney; }
            set { _usermoney = value; }
        }

        /// <summary>
        ///用户编号
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 真实名称
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Tell
        {
            set { _tell = value; }
            get { return _tell; }
        }
        /// <summary>
        /// 电话
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
        /// 保存用户的生日
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        /// <summary>
        ///积分
        /// </summary>
        public int Point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// 用户状态-》 0：正常，1：黑名单
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
        /// 密码
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
        /// 分销账户余额
        /// </summary>
        public decimal GroupID
        {
            set;
            get;
        }

        /// <summary>
        /// 分销总收入（不管是否提现）
        /// </summary>
        public decimal distributemoney
        {
            set;
            get;
        }

        /// <summary>
        /// 来源，对应 OrderSource 
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
        /// 推荐人的编号
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
        /// 第三帐号登录过程中用到的编号
        /// </summary>
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 记录是哪个三方网站（qq ,人人。。）
        /// </summary>
        public string wtype
        {
            set { _wtype = value; }
            get { return _wtype; }
        }

        /// <summary>
        /// 对应ucenter中的编号
        /// </summary>
        public int UC_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int num { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal orderSums { get; set; }
    }
}


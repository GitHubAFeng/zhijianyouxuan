// HJEncryption.cs :打印机相关类的定义页面
// CopyRight (c) 2010 HangJing Teconology. All Rights Reserved.
// wlf@ihangjing.com
// 2009-03-25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Hangjing.Model;

namespace HJPrinter
{

    /// <summary>
    ///HJEncryption 的摘要说明
    /// </summary>
    public class HJEncryption
    {
        #region  加密解密

        /// <summary>
        /// 移位操作，压缩。把ACII编码编程7位编码。ACII码有效
        /// </summary>
        /// <param name="dd">要移位的数据</param>
        /// <returns>移位后的数据</returns>
        static private byte[] MovBitL(string dd)
        {
            int le = dd.Length;
            int n = le / 8;
            byte[] cc = new byte[le - n];
            byte a;
            int k = 0;
            if (le < 1)
            {
                return null;//0A0104125
            }

            for (int i = 0; i < le; i++)
            {
                a = 0;
                for (int j = 2; j < 9 && i < le; j++)
                {
                    if (i == le - 1)
                    {
                        cc[i - k] = (byte)(dd[i] << j - 1);
                        i++;
                        break;
                    }
                    a = (byte)((byte)dd[i + 1] >> 8 - j);
                    cc[i - k] = (byte)((dd[i] << j - 1) | a);
                    i++;
                }
                k++;
            }
            return cc;

        }

        /// <summary>
        /// 右移位函数，解码
        /// </summary>
        /// <param name="dd">传入的字符</param>
        /// <returns>返回解码后的字符串</returns>
        static private string MovBitR(byte[] dd)
        {
            int le = dd.Length;
            byte[] cc = new byte[2 * le];
            string ff = "";
            byte a = 0;
            byte b;
            int k = 0;
            for (int i = 0; i < le; i++)
            {
                b = 1;
                a = 0;

                for (int j = 2; j < 9 && i < le; j++)
                {
                    cc[i + k] = (byte)((dd[i] >> j - 1) | a);
                    a = (byte)((dd[i] & b) << 8 - j);
                    if (j == 8)
                    {

                        cc[i + k + 1] = a;
                        break;
                    }
                    i++;
                    b <<= 1;
                    b |= 1;
                }
                k++;
            }

            for (int j = 0; cc[j] != 0; j++)
            {
                ff += (char)cc[j];
            }
            byte[] oo = MovBitL(ff);
            if (oo[oo.Length - 1] != dd[le - 1])
            {
                ff += (char)a;
            }
            return ff;
        }
        /// <summary>
        /// 取反操作
        /// </summary>
        /// <param name="dd">传入、传出数据</param>
        static private void Invert(byte[] dd)
        {
            int le = dd.Length;
            for (int i = 0; i < le; i++)
            {
                dd[i] ^= 255;
            }
        }

        /// <summary>
        /// 更换位，用后面字节的偶数位，替换前面的奇数位。如果只有奇数个字节，最后的字节里面的所奇数位放到后面的偶数位即可；
        /// </summary>
        /// <param name="dd">传入、传出的值</param>
        static private void ReplaceBitWithAft(byte[] dd)
        {
            int le = dd.Length;
            byte a;
            a = (byte)(dd[0] & 85);
            for (int i = 0; i < le - 1; i++)
            {
                dd[i] = (byte)(dd[i] & 170);
                dd[i] |= (byte)(dd[i + 1] & 85);
            }
            dd[le - 1] = (byte)(dd[le - 1] & 170);
            dd[le - 1] |= a;

        }

        /// <summary>
        /// 更换位，用后面字节的偶数位，替换前面的奇数位。如果只有奇数个字节，最后的字节里面的所奇数位放到后面的偶数位即可；
        /// </summary>
        /// <param name="dd">传入、传出的值</param>
        static private void ReplaceBitWithBef(byte[] dd)
        {
            int le = dd.Length;
            byte a;
            a = (byte)(dd[le - 1] & 85);
            for (int i = le - 1; i > 0; i--)
            {
                dd[i] = (byte)(dd[i] & 170);
                dd[i] |= (byte)(dd[i - 1] & 85);
            }
            dd[0] = (byte)(dd[0] & 170);
            dd[0] |= a;

        }

        /// <summary>
        /// 合并两个字符串，两个字符串的长度差不能超过8倍，可以为八倍
        /// </summary>
        /// <param name="dd">原数据</param>
        /// <param name="pk">密钥</param>
        /// <returns>合并后的数据</returns>
        static private byte[] OrgByte(byte[] dd, byte[] pk)
        {
            int lep = pk.Length;
            int led = dd.Length;
            int j = 0;
            int l = 0;
            int nl;
            int pp;
            byte[] re;
            if (lep > led)
            {
                nl = lep;
            }
            else
            {
                nl = led;
            }
            re = new byte[nl + 2];
            pp = 0;
            for (int i = 0; i < nl; i++)
            {
                re[i] = (byte)(pk[j] ^ dd[l]);
                if (j >= lep - 1)
                {
                    if (pp == 0)
                    {
                        pp = j + 1;
                        re[nl] = 0;
                    }
                    j = -1;


                }
                if (l >= led - 1)
                {
                    if (pp == 0)
                    {
                        pp = l + 1;
                        re[nl] = 1;
                    }
                    l = -1;


                }
                j++;
                l++;
            }
            re[nl + 1] = (byte)(pp);
            return re;
        }
        /// <summary>
        /// 根据KEY计算院址的BYTE类型
        /// </summary>
        /// <param name="dd">传入经过KEY等到的BYTE</param>
        /// <param name="pk">KEY</param>
        /// <returns>取出KEY的BYTE[]类型</returns>
        static private byte[] DecoByte(byte[] dd, byte[] pk)
        {
            int led = dd.Length;
            int lek = pk.Length;
            int lo = dd[led - 1];
            int n = 0;
            int j = 0;
            if (dd[led - 2] == 0)
            {
                lo = led - 2;
            }
            byte[] re = new byte[lo];
            for (int i = 0; i < lo; i++)
            {
                re[i] = (byte)(dd[i] ^ pk[n]);
                if (n == pk.Length - 1)
                {
                    n = -1;
                }
                n++;
            }
            return re;

        }

        /// <summary>
        /// Byte[]用十六进制串标识
        /// </summary>
        /// <param name="dd">传入的BYTE[]</param>
        /// <returns>十六进制串</returns>
        static private string ByteToOxStr(byte[] dd)
        {
            string ff = "";
            int le = dd.Length;
            byte a;
            for (int i = 0; i < le; i++)
            {
                a = (byte)(dd[i] >> 4);
                if (a <= 9)
                {
                    ff += (char)(a + 48);
                }
                else
                {
                    a -= 10;
                    ff += (char)(a + 65);
                }
                a = (byte)(dd[i] & 15);
                if (a <= 9)
                {
                    ff += (char)(a + 48);
                }
                else
                {
                    a -= 10;
                    ff += (char)(a + 65);
                }
            }
            return ff;
        }
        /// <summary>
        /// 十六进制字符串转BYTE[]类型
        /// </summary>
        /// <param name="dd">传入的十六进制字符串</param>
        /// <returns>返回BYTE[]类型</returns>
        static private byte[] OxStrToByte(string dd)
        {
            int le = dd.Length;
            int n = le / 2;
            byte[] re = new byte[n];
            int j = 0;
            for (int i = 0; i < n; i++)
            {
                if (dd[j] < 59)
                {
                    re[i] = (byte)((dd[j] - 48) << 4);
                }
                else
                {
                    re[i] = (byte)(((dd[j] - 65) + 10) << 4);
                }
                j++;
                if (dd[j] < 59)
                {
                    re[i] |= (byte)((dd[j] - 48));
                }
                else
                {
                    re[i] |= (byte)(((dd[j] - 65) + 10));
                }
                j++;
            }
            return re;
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="dd">要加密的数据</param>
        /// <param name="key">加密使用的KEY</param>
        /// <returns>加密后的数据</returns>
        static public string Encryption(string dd, string key)
        {
            int nLed = dd.Length;
            int nLek = key.Length;
            byte[] bd = new byte[nLed];
            byte[] bk = new byte[nLek];
            bd = MovBitL(dd);//原始数据压缩
           // string op = ByteToOxStr(bd);
            bk = MovBitL(key);//KEY压缩
           // op = ByteToOxStr(bk);


            Invert(bd);//原始数据取反
           // op = ByteToOxStr(bd);
            Invert(bk);//KEY取反
           // op = ByteToOxStr(bk);

            ReplaceBitWithAft(bd);//原始数据换位
           // op = ByteToOxStr(bd);
            ReplaceBitWithAft(bk);//KEY换位
           // op = ByteToOxStr(bk);

            byte[] end = OrgByte(bd, bk);


            return ByteToOxStr(end);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="dd">要解密的源码</param>
        /// <param name="key">解密使用的KEY</param>
        /// <returns>解密后的数据</returns>
        static public string Decryption(string dd, string key)
        {
            if (key == "" || key == null || dd == null || dd == "")
            {
                return "";
            }
            int nLed = dd.Length;
            int nLek = key.Length;
            byte[] bk = MovBitL(key);
            byte[] bd = OxStrToByte(dd);

            Invert(bk);//KEY取反
            ReplaceBitWithAft(bk);//KEY换位

            byte[] bde = DecoByte(bd, bk);
            ReplaceBitWithBef(bde);
            Invert(bde);
            return MovBitR(bde);
        }

        #endregion
    }



    /// <summary>
    /// 餐品类
    /// </summary>
    public class HJGoods
    {
        #region 餐品类

        string strGName = "";
        string strNotes = "";//备注
        decimal nPrice = 0.0m;//
        int nCount = 0;
        decimal nDiscount = 10;


        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name
        {
            get { return strGName; }
            set { strGName = value; }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price
        {
            get { return nPrice; }
            set { nPrice = value; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get { return nCount; }
            set { nCount = value; }
        }

        /// <summary>
        /// 折扣前价格
        /// </summary>
        public decimal Discount
        {
            get { return nDiscount; }
            set { nDiscount = value; }
        }
        /// <summary>
        /// 备注信息，如加辣等
        /// </summary>
        public string Notes
        {
            get { return strNotes; }
            set { strNotes = value; }
        }

        #endregion 餐品类
    }



    /// <summary>
    /// 商家类
    /// </summary>
    public class HJCustomer
    {
        #region  商家类

        string strCustName = "";            //商家名称
        string strCustID = "";              //商家编号
        string strCustPhone = "";           //商家联系电话
        string strOrdTime = "";             //订单时间
        string strPassKey = "";             //加密密钥
        string strUserName = "";            //订单用户名
        string strUserAddress = "";         //订单用户地址
        string strUserPhone = "";           //订单用户电话
        string strPrintEnd = "";            //打印结尾
        string strOSID = "";                //订单编号
        string strGifts = "";               //礼品
        int nPrintTimes = 1;
        decimal nAllPrice = 0;
        int nDiscount = 0;                  //折扣
        bool bOS = false;                   //点餐，订位标志，false点餐 true订位
        bool bEd = false;                   //false 为修改，true为修改
        bool bEnd = false;                  //订单结束标志，false未结束，true结束  (pagesize*pangecount > 总数  修改为true)
        ArrayList OrdCont = new ArrayList();//订单内容
        int nCount = 0;                     //餐品参数
        int orderstate = 1;
        string _remark;
        string _paymodel;
        string _paystate;
        string _eattype;
        string _sendtime;
        decimal _sendmoney;
        string _togoname;
        int _senttime;

        /// <summary>
        /// 送达时间
        /// </summary>
        public int senttime
        {
            get { return _senttime; }
            set { _senttime = value; }
        }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string TogoName
        {
            get { return _togoname; }
            set { _togoname = value; }
        }


        /// <summary>
        /// 商品总价格
        /// </summary>
        public decimal sendmoney
        {
            get { return _sendmoney; }
            set { _sendmoney = value; }
        }

        /// <summary>
        /// 送餐时间 
        /// </summary>
        public string sendtime
        {
            get { return _sendtime; }
            set { _sendtime = value; }
        }

        private string _tabClass;
        /// <summary>
        /// 表示是哪个包厢
        /// </summary>
        public string tabClass
        {
            get { return _tabClass; }
            set { _tabClass = value; }
        }
        private string _tabData;
        /// <summary>
        /// 表示是选择了那个桌子
        /// </summary>
        public string tabData
        {
            get { return _tabData; }
            set { _tabData = value; }
        }
  
        /// <summary>
        /// 支付方式
        /// </summary>
        public string paymodel
        {
            get { return _paymodel; }
            set { _paymodel = value; }
        }

        /// <summary>
        /// 支付状态
        /// </summary>
        public string paystate
        {
            get { return _paystate; }
            set { _paystate = value; }
        }

        /// <summary>
        /// 消费方式：1外卖	2.预定.3到店 
        /// </summary>
        public string eattype
        {
            get { return _eattype; }
            set { _eattype = value; }
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string CustName
        {
            get { return strCustName; }
            set { strCustName = value; }
        }

        /// <summary>
        /// 商家编号
        /// </summary>
        public string CustID
        {
            get { return strCustID; }
            set { strCustID = value; }
        }

        /// <summary>
        /// 商家联系电话
        /// </summary>
        public string CustPhone
        {
            get { return strCustPhone; }
            set { strCustPhone = value; }
        }

        /// <summary>
        /// 订单下发的时间
        /// </summary>
        public string OrdTime
        {
            get { return strOrdTime; }
            set { strOrdTime = value; }
        }

        /// <summary>
        /// 订单用户名称
        /// </summary>
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }

        /// <summary>
        /// 订单用户电话
        /// </summary>
        public string UserPhone
        {
            get { return strUserPhone; }
            set { strUserPhone = value; }
        }

        /// <summary>
        /// 订单用户地址，送货地址
        /// </summary>
        public string UserAddress
        {
            get { return strUserAddress; }
            set { strUserAddress = value; }
        }

        /// <summary>
        /// 商家加密密钥
        /// </summary>
        public string PassKey
        {
            get { return strPassKey; }
            set { strPassKey = value; }
        }

        /// <summary>
        /// 商家送的礼品
        /// </summary>
        public string Gifts
        {
            get { return strGifts; }
            set { strGifts = value; }
        }

        /// <summary>
        /// 打印联数
        /// </summary>
        public int PrintTimes
        {
            get { return nPrintTimes; }
            set { nPrintTimes = value; }
        }

        /// <summary>
        /// 打印结尾
        /// </summary>
        public string PrintEnd
        {
            get { return strPrintEnd; }
            set { strPrintEnd = value; }
        }

        /// <summary>
        /// 是点餐还是订位，false点餐 true订位
        /// </summary>
        public bool OS
        {
            get { return bOS; }
            set { bOS = value; }
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OSID
        {
            get { return strOSID; }
            set { strOSID = value; }
        }

        /// <summary>
        /// 商家信息修改标志，false 为修改，true为修改
        /// </summary>
        public bool Edit
        {
            get { return bEd; }
            set { bEd = value; }
        }

        /// <summary>
        /// 获取当前队列中商品的个数
        /// </summary>
        public int ArrayCount
        {
            get { return OrdCont.Count; }
        }

        /// <summary>
        /// 商品总价格
        /// </summary>
        public decimal AllPrice
        {
            get { return nAllPrice; }
            set { nAllPrice = value; }
        }

        /// <summary>
        /// 折扣
        /// </summary>
        public int Discount
        {
            get { return nDiscount; }
            set { nDiscount = value; }
        }

        /// <summary>
        /// //订单结束标志，false未结束，true结束  (pagesize*pangecount > 总数  修改为true)
        /// </summary>
        public bool End
        {
            get { return bEnd; }
            set { bEnd = value; }
        }

        /// <summary>
        /// 订单中商品总数，
        /// </summary>
        public int Count
        {
            get { return nCount; }
            set { nCount = value; }
        }

        /// <summary>
        /// 订单状态. 为1 正常订单.为5表示取消的订单
        /// </summary>
        public int Orderstate
        {
            get { return orderstate; }
            set { orderstate = value; }
        }

        /// <summary>
        /// 增加物品到订单内容中去
        /// </summary>
        /// <param name="name">物品名称</param>
        /// <param name="notes">备注，例如加辣</param>
        /// <param name="count">物品数量</param>
        /// <param name="price">物品价格</param>
        /// <param name="Discount">物品折扣</param>
        /// <returns>返回索引号</returns>
        public int AddOrd(string name, string notes, int count, decimal price, decimal Discount)
        {
            HJGoods goods = new HJGoods();
            goods.Name = name;
            goods.Count = count;
            goods.Price = price;
            goods.Discount = Discount;
            goods.Notes = notes;
            return OrdCont.Add((object)goods);
        }


        /// <summary>
        /// 增加物品到订单内容中去
        /// </summary>
        /// <param name="goods">商品</param>
        /// <returns>返回索引号</returns>
        public int AddOrd(HJGoods goods)
        {
            return OrdCont.Add((object)goods);
        }

        /// <summary>
        /// 获取订单中的第一个商品
        /// 商品每次拿出来之后便被删除。这个类似队列操作，先进先出
        /// </summary>
        /// <returns>订单中的第一个商品，如果没有则返回null</returns>
        public HJGoods GetGoods()
        {
            if (this.ArrayCount > 0)
            {
                HJGoods goods = (HJGoods)OrdCont[0];
                OrdCont.RemoveAt(0);
                return goods;
            }
            else
            {
                return null;
            }
        }

        #endregion  商家类
    }

    /// <summary>
    /// 报表类
    /// </summary>
    public class HJReport
    {
        #region 报表类

        private string _orderid;
        private decimal _price;
        private DateTime _ordertime;
        private int _ordercount;
        private decimal _total;

        private bool _isend;

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID
        {
            get { return _orderid;}
            set { _orderid = value; }
        }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime
        {
            get { return _ordertime; }
            set { _ordertime = value; }
        }

        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderCount
        {
            get { return _ordercount; }
            set { _ordercount = value; }
        }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }

        /// <summary>
        /// 是否是最后一页
        /// </summary>
        public bool IsEnd
        {
            get { return _isend; }
            set { _isend = value; }
        }

        #endregion
    }

}



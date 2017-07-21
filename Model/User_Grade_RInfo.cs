using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 会员等级
    /// </summary>
    public class User_Grade_RInfo
    {
        private int _pid;
        private int _gid;
        private decimal _sendmoneydiscount;
        private decimal _foodmoneydiscount;
        private decimal _pointrat;
        private int _sendprior;
        private int _reveint;
        private string _revevar;
        private decimal _reveflat;

      

        /// <summary>
        /// 
        /// </summary>
        public int pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 等级编号，对应VipGrade.dataid
        /// </summary>
        public int gid
        {
            set { _gid = value; }
            get { return _gid; }
        }
        /// <summary>
        /// 服务器折扣
        /// </summary>
        public decimal sendmoneyDiscount
        {
            set { _sendmoneydiscount = value; }
            get { return _sendmoneydiscount; }
        }
        /// <summary>
        /// 菜品折扣:10表示不打折，9.0表示9折
        /// </summary>
        public decimal foodmoneyDiscount
        {
            set { _foodmoneydiscount = value; }
            get { return _foodmoneydiscount; }
        }
        /// <summary>
        /// 积分倍数
        /// </summary>
        public decimal pointrat
        {
            set { _pointrat = value; }
            get { return _pointrat; }
        }
        /// <summary>
        /// 是否优先配送0表示不是，1表示是
        /// </summary>
        public int sendprior
        {
            set { _sendprior = value; }
            get { return _sendprior; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public int ReveInt
        {
            set { _reveint = value; }
            get { return _reveint; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public string ReveVar
        {
            set { _revevar = value; }
            get { return _revevar; }
        }
        /// <summary>
        /// 未用
        /// </summary>
        public decimal ReveFlat
        {
            set { _reveflat = value; }
            get { return _reveflat; }
        }
    }
}

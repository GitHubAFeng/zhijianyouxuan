using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

namespace Hangjing.WebCommon
{
    /// <summary>
    /// 用户相关工具类
    /// </summary>
    public class UserTool
    {
        ECustomer dal = new ECustomer();
        ECustomerInfo user = null;
        public UserTool(int userid)
        {
            user = dal.GetModel(userid);
        }


        /// <summary>
        /// 验证余额是否充足 
        /// </summary>
        /// <param name="orderprice"></param>
        /// <returns></returns>
        public resultinfo checkPayment(decimal orderprice,string payppwd)
        {
            resultinfo rs = checkPayPassWord(payppwd);
            if (rs.status != 1)
            {
                return rs;
            }

            rs = checkUserMoney(orderprice);
           
            return rs;
        }

        /// <summary>
        /// 验证余额是否充足 
        /// </summary>
        /// <param name="orderprice"></param>
        /// <returns></returns>
        public resultinfo checkUserMoney(decimal orderprice)
        {
            resultinfo rs = new resultinfo();
            rs.status = 1;
            rs.message = "";
            if (orderprice > user.Usermoney)
            {
                rs.status = 0;
                rs.message = "您的账户余额不足，请充值或者选择其他支付方式";
            }

            return rs;
        }

        /// <summary>
        /// 验证支付密码是否正确 
        /// </summary>
        /// <param name="payppwd">支付密码（明文）</param>
        /// <returns></returns>
        public resultinfo checkPayPassWord(string payppwd)
        {
            resultinfo rs = new resultinfo();
            rs.status = 1;
            rs.message = "";
            if (WebHelper.GetMd5(payppwd.Trim()) != user.PayPassword)
            {
                rs.status = 0;
                rs.message = "支付密码错误，请重新输入";
            }

            return rs;
        }
    }
}

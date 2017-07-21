using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using Hangjing.SQLServerDAL;

namespace Com.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        #region 字段
        private static string partner = "";
        private static string key = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        #endregion

        static Config()
        {

            //支付宝信息
            Hangjing.Model.acountInfo mymodel = CacheHelper.getOnlinePayType(1);
            if (mymodel == null)
            {
                return;
            }
            
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            //2088111089044406
            partner = mymodel.Ali_Partner;

            //交易安全检验码，由数字和字母组成的32位字符串
            //如果签名方式设置为“MD5”时，请设置该参数
            key = mymodel.Ali_Key;

            //商户的私钥
            //如果签名方式设置为“0001”时，请设置该参数
            private_key = @"MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAN5XP8uHzg4iKWhs0JWMvg2SzKHa0Jg22lwLCZ4VsZL+8mbmA+zEyQNa60WWBvOZ65EGRPtnFkLP8cCPQYdVHmQNPfizLF6loTs1S+WKN+xsTBrvFscRkFabPzf+/9oNyi66fOZUFgcX1+ZMrzpl+9IdDxQvzL3sV9SADIIIygtDAgMBAAECgYBuJcUp/G5dXBktbXLsE5x3tvj/WhqqvcnxfVpXYaHmE71csqjRHDAFJH6Xq5poBiHIZ9W2wjwp/0Bhx9aLx+RUekQIc/OPH8wexZGmijW7VkGZJHvaJoaoqxlbx/vwJ8ZEmtWcViYEu4tvXpTGOOoUzDeJrd0mTPXIIYHeJXw+WQJBAO74OW0Tde+LeaUCgJIm+XxVmSqBB1+qyNPJ4bjnUhkVmXKUUYvO8lhRGPp9sFEUR4R0z8RUgV7hS/aQRzCdhp0CQQDuL6XeYwDMAtPqD6j8SlbM0f8dLhiuZbS/lMJnsBYRd16TDt56Dm3Duliz7RN6oCNDAlFO+ZJdCHuQZn5sYkNfAkAkXlavCQUr3bg3qrfShmf1yjkzRMvQfXdu9AyMTXgrJSRjUbtPYcF0O3Nnu/U3gbSYrgZoxMujmvoqni6XcYHpAkBgZQIw5UpeRkqzMFFIgWFtlRM1IQG2Gs0yt6aRxg64VOH+jAb3yL0deF4Lu0el2gdLSDXVy2Uzp4oyX3iMrpvrAkBJoXqq9Y3LZpl1myhW561GLR5kqmVPcPOlfTu1D1cAqr5couzxQm8aO8F1vBsUP8sHqbPwN+dxPMjiCUgLDHYD";

            //支付宝的公钥
            //如果签名方式设置为“0001”时，请设置该参数
            public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDeVz/Lh84OIilobNCVjL4Nksyh2tCYNtpcCwmeFbGS/vJm5gPsxMkDWutFlgbzmeuRBkT7ZxZCz/HAj0GHVR5kDT34syxepaE7NUvlijfsbEwa7xbHEZBWmz83/v/aDcouunzmVBYHF9fmTK86ZfvSHQ8UL8y97FfUgAyCCMoLQwIDAQAB";

            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

            //字符编码格式 目前支持 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：0001(RSA)、MD5
            sign_type = "MD5";
            //无线的产品中，签名方式为rsa时，sign_type需赋值为0001而不是RSA
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设交易安全校验码
        /// </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        #endregion
    }
}
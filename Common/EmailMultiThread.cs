using System;
using System.Reflection;
using System.Text;
using System.Data;
using System.Threading;
// EmailMultiThread.cs :EmailMultiThread多线程发送邮件 
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 20010-06-18

namespace Hangjing.Common
{
    /// <summary>
    /// 多线程发送邮件 在需要发送大量的邮件时使用
    /// </summary>
    public class EmailMultiThread
    {
        protected static ISmtpMail ESM;

        private string _username = "";

        private string _email = "";

        private string _title = "";

        private string _body = "";

        private string _errorinfo;

        private bool _issuccess = false;

        protected static EmailConfigInfo _emailinfo = new EmailConfig().GetEmailConfigModel();

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string UserName
        {
            get { return _username; }
        }

        /// <summary>
        /// 收件人邮箱地址
        /// </summary>
        public string Email
        {
            get { return _email; }
        }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body
        {
            get { return _body; }
        }

        public string ErrorInfo
        {
            get { return _errorinfo; }
            set { _errorinfo = value; }
        }

        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool IsSuccess
        {
            get { return _issuccess; }
            set { _issuccess = value; }
        }

        public EmailMultiThread(string UserName, string Email, string Title, string Body)
        {
            _username = UserName;
            _email = Email;
            _title = Title;
            _body = Body;
            ESM = new Hangjing.Common.EMail();
        }

        public void Send()
        {
            lock (_emailinfo)//放置发送中途配置信息被修改
            {
                ESM.MailDomainPort = Convert.ToInt32(_emailinfo.Port);
                ESM.AddRecipient(this.Email);
                ESM.RecipientName = this.UserName;//设定收件人姓名

                ESM.From = _emailinfo.SysEmail;
                ESM.FromName = "食欲网";
                ESM.Html = true;
                ESM.Subject = this.Title;
                ESM.Body = "<pre style=\"width:100%;word-wrap:break-word\">" + this.Body.ToString() + "</pre>";
                ESM.MailDomain = _emailinfo.Smtp;
                ESM.MailServerUserName = _emailinfo.UserName;
                ESM.MailServerPassWord = _emailinfo.PassWord;

                //开始发送
                this.IsSuccess = ESM.Send();
            }
            Thread.CurrentThread.Abort();
        }
    }
}

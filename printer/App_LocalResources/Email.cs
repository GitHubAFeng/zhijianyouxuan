using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;

using Hangjing.Common;

/// <summary>
///Email 的摘要说明
/// </summary>
public class Email
{
    private string _from;
    private string _to;
    private string _title;
    private string _body;
    private string _host;
    private string _port;
    private bool _ssl;
    private string _username;
    private string _password;

    public Email(string to, string title, string body)
    {
        _to = to;
        _title = title;
        _body = body;
        EmailConfigInfo config = new EmailConfig().GetEmailConfigModel();
        _from = "" + config.SysEmail + "";
        _host = config.Smtp;
        _port = config.Port;
        _ssl = false;
        _username = config.UserName;
        _password = config.PassWord;
    }

    /// <summary>
    /// 发送EMAIL
    /// </summary>
    /// <returns>发送是否成功，返回 1 则发送成功，否则失败</returns>
    public string Send()
    {
        try
        {
            //邮件对象
            MailMessage emailMessage;

            //smtp客户端对象

            SmtpClient client;

            // 初始化邮件对象
            emailMessage = new MailMessage(_from, _to, _title, _body);
            emailMessage.IsBodyHtml = true;
            emailMessage.SubjectEncoding = System.Text.Encoding.Default;
            emailMessage.BodyEncoding = System.Text.Encoding.Default;
            //加入
            emailMessage.Headers.Add("X-Priority", "3");
            emailMessage.Headers.Add("X-MSMail-Priority", "Normal");
            emailMessage.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");
            emailMessage.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
            emailMessage.Headers.Add("ReturnReceipt", "1");

            //邮件发送客户端
            client = new SmtpClient();

            //邮件服务器及帐户信息
            client.Host = _host;

            if (!string.IsNullOrEmpty(_port))
                client.Port = int.Parse(_port);
            if (!_ssl)
                client.EnableSsl = _ssl;
            System.Net.NetworkCredential Credential = new System.Net.NetworkCredential();

            Credential.UserName = _username;
            Credential.Password = _password;

            client.Credentials = Credential;


            client.Send(emailMessage);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        return "1";

    }
    public string Send(string to, string title, string body)
    {
        _to = to;
        _title = title;
        _body = body;
        return Send();
    }
}

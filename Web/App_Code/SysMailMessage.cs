using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;

using Hangjing.Common;

public class SysMailMessage : ISmtpMail
{
    private string _subject;
    private string _body;
    private string _from;
    private string _fromName;
    private string _recipientName;
    private string _mailDomain;
    private int _mailserverport;
    private string _username;
    private string _password;
    private bool _html;
    private string _recipient;

    public SysMailMessage(string to, string title, string body)
    {
        EmailConfigInfo config = new EmailConfig().GetEmailConfigModel();

        _from = config.SysEmail;
        
        _mailDomain = config.Smtp;
        _mailserverport = Convert.ToInt32( config.Port);
        //_ssl = config.SMTPSsl;
        _username = config.UserName;
        _password = config.PassWord;

        /*
        _mailDomain = "smtp.163.com";
        _mailserverport =25;
        //_ssl = config.SMTPSsl;
        _username = "ihangjing2011@163.com";
        _password = "!@#$%^";
        */

        _subject = title;
        _body = body;
        _recipient = to;
        _recipientName = to;

    }

    // Methods
    public bool AddRecipient(params string[] username)
    {
        this._recipient = username[0].Trim();
        return true;
    }

    private string Base64Encode(string str)
    {
        return Convert.ToBase64String(Encoding.Default.GetBytes(str));
    }

    public bool Send()
    {
        MailMessage message = new MailMessage();
        Encoding displayNameEncoding = Encoding.GetEncoding("utf-8");
        message.From = new MailAddress(this.From, SectionProxyData.GetSetValue(2), displayNameEncoding);
        message.To.Add(this._recipient);
        message.Subject =  this.Subject;
       // message.
        message.IsBodyHtml = true;
        message.Body = this.Body;
        message.Priority = MailPriority.Normal;
        message.BodyEncoding = Encoding.GetEncoding("utf-8");
        SmtpClient client = new SmtpClient();
        client.Host = this.MailDomain;
        client.Port = this.MailDomainPort;
        client.Credentials = new System.Net.NetworkCredential(this.MailServerUserName, this.MailServerPassWord);
        if (this.MailDomainPort != 25)
        {
            client.EnableSsl = true;
        }
        try
        {
            client.Send(message);
        }
        catch (SmtpException exception)
        {
            string text1 = exception.Message;
            return false;
        }
        return true;
    }

    // Properties
    public string Body
    {
        get
        {
            return this._body;
        }
        set
        {
            this._body = value;
        }
    }

    public string From
    {
        get
        {
            return this._from;
        }
        set
        {
            this._from = value;
        }
    }

    public string FromName
    {
        get
        {
            return this._fromName;
        }
        set
        {
            this._fromName = value;
        }
    }

    public bool Html
    {
        get
        {
            return this._html;
        }
        set
        {
            this._html = value;
        }
    }

    public string MailDomain
    {
        get
        {
            return this._mailDomain;
        }
        set
        {
            this._mailDomain = value;
        }
    }

    public int MailDomainPort
    {
        get
        {
            return this._mailserverport;
        }
        set
        {
            this._mailserverport = value;
        }
    }

    public string MailServerPassWord
    {
        get
        {
            return this._password;
        }
        set
        {
            this._password = value;
        }
    }

    public string MailServerUserName
    {
        get
        {
            return this._username;
        }
        set
        {
            if (value.Trim() != "")
            {
                this._username = value.Trim();
            }
            else
            {
                this._username = "";
            }
        }
    }

    public string RecipientName
    {
        get
        {
            return this._recipientName;
        }
        set
        {
            this._recipientName = value;
        }
    }

    public string Subject
    {
        get
        {
            return this._subject;
        }
        set
        {
            this._subject = value;
        }
    }

}
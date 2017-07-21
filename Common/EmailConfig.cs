using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

// EmailConfig.cs :EmailConfig 
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 20010-05-07
namespace Hangjing.Common
{
    public class EmailConfig
    {
        private string DirName = (HjNetHelper.SiteRootPath + @"\" + HjNetHelper.ConfDirName);

        public EmailConfig()
        {
            if (!Directory.Exists(this.DirName))
            {
                Directory.CreateDirectory(this.DirName);
            }
        }

        /// <summary>
        /// 获取论坛配置信息
        /// </summary>
        /// <returns></returns>
        public EmailConfigInfo GetEmailConfigModel()
        {
            string filePath = this.DirName + @"\Email.config";
            System.Diagnostics.Debug.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                return null;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            EmailConfigInfo model = new EmailConfigInfo();

            XmlNode node = xmlDoc.ChildNodes[1];
            model.Smtp = node.ChildNodes[0].InnerText.ToString();
            model.Port = node.ChildNodes[1].InnerText.ToString();
            model.SysEmail = node.ChildNodes[2].InnerText.ToString();
            model.UserName = node.ChildNodes[3].InnerText.ToString();
            model.PassWord = node.ChildNodes[4].InnerText.Trim();
            model.RegContent = node.ChildNodes[5].InnerText.Trim();
            model.ErrorContent = node.ChildNodes[6].InnerText.Trim();
            return model;
        }

        /// <summary>
        /// 配置论坛配置文件
        /// </summary>
        /// <param name="model"></param>
        public bool SetEmailConfig(EmailConfigInfo model)
        {
            string filePath = this.DirName + @"\Email.config";
            try
            {
                if (!File.Exists(filePath))
                {
                    XmlTextWriter xmlTW = new XmlTextWriter(filePath, Encoding.UTF8);
                    xmlTW.Formatting = Formatting.Indented;
                    xmlTW.WriteStartDocument();
                    xmlTW.WriteStartElement("EmailConfigInfo");

                    xmlTW.WriteElementString("Smtp", model.Smtp.ToString());
                    xmlTW.WriteElementString("Port", model.Port.ToString());
                    xmlTW.WriteElementString("SysEmail", model.SysEmail.ToString());
                    xmlTW.WriteElementString("UserName", model.UserName.ToString());
                    xmlTW.WriteElementString("PassWord", model.PassWord.ToString());
                    //写html代码时需要使用CData输出
                    xmlTW.WriteStartElement("RegContent", null);
                    xmlTW.WriteCData(model.RegContent.ToString());
                    xmlTW.WriteEndElement();

                    xmlTW.WriteStartElement("ErrorContent", null);
                    xmlTW.WriteCData(model.ErrorContent.ToString());
                    xmlTW.WriteEndElement();

                    xmlTW.WriteEndElement();
                    xmlTW.WriteEndDocument();
                    xmlTW.Flush();
                    xmlTW.Close();
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);
                    XmlNode node = xmlDoc.ChildNodes[1];
                    node.ChildNodes[0].InnerText = model.Smtp.ToString();
                    node.ChildNodes[1].InnerText = model.Port.ToString();
                    node.ChildNodes[2].InnerText = model.SysEmail.ToString();
                    node.ChildNodes[3].InnerText = model.UserName.ToString();
                    node.ChildNodes[4].InnerText = model.PassWord.ToString();
                    node.ChildNodes[5].InnerText = model.RegContent.ToString();
                    node.ChildNodes[6].InnerText = model.ErrorContent.ToString();
                    xmlDoc.Save(filePath);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

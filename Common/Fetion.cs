using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

// Fetion.cs :Fetion  飞信的配置信息管理类
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 20010-06-19

namespace Hangjing.Common
{
    public class Fetion
    {
        private string DirName = (HjNetHelper.SiteRootPath + @"\" + HjNetHelper.ConfDirName);

        public Fetion()
        {
            if (!Directory.Exists(this.DirName))
            {
                Directory.CreateDirectory(this.DirName);
            }
        }

        /// <summary>
        /// 获取飞信配置信息
        /// </summary>
        /// <returns></returns>
        public FetionInfo GetFetionModel()
        {
            string filePath = this.DirName + @"\Fetion.config";
            if (!File.Exists(filePath))
            {
                return null;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            FetionInfo model = new FetionInfo();
            XmlNode node = xmlDoc.ChildNodes[1];
            model.UserName = node.ChildNodes[0].InnerText.ToString();
            model.PassWord = node.ChildNodes[1].InnerText.Trim();
            return model;
        }

        /// <summary>
        /// 配置飞信配置文件
        /// </summary>
        /// <param name="model"></param>
        public bool SetFetionConfig(FetionInfo model)
        {
            string filePath = this.DirName + @"\Fetion.config";
            try
            {
                if (!File.Exists(filePath))
                {
                    XmlTextWriter xmlTW = new XmlTextWriter(filePath, Encoding.UTF8);
                    xmlTW.Formatting = Formatting.Indented;
                    xmlTW.WriteStartDocument();
                    xmlTW.WriteStartElement("FetionInfo");
                    xmlTW.WriteElementString("UserName", model.UserName.ToString());
                    xmlTW.WriteElementString("PassWord", model.PassWord.ToString());

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
                    node.ChildNodes[0].InnerText = model.UserName.ToString();
                    node.ChildNodes[1].InnerText = model.PassWord.ToString();
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

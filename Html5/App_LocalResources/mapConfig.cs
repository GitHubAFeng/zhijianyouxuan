using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using Hangjing.Common;

public class mapConfig
{
    private string DirName = (HjNetHelper.SiteRootPath + @"\" + HjNetHelper.ConfDirName);

    public mapConfig()
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
    public mapConfigInfo GetEmailConfigModel()
    {
        string filePath = this.DirName + @"\map.config";
        if (!File.Exists(filePath))
        {
            return null;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);
        mapConfigInfo model = new mapConfigInfo();

        XmlNode node = xmlDoc.ChildNodes[1];
        model.Radius = Convert.ToDecimal( node.ChildNodes[0].InnerText.ToString());

        return model;
    }

    /// <summary>
    /// 配置论坛配置文件
    /// </summary>
    /// <param name="model"></param>
    public bool SetEmailConfig(mapConfigInfo model)
    {
        string filePath = this.DirName + @"\map.config";
        try
        {
            if (!File.Exists(filePath))
            {
                XmlTextWriter xmlTW = new XmlTextWriter(filePath, Encoding.UTF8);
                xmlTW.Formatting = Formatting.Indented;
                xmlTW.WriteStartDocument();
                xmlTW.WriteStartElement("MapInfo");
                xmlTW.WriteElementString("Radius", model.Radius.ToString());
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
                node.ChildNodes[0].InnerText = model.Radius+"";
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


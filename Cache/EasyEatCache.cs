using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.IO;

using XmlElement = System.Xml.XmlElement;
using Hangjing.Config;

namespace Hangjing.Cache
{
    /// <summary>
    /// 缓存 管理类 
    /// 可以根据配置选择使用不同的缓存方法比如 memcache 
    /// </summary>
    public class EasyEatCache
    {
        private static XmlElement objectXmlMap;
        private static ICacheStrategy cs;
        private static volatile EasyEatCache instance = null;
        private static object lockHelper = new object();
        private static XmlDocument rootXml = new XmlDocument();

        /// <summary>
        /// 是否使用memcached
        /// </summary>
        private static bool applyMemCached = false;

        /// <summary>
        /// 是否使用redis
        /// </summary>
        private static bool applyRedis = false;

        private static ICacheStrategy cachedStrategy;

        /// <summary>
        /// 构造函数
        /// </summary>
        private EasyEatCache()
        {

            //如果是提供memcached则使用
            //如果未提供则使用默认的缓存

            if (MemCachedConfigs.GetConfig() != null && MemCachedConfigs.GetConfig().ApplyMemCached)
            {
                applyMemCached = true;
            }

            if (applyMemCached)
            {
                //TEST
                AppLog.AppLog.Debug("applyMemCached");
                //cs = new MemCachedStrategy();
            }
            else
            {
                AppLog.AppLog.Debug("!applyMemCached");

                cs = new DefaultCacheStrategy();

                if (rootXml.HasChildNodes)
                {
                    rootXml.RemoveAll();
                }

                objectXmlMap = rootXml.CreateElement("Cache");

                //建立内部XML文档.
                rootXml.AppendChild(objectXmlMap);
            }
        }

        /*
        单体模式（Singleton）是经常为了保证应用程序操作某一全局对象，让其保持一致而产生的对象，例如对文件的读写操作的锁定，数据库操作的时候的事务回滚，
        还有任务管理器操作，都是一单体模式读取的。 
        创建一个单体模式类，必须符合三个条件： 
        1：私有构造函数（防止其他对象创建实例）； 
        2：一个单体类型的私有变量； 
        3：静态全局获取接口 
        */

        /// <summary>
        /// 单体模式返回当前类的实例
        /// </summary>
        /// <returns></returns>
        public static EasyEatCache GetCacheService()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new EasyEatCache();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 在XML映射文档中的指定路径,加入当前对象信息
        /// </summary>
        /// <param name="xpath">分级对象的路径 </param>
        /// <param name="o">被缓存的对象</param>
        public virtual void AddObject(string xpath, object o)
        {
            lock (lockHelper)
            {
                if (applyMemCached || applyRedis)
                {
                    //向缓存加入新的对象
                    cs.AddObject(xpath, o);
                }
                else
                {
                    //当缓存到期时间为0或负值,则不再放入缓存
                    if (cs.TimeOut <= 0) return;

                    //整理XPATH表达式信息
                    string newXpath = PrepareXpath(xpath);
                    int separator = newXpath.LastIndexOf("/");
                    //找到相关的组名
                    string group = newXpath.Substring(0, separator);
                    //找到相关的对象
                    string element = newXpath.Substring(separator + 1);

                    XmlNode groupNode = objectXmlMap.SelectSingleNode(group);

                    //建立对象的唯一键值, 用以映射XML和缓存对象的键
                    string objectId = "";

                    XmlNode node = objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node != null)
                    {
                        objectId = node.Attributes["objectId"].Value;
                    }

                    if (objectId == "")
                    {
                        groupNode = CreateNode(group);
                        objectId = Guid.NewGuid().ToString();
                        //建立新元素和一个属性 for this perticular object
                        XmlElement objectElement = objectXmlMap.OwnerDocument.CreateElement(element);
                        XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                        objectAttribute.Value = objectId;
                        objectElement.Attributes.Append(objectAttribute);
                        //为XML文档建立新元素
                        groupNode.AppendChild(objectElement);
                    }
                    else
                    {
                        //建立新元素和一个属性 for this perticular object
                        XmlElement objectElement = objectXmlMap.OwnerDocument.CreateElement(element);
                        XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                        objectAttribute.Value = objectId;
                        objectElement.Attributes.Append(objectAttribute);
                        //为XML文档建立新元素
                        groupNode.ReplaceChild(objectElement, node);
                    }

                    //向缓存加入新的对象 根据不用的缓存策略调用其对应实现的加入缓存的方法AddObject
                    cs.AddObject(objectId, o);
                }
            }
        }

        /// <summary>
        /// 在XML映射文档中的指定路径,加入当前对象信息
        /// </summary>
        /// <param name="xpath">分级对象的路径 </param>
        /// <param name="o">被缓存的对象</param>
        /// <param name="o">到期时间,单位:秒</param>
        public virtual void AddObject(string xpath, object o, int expire)
        {
            lock (lockHelper)
            {
                if (applyMemCached || applyRedis)
                {
                    //向缓存加入新的对象
                    cs.AddObject(xpath, o, expire);
                }
                else
                {
                    //当缓存到期时间为0或负值,则不再放入缓存
                    if (cs.TimeOut <= 0) return;

                    //整理XPATH表达式信息
                    string newXpath = PrepareXpath(xpath);
                    int separator = newXpath.LastIndexOf("/");
                    //找到相关的组名
                    string group = newXpath.Substring(0, separator);
                    //找到相关的对象
                    string element = newXpath.Substring(separator + 1);

                    XmlNode groupNode = objectXmlMap.SelectSingleNode(group);

                    //建立对象的唯一键值, 用以映射XML和缓存对象的键
                    string objectId = "";

                    XmlNode node = objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node != null)
                    {
                        objectId = node.Attributes["objectId"].Value;
                    }

                    if (objectId == "")
                    {
                        groupNode = CreateNode(group);
                        objectId = Guid.NewGuid().ToString();
                        //建立新元素和一个属性 for this perticular object
                        XmlElement objectElement = objectXmlMap.OwnerDocument.CreateElement(element);
                        XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                        objectAttribute.Value = objectId;
                        objectElement.Attributes.Append(objectAttribute);
                        //为XML文档建立新元素
                        groupNode.AppendChild(objectElement);
                    }
                    else
                    {
                        //建立新元素和一个属性 for this perticular object
                        XmlElement objectElement = objectXmlMap.OwnerDocument.CreateElement(element);
                        XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                        objectAttribute.Value = objectId;
                        objectElement.Attributes.Append(objectAttribute);
                        //为XML文档建立新元素
                        groupNode.ReplaceChild(objectElement, node);
                    }

                    //向缓存加入新的对象
                    cs.AddObject(objectId, o, expire);
                }
            }
        }

        /// <summary>
        /// 在XML映射文档中的指定路径,加入当前对象信息
        /// </summary>
        /// <param name="xpath">分级对象的路径 </param>
        /// <param name="o">被缓存的对象</param>
        public virtual void AddObject(string xpath, object o, string[] files)
        {
            xpath = xpath.Replace(" ", "_SPACE_");    //如果xpath中出现空格，则将空格替换为_SPACE_
            lock (lockHelper)
            {
                if (applyMemCached || applyRedis)
                {
                    //向缓存加入新的对象
                    cs.AddObject(xpath, o);
                }
                else
                {
                    //当缓存到期时间为0或负值,则不再放入缓存
                    if (cs.TimeOut <= 0) return;

                    //整理XPATH表达式信息
                    string newXpath = PrepareXpath(xpath);
                    int separator = newXpath.LastIndexOf("/");
                    //找到相关的组名
                    string group = newXpath.Substring(0, separator);
                    //找到相关的对象
                    string element = newXpath.Substring(separator + 1);

                    XmlNode groupNode = objectXmlMap.SelectSingleNode(group);
                    //建立对象的唯一键值, 用以映射XML和缓存对象的键
                    string objectId = "";

                    XmlNode node = objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node != null)
                    {
                        objectId = node.Attributes["objectId"].Value;
                    }
                    if (objectId == "")
                    {
                        groupNode = CreateNode(group);
                        objectId = Guid.NewGuid().ToString();
                        //建立新元素和一个属性 for this perticular object
                        XmlElement objectElement = objectXmlMap.OwnerDocument.CreateElement(element);
                        XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                        objectAttribute.Value = objectId;
                        objectElement.Attributes.Append(objectAttribute);
                        //为XML文档建立新元素
                        groupNode.AppendChild(objectElement);
                    }
                    else
                    {
                        //建立新元素和一个属性 for this perticular object
                        XmlElement objectElement = objectXmlMap.OwnerDocument.CreateElement(element);
                        XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                        objectAttribute.Value = objectId;
                        objectElement.Attributes.Append(objectAttribute);
                        //为XML文档建立新元素
                        groupNode.ReplaceChild(objectElement, node);
                    }

                    //向缓存加入新的对象
                    cs.AddObjectWithFileChange(objectId, o, files);
                }
            }
        }

        /// <summary>
        /// 取得指定XML路径下的数据项
        /// </summary>
        /// <param name="xpath">分级对象的路径</param>
        /// <returns></returns>
        public virtual object RetrieveObject(string xpath)
        {
            try
            {
                if (applyMemCached || applyRedis)
                {
                    //向缓存加入新的对象
                    return cs.RetrieveObject(xpath);
                }
                else
                {
                    XmlNode node = objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node != null)
                        return cs.RetrieveObject(node.Attributes["objectId"].Value);

                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 通过指定的路径删除缓存中的对象
        /// </summary>
        /// <param name="xpath">分级对象的路径</param>
        public virtual void RemoveObject(string xpath)
        {
            lock (lockHelper)
            {
                try
                {
                    if (applyMemCached || applyRedis)
                    {
                        //移除相应的缓存项
                        cs.RemoveObject(xpath);
                    }
                    else
                    {
                        XmlNode result = objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                        //检查路径是否指向一个组或一个被缓存的实例元素
                        if (result.HasChildNodes)
                        {
                            //删除所有对象和子结点的信息
                            XmlNodeList objects = result.SelectNodes("*[@objectId]");
                            string objectId = "";
                            foreach (XmlNode node in objects)
                            {
                                objectId = node.Attributes["objectId"].Value;
                                node.ParentNode.RemoveChild(node);
                                //删除对象
                                cs.RemoveObject(objectId);
                            }
                        }
                        else
                        {
                            //删除元素结点和相关的对象
                            string objectId = result.Attributes["objectId"].Value;
                            result.ParentNode.RemoveChild(result);
                            cs.RemoveObject(objectId);
                        }
                    }

                }
                catch//如出错误表明当前路径不存在
                { }
            }
        }

        /// <summary>
        /// 对象树形分级对象节点
        /// </summary>
        /// <param name="xpath">分级路径 location</param>
        /// <returns></returns>
        private XmlNode CreateNode(string xpath)
        {
            lock (lockHelper)
            {
                string[] xpathArray = xpath.Split('/');
                string root = "";
                XmlNode parentNode = objectXmlMap;
                //建立相关节点
                for (int i = 1; i < xpathArray.Length; i++)
                {
                    XmlNode node = objectXmlMap.SelectSingleNode(root + "/" + xpathArray[i]);
                    // 如果当前路径不存在则建立,否则设置当前路径到它的子路径上
                    if (node == null)
                    {
                        XmlElement newElement = objectXmlMap.OwnerDocument.CreateElement(xpathArray[i]);
                        parentNode.AppendChild(newElement);
                    }
                    //设置低一级的路径
                    root = root + "/" + xpathArray[i];
                    parentNode = objectXmlMap.SelectSingleNode(root);
                }
                return parentNode;
            }
        }

        /// <summary>
        /// 整理 xpath 确保 '/'被删除 is removed
        /// </summary>
        /// <param name="xpath">分级地址</param>
        /// <returns></returns>
        private string PrepareXpath(string xpath)
        {
            lock (lockHelper)
            {
                string[] xpathArray = xpath.Split('/');
                xpath = "/Cache";
                foreach (string s in xpathArray)
                {
                    if (s != "")
                    {
                        xpath = xpath + "/" + s;
                    }
                }
                return xpath;
            }
        }

        /// <summary>
        /// 加载指定的缓存策略
        /// </summary>
        /// <param name="ics"></param>
        public void LoadCacheStrategy(ICacheStrategy ics)
        {
            lock (lockHelper)
            {
                cs = ics;
            }
        }

        /// <summary>
        /// 加载默认的缓存策略
        /// </summary>
        public void LoadDefaultCacheStrategy()
        {
            lock (lockHelper)
            {
                ////当使用MemCached或redis时
                //if (applyMemCached || applyRedis)
                //{
                //    cs = cachedStrategy;
                //}
                //else
                {
                    cs = new DefaultCacheStrategy();
                }
            }
        }

        /// <summary>
        /// 清空的有缓存数据, 注: 考虑效率问题，建议仅在需要时（如后台管理）使用.
        /// </summary>
        public void FlushAll()
        {
            cs.FlushAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Cache
{
    /*
    (Strategy)策略者模式（行为型）
    在软件构建过程中，某些对象使用的算法可能多种多样，经常改变，如果将这些算法都编码到对象中，将会使对象变得异常复杂；而且有时候支持不使用的算法也是一个性能负担。
    如何在运行时根据需要透明地更改对象的算法,将算法与对象本身解耦，从而避免上述问题
    
    Strategy模式的几个要点
    Strategy及其子类为组件提供了一系列可重用的算法，从而可以使得类型在运行时方便地根据需要在各个算法之间进行切换。所谓封装算法，支持算法的变化。
    Strategy模式提供了用条件判断语句以外的另一种选择，消除条件判断语句，就是在解耦合。含有许多条件判断语句的代码通常都需要Strategy模式。
    与State类似，如果Strategy对象没有实例变量，那么各个上下文可以共享同一个Strategy对象，从而节省对象开销。
    */
    /// <summary>
    /// 缓存类接口
    /// </summary>
    public interface ICacheStrategy
    {
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">缓存键</param>
        /// <param name="o">缓存对象</param>
        void AddObject(string objId, object o);

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">缓存键</param>
        /// <param name="o">缓存对象</param>
        /// <param name="expire">到期时间,单位:秒</param>
        void AddObject(string objId, object o, int expire);

        /// <summary>
        /// 添加指定ID的对象(关联指定文件组)
        /// </summary>
        /// <param name="objId">缓存键</param>
        /// <param name="o">缓存对象</param>
        /// <param name="files">关联的文件名</param>
        void AddObjectWithFileChange(string objId, object o, string[] files);

        /// <summary>
        /// 添加指定ID的对象(关联指定键值组)
        /// </summary>
        /// <param name="objId">缓存键</param>
        /// <param name="o">缓存对象</param>
        /// <param name="dependKey">依赖键</param>
        void AddObjectWithDepend(string objId, object o, string[] dependKey);

        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId">缓存键</param>
        void RemoveObject(string objId);

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId">缓存键</param>
        /// <returns></returns>
        object RetrieveObject(string objId);

        /// <summary>
        /// 到期时间,单位：秒
        /// </summary>
        int TimeOut { set; get; }

        /// <summary>
        /// 清空的有缓存数据
        /// </summary>
        void FlushAll();       
    }
}

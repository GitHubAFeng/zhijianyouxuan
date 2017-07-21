/* ***********************************************
* author  :  jijunjian
 * email   :  jijunjian@ihangjing.com 
 * function:  处理没有处理的订单
 * history :  CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved. 
 * time    :  2010-5-19 18:15:06
 * ***********************************************/
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
///CheckOrder 的摘要说明
/// </summary>
public class CheckOrder
{
    public CheckOrder()
    {
        int result = 0; //一个标志位，如果是0表示程序没有出错，如果是1表明有错误发生
        Cell cell = new Cell();

        CellProd prod = new CellProd(cell);
        CellCons cons = new CellCons(cell, cell.queueList.Count);
        Thread producer = new Thread(new ThreadStart(prod.ThreadRun));  //该进程调用start后，就执行ThreadRun函数。
        Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));

        //生产者线程和消费者线程都已经被创建，但是没有开始执行 
        try
        {
            producer.Name = "producer";
            producer.Start();       //开始producer进程。
            consumer.Name = "consumer";
            consumer.Start();      //开始consumer进程。
            //producer.Join();       //让主进程等待，直到producer进程结束。
            //consumer.Join();        //让主进程等待，直到consumer进程结束。

            //等producer和consumer进程完成后，才向下面做。
            //调试信息：
            Console.WriteLine("Finish");

        }
        catch (ThreadStateException e)
        {
            //当线程因为所处状态的原因而不能执行被请求的操作
            Console.WriteLine(e);
            result = 1;
        }
        catch (ThreadInterruptedException e)
        {
            //当线程在等待状态的时候中止
            Console.WriteLine(e);
            result = 1;
        }
        //尽管Main()函数没有返回值，但下面这条语句可以向父进程返回执行结果
        Environment.ExitCode = result;
        Console.WriteLine(result.ToString());
        Console.ReadLine();
    }
}

public class Cell
{
    public bool readerFlag = false;
    public Queue<CustorderInfo> queueList = new Queue<CustorderInfo>();
    const int gate = 500;

    public void ReadFromCell()
    {
        lock (this)                  // Lock关键字保证了什么，请大家看前面对lock的介绍
        {
            if (!readerFlag)        //如果现在不可读取
            {
                try
                {
                    //等待WriteToCell方法中调用Monitor.Pulse()方法
                    Monitor.Wait(this);
                }
                catch (SynchronizationLockException e)
                {
                    Console.WriteLine(e);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                }
            }
            if (queueList.Count != 0)
            {
                CustorderInfo model = queueList.Peek();
                Custorder dal = new Custorder();
                for (int i = 0; i < queueList.Count; i++)
                {
                    if (dal.GetModel(model.Unid).OrderStatus == 6)
                    {
                        queueList.Dequeue();
                    }
                    if (DateTime.Compare(model.SetStateTime.AddSeconds(gate), DateTime.Now) <= 0 && (dal.GetModel(model.Unid).OrderStatus == 1))
                    {
                        dal.UpdataState(model.Unid, 6);
                        queueList.Dequeue();
                    }
                }
            }
            readerFlag = false;                             //重置readerFlag标志，表示消费行为已经完成
            Monitor.Pulse(this);                            //通知WriteToCell()方法（该方法在另外一个线程中执行，等待中）          
        }
    }

    public void WriteToCell(IList<CustorderInfo> orderlist)
    {
        lock (this)
        {
            if (readerFlag)
            {
                try
                {
                    Monitor.Wait(this);
                }
                catch (SynchronizationLockException e)
                {
                    //当同步方法（指Monitor类除Enter之外的方法）在非同步的代码区被调用
                    Console.WriteLine(e);
                }
                catch (ThreadInterruptedException e)
                {
                    //当线程在等待状态的时候中止 
                    Console.WriteLine(e);
                }
            }
            if (orderlist != null)
            {
                for (int j = 0; j < orderlist.Count; j++)
                {
                    queueList.Enqueue(orderlist[j]);
                }
            }
            readerFlag = true;
            Monitor.Pulse(this); //通知另外一个线程中正在等待的ReadFromCell()
        }
    }
}

/// <summary>
/// 生产者
/// </summary>
public class CellProd
{
    Cell cell; // 被操作的Cell对象
    Custorder dal = new Custorder();

    public CellProd(Cell box)
    {
        cell = box;
    }

    public void ThreadRun()
    {
        while (true)
        {
            IList<CustorderInfo> orderList = dal.GetNewOrder();
            cell.WriteToCell(orderList);
            Thread.Sleep(30000);
        }
    }
}

/// <summary>
/// 消费者
/// </summary>
public class CellCons
{
    Cell cell;
    public int quantity = 1;

    public CellCons(Cell box, int request)
    {
        cell = box;
        quantity = cell.queueList.Count;
    }

    public void ThreadRun()
    {
        while (true)
        {
            cell.ReadFromCell();
            Thread.Sleep(30000);
        }
    }
}

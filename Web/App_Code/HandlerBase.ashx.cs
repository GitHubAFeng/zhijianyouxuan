using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Reflection;
using System.Collections.Specialized;

/// <summary>
///HandlerBase 的摘要说明
/// </summary>
public class HandlerBase : IHttpHandler
{
    /// <summary>
    /// 指定过来的http请求类型  主要指定action方法名称的接收方式 get 或者 post
    /// </summary>
    protected NameValueCollection _httpReuqest = HttpContext.Current.Request.Form;

    /// <summary>
    /// 指定返回头
    /// </summary>
    protected string _contentType = "text/plain";

    /// <summary>
    /// 指定接收action方法的参数名称
    /// </summary>
    protected string _actionName = "method";

    //获取当前的http context
    protected HttpContext Context
    {
        get
        {
            return HttpContext.Current;
        }
    }

    /// <summary>
    /// 返回的内容
    /// </summary>
    protected string _resultContent = "";

    public void ProcessRequest(HttpContext context)
    {
        //修改请求为相应的请求
        if ("get" == context.Request.RequestType.ToLower())
        {
            _httpReuqest = context.Request.QueryString;
        }
        else
        {
            if ("post" == context.Request.RequestType.ToLower())
            {
                _httpReuqest = context.Request.Form;
            }
        }
        
        context.Response.ContentType = this._contentType;

        try
        {
            //动态调用方法 当然  你还可以在这里加上是否为同域名请求的判断
            this.DynamicMethod();
        }
        catch (AmbiguousMatchException amEx)
        {
            this.PrintErrorJson(string.Format("根据该参数{0}找到了多个方法", amEx.Message));
        }
        catch (ArgumentException argEx)
        {
            this.PrintErrorJson("参数异常" + argEx.Message);
            Hangjing.Common.HJlog.toLog(argEx.ToString());
        }
        catch (ApplicationException apEx)
        {
            this.PrintErrorJson("程序异常" + apEx.Message);
            Hangjing.Common.HJlog.toLog(apEx.ToString());
        }

    }

    #region 动态调用方法
    /// <summary>
    /// 动态调用方法
    /// </summary>
    private void DynamicMethod()
    {
        //根据指定的请求类型获取方法名
        string action = this._httpReuqest[this._actionName];
        if (!string.IsNullOrEmpty(action))
        {
            //获取方法的实例  非静态 需要Public访问权限 忽略大小写
            MethodInfo methodInfo = this.GetType().GetMethod(action, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (methodInfo != null)
            {
                //调用方法
                this._resultContent = methodInfo.Invoke(this, null).ToString();
                PrintText(this._resultContent);
            }
            else
            {
                throw new ApplicationException(string.Format("没有找到方法{0}", action));
            }
        }
        else
        {
            throw new ArgumentNullException("没有找到调用方法参数或者方法名为空");
        }
    }
    #endregion

    #region 打印相关处理（返回客户端的内容）
    /// <summary>
    /// 打印遇到异常的json
    /// </summary>
    /// <param name="msg"></param>
    protected void PrintErrorJson(string msg)
    {
        this.PrintJson("error", msg);
    }

    /// <summary>
    /// 打印成功处理的json
    /// </summary>
    /// <param name="msg"></param>
    protected void PrintSuccessJson(string msg)
    {
        this.PrintJson("success", msg);
    }

    /// <summary>
    /// 打印json
    /// </summary>
    /// <param name="state"></param>
    /// <param name="msg"></param>
    protected void PrintJson(string state, string msg)
    {
        this.Context.Response.Write("{\"state\":\"" + state + "\",\"msg\":\"" + msg + "\"}");
    }

    /// <summary>
    /// 打印文本信息
    /// </summary>
    /// <param name="state"></param>
    /// <param name="msg"></param>
    protected void PrintText(string msg)
    {
        this.Context.Response.Write(msg);
    }

    #endregion


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

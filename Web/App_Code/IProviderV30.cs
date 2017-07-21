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

/// <summary>
/// 功能提供者接口(底层接口)
/// </summary>
public interface IProviderV30
{

    void Init();

    void Shutdown();

}

/// <summary>
/// 异常
/// </summary>
public class InvalidConfigurationException : Exception
{

    public InvalidConfigurationException() : base() { }

    public InvalidConfigurationException(string message) : base(message) { }

    public InvalidConfigurationException(string message, Exception innerException) : base(message, innerException) { }

    public InvalidConfigurationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

}


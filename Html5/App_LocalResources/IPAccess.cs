using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// IPAccess 的摘要说明
/// </summary>
/// 

[System.ComponentModel.DataObject]
public class IPAccess
{
    public static  readonly string CONNSTRING=System.Configuration.ConfigurationManager.AppSettings["IPConnString"].ToString()+System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["dbPath"])+";";
    public static readonly string COMMANDTEXT = "Select top 1 city,province from ip_address where ";
    public static readonly string selectcityname = "Select pinyin from city_py Where city=";


	public IPAccess()
	{

    }

    // 获取客户端所在城市,省份,跳转域名
    public string[] GetCustomCity()
    {

        string Ip = GetClientIP();
        string[] ipcontent = new string[3];
        try
        {
            long ipint = IPtoNum(Ip);

            OleDbConnection oleconn = new OleDbConnection();
            oleconn.ConnectionString = CONNSTRING;

            OleDbCommand OleCommand = new OleDbCommand();
            OleCommand.Connection = oleconn;

            OleCommand.CommandText = COMMANDTEXT + ipint.ToString() + ">=ip1 and " + ipint.ToString() + "<=ip2 order by id desc";

            oleconn.Open();

            OleDbDataReader reader = OleCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                ipcontent[0] = reader["city"].ToString();
                ipcontent[1] = reader["province"].ToString();
            }
            else
            {
                ipcontent[0] = "";
                ipcontent[1] = "";
            }
            oleconn.Close();

            OleCommand.CommandText = selectcityname + "'" + ipcontent[0].ToString() + "'";

            oleconn.Open();
            reader = OleCommand.ExecuteReader();

            if (reader.Read())
            {
                ipcontent[2] = reader["pinyin"].ToString();
            }
            else
            {
                ipcontent[2] = "：该客户端的IP地址信息在数据库中没有找到相关信息";
            }
        }
        catch (Exception ex)
        {
            ipcontent[0] = ex.Message.ToString();
            ipcontent[1] = ex.Source.ToString();
            ipcontent[2] = ex.ToString();
        }
        return ipcontent;

    }

    //将IP 地址转化为数字
    public long IPtoNum(string Ip)
    {
        string[] stringip = new string[4];
        stringip = Ip.Split('.');
        long ipnum = Convert.ToInt64((stringip[0])) * 16777216 + Convert.ToInt64(stringip[1]) * 65536 + Convert.ToInt64(stringip[2])*256 + Convert.ToInt64(stringip[3]);
        return ipnum;
    }

    //获取客户端的ip地址
    public string GetClientIP()
    {
        string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        return result;
    }
}

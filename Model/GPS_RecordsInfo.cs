using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Hangjing.Model
{
	/// <summary>
	/// GPS设备状态报告记录表 记录GPS设备当前的坐标信息，由GPS设备上传到服务器
    /// 目前使用的字段做了注释 未注释的代表未使用
	/// </summary>
	public class GPS_RecordsInfo
	{

		private int _id;
		private string _jh1;
		private string _jh2;
		private string _jh3;
		private string _jh4;
		private string _jh5;
		private DateTime _addtime;
		private string _addname;
		private DateTime _uptime;
		private string _remark;
		private int _del;
		private int _baidu;

		/// <summary>
		/// 编号（自增）
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

		/// <summary>
		/// IMEI号 GPS设备中的IMEI号 以此为唯一标志区分设备与配送员/  注：二期修改成快递员编号 deliver.dataid
		/// </summary>
		public string JH1
		{
			set{ _jh1=value;}
			get{return _jh1;}
		}

		/// <summary>
        /// 经度：120.16711333333333
		/// </summary>
		public string JH2
		{
			set{ _jh2=value;}
			get{return _jh2;}
		}

		/// <summary>
        /// 纬度：30.260683333333333
		/// </summary>
		public string JH3
		{
			set{ _jh3=value;}
			get{return _jh3;}
		}

		/// <summary>
		/// 速度
		/// </summary>
		public string JH4
		{
			set{ _jh4=value;}
			get{return _jh4;}
		}

		/// <summary>
		/// 航向
		/// </summary>
		public string JH5
		{
			set{ _jh5=value;}
			get{return _jh5;}
		}

		/// <summary>
		/// 记录上传时间
		/// </summary>
		public DateTime AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string AddName
		{
			set{ _addname=value;}
			get{return _addname;}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime UpTime
		{
			set{ _uptime=value;}
			get{return _uptime;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

		/// <summary>
		/// 是否已经失效（设备故障） 1 失效 0 正常
		/// </summary>
		public int Del
		{
			set{ _del=value;}
			get{return _del;}
		}

		/// <summary>
		/// 
		/// </summary>
		public int baidu
		{
			set{ _baidu=value;}
			get{return _baidu;}
		}
	}
}


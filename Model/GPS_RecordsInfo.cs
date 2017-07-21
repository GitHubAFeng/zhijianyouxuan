using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Hangjing.Model
{
	/// <summary>
	/// GPS�豸״̬�����¼�� ��¼GPS�豸��ǰ��������Ϣ����GPS�豸�ϴ���������
    /// Ŀǰʹ�õ��ֶ�����ע�� δע�͵Ĵ���δʹ��
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
		/// ��ţ�������
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

		/// <summary>
		/// IMEI�� GPS�豸�е�IMEI�� �Դ�ΪΨһ��־�����豸������Ա/  ע�������޸ĳɿ��Ա��� deliver.dataid
		/// </summary>
		public string JH1
		{
			set{ _jh1=value;}
			get{return _jh1;}
		}

		/// <summary>
        /// ���ȣ�120.16711333333333
		/// </summary>
		public string JH2
		{
			set{ _jh2=value;}
			get{return _jh2;}
		}

		/// <summary>
        /// γ�ȣ�30.260683333333333
		/// </summary>
		public string JH3
		{
			set{ _jh3=value;}
			get{return _jh3;}
		}

		/// <summary>
		/// �ٶ�
		/// </summary>
		public string JH4
		{
			set{ _jh4=value;}
			get{return _jh4;}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string JH5
		{
			set{ _jh5=value;}
			get{return _jh5;}
		}

		/// <summary>
		/// ��¼�ϴ�ʱ��
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
		/// �Ƿ��Ѿ�ʧЧ���豸���ϣ� 1 ʧЧ 0 ����
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


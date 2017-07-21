using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ģ���Ӧ�Ĳ���(�鿴,����,�༭,ɾ����)
	/// </summary>
	[Serializable]
	public class sys_ModulePermitionInfo
	{
		private int _mid;
		private int _moduleid;
		private string _pername;
		private int _pvalue;
		private int _reveint;
		private string _revevar;
		/// <summary>
		/// 
		/// </summary>
		public int mid
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// ģ����
		/// </summary>
		public int ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// ģ��Ȩ������
		/// </summary>
		public string pername
		{
			set{ _pername=value;}
			get{return _pername;}
		}
		/// <summary>
		/// ģ��Ȩ��Ȩֵ(2��1,2,3,4..�η�)���鿴����ӣ��༭��ɾ����ȷ�ϵģ��ֱ���(2^1,2^2,2^3,2^4)
		/// </summary>
		public int pvalue
		{
			set{ _pvalue=value;}
			get{return _pvalue;}
		}
		/// <summary>
        /// ����(����)
		/// </summary>
		public int ReveInt
		{
			set{ _reveint=value;}
			get{return _reveint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReveVar
		{
			set{ _revevar=value;}
			get{return _revevar;}
		}
	}
}



// ETogoOpinionInfo���̼�����ʵ��.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// update by yangxiaolong@ihangjing.com
// 2010-03-12

using System;
namespace Hangjing.Model
{
	/// <summary>
	/// �̼�����ʵ��
	/// </summary>
	[Serializable]
	public class ETogoOpinionInfo
	{
        private int _dataid;
        private int _userid;
        private int _togoid;
        private int _point;
        private string _comment;
        private DateTime _posttime;
        private int _servicegrade;
        private int _flavorgrade;
        private int _speedgrade;
        private string _username;
        private string _togoname;
        private DateTime  _rtime;
        private string _rcontent;
        private int _rtype;
        private string _picture;
        private string _truename;

        /// <summary>
        /// �û�ͼƬ
        /// </summary>
        public string picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public string Rcontent
        {
            get { return _rcontent; }
            set { _rcontent = value; }
        }
       
        public int Rtype
        {
            get { return _rtype; }
            set { _rtype = value; }
        }

        public  DateTime Rtime
        {
            get { return _rtime; }
            set { _rtime = value; }
        } 

        /// <summary>
        /// ���ݱ��
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }

        /// <summary>
        /// �����߱��
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// �̼ұ��
        /// </summary>
        public int TogoID
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// 0��ʾû����ˣ�1��ʾ���
        /// </summary>
        public int Point
        {
            set { _point = value; }
            get { return _point; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime PostTime
        {
            set { _posttime = value; }
            get { return _posttime; }
        }

        /// <summary>
        /// ����ȼ�
        /// </summary>
        public int ServiceGrade
        {
            set { _servicegrade = value; }
            get { return _servicegrade; }
        }

        /// <summary>
        /// ��ζ�ȼ�
        /// </summary>
        public int FlavorGrade
        {
            set { _flavorgrade = value; }
            get { return _flavorgrade; }
        }

        /// <summary>
        /// �ٶȵȼ�
        /// </summary>
        public int SpeedGrade
        {
            set { _speedgrade = value; }
            get { return _speedgrade; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }

        /// <summary>
        /// �̼�����
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }
	}
}


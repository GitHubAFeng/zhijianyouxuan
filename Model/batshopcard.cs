using System;
namespace Hangjing.Model
{
	/// <summary>
	/// �Ż�ȯ����ʵ��
	/// </summary>
	[Serializable]
    public class batshopcardInfo
	{
        private int _dataid;
        private string _title;
        private string _batnum;
        private DateTime _adddate;
        private int _cantimes;
        private decimal _point;
        private int _inve1;
        private string _inve2;
        private int _cardcount;
        private string _contents;
        private string _adminname;
        private int _sortnum;
        private int _canusecount;
        private int _num;


        /// <summary>
        /// ת��json�õ�
        /// </summary>
        public int num
        {
            set { _num = value; }
            get { return _num; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int canusecount
        {
            set { _canusecount = value; }
            get { return _canusecount; }
        }

        /// <summary>
        /// ���򣨽���
        /// </summary>
        public int sortnum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
        }

        /// <summary>
        /// ����Ա����
        /// </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }

        /// <summary>
        /// ˵��
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public string batnum
        {
            set { _batnum = value; }
            get { return _batnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public int cantimes
        {
            set { _cantimes = value; }
            get { return _cantimes; }
        }
       
        /// <summary>
        /// ��ʾ�������ͣ�1->�ֽ��ۿۣ��������Żݶ��٣�;2->�ٷֱ��ۿۣ������������ۿ۶��٣�;3->�౶���֣����������ܣ�
        /// </summary>
        public int Inve1
        {
            set { _inve1 = value; }
            get { return _inve1; }
        }


        /// <summary>
        /// Inve1=1ʱ����ʾ�Żݵ��ֽ�;Inve1=2ʱ����ʾ���ܵ��ۿۣ�88������88��;Inve1=3ʱ����ʾ���ܵĻ��ֱ���
        /// </summary>
        public decimal point
        {
            set { _point = value; }
            get { return _point; }
        }

        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Inve2
        {
            set { _inve2 = value; }
            get { return _inve2; }
        }
        /// <summary>
        /// ���ɳ�ֵ������
        /// </summary>
        public int CardCount
        {
            set { _cardcount = value; }
            get { return _cardcount; }
        }

        private DateTime _starttime;
        /// <summary>
        /// ����ʱ������ʱ����ʼʱ��
        /// </summary>
        public DateTime starttime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }

        private DateTime _endtime;
        /// <summary>
        /// ����ʱ������ʱ������ʱ��
        /// </summary>
        public DateTime endtime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }

        private int _mtype;
        /// <summary>
        /// ���ͣ�1���ֶһ�ȯ 2�����Ż�ȯ
        /// ��ʾ�����ֶһ�ȯ��ʾ�û��û�������վ�һ�;�����Ż�ȯ��ʾ��ֱ�ӷ��͵��û��û��ֻ������������ȯ������ǰ̨��ʾ������ȯ��Ҫͨ���ϴ�excel����ȯ��
        /// </summary>
        public int mtype
        {
            get { return _mtype; }
            set { _mtype = value; }
        }

        private int _mydiscount;
        /// <summary>
        /// mtype=1,Ϊ�һ��������.����Ϊ��Ч
        /// </summary>
        public int mydiscount
        {
            get { return _mydiscount; }
            set { _mydiscount = value; }
        }

        private string _foossortids;
        /// <summary>
        ///δ��
        /// </summary>
        public string foossortids
        {
            get { return _foossortids; }
            set { _foossortids = value; }
        }

        private int _timelimity;
        /// <summary>
        /// δ��
        /// </summary>
        public int timelimity
        {
            get { return _timelimity; }
            set { _timelimity = value; }
        }

        private int _moneylimity;
        /// <summary>
        /// δ��
        /// </summary>
        public int moneylimity
        {
            get { return _moneylimity; }
            set { _moneylimity = value; }
        }

        private int _moneyline;
        /// <summary>
        /// moneylimity Ϊ0ʱ������Ԫ�ſ���
        /// </summary>
        public int moneyline
        {
            get { return _moneyline; }
            set { _moneyline = value; }
        }

        private string _togoname;
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


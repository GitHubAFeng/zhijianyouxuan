using System;
namespace Hangjing.Model
{
	/// <summary>
	/// ʵ����TogoDiscount ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class TogoDiscountInfo
	{
        private int _id;
        private int _togoid;
        private DateTime _starttime1;
        private DateTime _endtime;
        private decimal _range1;
        private decimal _range2;
        private decimal _digital;
        /// <summary>
        /// ��� ����
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// �̼ұ��
        /// </summary>
        public int togoid
        {
            set { _togoid = value; }
            get { return _togoid; }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime starttime1
        {
            set { _starttime1 = value; }
            get { return _starttime1; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime endtime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }

        /// <summary>
        /// �����۸�Χ
        /// </summary>
        public decimal range1
        {
            set { _range1 = value; }
            get { return _range1; }
        }

        /// <summary>
        /// �����۸�Χ
        /// </summary>
        public decimal range2
        {
            set { _range2 = value; }
            get { return _range2; }
        }

        /// <summary>
        /// �ۿ�
        /// </summary>
        public decimal digital
        {
            set { _digital = value; }
            get { return _digital; }
        }
	}
}


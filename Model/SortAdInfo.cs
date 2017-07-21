using System;
namespace Hangjing.Model
{
    /// <summary>
    /// �����Ӧ�����ֹ��
    /// </summary>
    [Serializable]
    public class SortAdInfo
    {
        private string _dataid;
        private string _picture;
        private string _link;
        private string _introduce;
        private string _state;
        private string _title;
        private int _width;
        private int _height;
        private int _islink;
        private int _cityID;
        private int _stype;
        private int _sortid;
        private decimal _Servicefee;
        private DateTime _AdStartDate;
        private DateTime _AdEndDate;
        private string _defautpic;
        private int _reveint1;
        private int _reveint2;
        private string _revevar1;
        private string _revevar2;
        /// <summary>
        /// ������չʾʱ��ʱ����ʾ��ͼƬ����û������
        /// </summary>
        public string defautpic
        {
            set { _defautpic = value; }
            get { return _defautpic; }
        }

        /// <summary>
        /// չʾ����ʱ��
        /// </summary>
        public DateTime AdEndDate
        {
            set { _AdEndDate = value; }
            get { return _AdEndDate; }
        }

        /// <summary>
        /// չʾ��ʼʱ��
        /// </summary>
        public DateTime AdStartDate
        {
            set { _AdStartDate = value; }
            get { return _AdStartDate; }
        }

        /// <summary>
        /// δ��
        /// </summary>
        public decimal Servicefee
        {
            set { _Servicefee = value; }
            get { return _Servicefee; }
        }

        /// <summary>
        /// δ��
        /// </summary>
        public int sortid
        {
            set { _sortid = value; }
            get { return _sortid; }
        }

        /// <summary>
        /// �������(0��ʾͼƬ��1��ʾ����)
        /// </summary>
        public int stype
        {
            set { _stype = value; }
            get { return _stype; }
        }

        /// <summary>
        /// δ��
        /// </summary>
        public int cityid
        {
            set { _cityID = value; }
            get { return _cityID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DataId
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string Link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public string Introduce
        {
            set { _introduce = value; }
            get { return _introduce; }
        }
        /// <summary>
        /// ���򡾽���
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public int Width
        {
            set { _width = value; }
            get { return _width; }
        }
        /// <summary>
        /// δ��
        /// </summary>
        public int Height
        {
            set { _height = value; }
            get { return _height; }
        }

        /// <summary>
        /// �Ƿ�����ӣ�0�����ǣ�1�����
        /// </summary>
        public int isLink
        {
            set { _islink = value; }
            get { return _islink; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ReveInt1
        {
            set { _reveint1 = value; }
            get { return _reveint1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ReveInt2
        {
            set { _reveint2 = value; }
            get { return _reveint2; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReveVar1
        {
            set { _revevar1 = value; }
            get { return _revevar1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ReveVar2
        {
            set { _revevar2 = value; }
            get { return _revevar2; }
        }
    }
}


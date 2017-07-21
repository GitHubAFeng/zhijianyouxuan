using System;
using System.Collections.Generic;
namespace Hangjing.Model
{
    /// <summary>
    /// ʵ����ETogoOrder ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class simpleorderInfo
    {
        private string _orderid;
        private string _username;
        private string _address;
        private string _togoname;
        private string _tel;

        /// <summary>
        /// �ֻ�����
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }

        /// <summary>
        /// �������(��ǰʱ���Զ�����)
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        /// <summary>
        /// �û���ʵ����(�ռ���)
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// �Ͳ͵�ַ
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        /// �̼�����
        /// 2010-03-15 8:47 by jijunjian
        /// </summary>
        public string TogoName
        {
            set { _togoname = value; }
            get { return _togoname; }
        }
        
    }
}


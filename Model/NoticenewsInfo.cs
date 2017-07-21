/*********************************************************************
 * CopyRight (c) 2010-2011 HangJing Teconology. All Rights Reserved.
 * Function : 商家公告实体类
 * Created by jijunjian at 2010-8-5 17:52:08.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Model
{
    /// <summary>
    /// 实体类noticenews 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class NoticenewsInfo
    {
		private int _dataid;
		private string _title;
		private string _producer;
		private int _status;
		private DateTime _adddate;
		private int _inve1;
		private string _inve2;
        private string _togoName;

        /// <summary>
        /// 商家名称
        /// </summary>
        public string TogoName
        {
            get { return _togoName; }
            set { _togoName = value; }
        }
		/// <summary>
		/// 活动编号
		/// </summary>
		public int DataId
		{
			set{ _dataid=value;}
			get{return _dataid;}
		}
		/// <summary>
		/// 活动标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 活动说明信息
		/// </summary>
		public string producer
		{
			set{ _producer=value;}
			get{return _producer;}
		}
		/// <summary>
        ///状 态（是否激活 1、0） 是，1 否， 2
		/// </summary>
		public int status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime Adddate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int inve1
		{
			set{ _inve1=value;}
			get{return _inve1;}
		}
		/// <summary>
		///网站审核是否通过 0表示没有审核，1表示审核通过，2表示未通过,未通过，商家编辑后变未审核
		/// </summary>
		public string inve2
		{
			set{ _inve2=value;}
			get{return _inve2;}
		}
    }
}

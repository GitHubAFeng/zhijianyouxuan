﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class AliPayInfo
    {
        private string _out_trade_no;
        private string _subject;
        private string _body;
        private string _total_fee;
        private string _show_url;

        /// <summary>
        /// 初始化AliPayInfo
        /// </summary>
        /// <param name="out_trade_no">订单号</param>
        /// <param name="subject">商品名称</param>
        /// <param name="body">商品描述</param>
        /// <param name="total_fee">商品总价</param>
        /// <param name="show_url">商品展示地址</param>
        public AliPayInfo(string out_trade_no, string subject, string body, string total_fee, string show_url)
        {
            _out_trade_no = out_trade_no;
            _subject = subject;
            _body = body;
            _total_fee = total_fee;
            _show_url = show_url;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OutTradeNo
        {
            get { return _out_trade_no; }
            set { _out_trade_no = value; }
        }
        /// <summary>
        ///  商品名称
        /// </summary>
        public string SubJect
        {
            get { return _subject; }
            set { _subject = value; }
        }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
        /// <summary>
        /// 商品总价
        /// </summary>
        public string TotalFee
        {
            get { return _total_fee; }
            set { _total_fee = value; }
        }
        /// <summary>
        /// 商品展示地址
        /// </summary>
        public string ShowUrl
        {
            get { return _show_url; }
            set { _show_url = value; }
        }
    }
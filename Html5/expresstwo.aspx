<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="expresstwo.aspx.cs" Inherits="Html5.expresstwo" %>


<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>
        <%= SectionProxyData.GetSetValue(2)%></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/pictip.css" rel="stylesheet" />
    <script src="javascript/jquery.js"></script>


</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="express.aspx" id="back" class=" top_left"></a>
            <h1>跑腿</h1>
        </div>
        <div class="container ">
            <ul class="my_order_list orderdetail">
                <li>
                    <div class="order-tit">
                        <span class="time"><strong>发件人</strong>
                            <input type="text" class="w_txt" value="" placeholder="请输入发件人" id="tbusername" />
                        </span>
                    </div>
                    <div class="order-tit">
                        <span class="time"><strong>发件人手机</strong>
                            <input type="text" class="w_txt" value="" placeholder="请输入发件人手机" id="tbTel" />
                        </span>
                    </div>
                    <div class="order-tit">
                        <span class="time"><strong>收件人</strong>
                            <input type="text" class="w_txt" value="" placeholder="请输入收件人" id="tbcallmsg" />
                        </span>
                    </div>
                    <div class="order-tit">
                        <span class="time"><strong>收件人手机</strong>
                            <input type="text" class="w_txt" value="" placeholder="请输入收件人手机" id="tbReveVar" />
                        </span>
                    </div>
<%--                    <div class="order-tit" id="buybox">
                        <span class="time"><strong class="orange">金额</strong>
                            <input type="text" class="w_txt" id="tbTotalPrice" value="0" style="width: 100px" placeholder="请输入代驾费用" />元
                        </span>
                    </div>--%>
                    <div class="order-tit" style="border-bottom: none;">
                        <span class="time"><strong>备注</strong>
                            <input type="text" class="w_txt" id="tbRemark" style="width: 240px;" value="" placeholder="可填写代驾说明，以便详细了解需求" />
                        </span>
                    </div>
                </li>
                <li>
                    <div class="order-info-dis bord re">
                        <p class="f14"><span class="xtx_tit">路程</span> <span class="cat_evt mess-dis"><i id="lbdistance">0</i>公里</span></p>
                    </div>
                    <div class="order-info-dis re">
                        <p class="f14"><span class="xtx_tit">跑腿费</span> <span class="cat_evt mess-dis"><i id="lbsendfee">0</i>元</span></p>
                    </div>
                </li>
            </ul>

            <div class="view_back_con">
                <input name="" value="下一步" onclick="nextstep()" id="btnextstep" class="view_back_btn" type="button">
            </div>
        </div>
    </div>
</body>
</html>


<script src="javascript/shopcarttool.js"></script>
<script src="javascript/jCommon.js?v=1106"></script>
<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/expresstwo.js"></script>
<script src="javascript/spin.min.js"></script>

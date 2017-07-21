<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="submitexpressorder.aspx.cs" Inherits="Html5.submitexpressorder" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%= SectionProxyData.GetSetValue(2)%></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="javascript/jquery.js"></script>

</head>

<body>

    <input type="hidden" runat="server" id="hfuid" value="0" />

    <div class="page">

        <div id="page_title">
            <a href="expresstwo.aspx" id="back" class=" top_left"></a>
            <h1>跑腿</h1>
        </div>
        <form action="submitexpressorder.aspx" method="post">
            <div class="container ">
                <input type="hidden" runat="server" id="tbexpressinfo" value="" />
                <ul class="my_order_list orderdetail">
                    <li>
                        <div class="order-info-pt">
                            <p><span>从：</span><label id="tbfuserinfo"></label></p>
                            <p><span>到：</span><label id="tbtuserinfo"></label></p>
                        </div>
                    </li>
                    <li>
                        <div class="order-info-dis bord re">
                            <p class="f14"><span class="xtx_tit">收件人电话</span> <span class="cat_evt mess-dis" id="tbTel"></span></p>
                        </div>
                        <div class="order-info-dis bord  re" id="buybox">
                            <p class="f14"><span class="xtx_tit">商品金额</span> <span class="cat_evt mess-dis" id="tbTotalPrice">0元</span></p>
                        </div>
                        <div class="order-info-dis re">
                            <p class="f14"><span class="xtx_tit">预约取件时间</span> <span class="cat_evt mess-dis" id="tbSentTime"></span></p>
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
                    <li>
                        <div class="order-tit order-title">
                            <span class="time">余额支付</span>
                            <span class="mess_ch">
                                <label class="no-checked"></label>
                                <input type="radio" name="ddlpaymode" value="3" class="pay_check" />
                            </span>
                        </div>
                        <div class="order-tit order-psw" style=" display: none;">
                            <span class="time">支付密码：</span>
                            <span class="mess_chice">
                                <input name="tbpaypwd" runat="server" type="password" id="tbpaypwd" class="passw_text" placeholder="请输入支付密码" />
                                <span>当前余额：<i runat="server" id="lbmymoney">0.0</i></span>
                            </span>
                        </div>
                        <div class="order-tit order-title">

                            <span class="time">发件人支付</span>
                            <span class="mess_ch">
                                <label class="checked"></label>
                                <input type="radio" name="ddlpaymode" value="2" checked="true" class="pay_check" />
                            </span>
                        </div>
                        <div class="order-tit order-title" style="border-bottom: none;">
                            <span class="time">收件人支付</span>
                            <span class="mess_ch">
                                <label class="no-checked"></label>
                                <input type="radio" name="ddlpaymode" value="4" class="pay_check" />
                            </span>
                        </div>


                    </li>
                </ul>
            </div>
            <div class="bom_cart">
                <div class="cart_info"><span class="price" style="margin-left:0; font-size:16px;">共计￥<label id="tballprice">0</label></span> </div>
                <input name="" value="确认下单" id="btsubmit" onclick="return checkorder()" class="check_cart_btn" type="submit" />
            </div>
        </form>
    </div>
</body>
</html>
<script src="javascript/jCommon.js?v=1106"></script>
<script src="javascript/shopcarttool.js"></script>
<script src="javascript/expressorderdetai.js"></script>
<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/spin.min.js"></script>

<script type="text/javascript">

    $(document).ready(function () {

        var err = request("msg");

        switch (err) {
            case "1":
                swal("", "提交订单失败", "error");
                break;
            case "2":
                swal("", "您的账户余额为0，不能选择账户余额支付", "error");
                break;
            case "3":
                swal("", "支付密码错误，请重新输入.", "error");
                break;
            case "6":
                swal("", "余额不足，请选择其他支付方式", "error");
                break;
            default:
                break;
        }
    })
</script>


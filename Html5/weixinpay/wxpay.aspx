<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxpay.aspx.cs" Inherits="Html5.weixinpay.wxpay" %>


<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>微信支付</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="../css/page.css?v=1" />


    <script src="/javascript/jquery.js"></script>
    <script src="/javascript/lazyloadv3.js"></script>
    <style type="text/css">
        .submit {
            background-color: #3da300;
            background-image: -moz-linear-gradient(center top, #3da300, #318200);
            border-radius: 6px 6px 6px 6px;
            color: #FFFFFF;
            display: block;
            font-size: 17px;
            height: 38px;
            line-height: 38px;
            text-align: center;
            text-decoration: none;
            font-family: "微软雅黑";
        }

        .loading-style-1 {
            width: 80px;
            height: 40px;
            margin: 50px auto;
        }

            .loading-style-1 span {
                display: inline-block;
                width: 8px;
                height: 100%;
                border-radius: 4px;
                background: lightgreen;
                -webkit-animation: loading-style-1 1s ease-in-out infinite;
                -moz-animation: loading-style-1 1s ease-in-out infinite;
                -ms-animation: loading-style-1 1s ease-in-out infinite;
                -o-animation: loading-style-1 1s ease-in-out infinite;
                animation: loading-style-1 1s ease-in-out infinite;
            }

        @-webkit-keyframes loading-style-1 {
            0%,100% {
                height: 40px;
                background: lightgreen;
            }

            50% {
                height: 70px;
                margin: -15px 0;
                background: lightblue;
            }
        }

        @-moz-keyframes loading-style-1 {
            0%,100% {
                height: 40px;
                background: lightgreen;
            }

            50% {
                height: 70px;
                margin: -15px 0;
                background: lightblue;
            }
        }

        @-ms-keyframes loading-style-1 {
            0%,100% {
                height: 40px;
                background: lightgreen;
            }

            50% {
                height: 70px;
                margin: -15px 0;
                background: lightblue;
            }
        }

        @-o-keyframes loading-style-1 {
            0%,100% {
                height: 40px;
                background: lightgreen;
            }

            50% {
                height: 70px;
                margin: -15px 0;
                background: lightblue;
            }
        }

        @keyframes loading-style-1 {
            0%,100% {
                height: 40px;
                background: lightgreen;
            }

            50% {
                height: 70px;
                margin: -15px 0;
                background: lightblue;
            }
        }

        .loading-style-1 span:nth-child(2) {
            -webkit-animation-delay: 0.2s;
            -moz-animation-delay: 0.2s;
            -ms-animation-delay: 0.2s;
            -o-animation-delay: 0.2s;
            animation-delay: 0.2s;
        }

        .loading-style-1 span:nth-child(3) {
            -webkit-animation-delay: 0.4s;
            -moz-animation-delay: 0.4s;
            -ms-animation-delay: 0.4s;
            -o-animation-delay: 0.4s;
            animation-delay: 0.4s;
        }

        .loading-style-1 span:nth-child(4) {
            -webkit-animation-delay: 0.6s;
            -moz-animation-delay: 0.6s;
            -ms-animation-delay: 0.6s;
            -o-animation-delay: 0.6s;
            animation-delay: 0.6s;
        }

        .loading-style-1 span:nth-child(5) {
            -webkit-animation-delay: 0.8s;
            -moz-animation-delay: 0.8s;
            -ms-animation-delay: 0.8s;
            -o-animation-delay: 0.8s;
            animation-delay: 0.8s;
        }
    </style>
</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="/index.aspx" id="back" class=" top_left"></a>
            <h1>开始支付</h1>
        </div>
        <div class="container">

            <div class="loading-style-1">
                <span></span>
                <span></span>
                <span></span>
                <span></span>
                <span></span>
            </div>




            <ul class="my_order_list" style="border-bottom: none; display: none;">
                <li>
                    <div class="order-tit">
                        <input id="getBrandWCPayRequest" type="submit" value="微信支付" onclick="return checkuser()" style="margin: 0 60px; font-size: 20px;" class="w_txt" data-ajax="false" />
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <uc2:Foot runat="server" ID="footer" />
</body>
</html>
<script src="../javascript/jCommon.js"></script>


<script type="text/javascript">
    $(document).ready(function () {
        localStorage.setItem("ShoppingCart", "");
        setTimeout("pay()", 2000);
    })

    //微信jspay支付
    function pay() {
        // 当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。

        //公众号支付

        var paydata =
        {
            "appId": "<%= appId %>", //公众号名称，由商户传入
            "timeStamp": "<%= timeStamp %>", //时间戳
            "nonceStr": "<%= nonceStr %>", //随机串
            "package": "<%= packageValue %>",//扩展包
            "signType": "MD5", //微信签名方式:md5
            "paySign": "<%= paySign %>" //微信签名
        }

        WeixinJSBridge.invoke('getBrandWCPayRequest', paydata, function (res) {
            if (res.err_msg == "get_brand_wcpay_request:ok") {

                var orderid = request("orderid") + "";
                if (orderid.indexOf('_') > 0) {
                    window.location = "/myqrcode.aspx";
                }

                else {

                    if (orderid.startsWith("r")) {
                        window.location = "/RechargeList.aspx?id=" + orderid;
                    }
                    else {
                        window.location = "/showtogoorder.aspx?id=" + orderid;
                    }
                    
                }
            }
            else {
                alert("支付失败，请联系客服");
            }
            // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
            //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
        });


    }

</script>

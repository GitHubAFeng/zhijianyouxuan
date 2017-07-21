<%@ Page Language="C#" AutoEventWireup="true" CodeFile="weixinpay.aspx.cs" Inherits="OrderSuccess_weixinpay" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>微信支付 - <%= SectionProxyData.GetSetValue(3)%></title>

    <link type="text/css" href="css/common.css?v=1" rel="stylesheet" />
        <link href="css/order.css?v=1" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-1.8.2.js"></script>



</head>
<body>
    <form id="Form1" runat="server">
        <!--导航控件位置-->
         <uc3:Banner ID="Banner2" runat="server" />
        <uc1:Banner ID="Banner1" runat="server" />
       
        <input type="hidden" runat="server" id="qrtext" />

        <div class="wrap">
            <div class="mainbord mgb10 whbg mgt10">
                <div style="box-shadow: none; border-bottom: 1px solid #ddd;" class="main-tit">

                    <ul class="left">
                        <li class="cul">微信支付</li>
                    </ul>
                </div>
                <div class="h1">感谢您在<span class="orange"><%=SectionProxyData.GetSetValue(2)%></span>订外卖，使用微信扫码完成支付！</div>
                <div class="clearfix susscon" style="width:800px; background-image:none; margin-bottom:30px; padding-left:0">
                    <div class="fl okbiao" style="width:400px;">

                        <div id="code"></div>


                    </div>
                    <div class="fl susscon_txt" style="width:400px;">


                        <div class="f14">
                            <p>
                                您的订单号：<span class="f16 orange" id="lborderid" runat="server"></span>
                            </p>
                            <p>
                                订单总金额：<span class="orange" id="paymoney" runat="server">￥</span>
                            </p>

                        </div>




                        <p style="margin-bottom: 32px" class="f12">您现在可以<a runat="server" id="orderlink" class="orange" href="user/MyOrderList.aspx?state=0">查看订单</a> <span class="padlr10">或者</span><a class="orange" href="Index.aspx">返回首页</a></p>
                    </div>
                </div>
            </div>


        </div>

        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>



<script type="text/javascript" src="javascript/jquery.qrcode.min.js"></script>



<script type="text/javascript">

    $(document).ready(function () {
        initnav(2);
        var str = toUtf8($("#qrtext").val());
        $("#code").qrcode({
            render: "table",
          
            text: str
        });

    })

    function toUtf8(str) {
        var out, i, len, c;
        out = "";
        len = str.length;
        for (i = 0; i < len; i++) {
            c = str.charCodeAt(i);
            if ((c >= 0x0001) && (c <= 0x007F)) {
                out += str.charAt(i);
            } else if (c > 0x07FF) {
                out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
                out += String.fromCharCode(0x80 | ((c >> 6) & 0x3F));
                out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
            } else {
                out += String.fromCharCode(0xC0 | ((c >> 6) & 0x1F));
                out += String.fromCharCode(0x80 | ((c >> 0) & 0x3F));
            }
        }
        return out;
    }
</script>

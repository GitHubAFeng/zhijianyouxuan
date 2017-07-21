<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderSuccess.aspx.cs" Inherits="OrderSuccess" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>外卖订单 - <%= SectionProxyData.GetSetValue(3)%></title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="css/sytle.css" rel="stylesheet" type="text/css" />
    <link href="css/order.css?v=1" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="javascript/jCommon.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            initnav(2);
        })

    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <!--导航控件位置-->
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:Banner ID="Banner1" runat="server" />
        <div class="wrap">
            <div class="mainbord mgb15 whbg">
                <div style="box-shadow: none; border-bottom: 1px solid #ddd;" class="main-tit">
                    <ul class="left">
                        <li class="cul">订单成功</li>
                    </ul>
                </div>
                <div class="h1">感谢您在<span class="orange"><%=SectionProxyData.GetSetValue(2)%></span> 购物！</div>
                <div class="clearfix susscon">
                    <p class="f24">
                        恭喜，您的订单已成功提交！<br>
                        我们会有专人处理您的订单
                    </p>
                    <div class="f20">

                        <div id="regnotice" runat="server">
                        </div>

                        您的订单号：<span class="f24 orange" id="orderid" runat="server"></span><br>
                        您选定的支付方式：<span class="orange" id="payway" runat="server"></span>
                        <br>
                        订单总金额：<span class="orange" id="paymoney" runat="server">￥</span>元
                        <p>
                            优惠券支付：<span class="orange" id="lbcardpay" runat="server">￥</span>元

                        </p>
                        <p>
                            促销优惠：<span class="orange" id="lbpromotion" runat="server"></span>元

                        </p>
                    </div>
                    <p class="f18 orange">送餐可能需要<span class="f24" id="sendtime" runat="server"></span>分钟左右，请耐心等候，谢谢！</p>
                    <p style="margin-bottom: 32px" class="f14">您现在可以 <a runat="server" id="orderlink" class="orange" href="user/MyOrderList.aspx?state=0">查看订单</a> <span class="padlr10">或者</span><a class="orange" href="Index.aspx">返回首页</a></p>

                </div>
            </div>

            <div class="mgb15">

                <script src="<%= WebUtility.GetUrl("~/js/Advertisement/2.js") %>" type="text/javascript"></script>


            </div>

        </div>

        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

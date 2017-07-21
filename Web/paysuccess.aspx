<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaySuccess.aspx.cs" Inherits="PaySuccess" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>支付成功 - <%= SectionProxyData.GetSetValue(3)%></title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="css/sytle.css" rel="stylesheet" type="text/css" />
    <link href="css/order.css?v=1" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="javascript/jCommon.js" type="text/javascript"></script>

</head>
<body>
    <form id="Form1" runat="server">
        <!--导航控件位置-->
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:Banner ID="Banner1" runat="server" />
        <div class="wrap">
            <div class="mainbord mgb10 whbg">
                <div style="box-shadow: none; border-bottom: 1px solid #ddd;" class="main-tit">
                    <ul class="left">
                        <li class="cul">支付成功</li>
                    </ul>
                </div>
                <div class="h1">感谢您在<span class="orange"><%=SectionProxyData.GetSetValue(2)%></span>购物！</div>
                <div class="clearfix susscon">
                    <p class="f24" runat="server" id="divmsg">
                        恭喜，您的订单已支付成功！
                      
                    </p>
                    <div class="f20">

                      

                        您的订单号：<span class="f24 orange" id="spanorderid" runat="server"></span><br>
                        支付总金额：￥<span class="orange" id="paymoney" runat="server"></span>
                    </div>
                  
                    <p style="margin-bottom: 32px" class="f14">您现在可以<a runat="server" id="orderlink" class="orange" href="user/MyOrderList.aspx?state=0">查看订单</a> <span class="padlr10">或者</span><a class="orange" href="Index.aspx">返回首页</a></p>

                </div>
            </div>

            <div class="mgb10">

                <script src="<%= WebUtility.GetUrl("~/js/Advertisement/2.js") %>" type="text/javascript"></script>


            </div>

        </div>

        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

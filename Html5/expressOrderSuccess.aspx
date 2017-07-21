<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="expressOrderSuccess.aspx.cs" Inherits="Html5.expressOrderSuccess" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <script src="javascript/jquery.js"></script>
    <script src="javascript/shopcarttool.js"></script>
    <script src="javascript/jCommon.js"></script>
</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="myexpresslist.aspx" id="back" class=" top_left" data-ajax="false"></a>
            <h1>订单提交成功</h1>
        </div>
        <div class="container">
            <ul class="my_order_list">
                <li data-icon="false">
                    <div data-icon="false" class="order-tit" style="white-space: normal; height: auto">
                        您的订单已经提交成功，请耐心等待
                    </div>
                 
                    <div class="order-tit">
                        订单编号：<label runat="server" id="lborderid"></label>
                    </div>
                    <div class="order-tit" style="border-bottom:none;">
                        订单金额：<label runat="server" id="lbprice"></label>元
                    </div>
                  
                </li>
            </ul>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    $(document).ready(function () {
        //express.delExpressinfo();
    })
</script>


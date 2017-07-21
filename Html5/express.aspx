<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="express.aspx.cs" Inherits="Html5.express" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>
        <%= SectionProxyData.GetSetValue(2)%></title>
    <link type="text/css" href="css/pictip.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <link href="css/mobiscroll.custom-2.17.0.min.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery.js"></script>

    <script src="javascript/mobiscroll.custom-2.17.0.min.js" type="text/javascript"></script>

</head>

<body id="page_allMenu">
    <input type="hidden" runat="server" id="hfcityjson" />
    <input type="hidden" runat="server" id="hffeesjon" />
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" id="back" class=" top_left"></a>
            <h1>跑腿</h1>
        </div>
        <div class="container ">

            <input type="hidden" runat="server" id="hfcityname" value="全国" />
            <input type="hidden" runat="server" id="lat" />
            <input type="hidden" runat="server" id="lng" />

            <input type="hidden" value="0" id="tbcallcount" />
            <!--寄件人经纬度-->
            <input type="hidden" runat="server" id="hidflat" value="0" />
            <input type="hidden" runat="server" id="hidflng" value="0" />
            <!--收件人经纬度-->
            <input type="hidden" runat="server" id="hidtlat" value="0" />
            <input type="hidden" runat="server" id="hidtlng" value="0" />
            <!--配送距离和配送费-->
            <input type="hidden" runat="server" id="hiddistance" value="0" />
            <input type="hidden" runat="server" id="hidsendfee" value="0" />

            <div class="paotui_con">
                <div class="paotui_song">
                    <p>代购</p>
                    <p class="cur" onclick="setexpresstype(this,0)"><a href="javascript:"></a></p>
                </div>
                <div class="paotui_mid"></div>
                <div class="paotui_mai">
                    <p>代驾</p>
                    <p onclick="setexpresstype(this,1)"><a href="javascript:"></a></p>
                </div>
            </div>
            <ul class="my_order_list orderdetail">
                <li>
                    <div class="order-tit">
                        <span class="time"><strong class="blue">从</strong>
                            <input type="text" id="tbAddress" name="tbAddress" onfocus="showmap($('#tbAddress').val(),'f')" readonly="readonly" class="w_txt" value="" style="width:220px;" placeholder="小区/大厦/标志性建筑/交叉路口" reg="\S" tip="请输入小区/大厦/标志性建筑/交叉路口" />
                        </span><span class="mess_ch"><i class="ico-open1" id="typeiconbox">购</i> </span>
                    </div>
                    <div class="order-tit">
                        <span class="time">
                            <input type="text" id="tbAddressdetail" name="tbAddressdetail" class="w_txt2" value="" placeholder="具体楼层/门牌号" canbenull="n" reg="\S" tip="请输入具体楼层/门牌号" />
                        </span>
                    </div>
                    <div class="order-tit">
                        <span class="time"><strong class="orange">到</strong>
                            <input type="text" class="w_txt" id="tbOorderid" onfocus="showmap($('#tbOorderid').val(),'t')" readonly="readonly" name="tbOorderid" value="" style="width:220px;" placeholder="小区/大厦/标志性建筑/交叉路口" reg="\S" tip="请输入小区/大厦/标志性建筑/交叉路口" />
                        </span><span class="mess_ch"></span>
                    </div>
                    <div class="order-tit" style="border-bottom: none;">
                        <span class="time">
                            <input type="text" class="w_txt2" id="tbOorderiddetail" name="tbOorderiddetail" value="" placeholder="具体楼层/门牌号" reg="\S" tip="请输入具体楼层/门牌号" />
                        </span>
                    </div>
                </li>
                <li id="yuyue">
                    <div class="order-info re" style="background-image: none;">
                        <p class="f14">
                            <span class="xtx_tit">预约时间</span>
                            <span class="cat_evt mess">
                                <input type="text" class="w_txt2" id="tbSentTime" name="tbSentTime" onchange="calDistanceAndFee()" onblur="calDistanceAndFee()" value="" placeholder="选择预约时间" tip="选择预约时间" />
                            </span>
                        </p>
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
            <div class="pricing_infor" style="padding-right:15px;">
                跑腿费用每公里:
                <a href="javascript:" >
                    <%=SectionProxyData.GetSetValue(66)%>
                </a>
                元
            </div>

            <div class="view_back_con">
                <input name="" value="下一步" class="view_back_btn" onclick="nextstep()" type="button" id="btnextstep">
            </div>

        </div>
    </div>


    <script id="priceModule" type="text/x-jsrender">
        <div style="text-align: left;" id="pricetip">
            <dl>
                <dt>跑腿费用每公里：</dt>
                <dd><%=SectionProxyData.GetSetValue(66)%>元</dd>
            </dl>
        </div>
    </script>


    <div class="mModal" style="z-index: 900; display: none;">
        <a href="javascript:void(0)" style="height: 480px;" id="mymark"></a>
    </div>
    <div class="mDialog freeSet" style="z-index: 901; display: none; top: 50px; width: 90%; height: 80%;"
        data-ffix-top="99" id="maptipbox">
        <div class="content">
            <b></b>
            <div>
                <div class="btn" style="margin: 10px; position: relative; margin-top: 20px;">
                    <input name="keyaddress" class="commonyinput" type="text" id="keyaddress" placeholder="请输入您的位置">
                </div>
            </div>
        </div>
        <a class="x" href="javascript:void(0)">X</a>
    </div>

</body>
</html>

<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=fMnzyhYs0D1cBEl5iGMQ0Dlg"></script>
<script src="javascript/eventwrapper.min.js"></script>
<script src="javascript/shopcarttool.js"></script>
<script src="javascript/jCommon.js?v=1106"></script>
<script src="javascript/sweetalert.min.js"></script>

<script src="javascript/spin.min.js"></script>
<script src="javascript/jsrender.js"></script>
<script src="javascript/express.js?v=2"></script>

<script type="text/javascript">


</script>
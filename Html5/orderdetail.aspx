<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderdetail.aspx.cs" Inherits="Html5.orderdetai" %>

<%@ Register Src="~/baseControl.ascx" TagPrefix="uc1" TagName="baseControl" %>




<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>
        <%= SectionProxyData.GetSetValue(2) %></title>

    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1215" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <script src="javascript/jquery.js"></script>

</head>

<body>
    <input type="hidden" runat="server" id="hfuid" value="0" />
    <input type="hidden" runat="server" id="hftid" value="0" />
    <div class="page" data-role="page" data-theme="d" id="w_infor">
        <div id="page_title">
            <a href="#" id="gocart" runat="server" class="back top_left"></a>
            <h1>提交订单</h1>
        </div>


        <form method="post" id="form1">
            <div id="divError" runat="server" class="error_list">
            </div>
            <input type="hidden" id="hfproductjson" value="" name="hfproductjson" />
            <div class="container ">
                <ul class="my_order_list orderdetail">
                    <li id="addnew" runat="server">
                        <a href="#" id="addnews" runat="server">
                            <div class="order-info">
                                <i class="add_address"></i>
                                <p class="f14 " style="display: inline-block">新增收货地址</p>
                            </div>
                        </a>
                    </li>
                    <li id="nowres" runat="server" class="address_style">
                        <div class="address_style_bot">
                            <a href="#" id="nowress" runat="server">
                                <div class="order-info" style="margin-left: 15px;">
                                    <div class="order-add-con">
                                        <p class="f14"><span id="tbname"><% =ReceiverText %></span>，<span id="tbtel"><% =PhoneText %></span></p>
                                        <p class="f14" id="tbaddress" runat="server"></p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </li>
                    <li>
                        <div class="order-info bord re">
                            <p class="f14">
                                <span class="xtx_tit">送达时间</span>
                                <span class="cat_evt mess">
                                    <select runat="server" id="ddltime" style="border: none;">
                                    </select>
                                </span>
                            </p>
                        </div>
                        <div class="order-info re" onclick="addremark(<%= Request["id"]  %>)">
                            <p class="f14">
                                <span class="xtx_tit">备注信息</span>
                                <span class="cat_evt mess" ><% =remark %></span>
                            </p>
                        </div>
                    </li>

                    <div class="tit">使用红包</div>

                    <li>
                        <div class="order-info " style="background-image: none;">
                            <select name="ddlpackage" id="ddlpackage" style="padding: 2px;">
                                <option value="0" data-moneyline="0">选择红包</option>
                                <asp:Repeater ID="rptpackage" runat="server">
                                    <ItemTemplate>
                                        <option value="<%# Eval("id") %>" data-moneyline="<%#Eval("moneyline")%>"><%# Convert.ToDecimal(Eval("alltotal")).ToString("#0") %>元红包，满<%# Convert.ToDecimal(Eval("moneyline")).ToString("#0") %>可用</option>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </select>
                        </div>
                    </li>
                    <div class="tit">
                        <a href="addshopcard.aspx" runat="server" id="addcardllink" style="float: right; padding-right: 10px;">绑定&gt;&gt;</a> 使用优惠券
                    </div>

                    <li>
                        <div class="order-info " style="background-image: none;">
                            <select runat="server" id="dllcard" style="padding: 2px;">
                            </select>
                        </div>
                    </li>
                    <div class="tit">支付方式</div>
                    <li>
                        <asp:Repeater runat="server" ID="rptstyle">
                            <ItemTemplate>
                                <div class="order-tit order-title">
                                    <span class="time"><%#Eval("classname")%></span>
                                    <span id="mess_<%# Container.ItemIndex + 1%>" class="mess_ch">
                                        <label class="no-checked"></label>
                                        <input type="radio" name="ddlpaymode" value="<%#Eval("Status")%>" class="pay_check" />
                                    </span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="order-tit order-psw" style="border-bottom: none; border-top: 1px solid #dfdfdf; display: none; height: 80px;">
                            <span class="time">支付密码：</span>
                            <div class="order-info" style="background-image: none; float: left; padding-top: 0;">
                                <input name="tbpaypwd" runat="server" type="password" id="tbpaypwd" class="j_text" style="width: 100px;" placeholder="请输入支付密码">
                                <div>当前余额：<i runat="server" id="lbmymoney">0.0</i></div>
                            </div>

                            <div class=" clear"></div>
                        </div>
                    </li>
                </ul>

                <ul class="my_order_list orderdetail">
                    <li>
                        <asp:Repeater runat="server" ID="rptpromotion">
                            <ItemTemplate>
                                <div class="order-tit order-title shoppromotion-marker">


                                    <div><i style="background: url('/images/jian_02.png') no-repeat scroll 0 0 rgba(0, 0, 0, 0); margin: 0; padding: 0; width: 15px;"></i>&nbsp;<%#Eval("revevar1")%></div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </li>

                </ul>


            </div>
            <div class="bom_cart">
                <div class="cart_info">
                    <span class="my_cart" id="btcartbox"><i class="ico-cart"></i><span class="cart-num">12</span></span>
                    <span class="price">共计<span class="f20 money_num">0</span></span>
                </div>
                <input type="submit" value="提交订单" id="btsubmit" class="check_cart_btn" onclick="return checkorder()" data-ajax="false" />
            </div>
        </form>
        <div data-role="popup" id="nofoodnotice" data-theme="e" data-overlay-theme="a" class="ui-content" style="display: none;">
            您的购物车是空的哦
        </div>
    </div>
    <div class="mModal" id="myordermsgk" style="z-index: 900; display: none;"><a href="javascript:void(0)" style="height: 480px;" id="mymark"></a></div>
    <uc1:baseControl runat="server" ID="baseControl" />
</body>
</html>

<script src="javascript/jCommon.js?v=2016080221"></script>
<script src="javascript/jquery.form.js"></script>
<script src="/javascript/shopcarttool.js" type="text/javascript"></script>
<script src="/javascript/jCommon.js" type="text/javascript"></script>
<script src="/javascript/orderdetai.js?v=2016081533"></script>

<script src="javascript/sweetalert.min.js"></script>



<script type="text/javascript">

    $(document).ready(function () {


        $("#mess_1").children("label").removeClass("no-checked").addClass("checked");
        $("#mess_1").children("input").attr("checked", "checked");
        if ($("input[name=ddlpaymode]:checked").val() == "3") {
            $(".order-psw").show();
        }
    })
</script>


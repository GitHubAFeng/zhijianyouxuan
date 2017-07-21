<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showtogoorder.aspx.cs" Inherits="Html5.showtogoorder" %>

<!DOCTYPE html>

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link href="css/timeline.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <script src="javascript/jquery.js"></script>

    <style type="text/css">
        .shop_detail ul li {
            width: 50%;
        }
    </style>

</head>


<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hMsg" />
        <asp:HiddenField runat="server" ID="hState" />

         <asp:HiddenField runat="server" ID="hfshopid" />
        <asp:HiddenField runat="server" ID="hffoodids" />

        <div class="page">
            <div id="page_title">
                <a href="myorderlist.aspx" data-ajax="false" id="back" class=" top_left"></a>
                <h1>订单详情</h1>
                <a href="Commentshop.aspx" class="reg top_right" data-ajax="false" runat="server" id="dellink">我要点评</a>
            </div>

            <div class="shop_detail" style="position: static; top: 0;">
                <ul>
                    <li class="cur_con" id="order1" onclick="settable('order', 1, 2, 'cur_con', '')"><a href="javascript:">订单状态</a></li>
                    <li id="order2" onclick="settable('order', 2, 2, 'cur_con', '')"><a href="javascript:">订单详情</a></li>
                </ul>
            </div>

            <div class="container">

                <div id="order_div1">


                       <div class="view_back_con" style="margin-top:20px;">
                               
                               <input type="button" class="view_back_btn"  value="再来一单" onclick="bookagain()" />
                            </div>



                     <div id="cd-timeline" class="cd-container">
                            <asp:Repeater runat="server" ID="rptppt">
                                <ItemTemplate>
                                    <div class="cd-timeline-block">
                                        <div class="cd-timeline-img cd-picture">
                                            <img src="/images/send_ico.png" alt="Picture">
                                        </div>
                                        <div class="cd-timeline-content">
                                            <h2><%# Eval("title") %></h2>
                                          
                                            <p style="<%#Eval("subtitle").ToString().Length  == 0 ? "none;": ""  %>"><%# Eval("subtitle") %></p>
                                            <span class="cd-date"><%# Convert.ToDateTime(Eval("addtime")).ToString("MM月dd日 HH:mm")%></span>

                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>


                </div>

                <div class="" style="display: none;" id="order_div2">

                    <div class="my_order_list orderdetail" style="margin-top: 20px;">
                        <div class="tit">状态信息</div>
                    </div>
                    <ul class="my_order_list">
                        <li>

                            <asp:Repeater runat="server" ID="rptorder2">
                                <ItemTemplate>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>订单号：</strong>
                                            <label class="w_txt">
                                                <%# Eval("orderid") %>[<%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>]
                                            </label>

                                        </span>
                                    </div>



                                </ItemTemplate>
                            </asp:Repeater>



                            <div class="view_back_con" id="divconfirm" runat="server">
                                <asp:Button ID="upStatus" OnClick="upStatus_Click" CssClass="view_back_btn" runat="server" Text="确认收货" />
                            </div>


                        </li>
                    </ul>

                    <div class="my_order_list orderdetail">
                        <div class="tit">订单明细</div>
                    </div>
                    <ul class="my_order_list">
                        <li>

                            <asp:Repeater runat="server" ID="rptfood">
                                <ItemTemplate>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <label><%# Eval("FoodName")%></label>
                                            <label class="w_txt">
                                                <%# Convert.ToDecimal(Eval("OldPrice"))%>元x<%# Eval("FCounts")%>
                                            </label>

                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:Repeater runat="server" ID="rptorder1">
                                <ItemTemplate>
                                    <div class="order-tit">
                                        <div>
                                            <label class="w_txt" style="float: right; padding-right: 15px;">
                                                商品总额：￥<%# Convert.ToDecimal(Eval("OldPrice"))%>
                                            </label>

                                        </div>
                                    </div>
                                      <div class="order-tit">
                                        <div>
                                            <label class="w_txt" style="float: right; padding-right: 15px;">
                                              + 打包费：￥<%# Convert.ToDecimal(Eval("packagefee"))%>
                                            </label>

                                        </div>
                                    </div>

                                    <div class="order-tit">
                                        <div>
                                            <label class="w_txt" style="float: right; padding-right: 15px;">
                                                + 配送费：￥<%# Convert.ToDecimal(Eval("SendFee"))%>
                                            </label>

                                        </div>
                                    </div>

                                    <div class="order-tit">
                                        <div>
                                            <label class="w_txt" style="float: right; padding-right: 15px;">
                                                - 优惠券：￥<%# Convert.ToDecimal(Eval("cardpay"))%>
                                            </label>

                                        </div>
                                    </div>


                                    <asp:Repeater ID="rptpromotons" runat="server" DataSource='<%# Eval("Promotions")%>'>
                                        <ItemTemplate>


                                            <div class="order-tit">
                                                <div>
                                                    <label class="w_txt" style="float: right; padding-right: 15px;">

                                                        <div>
                                                            <i style="background: url('/images/jian_02.png') no-repeat scroll 0 0 rgba(0, 0, 0, 0); margin: 0; padding: 0; width: 15px; display: inline-block; height: 15px;"></i>&nbsp;<%#Eval("revevar1")%>
                                                        </div>



                                                    </label>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <div class="order-tit">
                                        <div>
                                            <label class="w_txt" style="float: right; padding-right: 15px;">
                                                应付总额：￥<span style="color: red;"><%# Convert.ToDecimal(Eval("needpaymoney"))%>
                                            </label>

                                        </div>
                                    </div>

                                    <div class="order-tit" style="border: none;">
                                        <div>
                                            <label class="w_txt" style="float: right; padding-right: 15px;">
                                                支付方式： <%# WebUtility.TurnPayModel(Eval("PayMode").ToString())%>[<%# Eval("PayState").ToString()=="1"?"已付":"未付" %> <span style="color: red;"><%# Convert.ToDecimal(Eval("needpaymoney"))%></span>]
                                            </label>

                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </li>
                    </ul>

                    <div class="my_order_list orderdetail">
                        <div class="tit">其他信息</div>
                    </div>

                    <ul class="my_order_list">
                        <li>

                            <asp:Repeater runat="server" ID="rptorder">
                                <ItemTemplate>

                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>商家名称：</strong>
                                            <label class="w_txt">
                                                <%# Eval("togoname")%>
                                            </label>
                                        </span>
                                    </div>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>联系人：</strong>

                                            <label class="w_txt">
                                                <%# Eval("OrderRcver")%>
                                            </label>
                                        </span>
                                    </div>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>手机号码：</strong>
                                            <label class="w_txt">
                                                <%# Eval("OrderComm")%>
                                            </label>
                                        </span>
                                    </div>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>送达时间：</strong>

                                            <label class="w_txt">
                                                <%# Eval("SendTime")%>
                                            </label>
                                        </span>
                                    </div>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>送餐地址：</strong>


                                            <label class="w_txt">
                                                <%# Eval("AddressText")%>
                                            </label>
                                        </span>
                                    </div>
                                    <div class="order-tit" style="border-bottom: none;">
                                        <span class="time" data-icon="false">
                                            <strong>备注：</strong>

                                            <label class="w_txt">
                                                <%# Eval("OrderAttach")%>
                                            </label>
                                        </span>
                                    </div>


                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ul>

                    <div class="my_order_list orderdetail">
                        <div class="tit">商家信息</div>
                    </div>
                    <ul class="my_order_list">
                        <li>

                            <asp:Repeater runat="server" ID="rptshop">
                                <ItemTemplate>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>商家名称：</strong>
                                            <label class="w_txt">
                                                <%# Eval("name")%>
                                            </label>
                                        </span>
                                    </div>
                                    <div class="order-tit">
                                        <span class="time" data-icon="false">
                                            <strong>商家联系人：</strong>
                                            <label class="w_txt">
                                                <%# Eval("CommPerson")%>
                                            </label>
                                        </span>
                                    </div>
                                    <div class="order-tit" style="border-bottom: none;">
                                        <span class="time" data-icon="false">
                                            <strong>联系电话：</strong>
                                            <label class="w_txt">
                                                <%# Eval("Comm")%>
                                            </label>
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ul>
                </div>

                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>

            </div>
        </div>
    </form>
</body>
</html>
<script src="javascript/jCommon.js"></script>
<script src="javascript/sweetalert.min.js"></script>


<script type="text/javascript">

    $(document).ready(function () {

        var err = request("msg");

        switch (err) {
            case "1":
                swal("", "操作成功", "success");
                break;
            default:
                break;
        }
    })

    function bookagain()
    {
        gourl("ShowTogo.aspx?id=" + $("#hfshopid").val() + "&foodids=" + $("#hffoodids").val());
    }
</script>


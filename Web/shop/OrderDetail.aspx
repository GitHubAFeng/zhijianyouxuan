<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="shop_OrderDetail" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单详细-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/print.css" rel="stylesheet" />

    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="/Admin/javascript/jquery.PrintArea.js" type="text/javascript"></script>

    <style type="text/css">
        .usermima_order ul {
            float: left;
            width: 272px;
            margin: 0 12px;
        }
        .usermima {
            width: 544px;
        }
    </style>

    <script type="text/javascript">
        function checkdata(type) {
            if (type == 1) {
                showload_super();
                return true;
            }
            else {
                var tbshopremark = $("#tbshopremark").val();
                if (tbshopremark == "") {
                    alert("请输入拒绝原因");
                    return false;
                }
                if (confirm('确认要拒绝吗？')) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        $(document).ready(function () {
            initshopnav(3);

        })

        function printorder() {
            $('#print_area').html($('#printdata').html());
            $("#print_area").printArea();

        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">
                    <div class="shop_containner clearfix">
                        <%--<uc2:LeftBanner runat="server" ID="Left" />--%>
                        <div class="shop_main">
                            <div class="main-content">
                                <h1 class="topbg">订单状态（<%=OrderState %>）</h1>
                                <div class="usermima_order">
                                    <ul>
                                        <li><span class="left_span"><strong>订单信息</strong></span></li>
                                        <li><span class="left_span">订单号：</span>
                                            <asp:Label ID="lbOrderId" runat="server"></asp:Label>
                                        </li>
                                        <li><span class="left_span">商家名称：</span><asp:Label ID="togoname" runat="server"></asp:Label></li>
                                        <li style="display: none"><span class="left_span">就餐方式：</span><asp:Label ID="lbeattype" runat="server"></asp:Label></li>
                                        <li style="display: none"><span class="left_span">就餐人数：</span><asp:Label ID="lbpeople" runat="server"></asp:Label></li>
                                        <li style="display: none"><span class="left_span">会员折扣：</span><asp:Label ID="lbdiscount" runat="server"></asp:Label></li>
                                        <li><span class="left_span">送餐费：</span><asp:Label ID="lbcprice" runat="server"></asp:Label>元</li>



                                        <li><span class="left_span">总金额：</span><asp:Label ID="TextBoxtop" runat="server"></asp:Label>元</li>
                                        <li><span class="left_span">打包费：</span><asp:Label ID="lbpackage" runat="server"></asp:Label>元</li>
                                        <li><span class="left_span">菜品金额：</span><asp:Label ID="lboldprice" runat="server"></asp:Label>元</li>
                                        <li><span class="left_span">优惠券：</span><asp:Label ID="tbcardpay" runat="server"></asp:Label>元</li>
                                        <li><span class="left_span">促销优惠：</span><asp:Label ID="lbpromotion" runat="server"></asp:Label>元</li>
                                        <li><span class="left_span">骑士应付金额：</span><asp:Label ID="lbshopdiscountmoney" runat="server"></asp:Label>元</li>
                                        <li><span class="left_span">支付方式：</span><asp:Label ID="lblpaymode" runat="server"></asp:Label></li>
                                        <li><span class="left_span">支付状态：</span><asp:Label ID="lbpaytstate" runat="server"></asp:Label></li>
                                        <li runat="server" id="paymoney" style="display: none;"><span class="left_span">支付金额：</span><asp:Label
                                            ID="lblpaymoney" runat="server"></asp:Label>元</li>
                                        <li><span class="left_span">订单提交时间：</span><asp:Label ID="lbOrderdate" runat="server"></asp:Label></li>
                                    </ul>
                                    <ul>
                                        <li><span class="left_span"><strong>收货人信息</strong></span></li>
                                        <li><span class="left_span">收货人：</span><asp:Label ID="lbUName" runat="server"></asp:Label></li>
                                        <li><span class="left_span">手机：</span><asp:Label ID="lbTel" runat="server"></asp:Label></li>
                                        <li><span class="left_span">送餐时间：</span><asp:Label ID="lbmsg" runat="server"></asp:Label></li>
                                        <li><span class="left_span">备注：</span><asp:Label ID="lbremark" runat="server"></asp:Label></li>
                                        <li><span class="left_span">收货地址：</span><asp:Label ID="lbAddress" runat="server"></asp:Label></li>
                                        <li><span class="left_span">操作说明：</span>
                                            <div runat="server" id="opmsgbox"></div>
                                        </li>
                                        <li><span class="left_span">拒绝理由：</span>
                                            <asp:TextBox runat="server" ID="tbshopremark" TextMode="MultiLine" Width="210" placeholder="拒绝请输入原因" CssClass="input_on"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Button ID="tbreceive" runat="server" Text="接收" CssClass="subBtn" OnClick="set_Click" Style="margin-left: 10px;"
                                                OnClientClick="return checkdata(1);" />

                                            <asp:Button ID="tbrefuse" runat="server" Text="拒绝" CssClass="subBtn" OnClick="set_Click" Style="margin-left: 10px;"
                                                OnClientClick="return  checkdata(2);" />

                                            <asp:Button ID="tbdy" runat="server" Text="打印" CssClass="subBtn" OnClientClick="printorder();return false;" Style="margin-left: 10px;margin-top: 5px;" />

                                            <a href="/images/printset.jpg" target="_blank">打印设置</a>
                                        </li>

                                        <li><span class="left_span">订单状态：</span>
                                            <asp:DropDownList runat="server" ID="ddlFunction" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:Button ID="btSearch" runat="server" Text="更新" CssClass="subBtn" OnClick="btSaveState_Click" />
                                        </li>
                                    </ul>
                                </div>
                                <div class="usermima">
                                    <asp:Repeater ID="rtpBooks" runat="server">
                                        <HeaderTemplate>
                                            <table width="98%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;"
                                                class="listorder_table clear" align="center">
                                                <tr>
                                                    <th>商品名称
                                                    </th>

                                                    <th style="width: 20%">单价
                                                    </th>
                                                    <th style="width: 15%">数量
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("foodname")%>
                                                </td>

                                                <td class="orange">￥<%# Convert.ToDecimal(Eval("FoodPrice")) %>
                                                </td>
                                                <td>
                                                    <%#Eval("FCounts") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="process_btn">
                                    <asp:Button ID="Button1" OnClick="Button1_Click" CssClass="subBtn" runat="server"
                                        Text="返回列表" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="print_area" style="display: none;"></div>

                    <div style="display: none">

                        <div id="printdata">

                            <div class="printinfo">
                                <asp:Repeater runat="server" ID="rptorder">
                                    <ItemTemplate>
                                        <div style="width: 210px;">
                                            <h1>
                                                <%# Eval("togoname") %></h1>
                                            <ul style="border-bottom: 1px dashed #666666; margin-bottom: 5px; padding-bottom: 5px;">
                                                <li><span>单号:</span><label><%# Eval("orderid") %></label></li>
                                                <li><span>订单时间:</span><label><%# Eval("OrderDateTime")%></label></li>
                                                <li><span>用户名:</span><label><%# Eval("CustomerName")%></label></li>
                                                <li><span>收货人:</span><label><%# Eval("OrderRcver")%></label></li>
                                                <li><span>送餐时间:</span><label><%# Eval("SendTime")%></label></li>
                                                <li><span>手机:</span><label><%# Eval("CallPhoneNo")%></label></li>
                                                <li><span>备注:</span><label><%# Eval("OrderAttach")%></label></li>
                                            </ul>
                                            <ul class="food">
                                                <li class="name">商品</li>
                                                <li class="num">数量</li>
                                                <li class="price">单价</li>
                                                <li class="price">小计</li>
                                            </ul>
                                            <div class="clear"></div>
                                            <ul class="food">
                                                <asp:Repeater runat="server" ID="Repeater2" DataSource='<%# getproduct(Eval("orderid")) %>'>
                                                    <ItemTemplate>
                                                        <li class="name"><%# Eval("foodname")%></li>
                                                        <li class="num"><%#Eval("FCounts")%></li>
                                                        <li class="price"><%# Convert.ToDecimal( Eval("foodPrice")) %></li>
                                                        <li class="price"><%# Convert.ToDecimal(Eval("foodPrice")) * Convert.ToInt32(Eval("FCounts"))%></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                            <div class="clear"></div>
                                            <ul style="border-top: 1px dashed #666666; margin-top: 5px; padding-top: 5px;">
                                                <li>送餐费：<%# Eval("SendFee")%>元</li>
                                                <li>金额：<%# Eval("OrderSums")%>元</li>
                                                <li>类型：<%# Eval("ReveInt2").ToString() == "0"?"外卖":"堂食"%></li>
                                                <li>支付方式：<%#  WebUtility.TurnPayModel(Eval("paymode").ToString()) %></li>
                                                <li>支付状态：<%# WebUtility.TurnPayState(Eval("paystate")) %></li>
                                                <li>收货地址：<%# Eval("AddressText")%></li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

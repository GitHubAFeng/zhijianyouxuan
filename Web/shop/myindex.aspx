<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myindex.aspx.cs" Inherits="shop_myindex" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=7"/><!– IE7 mode –>
    <title>商家管理订单管理-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="/user/css/user_center.css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js" type="text/javascript"></script>

    <style type="text/css">
        .order_button {
            background: url(images/order_icon.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .order_button_hover {
            background: url(images/order_icon_hover.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .info_button {
            background: url(images/info_shop.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .info_button_hover {
            background: url(images/info_shop_hover.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .canpin_button {
            background: url(images/canpin_shop.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .canpin_button_hover {
            background: url(images/canpin_shop_hover.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .md_order {
            background: url(images/md_order.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .md_order_hover {
            background: url(images/md_order_hover.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .review_button {
            background: url(images/review_icon.png) no-repeat;
            width: 196px;
            height: 146px;
        }

        .review_button_hover {
            background: url(images/review_icon_hover.png) no-repeat;
            width: 196px;
            height: 146px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">
                    <div class="shop_main">
                        <%-- <h1 class="topbg">
                    管理首页</h1>--%>
                        <!--放置内容-->

                        <div class="main-content">
                            <div class="right_side_info">
                                <div class="info_right_side" style="float: none">
                                    <span class=" gray"><strong class="padding_r10">
                                        <asp:Literal ID="LitUserName" runat="server"></asp:Literal></strong>您好，欢迎登录！</span>
                                    <table border="0" cellpadding="0" cellspacing="0" width="540" class="info_table">
                                        <tbody>
                                            <tr>
                                                <td>加盟时间
                                                </td>
                                                <td class="gray">
                                                    <asp:Literal ID="litRegTime" runat="server"></asp:Literal>
                                                </td>
                                                <td>商家名称
                                                </td>
                                                <td class="gray">
                                                    <asp:Literal ID="LitShopName" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>订单数量
                                                </td>
                                                <td class="gray">
                                                    <asp:Literal ID="LitOrderCount" runat="server"></asp:Literal>
                                                </td>
                                                <td>评价次数
                                                </td>
                                                <td class="gray">
                                                    <asp:Literal ID="LitAllCount" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>账户余额
                                                </td>
                                                <td colspan="3" style="text-align: left; padding-left: 10px;">
                                                    <span class="orange fb">
                                                        <asp:Literal ID="LitUserBalance" runat="server"></asp:Literal>元</span>


                                                    <a href="cachoutdetail.aspx" style="color: #f39800">提现</a>

                                                    <a href="cachoutlist.aspx">查看收支记录</a>


                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="right_side_order">
                                <div class="order_bg">
                                    店铺今日订单
                                </div>
                            </div>
                            <table border="0" cellpadding="0" cellspacing="0" class="order_table">
                                <asp:Repeater ID="rptCollect" runat="server" OnItemCommand="rptproduct_ItemCommand">
                                    <HeaderTemplate>
                                        <tr>
                                            <th>时间
                                            </th>
                                            <th>用户名
                                            </th>
                                            <th>详情
                                            </th>
                                            <th>金额
                                            </th>
                                            <th>状态
                                            </th>
                                            <th>支付方式
                                            </th>
                                            <th>支付状态
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 80px;">
                                                <%# Convert.ToDateTime(Eval("OrderDateTime")).ToShortDateString()%>
                                            </td>
                                            <td style="width: 120px;">
                                                <%#Eval("OrderRcver")%>
                                            </td>
                                            <td style="width: 280px;">
                                                <a href="OrderDetail.aspx?id=<%# Eval("unid")%>" id="click_test2">查看</a>
                                            </td>
                                            <td style="width: 80px;">
                                                <%#Convert.ToInt32(Eval("OrderSums")).ToString()%>
                                            </td>
                                            <td style="width: 120px;">
                                                <%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>
                                            </td>
                                            <td style="width: 80px; border-right: none;">
                                                <%#  WebUtility.TurnPayModel(Eval("paymode").ToString()) %>
                                            </td>
                                            <td style="width: 80px; border-right: none;">
                                                <%# WebUtility.TurnPayState(Eval("paystate")) %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div id="noRecord" runat="server" style="display: none; margin-left: 20px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;<h4>今日暂无订购记录!</h4>
                            </div>
                            <div class="pages">
                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                    HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                    TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                    ShowPageIndex="True" PageSize="10" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                    TextAfterPageIndexBox=" 页 " Wrap="False">
                                </webdiyer:AspNetPager>
                            </div>
                            <div class="right_side_bottom">
                                <h2>您的功能应用:</h2>
                                <ul>

                                    <%--  <li style="<%= SectionProxyData.GetSetValue(55) == "1" ? "": "display:none"%>" class=" md_order" onclick="window.location= 'express.aspx'" onmouseout="this.className='md_order'"
                                    onmousemove="this.className='md_order_hover'"></li>--%>

                                    <li class=" order_button" onclick="window.location= 'index.aspx'" onmouseout="this.className='order_button'"
                                        onmousemove="this.className='order_button_hover'"></li>

                                    <li class=" info_button" onclick="window.location= 'myshop.aspx'" onmouseout="this.className='info_button'"
                                        onmousemove="this.className='info_button_hover'"></li>

                                    <li class=" canpin_button" onclick="window.location= 'FoodSortList.aspx'" onmouseout="this.className='canpin_button'"
                                        onmousemove="this.className='canpin_button_hover'"></li>

                                    <li class=" review_button" onclick="window.location= 'TogoCommentList.aspx'" onmouseout="this.className='review_button'"
                                        onmousemove="this.className='review_button_hover'"></li>

                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

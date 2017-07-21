<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myindex.aspx.cs" Inherits="user_myindex" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员中心-管理首页-<%= SectionProxyData.GetSetValue(3)%></title>

    <link rel="stylesheet" type="text/css" href="css/user_center.css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp">
            <uc2:LeftBanner runat="server" ID="Left" />
            <div class="rightmenu_cont_index">
                <h1 class="topbg">管理首页</h1>
                <!--放置内容-->
                <div class="huser_right_side">
                    <div class="right_side_info">
                        <div class="info_left_side">
                            <p>
                                <img src="images/image_view.jpg" runat="server" id="imgmypic" height="90" width="90" /></p>
                            <p><a href="<%= WebUtility.GetUrl("~/user/MyInfo.aspx") %>">更换图像</a></p>
                        </div>
                        <div class="info_right_side">
                            <span class=" gray"><strong class="padding_r10">
                                <asp:Literal ID="LitUserName" runat="server"></asp:Literal></strong> 您好，欢迎登录！</span>
                            <table border="0" cellpadding="0" cellspacing="0" width="540" class="info_table">
                                <tbody>
                                    <tr>
                                        <td>注册时间
                                        </td>
                                        <td class="gray">
                                            <asp:Literal ID="litRegTime" runat="server"></asp:Literal>
                                        </td>
                                        <td>分销账户
                                        </td>
                                        <td class="gray">
                                            <asp:Literal ID="LitLoginInt" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>成功订单
                                        </td>
                                        <td class="gray">
                                            <asp:Literal ID="LitOrder" runat="server"></asp:Literal>
                                        </td>
                                        <td>积分数
                                        </td>
                                        <td class="gray">
                                            <asp:Literal ID="LitPoint" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            账户余额
                                        </td>
                                        <td colspan="3" style="text-align: left; padding-left: 10px;">
                                            <span class="orange fb">
                                                <asp:Literal ID="LitUserBalance" runat="server"></asp:Literal>元</span> 
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="right_side_order">
                        <div class="order_bg">
                            我的今日订单
                        </div>
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" class="order_table">
                        <asp:Repeater ID="rptCollect" runat="server" OnItemCommand="rptproduct_ItemCommand">
                            <HeaderTemplate>
                                <tr>
                                    <th>时间
                                    </th>
                                    <th>店铺
                                    </th>
                                    <th>详情
                                    </th>
                                    <th>金额
                                    </th>
                                    <th>状态
                                    </th>
                                    <th>支付方式
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 80px;">
                                        <%# Convert.ToDateTime(Eval("OrderDateTime")).ToShortDateString()%>
                                    </td>
                                    <td style="width: 120px;">
                                        <%#Eval("TogoName")%>
                                    </td>
                                    <td style="width: 280px;">
                                        <a href="orderdetail.aspx?id=<%# Eval("Unid")%>" id="click_test2">查看</a>
                                    </td>
                                    <td style="width: 80px;">
                                        <%#Convert.ToInt32(Eval("OrderSums")).ToString()%>
                                    </td>
                                    <td style="width: 120px;">
                                        <%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>
                                    </td>
                                    <td style="width: 80px; border-right: none;">
                                        <%# Pay(Eval("paymode"))%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--<tbody>
                        <tr>
                          <th style="width:80px;">时间</th>
                          <th style="width:120px;">店铺</th>
                          <th style="width:280px;">详情</th>
                          <th style="width:80px;">金额</th>
                          <th style="width:120px;">状态</th>
                          <th style="width:80px; border-right:none;">支付方式</th>
                        </tr>
                        <tr>
                          <td colspan="6" style=" border-right:none;">今日暂无订购记录...<td>
                        </tr>
                      </tbody>--%>
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
                            <li class=" order_button" onclick="window.location= 'MyOrderList.aspx'" onmouseout="this.className='order_button'"
                                onmousemove="this.className='order_button_hover'"></li>
                            <li class=" account_button" onclick="window.location= 'MyInfo.aspx'" onmouseout="this.className='account_button'"
                                onmousemove="this.className='account_button_hover'"></li>
                            <li class=" favorites_button" onclick="window.location= 'MyShops.aspx'" onmouseout="this.className='favorites_button'"
                                onmousemove="this.className='favorites_button_hover'"></li>
                            <li class=" gift_button" onclick="window.location= 'mychange.aspx'" onmouseout="this.className='gift_button'"
                                onmousemove="this.className='gift_button_hover'"></li>
                            <li class=" review_button" onclick="window.location= 'ShopCommentList.aspx'" onmouseout="this.className='review_button'"
                                onmousemove="this.className='review_button_hover'"></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

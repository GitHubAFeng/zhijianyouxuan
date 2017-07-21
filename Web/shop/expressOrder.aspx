<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expressOrder.aspx.cs" Inherits="shop_expressOrder" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>跑腿订单-<%= SectionProxyData.GetSetValue(3)%></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/Common.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery.min.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hfcityid" />
        <asp:HiddenField runat="server" ID="hfstate" Value="-1" />
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">
                    <div class="shop_main">
                        <div class="main-content">
                            <div class="shop_menu" style="display:none;">
                                <ul>
                                    <li id="today"><a href="OrderList.aspx?id=1">今日订单</a></li>
                                    <li id="all"><a href="OrderList.aspx">所有订单</a></li>
                                    <li id="express" class="cur"><a href="expressOrder.aspx">跑腿订单</a></li>
                                    <li id="cancel"><a href="cancelOrder.aspx">退单</a></li>
                                    <li id="hurry"><a href="HurryOrder.aspx">催单</a></li>
                                </ul>
                            </div>
                            <div class="rightmenu_bg">
                                <%--                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <div class="listorder">
                                    <ul>
                                        <li>订单编号
                                    <input type="hidden" id="tbuserids" runat="server" />
                                            <input id="tbKeyword" runat="server" name="workAddr" autocomplete="off" type="text"
                                                class="input_on" />
                                            订单时间：
                                    <input type="text" name="textfield2" id="starttime" size="10" runat="server" class="input_on"
                                        onfocus="WdatePicker({readOnly:true})" />
                                            至
                                    <input type="text" name="textfield3" id="enttime" runat="server" size="10" class="input_on"
                                        onfocus="WdatePicker({readOnly:true})" />
                                            <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="subBtn" OnClick="btSearch_Click" />
                                        </li>
                                    </ul>
                                </div>

                                <div class="clear"></div>

                                <div class="usermima" style="clear: both; padding-left: 0">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                        style="border: 1px solid #ccc;">
                                        <asp:Repeater ID="rptPointCount" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th style="width: 20%">订单编号(点击查看定位)
                                                    </th>
                                                    <th style="width: 10%">状态
                                                    </th>
                                                    <th>商品
                                                    </th>
                                                    <th style="width: 8%">运费
                                                    </th>
                                                    <th style="width: 18%">取件时间
                                                    </th>
                                                    <th style="width: 7%">操作
                                                    </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: left; padding-left: 10px">
                                                        <a href="javascript:showorderexpress(<%# Eval("State") %>,0,<%# Eval("DeliveInfo.DeliverId") %>,'<%# Eval("orderid") %>','<%# Eval("callmsg") %>','<%# Eval("Oorderid") %>','<%# Eval("UserName") %>','0')">
                                                            <%#  Eval("orderid") %>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <%# WebUtility.TurnExpressOrderState(Eval("state")) %>
                                                    </td>
                                                    <td style="text-align: left; padding-left: 10px">
                                                        <%# Eval("Inve2")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("sendmoney")%>
                                                    </td>
                                                    <td style="text-align: left; padding-left: 10px">
                                                        <%# Eval("SentTime")%>
                                                    </td>
                                                    <td>
                                                        <label id="latlng_<%# Eval("OrderId") %>" style="display: none;">
                                                            <%#Eval("sitelat") %>
                                                        </label>
                                                        <a href="javascript:" onclick="showdetail('<%#  Eval("dataid") %>');">展开</a>
                                                    </td>
                                                </tr>
                                                <tr style="display: none;" class="tr_<%#  Eval("dataid") %>">
                                                    <td colspan="6">
                                                        <div class="text_align_left">发件联系：<%# Eval("UserName")%>，<%# Eval("Tel")%>，<%# Eval("Address")%></div>
                                                    </td>
                                                </tr>
                                                <tr style="display: none;" class="tr_<%#  Eval("dataid") %>">
                                                    <td colspan="6">
                                                        <div class="text_align_left">收件联系：<%# Eval("callmsg")%>，<%# Eval("ReveVar")%>，<%# Eval("Oorderid")%></div>
                                                    </td>
                                                </tr>
                                                <tr style="display: none;" class="tr_<%#  Eval("dataid") %>">
                                                    <td colspan="6">
                                                        <div class="text_align_left">
                                                            备注：<%#Eval("remark")%>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>

                                    <div id="noRecord" runat="server" style="display: none; margin-bottom: 20px;" class="no_infor">
                                        暂无数据!
                                    </div>

                                    <div class="indis_right_map" id="map" runat="server">
                                        <div id="map_canvas" style="width: 100%; height: 338px">
                                        </div>
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
                                </div>
                                <%--                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script src="/user/javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

<script src="/javascript/jCommon.js" type="text/javascript"></script>
<script src="/javascript/ShowDivDialog.js" type="text/javascript"></script>

<script src="/javascript/eventwrapper.min.js" type="text/javascript"></script>
<script src="http://api.map.baidu.com/api?v=1.2" type="text/javascript"></script>
<script src="/user/javascript/orderdetail.js" type="text/javascript"></script>

<script type="text/javascript">

    //显示、隐藏订单详细
    function showdetail(orderid) {
        $(".tr_" + orderid).toggle();
        //  $(".indis_right_map").toggle();
    }
</script>

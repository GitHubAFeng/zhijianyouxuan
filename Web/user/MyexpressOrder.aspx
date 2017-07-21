<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyexpressOrder.aspx.cs" Inherits="UserHome_MyexpressOrder" %>


<%@ Register Src="~/header.ascx" TagName="head" TagPrefix="uc1" %>
<%@ Register Src="~/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="uc2" TagName="Foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>跑腿订单-会员中心-<%= WebUtility.GetWebName() %></title>

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>


</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hfcityid" />

        <uc1:head runat="server" ID="head" />
        <uc1:TogoBanner runat="server" ID="TogoBanner1" />
        <div class="warp_con">
            <asp:HiddenField runat="server" ID="hfstate" Value="-1" />
            <uc2:LeftBanner runat="server" ID="Left" />
            <div class="rightmenu_cont">
                <h1 class="topbg" runat="server" id="h1title">跑腿订单</h1>
                <div class="rightmenu_bg">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
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
                            <div class="clear">
                            </div>
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
                                            <%--   <tr style="display: none;" class="tr_<%#  Eval("dataid") %>">
                                                <td colspan="6">
                                                    <div class="text_align_left">
                                                        <div class="indis_right_map">
                                                            <div class="map_canvas" style="width: 100%; height: 338px">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>--%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>

                                <div class="indis_right_map">
                                    <div id="map_canvas" style="width: 100%; height: 338px">
                                    </div>
                                </div>

                                <div id="noRecord" runat="server" style="display: none;" class="no_infor">
                                    暂无数据!
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div>
                    </div>
                </div>
            </div>
        </div>
        <uc2:Foot runat="server" ID="foot" />
    </form>
</body>
</html>
<script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

<script src="http://api.map.baidu.com/api?v=1.2" type="text/javascript"></script>

<script src="javascript/orderdetail.js" type="text/javascript"></script>
<script type="text/javascript">

    //显示、隐藏订单详细
    function showdetail(orderid) {
        $(".tr_" + orderid).toggle();
      //  $(".indis_right_map").toggle();
    }
</script>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyOrderList.aspx.cs" Inherits="UserHome_MyOrderList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-我的订单-<%= SectionProxyData.GetSetValue(3)%></title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Admin/javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <!--add for outcomplete-->

    <script src="../JavaScript/Common.js" type="text/javascript"></script>

    <script src="JavaScript/OrderAutoComplete.js" type="text/javascript"></script>

    <script src="javascript/soundmanager2.js" type="text/javascript"></script>

    <script type="text/javascript">
        function test(ddd) {
            var content2 = ddd;
            TINY.box.show(content2, 0, 0, 0, 1);

        };
        var xmlHttp;
        var completeDiv;
        var inputField;
        var nameTable;
        var nameTableBody;

        function createXMLHttpRequest() {
            if (window.ActiveXObject) {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            else if (window.XMLHttpRequest) {
                xmlHttp = new XMLHttpRequest();
            }
        }

        function initVars() {
            inputField = document.getElementById("tbKeyword"); //input
            nameTable = document.getElementById("name_table"); //table
            completeDiv = document.getElementById("popup"); //div
            nameTableBody = document.getElementById("name_table_body"); //tbody
        }

        function populateName(cell) {
            if (cell.firstChild.nodeValue != "没有此记录") {
                inputField.value = cell.firstChild.nodeValue;
            }
            clearNames();
        }

        function clearNames() {
            var ind = nameTableBody.childNodes.length;
            for (var i = ind - 1; i >= 0; i--) {
                nameTableBody.removeChild(nameTableBody.childNodes[i]);
            }
            completeDiv.style.border = "none";
        }
        //隐藏列表框
        function aaa() {
            if (completeDiv.style.border == "black 1px solid") {
                completeDiv.style.border = "none";
                nameTableBody.style.display = "none";
            }
        }

        function delayURL() {
            var delay = document.getElementById("time").innerHTML;
            if (delay > 0) {
                delay--;
                document.getElementById("time").innerHTML = delay;
            }
            else {
                var v = $("#hfstate").val() + "";
            }
            setTimeout(delayURL, 1000);
        }

        $(document).ready(delayURL);

        soundManager.debugMode = false;
        soundManager.debugFlash = false;
        soundManager.url = "soundmanager2.swf";

        function play(flag) {
            $("#time").html(30);
            if (flag == 0) {
                return;
            }
            var v = document.getElementById("cbSound").checked;
            if (v == true) {
                soundManager.play('mySound0', 'notify.mp3');
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <asp:HiddenField runat="server" ID="hfstate" Value="-1" />
        <div class="warp">
            <uc2:LeftBanner runat="server" ID="Left" />
            <div class="rightmenu_cont">
                <div class="rightmenu_cen">
                    <h1 class="topbg">我的订单</h1>
                    <%--<div class="no_address" runat="server" id="divmsg">
                    您还没有填写地址簿，<a href="MyAddress.aspx">现在填写</a>...</span></div>--%>
                    <div class="hdporder">
                        <span>关键字</span>
                        <input type="hidden" id="tbuserids" runat="server" />
                        <input id="tbKeyword" runat="server" name="workAddr" autocomplete="off" type="text"
                            class="text" />
                        <div style="position: absolute;" id="popup">
                            <table id="name_table" bgcolor="#FFFAFA" border="0" cellspacing="0" cellpadding="0">
                                <tbody id="name_table_body">
                                </tbody>
                            </table>
                        </div>
                        订单时间：
                    <input type="text" name="textfield2" id="starttime" size="10" class="text" runat="server"
                        onfocus="WdatePicker({readOnly:true})" />
                        至
                    <input type="text" name="textfield3" id="enttime" runat="server" class="text" size="10"
                        onfocus="WdatePicker({readOnly:true})" />
                        <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="subBtn" OnClick="btSearch_Click" />
                    </div>
                    <div class="clear">
                    </div>
                    <div class="orderhistory-tishi" style="display: none">
                        <label>
                            订单提示：</label>
                        <input type="checkbox" id="cbSound" style="vertical-align: text-top; *vertical-align: middle;" />
                        <label for="cbSound">
                            声音提醒</label>
                        <span>
                            <label id="time">
                                30</label>
                            秒后自动刷新。
                        <asp:Button runat="server" ID="tbnew" CssClass="btnew" Text="手动刷新" OnClick="Timer1_Tick" /></span>
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000">
                    </asp:Timer>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="usermima">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table" style="border: 1px solid #ccc;">
                                    <asp:Repeater ID="rptPointCount" runat="server" OnItemCommand="rptOrder_Command"
                                        OnItemDataBound="order_bound">
                                        <HeaderTemplate>
                                            <tr>
                                                <th>订单号
                                                </th>
                                                <th>收件人
                                                </th>
                                                <th>所属商家
                                                </th>
                                                <th>订单金额
                                                </th>
                                                <th>订单时间
                                                </th>
                                                <th>订单状态
                                                </th>
                                                <th>操作
                                                </th>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <a href="OrderDetail.aspx?id=<%#Eval("unid")%>">
                                                        <%#  Eval("orderid") %>
                                                    </a>
                                                </td>
                                                <td>
                                                    <%# Eval("OrderRcver")%>
                                                </td>
                                                <td>
                                                    <a href="../shop.aspx?id=<%# Eval("TogoId")%>" id="A1">
                                                        <%# Eval("togoname") %></a>
                                                </td>
                                                <td>
                                                    <%# Eval("OrderSums")%>
                                                </td>
                                                <td>
                                                    <%# Eval("OrderDateTime")%>
                                                </td>
                                                <td>
                                                    <%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>
                                                </td>
                                                <td>
                                                    <a href="orderdetail.aspx?id=<%# Eval("unid")%>" id="click_test2">查看</a>
                                                    <asp:LinkButton ID="lbcui" CommandName="call" CommandArgument='<%# Eval("unid")+","+Eval("orderid")+","+Eval("TogoId")%>'
                                                        runat="server">催单</asp:LinkButton>

                                                    <asp:LinkButton ID="lbpayagain" Visible="false" CommandName="pay" CommandArgument='<%# Eval("unid")+","+Eval("orderid")+","+Eval("TogoId")%>'
                                                        runat="server">立即支付</asp:LinkButton>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <div id="noRecord" runat="server" class="no_infor">
                                    暂无订单！
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
                </div>
            </div>
            <div style="display: none; left: 526px; top: 236px;" id="address_drop">
            </div>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

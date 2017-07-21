<%@ Page Language="C#" AutoEventWireup="true" CodeFile="express.aspx.cs" Inherits="aboutus_express" %>

<%@ Register Src="~/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/header.ascx" TagName="head " TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>跑腿 - <%= WebUtility.GetWebName() %></title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/paotui.css" rel="stylesheet" type="text/css" />
    <link href="css/page.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-1.7.min.js" type="text/javascript"></script>

    <script src="javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            initnav(7);
        })
        //选择的是余额支付
        function payusermoney(val) {
            if (val == "3") {
                $("#paypassword").show();
            } else {
                $("#paypassword").hide();
            }
        }
    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <uc1:TogoBanner runat="server" ID="nav" />
        <uc1:head runat="server" ID="head" />

        <input type="hidden" runat="server" id="hfcityname" value="杭州市" />
        <!--默认经纬度-->
        <input type="hidden" runat="server" id="hidLat" value="0" />
        <input type="hidden" runat="server" id="hidLng" value="0" />
        <!--寄件人经纬度-->
        <input type="hidden" runat="server" id="hidflat" value="0" />
        <input type="hidden" runat="server" id="hidflng" value="0" />
        <!--收件人经纬度-->
        <input type="hidden" runat="server" id="hidtlat" value="0" />
        <input type="hidden" runat="server" id="hidtlng" value="0" />
        <!--配送距离和配送费-->
        <input type="hidden" runat="server" id="hiddistance" value="0" />
        <input type="hidden" runat="server" id="hidsendfee" value="0" />

        <input type="hidden" runat="server" id="hiduid" value="0" />
        <div class="wrap">
            <div class="place">
                <ul>
                    <li class="place_home"><a href="index.aspx">
                        <img src="images/place_home_icon.jpg" /></a></li>
                    <li>
                        <img src="images/place_icon.jpg" /></li>
                    <li class="htext">跑腿</li>
                </ul>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="fn-clear">
                        <div class="lleft_side">
                            <div class="leftbdcon">
                                <div class="cen_title">
                                    跑腿信息
                                </div>
                                <div class="paotinfo form-list">

                                    <div class="sptext">
                                        <span class="lelabb" style="color: #ff6f00;">物品信息</span><br />
                                        <textarea style="height: 80px; width: 230px; border: 1px solid #ccc;"
                                            runat="server" id="tbInve2" value="请填写商品信息" canbenull="n" reg="\S" tip="请填写商品信息"></textarea>
                                    </div>

                                    <div class="spdd">
                                        <span class="lelabb" style="color: #ff6f00;">服务类型</span><br />
                                        <asp:DropDownList ID="ddlexpressserve" runat="server" Width="150">
                                            <asp:ListItem Value="0">请选择服务</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="spdd">
                                        <span class="lelabb" style="color: #ff6f00;">寄件信息</span>
                                        <input type="text" class="text" value="请输入发件人姓名"
                                            canbenull="n" reg="\S" tip="请输入发件人姓名" runat="server" id="tbUserName" />
                                    </div>
                                    <div class="spdd">
                                        <span class="lelabb" style="line-height: 10px;">&nbsp;</span>
                                        <input type="text" class="text" value="请输入发件人电话"
                                            runat="server" id="tbTel" canbenull="n" reg="\S"  tip="请输入发件人电话" />
                                    </div>
                                    <div class="spdd">
                                        <span class="lelabb" style="line-height: 10px;">&nbsp;</span>
                                        <input type="text"
                                            class="text" value="请输入发件人地址" canbenull="n" reg="\S" tip="请输入发件人地址"  runat="server"
                                            id="tbAddress" onchange="addfrom();" />
                                        <%--<input type="button" value="搜索" onclick="addfrom();" />--%>
                                    </div>
                                    <div class="spdd" style="margin-top: 18px;">
                                        <span class="lelabb" style="color: #ff6f00;">收件信息</span>
                                        <input type="text" class="text" value="请输入收件人姓名"
                                            runat="server" id="tbcallmsg" canbenull="n" reg="\S" tip="请输入收件人姓名" />
                                    </div>
                                    <div class="spdd">
                                        <span class="lelabb" style="line-height: 10px;">&nbsp;</span>
                                        <input type="text" class="text" value="请输入收件人电话"
                                            runat="server" id="tbReveVar" canbenull="n"  reg="\S" tip="请输入收件人电话" />
                                    </div>
                                    <div class="spdd">
                                        <span class="lelabb" style="line-height: 10px;">&nbsp;</span>
                                        <input type="text" class="text" value="请输入收件人地址" canbenull="n"  reg="\S" tip="请输入收件人地址" runat="server" onchange="addto();"
                                            id="tbOorderid" />
                                        <%--<input type="button" value="搜索" onclick="addto();" />--%>
                                    </div>
                                    <div class="spdd">
                                        <span class="lelabb" style="color: #ff6f00;">取件时间</span><input type="text" class="text" value="" style="width: 88px; margin-right: 10px;"
                                            onfocus="WdatePicker({readOnly:true,minDate:'%y-%M-%d'})" canbenull="n"  reg="\S" tip="请选择取件日期" runat="server"
                                            id="tbdate" />
                                        <asp:DropDownList ID="ddl_Time1" class="Runners_jishi_input_4" runat="server">
                                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                            <asp:ListItem Value="11:10">11:10</asp:ListItem>
                                            <asp:ListItem Value="11:20">11:20</asp:ListItem>
                                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                                            <asp:ListItem Value="11:40">11:40</asp:ListItem>
                                            <asp:ListItem Value="11:50">11:50</asp:ListItem>
                                            <asp:ListItem Value="12:00" Selected="True">12:00</asp:ListItem>
                                            <asp:ListItem Value="11:10">12:10</asp:ListItem>
                                            <asp:ListItem Value="11:20">12:20</asp:ListItem>
                                            <asp:ListItem Value="11:30">12:30</asp:ListItem>
                                            <asp:ListItem Value="11:40">12:40</asp:ListItem>
                                            <asp:ListItem Value="11:50">12:50</asp:ListItem>
                                            <asp:ListItem Value="13:00">13:00</asp:ListItem>
                                            <asp:ListItem Value="17:00">17:00</asp:ListItem>
                                            <asp:ListItem Value="17:10">17:10</asp:ListItem>
                                            <asp:ListItem Value="17:20">17:20</asp:ListItem>
                                            <asp:ListItem Value="17:30">17:30</asp:ListItem>
                                            <asp:ListItem Value="17:40">17:40</asp:ListItem>
                                            <asp:ListItem Value="17:50">17:50</asp:ListItem>
                                            <asp:ListItem Value="18:00">18:00</asp:ListItem>
                                            <asp:ListItem Value="18:10">18:10</asp:ListItem>
                                            <asp:ListItem Value="18:20">18:20</asp:ListItem>
                                            <asp:ListItem Value="18:30">18:30</asp:ListItem>
                                            <asp:ListItem Value="18:40">18:40</asp:ListItem>
                                            <asp:ListItem Value="18:50">18:50</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="spdd">
                                        <span class="lelabb" style="color: #ff6f00;">距离</span>
                                        <label id="lbdistance" style="color: red;" runat="server">0</label>公里
                                    </div>
                                    <div class="spdd">
                                        <span class="lelabb" style="color: #ff6f00;">费用</span>
                                        <label id="lbsendfee" style="color: red;" runat="server">0</label>元

                                         跑腿费用每公里
                                        <label id="Label1" style="color: red;"><%=SectionProxyData.GetSetValue(66)%></label>元
                                        <asp:CheckBox ID="cbsendfee" runat="server" onclick="changesendfee(this)" />自填费用
                                    </div>

                                    <div class="spdd divsendfee " style="display:none">
                                        <input type="text" class="text" value="请输入跑腿费用"
                                            runat="server" id="tbsendfee" onblur="tbchangesendfee(this)"
                                            tip="跑腿费用数据格式错误" />
                                    </div>

                                    <div class="spdd">
                                        <span class="lelabb" style="color: #ff6f00;">支付方式</span>
                                        <asp:RadioButtonList ID="rbpaymode" runat="server">
                                            <asp:ListItem Value="1" onclick="payusermoney(this.value);" Enabled="false">支付宝</asp:ListItem>
                                            <asp:ListItem Value="3" onclick="payusermoney(this.value);">余额支付</asp:ListItem>
                                            <asp:ListItem Value="2" onclick="payusermoney(this.value);" Selected="True">发件人支付</asp:ListItem>
                                            <asp:ListItem Value="4" onclick="payusermoney(this.value);">收件人支付</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="spdd" style="display: none;" id="paypassword">
                                        <span>支付密码：</span>
                                        <input class="info_box w_150p" name="" type="password" runat="server" id="tbpaypwd"
                                            maxlength="50" /><br />
                                        <span id="pwdmsg" runat="server">您还没有设置支付密码,点击
                                                <a href="UserHome/PayPwd.aspx" style="color: #ff6000;" runat="server" id="setpaypwd">这里</a>设置.</span>
                                    </div>
                                    <div class="sptext">
                                        <span class="lelabb" style="color: #ff6f00;">备&nbsp;&nbsp;&nbsp;&nbsp;注</span><br />
                                        <textarea style="height: 80px; width: 230px; border: 1px solid #ccc;"
                                            runat="server" id="tbRemark"></textarea>
                                    </div>

                                    <div style="margin: 28px 0; text-align: center">
                                        <asp:Button runat="server" ID="bt_Request1" CssClass="ssubmit_btn" OnClick="btsave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="rrightt_side">
                            <div id='run-mapWarp'>
                                <!--地址搜索-->
                                <h2 class="run-mapSearchH2">
                                    <div class="run-mapSeachTxt">
                                        <input value="请输入写字楼，小区，学校等地址信息" type="text" id="run-searchAddress"
                                            onfocus="if ($('#run-searchAddress').val() == '请输入写字楼，小区，学校等地址信息'){$('#run-searchAddress').val('');}"
                                            onblur="if ($('#run-searchAddress').val() == '') { $('#run-searchAddress').val('请输入写字楼，小区，学校等地址信息');}" />
                                        <i class="run-searchBtn" onclick="go_search();" style="background:url('/images/restaurant-icon.png') no-repeat scroll 0 -140px rgba(0, 0, 0, 0)"></i>
                                        <ul class="searchResult" id="searchResult"></ul>
                                    </div>
                                    <%--<a href="aboutus.aspx?id=38" class="run-mapTips">跑腿须知</a>--%>
                                </h2>

                                <div id="allmap" style='width: 625px; height: 870px'>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <uc2:Foot runat="server" ID="foot" />
    </form>
</body>
</html>
<script src="javascript/Common.js" type="text/javascript"></script>

<script src="javascript/Runner.js" type="text/javascript"></script>

<script src="javascript/ShowDivDialog.js" type="text/javascript"></script>

<script src="javascript/RunnerMap.js?v=20140819" type="text/javascript"></script>

<script src="http://api.map.baidu.com/api?v=2.0&ak=fMnzyhYs0D1cBEl5iGMQ0Dlg" type="text/javascript"></script>


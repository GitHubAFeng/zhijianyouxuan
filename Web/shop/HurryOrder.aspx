<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HurryOrder.aspx.cs" Inherits="shop_HurryOrder" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>催单-<%= SectionProxyData.GetSetValue(3)%></title>
    <link href="css/common.css?v=1" rel="stylesheet" type="text/css" />
    <link href="/css/print.css?v=2" rel="stylesheet" />

    <link href="/javascript/jbox/Skins/jbox.css" rel="stylesheet" />
    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="/Admin/javascript/jquery.PrintArea.js" type="text/javascript"></script>
    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="DatePicker/WdatePicker.js" type="text/javascript"></script>
    <%--<script src="js/soundmanager2.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .havea {
            color: #f39800;
            border: #f39800 1px solid;
            border-radius: 5px;
            padding: 2px 10px;
            float: right;
            font-size: 16px;
            font-weight: bold;
        }

        .havenone {
            color: #ffc000;
            border: #ffc000 1px solid;
            border-radius: 5px;
            padding: 2px 10px;
            float: right;
            font-size: 16px;
            font-weight: bold;
            color: #ffc000;
        }
    </style>

    <script type="text/javascript"> 
        $(document).ready(function () {
            cookeiinit();
            //delayURL();
            $("#order").hide();
        });

        function delayURL() {
            var timebox = $("#time");
            var delay = parseInt(timebox.html());
            if (delay > 0) {
                delay--;
                timebox.html(delay + "");
            }
            setTimeout(delayURL, 1000);
        }

        //soundManager.debugMode = false;
        //soundManager.debugFlash = false;
        //soundManager.url = "soundmanager2.swf";

        function play(flag) {
            $("#time").html(30);
            if (flag == 0) {
                return;
            }
            //var v = document.getElementById("cbSound").checked;
            //if (v == true) {
            //soundManager.play('mySound1', 'notify.mp3');
            //}
        }


        function Soundclike() {
            var soundflag = document.getElementById("cbSound").checked; //true表示有，false 表示没有
            handlecookie("sound", soundflag, { expires: 1, path: "/", secure: false });

        }

        function cookeiinit() {
            //var sound = handlecookie("sound");
            //document.getElementById("cbSound").checked = sound;
        }

        function printorder(id) {
            $('#print_area_'+id).html($('#printdata_'+id).html());
            $("#print_area_"+id).printArea();

        }
        function printset()
        {
            window.open("/images/printset.jpg");
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
                    <div id="order">
                        <!--订单详情-->
                        <div class="orderhistory-tishi">
                            <%--<label>订单提示：</label>
                        <input type="checkbox" id="cbSound" onclick="Soundclike()" /><label for="cbSound">声音提醒</label>--%>
                            <span style="margin-left: 30px;">
                                <label id="time">30</label>秒后自动刷新。
                            <asp:Button runat="server" ID="tbnew" CssClass="btnew" Text="手动刷新" OnClick="Timer1_Tick" />
                            </span>
                            <span id="automsg" runat="server" style="color: blue;"></span>
                        </div>
                    </div>
                    <p class="field switch">
                        处理状态：
                        <input type="radio" id="have_1" name="field6" checked="checked" onclick="gourl('<%=getSortUrl("oe","1") %>    ')" />
                        <label for="radio0" class="cb-label selected"><span>待处理</span></label>
                        <input type="radio" id="have_2" name="field6" onclick="gourl('<%=getSortUrl("oe","2") %>    ')" />
                        <label for="radio1" class="cb-label "><span>已处理</span></label>
                    </p>
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
                        <asp:Repeater ID="rptOrderList" runat="server">
                            <ItemTemplate>
                                <div class="orderlist_box">
                                    <div class="order_info">
                                        <p class="timestate">
                                            <span>立即送达<i>（<%#Eval("OrderDateTime")%>下单）</i></span>
                                            <span><%# WebUtility.TurnPayModel(Eval("paymode")) %></span>
                                            <span class="<%# Convert.ToInt32(Eval("hurhav")) == 0 ? "havenone": "havea"%>" style="line-height: 25px; margin-top: 3px; margin-right: 45px;">
                                                <%# WebUtility.HurryState(Eval("hurhav").ToString())%>
                                                <i></i>
                                            </span>
                                            <a href="OrderDetail.aspx?id=<%#Eval("unid")%>" class="order_check">查看详情</a>
                                        </p>
                                        <p class="ordernb">
                                            <span>#<%# Eval("Unid")%></span>
                                            <span>订单编号：<%#Eval("OrderID")%></span>
                                        </p>
                                    </div>
                                    <ul class="customer_info">
                                        <li>
                                            <%#Eval("OrderRcver")%>
                                            <%#Eval("OrderComm")%>
                                            <%--<span>
                                                <img src="Images/btn-position@2x.png" />1.5km</span>--%>
                                        </li>
                                        <li>送达地址：<%#Eval("AddressText")%></li>
                                    </ul>
                                    <div class="food_info">
                                        <p class="foodshow" onclick="showdetail('<%#  Eval("orderid") %>')">
                                            商品信息
                                            <span>全部商品
                                                <img src="Images/icon-chevron-down.png" />
                                                <img src="Images/icon-chevron-up.png" style="display: none" />
                                            </span>
                                        </p>
                                        <ul class="tr_<%#  Eval("orderid") %> foodlist <%# Convert.ToInt32(Eval("paystate")) == 1 ? "pay": ""%>" style="display: none">
                                            <asp:Repeater runat="server" ID="rptfood" DataSource='<%#Eval("Foodlist") %>'>
                                                <ItemTemplate>
                                                    <li><span><%# Eval("FoodName")%></span>
                                                        <span>￥<%# Eval("OldPrice")%></span>
                                                        <span>x<%# Eval("FCounts")%></span>
                                                        <span>￥<%# Convert.ToDecimal(Eval("OldPrice"))*Convert.ToInt32(Eval("FCounts"))%></span>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <li><span>包装费</span><span>￥<%# Convert.ToDecimal(Eval("packagefee"))%></span></li>
                                            <li><span>配送费</span><span>￥<%# Convert.ToDecimal(Eval("SendFee"))%></span></li>
                                            <li><span>优惠券</span><span style="width: 30%">-￥<%# Convert.ToDecimal(Eval("cardpay"))%></span></li>
                                            <li><span>合计</span><span class="price"><%# Eval("OrderSums")%></span></li>
                                        </ul>
                                    </div>
                                    <div class="knight_info clearfix" style="display: none">
                                        <label>配送信息：</label>
                                        <ul>
                                            <li>骑士已送达<span>送达时间（08-23  11:58）</span></li>
                                            <li>骑手：刘浩然  13545867854</li>
                                        </ul>
                                        <button class="btn btn1">评价骑手</button>
                                    </div>
                                    <p class="bottom_btn">
                                        <button style="<%# Convert.ToInt32(Eval("hurhav"))==0 ? "": "display:none"%>" class="btn" onclick="huOrder(<%#Eval("OrderID") %>,'IsShopSet')" type="button">处理</button>
                                        <button style="<%# Convert.ToInt32(Eval("IsShopSet"))==0 ? "": "display:none"%>" class="btn" onclick="acOrder(<%#Eval("OrderID") %>,'IsShopSet')" type="button">接收</button>
                                        <button style="<%# Convert.ToInt32(Eval("IsShopSet"))==0 ? "": "display:none"%>" class="btn" onclick="opOrder(<%#Eval("OrderID") %>,'IsShopSet')" type="button">拒绝</button>
                                        <button class="btn" type="button" onclick="printorder(<%#Eval("unid") %>)">打印</button>
                                        <button class="btn" type="button" onclick="printset()">打印设置</button>
                                    </p>
                                </div>
                                <div id="print_area_<%#Eval("unid") %>" style="display: none;"></div>
                                <div>
                                    <div id="printdata_<%#Eval("unid") %>" style="display: none">

                                        <div class="printinfo">
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
                                                    <asp:Repeater runat="server" ID="Repeater2" DataSource='<%#Eval("Foodlist") %>'>
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
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div id="noRecord" runat="server" class="no_infor">
                            暂无订单！
                        </div>
                        <div class="pages">
                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                ShowPageIndex="True" PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                TextAfterPageIndexBox=" 页 " Wrap="False" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PrevPageText="上一页">
                            </webdiyer:AspNetPager>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
<script src="/javascript/jbox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var field1 = $("[name='field1']").filter(":checked");
        console.log(field1.attr("id"));
    });

</script>

<script type="text/javascript">
    //显示、隐藏订单详细
    function showdetail(orderid) {
        $(".tr_" + orderid).toggle();
    }

    function gourl(url) {
        window.location = url;
    }

    var refuseorder = null;

    ///提现对像
    function RefuseObject(cid,msgbox){
        this.cid = cid;
        this.msgbox = msgbox;//备注输入款id
    }
    RefuseObject.prototype.SetState = function(state)
    {
        var id = this.cid;
        var msg = $("#tbremark").val();
        if(msg=="")
        {
            alert("请输入拒绝理由");
            return;
        }
        var flag = this.checkData(state);
        if (flag == false) {
            return flag;
        }

        var statemsg = "已打款";
        if (state == 2) {
            statemsg = "拒绝";
        }

        var url = "/ajaxHandler.ashx";
        var para = "t=" + new Date().getTime() + "&id=" + id + "&method=setRefuse&msg=" + escape(msg)+"&state="+state;
        showload_super();
        jQuery.ajax(
        {
            type: "post",
            url: url,
            data: para,
            success: function (msg) {
                hideload_super();
                if(msg=="1")
                {
                    alert("操作成功");
                    $.jBox.close();
                    window.location=window.location;
                }
            }
        })
    }
    RefuseObject.prototype.checkData= function(state)
    {
        //var box = $("#"+this.msgbox);
        //var msg = box.val();
        //if (state == 2 && msg == "") {
        //    alert("请输入拒绝理由");
        //    return false;
        //}
        return true;
    }
    //处理催单
    function huOrder(cid,msgbox) {
        refuseorder = new RefuseObject(cid,msgbox);
        refuseorder.SetState(0);
    }

    //处理订单
    function acOrder(cid,msgbox) {
        refuseorder = new RefuseObject(cid,msgbox);
        refuseorder.SetState(1);
    }

    //处理订单
    function opOrder(cid,msgbox) {
        refuseorder = new RefuseObject(cid,msgbox);
        var html = "<div style='padding: 10px;'> <div>理由：</div><textarea style=\"height: 80px; width: 300px;\" id='tbremark'></textarea><div style=\"margin-top:10px;\">";
        html += "<input type=\"button\" style=\"margin-left: 30px;\" value=\"拒绝\" onclick=\"refuseorder.SetState(2)\" /></div></div>";
       
        $.jBox(html, { title: "拒绝订单", buttons: {}  });
    }


    $(function () {
        var paralist = [];
        var para1 = request("para1").replace(/\/$/, "");
        if (para1 != "") {
            var myparas = para1.split("|");
            for (var i in myparas) {
                var value = myparas[i].replace(/[a-zA-Z]+/, "");
                var key = myparas[i].replace(/[0-9_]+/, "");
                paralist.push({ "key": key, "value": value });
               
                setCurByKey(key, value);
            }
        }

        function setCurByKey(key, value) {
            switch (key) {
                case "oe":
                    $("#have_"+value).attr("checked","checked");
                    break;
                default:
            }
        }
    });
</script>

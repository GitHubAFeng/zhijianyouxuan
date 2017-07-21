<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="Admin_shop_OrderList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>点餐订单管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="/Admin/javascript/soundmanager2.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        $(document).ready(function () { init(); });

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
            cookeiinit();
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的订单!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function SetOk() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要批量设置为处理成功的订单!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return SetConfirm();
        }

        $(document).ready(function () {
            delayURL();
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
                soundManager.play('mySound1', 'notify.mp3');
            }
        }


        function Soundclike() {
            var soundflag = document.getElementById("cbSound").checked; //true表示有，false 表示没有
            handlecookie("sound", soundflag, { expires: 1, path: "/", secure: false });

        }



        function cookeiinit() {
            var sound = handlecookie("sound");
            document.getElementById("cbSound").checked = sound;
        }
        function Dy() {
            var nums = Table.GetChecks();
            document.getElementById("hdDels").value = ArrayToString(nums);

        }
    </script>

    <style type="text/css">
        .orderhistory-tishi {
            -moz-background-clip: border;
            -moz-background-origin: padding;
            -moz-background-size: auto auto;
            background-attachment: scroll;
            background-color: #FFF5CE;
            background-image: none;
            background-position: 0 0;
            background-repeat: repeat;
            border-top-color: #FFE16A;
            border-top-style: solid;
            border-top-width: 1px;
            margin-bottom: 10px;
            padding-bottom: 10px;
            padding-left: 8px;
            padding-top: 10px;
            width: 99%;
        }

        .btnew {
            _padding-top: 3px;

           
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <asp:HiddenField ID="hfflag" runat="server" />
        <asp:HiddenField ID="hdDels" runat="server" />
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <!--banner start-->
            <uc1:TogoBanner runat="server" ID="Banner" />
            <!--banner end-->
            <!--center start-->
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <div class="side-col" id="page:left">
                            <uc4:togoleft ID="togoleft1" runat="server" />
                        </div>

                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>
                                <fieldset class="AdminSearchform">
                                    <legend>查询条件 </legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left"
                                        class="condition_table">
                                        <tr>
                                            <td align="right">
                                                <span style="width: 160px;">订单编号：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tborderid" runat="server" CssClass="j_text" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="width: 160px;">商家ID： </span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbTogoID" runat="server" CssClass="j_text" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="width: 160px;">商家：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="tbTogoName" CssClass="j_text" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="width: 160px;">电话：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbphone" runat="server" CssClass="j_text" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left"
                                        class="condition_table">
                                        <tr>
                                            <td align="right">
                                                <span class="span12" style="width: 160px;">订单时间：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" CssClass="inputclass" ID="tbStartTime" onfocus="WdatePicker({readOnly:true})"
                                                    Width="75"></asp:TextBox>
                                                <asp:Label runat="server" ID="lbTemp" Text="至"></asp:Label>
                                                <asp:TextBox runat="server" ID="tbEndTime" CssClass="inputclass" onfocus="WdatePicker({readOnly:true})"
                                                    Width="75"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="span12" style="width: 160px;">状态：</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlOrderState" Style="width: 80px;" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="所有订单" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList runat="server" ID="ddleatytpe" Style="width: 80px; display: none;" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="所有类型" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="外卖" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="堂食" Value="1"></asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList runat="server" ID="ddlsource" Style="width: 80px;" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="所有来源" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList runat="server" ID="ddlpaymodel" class="j_select">
                                                    <asp:ListItem Text="支付方式" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Value="3">余额支付</asp:ListItem>
                                                    <asp:ListItem Value="4">货到付款</asp:ListItem>
                                                    <asp:ListItem Value="1">支付宝</asp:ListItem>
                                                    <asp:ListItem Value="5">微信支付</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList runat="server" ID="ddlpaystate" class="j_select">
                                                    <asp:ListItem Text="支付状态" Value="-2"></asp:ListItem>
                                                    <asp:ListItem Text="未支付" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="支付成功" Value="1"></asp:ListItem>

                                                </asp:DropDownList>

                                                <asp:DropDownList runat="server" ID="ddlIsShopSet" class="j_select">
                                                    <asp:ListItem Text="商户接收状态" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="未接收" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="已接收" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="拒绝" Value="2"></asp:ListItem>
                                                </asp:DropDownList>



                                            </td>


                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="span12" style="width: 160px;">城市：</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLArea" runat="server" Width="100" AppendDataBoundItems="true"
                                                    class="j_select">
                                                    <asp:ListItem Value="0">所有城市</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click"
                                                    OnClientClick="loading(); return true;" />
                                                <asp:Button runat="server" ID="Button1" class="form-button" Text="导出" OnClick="btExport_Click" />
                                            </td>
                                        </tr>

                                    </table>
                                </fieldset>

                                <fieldset class="AdminSearchform">
                                    <legend>已经完成订单统计</legend>
                                    <div style="clear: both; padding: 10px;">
                                        <span>订单数量:<span style="margin-left: 20px; font-weight: bold; color: Red"
                                            id="lborder" runat="server">0</span><span style="color: red">单</span>
                                        </span>
                                        <span style="margin-left: 20px;">金额:
                                            <span style="font-weight: bold; color: Red" id="lbcount" runat="server">0</span>
                                            <span style="color: red">元</span>
                                        </span>
                                    </div>
                                </fieldset>
                                <div class="orderhistory-tishi">
                                    <label>
                                        订单提示：</label>
                                    <input type="checkbox" id="cbSound" onclick="Soundclike()" /><label for="cbSound">声音提醒</label>


                                    <span style="margin-left: 30px;">
                                        <label id="time" style="color: Red; font-size: 36px;">
                                            30</label>秒后自动刷新。
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
                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                                PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                                Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                            </webdiyer:AspNetPager>
                                        </div>
                                        <div id="sales_order_grid_massaction" style="clear: both;">
                                            <table class="massaction" cellpadding="0" cellspacing="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <a href="#" onclick="javascript:Table.CheckAll()">全选</a> <span class="separator">|</span>
                                                            <a href="#" onclick="javascript:Table.CheckNo()">取消选择</a><span class="separator">|</span>
                                                            <a href="#" onclick="javascript:Table.ReCheck()">反向选择</a><span class="separator">|</span>
                                                            <a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="lbDelsom" OnClientClick="return Del()" OnClick="DelList_Click">删除选定</asp:LinkButton>
                                                            </a><span class="separator">|</span> <a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="LinkButton1" OnClick="ot">按时间排序</asp:LinkButton>
                                                            </a><span class="separator">|</span>
                                                            <a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="LinkButton2" OnClientClick="return SetOk()" OnClick="SetOkList_Click">批量设置处理成功</asp:LinkButton>
                                                            </a>
                                                        </td>
                                                    
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="20" />
                                                    <col width="50" />
                                                    <col width="60" />
                                                    <col width="40" />
                                                    <col width="100" />
                                                    <col width="120" />
                                                    <col width="80" />
                                                    <col width="80" />
                                                    <col width="40" />
                                                    <col width="110" />
                                                  
                                                    <col width="120" />

                                                    <col width="70" />
                                                    <col width="100" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">编号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">用户名</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">商家ID</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">商家</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" class="not-sort"><span class="sort-title">订单时间</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">收件人</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">电话</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">金额</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">地址</span></a></span>
                                                            </th>
                                                          
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">状态-用户-商家-骑士</span></a></span>
                                                            </th>

                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">处理人</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr last">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpOrderlist" runat="server" OnItemCommand="rtpOrderlist_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="">
                                                                        <input name="" id="_inut" value="<%# Eval("Unid")%>" class="massaction-checkbox"
                                                                            type="checkbox">
                                                                        <asp:HiddenField runat="server" ID="hidDataId" Value='<%# Eval("Unid")%>' />
                                                                        <asp:HiddenField runat="server" ID="hidOrderId" Value='<%# Eval("Unid")%>' />
                                                                    </td>
                                                                    <td class="">
                                                                        <a href='OrderDetail.aspx?id=<%# Eval("Unid")%>'>
                                                                            <%#Eval("orderid")%></a>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("CustomerName")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("TogoId")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("TogoName") %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("OrderDateTime").ToString()%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("OrderRcver") %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("CallPhoneNo")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("OrderSums")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("AddressText")%>
                                                                    </td>
                                                                   
                                                                    <td class="">
                                                                        <%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>-<%#WebUtility.UserOrderState(Eval("paystate").ToString(),Eval("hurhav").ToString(),Eval("OrderStatus").ToString(),Eval("OrderChecker").ToString())%>-<%# Hangjing.WebCommon.WebHelper.TurnOrderReceiveState(Eval("isshopset")) %>-<%#WebUtility.TurnOrderSendState(Eval("sendstate").ToString(),Eval("IsShopSet").ToString())%>
                                                                    </td>

                                                                    <td class="">
                                                                        <%#Eval("writer")%>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <a href='OrderDetail.aspx?id=<%#Eval("Unid") %>'>编辑</a> &nbsp;
                                                                    <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("Unid")%>' OnClientClick="return DelConfirm();"
                                                                        runat="server" ID="delNew">删除</asp:LinkButton>&nbsp;
                                                                    
                                                                       
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="scott">
                                            <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="AspNetPager1">
                                            </webdiyer:AspNetPager>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
                <!--foot start-->
                <uc2:Foot runat="server" ID="FootUC" />
                <!--foot end-->
            </div>
        </div>
    </form>
</body>
</html>

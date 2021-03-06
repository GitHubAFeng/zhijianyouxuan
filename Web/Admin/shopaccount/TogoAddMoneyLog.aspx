﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TogoAddMoneyLog.aspx.cs"
    Inherits="Admin_Account_TogoAddMoneyLog" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="TogoAddMoneyLog" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商家充值记录-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/style.css" rel="stylesheet" type="text/css" />--%>
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function() { $("#loading-mask").hide(); });
        $(document).ready(function() { init(); });

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的商家添加记录!");
                return false;
            }
            document.getElementById("hidDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidDels" runat="server" />
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <uc3:TogoAddMoneyLog ID="TogoAddMoneyLog1" runat="server" />
                    &nbsp;<div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>
                                    <fieldset class="AdminSearchform">
                                        <legend>查询条件 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <span>商家编号</span>
                                                    <asp:TextBox ID="tb_UserId" runat="server"></asp:TextBox>
                                                </td>
                                                <%-- <td>
                                                  <span>商家名称</span>
                                                  <asp:TextBox ID="tb_UserName" runat="server"></asp:TextBox>
                                                </td>--%>
                                                <td>
                                                    <span>状态</span>
                                                    <asp:DropDownList ID="ddl_States" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">新增充值</asp:ListItem>
                                                        <asp:ListItem Value="1">充值成功</asp:ListItem>
                                                        <asp:ListItem Value="-1">充值失败</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="filter-actions a-right">
                                                    <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
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
                                                        <a href="TogoAddMoneyLogDetail.aspx">添加</a> <span class="separator">|</span> <a href="#"
                                                            onclick="javascript:Table.CheckAll()">全选</a> <span class="separator">|</span>
                                                        <a href="#" onclick="javascript:Table.CheckNo()">取消选择</a><span class="separator">|</span>
                                                        <a href="#" onclick="javascript:Table.ReCheck()">反向选择</a><span class="separator">|</span>
                                                        <a href="#" onclick="return false">
                                                            <asp:LinkButton runat="server" ID="lbDelsom" OnClientClick="return Del()" OnClick="DelList_Click">删除选定</asp:LinkButton>
                                                        </a>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <col class="a-center" width="20px" />
                                                <col width="40" />
                                                <col />
                                                <col width="80" />
                                                <col width="80" />
                                                <col width="100" />
                                                <thead>
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                    class="sort-title">编号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                    class="sort-title">商家名称</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                    class="sort-title">充值金额</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                                    class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                                    class="sort-title">支付类型</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                                    class="sort-title">支付时间</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                                    class="sort-title">支付状态</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpTogoAddMoneyLog" runat="server" OnItemCommand="rtpTogoAddMoneyLog_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="" width="20px">
                                                                        <input class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("dataid") %>'>
                                                                        </input>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("UserId")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# WebUtility.Left(Eval("TogoName"),3)%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("AddMoney")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# State(Eval("State"))%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# PayType(Eval("PayType"))%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("PayDate")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# PayState(Eval("PayState"))%>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <a href='TogoAddMoneyLogDetail.aspx?id=<%#Eval("DataId") %>'>编辑</a> |<asp:LinkButton
                                                                            ID="lbdel" runat="server" CommandArgument='<%# Eval("DataId") %>' CommandName="Del"
                                                                            OnClientClick="return DelConfirm();">删除</asp:LinkButton>
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
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <uc2:Foot runat="server" ID="FootUC" />
    </div>
    </form>
</body>
</html>

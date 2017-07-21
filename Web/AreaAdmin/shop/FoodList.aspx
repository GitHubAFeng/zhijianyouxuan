﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodList.aspx.cs" Inherits="AreaAdmin_shop_ShopFoodList" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>餐品列表－<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
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
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要进行操作的餐品!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function Fun() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要进行操作的餐品!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        function closeDiv() {
            var obj = document.getElementById("DivMessage");
            obj.visible
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }
    </script>

    <script language="javascript" type="text/javascript">

        function GotoAddFoodSort() {
            window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
        function GotoAddFood() {
            window.location.href = "FoodDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
        function GotoAddFoodSort() {
            window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
        function GotoUpFood() {
            window.location.href = "FoodList.aspx?tid=" + document.getElementById("hidTogoId").value + "&type='y'";
        }
        function GotoDownFood() {
            window.location.href = "FoodList.aspx?tid=" + document.getElementById("hidTogoId").value + "&type='n'";
        }
        function GotoFoodList() {
            window.location.href = "FoodList.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoFoodSort() {
            window.location.href = "FoodSortList.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <asp:HiddenField runat="server" ID="hidTogoId" />
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
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
                        <uc3:left runat="server" ID="left" />
                    </div>
                    <div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>

                                    <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none">
                                            <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" class="tab-item-link ">
                                                <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                            </li>
                                            <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                            <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link active"><span>
                                                <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                            <li><a href="Distancepaylist.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                            <li><a href="ShopLocal.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>

                                             <li><a href="AddPrinter.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>打印机</span> </a></li>

                                        </ul>

                                    <fieldset class="AdminSearchform">
                                        <legend>查询条件 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <span class="span12">餐品名称：</span>
                                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="inputclass" />
                                                </td>
                                                <td>
                                                    <span class="span12">餐馆名称：</span>
                                                    <asp:TextBox ID="tbshop" runat="server" CssClass="inputclass" />
                                                </td>
                                                <td class="filter-actions a-right">
                                                    &nbsp;
                                                    <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ul class="FunctionUl" id="divop" runat="server">
                                            <li>
                                                <button type="button" class="scalable " onclick="GotoAddFood()" style="">
                                                    <span>添加餐品</span></button></li>
                                            <li>
                                                <button type="button" class="scalable " onclick="GotoAddFoodSort()" style="">
                                                    <span>添加餐品分类</span></button></li>
                                            <li>
                                                <button type="button" class="scalable " onclick="GotoFoodSort()" style="">
                                                    <span>查看餐品分类</span></button></li>
                                            <li>
                                                <button type="button" class="scalable " onclick="GotoFoodList()" style="">
                                                    <span>查看所有餐品</span></button></li>


                                             <li>
                                                <button type="button" class="scalable " onclick="gourl('Foodimport.aspx?tid=<%= Hangjing.Common.HjNetHelper.GetQueryInt("tid",0) %>    ')"  style="">
                                                    <span>批量导入</span></button></li>
                                        </ul>
                                    </fieldset>
                                    <div class="scott">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                            PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" 页 "
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
                                                        </a><span class="separator">|</span><a href="#" onclick="return false">
                                                            <asp:LinkButton runat="server" ID="lbDownFood" OnClientClick=" return Fun()" OnClick="lbDownFood_Click">批量下架</asp:LinkButton>
                                                        </a><span class="separator">|</span><a href="#" onclick="return false">
                                                            <asp:LinkButton runat="server" ID="lbUpFood" OnClientClick="  return Fun()" OnClick="lbUpFood_Click">批量上架</asp:LinkButton>
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
                                                <col class="a-center" width="5%" />
                                                <col />
                                                <col width="10%" />
                                                <col width="10%" />
                                                <col width="15%" />
                                                <col width="5%" />
                                                 <col width="5%" />
                                                <col width="6%" />
                                                <col width="10%" />
                                                <thead>
                                                    <tr class="headings">
                                                        <th>
                                                            <span class="nobr">&nbsp;</span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">餐品名称</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">拼音</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">所属分类</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">所在餐馆</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">价格</span></a></span>
                                                        </th>
                                                          <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">打包费</span></a></span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">状态</span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">操作</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rptFoodlist" runat="server" OnItemCommand="rptUserList_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="" width="20px">
                                                                    <input name="" id="_inut" value="<%# Eval("Unid")%>" class="massaction-checkbox"
                                                                        type="checkbox">
                                                                    <asp:HiddenField ID="hdDels" runat="server" Value='<%# Eval("Unid")%>' />
                                                                    <asp:HiddenField ID="HdFoodNo" runat="server" Value='<%# Eval("FoodNo")%>' />
                                                                </td>
                                                                <td class="">
                                                                    <a href='FoodDetail.aspx?id=<%#Eval("Unid")%>&tid=<%# Eval("FPMaster") %>'>
                                                                        <%# Eval("FoodName")%></a>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("FoodNamePy")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("SortName")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("togoName")%>
                                                                </td>
                                                                <td class="">
                                                                    ￥<%# Eval("FPrice")%>
                                                                </td>
                                                                 <td class="">
                                                                    ￥<%# Eval("fullPrice")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("InUse").ToString() =="n"? "已下架":"正常"%>
                                                                </td>
                                                                <td class=" last">
                                                                    <a href='FoodDetail.aspx?id=<%#Eval("Unid")%>&tid=<%# Eval("FPMaster") %>'>编辑</a>
                                                                    <span class="separator">|</span>
                                                                    <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("Unid")%>' OnClientClick="return DelConfirm();"
                                                                        runat="server" ID="del">删除</asp:LinkButton>
                                                                    <span class="separator">
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
    </div>
    <uc2:Foot runat="server" ID="FootUC" />
    </form>
</body>
</html>

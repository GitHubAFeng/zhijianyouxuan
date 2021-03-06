﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetFood.aspx.cs" Inherits="AreaAdmin_shop_SetFood" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>餐品设置－<%= WebUtility.GetMyName()%></title>
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
            //Table = new CheckTable("grid_table");
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
                                    <fieldset class="AdminSearchform">
                                        <legend>查询条件 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <span class="span12">餐品名称：</span>
                                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="inputclass" />
                                                </td>
                                                <td>
                                                    <span class="span12">商家名称：</span>
                                                    <asp:TextBox ID="tbshop" runat="server" CssClass="inputclass" />
                                                </td>
                                                <td class="filter-actions a-right">
                                                    <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ul class="FunctionUl">
                                            <li>
                                                <asp:Button runat="server" ID="btSave" class="form-button" Text="提交" OnClick="btSave_Click" OnClientClick="loading(); return true;" /></li>
                                            <li>
                                                <asp:Button runat="server" ID="btSetStock" class="form-button" Text="更新库存" OnClick="btSetStock_Click" OnClientClick="loading(); return true;"  /></li>
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
                                                <col />
                                                <col width="20%" />
                                                <col width="15%" />
                                                <col width="15%" />
                                                <col width="15%" />
                                                <col width="15%" />
                                                <thead>
                                                    <tr class="headings">
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">餐品名称</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">所在商家</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">剩余库存</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">每日最大供应</span></a></span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">是否推荐</span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">是否特价</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rptFoodlist" runat="server" OnItemCommand="rptUserList_ItemCommand"
                                                        OnItemDataBound="rptFoodlist_ItemDataBound1">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="">
                                                                    <a href='FoodDetail.aspx?id=<%#Eval("Unid")%>&tid=<%# Eval("FPMaster") %>'>
                                                                        <%# Eval("FoodName")%></a>
                                                                    <asp:HiddenField runat="server" ID="hidUnid" Value='<%# Eval("Unid")%>' />
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("togoName")%>
                                                                </td>
                                                                <td class="">
                                                                    <asp:TextBox runat="server" ID="tbRemains" Text='<%# Eval("Remains")%> '></asp:TextBox>
                                                                </td>
                                                                <td class="">
                                                                    <asp:TextBox runat="server" ID="tbMaxPerDay" Text='<%# Eval("MaxPerDay")%> '></asp:TextBox>
                                                                </td>
                                                                <td class="">
                                                                    <asp:RadioButtonList runat="server" ID="rblIsRecommend" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td class="">
                                                                    <asp:RadioButtonList runat="server" ID="rblIsSpecial" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
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

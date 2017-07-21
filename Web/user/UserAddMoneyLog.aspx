<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAddMoneyLog.aspx.cs"
    Inherits="UserHome_UserAddMoneyLog" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagName="TopBanner" TagPrefix="uc1" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register Src="~/Foot.ascx" TagName="Foot" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-用户充值-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>

    <
    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/Building.js" type="text/javascript"></script>

    <script src="../Admin/javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="JavaScript/togobuilding.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }


        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style2
        {
            height: 38px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <uc1:TopBanner runat="server" ID="TogoBanner1" />
        <div class="warp" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <uc2:LeftBanner ID="LeftBanner1" runat="server" />
                    &nbsp;<div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="rightmenu_cont" style="margin-top: 0;">
                                    <h1 class="topbg">
                                        会员充值</h1>
                                    <div class="main-col-inner">
                                        <div class="listorder">
                                            <b>查询条件 </b>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <span>用户编号</span>
                                                        <asp:TextBox ID="tb_UserId" runat="server"></asp:TextBox>
                                                    </td>
                                                    <%--<td>
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
                                        </div>
                                        <div id="sales_order_grid_massaction" style="clear: both;">
                                        </div>
                                        <div class="usermima" style="clear: both; padding-left: 0pt;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                                style="border: 1px solid #ccc;">
                                                <asp:Repeater ID="rtpUserAddMoneyLog" runat="server" OnItemCommand="rtpUserAddMoneyLog_ItemCommand">
                                                    <HeaderTemplate>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                class="sort-title">编号</span></a></span>
                                                            </th>
                                                            <%--<th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                 class="sort-title">用户名称</span></a></span>
                                                            </th>--%>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                class="sort-title">充值金额</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                <span class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                <span class="sort-title">支付类型</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                <span class="sort-title">支付时间</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                <span class="sort-title">支付状态</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">操作</span>
                                                            </th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="pointer" title="">
                                                            <td class="">
                                                                <%# Eval("UserId")%>
                                                            </td>
                                                           <%-- <td class="">
                                                                <%# WebUtility.Left(Eval("TogoName"),3)%>
                                                            </td>--%>
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
                                                                <a href='UserAddMoneyLogDetail.aspx?id=<%#Eval("UserId") %>'>查看</a>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " UrlPagingTarget="_self" UrlPageIndexName="p" UrlPageSizeName="s"
                                                UrlPaging="True" PageIndexBoxClass="flattext" ShowPageIndex="True" PageSize="5"
                                                SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                                Wrap="False">
                                            </webdiyer:AspNetPager>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <uc3:Foot runat="server" ID="FootUC" />
    </div>
    </form>
</body>
</html>

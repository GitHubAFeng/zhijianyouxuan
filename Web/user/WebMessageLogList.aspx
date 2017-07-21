<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebMessageLogList.aspx.cs"
    Inherits="user_WebMessageLogList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-站内信-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

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

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidDels" runat="server" />
    <top:banner ID="Banner1" runat="server" />
    <div class="warp">
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp_con">
            <div id="page:main-container">
                <uc2:LeftBanner ID="LeftBanner1" runat="server" />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="rightmenu_cont">
                            <h1 class="topbg">
                                站内信</h1>
                            <div class="main-col-inner">
                                <div class="listorder">
                                    <b>查询条件 </b>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span>标题</span>
                                                <asp:TextBox ID="tbKeyName" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="filter-actions a-right">
                                                <asp:Button runat="server" ID="Button1" class="subBtn" Text="搜索" OnClick="btSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="sales_order_grid_massaction" style="clear: both;">
                                </div>
                                <div class="usermima" style="clear: both; padding-left: 0pt;">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                        style="border: 1px solid #ccc;">
                                        <asp:Repeater ID="rptpWebMessageLog" runat="server" OnItemCommand="rptpWebMessageLog_ItemCommand">
                                            <HeaderTemplate>
                                                <tr class="headings">
                                                    <th>
                                                        <span class="nobr">&nbsp;</span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                            class="sort-title">标题</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                            class="sort-title">用户名</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                            class="sort-title">发送时间</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                            class="sort-title">状态</span></a></span>
                                                    </th>
                                                    <th class="no-link last">
                                                        <span class="nobr">操作</span>
                                                    </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="pointer" title="">
                                                    <td class="" width="20px">
                                                        <input class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("DataId") %>'>
                                                        </input>
                                                    </td>
                                                    <td class="">
                                                        <%# WebUtility.Left(Eval("Title"), 18)%>
                                                    </td>
                                                    <td class="">
                                                        <%# Eval("UserName")%>
                                                    </td>
                                                    <td class="">
                                                        <%# Convert.ToDateTime(Eval("AddDate")).ToShortDateString()%>
                                                    </td>
                                                    <td class="">
                                                        <%# Convert.ToInt32(Eval("Status")) == 1 ? "已查看" : "未查看"%>
                                                    </td>
                                                    <td class=" last">
                                                        <a href='WebMessageLogDetail.aspx?id=<%#Eval("DataId") %>'>查看</a>
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
        <foot:foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>

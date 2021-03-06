﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailGsmRecordList.aspx.cs"
    Inherits="shop_EmailGsmRecordList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>营销记录管理-<%= WebUtility.GetMyName()%></title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="Style/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <style type="text/css">
        .style1 {
            width: 60px;
        }

        .style2 {
            width: 51px;
        }

        select {
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="warp">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="warp_con">
                        <uc2:LeftBanner runat="server" ID="Left" />
                        <div class="rightmenu_cont">
                            <h1 class="topbg">营销记录管理</h1>
                            <div class="listorder">
                                <fieldset class="AdminSearchform">
                                    <legend>查询条件 </legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="padding-left: 20px;">
                                        <tr>
                                            <td>
                                                <span>状态</span>
                                                <asp:DropDownList ID="ddl_States" runat="server" Width="60px">
                                                    <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                                    <asp:ListItem Value="0">新增</asp:ListItem>
                                                    <asp:ListItem Value="1">进行中</asp:ListItem>
                                                    <asp:ListItem Value="2">完成</asp:ListItem>

                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <span>类型</span>
                                                <asp:DropDownList ID="ddl_Sendtype" runat="server" Width="60px">
                                                    <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                                    <asp:ListItem Value="1">短信</asp:ListItem>
                                                    <asp:ListItem Value="2">邮件</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="filter-actions a-right">
                                                <asp:Button runat="server" ID="btSearch" class="subBtn" Text="搜索" OnClick="btSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                style="border: 1px solid #ccc;">
                                <asp:Repeater ID="rtpEmailRec" runat="server" OnItemCommand="rtpEmailRec_ItemCommand">
                                    <HeaderTemplate>
                                        <tr class="headings">
                                            <th>
                                                <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                    class="sort-title">编号</span></a></span>
                                            </th>
                                            <th>
                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                    class="sort-title">消费金额</span></a></span>
                                            </th>
                                            <th>
                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                    class="sort-title">类型</span></a></span>
                                            </th>

                                            <th>
                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                    class="sort-title">添加时间</span></a></span>
                                            </th>

                                            <th>
                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc"><span
                                                    class="sort-title">状态</span></a></span>
                                            </th>
                                            <th class="no-link last" colspan="2">
                                                <span class="nobr">操作</span>
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="pointer" title="">
                                            <td>
                                                <%# Eval("DataId")%>
                                            </td>
                                            <td>￥<%# Eval("DelMoney")%>
                                            </td>
                                            <td>
                                                <%# SentType(Eval("SentType"))%>
                                            </td>

                                            <td>
                                                <%# Convert.ToDateTime(Eval("AddDate")).ToLongDateString() %>
                                            </td>

                                            <td>
                                                <%# showState(Eval("Status"))%>
                                            </td>
                                            <td class=" last">
                                                <a href='EmailGsmRecDeTail.aspx?id=<%#Eval("DataId") %>'>查看</a> |
                                        <asp:LinkButton ID="lbtdelete" runat="server" CommandName="del" CommandArgument='<%#Eval("DataID")%>'
                                            OnClientClick="return confirm('确定要删除吗?');">&nbsp;删除&nbsp;</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="pages">
                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                    HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                    TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                    ShowPageIndex="True" PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                    TextAfterPageIndexBox=" 页 " Wrap="False">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

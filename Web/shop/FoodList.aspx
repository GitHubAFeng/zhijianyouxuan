<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodList.aspx.cs" Inherits="shop_FoodList" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>餐品列表-<%= WebUtility.GetWebName()%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

</head>

<body>
    <form id="form1" runat="server">
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <div class="shop_main">
                        <div class="main-content">
                            <div class="shop_menu">
                                <ul>
                                    <li><a href="FoodSortList.aspx?">餐品类别列表</a></li>
                                    <li><a href="FoodSortDetail.aspx">添加餐品类别</a></li>
                                    <li class="cur"><a href="FoodList.aspx">餐品列表</a></li>
                                    <li><a href="FoodDetail.aspx">添加餐品</a></li>
                                    <li><a href="Foodimport.aspx">批量导入</a></li>

                                </ul>
                            </div>
                            <%--<h1 class="topbg">餐品列表</h1>--%>
                            <div class="listorder" style="margin-bottom: 15px;">
                                <ul>
                                    <li><span>名称</span>
                                        <input id="tbKeyword" runat="server" name="workAddr" autocomplete="off" type="text" class="text" />
                                    </li>
                                    <li>
                                        <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="subBtn" OnClick="btSearch_Click" />
                                    </li>
                                </ul>
                            </div>

                            <div class="clear"></div>
                            <div class="usermima">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                    style="border: 1px solid #ccc;">
                                    <asp:Repeater ID="rptFoodList" runat="server" OnItemCommand="rptFoodList_ItemCommand">
                                        <HeaderTemplate>
                                            <tr>
                                                <th>餐品名称
                                                </th>
                                                <th style="width: 15%">类别
                                                </th>
                                                <th style="width: 10%">价格
                                                </th>
                                                <th style="width: 12%">打包费
                                                </th>
                                                <th style="width: 10%">状态
                                                </th>
                                                <th style="width: 20%">操作
                                                </th>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("FoodName")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SortName")%>
                                                </td>
                                                <td>
                                                    <%#Eval("FPrice")%>
                                                </td>
                                                <td class="">￥<%# Eval("fullPrice")%>
                                                </td>
                                                <td>
                                                    <%#Eval("InUse").ToString().Trim() == "n" ? "下线" : "上线"%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtdelete" runat="server" CommandName="del" OnClientClick="return confirm('确定在删除吗?');"
                                                        CommandArgument='<%#Eval("Unid")%>'>删除</asp:LinkButton>
                                                    |
                                            <asp:LinkButton
                                                ID="lbtupdate" runat="server" CommandName="update" CommandArgument='<%#Eval("Unid")%>'>修改</asp:LinkButton>
                                                    |
                                            <asp:LinkButton
                                                ID="LinkButton1" runat="server" CommandName="set" CommandArgument='<%#Eval("Unid")%>'><%#Eval("InUse").ToString().Trim() == "n" ? "上线" : "下线"%></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div id="noRecord" runat="server" class="no_infor">
                                暂无餐品！
                            </div>
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

                </div>
            </div>
        </div>
    </form>
</body>
</html>

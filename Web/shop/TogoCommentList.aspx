<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TogoCommentList.aspx.cs"
    Inherits="shop_TogoCommentList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收支记录-<%= SectionProxyData.GetSetValue(3)%></title>

    <link rel="stylesheet" type="text/css" href="/user/css/user_center.css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/userinfo.css" rel="stylesheet" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="DatePicker/WdatePicker.js" type="text/javascript"></script>

</head>

<body>
    <form id="form1" runat="server">
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="shop_main">
                                <div class="main-content">
                                    <%-- <h1 class="topbg">评论列表</h1>--%>
                                    <div style="margin-top: 10px;">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                            style="border: 1px solid #ccc;">
                                            <asp:Repeater ID="rptTogoOpinionList" runat="server" OnItemCommand="rptTogoOpinionList_ItemCommand">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th>编号
                                                        </th>
                                                        <th>评论者
                                                        </th>
                                                        <th>评论时间
                                                        </th>
                                                        <th>是否查看
                                                        </th>
                                                        <th style="border-right: 0;">操作
                                                        </th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="">
                                                            <%#Eval("dataid")%>
                                                        </td>
                                                        <td class="">
                                                            <%# Eval("UserName")%>
                                                        </td>
                                                        <td class="">
                                                            <%#Convert.ToDateTime(Eval("posttime")).ToShortDateString()%>
                                                        </td>
                                                        <td class="">
                                                            <%# isSee( Eval("rtime").ToString())%>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtdelete" runat="server" CommandName="del" CommandArgument='<%#Eval("DataID")%>'
                                                                OnClientClick="return confirm('确定要删除吗?');">删除</asp:LinkButton>
                                                            |
                                                    <asp:LinkButton ID="lbtupdate" runat="server" CommandName="update" CommandArgument='<%#Eval("DataID")%>'>查看</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <div id="noRecord" runat="server" class="no_infor">
                                            暂无评论！
                                        </div>
                                        <div class="pages">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                                ShowPageIndex="True" PageSize="10" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                                TextAfterPageIndexBox=" 页 " Wrap="False">
                                            </webdiyer:AspNetPager>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </form>
</body>
</html>


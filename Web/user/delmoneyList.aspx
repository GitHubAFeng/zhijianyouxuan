<%@ Page Language="C#" AutoEventWireup="true" CodeFile="delmoneyList.aspx.cs" Inherits="UserHome_delmoneyList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>帐户消费记录-<%= WebUtility.GetWebName()%></title>

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp_con">
            <uc2:LeftBanner runat="server" ID="Left" />
            <div class="rightmenu_cont">
                <div class="rightmenu_cen">
                     <h1 class="topbg">帐户消费记录</h1>
                    <div class="listorder">
                        <ul>
                            <li><span>
                                <asp:Label runat="server" ID="lbUserName"></asp:Label></span> 您好，您的帐户中目前共有 <span class="orange"><asp:Label
                                    runat="server" ID="lbMoney"></asp:Label>
                                    元</span></li>
                        </ul>
                    </div>
                    <div class="clear">
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="usermima" style="clear: both; padding-left: 0">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                    style="border: 1px solid #ccc;">
                                    <tr>
                                        <th>消费时间
                                        </th>
                                        <th>消费金额
                                        </th>
                                        <th>消费说明
                                        </th>
                                    </tr>
                                    <asp:Repeater ID="rptPointCount" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("AddDate")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DelMoney")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BuyItem")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <div id="noRecord" runat="server" class="no_infor">
                                    暂无任何消费记录！
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <foot:foot runat="server" ID="foot" />
    </form>
</body>
</html>

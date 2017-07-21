<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyPointCount.aspx.cs" Inherits="UserHome_MyPointCount" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-我的积分记录-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="javascript/DatePicker/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <style type="text/css">
        .mask
        {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 150%;
            filter: alpha(opacity=50);
            -moz-opacity: 0.5;
            opacity: 0.5;
            background: #ccc;
            z-index: 1 !important;
        }
        .imgview
        {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <top:banner ID="Banner1" runat="server" />
    <uc1:banner ID="Banner2" runat="server" />
    <div class="warp">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc2:LeftBanner runat="server" ID="Left" />
                <div class="rightmenu_cont">
                  <div class="rightmenu_cen">
                    <h1 class="topbg">
                        <span style='float: right; padding-right: 20px; display: none;'><a href="../Gift/GiftList.aspx"
                            target="_blank">我要兑换</a></span>我的积分记录</h1>
                    <div class="usermima">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                            style="border: 1px solid #ccc;">
                            <asp:Repeater ID="rptPointCount" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th>
                                            积分数
                                        </th>
                                        <th>
                                            得分原因
                                        </th>
                                        <th>
                                            得分时间
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("Point") %>
                                        </td>
                                        <td>
                                            <%#Eval("event") %>
                                        </td>
                                        <td>
                                            <%# Convert.ToDateTime( Eval("Time")).ToShortDateString() %>
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
                                    暂无数据！
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
    <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

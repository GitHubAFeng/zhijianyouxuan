<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDelMoneyLogDetail.aspx.cs"
    Inherits="UserHome_UserDelMoneyLogDetail" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-用户消费详情-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>
    <link href="../css/common.css" rel="stylesheet" type="text/css"></link>

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/Building.js" type="text/javascript"></script>

    <script src="../Admin/javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="../Admin/javascript/Common.js" type="text/javascript"></script>

    <script src="JavaScript/togobuilding.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function() { $("#loading-mask").hide(); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <top:banner ID="Banner1" runat="server" />
    <div class="warp">
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp_con">
            <uc2:LeftBanner ID="LeftBanner1" runat="server" />
            <div class="rightmenu_cont">
                <h1 class="topbg">
                    用户消费详情信息</h1>
                <div class="usermima">
                    <ul>
                        <li><span class="left_span">商家编号:</span>
                            <asp:Label ID="lbUserId" runat="server"></asp:Label>
                        </li>
                        <%--  <li><span class="left_span">商家名称：</span>
                        <asp:Label ID="lbTogoName" runat="server"></asp:Label>
                    </li>--%>
                        <li><span class="left_span">消费金额：</span>
                            <asp:Label ID="lbDelMoney" runat="server"></asp:Label>
                        </li>
                        <li><span class="left_span">消费名目：</span>
                            <asp:Label ID="lbBuyItem" runat="server"></asp:Label>
                        </li>
                        <li><span class="left_span">新增时间：</span>
                            <asp:Label ID="lbNewAdddate" runat="server"></asp:Label>
                        </li>
                        <br />
                    </ul>
                </div>
                <div style="margin-bottom: 20px; float: right; margin-right: 60px;">
                    <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="返回订单列表" />
                </div>
            </div>
        </div>
        <foot:foot runat="server" ID="Foot" />
    </div>
    </form>
</body>
</html>

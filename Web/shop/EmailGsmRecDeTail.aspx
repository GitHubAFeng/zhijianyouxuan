<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailGsmRecDeTail.aspx.cs"
    Inherits="shop_EmailGsmRecDeTail" %>

<%@ Register Src="~/shop/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>营销记录管理记录-<%= WebUtility.GetMyName()%></title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="Style/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });
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
        <div class="warp">
            <div class="warp_con">
                <uc2:LeftBanner ID="LeftBanner1" runat="server" />
                <div class="rightmenu_cont">
                    <h1 class="topbg">营销记录管理记录</h1>
                    <div class="usermima">
                        <ul>
                            <li><span class="left_span">编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号:</span>
                                <asp:Label ID="lbDataId" runat="server"></asp:Label>
                            </li>
                            <li><span class="left_span">消费金额：</span>
                                <asp:Label ID="lbDelMoney" runat="server"></asp:Label>
                            </li>
                            <li><span class="left_span">类&nbsp;&nbsp;&nbsp;&nbsp;型：</span>
                                <asp:Label ID="lbSentType" runat="server"></asp:Label>
                            </li>
                            <li><span class="left_span">内&nbsp;&nbsp;&nbsp;&nbsp;容：</span>
                                <asp:Label ID="lbContent" runat="server"></asp:Label>
                            </li>
                            <li><span class="left_span">添加时间：</span>
                                <asp:Label ID="lbAddDate" runat="server"></asp:Label>
                            </li>
                            <li><span class="left_span">用户ID列表：</span>
                                <asp:Label ID="lbUserIdList" runat="server"></asp:Label>
                            </li>
                            <li><span class="left_span">状&nbsp;&nbsp;&nbsp;&nbsp;态：</span>
                                <asp:Label ID="lbStatus" runat="server"></asp:Label>
                            </li>
                        </ul>
                    </div>
                    <div style="margin-bottom: 20px; float: right; margin-right: 200px;">
                        <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="返回列表" class='subBtn' />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAddMoneyLogDetail.aspx.cs"
    Inherits="UserHome_UserAddMoneyLogDetail" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagName="TopBanner" TagPrefix="uc1" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register Src="~/Foot.ascx" TagName="Foot" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员中心-用户充值详情-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/Building.js" type="text/javascript"></script>

    <script src="../Admin/javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="../Admin/javascript/Common.js" type="text/javascript"></script>

    <script src="JavaScript/togobuilding.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        
        $(window).load(function(){$("#loading-mask").hide();});
        function reset()
        {
            var list = document.getElementsByName("invalue");
            for (var i = 0 ; i < list.length ; i++)
            {
                list.item(i).value = "";
            }
        } 
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <uc1:TopBanner runat="server" ID="Banner" />
    <div class="warp_con">
        <uc2:LeftBanner ID="LeftBanner1" runat="server" />
        <div class="rightmenu_cont">
            <h1 class="topbg">
                用户充值详情信息</h1>
                 <div class="usermima">
                  <ul>
                    <li><span class="left_span">用户编号:</span>
                        <asp:Label ID="lbUserId" runat="server"></asp:Label>
                    </li>
                   <%-- <li><span class="left_span">用户名称：</span>
                        <asp:Label ID="lbTogoName" runat="server"></asp:Label>
                    </li>--%>
                    <li><span class="left_span">充值金额：</span>
                        <asp:Label ID="lbAddMoney" runat="server"></asp:Label>
                    </li>
                    <li><span class="left_span">状态：</span>
                        <asp:Label ID="lbState" runat="server"></asp:Label>
                    </li>
                    <li><span class="left_span">支付类型：</span>
                        <asp:Label ID="lbPayType" runat="server"></asp:Label>
                    </li>
                    <li><span class="left_span">支付时间：</span>
                        <asp:Label ID="lbPayDate" runat="server"></asp:Label>
                    </li>
                    <li><span class="left_span">支付状态：</span>
                        <asp:Label ID="lbPayState" runat="server"></asp:Label>
                    </li>
                </ul>
            </div>
            <div style="margin-bottom: 20px; float:right; margin-right: 200px;">
                <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="返回订单列表" />
            </div>
        </div>
    </div>
    <uc3:Foot runat="server" ID="Foot" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recharge.aspx.cs" Inherits="UserHome_Recharge" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>账户充值 -
        <%= WebUtility.GetPublishName()%></title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkdata() {
            var tbRealName = $("#tbRealName").val();

            var patrn = /^[-+]?\d+(\.\d+)?$/;

            if (!patrn.exec(tbRealName)) {
                //$.jBox.info("充值的金额格式错误", "温馨提示");
                alert("充值的积分格式错误");
                return false;
            }

            showload_super();

            return true;

        }


        // onkeypress="return only_num(event)"
        function only_num(e) {
            ee = e ? e : window.event ? event : null;
            var keyNum = ee.keyCode == 0 ? ee.which : ee.keyCode;
            if ((keyNum >= 48 && keyNum <= 57) || keyNum == 8) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp_con">
            <uc2:LeftBanner runat="server" ID="Left" />
            <div class="rightmenu_cont">
                <div class="rightmenu_cen">
                    <h1 class="topbg">账户充值</h1>
                    <div class="usermima">
                        <div class="info">
                            <asp:Label runat="server" ID="lbUserName"></asp:Label>
                            您好，您的帐户中目前共有<span style="color: #f60;"><asp:Label
                                runat="server" ID="lbMoney"></asp:Label>
                                元</span>
                        </div>
                        <div class="rech_infor">
                            <h3>说明</h3>
                            <ul>
                                <li>1元人民币 = 1元 </li>
                                <li>账户余额可用于在线订购外卖</li>
                                <li>第一次支付请先设置支付密码</li>
                                <li>特殊原因产生的退款，会退回本帐户，我们会为您提供详细的帐户明细</li>
                                <li>充值请使用IE浏览器</li>
                            </ul>
                        </div>
                        <div class="rech_infor_right">
                            <ul>
                                <li>
                                    <span><span class="red">*</span> 请输入您要充值的金额：<asp:TextBox runat="server" ID="tbRealName" CssClass="input_on"
                                        onkeypress="return only_num(event)" onkeyup="this.value=this.value.replace(/[^.\d]/g,'')"
                                        Width="70px"></asp:TextBox> 元</span> </li>
                                <li>
                                    <asp:Button Text="确定" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn"
                                        OnClientClick="return checkdata()" />
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <foot:foot runat="server" ID="foot" />
    </form>
</body>
</html>

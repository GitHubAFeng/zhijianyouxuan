<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyPointRecharge.aspx.cs"
    Inherits="UserHome_MyPointRecharge" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>积分充值 -
        <%= WebUtility.GetPublishName()%></title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" type="text/css" rel="Stylesheet" />
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
                //$.jBox.info("充值的积分格式错误", "温馨提示");
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

        //加载页面
        function loadpage() {
            alert('使用积分充值成功!');
            //window.location.href = 'user/MyPointRecharge.aspx';
            window.location.reload();
        }
    </script>

    <style type="text/css">
        .rightmenu_cont
        {
            background-color: #fff;
        }
        .input_on
        {
            background: none repeat scroll 0 0 #FFFFFF;
            border-color: #707070 #CECECE #CECECE #707070;
            border-style: solid;
            border-width: 1px;
            color: #666666;
            font-size: 14px;
            line-height: 18px;
            margin-right: 5px;
            padding: 2px 4px;
            vertical-align: middle;
            width: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <top:banner ID="Banner1" runat="server" />
    <uc1:banner ID="Banner2" runat="server" />
    <div class="warp_con">
        <uc2:LeftBanner runat="server" ID="Left" />
        <div class="rightmenu_cont">
            <h1 class="topbg">
                账户充值</h1>
            <style type="text/css">
                .info
                {
                    border: 1px solid #E1E1E1;
                    color: #0066CC;
                    font: 12px/36px Tahoma;
                    height: 36px;
                    margin-top: 22px;
                    padding-left: 20px;
                    margin-left: 20px;
                }
                .info span
                {
                    background-color: White;
                    font-weight: bold;
                    margin: 0 5px;
                }
                .required
                {
                    color: #FF0000;
                    font-family: 宋体;
                    font-size: 14px;
                    line-height: 18px;
                    margin-right: 5px;
                }
                .cell
                {
                    display: inline-block;
                    letter-spacing: normal;
                    vertical-align: top;
                    word-spacing: normal;
                }
                .container ul li
                {
                    font-family: 宋体;
                    line-height: 20px;
                    margin-bottom: 5px;
                    padding-left: 12px;
                }
            </style>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="rightmenu_bg">
                        <div>
                            <div class="info" style="display: block; margin: 0; padding: 0;">
                                <span>
                                    <asp:Label runat="server" ID="lbUserName"></asp:Label></span>您好，您的帐户中目前共有<span style="color: #f60;"><asp:Label
                                        runat="server" ID="lbMoney"></asp:Label>
                                        积分</span>
                            </div>
                        </div>
                        <div class="usermima">
                            <div class="container" style="float: left; background-color: #F5F5F5; border: 1px solid #E1E1E1;
                                padding: 11px 7px; width: 198px;">
                                <h3>
                                    说明</h3>
                                <ul>
                                    <li style="background-image: none; margin-bottom: 0;">积分比例：1积分 =
                                        <%= SectionProxyData.GetSetValue(31)%>元 </li>
                                    <li style="background-image: none; margin-bottom: 0;">账户余额可用于在线订购外卖</li>
                                    <%-- <li style="background-image: none; margin-bottom: 0;">第一次支付请先设置支付密码</li>--%>
                                    <%-- <li style="background-image: none; margin-bottom: 0;">特殊原因产生的退款，会退回本帐户，我们会为您提供详细的帐户明细</li>--%>
                                    <li style="background-image: none; margin-bottom: 0;">充值请使用IE浏览器</li>
                                </ul>
                            </div>
                            <div style="width: 460px; float: right;">
                                <ul>
                                    <li style="width: 300px;">
                                        <div class="cell required">
                                            *</div>
                                        <span>请输入您要充值的积分：<asp:TextBox runat="server" ID="tbRealName" CssClass="input_on"
                                            onkeypress="return only_num(event)" onkeyup="this.value=this.value.replace(/[^.\d]/g,'')"
                                            Width="60px"></asp:TextBox></span> </li>
                                    <li class="padding90px">
                                        <asp:Button Text="确定" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn"
                                            OnClientClick="return checkdata()" />
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <foot:foot runat="server" ID="foot" />
    </form>
</body>
</html>

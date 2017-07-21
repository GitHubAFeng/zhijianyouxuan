<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetGift.aspx.cs" Inherits="Gift_getGift1" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="giftleft.ascx" TagName="giftleft" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>积分兑换礼品 -
        <%= SectionProxyData.GetSetValue(3)%></title>

    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/gift.css" />

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function g(o) { return document.getElementById(o); }

        function jjjloing() {
            var tbEUsername = document.getElementById("tbEUsername").value;
            var tbEPassword = document.getElementById("tbEPassword").value;
            if (tbEUsername.trim() == "") {
                tipsWindown('提示信息', 'text:请输入帐号!', '250', '150', 'true', '3000', 'true', 'text');
                return false;
            }
            if (tbEPassword.trim() == "") {
                tipsWindown('提示信息', 'text:请输入密码!', '250', '150', 'true', '3000', 'true', 'text');
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            initnav(4);
        })
    </script>
</head>
<body>
    <!--最顶部-->
    <form id="Form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="banner" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="wrap">
            <div class="hplace_bg">
                <span class="color_1 hplace_house" id="dangqian" runat="server"><a href="../index.aspx">首页</a> >> <a href="Gift.aspx">积分商城</a> >> 填写详细资料</span>
            </div>
            <uc2:giftleft runat="server" ID="myleft" />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="hgift_right_intro">
                        <div class="hgift_right_title">
                                送货资料填写
                        </div>
                        <div class="order_intro">
                            <ul>
                                <li><span class="order_intro_left">礼品：</span>
                                    <asp:Label runat="server" ID="lbGiftName"></asp:Label>
                                </li>
                                <li id="lipoint" runat="server"><span class="order_intro_left">需要积分：</span>
                                    <asp:Label runat="server" ID="lbPoint"></asp:Label>
                                </li>
                                <li id="liprice" runat="server"><span class="order_intro_left">购买价格：</span> ￥<asp:Label
                                    runat="server" ID="lbprice"></asp:Label>
                                </li>
                                <li><span class="order_intro_left">库存：</span>
                                    <asp:Label runat="server" ID="lbstocks"></asp:Label>件 </li>
                                <li><span class="order_intro_left">收货人：</span><span class="red">*</span>
                                    <asp:TextBox runat="server" ID="tbPerson"></asp:TextBox>
                                </li>
                                <li><span class="order_intro_left">收货地址：</span><span class="red">*</span>
                                    <asp:TextBox runat="server" ID="tbAddress" Style="width: 300px"></asp:TextBox>
                                </li>
                                <li><span class="order_intro_left">电话：</span><span class="red">*</span>
                                    <asp:TextBox runat="server" ID="tbPhone"></asp:TextBox>
                                </li>
                                <li><span class="order_intro_left">送货时间：</span><span class="red">*</span>
                                    <asp:DropDownList runat="server" ID="ddlDate">
                                        <asp:ListItem Text="不限" Value="不限"></asp:ListItem>
                                        <asp:ListItem Text="周一－周五" Value="周一－周五"></asp:ListItem>
                                        <asp:ListItem Text="周末" Value="周末"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="height:68px; line-height:68px;"><span class="order_intro_left">备注：</span>
                                    <asp:TextBox runat="server" ID="tbremark" Style="width: 300px; padding: 2px;" MaxLength="100"
                                        TextMode="MultiLine"></asp:TextBox>
                                </li>
                            </ul>
                        </div>
                        <div class="order_refer">
                            <asp:Button class="shop_submit_btn" runat="server" ID="btGet" Text="提交" OnClick="btGet_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

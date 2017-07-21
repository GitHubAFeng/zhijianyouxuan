<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowGift.aspx.cs" Inherits="Gift_ShowGift1" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="giftleft.ascx" TagName="giftleft" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>

    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/gift.css" />

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            initnav(4);
        })

    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="warp">
            <div class="hplace_bg">
                <span class="color_1 hplace_house" id="dangqian" runat="server"><a href="../index.aspx">首页</a> >> <a href="Gift.aspx">积分商城</a> >> <span>积分兑换详情</span></span>
            </div>
            <uc2:giftleft runat="server" ID="myleft" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="hgift_right_detail">
                        <div class="duihuan_more">
                            <dl>
                                <dt>
                                    <asp:Image runat="server" ID="imgLogo" ImageUrl="~/upload/201012/201104121241005801.jpg" />
                                </dt>
                                <dd class="title">
                                    <asp:Label runat="server" ID="lbGiftName">奖品介绍奖品介绍奖品介绍</asp:Label></dd>
                                <dd>市场价格：<span class="chengse"><strong><asp:Label runat="server" ID="Label1"></asp:Label></strong></span></dd>
                                <dd>所需积分：<span class="chengse"><strong><asp:Label runat="server" ID="lbPoint"></asp:Label></strong></span></dd>
                                <dd>库存剩余：<span class="gray"><asp:Label runat="server" ID="lbHave"></asp:Label></span>
                                </dd>
                                <dd class="chengse" style="font-size: 12px; margin-top: 30px;">
                                    <asp:ImageButton runat="server" ID="ImageButton21" ImageUrl="~/images/youhui_8.gif"
                                        OnClick="go_buy" />
                                </dd>
                            </dl>
                        </div>
                        <div class="jiangpin">
                            <div class="div1">
                                介绍
                            </div>
                        </div>
                        <div class="jiangpin_info">
                            <asp:Literal runat="server" ID="litIntroduce"></asp:Literal>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <foot:foot ID="Foot2" runat="server" />
    </form>
</body>
</html>

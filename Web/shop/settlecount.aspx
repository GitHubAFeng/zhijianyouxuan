<%@ Page Language="C#" AutoEventWireup="true" CodeFile="settlecount.aspx.cs" Inherits="shop_settlecount" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>结算帐号-<%= WebUtility.GetWebName()%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

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
                                    <div class="shop_menu">
                                        <ul>
                                            <li id="today"><a href="settlelist.aspx">订单结算报表</a></li>
                                            <li id="all" class="cur"><a href="settlecount.aspx">结算帐号</a></li>
                                            <li><a href="cachoutlist.aspx">收支记录</a></li>
                                            <li><a href="cachoutdetail.aspx">提现申请</a></li>
                                        </ul>
                                    </div>

                                    <div class="usermima">
                                        <ul>
                                            <li><span class="left_span">开户行：</span>
                                                <epc:TextBox runat="server" ID="tbbankname" Size="25" onblur="queryfood()" class="input_on"
                                                    MaxLength="30"></epc:TextBox>
                                            </li>
                                            <li><span class="left_span">开户名：</span>
                                                <epc:TextBox runat="server" ID="tbbankusername" Size="25" class="input_on" MaxLength="30"></epc:TextBox>
                                            </li>

                                            <li><span class="left_span">卡号：</span>
                                                <epc:TextBox runat="server" ID="tbrevevar1" Size="30" onblur="queryfood()" class="input_on"
                                                    MaxLength="30"></epc:TextBox>
                                            </li>
                                            <li><span class="left_span">支付宝帐号：</span>
                                                <epc:TextBox runat="server" ID="tbaliaccount" Size="25" class="input_on" MaxLength="30"></epc:TextBox>
                                            </li>
                                            <li><span class="left_span">支付姓名：</span>
                                                <epc:TextBox runat="server" ID="tbaliname" Size="25" class="input_on" MaxLength="30"></epc:TextBox>
                                            </li>

                                        </ul>
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


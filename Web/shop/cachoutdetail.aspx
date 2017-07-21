<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cachoutdetail.aspx.cs" Inherits="shop_cachoutdetail" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>提现申请-<%= WebUtility.GetWebName()%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>


    <script type="text/javascript">

        function checkmoney() {
            var flag = j_submitdata("usermima");
            if (false == flag) {
                return flag;
            }
            showload_super();
            return true;
        }
    </script>

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
                                            <li id="all"><a href="settlecount.aspx">结算帐号</a></li>
                                            <li><a href="cachoutlist.aspx">收支记录</a></li>
                                            <li class="cur"><a href="cachoutdetail.aspx">提现申请</a></li>
                                        </ul>
                                    </div>

                                    <div class="usermima">
                                        <div class=" mynotice" style="display: block; margin-bottom: 10px; padding-left: 5px;">提示：提交申请后，请等待管理员处理，可以到<a href="cachoutlist.aspx" style="color: red;">收支记录</a>查看状态，或者取消申请</div>
                                        <ul>

                                            <li><span class="left_span">当前余额：</span>
                                                <asp:Label ID="LitUserBalance" runat="server"></asp:Label>元
                                            </li>
                                            <li><span class="left_span">冻结余额：</span>
                                                <asp:Label ID="lbnouse" runat="server"></asp:Label>元
                                        <div class="mynotice">提现申请后，提现部分金额将会冻结，如提现失败，这部分金额会转入可提余额</div>
                                            </li>
                                            <li><span class="left_span">可提余额：</span>
                                                <asp:Label ID="lbcanuse" runat="server"></asp:Label>元
                                            </li>
                                            <li><span class="left_span">提现金额：</span>
                                                <epc:TextBox runat="server" ID="tbwantmoney" reg="^\d+(\.\d+)?$" tip="提现金额错误,请输入数字" Size="5" class="input_on" MaxLength="30"></epc:TextBox>元
                                            </li>
                                            <li style="text-align: center;">
                                                <asp:Button ID="btSave" runat="server" class="subBtn" OnClick="btSave_Click" OnClientClick="return checkmoney();" Text="确定" />
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

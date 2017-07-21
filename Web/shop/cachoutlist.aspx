<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cachoutlist.aspx.cs" Inherits="shop_OrderList_myaccountlist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收支记录-<%= SectionProxyData.GetSetValue(3)%></title>

    <link rel="stylesheet" type="text/css" href="/user/css/user_center.css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/userinfo.css" rel="stylesheet" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="DatePicker/WdatePicker.js" type="text/javascript"></script>

</head>

<body>
    <form id="form1" runat="server">
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <div class="shop_main">
                        <div class="main-content">
                            <div class="shop_menu">
                                <ul>
                                    <li id="today"><a href="settlelist.aspx">订单结算报表</a></li>
                                    <li id="all"><a href="settlecount.aspx">结算帐号</a></li>
                                    <li class="cur"><a href="cachoutlist.aspx">收支记录</a></li>
                                    <li><a href="cachoutdetail.aspx">提现申请</a></li>

                                </ul>
                            </div>

                            <div class="info" style="margin-top: 20px;">
                                <asp:Label runat="server" ID="lbUserName"></asp:Label>
                                您好，您的帐户中目前共有<span style="color: #f60;"><asp:Label
                                    runat="server" ID="lbMoney"></asp:Label>
                                    元</span>    <a href="cachoutdetail.aspx" style="color: #f39800">提现</a>
                            </div>

                            <div class="listorder" style="width: 795px;">
                                <ul>

                                    <li>时间：
                                <input type="text" name="textfield2" id="starttime" size="10" class="j_text" runat="server" onfocus="WdatePicker({readOnly:true})" />
                                        至
                                <input type="text" name="textfield3" id="enttime" runat="server" class="j_text" size="10" onfocus="WdatePicker({readOnly:true})" />


                                        <asp:DropDownList ID="ddlpaymodel" runat="server" class="j_select">
                                            <asp:ListItem Value="-1">收支类型</asp:ListItem>
                                            <asp:ListItem Value="1">收入</asp:ListItem>
                                            <asp:ListItem Value="2">支出</asp:ListItem>

                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlPayType" runat="server" class="j_select">
                                            <asp:ListItem Value="-1">交易类型</asp:ListItem>
                                            <asp:ListItem Value="0">后台操作</asp:ListItem>

                                            <asp:ListItem Value="1">提现</asp:ListItem>
                                            <asp:ListItem Value="7">订单结算</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="subBtn" OnClick="btSearch_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div class="clear"></div>


                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                <ContentTemplate>
                                    <div class="usermima">
                                        <div class=" mynotice" style="display: block; margin-bottom: 10px; padding-left: 5px;">提示：提现申请只有要未处理可在取消。</div>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                            style="border: 1px solid #ccc;">
                                            <asp:Repeater ID="rptOrderList" runat="server" OnItemCommand="rptOrderList_ItemCommand">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th>时间
                                                        </th>
                                                        <th>金额
                                                        </th>
                                                        <th>类型
                                                        </th>
                                                        <th>状态
                                                        </th>
                                                        <th>备注
                                                        </th>
                                                        <th style="border-right: 0;">操作
                                                        </th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("AddDate")%>
                                                  
                                                        </td>
                                                        <td>
                                                            <%# Eval("AddMoney")%>
                                                        </td>

                                                        <td>
                                                            <%# Hangjing.WebCommon.WebHelper.shopRecharge(Eval("PayType").ToString())%>
                                                        </td>

                                                        <td>
                                                            <%# Hangjing.WebCommon.WebHelper.shopRechargeState(Eval("PayState").ToString())%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("inve2")%>
                                                        </td>

                                                        <td>
                                                            <div style="<%#Convert.ToInt32(Eval("PayState")) == 0 ? "": "display:none"%>">
                                                                <asp:Button runat="server" ID="tbcancel" Text="取消" OnClientClick="return confirm('确定取消本次提现?');" CssClass="subBtn" CommandName="cancel" CommandArgument='<%# Eval("dataid")%>' />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <div id="noRecord" runat="server" class="no_infor">
                                        暂无记录！
                                    </div>
                                    <div class="pages">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                            ShowPageIndex="True" PageSize="10" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                            TextAfterPageIndexBox=" 页 " Wrap="False" FirstPageText="首页" LastPageText="尾页"
                                            NextPageText="下一页" PrevPageText="上一页">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </form>
</body>
</html>


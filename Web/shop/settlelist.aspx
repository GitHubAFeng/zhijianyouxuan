<%@ Page Language="C#" AutoEventWireup="true" CodeFile="settlelist.aspx.cs" Inherits="shop_settlelist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务结算-<%= SectionProxyData.GetSetValue(3)%></title>

    <link rel="stylesheet" type="text/css" href="/user/css/user_center.css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/userinfo.css" rel="stylesheet" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />


    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="DatePicker/WdatePicker.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">

        function checkcashout() {
            var lbnopaymoney = parseFloat($("#lbnopaymoney").html());
            if (lbnopaymoney <= 0) {
                alert("此时间没有未结算金额，不能提现");
                return false;
            }
            var flag = j_submitdata("listorder");
            if (false == flag) {
                return false;
            }

            return true;
        }

        function getexcel() {
            var url = "SearchExcelTogoSet.aspx";
            window.open(url);
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

                                <div class="shop_main">
                <div class="main-content">
                    <div class="shop_menu">
                        <ul>
                            <li class="cur" id="today"><a href="settlelist.aspx">订单结算报表</a></li>
                            <li id="all"><a href="settlecount.aspx">结算帐号</a></li>
                            <li><a href="cachoutlist.aspx">收支记录</a></li>
                            <li><a href="cachoutdetail.aspx">提现申请</a></li>
                        </ul>
                    </div>
                    <div class="listorder" style="width:94%;">
                        <div class=" mynotice" style="display: block; margin-bottom: 10px; padding-left: 5px;">提示：只有完成的订单才能进入结算报表</div>
                        <ul>

                            <li>订单时间：
                                <asp:TextBox runat="server" ID="tbStartTime" class="j_text" reg="\S" tip="请输入开始时间" Style="width: 90px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                至
                                <asp:TextBox runat="server" ID="tbEndTime" class="j_text" reg="\S" tip="请输入结束时间" Style="width: 90px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>

                                <asp:DropDownList runat="server" ID="ddldeliversiteid" class="j_select">
                                    <asp:ListItem Text="结算状态" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="未结" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="已结" Value="1"></asp:ListItem>

                                </asp:DropDownList>

                            </li>
                            <li style="clear: both; display: block; margin-top: 15px; margin-bottom: 10px;">
                                <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="subBtn" OnClick="settime_Click" />

                                <asp:Button ID="tbtoday" runat="server" Text="今天" CssClass="subBtn" OnClick="settime_Click" />

                                <asp:Button ID="btweek" runat="server" Text="本周" CssClass="subBtn" OnClick="settime_Click" />

                                <asp:Button ID="btmounth" runat="server" Text="本月" CssClass="subBtn" OnClick="settime_Click" />

                                <asp:Button ID="btpremounth" runat="server" Text="上月" CssClass="subBtn" OnClick="settime_Click" />

                                <asp:Button runat="server" ID="btnExl" CssClass="subBtn" Text="导出" OnClientClick="getexcel();return false" />

                            </li>
                        </ul>
                        <div class="clear"></div>
                        <fieldset class="AdminSearchform">
                            <legend>统计</legend>
                            <div style="clear: both; padding: 10px;">
                                <span>订单数量:<span style="margin-left: 20px; font-weight: bold; color: Red"
                                    id="lborder" runat="server">0</span><span style="color: red">单</span>
                                </span>
                                <span style="margin-left: 20px;">金额:
                                            <span style="font-weight: bold; color: Red" id="lbcount" runat="server">0</span>
                                    <span style="color: red">元</span>
                                </span>

                                <span style="margin-left: 20px;">结算金额:
                                            <span style="font-weight: bold; color: Red" id="lbsettlemoney" runat="server">0</span>
                                    <span style="color: red">元</span>
                                </span>

                            
                                <span style="margin-left: 20px;">配送费:
                                            <span style="font-weight: bold; color: Red" id="lbSendFee" runat="server">0</span>
                                    <span style="color: red">元</span>
                                </span>
                                <span style="margin-left: 20px;">优惠卷:
                                            <span style="font-weight: bold; color: Red" id="lbCardPay" runat="server">0</span>
                                    <span style="color: red">元</span>

                                    
                                    <asp:Button ID="tbcashout" runat="server" Text="提现" CssClass="subBtn" OnClientClick="return checkcashout()" OnClick="cashout_Click" />
                                </span>
                            </div>
                        </fieldset>


                    </div>
                    <div class="clear"></div>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                        <ContentTemplate>
                            <div class="usermima">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                    style="border: 1px solid #ccc;">
                                    <asp:Repeater ID="rptOrderList" runat="server" OnItemCommand="rptOrderList_ItemCommand">
                                        <HeaderTemplate>
                                            <tr>
                                                <th>订单编号
                                                </th>
                                                <th>下单时间
                                                </th>
                                                <th>金额
                                                </th>
                                                <th>配送费
                                                </th>
                                                <th>支付方式
                                                </th>
                                                <th style="border-right: 0;">结算金额
                                                </th>
                                               
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <a href="OrderDetail.aspx?id=<%#Eval("unid")%>" target="_blank"><%#Eval("OrderID")%></a>
                                                </td>
                                                <td>
                                                    <%# Eval("OrderDateTime")%>
                                                </td>
                                                <td>
                                                    <%# Eval("OrderSums")%>
                                                </td>
                                                <td>
                                                    <%# Eval("sendFee")%>
                                                </td>
                                                <td>
                                                    <%# WebUtility.TurnPayModel(Eval("PayMode").ToString())%>[<%# Eval("PayState").ToString()=="1"?"已付":"未付" %>]
                                                </td>

                                                <td>
                                                    <%# Eval("shopdiscountmoney")%>
                                                    

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
                                暂无订单！
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
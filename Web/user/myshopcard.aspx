<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myshopcard.aspx.cs" Inherits="UserHome_myshopcard" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员中心-优惠券-<%= SectionProxyData.GetSetValue(3)%></title>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkdata() {
            var tbnum = $("#tbpwd1").val() + "-" + $("#tbpwd2").val() + "-" + $("#tbpwd3").val();
            if (tbnum == "--") {
                alert("请输入完整的券号");
                return false;
            }
            tbnum = $("#tbmycode").val() + "";
            if (tbnum == "") {
                alert("请输入验证码");
                return false;
            }
            showload_super();
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp_con">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <uc2:LeftBanner runat="server" ID="Left" />
            <div class="rightmenu_cont">
                <div class="rightmenu_cen">
                    <h1 class="topbg">优惠券</h1>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="usermima" style="clear: both; padding-left: 0">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                    style="border: 1px solid #ccc;">
                                    <asp:Repeater ID="rptPointCount" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <th style="width: 30%">券号
                                                </th>

                                                <th>说明
                                                </th>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#  Eval("ckey")%>[<%#Convert.ToInt32(Eval("isused")) == 1 ? "已用" : "未用"%>]
                                                </td>
                                                <td style="text-align: left; padding-left: 10px;">
                                                    <%#  Eval("ReveVar1")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <div id="noRecord" runat="server" class="no_infor">
                                    您还没有获取任何优惠券！
                                </div>
                                <div class="pages">
                                    <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                        CustomInfoHTML=""
                                        HorizontalAlign="Center" ShowCustomInfoSection="Never" CustomInfoTextAlign="Center"
                                        CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                        TextBeforePageIndexBox="jump to " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                        PageSize="10" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" Page "
                                        Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="shopcard">
                        绑定优惠券
                    </div>
                    <div class="usermima" style="clear: both; padding-left: 0">
                        <ul>
                            <li><span class="left_span" style="width: 150px;">券号：</span>
                                <asp:TextBox ID="tbpwd1" runat="server" CssClass="j_text" Style="width: 60px" MaxLength="4"
                                    onkeyup="nextinput('tbpwd1','tbpwd2')"></asp:TextBox>-<asp:TextBox ID="tbpwd2" runat="server" CssClass="j_text"
                                        Style="width: 60px" MaxLength="4" onkeyup="nextinput('tbpwd2','tbpwd3')"></asp:TextBox>-<asp:TextBox ID="tbpwd3" runat="server"
                                            CssClass="j_text" Style="width: 60px" MaxLength="4"></asp:TextBox>

                                <span>券号为数字0-9或者大写字母A-F</span>
                            </li>
                            <li><span class="left_span" style="width: 150px;">验证码：</span>
                                <asp:TextBox ID="tbmycode" runat="server" CssClass="j_text" Style="width: 60px; float: left;"></asp:TextBox>
                                <img onclick="this.src = '/VCode.aspx?t='+new Date().getTime();" src="/VCode.aspx?t='+new Date().getTime();"
                                    title="Click to change" style="cursor: pointer;" />
                            </li>
                            <li style="padding-left: 140px;">
                                <asp:Button ID="btUpdate" runat="server" Text="确认" class="subBtn" OnClick="btUpdate_Click"
                                    OnClientClick="return checkdata()" />
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

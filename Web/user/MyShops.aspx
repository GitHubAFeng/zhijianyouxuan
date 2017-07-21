<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyShops.aspx.cs" Inherits="UserHome_MyShops" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-我的商家收藏-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="javascript/UserHomeCommon.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(init);  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="Userid" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField runat="server" ID="hdDels" />
    <top:banner ID="Banner1" runat="server" />
    <uc1:banner ID="Banner2" runat="server" />
    <div class="warp">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc2:LeftBanner runat="server" ID="Left" />
                <div class="rightmenu_cont">
                    <div class="rightmenu_cen">
                        <h1 class="topbg">
                            商家收藏</h1>
                        <div id="handlelist">
                            <ul class="myhandlelist" style="padding-left:15px">
                                <li class="userGroupCheckAll"><a href="javascript:Table.CheckAll()">全部选定</a> </li>
                                <li class="spirtLines">|</li>
                                <li class="userGroupReverseCheck"><a href="javascript:Table.ReCheck()">反向选择</a></li>
                                <li class="spirtLines" runat="server" id="menu3">|</li>
                                <li class="userGroupDelCheck" id="menu4" runat="server"><a href="#">
                                    <asp:LinkButton runat="server" ID="lbmdel" OnClick="Mlb_Click" OnClientClick="return jDel();">删除选定</asp:LinkButton>
                                </a></li>
                            </ul>
                        </div>
                        <div style="clear: both">
                        </div>
                        <div class="usermima">
                            <table id="TBuserfood" width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                style="border: 1px solid #ccc;">
                                <asp:Repeater ID="rptCollect" runat="server" OnItemCommand="rptproduct_ItemCommand">
                                    <HeaderTemplate>
                                        <tr>
                                            <th>
                                                选择
                                            </th>
                                            <th>
                                                商家名称
                                            </th>
                                            <th>
                                                收藏时间
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td width="50">
                                                <input id="Checkbox1" type="checkbox" value="<%# Eval("dataid") %>" />
                                            </td>
                                            <td align="left" style="padding-left: 10px;">
                                                <a href='../shop.aspx?id=<%# Eval("togoid") %>'>
                                                    <%#Eval("togoname") %>
                                                </a>
                                            </td>
                                            <td width="120" align="left">
                                                <%# Convert.ToDateTime(Eval("ctime")).ToShortDateString()%>
                                            </td>
                                            <td width="70">
                                                <asp:LinkButton ID="LinkButton1" CommandName="del" CommandArgument='<%# Eval("dataid")%>'
                                                    runat="server" OnClientClick="return confirm('确认删除吗？')">删除 </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div id="noRecord" runat="server" class="no_infor">
                                    暂无数据！
                            </div>
                            <div class="pages">
                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                    HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                    TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                    ShowPageIndex="True" PageSize="10" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                    TextAfterPageIndexBox=" 页 " Wrap="False">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reminder.aspx.cs" Inherits="shop_Reminder" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>催外-<%= SectionProxyData.GetSetValue(3)%></title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="Style/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    
    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="DatePicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="warp">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
            <div class="center_bg">
                <uc2:LeftBanner runat="server" ID="Left" />
                <div class="rightmenu_cont">
                    <h1 class="topbg">
                        订单列表</h1>
                    <div class="listorder">
                     <%--   <ul>
                            <li><span>订单编号</span>
                                <input type="hidden" id="tbuserids" runat="server" />
                                <input id="tbKeyword" runat="server" name="workAddr" autocomplete="off" type="text" />
                            </li>
                            <li>订单时间：
                                <input type="text" name="textfield2" id="starttime" size="10" runat="server" onfocus="WdatePicker({readOnly:true})" />
                                至
                                <input type="text" name="textfield3" id="enttime" runat="server" size="10" onfocus="WdatePicker({readOnly:true})" />
                            </li>
                            <li>
                                <asp:Button ID="btSearch" runat="server" Text="查询" CssClass="subBtn" OnClick="btSearch_Click" />
                            </li>
                        </ul>--%>
                    </div>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                        style="border: 1px solid #ccc;">
                        <asp:Repeater ID="rptOrderList" runat="server" OnItemCommand="rptOrderList_ItemCommand">
                            <HeaderTemplate>
                                <tr>
                                    <th>
                                        订单编号
                                    </th>
                             
                                    <th>
                                        催单人
                                    </th>
                                    <th>
                                        催单时间
                                    </th>
                                    <th>
                                       催单次数
                                    </th>
                                   
                                    <th style="border-right: 0;">
                                        操作
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="lanse_1">
                                        <%#Eval("oid")%>
                                    </td>
                                
                                    <td class="lanse_1">
                                        <%# Eval("Name")%>
                                    </td>
                                    <td class="lanse_1">
                                      <%# Convert.ToDateTime(Eval("addtime")).ToShortTimeString()%>
                                    </td>
                                    <td class="lanse_1">
                                         <%# Eval("Ccount")%>
                                    </td>
                                 
                                    <td>
                                        <a href="OrderDetail.aspx?id=<%#Eval("id")%>">查看</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <div id="noRecord" runat="server" style="display: none; margin-left: 20px;">
                        &nbsp;&nbsp;&nbsp;&nbsp;<h5>
                            暂无数据!</h5>
                    </div>
                    <div class="pages">
                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                            ShowPageIndex="True" PageSize="10" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                            TextAfterPageIndexBox=" 页 " Wrap="False" FirstPageText="首页"  LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>

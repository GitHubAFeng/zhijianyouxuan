<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyMessage.aspx.cs" Inherits="UserHome_MyMessage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-我的留言-<%= SectionProxyData.GetSetValue(3)%></title>
    
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>
     <link href="../css/common.css" rel="stylesheet" type="text/css" />
    
    <script src="JavaScript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="javaScript/ShowDivDialog.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <top:banner ID="Banner1" runat="server" />
    <div class="warp">
        <uc1:banner ID="Banner2" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <div class="warp_con">
             <uc2:LeftBanner runat="server" ID="Left" />
                <div class="rightmenu_cont">
                    <h1 class="topbg">
                        我的留言</h1>
                    <div class="myrfeview">
                        <ul>
                            <li>编号 </li>
                            <li>时间</li>
                            <li>状态</li>
                            <li>操作</li>
                        </ul>
                        <asp:Repeater ID="rptComment" runat="server" OnDataBinding="test">
                            <ItemTemplate>
                                <ul>
                                    <li>
                                        <%#(Container.ItemIndex+1).ToString() %>
                                    </li>
                                    <li>发表于
                                        <%#Convert.ToDateTime(Eval("time")).ToShortDateString()%></li>
                                    <li>
                                        <%# StateToStr( Eval("state") )%>
                                    </li>
                                    <li><a href="MessageDetail.aspx?id=<%#Eval("dataid") %>"class="zxx_list_title" id="click_test2">
                                        查看</a></li>
                                </ul>
                               
                       
                            </ItemTemplate>
                        </asp:Repeater>
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
                                ShowPageIndex="True" PageSize="7" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                TextAfterPageIndexBox=" 页 " Wrap="False">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <foot:foot ID="Foot1" runat="server" />
     </div>
    </form>
</body>
</html>

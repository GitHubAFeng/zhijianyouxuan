<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Newslist.aspx.cs" Inherits="Newslist" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="uc2" TagName="foot" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公告</title>
    <link href="css/common.css" rel="stylesheet" />
    <link href="css/news.css" rel="stylesheet" type="text/css" />
     <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

</head>
<body>
    <form id="Form1" runat="server">
    <uc3:Banner ID="Banner2" runat="server" />
    <uc1:Banner ID="Banner1" runat="server" />
    <div class="wrap">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="hplace_bg">
                    <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 网站公告</span>
                </div>
                <div class="annouce_con">
                    <div class="annouce_left">
                        <div class="hnews_left_bot">
                            <ul>
                                <asp:Repeater runat="server" ID="rptnews">
                                    <ItemTemplate>
                                        <li><span class="hnews_date gray_03">
                                            <%# Convert.ToDateTime( Eval("posttime")).ToShortDateString()%></span><a href="ShowNews.aspx?id=<%# Eval("dataid") %><%# Hangjing.Common.HjNetHelper.GetQueryInt("tid" , 0) > 0 ? "&tid=" + Hangjing.Common.HjNetHelper.GetQueryInt("tid" ,0) : ""%>"
                                                title="<%# Eval("title") %>">
                                                <%# WebUtility.Left(Eval("title") ,30) %></a> </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div class="pages">
                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                CustomInfoSectionWidth="27%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                ShowPageIndex="True" PageSize="30" SubmitButtonClass="flatbutton" SubmitButtonText="GO"
                                TextAfterPageIndexBox=" 页 " Wrap="False">
                            </webdiyer:AspNetPager>
                        </div>
                        <div class="list_ck_shop_no" runat="server" id="divnojoin" style="display: none">
                            <p>
                                抱歉！没有搜索到相关公告，请您重新搜索！</p>
                            <p>
                                <input name="" type="button" class="ck_shop_no_btn" value="返回首页" onclick="gourl('index.aspx');" /></p>
                        </div>
                    </div>
                    <div class="rightsider" style="margin-bottom: 20px;">
                        <!-- 网站公告-->
                        <div class="htitle">
                            <div class="htitle_bg">
                                <h4>
                                    最新公告</h4>
                            </div>
                        </div>
                        <div class="div_right_info">
                            <ul class="right_ul">
                                <asp:Repeater runat="server" ID="rptNewsList">
                                    <ItemTemplate>
                                        <li><a href="ShowNews.aspx?id=<%# Eval("dataid") %>" title="<%# Eval("title") %>">
                                            <%# WebUtility.Left( Eval("title"),15) %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <uc2:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

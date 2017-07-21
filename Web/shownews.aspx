<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNews.aspx.cs" Inherits="ShowNews" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="uc2" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="css/news.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="javascript/jCommon.js" type="text/javascript"></script>

</head>
<body>
    <form id="Form1" runat="server">
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:Banner ID="Banner1" runat="server" />
        <div class="wrap margin_b10">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="hplace_bg">
                        <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 网站公告</span>
                    </div>
                    <div class="annouce_con">
                        <div class="annouce_left">
                            <div class="news_detail">
                                <div class="news_detail_title">
                                    <h4 runat="server" id="newstitle"></h4>
                                    <p runat="server" id="divtime">
                                    </p>
                                </div>
                                <div class="news_detail_con" runat="server" id="newsContent">
                                </div>
                            </div>
                        </div>
                        <div class="rightsider" style="margin-bottom: 20px;">
                            <!-- 网站公告-->
                            <div class="htitle">
                                <div class="htitle_bg">
                                    <h4>最新公告</h4>
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

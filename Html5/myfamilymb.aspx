<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myfamilymb.aspx.cs" Inherits="Html5.myfamilymb" %>

<%@ Register Src="~/header.ascx" TagName="head" TagPrefix="uc3" %>
<%@ Register Src="~/distributorfooter.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
     <title><%= SectionProxyData.GetSetValue(2) %></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:head runat="server" ID="head" />
        <div class="main_title">我的<span runat="server" id="gradelabel"></span>分销成员（<span runat="server" id="allchild"></span>）人</div>
        <div class="warp" style="display:none;">
            <div class="serachbox">
                <input value="请输入会员ID" />
                <button></button>
            </div>
        </div>

        <div class="numberitem clearfix">

            <asp:Repeater runat="server" ID="rptusers">
                <ItemTemplate>
                    <div class="numberlist">
                      <img src="<%# WebUtility.ShowPicFix(Eval("picture").ToString(),"user-noface.png") %>" />
                        <ul>
                            <li>昵称：<span class="t_txt"><%# Eval("name") %></span></li>
                            <li>注册时间：<%# Convert.ToDateTime(Eval("RegTime")).ToShortDateString()%></li>
                            <li>会员ID：<%# Eval("dataid") %></li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


              <div class="con-btn" id="pages" runat="server">
            </div>
        </div>
    </form>
    <uc2:Foot runat="server" ID="footer" />
</body>
</html>

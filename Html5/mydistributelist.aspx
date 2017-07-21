<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mydistributelist.aspx.cs" Inherits="Html5.mydistributelist" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/jquery.mobile-1.2.0.css" />

    <script src="javascript/jquery.js"></script>

    <script src="javascript/jquery.mobile-1.2.0.js"></script>

    <link rel="stylesheet" href="css/wxdemo_mobile.css">
</head>
<body>
    <div data-role="page" data-theme="d" id="myorderlist">
        <div data-role="header" data-theme="orange">
            <a href="familyhonor.aspx" data-ajax="false" data-icon="back" data-theme="green" runat="server" id="gocart">返回</a>
            <h1>佣金明细</h1>
        </div>
        <div data-role="content" role="main" class="ui-content">
            <div class="myorderlist">
                <ul data-role="listview" data-inset="true" data-theme="d" id="imglist">
                    <asp:Repeater runat="server" ID="rptorder">
                        <ItemTemplate>
                            <li>
                                <h4><%# Eval("Inve2") %></h4>
                                <p>佣金：<label><%# Eval("AddMoney")%></label>元</p>
                                <p>时间：<label><%# Eval("AddDate")%></label></p>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="con-btn" id="pages" runat="server">
            </div>
        </div>
    </div>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="Html5.header" %>
<link href="css/common.css" rel="stylesheet" type="text/css" />
<link href="css/style.css" rel="stylesheet" type="text/css" />
<div class="header_permsg">
    <div class="header clearfix">

        <asp:Repeater runat="server" ID="rptppt">
            <ItemTemplate>

                <img src="<%# WebUtility.ShowPicFix(Eval("picture").ToString(),"user-noface.png") %>" />
                <ul>
                    <li>昵称：<span class="t_txt"><%# Eval("name") %></span></li>
                    <li>注册时间：<%# Convert.ToDateTime(Eval("RegTime")).ToShortDateString()%></li>
                    <li>会员ID：<%# Eval("dataid") %></li>
                </ul>

            </ItemTemplate>
        </asp:Repeater>


    </div>
</div>

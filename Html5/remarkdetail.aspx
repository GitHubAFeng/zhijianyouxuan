<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="remarkdetail.aspx.cs" Inherits="Html5.remarkdetail" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=<%=(new Random()).Next(0000,9999) %>" />

    <script type="text/javascript" src="javascript/jquery.js"></script>
</head>

<body>
    <form method="post" action="remarkdetail.aspx?id=<%= Request["id"]  %>" data-ajax="false">
        <div class="page">
            <div id="page_title">
                <a href="orderdetail.aspx?id=<%= Request["id"]  %>" id="back" class=" top_left" data-ajax="false"></a>
                <h1>添加备注</h1>
                <input type="submit" value="确定" class=" top_right" style="margin: 12px 0px; border: none; color: #fff; background:none; font-family: 微软雅黑; font-size: 14px;" onclick="return checkuserregin()" data-ajax="false" />
            </div>
            <div class="container">
                <div class="view_back">
                       <p> <textarea name="tbremark" id="tbremark" class="view_back_text" placeholder="请输入备注信息，如：不要辣等"><% =remark %></textarea></p>
                   
                        <p><asp:Repeater runat="server" ID="rptfastremark">
                            <ItemTemplate>
                                <a href="javascript:;" onclick="settag(this)" mytag="<%# Eval("classname") %>">
                                    <%# WebUtility.Left(Eval("classname"),3) %></a>
                            </ItemTemplate>
                        </asp:Repeater></p>
             </div>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function settag(tag) {
        var or = $("#tbremark").val();
        if (or == "请输入备注信息，如：不要辣等") {
            or = "";
        }
        $("#tbremark").val(or + " " + $(tag).attr("mytag"));
    }
</script>


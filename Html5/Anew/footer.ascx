<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="Html5.Anew.footer" %>
<link href="css/common.css" rel="stylesheet" type="text/css" />
<%--    <script src="javascript/jquery.min.js" type="text/javascript"></script>--%>
<script>
    $(function () {
        var tabbar = $(".footers a");
        tabbar.click(function () {
            $(this).addClass("selected").siblings().removeClass("selected");
        });
    });
</script>

<div class="footers">
    <a href="index.aspx" class="ico_b_index selected">首页</a>
    <a href="javascript:" class="ico_b_found">发现</a>
    <a href="myorderlist.aspx" class="ico_b_order">订单</a>
    <a href="javascript:" class="ico_b_mine">我的</a>
</div>

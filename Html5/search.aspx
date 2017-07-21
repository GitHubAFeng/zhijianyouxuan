<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="Html5.search" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <style>
        .page {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
        }
    </style>
</head>
<body>

    <div class="page">
        <div id="page_title">
            <a href="index.aspx" id="back" runat="server" class=" top_left"></a>
            <div class="searchbox">

                <form method="get" action="waimaijie.aspx" onsubmit="return checkdata();">

                    <i class="icon-search"></i>
                    <input class="header_search" name="keyword" id="keyword" placeholder="请输入商品名、商家名" />

                </form>

            </div>
        </div>
    </div>


    <div class="hot_search clearfix" style="display: none">
        <p>热门搜索</p>
        <ul>
            <li>黄焖鸡米饭</li>
            <li>衢州人家</li>
            <li>外婆家</li>
            <li>重庆鸡公煲</li>
        </ul>
    </div>


    <div class="history_search clearfix" style="display: none">
        <p>历史搜索</p>
        <ul>
            <a href="">
                <li><i class="icon-time"></i>黄焖鸡米饭<i class="icon-chevron-right "></i></li>
            </a>
            <a href="">
                <li><i class="icon-time"></i>衢州人家<i class="icon-chevron-right "></i></li>
            </a>
            <a href="">
                <li><i class="icon-time"></i>外婆家<i class="icon-chevron-right "></i></li>
            </a>
            <a href="">
                <li><i class="icon-time"></i>重庆鸡公煲<i class="icon-chevron-right "></i></li>
            </a>
            <a href="">
                <li><i class="icon-time"></i>黄焖鸡米饭<i class="icon-chevron-right "></i></li>
            </a>
        </ul>
        <button type="button">清除历史记录</button>
    </div>



</body>
</html>
<script src="javascript/jquery.js"></script>
<script type="text/javascript">
    function checkdata()
    {
        var keyword = $("#keyword").val();
        if (keyword.length == 0) {
            alert("请输入商品名、商家名");
        }
    }

</script>
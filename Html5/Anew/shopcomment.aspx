<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopcomment.aspx.cs" Inherits="Html5.Anew.shopcomment" %>

<%@ Register Src="~/Anew/header.ascx" TagName="header" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>评价</title>
    <link type="text/css" rel="stylesheet" href="css/reset.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link type="text/css" rel="stylesheet" href="css/shop.css" />
    <script type="text/javascript" src="javascript/importantchange.js"></script>
    <script type="text/javascript" src="javascript/jquery.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:header runat="server" ID="header" />
        <div class="shoppage">
            <!--评分-->
            <div class="commentscore clearfix">
                <div class="com-num">
                    <span>4.3</span>
                    <span>综合评估</span>
                    <span>高于周边商家32.6%</span>
                </div>
                <div class="com-info">
                    <div><span>服务态度</span><span class="star"><span style="width:88%"></span></span><span class="score">4.4分</span></div>
                    <div><span>商品评分</span><span class="star"><span style="width:80%"></span></span><span class="score">4.0分</span></div>
                    <div><span>商品评分</span><span style="margin-left:.5rem">50分钟</span></div>
                </div>
            </div>
            <!--筛选-->
            <div class="screening-label">
                <a class="cur">全部（888）</a><a>很好</a><a>还不错</a><a>再也不想吃了</a><a>值得推荐的</a>
            </div>
            <p class="screening"><img src="image/pic-Tick-green@2x.png" />只看有内容的评价</p>
            <ul class="comment-list-all">
                <li class="comment-content">
                    <div class="comment-img">
                        <img src="image/icon_user_headimg.png" />
                    </div>
                    <ul class="comment-info">
                        <li class="clearfix"><span class="fleft">158****5858</span><span class="fright">2016-07-10 12:54</span></li>
                        <li><span class="star"><span style="width:80%"></span></span></li>
                        <li style="color:#333">味道还不错</li>
                        <li><span class="screening-lab">味道还不错</span></li>
                    </ul>
                </li>
                <li class="comment-content">
                    <div class="comment-img">
                        <img src="image/banner_guo@2x.png" />
                    </div>
                    <ul class="comment-info">
                        <li class="clearfix"><span class="fleft">158****5858</span><span class="fright">2016-07-10 12:54</span></li>
                        <li><span class="star"><span style="width:80%"></span></span></li>
                        <li style="color:#333">味道还不错</li>                      
                    </ul>
                </li>
                   <li class="comment-content">
                    <div class="comment-img">
                        <img src="image/icon_user_headimg.png" />
                    </div>
                    <ul class="comment-info">
                        <li class="clearfix"><span class="fleft">158****5858</span><span class="fright">2016-07-10 12:54</span></li>
                        <li><span class="star"><span style="width:80%"></span></span></li>
                        <li style="color:#333">味道还不错</li>
                        <li><span class="screening-lab">味道还不错</span></li>
                    </ul>
                </li>
                <li class="comment-content">
                    <div class="comment-img">
                        <img src="image/banner_guo@2x.png" />
                    </div>
                    <ul class="comment-info">
                        <li class="clearfix"><span class="fleft">158****5858</span><span class="fright">2016-07-10 12:54</span></li>
                        <li><span class="star"><span style="width:80%"></span></span></li>
                        <li style="color:#333">味道还不错</li>                      
                    </ul>
                </li>

            </ul>
        </div>
    </form>
</body>
</html>

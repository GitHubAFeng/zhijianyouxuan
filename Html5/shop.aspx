<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop.aspx.cs" Inherits="Html5.shop" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>超市</title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/home.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=2" />
    <style type="text/css">
        body {
            background-color: #00bc73;
        }

        .storesitem {
            background-color: #00bc73;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page" style="padding-bottom: 0;">
            <div id="page_title">
                <a href="TogoList.aspx" id="back" runat="server" class=" top_left"></a>
                <h1>鲜果大促，限时抢购</h1>
            </div>
        </div>
        <div class="ad_picuter">
            <img src="images/ad_banner1.png" style="width: 100%;" /></div>
        <div class="top_nav">
            <ul>
                <li>水果</li>
                <li>饮料</li>
                <li>蔬菜</li>
                <li>零食</li>
                <li>日用品</li>
            </ul>
        </div>
        <div class="sortitem">
            <div class="sortlist">
                <div class="sort_title">————&nbsp;水果&nbsp;————</div>
                <div class="sort_content ">
                    <div class="sell clearfix">
                        <div class="pic"><img src="images/fruit01.png" /></div>
                        
                        <ul class="sell_txt">
                            <li style="color:#222">牛油果（进口）</li>
                            <li><span>￥<i>15</i>元/斤</span><span>月销量：<i>200</i></span></li>
                            <li style="border-top:#eaeaea 1px solid; font-size:12px">满25元起送,满30元减5（在线支付),配送费3元,满36元免费送费</li>
                        </ul>
                        <button>选购</button>
                    </div>
                    <div class="sell clearfix">
                        <div class="pic"><img src="images/fruit02.png" /></div>
                        
                        <ul class="sell_txt">
                            <li style="color:#222">山竹</li>
                            <li><span>￥<i>25</i>元/斤</span><span>月销量：<i>200</i></span></li>
                            <li style="border-top:#eaeaea 1px solid">满25元起送</li>
                        </ul>
                        <button>选购</button>
                    </div>
                    <div class="sell clearfix">
                        <div class="pic"><img src="images/fruit03.png" /></div>
                        
                        <ul class="sell_txt">
                            <li style="color:#222">蓝莓</li>
                            <li><span>￥<i>18</i>元/斤</span><span>月销量：<i>200</i></span></li>
                            <li style="border-top:#eaeaea 1px solid">满25元起送</li>
                        </ul>
                        <button>选购</button>
                    </div>
                </div>
            </div>
            <div class="sortlist">
                <div class="sort_title">————&nbsp;蔬菜&nbsp;————</div>
                <div class="sort_content ">
                    <div class="sell clearfix">
                        <div class="pic"><img src="images/vegetable001.jpg" /></div>
                        
                        <ul class="sell_txt">
                            <li style="color:#222">胡萝卜</li>
                            <li><span>￥<i>15</i>元/斤</span><span>月销量：<i>200</i></span></li>
                            <li style="border-top:#eaeaea 1px solid; font-size:12px">满25元起送,满30元减5（在线支付),配送费3元,满36元免费送费</li>
                        </ul>
                        <button>选购</button>
                    </div>
                    <div class="sell clearfix">
                        <div class="pic"><img src="images/vegetable002.jpg" /></div>
                        
                        <ul class="sell_txt">
                            <li style="color:#222">苦苣</li>
                            <li><span>￥<i>25</i>元/斤</span><span>月销量：<i>200</i></span></li>
                            <li style="border-top:#eaeaea 1px solid">满25元起送</li>
                        </ul>
                        <button>选购</button>
                    </div>
                    <div class="sell clearfix">
                        <div class="pic"><img src="images/vegetable003.jpg" /></div>
                        
                        <ul class="sell_txt">
                            <li style="color:#222">豌豆</li>
                            <li><span>￥<i>18</i>元/斤</span><span>月销量：<i>200</i></span></li>
                            <li style="border-top:#eaeaea 1px solid">满25元起送</li>
                        </ul>
                        <button>选购</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

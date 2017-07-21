<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sharedetail.aspx.cs" Inherits="Html5.sharedetail" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1" />
    <script type="text/javascript" src="javascript/jquery.js"></script>
    <style>
        #shareit {
            -webkit-user-select: none;
            display: none;
            position: absolute;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.85);
            text-align: center;
            top: 0;
            left: 0;
            z-index: 105;
        }

            #shareit img {
                max-width: 100%;
            }

        .arrow {
            position: absolute;
            right: 10%;
            top: 5%;
        }

        #share-text {
            margin-top: 400px;
            color: #fff;
        }
    </style>
</head>


<body>
    <div class="page">
        <div id="page_title">
            <a href="sharelist.aspx" data-ajax="false" id="back" class=" top_left"></a>
            <h1>分享红包</h1>
        </div>

        <input id="hfsharetitle" type="hidden" value="<%= SectionProxyData.GetSetValue(73) %>" />
        <input id="hfsharecontent" type="hidden" value="<%= SectionProxyData.GetSetValue(74) %>" />

        <div class="container">

           

            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rptorder">
                    <ItemTemplate>
                        <li>
                          
                            <div class="order-info" style="background-image:none;">
                                <p class="f14">编号：<span class="red"><%#Eval("id")%></span></p>
                                <p class="grey">红包个数：<span id="pcount"><%#Eval("reveint")%></span></p>
                            </div>


                           <input type="hidden" id="shareurl" value="<%# Hangjing.SQLServerDAL.CacheHelper.GetWeiXinAccount().revevar2%>/moneydetail.aspx?urlkey=<%#Eval("revevar")%>" />


                 
                          <div class="view_back_con">
                                <input type="submit" value="分享" id="share_btn" class="view_back_btn"  data-urlkey="<%#Eval("revevar")%>" onclick="payagain(this); return false;" data-ajax="false" style=" border-radius: 5px;" />
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>


    <div class="con-btn" id="pages" runat="server"></div>

    <div id="shareit">
        <img class="arrow" src="images/share-it.png" />
        <div id="share-text">点击右上角分享</div>
    </div>


</body>
</html>
<script src="javascript/jCommon.js"></script>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>


<script>

    /*
     * 注意：
     * 1. 所有的JS接口只能在公众号绑定的域名下调用，公众号开发者需要先登录微信公众平台进入“公众号设置”的“功能设置”里填写“JS接口安全域名”。
     * 2. 如果发现在 Android 不能分享自定义内容，请到官网下载最新的包覆盖安装，Android 自定义分享接口需升级至 6.0.2.58 版本及以上。
     * 3. 常见问题及完整 JS-SDK 文档地址：http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
     *
     * 开发中遇到问题详见文档“附录5-常见错误及解决办法”解决，如仍未能解决可通过以下渠道反馈：
     * 邮箱地址：weixin-open@qq.com
     * 邮件主题：【微信JS-SDK反馈】具体问题
     * 邮件内容说明：用简明的语言描述问题所在，并交代清楚遇到该问题的场景，可附上截屏图片，微信团队会尽快处理你的反馈。
     */
    wx.config({
         debug: false,
        appId: '<%= appId %>',
        timestamp: '<%= timeStamp %>',
        nonceStr: '<%= nonceStr %>',
        signature: '<%= signature %>',
      jsApiList: [
        'checkJsApi',
        'onMenuShareTimeline',
        'onMenuShareAppMessage',
        'onMenuShareQQ',
        'onMenuShareWeibo'
      ]
    });


    


    wx.ready(function () {

        var title = $("#hfsharetitle").val();
        var desc = $("#hfsharecontent").val();
        var link = $("#shareurl").val();
        var imgurl = "http://" + location.host + "/images/logo.png";

        //获取“分享给朋友”按钮点击状态及自定义分享内容接口
        wx.onMenuShareAppMessage({
            title: title,
            desc: desc,
            link: link,
            imgUrl: imgurl,
            success: function (res) {
                //alert('已分享');
                useSharepackage();
            },
            cancel: function (res) {
                //alert('已取消');
            }
        });

        //朋友圈
        wx.onMenuShareTimeline({
            title: title,
            link: link,
            imgUrl: imgurl,
            trigger: function (res) {
                // 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
                //alert('用户点击发送给朋友');
            },
            success: function (res) {
                //alert('已分享');
                useSharepackage();
            },
            cancel: function (res) {
                //alert('已取消');
            },
            fail: function (res) {
                //alert(JSON.stringify(res));
            }
        });

    });

    wx.error(function (res) {
        alert(res.errMsg);
    });


    //分享红包数量减少
    function useSharepackage() {
        jQuery.ajax(
        {
            type: "post",
            url: "/ajaxHandler.ashx",
            data: "method=useSharepackage&id=" + request("id") + "&_=" + new Date().getTime() + "",
            success: function (msg) {
                var count = parseInt($("#pcount").html());
                if (count <= 1) {
                    $(".my_order_list").hide();
                }
                $("#pcount").html(count - 1);

            }
        })
    }




</script>
<script>
   
    $("#shareit").on("click", function () {
        $("#shareit").hide();
    })
    function payagain(target) {
        $("#shareit").show();
    }
</script>


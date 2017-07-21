<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myinfolist.aspx.cs" Inherits="Html5.myinfolist" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=708" />
    <script src="javascript/jquery.js"></script>


    <style type="text/css">
        .bom_menu .icon-mine {
            background-image: url(/images/ico_b_mine_cur.png);
            color: #f39800;
        }
    </style>


</head>




<body>
    <form id="form1" runat="server">
        <div class="page">
            <div id="page_title">
                <a href="index.aspx" id="back" class=" top_left"></a>
                <h1>个人中心</h1>
                <a href="myqrcode.aspx" class="reg top_right">我的二维码</a>
            </div>
            <div class="container ">
                <ul class="my_info_list">
                    <li class="user_main clearfix">
                        <a href="myuserinfo.aspx" data-ajax="false">
                            <div class="user-info">
                                <div class="left_ico"><i class="face"></i></div>
                                <div class="fl">
                                    <p class="f14" id="username" runat="server"></p>
                                    <p class="grey" id="userphone" runat="server">13544773322</p>
                                </div>
                            </div>
                        </a>
                    </li>


                    <li>
                        <div class="user-info" style="background-image: none;">

                            <div class="left_ico"><i class="points"></i></div>
                            <p class="f14 bord">积分：<label id="lbpoint" runat="server"></label></p>

                        </div>
                        <div class="user-info">
                            <span style="position: absolute; right: 18px; top: 18px;">充值</span>
                            <a href="recharge.aspx" data-ajax="false">
                                <div class="left_ico"><i class="points"></i></div>
                                <p class="f14 ">余额：<label id="lbmoney" runat="server"></label></p>
                            </a>
                        </div>


                    </li>



                    <li>
                        <div class="user-info">
                            <a href="myorderlist.aspx?t=1" data-ajax="false">
                                <div class="left_ico"><i class="t_order"></i></div>
                                <p class="f14 bord">今日订单</p>
                            </a>
                        </div>
                        <div class="user-info">
                            <a href="myorderlist.aspx" data-ajax="false">
                                <div class="left_ico"><i class="orders"></i></div>
                                <p class="f14">全部订单</p>
                            </a>
                        </div>
                    </li>
                    <li>

                          <div class="user-info">
                            <a href="sharelist.aspx" data-ajax="false">
                                <div class="left_ico"><i class="gift"></i></div>
                                <p class="f14 bord">待分享红包</p>
                            </a>
                        </div>
                        <div class="user-info">
                            <a href="moneylist.aspx" data-ajax="false">
                                <div class="left_ico"><i class="gift"></i></div>
                                <p class="f14 bord">我的红包</p>
                            </a>
                        </div>


                        <div class="user-info">
                            <a href="myAddresslist.aspx" data-ajax="false">
                                <div class="left_ico"><i class="addresss"></i></div>
                                <p class="f14 bord">地址管理</p>
                            </a>
                        </div>
                        <div class="user-info">
                            <a href="PayPwd.aspx" data-ajax="false">
                                <div class="left_ico"><i class="password"></i></div>
                                <p class="f14">设置支付密码</p>
                            </a>
                        </div>
                    </li>
                    <li>

                        <div class="user-info">
                            <a href="myshops.aspx" data-ajax="false">
                                <div class="left_ico"><i class="fav"></i></div>
                                <p class="f14 bord">我的收藏</p>
                            </a>
                        </div>
                        <div class="user-info">
                            <a href="Feedback.aspx" data-ajax="false">
                                <div class="left_ico"><i class="message"></i></div>
                                <p class="f14">我的评论</p>
                            </a>
                        </div>
                    </li>

                    <li>
                        <div class="user-info no_bg">
                            <div class="left_ico"><i class="points"></i></div>
                            <p class="f14 bord"><span id="userPoint" runat="server">100</span>我的积分</p>
                        </div>
                        <div class="user-info">
                            <a href="myChange.aspx" data-ajax="false">
                                <div class="left_ico"><i class="gift"></i></div>
                                <p class="f14 bord">我的礼品</p>
                            </a>
                        </div>
                        <div class="user-info">
                            <a href="myshopcard.aspx" data-ajax="false">
                                <div class="left_ico"><i class="coupon"></i></div>
                                <p class="f14">我的优惠券</p>
                            </a>
                        </div>
                    </li>
                    <li>
                        <div class="user-info">
                            <a href="myexpresslist.aspx" data-ajax="false">
                                <div class="left_ico"><i class="ptorder"></i></div>
                                <p class="f14 bord">我的跑腿订单</p>
                            </a>
                        </div>
                        <div class="user-info">
                            <a href="express.aspx" data-ajax="false">
                                <div class="left_ico"><i class="pt"></i></div>
                                <p class="f14 ">跑腿下单</p>
                            </a>
                        </div>
                    </li>

                </ul>


                <div class="view_back_con" style="margin-top: 20px;">
                    <input type="button" value="退出当前账号" onclick="return gourl('logout.aspx')" class="view_back_btn" data-ajax="false" />
                </div>

            </div>
        </div>
    </form>

    <uc2:Foot runat="server" ID="foot" />


</body>
</html>
<script src="javascript/jCommon.js"></script>

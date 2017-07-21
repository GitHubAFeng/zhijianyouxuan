<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Left.ascx.cs" Inherits="UserHome_Left" %>

<script type="text/javascript">
    $(document).ready(function () {
        initnav(5);
    })
</script>
<script type="text/javascript">
    function showmenu(index) {
        $(".my" + index).toggle();
        var s = $(".my" + index).css("display");
        if (s == "none") {
            $("#img_" + index).attr("src", "Images/add_03.png")
        }
        else {
            $("#img_" + index).attr("src", "Images/cut_03.png")
        }
    }
</script>
<div class="leftmenu_cont">
    <a href="myindex.aspx">
        <h2>管理首页</h2>
    </a>
    <div class="left_gr_pic">
        <img src="../images/nopic_02.jpg" runat="server" id="imghead" alt="会员头像" />
        <p>
            名称：<asp:Literal ID="litName" runat="server"></asp:Literal>
        </p>
        <p style="display: none;">
            等级：<asp:Literal ID="litStatus" runat="server"></asp:Literal>
        </p>
        <p>
            积分：<asp:Literal ID="LitPoint" runat="server"></asp:Literal>
        </p>
        <p>
            余额：<asp:Literal ID="LitUserMoney" runat="server"></asp:Literal>
        </p>
          <p>
            分销账户：<asp:Literal ID="litgroupid" runat="server"></asp:Literal>
        </p>

    </div>
    <ul class="mo_wlft_ul">
        <li class="orderinfo" id="fli_0" onclick="showmenu(0);">
            <div class="mo_wlft_ul_div">
                <img src="Images/cut_03.png" id="img_0">
            </div>
            订单管理</li>
        <li class="my0"><a href="MyOrderList.aspx?state=0">今日订单</a> </li>
        <li class="my0"><a href="MyOrderList.aspx">所有订单</a> </li>
        <li class="my0"><a href="MyexpressOrder.aspx">跑腿订单</a> </li>

        <li class="orderinfo " id="fli_1" onclick="showmenu(1);">
            <div class="mo_wlft_ul_div">
                <img src="Images/cut_03.png" id="img_1">
            </div>
            帐户管理</li>

        <li class="my1"><a href="MyInfo.aspx">个人资料</a></li>
        <li class="my1"><a href="UpdatePwd.aspx">修改密码</a></li>
        <li class="my1"><a href="MyPointCount.aspx">我的积分</a></li>
        <li class="my1"><a href="PayPwd.aspx">设置支付密码</a></li>
        <li class="my1"><a href="myshopcard.aspx">优惠券</a></li>
        <li class="my1"><a href="RechargeList.aspx">充值记录</a></li>
        <li class="my1"><a href="delmoneyList.aspx">帐户消费记录</a></li>
        <li class="my1"><a href="Recharge.aspx">支付宝充值</a></li>

        <li class="my1"><a href="MyAddress.aspx">收货地址簿</a></li>
        <li class="orderinfo" id="fli_2" onclick="showmenu(2);">
            <div class="mo_wlft_ul_div">
                <img src="Images/cut_03.png" id="img_2">
            </div>
            收藏夹管理</li>
        <li class="my2"><a href="MyShops.aspx">店铺收藏</a></li>

        <li class="orderinfo" id="Li1" onclick="showmenu(3);">
            <div class="mo_wlft_ul_div">
                <img src="Images/cut_03.png" id="img1">
            </div>
            礼品兑换管理</li>
        <li class="my3"><a href="mychange.aspx">礼品兑换</a></li>
        <li class="orderinfo" id="fli_3" onclick="showmenu(3);">
            <div class="mo_wlft_ul_div">
                <img src="Images/cut_03.png" id="img_3">
            </div>
            留言评论管理</li>
        <li class="my3"><a href="ShopCommentList.aspx">餐馆评价</a></li>
        <li class="my3" style="display: none;"><a href="WebMessageLogList.aspx">站内信</a></li>
    </ul>
</div>


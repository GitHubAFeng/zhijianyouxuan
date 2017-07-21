<%@ Control Language="C#" AutoEventWireup="true" CodeFile="rightbar.ascx.cs" Inherits="shop_rightbar" %>


<div class="containers">

    <div class="rightsidebar_box">
        <asp:Repeater ID="rptTogo" runat="server">
            <ItemTemplate>
                <div class="shopinfo">
                    <img src="Images/shoplogo.png" />
                    <h1><%#Eval("name")%></h1>
                    <p><%#Eval("Address")%></p>
                </div>
                <div class="openorclose">
                    <%# shopdate(Eval("isonline").ToString())%>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <ul class="thetabs">
            <li class="cur">今日经营</li>
            <%--<li>消息通知<i class="thenew"></i></li>--%>
        </ul>
        <div class="clear"></div>
        <div class="tabcag">
            <div>
                <img src="Images/pic-sales.png" />
                <span>今日营业额</span>
                <span id="salemoney" runat="server">0</span>
            </div>
            <div>
                <img src="Images/pic-order.png" />
                <span>今日订单</span>
                <span id="noworder" runat="server">0</span>
            </div>
            <div>
                <img src="Images/pic-withdrawal.png" />
                <span>可提现金额</span>
                <span id="mymoney" runat="server">0</span>
            </div>
            <div>
                <img src="Images/pic-commission.png" />
                <span>今日佣金</span>
                <span id="nowmoney" runat="server">0</span>
            </div>
        </div>
    </div>

</div>
<script>
    $(function () {
        $(".openorclose span").click(function () {
            $(this).addClass("current").siblings().removeClass("current")
        });
    })
    $(function () {
        $(".tab li").click(function () {
            $(this).addClass("cur").siblings().removeClass("cur")
        });
    })

</script>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="familyhonor.aspx.cs" Inherits="Html5.familyhonor" %>

<%@ Register Src="~/header.ascx" TagName="head" TagPrefix="uc3" %>
<%@ Register Src="~/distributorfooter.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <title>
        <%= SectionProxyData.GetSetValue(2) %></title>
    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#food_menu_3").addClass("current_page_item");
            $(".toggleitem").toggle(function () {
                $(this).children("span.d_text").eq(0).hide();
                $(this).children("span.d_text").eq(1).show()
                $(this).toggleClass("currentDd").siblings(".subNav").removeClass("currentDd")
                $(this).toggleClass("currentDt").siblings(".subNav").removeClass("currentDt")
                // 修改数字控制速度， slideUp(500)控制卷起速度
                $(this).next(".navContent").slideToggle(500).siblings(".navContent").slideUp(500);
            }, function () {
                $(this).children("span.d_text").eq(1).hide();
                $(this).children("span.d_text").eq(0).show()
                $(this).toggleClass("currentDd").siblings(".subNav").removeClass("currentDd")
                $(this).toggleClass("currentDt").siblings(".subNav").removeClass("currentDt")
                // 修改数字控制速度， slideUp(500)控制卷起速度
                $(this).next(".navContent").slideToggle(500).siblings(".navContent").slideUp(500);
            })
        })
    </script>
    <style>
        body {
            background-color: #f8f8f8;
        }

        .navContent .childitem:last-child {
        
            
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:head runat="server" ID="head" />
        <div class="wrap clearfix">

            <asp:Repeater runat="server" ID="rptuser">
                <ItemTemplate>

                    <div class="main_title">
                        <ul>
                            <li style="border-right: #fff 1px solid;">可提现佣金：<%# Eval("groupid") %>元</li>
                            <li>历史佣金：<%# Eval("distributemoney") %>元</li>
                        </ul>
                    </div>
                    
                     <div class="main_title" style="margin-top:10px;display: <%# Convert.ToInt32(Eval("DataID")) < 0 ? "none" :""  %>">
                        <ul>
                            <li style="border-right: #fff 1px solid;">区域订单：<%# Eval("DataID") %>单</li>
                            <li>区域提成：<%# Eval("Usermoney") %>元</li>
                        </ul>
                    </div>

                    <div class="recommend" style="display: <%# Eval("ActivateCode").ToString().Length == 0 ? "none" :""  %>">
                        <p>您是由<i><%# Eval("ActivateCode") %></i>推荐</p>

                    </div>
                </ItemTemplate>
            </asp:Repeater>



            <div class="subNavBox" style=" padding-bottom:50px;">

                <div class="subNav currentDd currentDt toggleitem">
                    <img src="images/family.png" />家族成员<span class="f_text"><span runat="server" id="allchild"></span>人</span>
                </div>
                <ul class="navContent " style="display: block">
                    <asp:Repeater runat="server" ID="rptchild">
                        <ItemTemplate>
                           
                            <li><a href="myfamilymb.aspx?type=<%# Convert.ToInt32(Container.ItemIndex + 1)%>" class="childitem">
                                <img src="images/lv.png" /><%# Eval("classname") %>级会员<span class="f_text"><%# Eval("id") %>人</span></a></li>
                           

                        </ItemTemplate>
                    </asp:Repeater>

                </ul>


                 <a href="mydistributelist.aspx">
                    <div class="subNav tx_sbnav">
                        <img src="images/money.png" />佣金明细<span class="d_text">点击查看</span><span class="d_text">点击查看</span>
                    </div>
                </a>

                <a href="CashAdvance.aspx">
                    <div class="subNav tx_sbnav">
                        <img src="images/withdraw.png" />申请提现<span class="d_text">点击进入</span><span class="d_text">点击进入</span>
                    </div>
                </a>

            </div>

        </div>

    </form>
    <uc2:Foot runat="server" ID="footer" />
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gift.aspx.cs" Inherits="Gift_Gift1" %>

<%@ Register Src="~/Banner.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/header.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/Foot.ascx" TagName="foot" TagPrefix="uc3" %>
<%@ Register Src="giftleft.ascx" TagName="giftleft" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>积分商城 - <%= SectionProxyData.GetSetValue(3) %>
        
    </title>
    <link href="../css/common.css" rel="stylesheet" />
    <link href="../css/gift.css?v=0108" rel="stylesheet" />


    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.lightbox.min.js"></script>

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            initnav(4);
        })

        $(function() {
            $(".smooth").click(function() {
                var href = $(this).attr("href");
                var pos = $(href).offset().top;
                $("html,body").animate({ scrollTop: pos }, 600);
                return false;
            });
        })

    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <uc1:banner ID="Banner1" runat="server" />
        <uc2:header ID="header" runat="server" />
        <div class="wrap margin_b10" id="n_div2">
            <div class="hplace_bg">
                <span class="color_1 hplace_house" id="dangqian" runat="server"><a href="../index.aspx">首页</a> >>
                积分商城</span>
            </div>
            <uc2:giftleft runat="server" ID="myleft" />
            <div class="hgift_right" style="margin-bottom: 15px">
                <div class="htitle">
                    <div class="htitle_bg">
                        <h4>积分兑换</h4>
                    </div>
                </div>
                <div class="hgift_right_bot">
                    <div class="gift_category">
                        <strong class="fl">分类：</strong> <span class="fl">
                            <a href="#sort0" class="smooth">优惠券</a>
                            <asp:Repeater runat="server" ID="rptSortList">
                                <ItemTemplate>
                                    | <a href="#sort<%#Eval("ClassId")%>" class="smooth">
                                        <%# Eval("classname")%></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </span>
                        <br class="clear" />
                    </div>

                    <div class="hgift_div_01" id="sort0">
                        <div class="hgift_title_01">
                            优惠券
                        </div>
                        <div class="hgift_content">
                            <ul>
                                <asp:Repeater runat="server" ID="rptVoucher">
                                    <ItemTemplate>
                                        <li>
                                            <div class="hgift_body_01">
                                                <div class="hbody_left">
                                                    <p>
                                                        <a title="<%# Eval("title") %>" href="ShowCard.aspx?id=<%#Eval("dataid") %>" id="a_<%# Eval("dataid") %>">
                                                            <img src='<%#WebUtility.ShowPic(Eval("Inve2").ToString()) %>' alt="<%# Eval("title") %>"
                                                                onmouseover="showborder(this , 'hasborder')" onmouseout="showborder(this)" width="100px"
                                                                height="100px" id="pic_<%# Eval("dataid") %>" /></a>
                                                    </p>
                                                    <p>
                                                        <a href="<%# Eval("Inve2").ToString().Replace("~","..") %>" rel="lightbox-tour"
                                                            title="<%# Eval("title") %>">查看大图</a>
                                                    </p>
                                                </div>
                                                <div class="hbody_right">
                                                    <h4><a title="<%# Eval("title") %>" href="ShowCard.aspx?id=<%#Eval("dataid") %>">
                                                        <%# WebUtility.Left( Eval("title").ToString() , 11) %></a></h4>
                                                    <p>
                                                        兑换积分：<span><%# Eval("mydiscount")%>个</span>
                                                    </p>
                                                    <p>
                                                        优惠类型：<span><%# Eval("TogoName")%></span>
                                                    </p>
                                                    <p>
                                                        库存数量：<span><%# Eval("CardCount")%></span>
                                                    </p>
                                                    <p>
                                                        <input class="shop_send_btn" name="" type="button" value="我要兑换" onclick="window.location = 'ShowCard.aspx?id=<%#Eval("dataid") %>    '" />
                                                    </p>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <asp:Repeater runat="server" ID="rpttype">
                            <ItemTemplate>
                                <div class="hgift_div_01" id="sort<%# Eval("ClassId") %>">
                                    <div class="hgift_title_01">
                                        <%# Eval("classname") %>
                                    </div>
                                    <div class="hgift_content">
                                        <ul>
                                            <asp:Repeater runat="server" ID="rptGiftList" DataSource='<%# getsub(Eval("ClassId") , (Container.ItemIndex+1).ToString()) %>'>
                                                <ItemTemplate>
                                                    <li>
                                                        <div class="hgift_body_01">
                                                            <div class="hbody_left">
                                                                <p>
                                                                    <a title="<%# Eval("Gname") %>" href="ShowGift.aspx?id=<%#Eval("giftsid") %>" id="a_<%# Eval("giftsid") %>">
                                                                        <img src='<%#WebUtility.ShowPic(Eval("Picture").ToString()) %>' alt="<%# Eval("Gname") %>"
                                                                            onmouseover="showborder(this , 'hasborder')" onmouseout="showborder(this)" width="100px"
                                                                            height="100px" id="pic_<%# Eval("giftsid") %>" /></a>
                                                                </p>
                                                                <p>
                                                                    <a href="<%# Eval("bigpicture").ToString().Replace("~","..") %>" rel="lightbox-tour"
                                                                        title="<%# Eval("Gname") %>">查看大图</a>
                                                                </p>
                                                            </div>
                                                            <div class="hbody_right">
                                                                <h4><a title="<%# Eval("Gname") %>" href="ShowGift.aspx?id=<%#Eval("giftsid") %>">
                                                                    <%# WebUtility.Left( Eval("Gname").ToString() , 11) %></a></h4>
                                                                <p>
                                                                    需要积分：<span class="orange"><%# Eval("NeedIntegral")%>个</span>
                                                                </p>
                                                                <p>
                                                                    市场价格：<span class="orange">￥<%# Convert.ToDecimal( Eval("GiftsPrice")).ToString("#0.0")%></span>
                                                                </p>
                                                                <p>
                                                                    库存数量：<span class="orange"><%# Eval("stocks")%></span>
                                                                </p>
                                                                <p>
                                                                    <input class="shop_send_btn" name="" type="button" value="我要兑换" onclick="window.location = 'GetGift.aspx?id=<%#Eval("giftsid") %>    &buy=0'" />
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="clear">
                        </div>
                        <div class="gift_prompt">
                            温馨提醒:以上所有图片均以实物为准。请注意“查看实物”：以上标注的兑换积分和购买价格均指单件礼品！
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

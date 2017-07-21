<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="OrderDetail" %>

<!DOCTYPE html>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />
    <title>确认订单 - <%=SectionProxyData.GetSetValue(3) %></title>
    <link href="css/common.css" rel="stylesheet" />
    <link href="css/orderinfo.css" rel="stylesheet" />
    <script src="javascript/jquery-1.7.min.js"></script>

    <style type="text/css">
        .main-tit ul.left li {
            text-align: left;
        }

        .restaurant-icons {
            display: inline-block;
            height: 15px;
            margin-right: 5px;
            width: 15px;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidUid" Value="0" />
        <top:banner ID="Banner1" runat="server" />
        <uc1:header ID="header" runat="server" />
        <asp:HiddenField ID="hfshopcardcount" Value="1" runat="server" />
        <asp:HiddenField ID="hfshopcardpay" Value="0" runat="server" />
        <asp:HiddenField ID="hfshopcardinfo" Value="" runat="server" />
        <asp:HiddenField ID="hffoodprice" Value="0" runat="server" />
        <asp:HiddenField ID="hfsalemoney" Value="0" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="wrap">
                    <div class="mainbord mgb15">
                        <div style="box-shadow: none; border-bottom: 1px solid #ddd;" class="main-tit">
                            <ul class="left">
                                <li class="cul" style="width: 200px; text-align: left;">我的餐车(<a href="" runat="server" id="backurl" style="font-weight: normal; font-size: 13px;">&lt;&lt;&nbsp;返回商家修改</a>)</li>
                            </ul>
                        </div>
                        <div class="order-con">
                            <table width="100%" class="order_table">
                                <tbody>
                                    <tr>
                                        <th width="32%">商品</th>
                                        <th>单价</th>
                                        <th>加价</th>
                                        <th>数量</th>
                                        <th>小计</th>
                                        <th>操作</th>
                                    </tr>

                                    <asp:Repeater ID="rptorder" runat="server" OnItemCommand="rptorder_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="fir">

                                                    <img style="width: 86px; height: 86px; <%#Convert.ToString(Eval("Picture")).Trim() == "" ? "display:none": ""%>; float: left;" src="<%# WebUtility.ShowPic(Eval("Picture").ToString())%>">

                                                    <div><%#Eval("PName") %></div>
                                                    <div class=" clear"></div>

                                                </td>
                                                <td>
                                                    <span class="orange">￥<%#Eval("PPrice") %></span></td>
                                                 <td>
                                                    <span class="orange">￥<%#Eval("addprice") %></span></td>
                                                <td>
                                                    <span class="bord_icon">
                                                        <asp:LinkButton ID="cutnum" runat="server" OnClientClick='<%# "return cutcount("+ Eval("DataId")+","+ Eval("PNum")+",0)"%>' CommandName="cut" CommandArgument='<%#Eval("DataId") +"|"+ Eval("PNum")%>'>-</asp:LinkButton>
                                                    </span>
                                                    <span class="cartnum"><%#Eval("PNum") %></span>
                                                    <span class="bord_icon">
                                                        <asp:LinkButton ID="addnum" runat="server" OnClientClick='<%# "return addcount("+ Eval("DataId")+","+ Eval("PNum")+",0)"%>' CommandName="add" CommandArgument='<%#Eval("DataId")+ "|" +Eval("PNum")%>'>+</asp:LinkButton>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span class="orange">￥<%#Eval("AllPrice") %></span></td>
                                                <td>
                                                    <asp:LinkButton CommandName="del" CommandArgument='<%#Eval("dataid")%>' runat="server" ID="delOrder" CssClass="del_icon"></asp:LinkButton>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>

                            <label style="display: none" id="lbtogominmoney" runat="server"></label>
                            <p class="sumsize">共<span runat="server" id="count" class="orange f16 padlr10"></span>件商品<span style="padding-left: 50px;">配送费：<span runat="server" id="lbsendfee" class="orange f16 padlr10"></span>元</span><span style="padding-left: 50px;">打包费：<span runat="server" id="lbpackage" class="orange f16 padlr10"></span>元</span><span style="padding-left: 50px;">总计：<span runat="server" id="allprice" class="orange f16 padlr10"></span>元</span></p>
                        </div>
                        <div style="box-shadow: none; border-bottom: 1px solid #ddd;" class="main-tit">

                            <ul class="left">
                                <li class="cul">送餐信息</li>
                            </ul>
                        </div>
                        <div class="songpad">
                            <div class="txtp"><span class="le_in"><span class="red">*</span>联系人：</span>
                                <input id="tbname" type="text" style="width: 140px;" class="t_text" runat="server" /></div>
                            <div class="txtp">
                                <span class="le_in"><span class="red">*</span>送餐地址：</span>
                                <input id="tbaddress" type="text" style="width: 320px;" class="t_text" runat="server" disabled="disabled" />
                                <input id="historyAddr" type="button" value="我曾经使用过的地址" runat="server" style="display: none;" />

                                <asp:Repeater ID="rptaddress" runat="server" OnItemCommand="rptaddress_ItemCommand">
                                    <ItemTemplate>
                                        <div class="infop" style="display: none; margin-left: 15px">
                                            <input name="rbaddress" id="adid_<%#Eval("dataid") %>" type="radio" addressid='<%#Eval("dataid") %>'
                                                onclick="setaddr(this);" value='<%# Eval("Receiver") %>^<%# Eval("Address")%>^<%# Eval("lat") %>^<%# Eval("lng")%>^<%# Eval("dataid") %>^<%# Eval("BuildingID") %>^<%# Eval("Mobilephone")%>^<%# Eval("Phone") %>'
                                                class="fvtb" />
                                            <label for="adid_<%#Eval("dataid") %>">
                                                <%#Eval("Address") %>
                                            </label>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                            <div class="txtp">
                                <span class="le_in"><span class="red">*</span>详细地址：</span>
                                <input id="tbdetailaddress" type="text" style="width: 320px;" class="t_text" runat="server" />
                                <span style="color: red;">如小区须填写具体门牌号、网吧必须填写几号机、否则无法送餐。</span>
                            </div>
                            <div class="txtp">
                                <span class="le_in"><span class="red">*</span>联系电话：</span>
                                <input id="tbtel" type="text" style="width: 145px;" class="t_text" runat="server" />
                                <span style="color: red;">请确认手机号码无误，并保持手机畅通，否则可能导致无法送餐。</span>
                            </div>
                            <div class="txtp">
                                <span class="le_in"><span class="red">*</span>送达时间：</span>
                                <asp:DropDownList runat="server" ID="ddltime" Style="width: 80px;">
                                    <asp:ListItem> </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="txtp">
                                <span style="vertical-align: top;" class="le_in"><span class="red"></span>送餐备注：</span>
                                <textarea runat="server" id="tbremark" style="border: 1px solid #ddd; width: 468px; height: 90px; line-height: 22px; padding: 3px;"></textarea>
                            </div>
                            <div style="padding-left: 100px; margin-top: -11px;" class="quick_note_control">
                                <asp:Repeater runat="server" ID="rptfastremark">
                                    <ItemTemplate>
                                        <span onclick="settag(this)" mytag="<%#Eval("classname")%>">
                                            <%# Eval("classname") %></span>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div style="box-shadow: none; border-bottom: 1px solid #ddd;" class="main-tit">

                            <ul class="left">
                                <li class="cul">结算信息</li>
                            </ul>
                        </div>
                        <div class="songpad">
                            <div class="txtp">
                                <div class="ordercon">
                                    <span class="tname">优惠券：</span>
                                    <asp:RadioButtonList runat="server" ID="rptusercard" CssClass="cart_table" onclick="usercardchange()"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="true">不使用</asp:ListItem>
                                        <asp:ListItem Value="1">使用</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <div class="clear">
                                    </div>
                                    <div class="order_intro" id="giftcardpay_div" style="display: none;">
                                        <div class="card_title">
                                            <p>
                                                商品原价：<span id="spanfoodprice"><%= foodprice%></span>元 <span id="spansaleprice" runat="server" style="display: none;">0.00</span> - 优惠券：<span id="spanshopcardprice">0.00</span>元
                                                = 商品现价：<span id="spanpaymenry" style="color: Red; font-weight: bold;" runat="server"><%= foodprice%></span>元
                                            </p>
                                        </div>
                                        <div class="order_gift_card">
                                            <h3>提示：一个订单只能使用一张优惠券
                                            </h3>
                                            <h4>绑定优惠券</h4>
                                            <p>
                                                券号：<input id="tbpwd1" type="text" name="textfield2" class="text" style="width: 50px;"
                                                    maxlength="4" onkeyup="nextinput('tbpwd1','tbpwd2')" />-<input id="tbpwd2" class="text"
                                                        type="text" name="textfield2" style="width: 50px;" maxlength="4" onkeyup="nextinput('tbpwd2','tbpwd3')" />-<input
                                                            id="tbpwd3" type="text" name="textfield2" class="text" style="width: 50px;" maxlength="4" />
                                                <input name="" onclick="bindmyshopcard()" type="button" class="gift_card_sure_btn"
                                                    value="确定" /><span class="gray_3" style="padding-left: 5px;">券号为数字0-9或者大写字母A-F</span>
                                            </p>
                                            <h4><a id="Bindingurl" runat="server" href="javascript:">绑定优惠券 >></a>我的优惠券</h4>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="order_gift_card_table"
                                                id="tabcard_banded">
                                                <tr>
                                                    <th style="width: 25%">券号
                                                    </th>
                                                    <th>说明 
                                                    </th>

                                                </tr>
                                                <asp:Repeater ID="rptcartlist" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <input id="card_<%#Eval("CID") %>" onclick="setthisshopcard(this)" class="shopcardcheck"
                                                                    cid="<%#Eval("CID") %>" ckey="<%#Eval("ckey") %>"
                                                                    cprice="<%#Eval("point") %>" price="0"
                                                                    name="cardcheck" type="checkbox" moneydoor="<%#Eval("moneyline") %>" value="" cardtype="<%#Eval("ReveInt1") %>" />
                                                                <label for="card_<%#Eval("CID") %>">
                                                                    <%#Eval("ckey") %></label>
                                                            </td>
                                                            <td align="left"><%#  Eval("ReveVar1")%>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <input type="hidden" id="paynum" value="0" />
                                            <input type="hidden" id="checkedid" value="" />
                                            <h5>使用了 <span id="shopcardnum">0</span> 张优惠券，优惠 <span id="shopcutprice" runat="server">0.00</span> 元</h5>
                                        </div>
                                    </div>
                                </div>

                                <div class="clear"></div>

                            </div>
                            <div class="txtp" runat="server" id="promotionbox">
                                <span class="le_in" style="float:left;"><span class="red"></span>优惠活动：</span>

                                <div style="width:500px; float:left;">

                                    <asp:Repeater runat="server" ID="rptpromotion">
                                        <ItemTemplate>
                                            <div>
                                                <span class="restaurant-icons  tooltip_on" style="background: url('/images/jian_02.png') no-repeat scroll 0 0 rgba(0, 0, 0, 0)"></span><%#Eval("revevar1")%>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>

                                <div class="clear"></div>
                            </div>


                            <div class="txtp">
                                <span class="le_in"><span class="red">*</span>支付方式：</span>
                                <asp:RadioButtonList ID="rblpay" runat="server" RepeatDirection="Horizontal" onclick="payusermoney();" CssClass="cart_table">
                                </asp:RadioButtonList>
                                <div class="clear"></div>
                            </div>
                            <div class="txtp" style="display: none;" id="paypassword">
                                <span class="le_in"><span class="red">*</span>支付密码：</span>
                                <input class="info_box w_150p" name="" type="password" runat="server" id="tbpaypwd"
                                    maxlength="50" />
                                <span id="pwdmsg" runat="server" style="padding-left: 10px;">您还没有设置支付密码，点击 <a href="user/PayPwd.aspx" class="orange"
                                    runat="server" id="setpaypwd">这里</a> 设置！</span>

                                <span>当前余额为：<span style="color: red;"><label id="lbaccountmoney" runat="server">0</label></span>元</span>

                            </div>


                            <div class="btn">
                                <asp:Button ID="btncheck" runat="server" OnClientClick="return CheckCart()" CssClass="order_check_btn" OnClick="btncheck_Click" Text="确认下单" />
                            </div>
                        </div>


                        <div class="tipinfo">
                            <h1>点餐小贴士</h1>
                            <%=SectionProxyData.GetSetValue(54) %>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <foot:foot ID="Foot1" runat="server" />

        <div style="display: none;">
            <div id="showtip">
                <div style="width: 400px; height: 120px; padding: 8px; line-height: 250%">
                    您还没有登录！登录后订餐，获得<font color="#FF8A00"><strong>积分</strong></font>和<font color="#FF8A00"><strong>礼品</strong></font>哦~。<br />
                    你可以立即
        <input type="button" id="login_first" value=" " style="background: url(images/button_login.gif); width: 131px; height: 35px; border: 0; cursor: pointer;"
            onclick="gologin()" />
                    <input type="button" value=" " style="background: url(images/button_reg.gif); width: 131px; height: 35px; border: 0; cursor: pointer;"
                        onclick="goreg()" />
                    <div style="margin-top: 10px; padding-left: 35px;">
                        或者
                <input type="button" value=" " style="background: url(images/jjj_direct_order_button.jpg); width: 132px; height: 39px; border: 0; cursor: pointer;"
                    onclick="$.jBox.close()" />

                    </div>
                </div>
            </div>
        </div>

        <script id="cardTemplate" type="text/x-jsrender">

            <tr>
                <td>
                    <input id="card_{{:CID}}" onclick="setthisshopcard(this)" class="shopcardcheck"
                        cid="{{:CID}}" ckey="{{:ckey}}"
                        cprice="{{:point}}" price="0"
                        name="cardcheck" type="checkbox" moneydoor="{{:moneyline}}" value="" cardtype="{{:ReveInt1}}" />
                    <label for="card_{{:CID}}">{{:ckey}}</label>
                </td>
                <td align="left">{{:ReveVar1}}
                </td>

            </tr>

        </script>

    </form>
</body>
</html>
<script src="javascript/ShowDivDialog.js"></script>
<script src="javascript/submitorder.js?v=0203"></script>
<script type="text/javascript" src="javascript/jsrender.js"></script>
<script src="javascript/json2.js"></script>
<script src="javascript/orderaddress.js"></script>



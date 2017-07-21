<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showMenu.aspx.cs" Inherits="qy_54tss_AreaAdmin_Service_showMenu" %>

<%@ Register Src="CrmTop.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="CRMLeft.ascx" TagName="left" TagPrefix="uc1" %>
<%@ Register Src="~/stylebanner.ascx" TagName="stylebanner" TagPrefix="uc4" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>客服系统 -
        <%= WebUtility.GetMyName()%></title>
    <link type="text/css" rel="stylesheet" href="css/indis_style.css" />
    <link href="/css/cart.css" rel="stylesheet" />


    <script src="../../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        //显示这个分类的菜,0表示全部
        function showsort(sortid) {
            $(".smooth").removeClass("class_hover");
            $("#j_s_" + sortid).addClass("class_hover");
            $(".food_items").show();
            if (sortid != 0) {
                $(".sortitem").hide();
                $(".sortitem_" + sortid).show();
            }
            else {
                $(".sortitem").show();
            }
           
        }

        $(document).ready(function() {
            $(".noaccessitem").attr('disabled', true);
        })

    </script>

    <script language="javascript" type="text/javascript">
        function searchfoodname() {
            var key = $("#keyword").val() + "";
            if (key == "" || key == "请输入餐品") {
                $(".food_items").show();
                return;
            }
            $(".hdish_ul li").each(function() {
                var cname = $(this).attr("cname");
                if (cname.indexOf(key) >= 0) {
                    $(this).show();
                }
                else {
                    $(this).hide();
                }
            })
        }
    </script>

    <style type="text/css">
        .noseeitem {
            display: none;
        }

        #keyword {
            color: Gray;
        }

        #divfood {
            margin: 5px 0px 0px 5px;
        }

        .hdish_ul .hover {
            background: none repeat scroll 0 0 #72A8DF;
            color: #FFFFFF;
        }

            .hdish_ul .hover .d_name {
                color: #FFFFFF;
            }

        .hdish_ul li:hover {
            background: none repeat scroll 0 0 #72A8DF;
            color: #FFFFFF;
        }

            .hdish_ul li:hover span {
                color: #FFFFFF;
            }

            .hdish_ul li:hover .shopFhandle {
                background: none repeat scroll 0 0 #4784C1;
            }

        .hdish_ul li .shopFhandle {
            color: #FFFFFF;
            font-size: 24px;
            height: 29px;
            left: 0;
            line-height: 29px;
            position: absolute;
            text-align: center;
            top: 0;
            font-weight: bold;
            width: 30px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="wrap">
            <asp:HiddenField runat="server" ID="hfname" />
            <asp:HiddenField runat="server" ID="hidTogoId" />
            <asp:HiddenField runat="server" ID="hidTogoName" />
            <input type="hidden" id="hnTogoStatus" runat="server" />
            <asp:HiddenField ID="hidUid" runat="server" Value="-1" />
            <asp:HiddenField runat="server" ID="hftime" />
            <asp:HiddenField runat="server" ID="hfcode" />
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="infaq_con">
                <uc1:left runat="server" ID="left" />
                <div class="indis_right">
                    <div class="indis_right_top">
                        <div class="infaq_list_title" style="line-height: 60px; height: 50px">
                            <input type="button" onclick="gourl('OrderCrm.aspx?type=change&tel=<%= Request["tel"] %>    ')" value="重新选择商家"
                                style="margin: 10px; float: left; margin-right: 20px;" class="user_deng" />
                            <div style="float: left;">
                                <span>送达时间：<font runat="server" id="strongsendtime">0</font>分钟</span> <span style="padding-left: 10px;">起送额：<font runat="server" id="strongRemark">0</font>元</span> <span style="padding-left: 10px;">送餐费：<font runat="server" id="strongsendcount">0</font>元</span>
                            </div>
                        </div>
                        <div>
                            <div class="dish_type com_title" style="border: 1px solid #eeeeee;">
                                <span class="f_right more"></span>
                                <p>
                                    <strong>分类：</strong> <a href="javascript:showsort(0);" class="smooth" sortid="0"
                                        id="j_s_0">全部</a>
                                    <asp:Repeater runat="server" ID="rptFoodSortList">
                                        <ItemTemplate>
                                            <a href="javascript:showsort(<%# Eval("SortID") %>);" class="smooth" sortid="<%# Eval("SortID") %>"
                                                id="j_s_<%# Eval("SortID") %>">
                                                <%# Eval("sortname") %></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </p>
                            </div>
                            <div id="divfood" style="margin-bottom: 5px;">
                                <input type="text" maxlength="10" class="j_text" name="keyword" autocomplete="off"
                                    class="text" value="请输入餐品" id="keyword" onfocus="if(value=='请输入餐品') {value=''}"
                                    onblur="if (value=='') {value='请输入餐品'}" style="float: left; margin-top: 5px;" onkeydown="return enterIn(event,searchfoodname)" />
                                <input type="button" value="搜索" onclick="searchfoodname();" class="clear_box_btn" style="float: left; margin-left: 10px;" />
                            </div>
                            <div class="dish_type_unit" id="searchfood">
                                <asp:Repeater runat="server" ID="rptFoodSort" OnItemDataBound="rptProductSort_ItemCommand">
                                    <ItemTemplate>
                                        <div class="sortitem_<%#Eval("SortId")%> sortitem">
                                            <div class="disy_type_title ">
                                                <span class="more"><a href="#j_s_0">回到顶部</a></span>
                                                <%# Eval("sortname") %>
                                            </div>
                                            <ul class="hdish_ul">
                                                <asp:Repeater runat="server" ID="rptFoodList">
                                                    <ItemTemplate>

                                                        <li id="foodid_<%#Eval("unid") %>" style="cursor: pointer; padding-left: 40px;" class="food_items" cname="<%#Eval("foodName")%><%#Eval("FoodNamePy")%>" onclick="showdiv(<%# Eval("unid") %>, '<%# Eval("foodName") %>','份', '<%# Eval("FPrice") %>',<%# Eval("isspecial") %>,<%# Eval("isauth") %>);">
                                                            <span class="shopFhandle">+</span>


                                                            <span class="py" id="py_<%#Eval("unid") %>" style="display: none"><a
                                                                href="javascript:">订购</a></span><span class="price">￥<%#Eval("FPrice")%><%# Convert.ToInt32(Eval("IsSpecial")) <= 1 ? "" :"<strong  style='color:red'>+</strong>"  %></span><span title="<%#Eval("foodName") %>">
                                                                    <%# WebUtility.Left( Eval("foodName") ,12)%></span> </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <uc4:stylebanner runat="server" ID="mycart1" />

    </form>
</body>
</html>



<script src="/javascript/float.js"></script>

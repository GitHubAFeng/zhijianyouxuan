<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myAddresslist.aspx.cs" Inherits="Html5.myAddresslist" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>
        <%= SectionProxyData.GetSetValue(2) %></title>

    <link type="text/css" rel="stylesheet" href="css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1" />

    <script src="javascript/jquery.js"></script>

</head>
<body>
    <input id="hfret" runat="server" type="hidden" value="0" />
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" id="back" runat="server" class=" top_left" data-ajax="false"></a>
            <h1>地址列表</h1>
            <a href="" id="newadd" class="reg top_right" runat="server" data-ajax="false">新增地址</a>
        </div>
        <div class="container ">
            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rtpAddressList">
                    <ItemTemplate>
                        <li>
                            <div class="order-info">
                                <span class="saddress" add-id="<%# Eval("dataid")%>" add-uid="<%# Eval("UserID")%>" data-lat="<%# Eval("lat")%>" data-lng="<%# Eval("lng")%>">
                                    <span class="ico-setok" <%#Convert.ToInt32(Eval("Pri")) ==1 ? "":"style=display:none" %>></span>
                                    <p class="f14">
                                        收货地址：
                                    <span class="orange " style="cursor: pointer"><%# Eval("Address")%><%# Eval("Phone")%></span>
                                    </p>
                                    <p class="grey">收货人：<%# Eval("Receiver")%> ，联系电话：<%# Eval("Mobilephone")%></p>
                                </span>
                                <a href="myAddressdetail.aspx?returnurl=<% = Server.UrlEncode(Request["returnurl"])%>&id=<%# Eval("dataid")%>" class="ico_right"></a>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="con-btn" id="pages" runat="server">
        </div>
    </div>
</body>
</html>
<script src="javascript/jCommon.js?v=1" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {
        $(".saddress").click(function () {
            //var rat = $("#hfret").val();
            //$(this).parent().attr("href", "");
            var aid = $(this).attr("add-id");
            var uid = $(this).attr("add-uid");

            var lat = $(this).attr("data-lat");
            var lng = $(this).attr("data-lng");

            //if (rat == "1") {
            //    window.location.href = "myAddressdetail.aspx?id=" + aid;
            //    return;
            //}

            jQuery.ajax(
            {
                type: "post",
                url: "ajaxHandler.ashx",
                data: "method=changeadd&aid=" + aid + "&uid=" + uid,
                success: function (msg) {
                    if (msg == "0") {
                        alert("修改失败！");
                    }
                    else {

                        handlecookie("mylat", lat, { expires: 30, path: "/", secure: false });
                        handlecookie("mylng", lng, { expires: 30, path: "/", secure: false });

                        window.location.href = unescape(request("returnurl"));
                    }
                }
            })
        })
    })
</script>

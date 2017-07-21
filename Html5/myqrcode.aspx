<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myqrcode.aspx.cs" Inherits="Html5.mycommission" %>

<%@ Register Src="~/distributorfooter.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <title>我的二维码</title>


    <script src="javascript/jquery.js"></script>

    <link href="css/sweetalert.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f0423b;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <input type="hidden" value="0" id="isdistributor" runat="server" />
        <input type="hidden" value="" id="qrcodeurl" runat="server" />

        <div class="ad_notice" style="display: none;">
            提现30即送“一键转发”，提现20送“置顶”！
        </div>
        <div class="warp">
            <div class="banner_content" style="padding-bottom:100px;">
                <img src="images/bg_pic.png?v=1" />
                <div class="per_msg">
                    <img src="images/im.jpg" />


                     


                    <ul>
                        <li>我是<span runat="server" id="username"></span></li>
                        <li><%= SectionProxyData.GetSetValue(2) %>圆您创业梦！</li>
                    </ul>
                </div>
                <center class=""  style="margin-top:50px;">
                   <div style="margin-bottom:20px; color:white; font-weight:bold;">我的赚钱码</div>
                    <img src="" id="myrq" runat="server" style="width:200px;height:200px;" />
                   
                    
                </center>
            </div>
        </div>
    </form>
    <uc2:Foot runat="server" ID="footer" />
</body>
</html>

<script src="javascript/sweetalert.min.js"></script>



<script type="text/javascript">
    $(function () {

        $("#food_menu_4").addClass("current_page_item");
        //提交订单post信息
        var err = $("#isdistributor").val();
        if (err != "1") {

            swal(
                {
                    title: "温馨提醒",
                    text: "你还不是分销商，点击OK成为分销商",
                    type: "warning",
                    closeOnConfirm: false
                },
                function () {
                    window.location = "becomedistributor.aspx";
                });

        }
    })

</script>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addshopcard.aspx.cs" Inherits="Html5.addshopcard" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <script src="javascript/jquery.js"></script>

    <style type="text/css">
        .my_order_list li .order-tit:last-child {
            border-bottom: none;
        }
    </style>

</head>
<body>

    <div class="page">
        <div id="page_title">
            <a href="myshopcard.aspx" id="back" data-ajax="false" class=" top_left"></a>
            <h1>绑定优惠券</h1>

        </div>
         <form method="get" action="/ajaxHandler.ashx?method=userAddShopCard&hfuserid=<%=userid %>" id="addform">

            <div class="container">
                <ul class="my_order_list">
                    <li>


                        <div class="order-tit" style="height: 50px;">
                            <span class="time"><strong>优惠券</strong>
                                <input name="tbcardckey" type="text" id="tbcardckey" class="w_txt" placeholder="请输入优惠券券号" />

                                <p class="red">券号格式为：AAAA-AAAA-AAAA</p>


                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>验证码</strong>
                                <input name="tbvcode" type="text" id="tbvcode" class="w_txt" style="width:100px" placeholder="请输入验证码" />


                                <img src="VCode.aspx" onclick="this.src = 'VCode.aspx?t='+new Date().getTime();"  id="codepic"
                                    alt="点击换一张" style="padding-left: 10px; cursor: pointer; vertical-align: bottom;" />

                            </span>
                        </div>


                    </li>
                </ul>

                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>
                <div class="view_back_con" id="div1" runat="server">
                    <input type="submit" id="btsubmit" value="确认" class="view_back_btn" onclick="return checkuserInfo()" data-ajax="false" />
                </div>
            </div>

        </form>

    </div>



</body>
</html>


<script src="javascript/spin.min.js"></script>
<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/jCommon.js?v=1"></script>
<script src="javascript/jquery.form.js"></script>


<script type="text/javascript">
    function checkuserInfo() {


        var tbcardckey = $("#tbcardckey").val() + "";
        if (tbcardckey != "") {
            var myreg = /^[0-9A-Z]{4}-[0-9A-Z]{4}-[0-9A-Z]{4}$/;
            if (!myreg.test(tbcardckey)) {

                sweetAlert("", "优惠券券号格式错误，请重新输入", "warning");
                return false;
            }
        }
        else {
            sweetAlert("", "请输入优惠券券号", "warning");
            return false;
        }

        var tbvcode = $("#tbvcode").val();
        if ($.trim(tbvcode) == "") {
            sweetAlert("", "请输入验证码", "warning");
            return false;
        }

        Loader.show("#btsubmit");


        var options = {
            success: function (data) {

                Loader.hide();

                var json = eval("(" + data + ")");

                if (json.state == "1") {
                    swal({
                        title: "温馨提示",
                        text: "绑定成功，点击查看",
                        type: "success"
                    },
                            function () {
                                var returnurl = request("returnurl");
                                if (returnurl.length == 0) {
                                    window.location = "myshopcard.aspx";
                                }
                                else
                                {
                                    window.location = decodeURIComponent(returnurl);
                                }
                              
                            });
                }
                else {

                    $("#codepic").click();

                    swal({
                        title: "温馨提示",
                        text: json.msg,
                        type: "error"
                    });


                }


            }
        };

        $("#addform").ajaxForm(options);


    }

   



</script>

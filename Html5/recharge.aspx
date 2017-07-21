<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recharge.aspx.cs" Inherits="Html5.m_recharge" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />

    <script src="javascript/jquery.js"></script>

    <style type="text/css">

        .my_order_list li .order-tit:last-child,.my_order_list li:last-child {
            border-bottom:none;
        }

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <div id="page_title">
                <a href="myinfolist.aspx" id="back" data-ajax="false" class=" top_left"></a>
                <h1>充值</h1>
                <a href="RechargeList.aspx" class="reg top_right">充值记录</a>
            </div>

            <div class="container">
                <ul class="my_order_list">
                    <li>

                        <div class="order-tit">
                            <span class="time"><strong>当前余额：</strong>
                                <label class="w_txt" id="lbusermoney" runat="server">
                                </label>
                                元
                            </span>
                        </div>


                        <div class="order-tit" style="height:auto;">



                         

                            <div class="">




                                <div style="padding-left: 0; font-weight: bold;">请点选充值金额</div>


                                <asp:Repeater runat="server" ID="rptordersouce">
                                    <ItemTemplate>

                                        <div class="order-tit order-title clearfix">
                                            <span class="time">充值 <%# Eval("classname") %>元送<%# Eval("status") %>元</span>
                                            <span id="mess_<%# Container.ItemIndex + 1%>" class="mess_ch">
                                                <label class="no-checked"></label>
                                                <input type="radio" name="ddlpaymode" value="<%# Eval("id") %>" class="pay_check" />
                                            </span>
                                        </div>




                                    </ItemTemplate>
                                </asp:Repeater>




                            </div>






                        </div>

                        <div class=" clear"></div>



                    </li>
                </ul>

                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>


                <div class="clear"></div>

                <div class="view_back_con" id="div1" runat="server">
                    <input type="submit" value="充值" class="view_back_btn"  data-ajax="false" />
                </div>
            </div>

        </div>

    </form>

</body>
</html>

<script src="javascript/CommonJs.js"></script>
<script type="text/javascript">
    function checkuserInfo() {

        var tbname = $("#tbname").val();
        if ($.trim(tbname) == "") {
            alert("请输入昵称！");
            return false;
        }

        var tbemail = $("#tbemail").val();
        if ($.trim(tbemail) != "") {
            if (!CheckEmail($.trim(tbemail))) {
                alert("邮箱格式错误！");
                return false;
            }
        }

    }

    setTimeout(function () {
        $("#divError").hide();
    }, 3000);



</script>



<script type="text/javascript">

    $(document).ready(function () {

        $("#mess_1").children("label").removeClass("no-checked").addClass("checked");
        $("#mess_1").children("input").attr("checked", "checked");

        $(".order-title").click(function () {
            $(this).find("label")
                .removeClass("no-checked")
                .addClass("checked").parent().parent().siblings()
                .find("label")
                .removeClass("checked")
                .addClass("no-checked");
            //当点击时将intput设置为选中并且将其他input设置为未选中
            $(this).find("input[name=ddlpaymode]").attr("checked", true).parent().parent().siblings().find("input[name=ddlpaymode]").attr("checked", false);

            if ($(this).find("input[name=ddlpaymode]").val() == "3") {
                $(".order-psw").show();
            }
            else {
                $(".order-psw").hide();
            }
        })

    })
</script>


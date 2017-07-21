<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashAdvance.aspx.cs" Inherits="Html5.CashAdvance" %>

<%@ Register Src="~/header.ascx" TagName="head" TagPrefix="uc3" %>
<%@ Register Src="~/distributorfooter.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <script src="javascript/jquery.js"></script>


</head>
<body>

    <uc3:head runat="server" ID="head" />
    <div class="main_title">提现到余额</div>
    <!--Start of the Tabmenu1 -->
    <div class="warp">
        <div class="menu">
            <ul>
                <li><a id="tablink1" href="javascript:" class="tabactive" title="">提现到余额</a></li>

            </ul>
        </div>


        <form method="get" action="/ajaxHandler.ashx?method=CashAdvance" id="addform">

            <input runat="server" id="hfuserid" type="hidden" value="0" />

            <!--Start Tabcontent 1 -->
            <div id="tabcontent1" style="padding-bottom: 50px;">
                <ul>
                    <li>
                        <span>佣金余额：</span><label runat="server" id="lbhavemoney"></label>元</li>
                    <li>
                        <span>提现金额：</span><input style="width: 30%;" id="tbmoney" name="tbmoney" />元
                    </li>
                </ul>
                <button type="submit" onclick="return checkdata()" id="btsubmit">确认提交</button>
            </div>
        </form>


    </div>
    <uc2:Foot runat="server" ID="footer" />

</body>
</html>
<script src="javascript/jCommon.js"></script>
<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/spin.min.js"></script>
<script src="javascript/jquery.form.js"></script>


<script type="text/javascript">

    //检查用户名称
    function checkdata() {
        var monye = $.trim(document.getElementById("tbmoney").value);
        var myreg = /^\d+(\.\d+)?$/;
        if (!myreg.test(monye)) {
            alert("提现金额格式错误。");
            return false;
        }

        var lbhavemoney = parseFloat($("#lbhavemoney").html());

        if (parseFloat(monye) > lbhavemoney) {
            alert("提现金额必须小于等于佣金余额。");
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
                        text: "佣金提现成功",
                        type: "success"
                    },
                            function () {
                                window.location = "familyhonor.aspx";
                            });
                }
                else {
                    swal({
                        title: "温馨提示",
                        text: "系统错误，请联系管理员",
                        type: "error"
                    });


                }


            }
        };


        // ajaxForm
        $("#addform").ajaxForm(options);



    }

</script>


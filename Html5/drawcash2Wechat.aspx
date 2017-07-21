<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drawcash2Wechat.aspx.cs" Inherits="Html5.drawcash2Wechat" %>


<%@ Register Src="~/header.ascx" TagName="head" TagPrefix="uc3" %>
<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/sweetalert.css" rel="stylesheet" />


    <title>申请自动提现</title>
    <script src="javascript/jquery.js"></script>

    <style type="text/css">
            .main_title ul li a {
                text-decoration: none;
                color: #fff;
            }
    </style>


</head>
<body>

    <uc3:head runat="server" ID="head" />

    <div class="main_title">
        <ul>
            <li style="border-right: #fff 1px solid;"><a href="drawcash.aspx" >申请提现</a></li>
            <li style="border-right: #fff 1px solid;" id="drawcash2Wechat_li"><a href="drawcash2Wechat.aspx" style="font-weight: bold;">申请自动提现</a></li>
            <li>余额:<label runat="server" id="lbhavemoney"></label></li>
        </ul>
    </div>

    <input type="hidden" runat="server" id="hfhavemoney" value="0" />

    <!--Start of the Tabmenu1 -->
    <div class="warp" style="padding-bottom: 50px;">
     

        <!--Start Tabcontent 1 -->

        <form method="get" action="/ajaxHandler.ashx?method=autodrawcash&hfuserid=<%=userid %>" id="tabcontent1form">

            <div id="tabcontent1" class="tabcontent1">
                <ul>
                   
                    <li>
                        <span>提现金额：</span><input id="tbmoney1" name="tbmoney1" reg="^\d+(\.\d+)?$" placeholder="提现金额必须界于1-200" tip="提现金额格式错误,请输入数字" /></li>
                </ul>

                <button type="submit" onclick="return checkdata('1')" id="btsubmit1">确认提交</button>

            </div>
        </form>



    </div>

    <uc2:Foot runat="server" ID="footer" />

</body>
</html>


<style type="text/css">
    .main_title ul li {
        width: 33%;
    }
</style>


<script src="javascript/jCommon.js?v=0612"></script>
<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/spin.min.js"></script>
<script src="javascript/jquery.form.js"></script>


<script type="text/javascript">

    //检查用户名称
    function checkdata(tab) {

        var flag = j_submitdata("tabcontent" + tab);
        if (false == flag) {
            return false;
        }
        var monye = $.trim(document.getElementById("tbmoney"+tab).value);

        var lbhavemoney = parseFloat($("#lbhavemoney").html());

        if (parseFloat(monye) > lbhavemoney) {
            alert("提现金额必须小于等于可提金额。");
            return false;
        }

        if (parseFloat(monye) < 1 || parseFloat(monye) > 200) {
            alert("提现金额必须界于1-200。");
            return false;
        }


        Loader.show("#btsubmit"+tab);


        var options = {
            success: function (data) {

                Loader.hide();

                var json = eval("(" + data + ")");

                if (json.state == "1") {
                    swal({
                        title: "温馨提示",
                        text: json.msg,
                        type: "success"
                    },
                            function () {
                                window.location = "drawcash2Wechat.aspx";
                            });
                }
                else {
                    swal({
                        title: "温馨提示",
                        text: json.msg,
                        type: "error"
                    });


                }


            }
        };

        // ajaxForm
        $("#tabcontent"+tab+"form").ajaxForm(options);
    }

    $(document).ready(function ()
    {


    })

</script>


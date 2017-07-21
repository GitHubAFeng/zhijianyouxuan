<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drawcash.aspx.cs" Inherits="Html5.drawcash" %>


<%@ Register Src="~/header.ascx" TagName="head" TagPrefix="uc3" %>
<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/sweetalert.css" rel="stylesheet" />


    <title>申请提现</title>
    <script src="javascript/easytabs.js" type="text/javascript"></script>
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
            <li style="border-right: #fff 1px solid;"><a href="drawcash.aspx" style="font-weight: bold;">申请提现</a></li>
            <li style="border-right: #fff 1px solid;" id="drawcash2Wechat_li"><a href="drawcash2Wechat.aspx">申请自动提现</a></li>
            <li>余额:<label runat="server" id="lbhavemoney"></label></li>
        </ul>
    </div>

    <input type="hidden" runat="server" id="hfhavemoney" value="0" />

    <input id="hfisopenatuodraw" type="hidden" value="<%= SectionProxyData.GetSetValue(77) %>" />

    <input type="hidden" runat="server" id="hfjsonddata" value="" />


    <!--Start of the Tabmenu1 -->
    <div class="warp" style="padding-bottom: 50px;">
        <div class="menu">
            <ul>
                <li><a href="#" onmouseover="easytabs('1', '1');" onfocus="easytabs('1', '1');" onclick="return false;" title="" id="tablink1">提现到银行卡</a></li>
                <li><a href="#" onmouseover="easytabs('1', '2');" onfocus="easytabs('1', '2');" onclick="return false;" title="" id="tablink2">提现到微信</a></li>
                <li><a href="#" onmouseover="easytabs('1', '3');" onfocus="easytabs('1', '3');" onclick="return false;" title="" id="tablink3">提现到支付宝</a></li>

            </ul>
        </div>

        <!--Start Tabcontent 1 -->

        <form method="get" action="/ajaxHandler.ashx?method=drawcash2back&hfuserid=<%=userid %>" id="tabcontent1form">

            <div id="tabcontent1" class="tabcontent1">
                <ul>
                    <li>
                        <span>银行卡开户行：</span><input id="bankname" name="bankname" reg="\S" tip="请输入银行卡开户行" /></li>
                    <li>
                        <span>银行卡卡号：</span><input id="revevar1" name="revevar1" reg="\S" tip="请输入银行卡卡号" /></li>
                    <li>
                        <span>户主姓名：</span><input id="bankusername" name="bankusername" reg="\S" tip="请输入户主姓名" /></li>
                    <li>
                        <span>提现金额：</span><input id="tbmoney1" name="tbmoney1" reg="^\d+(\.\d+)?$" tip="提现金额格式错误,请输入数字" /></li>
                </ul>

                <button type="submit" onclick="return checkdata('1')" id="btsubmit1">确认提交</button>

            </div>
        </form>


        <form method="get" action="/ajaxHandler.ashx?method=drawcash2Wechat&hfuserid=<%=userid %>" id="tabcontent2form">
            <!--Start Tabcontent 2-->
            <div id="tabcontent2" class="tabcontent2">
                <ul>
                    <li>
                        <span>微信号：</span><input id="opuser" name="opuser" reg="\S" tip="请输入微信号" /></li>
                    <li>
                        <span>提现金额：</span><input id="tbmoney2" name="tbmoney2" /></li>
                </ul>

                <button type="submit" onclick="return checkdata('2')" id="btsubmit2">确认提交</button>

            </div>
        </form>


        <form method="get" action="/ajaxHandler.ashx?method=drawcash2alipay&hfuserid=<%=userid %>" id="tabcontent3form">
            <!--Start Tabcontent 3-->
            <div id="tabcontent3" class="tabcontent3">
                <ul>
                    <li>
                        <span>支付宝账号：</span><input id="aliaccount" name="aliaccount" reg="\S" tip="请输入支付宝账号" /></li>
                    <li>
                        <span>账户姓名：</span><input id="aliname" name="aliname" reg="\S" tip="请输入账户姓名" /></li>
                    <li>
                        <span>提现金额：</span><input id="tbmoney3" name="tbmoney3" /></li>
                </ul>

                <button type="submit" onclick="return checkdata('3')" id="btsubmit3">确认提交</button>

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
        Loader.show("#btsubmit"+tab);


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
                                window.location = "drawcash.aspx";
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
        $("#tabcontent"+tab+"form").ajaxForm(options);


    }

    $(document).ready(function ()
    {
        var hfjsonddata = $("#hfjsonddata").val();
        if (hfjsonddata.length > 0) {
            var jsondata = eval("(" + hfjsonddata + ")");
            //保存信息 
            for (var key in jsondata) {
                $("#" + key).val(jsondata[key]);
            }
        }

        var hfisopenatuodraw = $("#hfisopenatuodraw").val();
        if (hfisopenatuodraw != "1") {
            $(".main_title ul li").css({ "width": "49.5%" });
            $("#drawcash2Wechat_li").hide();
        }

    })

</script>


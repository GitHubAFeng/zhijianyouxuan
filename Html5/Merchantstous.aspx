<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Merchantstous.aspx.cs" Inherits="Html5.Merchantstous" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商家加盟</title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=708" />
    <link href="css/sweetalert.css" rel="stylesheet" />


    <script src="javascript/jquery.js"></script>

    <style type="text/css">
        body {
            background-color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" method="post">
        <div class="page">

            <div id="page_title">
                <a href="TogoList.aspx" id="back" class=" top_left" data-ajax="false"></a>
                <h1>加盟申请表</h1>
            </div>
            <div class="merchantmsg postdata">
                <ul>
                    <li><span>*</span>联&ensp;系&ensp;人：<input name="tbCommPerson" id="tbCommPerson" reg="\S" tip="联系人不能为空" /></li>
                    <li><span>*</span>联系电话：<input name="tbComm" id="tbComm" reg="\d+$" tip="联系电话不能为空"  /></li>
                    <li><span>*</span>商家名称：<input name="tbName" id="tbName" reg="\S" tip="商家名称不能为空" /></li>
                    <li><span>*</span>商家地址：<input name="tbAddress" id="tbAddress" id="tbAddress" tip="商家地址不能为空"  /></li>
                    <li><span>*</span>验&ensp;证&ensp;码：<input style="width: 100px" id="tbadcode" name="tbadcode"  reg="\S" tip="请输入验证码"/><img src="VCode.aspx" id="piccode" onclick="this.src = 'VCode.aspx?t='+new Date().getTime();"
                        alt="点击换一张" style="padding-left: 10px; cursor: pointer; vertical-align: middle;" /></li>
                </ul>

                <button id="btnAjaxSubmit" type="submit">立即申请</button>




            </div>
        </div>
    </form>
</body>
</html>

<script src="javascript/jCommon.js"></script>
<script src="javascript/spin.min.js"></script>
<script src="javascript/sweetalert.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnAjaxSubmit").click(function () {

            var flag = j_submitdata('postdata');;

            if (!flag ) {
                return false;;
            }



            Loader.show("#btnAjaxSubmit");

            var options = {
                url: 'Merchantstous.aspx?action=SaveShopInfo',
                type: 'post',
                dataType: 'text',
                data: $("#form1").serialize(),
                success: function (data) {
                    Loader.hide();
                    var json = JSON.parse(data);
                    var tiptype = "";
                    if (json.state == 1) {
                        tiptype = "success";
                    }
                    else {
                        tiptype = "warning";
                    }
                    sweetAlert("", json.msg, tiptype);
                    $("#piccode").click();
                }
            };
            $.ajax(options);
            return false;
        });
    });
</script>

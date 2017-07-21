<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="qy_54tss_AreaAdmin_login"
    EnableEventValidation="false" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>登录 -
        <%= WebUtility.GetMyName() %></title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="stylesheet" type="text/css" href="/Admin/lib/bootstrap/css/bootstrap.css">
    <link rel="stylesheet" type="text/css" href="/Admin/lib/theme.css">
    <link rel="stylesheet" href="/Admin/lib/css/font-awesome.css">

    <script src="lib/jquery-1.7.2.min.js" type="text/javascript"></script>

    <!-- Demo page code -->
    <style type="text/css">
        #line-chart {
            height: 300px;
            width: 800px;
            margin: 0px auto;
            margin-top: 1em;
        }

        .brand {
            font-family: georgia, serif;
        }

            .brand .first {
                color: #ccc;
                font-style: italic;
            }

            .brand .second {
                color: #fff;
                font-weight: bold;
            }
    </style>
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
</head>
<!--[if lt IE 7 ]> <body class="ie ie6"> <![endif]-->
<!--[if IE 7 ]> <body class="ie ie7 "> <![endif]-->
<!--[if IE 8 ]> <body class="ie ie8 "> <![endif]-->
<!--[if IE 9 ]> <body class="ie ie9 "> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<body class="">
    <!--<![endif]-->
    <div class="navbar">
        <div class="navbar-inner">
            <ul class="nav pull-right">
            </ul>
            <a class="brand" href="javascript:"><span class="second">
                <%= SectionProxyData.GetSetValue(2) %>管理平台-分区域管理系统</span></a>
        </div>
    </div>
    <div class="row-fluid">
        <form id="form1" runat="server">
            <div class="dialog">
                <div class="block">
                    <p class="block-heading">
                        登录
                    </p>
                    <div class="block-body">
                        <label>
                            账号</label>
                        <input type="text" class="span12" id="tbname" runat="server">
                        <label>
                            密码</label>
                        <input id="tbpwd" runat="server" type="password" class="span12">
                        <label>
                            验证码</label>
                        <input id="tbvcode" runat="server" type="text" class="">
                        <img src="VCode.aspx" onclick="this.src = 'VCode.aspx?t='+new Date().getTime();"
                            alt="点击换一张" style="padding-left: 10px; cursor: pointer;" />
                        <div>
                            <asp:Button ID="Button1" runat="server" Text="确定" OnClick="btLogin_Click" class="btn btn-primary pull-right"
                                OnClientClick="return CheckForm()" />
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
                <p style="color: Red;" class="pull-right">
                    请使用firefox浏览器访问，<a target="blank" href="http://www.firefox.com.cn/download/">下载地址</a>
                </p>
            </div>
        </form>
    </div>

    <%--<script src="/Admin/lib/bootstrap/js/bootstrap.js"></script>--%>

</body>
</html>

<script type="text/javascript">

    function CheckForm() {
        if ($F("tbname").value == "") {
            alert("请输入帐号!");
            $F("tbname").focus();
            return false;
        }
        if ($F("tbpwd").value == "") {
            alert("请输入密码!");
            $F("tbpwd").focus();
            return false;
        }
        return true;
    }

    function $F(ele) {
        return document.getElementById(ele);
    }
</script>


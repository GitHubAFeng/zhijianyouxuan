<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myuserinfo.aspx.cs" Inherits="Html5.myuserinfo" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=<%=(new Random()).Next(0000,9999) %>" />

    <script src="javascript/jquery.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <div id="page_title">
                <a href="myinfolist.aspx" id="back" data-ajax="false" class=" top_left"></a>
                <h1>我的信息</h1>
                 <a href="updatepwd.aspx" class="reg top_right">修改密码</a>
            </div>

            <div class="container">
                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>积分</strong>
                                <label class="w_txt" id="lbpont" runat="server">
                                </label>
                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>余额</strong>
                                <label class="w_txt" id="lbmoney" runat="server">
                                </label>
                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>手机</strong>
                                <label class="w_txt" id="tbTel" runat="server">
                                </label>
                            </span>
                        </div>


                        <div class="order-tit">
                            <span class="time"><strong>昵称</strong>
                                <input name="tbname" type="text" id="tbname" class="w_txt" placeholder="请输入昵称" runat="server" />
                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>邮箱</strong>
                                <input name="tbemail" type="text" id="tbemail" class="w_txt" placeholder="请输入邮箱" runat="server" />
                            </span>
                        </div>
                        <div class="order-tit" style="">
                            <span class="time"><strong>QQ</strong>
                                <input name="tbQQ" type="text" id="tbQQ" class="w_txt" placeholder="请输入QQ" runat="server" />
                            </span>
                        </div>
                        <div class="order-tit" style="border-bottom: none;">
                            <span class="time"><strong>姓名</strong>
                                <input name="tbRealName" type="text" id="tbRealName" class="w_txt" placeholder="请输入姓名" runat="server" />
                            </span>
                        </div>

                    </li>
                </ul>

                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>
                <div class="view_back_con" id="div1" runat="server">
                    <input type="submit" value="保存" class="view_back_btn" onclick="return checkuserInfo()" data-ajax="false" />
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

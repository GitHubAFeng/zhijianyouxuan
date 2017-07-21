<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Commentshop.aspx.cs" Inherits="Html5.Commentshop" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />

    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/star-rating.css" rel="stylesheet" />

    <script src="javascript/jquery.js"></script>
    <script src="javascript/star-rating.js"></script>

    <style type="text/css">
        .rating-xl {
            line-height: 50px;
            font-size: 3.5em;
        }
    </style>
</head>
<body style="background-color: #fff;">
    <div class="page">
        <div id="page_title">
            <a href="myorderlist.aspx" class="back top_left" runat="server" id="backlink"></a>
            <h1 style="text-align: center;">点评</h1>
        </div>
        <div class="container">

            <div style="margin: 10px 0 0;">
                <input id="tbrat" value="3" type="number" class="rating" min="0" max="5" step="1" data-size="xl">
            </div>
            <form id="form1" runat="server">
                <div style="background: none; border: none; padding: 10px 0;">
                    <input type="hidden" id="tbpoint" name="tbpoint" value="3" />
                    <strong style="display: block; font-size: 14px; margin-bottom: 10px;">内容</strong>
                    <textarea class="" name="tbComment" id="tbComment" style="width: 100%; height: 100px; padding: 10px; border: 1px solid #ccc;" placeholder=""></textarea>
                </div>
                <div class="view_back_con" id="divError" runat="server" style="margin: 0;">
                    <input type="submit" value="提 交" id="btsubmit" class="view_back_btn" onclick="return checkdata()" data-ajax="false" />
                </div>
            </form>
        </div>
        <uc2:Foot runat="server" ID="footer" />
    </div>
</body>
</html>

<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/jCommon.js"></script>
<script src="javascript/spin.min.js"></script>

<script type="text/javascript">
    function checkdata() {
        var tbnum = $("#tbComment").val() + "";
        if (tbnum == "") {
            sweetAlert("", "请输入评论内容!", "error");
            return false;
        }
        Loader.show($('#btsubmit'))

        return true;
    }

</script>


<script>
    jQuery(document).ready(function () {
        $('#tbrat').rating('refresh', {
            showClear: false,
            showCaption: false,
            starCaptions: {
                1: '非常差',
                2: '比较差',
                3: '一般',
                4: '还可以',
                5: '非常好'
            }
        });
    });
</script>

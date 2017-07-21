<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showshopic.aspx.cs" Inherits="Html5.showshopic" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/home.css?v=708" />
    <link href="css/pictip.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=708" />

    <link href="css/swiper.min.css" rel="stylesheet" />

    <style type="text/css">
        body {
            background-color: #eee;
        }

        .bom_menu .icon-food {
            background-image: url(../images/ico_b_food_cur.png);
            color: #f39800;
        }
    </style>
</head>
<body id="page_allMenu">
    <input id="hfpage" runat="server" type="hidden" />
    <input id="hfcpage" runat="server" type="hidden" value="1" />


    <div id="page_title" style="position: fixed;">
        <a href="shopdetail.aspx?id=<% =Request["id"] %>" id="back" class=" top_left"></a>
        <h1 id="h1togoname" runat="server">商家图片</h1>
    </div>



    <div class="container">
        <div class="index_img">

            <div class="swiper-container swiper" style="height: 100%;" id="pptbox">
                <div class="swiper-wrapper" style="height: 100%;">
                    <asp:Repeater runat="server" ID="rptppt">
                        <ItemTemplate>
                            <div class="swiper-slide" style="height: 100%;">

                                <img src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>" style="width: 100%; height: 100%;" title="<%# Eval("title") %>">
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="swiper-pagination" style="bottom:40px;">&nbsp;</div>
            </div>
        </div>



    </div>



</body>
</html>

<script src="javascript/jquery.js" type="text/javascript"></script>

    <script src="javascript/swiper-3.3.1.jquery.min.js"></script>

<script src="javascript/jCommon.js" type="text/javascript"></script>

<script type="text/javascript">

    $(function () {

        var clientHeight = document.documentElement.clientHeight;

        $("#pptbox").css({ "height": clientHeight + "px" });



        var swiper = new Swiper('.swiper-container', {
            pagination: '.swiper-pagination',
            paginationType: 'fraction',
            paginationFractionRender: function (swiper, currentClassName, totalClassName) {
                return '<span class="' + currentClassName + '"></span>' +
                       ' of ' +
                       '<span class="' + totalClassName + '"></span>';
            }
        });



    })




</script>



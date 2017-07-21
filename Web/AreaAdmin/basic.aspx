<%@ Page Language="C#" AutoEventWireup="true" CodeFile="basic.aspx.cs" Inherits="AreaAdmin_basic" %>

<%@ Register Src="Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="ofc" Namespace="OpenFlashChart" Assembly="OpenFlashChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%= WebUtility.GetMyName()%></title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/menu.css" rel="stylesheet" type="text/css" />
    <link href="css/boxes.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="css/ie7.css" media="all" />
    <![endif]-->

    <link href="/javascript/jbox/Skins/jbox.css" rel="stylesheet" type="text/css" />

    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="javascript/Common.js" type="text/javascript"></script>


</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField runat="server" ID="hfxjson" />
        <asp:HiddenField runat="server" ID="hfyjson" />

        <asp:HiddenField runat="server" ID="hfisshowupdate" Value="0" />

        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <div class="wrapper">
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div id="messages">
                    </div>
                    <div style="visibility: visible;" class="content-header">
                        <table cellspacing="0">
                            <tbody>
                                <tr>
                                    <td>
                                       <a id="updatelingk" href="javascript:" onclick="showupdate();" style="float: right; font-size: 1.25em; line-height: 1.2em; text-decoration: none;">更新日志</a> <h3 class="head-dashboard">系统管理导航区</h3>
                                    <div class=" clear;"></div>
                                         </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="dashboard-container">
                        <p class="switcher">
                            <label for="store_switcher">
                                系统管理导航区</label>
                        </p>
                        <table cellspacing="25" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 290px">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4>客服操作区</h4>
                                            </div>
                                            <fieldset class="a-center bold">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <button type="button" class="scalable back" onclick="setLocation('shop/OrderList.aspx')"
                                                                style="">
                                                                <span>订单列表</span></button>
                                                        </td>
                                                        <td>
                                                             <button type="button" class="scalable" onclick="setLocation('Shop/ShopDetail.aspx')"
                                                                style="">
                                                                <span>新增商家</span></button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </div>

                                  

                                    </td>
                                    <td>

                                        <div class="entry-edit" style="border: 1px solid rgb(204, 204, 204);">
                                            <ul id="diagram_tab" class="tabs-horiz">
                                                <li><a href="javascript:" id="diagram_tab_orders" title="点餐" class="tab-item-link active"><span>
                                                    <span class="changed"></span><span class="error"></span>今日订单</span> </a></li>
                                            </ul>
                                            <div id="diagram_tab_content">
                                                <div class="hor-scroll" id="divDay">

                                                    <div id="cancaschars" style="height: 400px;"></div>

                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <!--center end-->
            </div>
            <uc2:Foot runat="server" ID="FootUC" />
        </div>
    </form>
</body>
</html>
<script src="/Admin/javascript/echarts/echarts-all.js" type="text/javascript"></script>
<script src="/Admin/javascript/echarts/echartsApp.js" type="text/javascript"></script>
<script src="/javascript/jbox/jquery.jBox-2.3.min.js" type="text/javascript"></script>


<script language="javascript" type="text/javascript">
    $(window).load(function () { $("#loading-mask").hide(); })

    $(document).ready(function () {

        var sortid = Request("msg");
        if (sortid != "") {
            alert("您没有访问权限");
        }

        ordertimeshow();
        var hasshow = handlecookie("hasshow"); //判断是否显示过，如果显示过了，就不显示了
        var hfisshowupdate = $("#hfisshowupdate").val();
        if (hfisshowupdate == "1") {
            if (hasshow == null || hasshow == "") {
                showupdate();
                handlecookie("hasshow", "1", { expires: 1, path: "/", secure: false });
            }
        }
        else {
            $("#updatelingk").hide();
        }

    });

    //显示更新日志
    function showupdate()
    {
        $.jBox.open("iframe:updatelog.aspx", "更新日志", 800, 480, { buttons: { '关闭': true } });
    }
</script>

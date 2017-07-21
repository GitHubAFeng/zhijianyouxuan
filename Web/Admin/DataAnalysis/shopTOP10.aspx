<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopTOP10.aspx.cs" Inherits="Admin_Sale_shopTOP10" %>

<%@ Register TagPrefix="ofc" Namespace="OpenFlashChart" Assembly="OpenFlashChart" %>
<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家销量TOP10-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/echarts/echarts-all.js" type="text/javascript"></script>
    <script src="../javascript/echarts/echartsApp.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function () {
            $("#loading-mask").hide();
        });

        $(document).ready(function () {
            ShopTop10();

            var ordersort = request("on");;
            if (ordersort == "") {
                ordersort = "ordercount";
            }
            $("#sort_" + ordersort).addClass("active");

        })

    </script>

</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField runat="server" ID="hfxjson" />
        <asp:HiddenField runat="server" ID="hfyjson" />

        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <!--banner start-->
            <uc1:TogoBanner runat="server" ID="Banner" />
            <!--banner end-->
            <!--center start-->
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <div class="side-col" id="page:left">
                            <uc3:left runat="server" ID="left" />
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <div class="main-col" id="content">
                            <!--start-->
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>

                                 <ul class="tabs-horiz" runat="server" id="divop">
                                    <li><a href="shopTOP10.aspx?on=ordercount" id="sort_ordercount" class="tab-item-link ">
                                        <span><span class="changed"></span><span class="error"></span>按订单量</span> </a>
                                    </li>

                                    <li><a href="shopTOP10.aspx?on=TotalPrice" id="sort_TotalPrice" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>按营业额</span> </a></li>


                                    <li><a href="shopTOP10.aspx?on=Shopprofit" id="sort_Shopprofit" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>按利润</span> </a></li>

                                </ul>

                                <fieldset class="AdminSearchform">
                                    <legend>查询条件 </legend>

                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left;"
                                        class="condition_table">

                                        <tr>
                                            <td align="right" class="tab_label">
                                                <span>时间：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" CssClass="j_text" ID="tbStartTime" Width="140px" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                至
                                                    <asp:TextBox runat="server" ID="tbEndTime" CssClass="j_text" Width="140px" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>


                                            </td>
                                        </tr>





                                        <tr>
                                            <td align="right" class="tab_label"></td>
                                            <td>
                                                <asp:Button runat="server" ID="Button1" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                <asp:Button runat="server" ID="btyestoday" class="form-button" Text="昨天" OnClick="settime_Click" />
                                                <asp:Button runat="server" ID="tbtoday" class="form-button" Text="今天" OnClick="settime_Click" />
                                                <asp:Button runat="server" ID="btpre" class="form-button" Text="前一天" OnClick="settime_Click" />
                                                <asp:Button runat="server" ID="btnext" class="form-button" Text="后一天" OnClick="settime_Click" />
                                                <asp:Button runat="server" ID="tbweek" class="form-button" Text="本周" OnClick="settime_Click" />
                                                <asp:Button runat="server" ID="tbmonth" class="form-button" Text="本月" OnClick="settime_Click" />
                                                <asp:Button runat="server" ID="tbyear" class="form-button" Text="本年" OnClick="settime_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="clear">
                                    </div>

                                </fieldset>

                               


                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <!--CountOrderData.aspx?type=hour-->
                                        <div class="entry-edit">

                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-billing-address">
                                                    <asp:Label runat="server" ID="lbTitle"></asp:Label></h4>
                                            </div>
                                            <fieldset class="np">
                                                <div class="order-address">
                                                    <div class="content">
                                                        <div class="hor-scroll" id="divDay">

                                                            <div id="cancaschars" style="height: 400px;"></div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <!--end-->
                        </div>
                    </div>
                </div>
            </div>
            <uc2:Foot runat="server" ID="FootUC" />

        </div>
    </form>
</body>
</html>

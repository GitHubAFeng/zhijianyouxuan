<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printMonitor.aspx.cs" Inherits="qy_54tss_Admin_Sale_printMonitor" Async="true" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单打印监控-
        <%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        AddLoadFun(init);

        var flushtimer = null;

        var Table;
        function init() {
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        $(document).ready(function () {
            delayURL();
        });

        function delayURL() {
            var delay = $("#time").html();
            if (delay > 0) {
                delay--;
                $("#time").html(delay);
            }
            else {
                window.location = window.location.href;
            }
            flushtimer = setTimeout(delayURL, 1000);
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfflag" runat="server" />
        <asp:HiddenField ID="hdDels" runat="server" />
        <asp:HiddenField runat="server" ID="hfstate" Value="-1" />
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <div class="side-col" id="page:left">
                            <uc3:left runat="server" ID="adleft" />
                        </div>
                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="divMessages" runat="server" style="color: red; font-weight: bold;">
                                </div>
                                <fieldset class="AdminSearchform">
                                    <legend>订单打印监控-请保证这个界面一直打开</legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left;"
                                        class="condition_table">
                                        <tr>
                                            <td align="right" class="tab_label">
                                                <div class="orderhistory-tishi">
                                                    <span style="">


                                                        <span style="margin-left: 10px;">
                                                            <label id="time" style="color: Red">
                                                                30</label>秒后自动刷新 </span></span><span style="margin-left: 20px;"></span>



                                                </div>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>
                                                <div runat="server" id="automsg" class=" notice"></div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="clear">
                                    </div>
                                </fieldset>

                            </div>
                        </div>
                    </div>
                </div>
                <uc2:Foot runat="server" ID="FootUC" />
            </div>
        </div>
    </form>
</body>
</html>

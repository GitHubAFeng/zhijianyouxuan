<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserWorkOnceOrderStatis.aspx.cs" Inherits="Admin_User_UserWorkOnceOrderStatis" %>


<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register TagPrefix="uc3" Src="~/Admin/Adleft.ascx" TagName="left" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册以来（7天前注册）只定过一次的用户-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/style.css" rel="stylesheet" type="text/css" />--%>
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });
        $(document).ready(function () { init(); });

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidDels" runat="server" />
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
        <div class="wrapper">
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <uc3:left ID="UserDelMoneyLog1" runat="server" />
                        &nbsp;<div class="main-col" id="content">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="main-col-inner">
                                        <div id="divMessages">
                                        </div>
                                        <div id="sales_order_grid_massaction" style="clear: both;">
                                            <table class="massaction" cellpadding="0" cellspacing="0">
                                                <tbody>
                                                    <tr>
                                                        <td>注册以来（7天前注册）只定过一次的用户
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="15%" />
                                                    <col width="15%" />
                                                    <col width="15%" />
                                                    <col />
                                                    <thead>
                                                        <thead>
                                                            <tr class="headings">
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                        class="sort-title">编号</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                        class="sort-title">用户名称</span></a></span>
                                                                </th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody class="grid_data">
                                                            <asp:Repeater ID="rptUserWorkOnceOrderStatis" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class="pointer" title="">

                                                                        <td class="">
                                                                            <%# Eval("DataID")%>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("Name")%>
                                                                        </td>

                                                                        <td class=""></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                </table>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <uc2:Foot runat="server" ID="FootUC" />
        </div>
    </form>
</body>
</html>

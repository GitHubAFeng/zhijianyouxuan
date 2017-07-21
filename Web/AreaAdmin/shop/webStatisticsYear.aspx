<%@ Page Language="C#" AutoEventWireup="true" CodeFile="webStatisticsYear.aspx.cs"
    Inherits="AreaAdmin_Sale_webStatisticsYear" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单统计－<%= WebUtility.GetMyName()%></title>
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="../javascript/Building.js" type="text/javascript"></script>

    <script src="../javascript/Foodautocompelete.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function() { $("#loading-mask").hide(); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }

        //给一个输入框赋值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }
        
        

    </script>

    <style type="text/css">
        .summary
        {
            border: 1px solid #ccc;
        }
        .summary th
        {
            color: #333333;
            text-align: center;
            border: 1px solid #ccc;
        }
        .summary td
        {
            color: #333333;
            text-align: center;
            border: 1px solid #ccc;
            height: 25px;
            line-height: 25px;
        }
        .summary .tableft
        {
            padding-left: 3px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
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
                    <div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>
                                    <div style="visibility: visible;" class="content-header">
                                        <h3 class="icon-head head-customer">
                                            <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                    </div>
                                    <!--start-->
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address" runat="server" id="h4title">
                                                        网站订单统计
                                                    </h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="order-billing_address_fields">
                                                        <div class="content">
                                                            <div class="hor-scroll">
                                                                <table class="form-list" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountprefix">
                                                                                    年份<span class="required"></span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList runat="server" ID="ddlyear" Width="60">
                                                                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                                                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                                                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                                                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                                    <asp:ListItem Value="2011">2018</asp:ListItem>
                                                                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="display: none">
                                                                            <td class="label">
                                                                                <label for="_accountprefix">
                                                                                    月份<span class="required"></span></label>
                                                                            </td>
                                                                            <td class="value" style="width: 600px;">
                                                                                <asp:DropDownList runat="server" ID="ddlmounth" Width="70">
                                                                                    <asp:ListItem Value="0">选择月份</asp:ListItem>
                                                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="mynotice">
                                                                                    注：不选择月份表示统计全年</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    订单数 <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <label runat="server" id="lborder">
                                                                                    0</label>单
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    交易额 <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <label runat="server" id="lbcount">
                                                                                    0</label>元
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:Button ID="Button2" runat="server" CssClass="button_1" Text="统计" OnClick="search_click">
                                                                                </asp:Button>
                                                                                <asp:Button ID="Button1" runat="server" CssClass="button_1" Text="导出" OnClick="btExport_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <span runat="server" id="lbnotice" style="margin-top: 10px; margin-bottom: 5px; color: Red;
                                                                    font-weight: bold;">点击月份查看当月每日财务</span>
                                                                <table width="100%" class="summary">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th style="width: 15%">
                                                                                <label runat="server" id="lbmytype">
                                                                                    月份</label>
                                                                            </th>
                                                                            <th>
                                                                                图例
                                                                            </th>
                                                                            <th style="width: 20%">
                                                                                付款(元)
                                                                            </th>
                                                                            <th style="width: 10%">
                                                                                单数(单)
                                                                            </th>
                                                                            <th style="width:10%">
                                                                                菜品总价
                                                                            </th>
                                                                             <th style="width:10%">
                                                                                饮品总价
                                                                             </th>
                                                                             <th style="width:10%">
                                                                                送餐费
                                                                             </th>
                                                                        </tr>
                                                                        <asp:Repeater runat="server" ID="rptsum" OnItemCommand="getdays">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("CountKey")%>' runat="server"
                                                                                            ID="del"><%# Eval("CountKey")%></asp:LinkButton>
                                                                                    </td>
                                                                                    <td class="tableft">
                                                                                        <div style="width: 80%; height: 20px; float: left;">
                                                                                            <div style="width: <%# Eval("rat")%>%; background: #0863C0; float: left;">
                                                                                                &nbsp;</div>
                                                                                            <label style="margin-left: 4px;">
                                                                                                <%# Eval("CountDecimalValue")%>元</label>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("CountDecimalValue")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("CountIntValue")%>
                                                                                    </td>
                                                                                    <td>
                                                                                       <%# Convert.ToDecimal(Eval("CountDecimalValue")) - Convert.ToDecimal(Eval("CountDrinkPrice")) - Convert.ToDecimal(Eval("CountSendFee"))%>
                                                                                    </td>
                                                                                    <td>
                                                                                       <%# Eval("CountDrinkPrice")%>
                                                                                    </td>
                                                                                    <td>
                                                                                       <%# Eval("CountSendFee")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end-->
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <!--foot start-->
        <uc2:Foot runat="server" ID="FootUC" />
        <!--foot end-->
    </div>
    </form>
</body>
</html>

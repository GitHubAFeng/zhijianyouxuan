<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="AreaAdmin_shop_OrderDetail" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单信息管理-<%= WebUtility.GetMyName()%></title>
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
    <link href="../../css/print.css" rel="stylesheet" />


    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="../javascript/order.js" type="text/javascript"></script>
        <script src="/Admin/javascript/jquery.PrintArea.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function () {
            $("#loading-mask").hide(); showContent(3);
        });

        function init() {

        }
        function reset() {
            $("#tbName").html();
            $("#tbLinkUrl").html();
            $("#tbOrder").html();
        }
        function SetTimeS(value) {
            $("#tbTime1Start").val(value);
        }

        function SetTimeE(value) {
            $("#tbTime1End").val(value);
        }
        //给一个输入框赋值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }
        $(document).ready(function () { WhitchActive(4) });

        function printorder() {
            $('#print_area').html($('#printdata').html());
            $("#print_area").printArea();

        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
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
                            <uc4:togoleft ID="togoleft1" runat="server" />
                        </div>
                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>
                                <div style="visibility: visible;" class="content-header">
                                    <h3 class="icon-head head-customer">
                                        <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                    <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                        <p style="" class="content-buttons form-buttons">
                                            <asp:Button ID="Button2" runat="server" CssClass="button_1" OnClientClick="history.go(-1);return false;"
                                                Text="返回"></asp:Button>

                                            <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="printorder();return false;"
                                                Text="小票打印" ></asp:Button>

                                             <a href="/images/printset.jpg" target="_blank">打印设置</a>


                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                Text="保存"></asp:Button>
                                        </p>
                                    </div>
                                </div>
                                <style type="text/css">
                                    .form-list td.label {
                                        width: 80px;
                                    }
                                </style>
                                <div id="order-addresses">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <%--class="box-left"--%>
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">订单基本信息</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="order-billing_address_fields">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <fieldset>
                                                                        <legend runat="server" id="lbstatus">订单操作</legend>
                                                                        <table class="form-list" cellspacing="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="label" style="width: 80px;">订单状态
                                                                                    </td>
                                                                                    <td class="value" style="width: 500px;">
                                                                                        <asp:DropDownList runat="server" ID="ddlOrderState" Style="width: 80px;">
                                                                                        </asp:DropDownList>
                                                                                        <div class="mynotice">
                                                                                            状态修改成“已经调度”后相应配送点可看到订单
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            配送状态<span class="required"></span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" Enabled="false" ID="tbsendstate" CanBeNull="必填" Width="80px"
                                                                                            class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            商户接收<span class="required"></span></label>
                                                                                    </td>
                                                                                    <td class="value">

                                                                                        <asp:DropDownList runat="server" ID="ddlIsShopSet" class="j_select" Width="100" Enabled="false">

                                                                                            <asp:ListItem Text="未接收" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="已接收" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="拒绝" Value="2"></asp:ListItem>
                                                                                        </asp:DropDownList>

                                                                                        <label runat="server" id="lbshopnotice" style="color: red;"></label>

                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>

                                                                            </tbody>
                                                                        </table>
                                                                    </fieldset>
                                                                    <fieldset>
                                                                        <legend>收单点信息</legend>
                                                                        <table class="form-list" cellspacing="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="label" style="width: 80px;">
                                                                                        <label for="_accountwebsite_id">
                                                                                            订单编号 <span class="required">*</span></label>
                                                                                        <asp:HiddenField runat="server" ID="hidDataId" />
                                                                                        <asp:HiddenField runat="server" ID="hidUserId" />
                                                                                        <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" Enabled="false" ID="tbOrderId" CanBeNull="必填" Width="200px"
                                                                                            class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            商家名称<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" Enabled="false" ID="tbTogoName" CanBeNull="必填" Width="200px"
                                                                                            class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            联系人<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" Enabled="false" ID="tbUserName" CanBeNull="必填" Width="200px"
                                                                                            class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            手机<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" Enabled="false" ID="tbTel" Width="200px" class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            地址<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox TextMode="MultiLine" Cols="2" Height="40px" runat="server" ID="tbAddress"
                                                                                            CanBeNull="必填" Width="200px" class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountpassword">
                                                                                            备注
                                                                                        </label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbremark" class="required-entry input-text" Width="200px"
                                                                                            TextMode="MultiLine" Height="40px"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </fieldset>
                                                                    <fieldset>
                                                                        <legend>客户信息 </legend>
                                                                        <table class="form-list" cellspacing="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            用户编号<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" Enabled="false" ID="tbUserId" CanBeNull="必填" Width="200px"
                                                                                            class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            用户名<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" Enabled="false" ReadOnly="true" ID="tbCustomerName" Width="200px"
                                                                                            class="required-entry input-text"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </fieldset>
                                                                    <fieldset>
                                                                        <legend>订单信息</legend>
                                                                        <table class="form-list" cellspacing="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            就餐方式<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <asp:DropDownList runat="server" ID="ddleatytpe" Style="width: 80px;" Enabled="false">
                                                                                            <asp:ListItem Text="外卖" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="堂食" Value="1"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="display: none">
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            就餐人数<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbReveInt1" Enabled="false" Text="0" class="required-entry input-text"
                                                                                            Width="60px"></epc:TextBox>人
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            订单来源<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <asp:DropDownList ID="ddlsource" runat="server" Style="width: 120px;" Enabled="false"
                                                                                            CssClass="j_seclect">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tradminname" runat="server">
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            代客下单管理员<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbadminname" Enabled="false" class="required-entry input-text"
                                                                                            Width="200px"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            商品原价<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tboldprice" Enabled="false" CanBeNull="必填" Width="60px"
                                                                                            class="required-entry input-text" Text=""></epc:TextBox>元
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            支付商家<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbshopdiscountmoney" Enabled="false" CanBeNull="必填" Width="60px"
                                                                                            class="required-entry input-text" Text=""></epc:TextBox>元
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            优惠券支付<span class="required"></span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbcardpay" Enabled="false" CanBeNull="必填" Width="60px"
                                                                                            class="required-entry input-text" Text=""></epc:TextBox>元
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                 <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            促销优惠<span class="required"></span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbpromotion" Enabled="false" CanBeNull="必填" Width="60px"
                                                                                            class="required-entry input-text" Text=""></epc:TextBox>元
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            订单总金额<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="TextBoxto" Enabled="false" CanBeNull="必填" Width="60px" class="required-entry input-text"
                                                                                            Text=""></epc:TextBox>
                                                                                        元
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            菜总价<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbTotalPrice" Enabled="false" CanBeNull="必填" Width="60px"
                                                                                            class="required-entry input-text" Text=""></epc:TextBox>元
                                                                                   
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="display: none;">
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            折扣<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <label runat="server" id="lbdiscount">
                                                                                        </label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            送餐费<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tbsendfree" CanBeNull="必填" Width="60px" class="required-entry input-text"
                                                                                            Text=""></epc:TextBox>元
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            订单时间<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="tborderTime" Enabled="false" class="required-entry input-text"
                                                                                            Width="200px"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            支付方式<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <asp:DropDownList ID="ddlPaymode" runat="server" Style="width: 120px;" Enabled="false"
                                                                                            CssClass="j_seclect">
                                                                                            <asp:ListItem Value="3"> 余额支付</asp:ListItem>
                                                                                            <asp:ListItem Value="4"> 货到付款</asp:ListItem>
                                                                                             <asp:ListItem Value="1">支付宝</asp:ListItem>
                                                                                            <asp:ListItem Value="5">微信支付</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                 <tr  runat="server" id="paysttetr">
                                                                                    <td class="label">
                                                                                        <label for="_accountprefix">
                                                                                            支付状态</label>
                                                                                    </td>
                                                                                    <td class="value" style="width: 300px">
                                                                                        <asp:DropDownList runat="server" ID="ddlPaystate" Style="width: 80px;">
                                                                                            <asp:ListItem Text="未支付" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="支付成功" Value="1"></asp:ListItem>

                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr >
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            支付金额<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="lblpaymoney" Enabled="false" class="required-entry input-text"
                                                                                            Width="120px"></epc:TextBox>元
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            送餐时间<span class="required">*</span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <epc:TextBox runat="server" ID="Textstatr" Enabled="false" class="required-entry input-text"
                                                                                            Width="200px"></epc:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label">
                                                                                        <label for="_accountwebsite_id">
                                                                                            催单信息<span class="required"></span></label>
                                                                                    </td>
                                                                                    <td class="value">
                                                                                        <label runat="server" id="lbcallmsg">
                                                                                        </label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <small>&nbsp;</small>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </fieldset>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div>
                                                <%--class="box-right"--%>
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head fieldset-legend head-billing-address">订单中的餐品信息</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="">
                                                            <div class="grid">
                                                                <div class="hor-scroll">
                                                                    <table class="data" cellspacing="0" id="grid_table">
                                                                        <colgroup>
                                                                            <col width="60%" />
                                                                            <col width="10%" />
                                                                            <col width="10%" />
                                                                            <thead>
                                                                                <tr class="headings">
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">餐品名称</span></a></span>
                                                                                    </th>
                                                                                    <%--  <th>
                                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">备注</span></a></span>
                                                                                </th>--%>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">价格</span></a></span>
                                                                                    </th>
                                                                                      <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">加价</span></a></span>
                                                                                    </th>
                                                                                    <th class="no-link last">
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">数量</span></a></span>
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody class="grid_data">
                                                                                <asp:Repeater ID="rptFoodlist" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <tr class="pointer" title="">
                                                                                            <td class="">
                                                                                                <%#Eval("FoodName")%>
                                                                                            </td>
                                                                                            <%--  <td class="">
                                                                                            <%#Eval("remark")%>&nbsp;
                                                                                        </td>--%>
                                                                                            <td class="">￥<%# Convert.ToDecimal(Eval("FoodPrice"))%>
                                                                                            </td>
                                                                                             <td class="">￥<%# Convert.ToDecimal(Eval("addPrice"))%>
                                                                                            </td>
                                                                                            <td class="last">
                                                                                                <%#Eval("FCounts").ToString()%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </colgroup>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <div class="order-save-in-address-book">
                                                                <label for="order-billing_address_save_in_address_book">
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head fieldset-legend head-billing-address">该用户最近订单</h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="Div1">
                                                        <div class="grid">
                                                            <div class="grid">
                                                                <div class="hor-scroll">
                                                                    <table class="data" cellspacing="0">
                                                                        <colgroup>
                                                                            <col width="50" />
                                                                            <col width="60" />
                                                                            <col width="120" />
                                                                            <col width="160" />
                                                                            <col width="80" />
                                                                            <col width="40" />
                                                                            <col />
                                                                            <col width="90" />
                                                                            <col width="110" />
                                                                            <thead>
                                                                                <tr class="headings">
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">编号</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">用户名</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">商家</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#" name="created_at">
                                                                                            <!--sort-arrow-desc-->
                                                                                            <span class="sort-title">订单时间</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">收件人</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">金额</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">地址</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">状态</span></a></span>
                                                                                    </th>
                                                                                    <th>
                                                                                        <span class="nobr">操作</span>
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody class="grid_data">
                                                                                <asp:Repeater ID="rtpOrderlist" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <tr class="pointer" title="">
                                                                                            <asp:HiddenField ID="hidDataId" runat="server" Value='<%# Eval("Unid")%>' />
                                                                                            <asp:HiddenField ID="hidOrderId" runat="server" Value='<%# Eval("Unid")%>' />
                                                                                            <td class="">
                                                                                                <a href='OrderDetail.aspx?id=<%# Eval("Unid")%>'>
                                                                                                    <%#Eval("orderid")%></a>
                                                                                            </td>
                                                                                            <td class="">
                                                                                                <%#Eval("CustomerName")%>
                                                                                            </td>
                                                                                            <td class="">
                                                                                                <%#Eval("TogoName") %>
                                                                                            </td>
                                                                                            <td class="">
                                                                                                <%#Eval("OrderDateTime").ToString()%>
                                                                                            </td>
                                                                                            <td class="">
                                                                                                <%#Eval("OrderRcver") %>
                                                                                            </td>
                                                                                            <td class="">
                                                                                                <%#Eval("OrderSums")%>
                                                                                            </td>
                                                                                            <td class="">
                                                                                                <%#Eval("AddressText")%>
                                                                                            </td>
                                                                                            <td class="">
                                                                                                <%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>
                                                                                            </td>
                                                                                            <td class=" last">
                                                                                                <a href='OrderDetail.aspx?id=<%#Eval("Unid") %>'>查看</a>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </colgroup>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--foot start-->
            <uc2:Foot runat="server" ID="FootUC" />
            <!--foot end-->
        </div>
        <!--新增完成后弹出的窗口-->
        <div id="divShowContent" style="display: none;">
            您可以继续进行的操作<br />
            <ul>
                <li>
                    <button type="button" class="scalable " onclick="GotoBack()" style="">
                        <span>返回</span></button></li>
                <li>
                    <button type="button" class="scalable " onclick="GotoList()" style="">
                        <span>查看订单列表</span></button></li>
                <li><a href="../Shop/ShopList.aspx">进入商家列表</a></li>
            </ul>
        </div>

        <script language="javascript" type="text/javascript">
            function GotoBack() {
                history.go(-1);
            }

            function GotoList() {
                window.location.href = "OrderList.aspx";
            }
        </script>

        <div id="test" style="position: absolute; border-right: #a5a5a5 1px solid; padding-right: 10px; border-top: #a5a5a5 1px solid; padding-left: 15px; border-left: #a5a5a5 1px solid; width: 200px; border-bottom: #a5a5a5 1px solid; background-color: #ffffff; display: none"
            onmouseover="this.style.display='block'">
            <span>商家信息</span><span style="float: right;"><a href="#" onclick="Hide(this);return false;">关闭</a>
            </span>
            <hr />
            <br />
        </div>


        <div id="print_area" style="display: none;"></div>

        <div style="display: none">

            <div id="printdata">

                <div class="printinfo">
                    <asp:Repeater runat="server" ID="rptorder">
                        <ItemTemplate>
                            <div style="width: 210px;">
                                <h1>
                                    <%# Eval("togoname") %></h1>
                                <ul style="border-bottom: 1px dashed #666666; margin-bottom: 5px; padding-bottom: 5px;">
                                    <li><span>单号:</span><label><%# Eval("orderid") %></label></li>
                                    <li><span>订单时间:</span><label><%# Eval("OrderDateTime")%></label></li>
                                    <li><span>用户名:</span><label><%# Eval("CustomerName")%></label></li>
                                    <li><span>收货人:</span><label><%# Eval("OrderRcver")%></label></li>
                                    <li><span>送餐时间:</span><label><%# Eval("SendTime")%></label></li>
                                    <li><span>手机:</span><label><%# Eval("CallPhoneNo")%></label></li>
                                    <li><span>备注:</span><label><%# Eval("OrderAttach")%></label></li>
                                </ul>
                                <ul class="food">
                                    <li class="name">商品</li>
                                    <li class="num">数量</li>
                                    <li class="price">单价</li>
                                    <li class="price">小计</li>
                                </ul>
                                <div class="clear"></div>
                                <ul class="food">


                                    <asp:Repeater runat="server" ID="Repeater2" DataSource='<%# getproduct(Eval("orderid")) %>'>
                                        <ItemTemplate>
                                            <li class="name"><%# Eval("foodname")%></li>
                                            <li class="num"><%#Eval("FCounts")%></li>
                                            <li class="price"><%# Convert.ToDecimal( Eval("foodPrice")) %></li>
                                            <li class="price"><%# Convert.ToDecimal(Eval("foodPrice")) * Convert.ToInt32(Eval("FCounts"))%></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div class="clear"></div>
                                <ul style="border-top: 1px dashed #666666; margin-top: 5px; padding-top: 5px;">
                                    <li>送餐费：<%# Eval("SendFee")%>元</li>
                                    <li>金额：<%# Eval("OrderSums")%>元</li>
                                    <li>类型：<%# Eval("ReveInt2").ToString() == "0"?"外卖":"堂食"%></li>
                                    <li>支付方式：<%#  WebUtility.TurnPayModel(Eval("paymode").ToString()) %></li>
                                    <li>支付状态：<%# WebUtility.TurnPayState(Eval("paystate")) %></li>
                                    <li>收货地址：<%# Eval("AddressText")%></li>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>

        </div>
    </form>
</body>
</html>

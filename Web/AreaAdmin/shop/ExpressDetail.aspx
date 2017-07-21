<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpressDetail.aspx.cs" Inherits="qy_54tss_AreaAdmin_Sale_OrderDetailExpressDetail"
    EnableEventValidation="false" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>跑腿订单信息管理-<%= WebUtility.GetMyName() %></title>
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

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/order.js" type="text/javascript"></script>

    <script src="../javascript/site_deliver.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">

        $(window).load(function () {
            $("#loading-mask").hide();
        });
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
        $(document).ready(function () {
            if (Request("id") != "") {
               // sitechange(2);
            }
            if (Request("tel") != "") {
                getuserinfo();
            }


            var state = $("#hfState").val();
            var _latlng = $("#hflatlng").val();
            var deliverid = $("#hfdeliverid").val();
            var orderid = $("#hforderid").val();
            var username = $("#hfusername").val();
            var address = $("#hfaddress").val();
            var shopname = $("#hfshopname").val();
            showorderexpress(state, 0, deliverid, orderid, username, address, shopname, 0)

        });

        function goprinter() {
            var id = Request("id");
            window.open("printorder.aspx?id=" + id);
        }

        function goa4printer() {
            var id = Request("id");
            window.open("A4PrintOrder.aspx?id=" + id);
        }

    </script>

</head>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hfcityid" />

        <asp:HiddenField runat="server" ID="hfState" />
        <asp:HiddenField runat="server" ID="hflatlng" />
        <asp:HiddenField runat="server" ID="hfdeliverid" />
        <asp:HiddenField runat="server" ID="hforderid" />
        <asp:HiddenField runat="server" ID="hfusername" />
        <asp:HiddenField runat="server" ID="hfaddress" />
        <asp:HiddenField runat="server" ID="hfshopname" />



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
                            <uc3:left runat="server" ID="adleft" />
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
                                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                <p style="" class="content-buttons form-buttons">
                                                    <asp:Button ID="Button2" runat="server" CssClass="button_1" OnClientClick="gourl('ExpressOrderList.aspx');return false;"
                                                        Text="返回"></asp:Button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                        OnClientClick="showload_super();" Text="保存"></asp:Button>
                                                </p>
                                            </div>
                                        </div>

                                        <!--start-->

                                        <div id="order-addresses">
                                            <div class="box-left">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">订单基本信息</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="order-billing_address_fields">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <table class="form-list" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountpassword">
                                                                                        订单编号 <span class="required"></span>
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <label id="latlng" runat="server" style="display:none"></label>
                                                                                    <epc:TextBox runat="server" ID="tborderid" class="required-entry input-text" Width="200px"
                                                                                        Height="40px"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountpassword">
                                                                                        商 品 <span class="required"></span>
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbInve2" class="required-entry input-text" Width="200px"
                                                                                        TextMode="MultiLine" Height="100px"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        发件联系人<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbUserName" Width="200px" CanBeNull="必填" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        发件联系电话<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbTel" Width="200px" onblur="getuserinfo()" CanBeNull="必填"
                                                                                        class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        发件联系人地址<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox TextMode="MultiLine" Cols="2" Height="60px" CanBeNull="必填" runat="server"
                                                                                        ID="tbAddress" Width="200px" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        收件联系人<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbcallmsg" Width="200px" CanBeNull="必填" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        收件联系电话<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbReveVar" Width="200px" onblur="getuserinfo()" CanBeNull="必填"
                                                                                        class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        收件地址<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox TextMode="MultiLine" Cols="2" Height="60px" CanBeNull="必填" runat="server"
                                                                                        ID="tbOorderid" Width="200px" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountpassword">
                                                                                        取件时间 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbSentTime" class="required-entry input-text" Width="200px"
                                                                                        CanBeNull="必填"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountpassword">
                                                                                        备注 <span class="required"></span>
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbremark" class="required-entry input-text" Width="200px"
                                                                                        TextMode="MultiLine" Height="100px"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountwebsite_id">
                                                                                        服务费用 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbsendmoney" CanBeNull="必填" RequiredFieldType="数据校验"
                                                                                        Width="60px" class=" required-entry required-entry input-text" Text="1"></epc:TextBox>元
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none" runat="server" id="tr_ordertime">
                                                                                <td class="label">
                                                                                    <label for="_accountwebsite_id">
                                                                                        订单时间<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tborderTime" class="required-entry input-text" Enabled="false"
                                                                                        Width="200px"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        订单状态<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value" style="width: 300px">
                                                                                    <asp:DropDownList runat="server" ID="ddlOrderState" Style="width: 80px;">
                                                                                        <asp:ListItem Text="新增" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="已经调度" Value="2"></asp:ListItem>
                                                                                        <asp:ListItem Text="配送中" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="成功" Value="3"></asp:ListItem>
                                                                                        <asp:ListItem Text="取消" Value="6"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        配送员<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value" style="width: 300px">
                                                                                    <%-- <asp:DropDownList runat="server" ID="ddlbid" Style="width: 100px;" onchange="sitechange(1);" AppendDataBoundItems="true">
                                                                                <asp:ListItem Value="0">选择配送点</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField runat="server" ID="tbbid" Value="0" />
                                                                            ---%>
                                                                                    <asp:DropDownList runat="server" ID="ddlInve1" Style="width: 80px;" onchange="getsubsort();"
                                                                                        Enabled="false">
                                                                                        <asp:ListItem Text="选择配送员" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:HiddenField runat="server" ID="tbInve1" Value="0" />
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="box-right">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">地图定位</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <div id="map_canvas" style='width: 100%; height: 870px'>
                                                                    </div>
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
            <uc2:Foot runat="server" ID="FootUC" />
        </div>
    </form>
</body>
</html>
<script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>
<script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>
<script src="../javascript/expressorderdetail.js"></script>




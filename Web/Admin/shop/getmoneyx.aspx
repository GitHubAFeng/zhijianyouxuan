﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getmoneyx.aspx.cs" Inherits="Admin_Shop_getmoneyx" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>网站统计－<%= WebUtility.GetMyName()%></title>
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
                                        <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                        </div>
                                    </div>
                                    <!--start-->
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address" runat="server" id="h4title">
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
                                                                                    日期<span class="required"></span></label>
                                                                                <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                                <asp:HiddenField runat="server" ID="hidDataId" />
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:TextBox runat="server" ID="tb_Start" class="inputclass" Width="80" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                                                至
                                                                                <asp:TextBox runat="server" ID="tb_End" class="inputclass" Width="80" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountprefix">
                                                                                    订单状态<span class="required"></span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList runat="server" ID="ddlOrderState" Width="100px">
                                                                                    <asp:ListItem Text="请选择" Selected="True" Value="-2"></asp:ListItem>
                                                                                    <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="审核通过" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="正在打印" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="处理成功" Value="3"></asp:ListItem>
                                                                                    <asp:ListItem Text="处理失败" Value="4"></asp:ListItem>
                                                                                    <asp:ListItem Text="订单已经取消" Value="5"></asp:ListItem>
                                                                                    <asp:ListItem Text="订单已经失效" Value="6"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <td>
                                                                                    <span>&nbsp;</span>
                                                                                </td>
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
                                                                                <asp:Button ID="Button2" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                                                    Text="统计"></asp:Button>
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
    <div style="display: none; left: 526px; top: 236px;" id="address_drop">
    </div>
    </form>
</body>
</html>

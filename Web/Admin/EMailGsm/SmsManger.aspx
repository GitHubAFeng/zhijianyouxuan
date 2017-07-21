<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmsManger.aspx.cs" Inherits="Admin_EMailGsm_SmsManger" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagPrefix="uc3" TagName="left" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量邮件发送第二步编辑邮件-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidUserIdList" runat="server" />
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
                    <uc3:left ID="Left1" runat="server" />
                    <div class="main-col" id="content">
                        <div class="main-col-inner">
                            <div id="divMessages">
                            </div>
                            <div style="visibility: visible;" class="content-header">
                                <h3 class="icon-head head-customer">
                                    <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                    <p style="" class="content-buttons form-buttons">
                                        <asp:Button ID="btSent" runat="server" CssClass="button_1" Text="注册" OnClick="btSent_Click">
                                        </asp:Button>
                                    </p>
                                </div>

                            </div>
                            <!--start-->
                            <div class="entry-edit">
                                <div id="customer_info_tabs_account_content" style="">
                                    <div class="entry-edit">
                                        <div class="entry-edit-head">
                                            <h4 class="icon-head head-billing-address">
                                                短信注册</h4>
                                        </div>
                                        <div class="order-address" id="order-billing_address_fields">
                                            <div class="content">
                                                <div class="hor-scroll">
                                                    <table class="form-list" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td class="label" colspan="2">
                                                                <fieldset class="AdminSearchform">
                                                                    <legend>注册信息</legend>
                                                                    <table border="0" cellpadding="0" cellspacing="0" class="condition_table">
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountprefix">
                                                                                    序列号<span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="txtCDKey" CanBeNull="必填" Width="160" 
                                                                                    class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    密码 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="txtPassword" CanBeNull="必填" Width="160" 
                                                                                    class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end-->
                        </div>
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

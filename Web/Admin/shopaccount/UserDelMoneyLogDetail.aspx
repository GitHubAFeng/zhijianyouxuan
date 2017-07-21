<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDelMoneyLogDetail.aspx.cs"
    Inherits="Admin_shopaccount_UserDelMoneyLogDetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="UserDelMoneyLogDetail" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户消费记录-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/style.css" rel="stylesheet" type="text/css" />--%>
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
        
        $(window).load(function(){$("#loading-mask").hide();});
        function reset()
        {
            var list = document.getElementsByName("invalue");
            for (var i = 0 ; i < list.length ; i++)
            {
                list.item(i).value = "";
            }
        } 
    </script>

    <script language="javascript" type="text/javascript">
        
        function GotoADList()
        {
            history.go(-1);
        }
    </script>

    <script language="javascript" type="text/javascript">
        
        function GotoBuildingDetail()
        {
            window.location.href = "UserDelMoneyLogDetail.aspx";
        }
        function GotoADList()
        {
            window.location.href = "UserDelMoneyLog.aspx";
        }
    </script>

    <style type="text/css">
        .lable{ width:70px ;}
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="~/Admin/images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
    <div class="wrapper">
        <uc1:TogoBanner runat="server" ID="Banner" />
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
                        <uc3:UserDelMoneyLogDetail ID="UserDelMoneyLogDetail" runat="server" />
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
                                                <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="GotoADList();return false;"
                                                    Text="返回列表"></asp:Button>
                                                <asp:Button ID="btSave" runat="server" CssClass="button_1" Text="保存" OnClick="btSave_Click">
                                                </asp:Button>
                                            </p>
                                        </div>
                                        <fieldset class="AdminSearchform">
                                            <legend>
                                                <asp:Label runat="server" ID="lbTogoName"></asp:Label></legend>
                                            <ul class="FunctionUl">
                                                <li>
                                                    <li>
                                                        <button type="button" class="scalable " onclick="GotoADList()" style="">
                                                            <span>用户消费记录列表</span></button></li>
                                            </ul>
                                        </fieldset>
                                    </div>
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">
                                                        用户消费记录信息</h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="order-billing_address_fields">
                                                        <div class="content">
                                                            <div class="hor-scroll">
                                                                <table class="form-list" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                用户编号<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <asp:HiddenField runat="server" ID="HiddenField2" />
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbDataid" CanBeNull="必填" Width="40px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                用户名称<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                            <td class="value">
                                                                                <asp:Label ID="tbTogoName" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                消费金额<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <asp:HiddenField runat="server" ID="HiddenField3" />
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbDelMoney" CanBeNull="必填" Width="100px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                消费名目<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <asp:HiddenField runat="server" ID="HiddenField4" />
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbBuyItem" CanBeNull="必填" Width="100px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                新增时间<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbNewAdddate" CanBeNull="必填" Width="100px" onfocus="WdatePicker()"></epc:TextBox>
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
        <uc2:Foot runat="server" ID="FootUC" />
    </div>
    <div style="display: none; left: 526px; top: 236px;" id="address_drop">
    </div>
    </form>
</body>
</html>

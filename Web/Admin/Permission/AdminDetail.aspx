<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminDetail.aspx.cs" Inherits="Admin_AdminDetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理员管理-<%= WebUtility.GetMyName() %></title>
     <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="../javascript/Building.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function() { $("#loading-mask").hide(); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }
        $(document).ready(function() { WhitchActive(7) }); 

        //给一个输入框赋值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }  
    </script>

    <script language="javascript" type="text/javascript">

        function GotoBuildingDetail() {
            window.location.href = "BuildingDetail.aspx";
        }
        function GotoAdminList() {
            window.location.href = "AdminList.aspx";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
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
                                            <p style="" class="content-buttons form-buttons">
                                                <button type="button" class="scalable back" onclick='GotoAdminList()' style="">
                                                    <span>返回列表</span></button>
                                                <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                    Text="保存"></asp:Button>
                                            </p>
                                        </div>
                                    </div>
                                    <!--start-->
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">
                                                        管理员信息</h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="order-billing_address_fields">
                                                        <div class="content">
                                                            <div class="hor-scroll">
                                                                <table class="form-list" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="3" class="hidden">
                                                                                <input id="_accountpassword_hash" name="account[password_hash]" value="" class=""
                                                                                    type="hidden">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="left_td">
                                                                                <label for="_accountwebsite_id">
                                                                                    城市 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value" style="width: 600px">
                                                                                <asp:DropDownList ID="DDLArea" runat="server" Width="100" class=" required-entry required-entry input-text"
                                                                                    AppendDataBoundItems="true">
                                                                                    <asp:ListItem Value="0">所有城市</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="mynotice">
                                                                                    选择“所有城市”管理员可管理所有数据</div>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountwebsite_id">
                                                                                    用户类型 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value" style="width: 600px;">
                                                                                <asp:DropDownList ID="stpermition" runat="server" Width="100px">
                                                                                    <asp:ListItem Value="0">普通管理员</asp:ListItem>
                                                                                    <asp:ListItem Value="1">超級管理员</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="mynotice">
                                                                                    超級管理员不受权限的限制</div>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountwebsite_id">
                                                                                    用户角色 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value" style="width: 600px;">
                                                                                <asp:DropDownList ID="ddlRem" runat="server" Width="100px" AppendDataBoundItems="true">
                                                                                <asp:ListItem Value="-1">所有</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="display:none;">
                                                                            <td class="label">
                                                                                <label for="_accountwebsite_id">
                                                                                    分区域管理员 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddlroot" runat="server" Width="100px">
                                                                                    <asp:ListItem Value="1">是</asp:ListItem>
                                                                                    <asp:ListItem Value="0">否</asp:ListItem>
                                                                                    
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountprefix">
                                                                                    登录帐号<span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbName" CanBeNull="必填" Width="160" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    名字 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbRealname" CanBeNull="必填" Width="160" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountpassword">
                                                                                    密码 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox TextMode="Password" runat="server" ID="tbPassword" class=" required-entry required-entry input-text"></epc:TextBox><br />
                                                                                <label runat="server" id="lbNotice">
                                                                                </label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountpassword">
                                                                                    密码确认 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox TextMode="Password" runat="server" ID="tbConfirm" class=" required-entry required-entry input-text"></epc:TextBox>
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

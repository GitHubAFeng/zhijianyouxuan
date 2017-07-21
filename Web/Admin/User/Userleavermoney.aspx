<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Userleavermoney.aspx.cs"
    Inherits="Admin_User_Userleavermoney" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagPrefix="uc3" TagName="left" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户的账户余额管理-<%= WebUtility.GetMyName()%></title>
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
        $(document).ready(function(){init();});

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }

        function GotoUserList() {
            window.location.href = 'UserList.aspx';
        }

        function getData() {
            var selecttype = document.getElementById("sttype").value;
            var _value = $I("tbUsername").value;
            var _type;
            if (selecttype == '1') {
                _type = "uid";
            }
            else {
                _type = "nike";
            }
            jQuery.ajax({
                type: "post",
                url: "../../Ajax/CheckUser.aspx",
                async: true,
                data: "type=" + _type + "&value=" + _value + "",
                success: function(msg) {
                    if (msg == 'n') {
                        jQuery("#lbCurrentPoint").html("");
                        alert('没有此用户');
                    }
                    else {
                        jQuery("#lbCurrentPoint").html(msg);
                    }
                }

            });
        }
        function $I(eid) {
            return document.getElementById(eid);
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
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
                                        <fieldset class="AdminSearchform">
                                            <legend>
                                                <asp:Label runat="server" ID="lbTogoName"></asp:Label></legend>
                                            <ul class="FunctionUl">
                                                <li>
                                                    <button type="button" class="scalable " onclick="GotoUserList()" style="">
                                                        <span>返回用户列表</span></button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                        Text="保存"></asp:Button>
                                                </li>
                                            </ul>
                                        </fieldset>
                                    </div>
                                    <!--start-->
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">
                                                        用户详细信息</h4>
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
                                                                                    用户编号<span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbDataId" Enabled="false" Width="40" name="invalue"
                                                                                    CanBeNull="必填" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    功能 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddlfunction" runat="server" Width="70px">
                                                                                    <asp:ListItem Selected="True" Value="增加">增加</asp:ListItem>
                                                                                    <asp:ListItem Value="减少">减少</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    金额 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <span>
                                                                                    <epc:TextBox runat="server" ID="tboldtbmoney" CanBeNull="必填" Width="60"  RequiredFieldType="数据校验" class=" required-entry required-entry input-text"></epc:TextBox>元
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    要加减的金额 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <span>
                                                                                    <epc:TextBox runat="server" ID="tbmoney" name="invalue"  RequiredFieldType="数据校验" CanBeNull="必填" Width="60"
                                                                                        class=" required-entry required-entry input-text"></epc:TextBox>元 </span>
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
    </form>
</body>
</html>

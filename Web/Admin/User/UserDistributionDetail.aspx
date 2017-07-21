<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDistributionDetail.aspx.cs"
    Inherits="qy_54tss_Admin_User_UserDistributionDetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户充值管理-<%= WebUtility.GetMyName() %></title>
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
        AddLoadFun(init);

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的会员!");
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
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="gourl('UserDistributionList.aspx?uid='+Request('uid'));return false;"
                                                        Text="返回"></asp:Button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click" OnClientClick="showload_super()"
                                                        Text="保存"></asp:Button>
                                                </p>
                                            </div>
                                        </div>
                                        <!--start-->
                                        <div class="entry-edit">
                                            <div id="customer_info_tabs_account_content" style="">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">充值记录</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="order-billing_address_fields">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <table class="form-list" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountwebsite_id">
                                                                                        充值用户
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <label for="_accountprefix" runat="server" id="lbnikename">
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountwebsite_id">
                                                                                        分销账户余额
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <label for="_accountprefix" runat="server" id="lbmoney">
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="width: 300px;">
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        充值金额</label>
                                                                                </td>
                                                                                <td class="value" style="width: 500px;">
                                                                                    <epc:TextBox runat="server" ID="tbpoint" CanBeNull="必填" RequiredFieldType="数据校验"
                                                                                        Width="60px" class="required-entry input-text"></epc:TextBox>元
                                                                                <div class="mynotice">
                                                                                    只能输入数字
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>


                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                       备注：<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbspecial" Width="290px" MaxLength="128" TextMode="MultiLine" class="required-entry input-text"></epc:TextBox>
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

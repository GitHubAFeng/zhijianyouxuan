<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopReviewdetail.aspx.cs"
    Inherits="Admin_Shop_ShopReviewdetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看评论 -
        <%= WebUtility.GetMyName()%></title>
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

        $(window).load(function() { $("#loading-mask").hide(); showContent(1); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }
        $(document).ready(function() { WhitchActive(2); $("#aA2").addClass("active"); });

        //给一个输入框赋值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }
    </script>

    <script type="text/javascript">
        function chekckdata() {
            var v = $("#tbrcontent").val() + "";
            if (v == "") {
                jtip("请输入回复内容。");
                return false;
            }
            return true;
        }
    </script>

    <style type="text/css">
        textarea
        {
            font-size: 12px;
        }
        .label
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                        <uc4:togoleft runat="server" ID="left" />
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
                                                <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="window.location = 'ShopReviewList.aspx'"
                                                    Text="返回"></asp:Button>
                                                <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="Save_Click" OnClientClick="return chekckdata();"
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
                                                        评论信息</h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="order-billing_address_fields">
                                                        <div class="content">
                                                            <div class="hor-scroll">
                                                                <table class="form-list" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    用户名： <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:Label ID="lbUsername" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                   <%--     <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    用户名： <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList runat="server" ID="ddlpint" Width="70px">
                                                                                    <asp:ListItem Value="0">未审核</asp:ListItem>
                                                                                    <asp:ListItem Value="1">已通过</asp:ListItem>
                                                                                    <asp:ListItem Value="2">未通过</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    点评商家： <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:Label ID="lbtogoname" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    内容： <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:Literal ID="LitContent" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    时间： <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:Label ID="lbtime" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    回复： <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:TextBox runat="server" ID="tbrcontent" TextMode="MultiLine" Width="300px" Height="100px"></asp:TextBox>
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
    </div>
    <!--foot start-->
    <uc2:Foot runat="server" ID="FootUC" />
    <!--foot end-->
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="autodispatchconfig.aspx.cs" Inherits="Admin_Shop_autodispatchconfig"
    ValidateRequest="false" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自动调度配置 - <%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css?v=1" rel="stylesheet" type="text/css" />
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


    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js?v=1" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });

        //状态改变
        function sortchange() {
            var shoptype = $("input[name='tbisopen']:checked").val();
            switch (shoptype) {
                case "0":
                    $("#trlaw").hide();
                    break;
                case "1":
                    $("#trlaw").show();
                    break;

            }
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
                                                        <h4 class="icon-head head-billing-address">自动调度配置</h4>
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
                                                                                        自动调度<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:RadioButtonList runat="server" ID="tbisopen" RepeatDirection="Horizontal" onchange="sortchange()">
                                                                                        <asp:ListItem Value="0">关闭</asp:ListItem>
                                                                                        <asp:ListItem Value="1">开启</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trlaw" runat="server">
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        调度规则<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <div style="height: 30px; line-height: 30px">
                                                                                        <input type="radio" name="uselaw" runat="server" id="law1" /><span>
                                                                                订单发给所有骑士进行抢单
                                                                                        </span>
                                                                                    </div>
                                                                                    <div style="height: 30px; line-height: 30px;">
                                                                                        <input type="radio" name="uselaw" runat="server" id="law2" /><span>
                                                                               
                                                                                订单发给距离商家<asp:TextBox runat="server" ID="tbdistance" reg="^\d+$" tip="距离请输入整数"
                                                                                    canbenull="y" Width="40" Text="0" class=" j_text"></asp:TextBox>公里内的<asp:TextBox runat="server" ID="tbreveint1" reg="^\d+$" tip="骑士数量请输入整数"
                                                                                        canbenull="y" Width="40" Text="0" class=" j_text"></asp:TextBox>个骑士进行抢单
                                                                                                                                                                              
                                                                                        </span>
                                                                                    </div>
                                                                                    <div style="height: 30px; line-height: 30px;">
                                                                                        <input type="radio" name="uselaw" runat="server" id="law3" /><span>
                                                                                        订单调度给最近且在线的骑士
                                                                                        </span>
                                                                                    </div>
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
    </form>
</body>
</html>

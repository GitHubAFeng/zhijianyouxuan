<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupSentGsm.aspx.cs" Inherits="shop_GroupSentGsm" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>群发短信编辑-<%= WebUtility.GetMyName()%></title>
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../Admin/css/boxes.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidUserIdList" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="warp_con">
                    <uc2:LeftBanner runat="server" ID="Left" />
                    <div class="rightmenu_cont">
                        <div style="visibility: visible;" class="content-header">
                            <h3 class="icon-head head-customer">
                                <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                <p style="" class="content-buttons form-buttons">
                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                        Text="返回重新选择用户"></asp:Button>
                                    <asp:Button ID="btSent" runat="server" CssClass="button_1" Text="提交" OnClick="btSent_Click"></asp:Button>
                                </p>
                            </div>
                            <fieldset class="AdminSearchform">
                                <legend>
                                    <asp:Label runat="server" ID="lbTogoName"></asp:Label></legend>
                            </fieldset>
                        </div>
                        <div class="entry-edit" style="padding: 10px;">
                            <div id="customer_info_tabs_account_content" style="">
                                <div class="entry-edit">
                                    <div class="entry-edit-head">
                                        <h4 class="icon-head head-billing-address">批量发送短信</h4>
                                    </div>
                                    <div class="order-address" id="order-billing_address_fields">
                                        <div class="content">
                                            <div class="hor-scroll">
                                                <table class="form-list" cellspacing="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="label" colspan="2">
                                                                <fieldset class="AdminSearchform">
                                                                    <legend>收件人:</legend>
                                                                    <table border="0" cellpadding="0" cellspacing="0" class="condition_table">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox TextMode="MultiLine" Height="200px" Width="500px" runat="server" ID="tbPhoneList"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td class="label" colspan="2">
                                                                <label for="_accountprefix">
                                                                    短息内容：</label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:TextBox TextMode="MultiLine" Height="100px" Width="300px" runat="server" ID="tbContent"></asp:TextBox>
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
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

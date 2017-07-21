<%@ Page Language="C#" AutoEventWireup="true" CodeFile="distributeRatioset.aspx.cs" Inherits="Admin_manage_distributeRatioset" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
        function reset() {

            $("#tbName").html();
            $("#tbLinkUrl").html();
            $("#tbOrder").html();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <asp:HiddenField ID="currntBord" Value="8" runat="server" />
        <div class="wrapper">
            <uc1:TogoBanner runat="server" ID="Banner" />
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
                                                    <div style="float: right; height: 25px; margin-top: -1px">
                                                        <asp:Button ID="btSave" runat="server" CssClass="form-button" OnClick="btSave_Click"
                                                            Text="保存"></asp:Button>
                                                    </div>
                                                    <p>
                                                    </p>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="entry-edit">
                                            <div id="customer_info_tabs_account_content" style="">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-edit-form fieldset-legend">基本信息</h4>
                                                        <asp:HiddenField runat="server" ID="hidDataId" />
                                                        <div class="form-buttons">
                                                        </div>
                                                    </div>
                                                    <div class="fieldset " id="_accountbase_fieldset">
                                                        <div class="hor-scroll" style="overflow: auto; height: auto">
                                                            <table class="form-list" cellspacing="0">
                                                                <tbody>


                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountprefix">
                                                                                名称<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                           <asp:Label runat="server" ID="tbtitle"></asp:Label>


                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountprefix">
                                                                                一级比例<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <epc:TextBox runat="server" ID="tbonegraderatio" CanBeNull="必填" Width="60" RequiredFieldType="正整数" class=" required-entry required-entry input-text">
                                                                            </epc:TextBox>%
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountwebsite_id">
                                                                                二级比例 <span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <epc:TextBox runat="server" ID="tbtwograderatio" CanBeNull="必填" Width="60" RequiredFieldType="正整数" class=" required-entry required-entry input-text">
                                                                            </epc:TextBox>%
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountfirstname">
                                                                                三级比例 <span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <epc:TextBox runat="server" ID="tbthreegraderatio" CanBeNull="必填" Width="60" RequiredFieldType="正整数" class=" required-entry required-entry input-text">
                                                                            </epc:TextBox>%
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
                                            </div>
                                        </div>
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

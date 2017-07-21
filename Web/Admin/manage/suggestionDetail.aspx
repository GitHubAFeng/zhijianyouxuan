<%@ Page Language="C#" AutoEventWireup="true" CodeFile="suggestionDetail.aspx.cs"
    Inherits="qy_54tss_Admin_suggestionDetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>留言版-<%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
        function reset() {
            $("#tbTitle").html();
            $("#tbNewsContent").html();
            $("#tbFrom").html();
        }

        $(document).ready(function() { $("#tA6").addClass("active") });
    </script>

    <style type="text/css">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
                        <uc3:left runat="server" ID="adleft" />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="main-col" id="content">
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>
                                    <div style="visibility: visible;" class="content-header">
                                        <h3 class="icon-head head-customer">
                                            <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                        <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                            <span class="content-buttons form-buttons">
                                                <div style="float: right;">
                                                    <asp:Button ID="Button1" runat="server" CssClass="form-button" OnClientClick='javascript:location.href="suggestionList.aspx" ; return false;'
                                                        Text="返回"></asp:Button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="form-button" OnClick="btSave_Click"
                                                        Text="保存"></asp:Button></div>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-edit-form fieldset-legend">
                                                        留言版</h4>
                                                    <asp:HiddenField runat="server" ID="hidDataId" />
                                                    <div class="form-buttons">
                                                    </div>
                                                </div>
                                                <div class="fieldset " id="_accountbase_fieldset">
                                                    <div class="hor-scroll" style="overflow: auto; height: auto">
                                                        <table class="form-list" cellspacing="0" cellpadding="0" style="height: auto">
                                                            <tbody>
                                                                <tr style="display:none">
                                                                    <td class="label">
                                                                        <label for="_accountwebsite_id">
                                                                            商户名称<span class="required"></span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <asp:TextBox ID="tbshopname" runat="server" ReadOnly="true" CssClass="j_text"></asp:TextBox>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountwebsite_id">
                                                                            用户名 <span class="required"></span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <asp:TextBox ID="tbUserName" runat="server" ReadOnly="true" CssClass="j_text"></asp:TextBox>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                              
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountfirstname">
                                                                            内容 <span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <div  id="tbdesc" runat="server"></div>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountfirstname">
                                                                            状态 <span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <asp:DropDownList runat="server" ID="ddlstate" Width="100">
                                                                            <asp:ListItem Value="0">未审核</asp:ListItem>
                                                                            <asp:ListItem Value="1">审核通过</asp:ListItem>
                                                                            <asp:ListItem Value="2">审核失败</asp:ListItem>
                                                                        </asp:DropDownList>
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
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <uc2:Foot runat="server" ID="FootUC" />
    </form>
</body>
</html>

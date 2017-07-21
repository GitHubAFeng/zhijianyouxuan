<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="Admin_NewsDetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="Adleft" TagPrefix="uc4" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公告信息管理-<%= WebUtility.GetMyName()%></title>
     <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="css/ie7.css" media="all" />
    <![endif]-->

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

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
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
                        <uc4:Adleft ID="Adleft1" runat="server" />
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
                                                    <asp:Button ID="Button1" runat="server" CssClass="form-button" OnClientClick='javascript:location.href="NewsList.aspx" ; return false;'
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
                                                        公告信息</h4>
                                                    <asp:HiddenField runat="server" ID="hidDataId" />
                                                    <div class="form-buttons">
                                                    </div>
                                                </div>
                                                <div class="fieldset " id="_accountbase_fieldset">
                                                    <div class="hor-scroll" style="overflow: auto; height: auto">
                                                        <table class="form-list" cellspacing="0" cellpadding="0" style="height: auto">
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="3" class="hidden">
                                                                        <input id="_accountpassword_hash" name="account[password_hash]" value="" class=""
                                                                            type="hidden">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountwebsite_id">
                                                                            标题 <span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" ID="tbTitle" MaxLength="20" CanBeNull="必填" Width="360"
                                                                            class=" required-entry required-entry input-text">
                                                                        &nbsp;
                                                                        </epc:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td class="label">
                                                                        <label for="_accountfirstname">
                                                                            来源 <span class="required"></span>
                                                                        </label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" ID="tbOrigin" Width="360" class=" required-entry required-entry input-text">
                                                                    
                                                                        </epc:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td class="label">
                                                                        <label for="_accountfirstname">
                                                                            新闻图片 <span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <asp:HiddenField ID="ImgUrl1" runat="server" Value="20" />
                                                                        &nbsp; <a href="" id="txturldz" target="_blank">
                                                                            <img border="0" src="../images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 106px;
                                                                                height: 95px" runat="server" /></a><br />
                                                                        <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display='block';" /><br />
                                                                        <div id="rowTest" style="display: none">
                                                                            <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 550px; height: 130px"
                                                                                frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight">
                                                                            </iframe>
                                                                        </div>
                                                                        <div id="Upload">
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountwebsite_id">
                                                                            排序 <span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" ID="tbistop" Text="1" Width="60" RequiredFieldType="数据校验"
                                                                            CanBeNull="必填" class=" required-entry required-entry input-text">
                                                                    
                                                                        </epc:TextBox>(提示：数字大，排在前)
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountwebsite_id">
                                                                            浏览 <span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" ID="tbreve1" Text="0" Width="60" RequiredFieldType="数据校验"
                                                                            CanBeNull="必填" class=" required-entry required-entry input-text">
                                                                    
                                                                        </epc:TextBox>
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
                                                                        <FCKeditorV2:FCKeditor ID="FCKContent" runat="server" ToolbarSet="Basic" BasePath="../fckeditor/"
                                                                            Width="580px" Height="400px">
                                                                        </FCKeditorV2:FCKeditor>
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

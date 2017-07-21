<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailGsmRecDeTail.aspx.cs"
    Inherits="Admin_shopaccount_a" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="Integralleft" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家邮件信息-<%= WebUtility.GetMyName()%></title>
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

</head>
<body>
    <form id="form1" runat="server">
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <!--banner end-->
        <!--center start-->
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <uc3:Integralleft ID="Integralleft1" runat="server" />
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
                                                            <span>佣金记录列表</span></button></li>
                                            </ul>
                                        </fieldset>
                                    </div>
                                    <!--start-->
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">
                                                        商家邮件信息</h4>
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
                                                                                    编号<span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbDataid"  Enabled="false" CanBeNull="必填" Width="400px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                商家名称<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" Enabled="false"  ID="tbTogoName" CanBeNull="必填" Width="400px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                消费金额 <font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbDelMoney" CanBeNull="必填" Width="400px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                类型<font>*</font>：
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddlType" runat="server">
                                                                                    <asp:ListItem Value="0">短信 </asp:ListItem>
                                                                                    <asp:ListItem Value="1">邮件</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                数量<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbSum" CanBeNull="必填" Width="400px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                添加时间<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbCdate" CanBeNull="必填" Width="400px" onfocus="WdatePicker()"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                用户ID列表<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbUserIdList" CanBeNull="必填" Width="400px"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="lable">
                                                                                状态<font color="#FF0000">*</font>：
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddlState" runat="server">
                                                                                    <asp:ListItem Value="0">新增 </asp:ListItem>
                                                                                    <asp:ListItem Value="1">进行中</asp:ListItem>
                                                                                    <asp:ListItem Value="2">完成 </asp:ListItem>
                                                                                    <asp:ListItem Value="3">失败</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    内容 <span class="reContent">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <FCKeditorV2:FCKeditor ID="fcContent" runat="server" ToolbarSet="Basic" BasePath="../fckeditor/"
                                                                                    Value="" Height="300px" Width="650px">
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

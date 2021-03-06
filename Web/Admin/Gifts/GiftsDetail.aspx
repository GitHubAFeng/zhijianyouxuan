﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GiftsDetail.aspx.cs" Inherits="Admin_Gifts_GiftsDetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register src="~/Admin/Adleft.ascx" tagname="Giftsleft" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>礼品管理-<%= WebUtility.GetMyName()%></title>
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
        
        //给一个输入框赋值
        function SetValue(objectName,objectValue)
        {
            $("#"+objectName+"").val(objectValue);
        }
    </script>

    <script language="javascript" type="text/javascript">
        
        function GotoBuildingDetail()
        {
            window.location.href = "BuildingDetail.aspx";
        }
        function GotoADList()
        {
            window.location.href = "GiftsList.aspx";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hfpicture" />
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
                    <uc3:Giftsleft ID="Giftsleft1" runat="server" />
&nbsp;<div class="main-col" id="content">
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
                                                        礼品信息</h4>
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
                                                                                    礼品名称<span class="required">*</span></label>
                                                                                <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                                <asp:HiddenField runat="server" ID="hidDataId" />
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbName" CanBeNull="必填" Width="400px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    礼品所需积分 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbNeed" CanBeNull="必填" Width="60px" RequiredFieldType="数据校验"></epc:TextBox>分
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    礼品价值 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbPrice" CanBeNull="必填" Width="60px" RequiredFieldType="数据校验"></epc:TextBox>元
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    礼品类别 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddlGclass" runat="server" Width="100">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    礼品信息图片<span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value" style="width: 500px;">
                                                                                <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                                <asp:HiddenField ID="FolderType" Value="1" runat="server" />
                                                                                <asp:HiddenField ID="WaterType" Value="0" runat="server" />
                                                                              
                                                                                    <img border="0" src="../Images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 300px;
                                                                                        height: 300px" runat="server" /><br />
                                                                                <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display='block';return txtupload_onclick();" />请上传600*600的图片<br />
                                                                                <div id="rowTest" style="display: none">
                                                                                    <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 400px; height: 130px"
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
                                                                                <label for="_accountfirstname">
                                                                                    库存 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbstocks" CanBeNull="必填" Width="60px" RequiredFieldType="数据校验"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    排序 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbsortnum" Text="1" CanBeNull="必填" Width="60px" RequiredFieldType="数据校验"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    礼品信息内容 <span class="required">*</span></label>
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

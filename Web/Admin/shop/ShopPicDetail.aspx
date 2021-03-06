﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopPicDetail.aspx.cs" Inherits="Admin_ShopPicDetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家环境图管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
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
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/easyajaxcore.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });


        $(document).ready(function () {

            imgonload();
        });

        //给一个输入框赋值
        function imgonload() {
            var img = document.getElementById('ImgUrl');
            img.onload = function () {
                DrawImage(img);
            };

        }

        function GotoADList() {
            window.location.href = "ShopPicList.aspx?tid=" + document.getElementById("hidTogoId").value + "";
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

                                        <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none" runat="server">
                                            <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" id="diagram_tab_orders" class="tab-item-link">
                                                <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                            </li>
                                            <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                            <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                   
                                            <li><a href="ShopPicList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link active"><span>
                                                <span class="changed"></span><span class="error"></span>商家环境图片</span> </a></li>
                                          
                                        </ul>

                                        <div style="visibility: visible;" class="content-header">
                                            <h3 class="icon-head head-customer">
                                                <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                <p style="" class="content-buttons form-buttons">
                                                    <button type="button" class="button_1 " onclick="GotoADList()" style="">
                                                        <span>返回列表</span></button>
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
                                                        <h4 class="icon-head head-billing-address">商家环境图信息</h4>
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
                                                                                        标题<span class="required">*</span></label>
                                                                                    <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbName" CanBeNull="必填" Width="260" class=" required-entry required-entry input-text"></epc:TextBox>
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
                                                                                    <epc:TextBox runat="server" ID="tbsort" CanBeNull="必填" Width="60px" RequiredFieldType="数据校验"></epc:TextBox><div
                                                                                        class="mynotice">
                                                                                        数字越大越靠前
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountpassword">
                                                                                        图片 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                                    <asp:HiddenField ID="FolderType" Value="1" runat="server" />
                                                                                    <asp:HiddenField ID="WaterType" Value="0" runat="server" />
                                                                                    <img border="0" src="../Images/System/wutu.gif" id="ImgUrl" alt=""
                                                                                        runat="server" /><br />
                                                                                    <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" />
                                                                                    <label id="admsg">提示:请上传480*800的图片 </label>
                                                                                    <br />
                                                                                    <div id="rowTest" style="display: none">
                                                                                        <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 500px; height: 130px" frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
                                                                                    </div>
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

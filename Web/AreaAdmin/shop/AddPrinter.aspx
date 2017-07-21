<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPrinter.aspx.cs" Inherits="qy_54tss_AreaAdmin_Shop_AddPrinter" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商店打印机管理-<%= WebUtility.GetMyName()%></title>
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

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>


    <script src="../javascript/Printer.js?v=1" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function() { $("#loading-mask").hide(); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }

        //给一个输入框赋值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }
    </script>

    <script language="javascript" type="text/javascript">

        function GotoAddFoodSort() {
            window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
        function GotoAddFood() {
            window.location.href = "FoodDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoAddPrinter() {
            window.location.href = "AddPrinter.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoAddFoodSort() {
            window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoFoodList() {
            window.location.href = "FoodList.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
        function GotoShopList() {
            window.location.href = "ShopList.aspx?t=" + new Date().getTime() + "";
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

                                     <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none">
                                            <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" class="tab-item-link ">
                                                <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                            </li>
                                            <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                            <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                            <li><a href="Distancepaylist.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                            <li><a href="ShopLocal.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>

                                             <li><a href="AddPrinter.aspx?tid=<%= Request["tid"] %>" class="tab-item-link active"><span>
                                                <span class="changed"></span><span class="error"></span>打印机</span> </a></li>

                                        </ul>


                                    <div style="visibility: visible;" class="content-header">
                                        <h3 class="icon-head head-customer">
                                            <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                        <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                            <p style="" class="content-buttons form-buttons">
                                                <button type="button" class="scalable back" onclick='GotoShopList()' style=" margin-bottom:0; float:left; margin-right:0px;">
                                                    <span>返回商店列表</span></button>
                                               
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
                                                        商店打印机信息</h4>
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
                                                                                    是否设置打印机<span class="required"></span></label>
                                                                            </td>
                                                                            <td class="value" colspan="2">
                                                                                <asp:CheckBox runat="server" ID="cbUsePrinter" Checked="true" OnCheckedChanged="cbUsePrinter_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                注意：删除商店使用的打印机则不勾选此项并保存
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountprefix">
                                                                                   商店名称<span class="required">*</span></label>
                                                                                <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                                <asp:HiddenField runat="server" ID="hidDataId" />
                                                                                <asp:HiddenField runat="server" ID="hidPrinterSn" />
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbName" name="invalue" CanBeNull="必填" Width="200px"
                                                                                    class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    手机号码 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbtogonum" name="invalue" 
                                                                                    Width="200px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    打印机编号 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbPrinterNum" name="invalue" Width="200px" class=" required-entry required-entry input-text"
                                                                                    onblur="GetPrinterSn()"></epc:TextBox>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    打印机SN <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbPrinterSn" Enabled="false" name="invalue"
                                                                                    Width="200px" class=" required-entry required-entry input-text"></epc:TextBox>根据打印机编号自动获取无需填写
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    打印抬头 <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" name="invalue" ID="tbPrintTop" Width="200px" TextMode="MultiLine"
                                                                                    Height="40px" Cols="2" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    打印结尾 <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" name="invalue" ID="tbPrintFoot" Width="200px" class=" required-entry required-entry input-text"
                                                                                    TextMode="MultiLine" Height="40px" Cols="2"></epc:TextBox>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    打印联数 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList runat="server" ID="ddlPage">
                                                                                    <asp:ListItem Text="1份" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="2份" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="3份" Value="3"></asp:ListItem>
                                                                                    <asp:ListItem Text="4份" Value="4"></asp:ListItem>
                                                                                    <asp:ListItem Text="5份" Value="5"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                          
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    是否更新<span class="required"></span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:CheckBox runat="server" ID="cbIsUpdate" Enabled="false" />
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    最后更新时间 <span class="required"></span>
                                                                                </label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" name="invalue" ID="tbLastLoginDate" Width="200px" Enabled="false"
                                                                                    class=" required-entry required-entry input-text"></epc:TextBox>
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
    <div id="divShowContent" style="display: none;">
        您可以继续进行的操作<br />
        <ul>
            <li>
                <button type="button" class="scalable " onclick="GotoAddFoodSort()" style="">
                    <span>为此商店添加商品分类</span></button></li>
            <li>
                <button type="button" class="scalable " onclick="GotoAddFood()" style="">
                    <span>为此商店添加菜单</span></button></li>
            <li><a href="ShopDetail.aspx">新增商店</a></li>
            <li><a href="ShopList.aspx">进入商店列表</a></li>
        </ul>
    </div>

    <script language="javascript" type="text/javascript">
        function GotoAddFood() {
            window.location.href = "FoodDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoAddFoodSort() {
            window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
       
    </script>

    </form>
</body>
</html>


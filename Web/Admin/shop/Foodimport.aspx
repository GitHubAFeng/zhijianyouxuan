<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Foodimport.aspx.cs" Inherits="qy_54tss_Admin_Shop_Foodimport" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜品导入-<%= WebUtility.GetMyName() %></title>
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

        $(window).load(function () { $("#loading-mask").hide(); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }
        $(document).ready(function () { WhitchActive(2); showContent(1); });

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

        function GotoFoodSort() {
            window.location.href = "FoodSortList.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function setDays() {

        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <div class="side-col" id="page:left">
                            <uc3:left runat="server" ID="left" />
                        </div>
                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>

                                <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none">
                                    <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" class="tab-item-link ">
                                        <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                    </li>
                                    <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                    <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link active"><span>
                                        <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                    <li><a href="Distancepaylist.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                    <li><a href="ShopLocal.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>

                                    <li><a href="AddPrinter.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>打印机</span> </a></li>

                                </ul>
                                <div style="visibility: visible;" class="content-header">
                                    <h3 class="icon-head head-customer">
                                        <asp:Label runat="server" ID="pageType">菜品导入导出</asp:Label></h3>

                                </div>
                                <!--start-->
                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-billing-address">菜品导入导出</h4>
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
                                                                                选择文件<span class="required">*</span></label>
                                                                            <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                            <asp:HiddenField runat="server" ID="hidDataId" />
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:FileUpload runat="server" ID="fuFoodExcel" />
                                                                            <a href="/upload/demo.xls" runat="server" id="a_downloadexcel">下载Excel模版</a>&nbsp;&nbsp;
                                                                        <asp:Button Text="菜单导入" runat="server" ID="tbinfood" OnClick="in_Click" class="button_1" OnClientClick="showloadfix('' , '' , 'tbinfood')" />&nbsp;&nbsp;
                                                                        <asp:Button Text="删除全部菜品" runat="server" ID="Button1" OnClick="del_Click" class="button_1" OnClientClick="return confirm('确认要删除吗？');" />

                                                                            <asp:Button Text="删除全部分类" runat="server" ID="Button2" OnClick="delsort_Click" class="button_1" OnClientClick="return confirm('确认要删除吗？');" />
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
                        </div>
                    </div>
                </div>
            </div>
            <uc2:Foot runat="server" ID="FootUC" />
        </div>
    </form>
</body>
</html>

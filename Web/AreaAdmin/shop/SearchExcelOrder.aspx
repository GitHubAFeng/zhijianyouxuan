<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchExcelOrder.aspx.cs"
    Inherits="AreaAdmin_Sale_SearchExcelOrder" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="EmailGsmRecordleft" TagPrefix="uc3" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单检索系统-<%= WebUtility.GetMyName()%></title>
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

    <script src="../javascript/TogoNameAutoCompelete.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function(){$("#loading-mask").hide();});
        AddLoadFun(init);

        var Table;
        function init() 
        {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function() {$(this).addClass("on-mouse");}).mouseout(function() {$(this).removeClass("on-mouse");});
            $(".grid_data tr:even").addClass("even pointer");  
            $("#loading-mask").hide();
        }
        
        function Del()
        {
            var nums = Table.GetChecks();
            if(nums == undefined || nums.length == 0)
            {
                alert("请选择要删除的订单!");
                return false;
            }            
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }   
        
        $(document).ready(function(){ WhitchActive(3)});    
    </script>

    <script language="javascript" type="text/javascript">
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
        $(document).ready(function(){ WhitchActive(12)});   
    </script>

    <script language="javascript" type="text/javascript">
        
        function GotoOrderList()
        {
            window.location.href = "OrderList.aspx";
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
                        <h3>
                            订单管理中心</h3>
                      <uc3:EmailGsmRecordleft ID="EmailGsmRecord1" runat="server" />
                    </div>
                    <div class="main-col" id="content">
                        <%--                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                        <div class="main-col-inner">
                            <div id="messages">
                                <ul class="messages">
                                    <li class="notice-msg">
                                        <ul>
                                            <li>此处提供两种查看检索结果的方式：此页面是导出为Excel格式，需要直接查看结果的请点击<a href="SearchOrder.aspx">进入直接查看结果的检索系统</a></li>
                                            <li></li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                            <div style="visibility: visible;" class="content-header">
                                <h3 class="icon-head head-customer">
                                    <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                    <p style="" class="content-buttons form-buttons">
                                        <button type="button" class="scalable back" onclick='GotoOrderList()' style="">
                                            <span>返回查看订单列表</span></button>
                                        <asp:Button ID="btExport" runat="server" CssClass="button_1" Text="导出Excel" OnClick="btExport_Click" OnClientClick="loading(); return true;" >
                                        </asp:Button>
                                    </p>
                                </div>
                            </div>
                            <!--start-->
                            <div class="entry-edit">
                                <div id="customer_info_tabs_account_content" style="">
                                    <div class="entry-edit">
                                        <div class="entry-edit-head">
                                            <h4 class="icon-head head-billing-address">
                                                检索信息</h4>
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
                                                                            餐馆名称<span class="required"></span></label>
                                                                        <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" ID="tbTogoName" name="invalue" Width="200px" autocomplete="off"
                                                                            onkeyup="suggestTogoName(event,this)" onkeydown="responseInput(event,this)" onkeypress="return event.keyCode==13?false:true"
                                                                            class=" required-entry required-entry input-text"></epc:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountfirstname">
                                                                            订单起始时间 <span class="required"></span>
                                                                        </label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" name="invalue" ID="tbStartTime" onfocus="WdatePicker({readOnly:true})"
                                                                            Width="200px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountfirstname">
                                                                            订单结束时间 <span class="required"></span>
                                                                        </label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" name="invalue" ID="tbEndTime" onfocus="WdatePicker({readOnly:true})"
                                                                            Width="200px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountfirstname">
                                                                            订单状态 <span class="required"></span>
                                                                        </label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <asp:DropDownList runat="server" ID="ddlOrderState" Style="width: 120px;">
                                                                            <asp:ListItem Text="未分发订单" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="已收订单" Value="10"></asp:ListItem>
                                                                            <asp:ListItem Text="已打印" Value="20"></asp:ListItem>
                                                                            <asp:ListItem Text="已配齐" Value="30"></asp:ListItem>
                                                                            <asp:ListItem Text="已配送" Value="40"></asp:ListItem>
                                                                            <asp:ListItem Text="已收款" Value="50"></asp:ListItem>
                                                                            <asp:ListItem Text="订单取消" Value="-10"></asp:ListItem>
                                                                            <asp:ListItem Text="已收取消订单" Value="-20"></asp:ListItem>
                                                                            <asp:ListItem Text="确认取消" Value="-30"></asp:ListItem>
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
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <table class="actions" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td class="export a-right">
                                                <img src="../Images/icon_export.gif" alt="" class="v-middle" />&nbsp; 导出到Excel:
                                                <asp:Button ID="btExport2" runat="server" CssClass="button_1" Text="导出Excel" OnClick="btExport_Click" OnClientClick="loading(); return true;" >
                                                </asp:Button>
                                            </td>
                                            <td class="filter-actions a-right">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
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

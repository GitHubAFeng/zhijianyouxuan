<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchOrder.aspx.cs" Inherits="AreaAdmin_shop_SearchOrder" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
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
        $(window).load(function() { $("#loading-mask").hide(); });

        var Table;

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的订单!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }
        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }

    </script>

    <script language="javascript" type="text/javascript">
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
        $(document).ready(function() { WhitchActive(12) });   
    </script>

    <script language="javascript" type="text/javascript">

        function GotoOrderList() {
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
                        <uc4:togoleft ID="togoleft1" runat="server" />
                    </div>
                    <div class="main-col" id="content">
                        <div class="main-col-inner">
                            <div style="visibility: visible;" class="content-header">
                                <h3 class="icon-head head-customer">
                                    <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                    <p style="" class="content-buttons form-buttons">
                                        <asp:Button ID="btShowRecord" runat="server" CssClass="button_1" Text="查看搜索结果" OnClientClick="loading(); return true;"
                                            OnClick="btShowRecord_Click1"></asp:Button>
                                        <asp:Button ID="btExport" runat="server" CssClass="button_1" Text="导出Excel" OnClick="btExport_Click">
                                        </asp:Button>
                                        <button type="button" class="scalable back" onclick='GotoOrderList()' style="">
                                            <span>查看订单列表</span></button>
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
                                                                            收餐人联系电话<span class="required"></span></label>
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" ID="tbTel" name="invalue" Width="200px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <small>&nbsp;</small>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="label">
                                                                        <label for="_accountprefix">
                                                                            餐馆名称<span class="required"></span></label>
                                                                        <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                    </td>
                                                                    <td class="value">
                                                                        <epc:TextBox runat="server" ID="tbTogoName" name="invalue" Width="200px" autocomplete="off"
                                                                          
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
                                                                            <asp:ListItem Text="所有订单" Value="-1"></asp:ListItem>
                                                                           
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
                                                <asp:Button ID="btExport2" runat="server" CssClass="button_1" Text="导出Excel" OnClick="btExport_Click">
                                                </asp:Button>
                                            </td>
                                            <td class="filter-actions a-right">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <!--检索结果-->
                            <div class="entry-edit">
                                <div id="Div1" style="">
                                    <div class="entry-edit">
                                        <div class="entry-edit-head">
                                            <h4 class="icon-head head-billing-address">
                                                检索结果</h4>
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <colgroup>
                                                        <col class="a-center" width="20px" />
                                                        <col width="30" />
                                                        <col width="80" />
                                                        <col />
                                                        <col width="120" />
                                                        <col width="50" />
                                                         <col width="80" />
                                                        <col width="30" />
                                                        <col width="70" />
                                                        <col width="80" />
                                                        <thead>
                                                            <tr class="headings">
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">订单编号(点击查看)</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">用户名</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">商家</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#" name="created_at">
                                                                        <!--sort-arrow-desc-->
                                                                        <span class="sort-title">订单时间</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">收件人</span></a></span>
                                                                </th>
                                                                 <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">电话</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">金额</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">状态</span></a></span>
                                                                </th>
                                                                <th class="no-link last">
                                                                    <span class="nobr">操作</span>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody class="grid_data">
                                                            <tr id="noRecord" runat="server" class="even">
                                                                <td class="empty-text a-center" colspan="10">
                                                                    暂无查询结果
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="rtpOrderlist" runat="server" OnItemCommand="rtpOrderlist_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr class="pointer" title="">
                                                                        <td class="" width="20px">
                                                                            <input id="_inut" class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("Unid")%>'>
                                                                                <asp:HiddenField ID="hidDataId" runat="server" Value='<%# Eval("Unid")%>' />
                                                                                <asp:HiddenField ID="hidOrderId" runat="server" Value='<%# Eval("Unid")%>' />
                                                                            </input>
                                                                        </td>
                                                                        <td class="" width="100px">
                                                                            <a href='OrderDetail.aspx?id=<%# Eval("Unid")%>'>
                                                                                <%#Eval("orderid")%></a>
                                                                        </td>
                                                                        <td class="">
                                                                            <%#Eval("CustomerName") %>
                                                                        </td>
                                                                        <td class="">
                                                                            <%#Eval("TogoName") %>
                                                                        </td>
                                                                        <td class="" width="60px">
                                                                            <%#Eval("OrderDateTime").ToString()%>
                                                                        </td>
                                                                        <td class="" width="60px">
                                                                            <%#Eval("OrderRcver")%>
                                                                        </td>
                                                                        <td >
                                                                            <%#Eval("OrderComm")%>
                                                                        </td>
                                                                        <td class="">
                                                                            <%#Eval("OrderSums")%>
                                                                        </td>
                                                                        <td class="" width="55px">
                                                                            <%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>
                                                                        </td>
                                                                        <td class=" last" width="70px">
                                                                            <a href='OrderDetail.aspx?id=<%#Eval("Unid") %>'>查看</a> &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </colgroup>
                                                </table>
                                            </div>
                                            <div class="scott">
                                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                    HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                    TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                                    PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                                    Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                                </webdiyer:AspNetPager>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--foot start-->
        <uc2:Foot runat="server" ID="FootUC" />
        <!--foot end-->
    </div>
    </form>
</body>
</html>

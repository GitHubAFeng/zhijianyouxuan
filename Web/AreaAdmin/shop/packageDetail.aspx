<%@ Page Language="C#" AutoEventWireup="true" CodeFile="packageDetail.aspx.cs" Inherits="qy_54tss_AreaAdmin_Shop_packageDetail" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>套餐信息管理--<%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css?v=1" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../javascript/autocomplete/jquery.autocomplete.css" rel="stylesheet" />

    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->

    <script src="../javascript/jquery-1.7.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidStyle" />
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <div class="wrapper">
            <!--banner start-->
            <uc1:TogoBanner runat="server" ID="Banner" />
            <!--banner end-->
            <!--center start-->
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <asp:HiddenField runat="server" ID="hidTogoId" />
                        <div class="main-col" id="content" style="margin-left: 0px; padding-left: 0;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="main-col-inner">
                                        <div id="dangqian" runat="server" style="padding: 10px; padding-left: 0; padding-top: 0;">
                                        </div>
                                        <div style="visibility: visible;" class="content-header">
                                            <h3 class="icon-head head-customer">
                                                <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                <p style="" class="content-buttons form-buttons">
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="GotoFoodSortList();return false"
                                                        Text="返回列表"></asp:Button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                        Text="保存" OnClientClick="return checkdata()"></asp:Button>
                                                </p>
                                            </div>
                                        </div>
                                        <!--start-->
                                        <div class="entry-edit">
                                            <div id="customer_info_tabs_account_content" style="">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">套餐信息</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="order-billing_address_fields">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <table class="form-list" cellspacing="0">
                                                                        <tbody>

                                                                            <tr>
                                                                                <th style="width: 30%">套餐名称
                                                                                </th>
                                                                                <th>供应时间
                                                                                </th>
                                                                                <th>原价
                                                                                </th>
                                                                                <th>套餐价
                                                                                </th>
                                                                                <th>本套餐数量
                                                                                </th>

                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbtitle" CanBeNull="必填" Width="90%" class=" j_text" reg="\S" tip="套餐名称不能为空"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbstarttime" Width="75" class="j_text"
                                                                                        onfocus="WdatePicker({readOnly:true,dateFmt:'HH:mm'})" CanBeNull="必填" reg="\S" tip="供应时间不能为空"></asp:TextBox>
                                                                                    ~
                                                                                    <asp:TextBox runat="server" ID="tbendtime" Width="75" class="j_text"
                                                                                        onfocus="WdatePicker({readOnly:true,dateFmt:'HH:mm'})" CanBeNull="必填" reg="\S" tip="供应时间不能为空"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tboldprice" CanBeNull="必填" Text="" RequiredFieldType="数据校验" reg="^[-+]?\d+(\.\d+)?$" tip="原价格式错误,请输入数字"
                                                                                        Width="60px" class=" j_text"></asp:TextBox>元
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbPrice" CanBeNull="必填" Text="" RequiredFieldType="数据校验" reg="^[-+]?\d+(\.\d+)?$" tip="套餐价格式错误,请输入数字"
                                                                                        Width="60px" class=" j_text"></asp:TextBox>元
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbNum" CanBeNull="必填" Text="100" RequiredFieldType="数据校验" reg="^\d+$" tip="套餐数量格式错误,请输入整数"
                                                                                        Width="60px" class=" j_text"></asp:TextBox>份

                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <div>
                                                                        <table class="form-list" cellspacing="0" style="width: 400px;" id="foodtable">

                                                                            <tr>
                                                                                <th style="width: 60%">菜式名称
                                                                                </th>
                                                                                <th>数量
                                                                                </th>
                                                                            </tr>

                                                                           <tr id="food_row_1">
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbfoodname1" reg="\S" tip="菜式名称不能为空" Width="200px" class=" j_text fooditem"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbfoodcount1" Text="1" reg="^\d+$" tip="数量格式错误,请输入整数" RequiredFieldType="数据校验"
                                                                                        Width="60px" class=" j_text" onblur="foodTable.delRow(1)"></asp:TextBox>份
                                                                                             <input type="button" value="删除" onclick="foodTable.delRow(1)" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="food_row_2">
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbfoodname2" reg="\S" tip="菜式名称不能为空" Width="200px" class=" j_text fooditem"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="tbfoodcount2" Text="1" reg="^\d+$" tip="数量格式错误,请输入整数" RequiredFieldType="数据校验"
                                                                                        Width="60px" class=" j_text" onclick="foodTable.getOldPrice()"></asp:TextBox>份
                                                                                             <input type="button" value="删除" onclick="foodTable.delRow(2)" />
                                                                                </td>
                                                                            </tr>

                                                                            <asp:Repeater runat="server" ID="rptfood">
                                                                                <ItemTemplate>

                                                                                    <tr id="food_row_<%#(Container.ItemIndex+1) %>" class="hasitem">
                                                                                        <td>
                                                                                            <input type="text" id="tbfoodname<%#(Container.ItemIndex+1) %>" reg="\S" value="<%# Eval("foodname") %>" tip="菜式名称不能为空" style="width: 200px;" class=" j_text fooditem" data_name="<%# Eval("foodname") %>" data_id="<%# Eval("fid") %>" data_price="<%# Eval("ReveVar") %>" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <input type="text" reg="\S" onclick="foodTable.getOldPrice()" tip="数量格式错误,请输入整数" style="width: 60px;" value="<%# Eval("foodcount") %>"  class=" j_text" id="tbfoodcount<%#(Container.ItemIndex+1) %>" />份<input type="button" value="删除" onclick="    foodTable.delRow(<%#(Container.ItemIndex+1) %>)" />
                                                                                        </td>
                                                                                    </tr>



                                                                                </ItemTemplate>
                                                                            </asp:Repeater>


                                                                        </table>

                                                                    </div>
                                                                    <div style="margin-top: 20px;">
                                                                        <input type="button" class="commonbutton" value="添加" onclick="foodTable.addrow()" />

                                                                    </div>

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
            <uc2:Foot runat="server" ID="FootUC" />
        </div>

        <script id="myfooditem" type="text/x-jsrender">
            <tr id="food_row_{{:index}}">
                <td>
                    <input type="text" reg="\S" tip="菜式名称不能为空" style="width: 200px;" class=" j_text fooditem" id="tbfoodname{{:index}}" />
                </td>
                <td>
                    <input type="text" reg="\S" tip="数量格式错误,请输入整数" onclick="foodTable.getOldPrice()" style="width: 60px;" value="1" class=" j_text" id="tbfoodcount{{:index}}" />份<input type="button" value="删除" onclick="    foodTable.delRow({{:index}})" />
                </td>
            </tr>
        </script>
    </form>
</body>
</html>

<script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>
<script src="../javascript/jsrender.js" type="text/javascript"></script>
<script src="../javascript/Common.js?v=1.1" type="text/javascript"></script>
<script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>
<script src="../javascript/autocomplete/jquery.autocomplete.js" type="text/javascript"></script>
<script src="../javascript/Package.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">

    $(window).load(function () { $("#loading-mask").hide(); });

    function GotoFoodSortList() {
        window.location.href = "packagelist.aspx?tid=" + document.getElementById("hidTogoId").value + "";
    }
</script>

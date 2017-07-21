<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodDetail.aspx.cs" Inherits="Admin_shop_F3oodDetail"
    ValidateRequest="false" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜品管理</title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="../javascript/Foodautocompelete.js" type="text/javascript"></script>

    <script src="../javascript/QueryChina.js" type="text/javascript"></script>

    <script src="/javascript/foodmanage.js" type="text/javascript"></script>
    <link href="../../javascript/jbox/Skins/jbox.css" rel="stylesheet" />



    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });
        $(document).ready(function () { WhitchActive(2); });

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

        function GotoFoodSort() {
            window.location.href = "FoodSortList.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function setDays() {

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
        <div id="doing-mask" style="display: none;">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="正在导入数据请勿关闭此页面..." /><br />
                正在导入数据请勿关闭此页面...
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
                                        <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                    <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                        <p style="" class="content-buttons form-buttons">
                                            <button type="button" class="scalable back" onclick='GotoFoodList()' style="">
                                                <span>返回列表</span></button>
                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                Text="保存"></asp:Button>
                                        </p>
                                    </div>
                                    <fieldset class="AdminSearchform">
                                        <legend>
                                            <asp:Label runat="server" ID="lbTogoName"></asp:Label></legend>
                                        <ul class="FunctionUl">
                                            <li>
                                                <button type="button" class="scalable " onclick="GotoFoodList()" style="">
                                                    <span>查看餐品列表</span></button></li>
                                            <li>
                                                <button type="button" class="scalable " onclick="GotoAddFoodSort()" style="">
                                                    <span>添加餐品分类</span></button></li>
                                            <li>
                                                <button type="button" class="scalable " onclick="GotoFoodSort()" style="">
                                                    <span>查看餐品分类</span></button></li>
                                            <li>
                                                <button type="button" class="scalable " onclick="gourl('Foodimport.aspx?tid=<%= Hangjing.Common.HjNetHelper.GetQueryInt("tid",0) %>    ')" style="">
                                                    <span>批量导入</span></button></li>
                                        </ul>
                                    </fieldset>
                                </div>

                                <!--start-->
                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">


                                        <div class="box-left">

                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">餐品信息</h4>
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
                                                                                    商家名称 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <span>
                                                                                    <asp:Label ID="lbtogo" runat="server"></asp:Label>
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountprefix">
                                                                                    餐品名称<span class="required">*</span></label>
                                                                                <asp:HiddenField runat="server" ID="hidTogoId" />


                                                                                <asp:HiddenField runat="server" ID="hidDataId" />


                                                                                <asp:HiddenField ID="HdUnid" runat="server" Value="" />
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbNamefx" name="invalue" onblur="queryfood()" Width="260"
                                                                                    class=" required-entry required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="display: none">
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    是否审核 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:RadioButtonList runat="server" ID="rblisauth" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                                                                                    <asp:ListItem Value="1" Text="已审核"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="未通过"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    餐品拼音 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <span>
                                                                                    <epc:TextBox runat="server" ID="tbFoodNamePy" name="invalue" Width="260" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                                </span>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    餐品类别 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddlFoodType" runat="server" Width="100px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    是否上架 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddlIsUse" runat="server" Width="50px">
                                                                                    <asp:ListItem Selected="True" Value="y">是</asp:ListItem>
                                                                                    <asp:ListItem Value="n">否</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="display: none;">
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    店家推荐 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:RadioButtonList runat="server" ID="rblfoodno" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="1">是</asp:ListItem>
                                                                                    <asp:ListItem Value="0">否</asp:ListItem>
                                                                                </asp:RadioButtonList>
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
                                                                                <epc:TextBox runat="server" name="invalue" Text="1000" ID="tbMaxPerDay" CanBeNull="必填" Width="60px" RequiredFieldType="正整数" class="required-entry input-text"></epc:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server" id="pricebox">
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    价格 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" name="invalue" Text="0" ID="tbPrice" CanBeNull="必填" Width="60px"
                                                                                    RequiredFieldType="数据校验" class=" required-entry required-entry input-text"></epc:TextBox>元
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>



                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    打包费 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" name="invalue" Text="0" ID="tbFullPrice" CanBeNull="必填" Width="60px"
                                                                                    RequiredFieldType="数据校验" class="required-entry input-text"></epc:TextBox>元
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
                                                                                <epc:TextBox runat="server" name="invalue" ID="tbOrderNum" CanBeNull="必填" Width="60px"
                                                                                    HintInfo="整数" RequiredFieldType="数据校验" class="required-entry input-text"
                                                                                    Text="0"></epc:TextBox><div class="mynotice">
                                                                                        数字大，排在前
                                                                                    </div>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="display: none">
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    是否推荐 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:RadioButtonList runat="server" ID="rblIsRecommend" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Value="0">否</asp:ListItem>
                                                                                    <asp:ListItem Value="1">是</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountfirstname">
                                                                                    图片 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                                <asp:HiddenField ID="FolderType" Value="1" runat="server" />
                                                                                <asp:HiddenField ID="WaterType" Value="1" runat="server" />
                                                                                <img border="0" src="../Images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 200px; height: 200px"
                                                                                    runat="server" /><br />
                                                                                <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" />请上传200*200的图片<br />
                                                                                <div id="rowTest" style="display: none">
                                                                                    <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 340px; height: 130px"
                                                                                        frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
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
                                                                                    介绍： <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:TextBox runat="server" ID="tbtaste" size="" class="input_on" TextMode="MultiLine"
                                                                                    Width="300px" Height="100px"></asp:TextBox>
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



                                        <div class="box-right">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">商品规格&nbsp;&nbsp;
                                                            <button type="button" id="btAddStyle" onclick="addsytle();" style="<%= Convert.ToInt32(SectionProxyData.GetSetValue(69)) == 1 ? "": "display:none"  %>">
                                                                添加规格</button></h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="">
                                                        <div class="grid">
                                                            <div class="hor-scroll">
                                                                <table class="data" cellspacing="0" id="grid_table">
                                                                    <colgroup>
                                                                        <col class="a-center" width="6%" />
                                                                        <col width="40%" />
                                                                        <col width="20%" />
                                                                        <col />
                                                                        <thead>
                                                                            <tr class="headings">
                                                                                <th>
                                                                                    <span class="nobr"><a class="not-sort" href="#"></a></span>
                                                                                </th>
                                                                                <th>
                                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">规格名称</span></a></span>
                                                                                </th>
                                                                                <th>
                                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">价格</span></a></span>
                                                                                </th>
                                                                                <th class="no-link last">
                                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">操作</span></a></span>
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody class="grid_data">
                                                                            <asp:Repeater ID="rptFoodlist" runat="server" OnItemCommand="rptFoodlist_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr class="pointer" title="">
                                                                                        <td class="" width="20px">
                                                                                            <input id="_inut" class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("dataid")%>'>
                                                                                                <asp:HiddenField ID="hidDataId" runat="server" Value='<%# Eval("DataId")%>' />
                                                                                            </input>
                                                                                        </td>
                                                                                        <td class="">
                                                                                            <%#Eval("Title")%>&nbsp;
                                                                                        </td>
                                                                                        <td class="">
                                                                                            <%#Eval("Price")%>&nbsp;
                                                                                        </td>
                                                                                        <td class="last">
                                                                                            <a href="javascript:edit(<%# Eval("dataid") %>)">编辑</a>

                                                                                            <span style="<%= Convert.ToInt32(SectionProxyData.GetSetValue(69)) == 1 ? "": "display:none"  %>">|
                                                                                                <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("DataId")%>' OnClientClick="return DelConfirm();"
                                                                                                    runat="server" ID="del">删除</asp:LinkButton>
                                                                                            </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </colgroup>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="order-save-in-address-book">
                                                            <label for="order-billing_address_save_in_address_book">
                                                            </label>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>

                                            <div class="entry-edit" style="<%= Convert.ToInt32(SectionProxyData.GetSetValue(70)) == 1 ? "": "display:none"  %>">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">商品属性&nbsp;&nbsp;
                                                            <button type="button" id="btAddAtt" onclick="addattr();">
                                                                增加属性</button></h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="Div1">
                                                        <div class="grid">
                                                            <div class="hor-scroll">
                                                                <table class="data" cellspacing="0" id="Table1">
                                                                    <colgroup>
                                                                        <col class="a-center" width="6%" />
                                                                        <col width="40%" />
                                                                        <col width="20%" />
                                                                        <col />
                                                                        <thead>
                                                                            <tr class="headings">
                                                                                <th>
                                                                                    <span class="nobr"><a class="not-sort" href="#"></a></span>
                                                                                </th>
                                                                                <th>
                                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">名称</span></a></span>
                                                                                </th>
                                                                                <th>
                                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">必选</span></a></span>
                                                                                </th>
                                                                                <th class="no-link last">
                                                                                    <span class="nobr"><a class="not-sort" href="#"><span class="sort-title">操作</span></a></span>
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody class="grid_data">
                                                                            <asp:Repeater ID="rptattr" runat="server" OnItemCommand="rptattr_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr class="pointer" title="">
                                                                                        <td class="" width="20px">
                                                                                            <input id="_inut" class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("dataid")%>'>
                                                                                                <asp:HiddenField ID="hidDataId" runat="server" Value='<%# Eval("DataId")%>' />
                                                                                            </input>
                                                                                        </td>
                                                                                        <td class="">
                                                                                            <%#Eval("Title")%>&nbsp;
                                                                                        </td>
                                                                                        <td class="">
                                                                                            <%#Eval("inve1").ToString() == "0" ? "否":"是"%>&nbsp;
                                                                                        </td>
                                                                                        <td class="last">
                                                                                            <a href="javascript:editattr(<%# Eval("dataid") %>)">编辑</a> |
                                                                                                <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("DataId")%>' OnClientClick="return DelConfirm();"
                                                                                                    runat="server" ID="del">删除</asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </colgroup>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="order-save-in-address-book">
                                                            <label for="order-billing_address_save_in_address_book">
                                                            </label>
                                                        </div>
                                                    </div>
                                                </fieldset>
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

<script src="../../javascript/jbox/jquery.jBox-2.3.min.js"></script>

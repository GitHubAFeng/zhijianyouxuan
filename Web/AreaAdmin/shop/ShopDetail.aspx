<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopDetail.aspx.cs" Inherits="AreaAdmin_Shop_ShopDetail"
    ValidateRequest="false" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register Src="buildSelect.ascx" TagName="build" TagPrefix="uc4" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家信息管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <link href="../css/time.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/togobuilding.js" type="text/javascript"></script>

    <script src="../javascript/getbuild.js" type="text/javascript"></script>

    <script src="../javascript/time.js" type="text/javascript"></script>

    <script src="../javascript/QueryChina.js" type="text/javascript"></script>




    <script language="javascript" type="text/javascript">

        //给一个输入框赋值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }
        $(document).ready(function () {
            WhitchActive(2);
            $("#loading-mask").hide();
            isPromotion();
        });

        function queryChinese() {
            var str = document.getElementById("tbName204234").value.trim();
            if (str == "") return;
            var arrRslt = makePy(str);
            var vaccount = $("#tbLoginName").val() + "";
            if (vaccount == "") {
                $("#tbLoginName").val(arrRslt[0]);
                // $("#tbPassword").val(arrRslt[0]);
            }
            $("#tbNamePy").val(arrRslt[0]);
        }


        function table_display2(t_id) {
            if (t_id == "table100" && document.getElementById("table100").style.display == "none") {
                document.getElementById("table100").style.display = "";

            }
            else {
                document.getElementById("table100").style.display = "none";
            }
        }


        function DeletePic(t_id) {
            document.getElementById("ppic" + t_id).src = "../../images/nopic_02.jpg";
            document.getElementById("pic" + t_id).value = "0";
        }

        function changecity() {
            var cid = $("#DDLArea").val();
            jQuery.ajax(
            {
                type: "post",
                url: "../../ajaxHandler.ashx",
                data: "method=changgecity&id=" + cid + "&t=" + new Date().getTime(),
                dataType: "json",
                error: function () { //失败
                    alert('Error loading document');
                },
                success: function (msg) {
                    parsexml(msg);
                    $("#DDLArea").val(cid);
                }
            })
        }
        function parsexml(smg) {
            document.getElementById("ddlsection").options.length = 0;
            if (typeof (smg) == "object" && typeof (smg.datalist) == "object") {
                var length = smg.datalist.length;
                alert(length);
                for (var i = 0; i < length; i++) {
                    var id = smg.datalist[i].id;
                    var name = smg.datalist[i].name;
                    $I("ddlsection").options.add(new Option(name, id));

                }
            }

        }

        function init() {
            hideload_super();
        }

        //佣金变化
        function isPromotion() {
            var snvalue = $("#ddlSN1").val();

            $("#SN1_sub0").hide();
            $("#SN1_sub1").hide();
            $("#SN1_sub" + snvalue).show();


        }
    </script>

    <style type="text/css">
        .form-list td.label {
            width: 110px;
        }
    </style>
    <style type="text/css">
        .tooltip {
            text-align: center;
            opacity: .70;
            -moz-opacity: .70;
            filter: Alpha(opacity=70);
            white-space: nowrap;
            margin: 0;
            padding: 2px 0.5ex;
            border: 1px solid #000;
            font-weight: bold;
            font-size: 9pt;
            font-family: Verdana;
            background-color: #fff;
        }

        #cbopentime label {
            float: left;
            margin-left: 5px;
        }

        #cbopentime input {
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
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
                                    <asp:HiddenField runat="server" ID="hfcatogray" Value="0" />
                                    <asp:HiddenField runat="server" ID="hfpingyin" Value="0" />
                                    <asp:HiddenField runat="server" ID="hfintime" />
                                    <div class="main-col-inner">
                                        <div id="divMessages">
                                        </div>

                                        <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none" runat="server">
                                            <li><a href="ShopDetail.aspx?id=<%= Request["id"] %>" id="diagram_tab_orders" class="tab-item-link active">
                                                <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                            </li>
                                            <li><a href="FoodSortList.aspx?tid=<%= Request["id"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                            <li><a href="FoodList.aspx?tid=<%= Request["id"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                            <li><a href="Distancepaylist.aspx?tid=<%= Request["id"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                            <li><a href="ShopLocal.aspx?tid=<%= Request["id"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>
                                            <li><a href="AddPrinter.aspx?tid=<%= Request["id"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>打印机</span> </a></li>

                                            <li><a href="settlecount.aspx?tid=<%= Request["id"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>商家结算帐号信息</span> </a></li>                                            
                                            
                                            <li><a href="qualification.aspx?tid=<%= Request["id"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>商家资质管理</span> </a></li>

                                        </ul>


                                        <div style="visibility: visible;" class="content-header">
                                            <h3 class="icon-head head-customer">
                                                <label runat="server" id="pageType">
                                                </label>
                                            </h3>
                                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                <p style="" class="content-buttons form-buttons">
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="history.go(-1);return false;"
                                                        Text="返回"></asp:Button>

                                                    <asp:Button ID="btcopy" runat="server" CssClass="button_1" Text="复制店铺" OnClick="copy_Click"
                                                        OnClientClick="showload_super();"></asp:Button>



                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" Text="保存" OnClick="btSave_Click1"
                                                        OnClientClick="showload_super();"></asp:Button>
                                                </p>
                                            </div>
                                        </div>
                                        <!--start-->
                                        <div id="order-addresses">
                                            <div class="box-left">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">商家基本信息(此区域内的输入框全部需要填写完整)</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="order-billing_address_fields">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <table class="form-list" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountwebsite_id">
                                                                                        商家名称：<span class="required">*</span></label>
                                                                                    <asp:HiddenField runat="server" ID="hidDataId" />
                                                                                    <asp:HiddenField runat="server" ID="hidPicture" />
                                                                                    <asp:HiddenField runat="server" ID="hidInTime" />
                                                                                    <asp:HiddenField runat="server" ID="hidIsDelete" />
                                                                                    <asp:HiddenField runat="server" ID="hidStatue" />
                                                                                    <asp:HiddenField runat="server" ID="hidPassword" />
                                                                                    <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbName204234" CanBeNull="必填" Width="190px" class="required-entry input-text"
                                                                                        onblur="queryChinese()"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        餐馆名拼音：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbNamePy" CanBeNull="必填" Width="190px" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        是否审核：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:RadioButtonList runat="server" ID="rblstart" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="已审核"></asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="审核不通过"></asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                            
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        自动接单：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:RadioButtonList runat="server" ID="rblRcvType" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                                                                       
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        热门商家：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlid" Width="80">
                                                                                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <div class=" notice">热门商家会在商家列表，“热门商家”一栏显示</div>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        标签：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:CheckBoxList runat="server" ID="cbopentime" RepeatDirection="Horizontal" RepeatColumns="1">
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        送饮料：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlshowpicture" Width="80">
                                                                                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        商家主营<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:CheckBoxList runat="server" ID="cblqq" RepeatDirection="Horizontal" RepeatColumns="3">
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>

                                                                            <tr style="display: none;">
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        配送半径：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbInve1" Width="60px" Text="0" CanBeNull="必填" RequiredFieldType="正整数"
                                                                                        MaxLength="4" class="required-entry input-text"></epc:TextBox>公里<div class=" mynotice">
                                                                                            输入整数
                                                                                        </div>
                                                                                </td>
                                                                            </tr>





                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        佣金：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">


                                                                                    <asp:DropDownList ID="ddlSN1" runat="server" onchange="isPromotion();">
                                                                                        <asp:ListItem Value="0">按百分比提成</asp:ListItem>
                                                                                        <asp:ListItem Value="1">按单收费</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                    <span style="" id="SN1_sub0">
                                                                                        <epc:TextBox runat="server" ID="tbPoint" RequiredFieldType="正整数" CanBeNull="必填" Width="60px" Text="0" class="required-entry input-text"></epc:TextBox>%
                                                                                    
                                                                                    </span>

                                                                                    <span style="display: none;" id="SN1_sub1">
                                                                                        <epc:TextBox runat="server" ID="tbPoint2" RequiredFieldType="正整数" CanBeNull="必填" Width="60px" Text="0" class="required-entry input-text"></epc:TextBox>元
                                                                                    
                                                                                    </span>


                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none">
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        账户余额：<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbMoney" Width="60px" Text="0" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        联系人：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbCommPerson" CanBeNull="必填" Width="190px" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        餐馆地址：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbAddress" CanBeNull="必填" Width="190px" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountwebsite_id">
                                                                                        营业时间一：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="TextBoxtime" Width="80px" onfocus="timeinit('TextBoxtime');"
                                                                                        class="equired-entry input-text" CanBeNull="必填" RequiredFieldType="营业时间"></epc:TextBox>至<epc:TextBox
                                                                                            runat="server" ID="TtimeEnd" Width="80px" onfocus="timeinit('TtimeEnd');" class="required-entry input-text"
                                                                                            CanBeNull="必填" RequiredFieldType="营业时间"></epc:TextBox><br />
                                                                                    <span>
                                                                                        <fon color="red">注意：请选择分钟，如：1:00</fon>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountwebsite_id">
                                                                                        营业时间二：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="TextBoxtime2" Width="80px" onfocus="timeinit('TextBoxtime2');"
                                                                                        class="equired-entry input-text" CanBeNull="必填" RequiredFieldType="营业时间"></epc:TextBox>至<epc:TextBox
                                                                                            runat="server" ID="TtimeEnd2" Width="80px" onfocus="timeinit('TtimeEnd2');" class="required-entry input-text"
                                                                                            CanBeNull="必填" RequiredFieldType="营业时间"></epc:TextBox><br />
                                                                                    <span>
                                                                                        <fon color="red">注意：请选择分钟，如：1:00，一个时间段此栏设置同上一栏</fon>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>


                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        配送时间：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" Text="0" ID="tbSentTime"
                                                                                        Width="60px" class="required-entry input-text" RequiredFieldType="正整数" CanBeNull="必填"></epc:TextBox>分钟(填写数字)
                                                                                </td>
                                                                            </tr>


                                                                           



                                                                            <tr style="display:none;">
                                                                                <td class="left_td" >
                                                                                    <label for="_accountprefix">
                                                                                        免配送费：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">餐品金额满<epc:TextBox runat="server" ID="tbPTimes" RequiredFieldType="正整数" CanBeNull="必填" Width="60px" Text="0" class="required-entry input-text"></epc:TextBox>
                                                                                    元，免配送费，0表示不启用
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        配送机构：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlSentOrg" Width="100px">
                                                                                        <asp:ListItem Value="0" Text="统一配送"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="商家自送"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountwebsite_id">
                                                                                        排序：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbSortNum" CanBeNull="必填" Width="60px" RequiredFieldType="正整数"
                                                                                        class=" required-entry required-entry input-text" Text="1"></epc:TextBox>
                                                                                    <div class="mynotice">
                                                                                        数字大的排在前
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountwebsite_id">
                                                                                        店铺状态：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlStatus" Width="100">
                                                                                        <asp:ListItem Text="正常营业" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="暂停营业" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountwebsite_id">
                                                                                        是否隐藏：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:CheckBox runat="server" ID="cbIsdelete" />
                                                                                    隐藏后前台将不会显示该商家
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        登录帐号：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbLoginName" Width="190px" class="required-entry input-text"
                                                                                        name="invalue"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountpassword">
                                                                                        登录密码：<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbPassword" class="required-entry input-text" Width="120px"></epc:TextBox><div
                                                                                        class="mynotice">
                                                                                        不修改密码则此输入框留空即可
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
                                            <div class="box-right">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head fieldset-legend head-billing-address">商家其他信息(此区域内的输入框根据实际情况填写)</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <table class="form-list" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        联系电话：<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbComm" Width="190px" class="required-entry input-text"
                                                                                        Text=""></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        商家邮箱：<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbemail" RequiredFieldType="电子邮箱" Width="190px" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        店铺活动：<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbspecial" Width="190px" MaxLength="128" TextMode="MultiLine" class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none">
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        浏览次数：<span class="required"></span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbViewTimes" Text="0" Width="80px" RequiredFieldType="数据校验"
                                                                                        class="required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="left_td">
                                                                                    <label for="_accountfirstname">
                                                                                        店铺图片：<span class="required"></span>
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value" style="width: 340px;">
                                                                                    <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                                    <asp:HiddenField ID="FolderType" Value="1" runat="server" />
                                                                                    <asp:HiddenField ID="WaterType" Value="1" runat="server" />

                                                                                    <img border="0" src="../Images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 316px; height: 248px"
                                                                                        runat="server" /><br />
                                                                                    <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" />
                                                                                    <label runat="server" id="picnotice">请上传320*220的图片</label>
                                                                                    <br />
                                                                                    <div id="rowTest" style="display: none">
                                                                                        <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 330px; height: 130px"
                                                                                            frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
                                                                                    </div>
                                                                                    <div id="Upload">
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none">
                                                                                <td class="left_td">
                                                                                    <label for="_accountprefix">
                                                                                        商家简介<span class="required"></span></label>
                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr style="display: none">
                                                                                <td class="value" colspan="2">
                                                                                    <FCKeditorV2:FCKeditor ID="tbIntroduce" runat="server" ToolbarSet="ShopDetail" BasePath="/Admin/fckeditor/"
                                                                                        Width="350px" Height="300px">
                                                                                    </FCKeditorV2:FCKeditor>
                                                                                </td>
                                                                            </tr>

                                                                        </tbody>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <!--foot start-->
            <uc2:Foot runat="server" ID="FootUC" />
            <uc4:build ID="Build1" runat="server" />
        </div>
        <!--新增完成后弹出的窗口-->
        <div id="divShowContent" style="display: none;">
            <ul class="divnotice">
                <li>
                    <button type="button" class="scalable " onclick="GotoAddFoodSort()" style="">
                        <span>为此商家添加餐品分类</span></button>
                </li>
                <li>
                    <button type="button" class="scalable " onclick="GotoAddFood()" style="">
                        <span>为此商家添加菜单</span></button>
                </li>
                <li><a href="ShopDetail.aspx">继续新增商家</a></li>
                <li><a href="ShopList.aspx">进入商家列表</a></li>
            </ul>
        </div>

        <script language="javascript" type="text/javascript">
            function GotoAddFood() {
                window.location.href = "FoodDetail.aspx?tid=" + document.getElementById("hidDataId").value + "";
            }

            function GotoAddPrinter() {
                window.location.href = "AddPrinter.aspx?tid=" + document.getElementById("hidDataId").value + "";
            }

            function GotoAddFoodSort() {
                window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidDataId").value + "";
            }

        </script>

        <div class="time_div" id="timediv" style="display: none">
            <div class="time_div_top">
            </div>
            <div class="time_div_con">
                <div style="clear: both; font-size: 12px; border-bottom: 1px solid #ccc; padding-left: 10px; padding-bottom: 5px;">
                    <h1 style="float: right; padding-right: 5px; cursor: pointer; font-size: 12px;" onclick="$('#timediv').hide()">关闭</h1>
                    请选择时间
                </div>
                <div class="con_left">
                    <h1>时
                    </h1>
                    <div style="border-right: 2px solid #CCC; height: 110px;">
                        <ul class="key_ul">
                            <li onclick="sethour(this)">0</li>
                            <li onclick="sethour(this)">1</li>
                            <li onclick="sethour(this)">2</li>
                            <li onclick="sethour(this)">3</li>
                            <li onclick="sethour(this)">4</li>
                            <li onclick="sethour(this)">5</li>
                            <li onclick="sethour(this)">6</li>
                            <li onclick="sethour(this)">7</li>
                            <li onclick="sethour(this)">8</li>
                            <li onclick="sethour(this)">9</li>
                            <li onclick="sethour(this)">10</li>
                            <li onclick="sethour(this)">11</li>
                            <li onclick="sethour(this)">12</li>
                            <li onclick="sethour(this)">13</li>
                            <li onclick="sethour(this)">14</li>
                            <li onclick="sethour(this)">15</li>
                            <li onclick="sethour(this)">16</li>
                            <li onclick="sethour(this)">17</li>
                            <li onclick="sethour(this)">18</li>
                            <li onclick="sethour(this)">19</li>
                            <li onclick="sethour(this)">20</li>
                            <li onclick="sethour(this)">21</li>
                            <li onclick="sethour(this)">22</li>
                            <li onclick="sethour(this)">23</li>
                        </ul>
                    </div>
                </div>
                <div class="con_right">
                    <h1>分</h1>
                    <ul class="key_ul" style="margin-left: 7px;">
                        <li onclick="setminute(this)">00</li>
                        <li onclick="setminute(this)">15</li>
                        <li onclick="setminute(this)">30</li>
                        <li onclick="setminute(this)">45</li>
                    </ul>
                </div>
            </div>
            <div class="time_div_bottom">
            </div>
        </div>
        <uc4:build runat="server" ID="build2" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliverDetail.aspx.cs" Inherits="qy_55tuan_AreaAdmin_DeliverDetail" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>配送员信息管理-<%=  WebUtility.GetMyName() %></title>
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
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
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
                                            <li><a id="diagram_tab_orders" class="tab-item-link active" href="javascript:">
                                                <span><span class="changed"></span><span class="error"></span>基本信息</span> </a>
                                            </li>

                                            <li><a href="deliverpath.aspx?tid=<%= Request["id"] %>&cityid=<%= Request["cityid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>配送轨迹</span> </a></li>

                                        </ul>




                                        <div style="visibility: visible;" class="content-header">
                                            <h3 class="icon-head head-customer">
                                                <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                <p style="" class="content-buttons form-buttons">
                                                    <button type="button" class="scalable back" onclick='GotoADList()' style="margin-bottom: 0; float: left; margin-right: 3px;">
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
                                                        <h4 class="icon-head head-billing-address">配送员轨迹</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="order-billing_address_fields">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <table class="form-list" cellspacing="0">
                                                                        <tbody>
                                                                            <%--<tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        群组 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList ID="ddlDeliverGroup" runat="server" Width="100">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>--%>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        姓名<span class="required">*</span></label>
                                                                                    <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                                    <asp:HiddenField runat="server" ID="hidDataId" />
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbName" CanBeNull="必填" Width="160px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>

                                                                                <td class="value" colspan="3">
                                                                                    <div class="notice">
                                                                                        “用户名”，“密码”用于登录骑士客户端
                                                                                    </div>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        用户名 <span class="required"></span>
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbUserName" Width="160px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        密码<span class="required"></span></label>
                                                                                    <asp:HiddenField runat="server" ID="HiddenField3" />
                                                                                    <asp:HiddenField runat="server" ID="HiddenField4" />
                                                                                    <asp:HiddenField runat="server" ID="hidPassword" />
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbpassword" Width="160px" class=" required-entry required-entry input-text"></epc:TextBox><div
                                                                                        class="mynotice">
                                                                                        如不修改密码留空即可
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        电话 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbPhone" CanBeNull="必填" Width="160px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>


                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        骑士状态 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlStatus" Width="100">
                                                                                        <asp:ListItem Value="1" Text="正常"></asp:ListItem>
                                                                                        <asp:ListItem Value="0" Text="离线"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        是否接单 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlIsWorking" Width="100">
                                                                                        <asp:ListItem Value="1" Text="正常接单"></asp:ListItem>
                                                                                        <asp:ListItem Value="0" Text="暂停接单"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="ddlApprovedState">
                                                                                        审核状态 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlApprovedState" Width="100">
                                                                                        <asp:ListItem Value="0" Text="审核通过"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="未审核"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        工资<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">每单<epc:TextBox runat="server" ID="tbCodeId" RequiredFieldType="数据校验" MinimumValue="0" CanBeNull="必填" Width="60px" class=" required-entry required-entry input-text"></epc:TextBox>元 + 配送费提成<epc:TextBox runat="server" ID="tbSection" RequiredFieldType="数据校验" MaximumValue="99" MinimumValue="0" CanBeNull="必填" Width="60px" class=" required-entry required-entry input-text"></epc:TextBox>%
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>


                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        身份证照片：<span class="required"></span>
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="3">
                                                                                    <img src="/Images/nopic_02.jpg" name="ppic1" id="ppic1" runat="server" style="width: 145px; height: 95px; float: left" />
                                                                                    <label>
                                                                                        <asp:HiddenField runat="server" ID="pic1" />
                                                                                        <input type="button" name="Submit" value="上传" onclick="table_display2('table100');" />
                                                                                        <input type="button" name="Submit2" onclick="DeletePic('1')" value="删除" />
                                                                                    </label>

                                                                                    <div class="mynotice">
                                                                                        请上传50*50的图片
                                                                                    </div>

                                                                                    <div class="mynotice">
                                                                                        <a href="javascript:" onclick="showpicstr('ppic1')" title="查看大图">点击查看大图</a>
                                                                                    </div>

                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table100" style="display: none">
                                                                                        <tr>
                                                                                            <td align="left">&nbsp;&nbsp;&nbsp;<iframe name="upload0" marginheight="0" marginwidth="0" frameborder="0"
                                                                                                scrolling="no" width="100%" height="115" bordercolor="#000000" src="/admin/upfile/UpFile.aspx?id=pic1&WaterType=0"></iframe>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
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
    </form>


    <!--查看大图-->
    <div style="text-align: center; display: none; font-size: 12px; font-family: Arial,Helvetica,sans-serif;" id="picstr">
        <img border="0" target="_blank" src="" />
    </div>
</body>
</html>

<script type="text/javascript">

    $(window).load(function () { $("#loading-mask").hide(); });


    function GotoADList() {
        var sid = Request("sid");
        window.location.href = "DeliverList.aspx";
    }

    //$(document).ready(function () {
    //    var id = request("cityid");
    //    if (id == "") {
    //        show_citytable();
    //    }

    //})

    //function select_mycity(name, id) {
    //    var url = "DeliverDetail.aspx?cityid=" + id + "&cname=" + escape(name);
    //    window.location = url;

    //}

    //function closediv() {
    //    alert("请先选择城市");
    //    return false;
    //}

    //上传图片操作
    function table_display2(t_id) {
        if ($("#" + t_id).css("display") == "none") {
            $("#" + t_id).attr("style", "display:");
        }
        else {
            $("#" + t_id).attr("style", "display:none");
        }
    }
    function DeletePic(t_id) {
        document.getElementById("ppic" + t_id).src = "/images/nopic_02.jpg";
        document.getElementById("pic" + t_id).value = "0";
    }
    //显示合同协议
    function showpicstr(ID) {
        var src = $("#" + ID).attr("src");
        var title = $("#" + ID).attr("title");
        $("#picstr").children("img").attr("src", src);
        tipsWindown(title, "id:picstr", '800', '800', 'true', '60000', 'true', 'text');
    }

</script>

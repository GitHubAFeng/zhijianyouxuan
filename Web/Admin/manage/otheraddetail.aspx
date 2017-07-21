<%@ Page Language="C#" AutoEventWireup="true" CodeFile="otheraddetail.aspx.cs" Inherits="Admin_AdDetailotheraddetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="Adleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>首页分类广告管理-<%= WebUtility.GetMyName()%></title>
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

        $(window).load(function () { $("#loading-mask").hide(); });
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

        function GotoBuildingDetail() {
            window.location.href = "BuildingDetail.aspx";
        }
        function GotoADList() {
            window.location.href = "otheradlist.aspx";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
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
                            <uc4:Adleft runat="server" />
                        </div>
                        <div class="main-col" id="content">
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
                                                    <button type="button" class="scalable back" onclick='GotoADList()' style="">
                                                        <span>返回列表</span></button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                        OnClientClick="showload_super();" Text="保存"></asp:Button>
                                                </p>
                                            </div>
                                        </div>
                                        <!--start-->
                                        <div class="entry-edit">
                                            <div id="customer_info_tabs_account_content" style="">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">广告信息</h4>
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
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbtitle" name="invalue" CanBeNull="必填" Width="200px"
                                                                                        class=" required-entry required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        排序<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbsortnum" name="invalue" RequiredFieldType="正整数" CanBeNull="必填" Width="60px"
                                                                                        Text="0" class=" required-entry required-entry input-text"></epc:TextBox> <div class="mynotice">
                                                                                        数字大的排在前
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>

                                                                            <tr style="display: none">
                                                                                <td class="label">
                                                                                    <label for="_accountwebsite_id">
                                                                                        是否可链接<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value" style="width: 600px">
                                                                                    <asp:DropDownList ID="islinked" runat="server" Width="120" class="j_select">
                                                                                        <asp:ListItem Value="0">是</asp:ListItem>
                                                                                        <asp:ListItem Value="1">否</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <div class="mynotice">
                                                                                        选择否,可不输入"链接地址"项
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        链接地址 <span class="required">*</span>
                                                                                    </label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" RequiredFieldType="网页地址" ID="tbAddress" name="invalue" CanBeNull="必填"
                                                                                        Width="300px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        广告图片 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value" style="width: 500px;">
                                                                                    <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                                    <asp:HiddenField ID="FolderType" Value="1" runat="server" />
                                                                                    <asp:HiddenField ID="WaterType" Value="0" runat="server" />

                                                                                    <img border="0" src="../Images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 316px; height: 373px;"
                                                                                        runat="server" /><br />
                                                                                    <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" /><label
                                                                                        runat="server" id="admsg"></label><br />
                                                                                    <div id="rowTest" style="display: none">
                                                                                        <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 500px; height: 130px"
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
                                                                                <td class="label">广告规格：
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox ID="tb_Width" runat="server" Width="30px" Text="316" RequiredFieldType="数据校验" CanBeNull="必填"></epc:TextBox>px
                                                                                *
                                                                                <epc:TextBox ID="tb_Length" runat="server" Width="30px" Text="373" RequiredFieldType="数据校验" CanBeNull="必填"></epc:TextBox>px
                                                                                    <div class=" mynotice">请不要随意修改此数字</div>
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

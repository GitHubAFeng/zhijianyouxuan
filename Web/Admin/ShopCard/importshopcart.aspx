<%@ Page Language="C#" AutoEventWireup="true" CodeFile="importshopcart.aspx.cs" Inherits="qy_54tss_Admin_Gifts_importshopcart" Async="true"
    ValidateRequest="false" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入优惠券-<%=  WebUtility.GetMyName() %></title>
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

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">

        function typechange() {
            var sid = document.getElementById("ddlReveInt").value;
            $(".carttypeitem").hide();
            $(".carttype" + sid).show();
        }

        $(document).ready(function () {
            typechange();
        });

    </script>


    <script language="javascript" type="text/javascript">



        function validate_d() {
            var mtype = $("#ddlReveInt").val();
            if (mtype == "1") {
                var tbpoint = $("#tbpoint").val();
                if (tbpoint == "") {
                    alert("请输入优惠券所含金额");
                    return false;
                }

            }
            else {
                var tbpoint = $("#tbmydiscount").val();
                if (tbpoint == "") {
                    alert("请输入优惠券所含折扣");
                    return false;
                }
            }
            if (document.getElementById("law1").checked) {
                var tbpoint = $("#tbmoneyline1").val();
                if (tbpoint == "") {
                    alert("请输入优惠券金额限制");
                    return false;
                }
            }
            if (document.getElementById("law2").checked) {
                var tbpoint = $("#tbstarttime1").val();
                if (tbpoint == "") {
                    alert("请输入优惠券开始时间");
                    return false;
                }
                tbpoint = $("#tbendtime1").val();
                if (tbpoint == "") {
                    alert("请输入优惠券结束时间");
                    return false;
                }
            }
            if (document.getElementById("law3").checked) {
                var tbpoint = $("#tbmoneyline3").val();
                if (tbpoint == "") {
                    alert("请输入优惠券金额限制");
                    return false;
                }
                tbpoint = $("#tbstarttime2").val();
                if (tbpoint == "") {
                    alert("请输入优惠券开始时间");
                    return false;
                }
                tbpoint = $("#tbendtime2").val();
                if (tbpoint == "") {
                    alert("请输入优惠券结束时间");
                    return false;
                }
            }

            showload_super();

            var flag = j_submitdata('form-list');
            return flag;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
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
                                <div style="visibility: visible;" class="content-header">
                                    <h3 class="icon-head head-customer">
                                        <asp:Label runat="server" ID="pageType">批量生成优惠券</asp:Label></h3>
                                    <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                        <p style="" class="content-buttons form-buttons">

                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                Text="保存" OnClientClick=" return validate_d();"></asp:Button>
                                        </p>
                                    </div>
                                </div>
                                <!--start-->
                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-billing-address">优惠券信息</h4>
                                            </div>
                                            <fieldset class="np">
                                                <div class="order-address" id="order-billing_address_fields">
                                                    <div class="content">
                                                        <div class="hor-scroll">
                                                            <table class="form-list" cellspacing="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <div class="mynotice" style="display: block;">
                                                                                优惠券生成后,请到优惠券管理中激活后再使用
                                                                            </div>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountprefix">
                                                                                批次名称<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value" style="width: 300px;">
                                                                            <asp:TextBox runat="server" ID="tbtitle" reg="^[^.]+$" tip="批次名称不能为空" Width="260"
                                                                                class=" j_text"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountfirstname">
                                                                                使用规则<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <div style="height: 30px; line-height: 30px">
                                                                                <input type="radio" name="uselaw" runat="server" id="law1" /><span>
                                                                                商品金额满<asp:TextBox runat="server" ID="tbmoneyline1" reg="^\d+$" tip="金额格式错误,没有请输入整数"
                                                                                    canbenull="y" Width="40" Text="0" class=" j_text"></asp:TextBox>元，优惠

                                                                                    <asp:TextBox runat="server" ID="tbpoint1" reg="^[-+]?\d+(\.\d+)?$" tip="所含金额格式错误,请输入整数或者小数"
                                                                                        canbenull="y" Width="40" class=" j_text"></asp:TextBox>元


                                                                                </span>





                                                                            </div>
                                                                            <div style="height: 30px; line-height: 30px; ">
                                                                                <input type="radio" name="uselaw" runat="server" id="law2" /><span>
                                                                               
                                                                                商品金额满<asp:TextBox runat="server" ID="tbmoneyline2" reg="^\d+$" tip="金额格式错误,没有请输入整数"
                                                                                    canbenull="y" Width="40" Text="0" class=" j_text"></asp:TextBox>元，享受

                                                                                    <asp:TextBox runat="server" ID="tbpoint2" reg="^[-+]?\d+(\.\d+)?$" tip="所含折扣格式错误,请输入整数或者小数"
                                                                                        canbenull="y" Width="40" class=" j_text"></asp:TextBox>折 
                                                                                    <div class="mynotice">
                                                                                        提示：8.8折，请输入88，以此类推。只对商品金额进行折扣
                                                                                    </div>
                                                                                </span>
                                                                            </div>
                                                                            <div style="height: 30px; line-height: 30px;display:none;">
                                                                                <input type="radio" name="uselaw" runat="server" id="law3" /><span>

                                                                                商品金额满<asp:TextBox runat="server" ID="tbmoneyline3" reg="^\d+$" tip="金额格式错误,没有请输入整数"
                                                                                    canbenull="y" Width="40" Text="0" class=" j_text"></asp:TextBox>元，享受

                                                                                    <asp:TextBox runat="server" ID="tbpoint3" reg="^[-+]?\d+(\.\d+)?$" tip="所含积分倍数格式错误,请输入整数或者小数"
                                                                                        canbenull="y" Width="40" class=" j_text"></asp:TextBox>倍积分  




                                                                                </span>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountfirstname">
                                                                                类型<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <asp:DropDownList runat="server" ID="ddlReveInt" onchange="typechange();">
                                                                                <asp:ListItem Value="1">积分兑换券</asp:ListItem>
                                                                                <asp:ListItem Value="2">电子优惠券</asp:ListItem>
                                                                            </asp:DropDownList>

                                                                            <div class=" notice" style="line-height: 25px; display:none;">提示：积分兑换券表示用户用积分在网站兑换;电子优惠券表示是直接发送到用户用户手机或者邮箱里的券，不在前台显示，这种券需要通过上传excel生成券号</div>

                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>


                                                                    <tr class="carttype1 carttypeitem">
                                                                        <td class="label">
                                                                            <label for="_accountprefix">
                                                                                可兑换张数<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value" style="width: 600px;">
                                                                            <asp:TextBox runat="server" ID="tbCardCount" reg="^\d+$" tip="生成优惠券张数格式错误,请输入整数"
                                                                                Width="60" Text="0" class=" j_text"></asp:TextBox>张<div class="mynotice">
                                                                                    提示：用户兑换后会相应变化
                                                                                </div>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>


                                                                    <tr class="carttype1 carttypeitem">
                                                                        <td class="label">
                                                                            <label for="_accountprefix">
                                                                                兑换积分<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value" style="width: 600px;">
                                                                            <asp:TextBox runat="server" ID="tbmydiscount" reg="^\d+$" tip="生成优惠券张数格式错误,请输入整数"
                                                                                Width="60" Text="0" class=" j_text"></asp:TextBox>分
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>

                                                                    <tr class="carttype2 carttypeitem" style="display: none;" runat="server" id="trexcel">
                                                                        <td class="label">
                                                                            <label for="_accountprefix">
                                                                                Excel文件<span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <asp:FileUpload runat="server" ID="fuFoodExcel" />
                                                                            <a href="demo.xls">下载模版</a>
                                                                            <div class="mynotice">
                                                                                选择要导入的券号excel
                                                                            </div>
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
                                                                        <td class="value" style="width: 500px;">
                                                                            <asp:TextBox runat="server" ID="tbsortnum" Text="0" Width="60" class=" j_text"></asp:TextBox><div
                                                                                class="mynotice">
                                                                                数字大,排序在前
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountfirstname">
                                                                                图片<span class="required"></span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                            <asp:HiddenField ID="FolderType" Value="1" runat="server" />
                                                                            <asp:HiddenField ID="WaterType" Value="0" runat="server" />
                                                                            <img border="0" src="../Images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 300px; height: 300px"
                                                                                runat="server" /><br />
                                                                            <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" />请上传300*300的图片<br />
                                                                            <div id="rowTest" style="display: none">
                                                                                <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 330px; height: 130px"
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
                                                                                说明 <span class="required"></span>
                                                                            </label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <FCKeditorV2:FCKeditor ID="fcContent" runat="server" ToolbarSet="Basic" BasePath="../fckeditor/"
                                                                                Value="" Height="300px" Width="650px">
                                                                            </FCKeditorV2:FCKeditor>
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
                        </div>
                    </div>
                </div>
            </div>
            <uc2:Foot runat="server" ID="FootUC" />
        </div>
    </form>
</body>
</html>

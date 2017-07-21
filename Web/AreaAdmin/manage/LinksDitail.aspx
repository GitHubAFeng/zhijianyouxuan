<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinksDitail.aspx.cs" Inherits="AreaAdmin_LinksDitail" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="Adleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>友情衔接-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />>
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function() { $("#loading-mask").hide(); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }
    </script>

    <script language="javascript" type="text/javascript">    
        function GetGo()
        {
          history.go(-1);
        }
  
    </script>

    <script type="text/javascript">
    function changep() {
            var pri = $("#ddltype").val();
            if (pri == "1") {
                $("#trpic").hide();
            }
            else {
                $("#trpic").show();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
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
                        <uc4:Adleft ID="Adleft1" runat="server" />
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
                                                <button type="button" class="scalable back" onclick='GetGo()' style="">
                                                    <span>返回列表</span></button>
                                                <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                    Text="保存"></asp:Button>
                                            </p>
                                        </div>
                                        <fieldset class="AdminSearchform">
                                            <legend>
                                                <asp:Label runat="server" ID="lbTogoName"></asp:Label></legend>
                                        </fieldset>
                                    </div>
                                    <!--start-->
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">
                                                        友情衔接</h4>
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address" id="order-billing_address_fields">
                                                        <div class="content">
                                                            <div class="hor-scroll">
                                                                <table class="form-list" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="3" class="hidden">
                                                                                <input id="_accountpassword_hash" name="account[password_hash]" value="" class=""
                                                                                    type="hidden">
                                                                            </td>
                                                                        </tr>
                                                                        <tr style=" display:none">
                                                                            <td class="label">
                                                                                <label for="_accountwebsite_id">
                                                                                    类型 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:DropDownList ID="ddltype" runat="server" Width="100px" onchange="changep();">
                                                                                    <asp:ListItem Value="1">文字连接</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Logo连接</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_acc">
                                                                                    标题<span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbtitle" CanBeNull="必填" Width="260" class=" required-entry required-entry input-text"></epc:TextBox>
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
                                                                                <epc:TextBox runat="server" ID="tbIntroduce" CanBeNull="必填" Text="0" Width="60" class=" required-entry required-entry input-text"></epc:TextBox><div class="mynotice">提示：数字大，排在前。</div>
                                                                            </td>
                                                                            <td>
                                                                                <small>&nbsp;</small>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label">
                                                                                <label for="_accountpassword">
                                                                                    衔接地址 <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <epc:TextBox runat="server" ID="tbURL" RequiredFieldType="网页地址" Text="http://" class=" required-entry required-entry input-text"></epc:TextBox><br />
                                                                                <label runat="server" id="lbNotice">
                                                                                </label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="trpic" style="display: none" runat="server">
                                                                            <td  class="label">
                                                                                <label for="_accountpassword">
                                                                                    Logo <span class="required">*</span></label>
                                                                            </td>
                                                                            <td class="value">
                                                                                <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                                <asp:HiddenField ID="FolderType" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="WaterType" runat="server" Value="0" />
                                                                                &nbsp; <a href="" id="txturldz" target="_blank">
                                                                                    <img border="0" src="../Admin/images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 90px;
                                                                                        height: 40px" runat="server" /></a><br />
                                                                                <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display='block';" />请上传88*31的图片<br />
                                                                                <div id="rowTest" style="display: none">
                                                                                    <iframe name="tag" src="../UpFile/Upload.html?Links" style="width: 550px; height: 130px"
                                                                                        frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight">
                                                                                    </iframe>
                                                                                </div>
                                                                                <div id="Upload">
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

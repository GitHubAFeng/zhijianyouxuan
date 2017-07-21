<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addFoodStyle.aspx.cs" Inherits="AreaAdmin_addFoodStyle" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品规格信息管理</title>
    <link href="/Admin/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/menu.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/Common.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/building.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="/Admin/css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="/Admin/css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="/Admin/css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/easyajaxcore.js" type="text/javascript"></script>

    <script src="../javascript/Building.js" type="text/javascript"></script>

    <script src="../javascript/Foodautocompelete.js" type="text/javascript"></script>

    <style type="text/css">
        body, html
        {
            background-color: #fff;
        }
        .value
        {
          text-align:left;    
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hffid" Value="0" />
    <asp:HiddenField runat="server" ID="hftid" Value="0" />
    <asp:HiddenField runat="server" ID="hfdid" Value="0" />
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
    <div id="content" style="width: 600px; margin-left: 0px; 
        padding-left: 10px;">
        <div class="content">
            <div class="hor-scroll">
                <table class="form-list" cellspacing="0">
                    <tbody>
                        <tr>
                            <td class="label">
                                <label for="_accountprefix">
                                    商品名称<span class="required">*</span></label>
                                <asp:HiddenField runat="server" ID="hidTogoId" />
                                <asp:HiddenField runat="server" ID="hidDataId" />
                            </td>
                            <td class="value">
                                <asp:Label ID="Lbfoodname" runat="server"></asp:Label>
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
                                <asp:Label ID="Lbfoodtype" runat="server"></asp:Label>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label for="_accountfirstname">
                                    商品规格 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <span>
                                    <epc:TextBox runat="server" name="invalue" ID="tbtitle" Width="100px" class=" required-entry required-entry input-text"></epc:TextBox>&nbsp;<div
                                        class="mynotice">
                                        只有一个规格可不填写</div>
                                </span>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label for="_accountfirstname">
                                    价格 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <epc:TextBox runat="server" name="invalue" ID="tbPrice" RequiredFieldType="数据校验"
                                    CanBeNull="必填" Width="60px" class=" required-entry required-entry input-text"></epc:TextBox>元
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <label for="_accountfirstname">
                                    市场价格 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <epc:TextBox runat="server" name="invalue" ID="tbMarkeyPrice" CanBeNull="必填" Width="60px"
                                    RequiredFieldType="数据校验" Text="0" class=" required-entry required-entry input-text"></epc:TextBox>元
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <label for="_accountfirstname">
                                    库存 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <epc:TextBox runat="server" name="invalue" ID="tbSaleSum" CanBeNull="必填" Width="60px"
                                    HintInfo="整数" RequiredFieldType="数据校验" class=" required-entry required-entry input-text"
                                    Text="0"></epc:TextBox>元
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <label for="_accountfirstname">
                                    每日供给量 <span class="required">*</span></label>
                            </td>
                            <td>
                                <epc:TextBox runat="server" name="invalue" ID="tbMaxPerDay" CanBeNull="必填" Width="60px"
                                    RequiredFieldType="数据校验" class=" required-entry required-entry input-text" Text="10"></epc:TextBox>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <label for="_accountfirstname">
                                    状态 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <asp:DropDownList ID="DropIsUse" runat="server" Width="80px">
                                    <asp:ListItem Value="1">上架</asp:ListItem>
                                    <asp:ListItem Value="0">下架</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <small>&nbsp;</small>
                            </td>
                            <td class="value">
                                <asp:Button ID="btSave" runat="server" CssClass="commonbutton" OnClick="btSave_Click"
                                    Text="保存"></asp:Button>
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
    </form>
</body>
</html>

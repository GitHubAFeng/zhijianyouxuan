<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDetail.aspx.cs" Inherits="Admin_User_UserDetail" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>注册会员积分管理-<%= WebUtility.GetMyName()%></title>
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

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
        AddLoadFun(init);

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的会员!");
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

        function GotoUserList() {
            window.location.href = 'UserList.aspx';
        }

        function getData() {
            var selecttype = document.getElementById("sttype").value;
            var _value = $I("tbUsername").value;
            var _type;
            if (selecttype == '1') {
                _type = "uid";
            }
            else {
                _type = "nike";
            }
            jQuery.ajax({
                type: "post",
                url: "../../Ajax/CheckUser.aspx",
                async: true,
                data: "type=" + _type + "&value=" + _value + "",
                success: function (msg) {
                    if (msg == 'n') {
                        jQuery("#lbCurrentPoint").html("");
                        alert('没有此用户');
                    }
                    else {
                        jQuery("#lbCurrentPoint").html(msg);
                    }
                }

            });
        }
        function $I(eid) {
            return document.getElementById(eid);
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
                                <div id="divMessages">
                                </div>


                                <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none">
                                    <li><a href="UserDetail.aspx?id=<%= Request["id"] %>" class="tab-item-link  active">
                                        <span><span class="changed"></span><span class="error"></span>用户信息</span> </a>
                                    </li>
                                    <li><a href="UserAddMoneyLog.aspx?uid=<%= Request["id"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>余额账户</span> </a></li>
                                    <li><a href="UserDistributionList.aspx?uid=<%= Request["id"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>分销账户</span> </a></li>
                                   

                                     <li><a href="userdrawcashcount.aspx?id=<%= Request["id"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>提现账户</span> </a></li>




                                    
                                     <li><a href="UserPoint.aspx?id=<%= Request["id"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>积分记录</span> </a></li>



                                </ul>



                                <div id="order-addresses">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="main-col-inner">




                                                <div style="visibility: visible;" class="content-header">
                                                    <h3 class="icon-head head-customer" runat="server" id="h3content">用户信息</h3>
                                                    <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                        <p style="" class="content-buttons form-buttons">
                                                            <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="GotoUserList();return false;"
                                                                Text="返回"></asp:Button>

                                                       
                                                        </p>
                                                    </div>
                                                </div>


                                                <!--start-->
                                                <div class="entry-edit">
                                                    <div id="customer_info_tabs_account_content" style="">
                                                        <div class="entry-edit">
                                                            <div class="entry-edit-head">
                                                                <h4 class="icon-head head-billing-address">用户详细信息</h4>
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
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountwebsite_id">
                                                                                                昵称：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbnikename">
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                真实姓名：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbTruename">
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                手机号码：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbPhone">
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                QQ：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbQQ">
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                E-mail：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbEmail">
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                积分</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label runat="server" id="tbpoint"></label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                性别：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbsex">
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                生日：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbbirthday">
                                                                                            </label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <small>&nbsp;</small>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="label">
                                                                                            <label for="_accountprefix">
                                                                                                注册时间：</label>
                                                                                        </td>
                                                                                        <td class="value">
                                                                                            <label for="_accountprefix" runat="server" id="lbRegtime">
                                                                                            </label>
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
                                                <div class="entry-edit">
                                                    <div id="Div2" style="">
                                                        <div class="entry-edit">
                                                            <div class="entry-edit-head">
                                                                <h4 class="icon-head head-billing-address">用户地址</h4>
                                                            </div>
                                                            <fieldset class="np">
                                                                <div class="order-address" id="Div3">
                                                                    <div class="content">
                                                                        <div class="hor-scroll">
                                                                            <asp:Repeater runat="server" ID="rtpAddressList" OnItemCommand="orderfood_Command">
                                                                                <ItemTemplate>
                                                                                    <table class="form-list" cellspacing="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td colspan="3" class="hidden">
                                                                                                    <input id="Hidden1" name="account[password_hash]" value="" class="" type="hidden">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label">
                                                                                                    <label for="_accountwebsite_id">
                                                                                                        收货人
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="value">
                                                                                                    <label for="_accountprefix" runat="server" id="Label1">
                                                                                                        <%#Eval("receiver")%>
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <small>&nbsp;</small>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label">
                                                                                                    <label for="_accountprefix">
                                                                                                        称呼</label>
                                                                                                </td>
                                                                                                <td class="value">
                                                                                                    <label for="_accountprefix" runat="server" id="Label4">
                                                                                                        <%#Eval("Phone")%>
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <small>&nbsp;</small>
                                                                                                </td>
                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td class="label">
                                                                                                    <label for="_accountprefix">
                                                                                                        详细地址</label>
                                                                                                </td>
                                                                                                <td class="value" style="width: 600px;">
                                                                                                    <%# Eval("address") %>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <small>&nbsp;</small>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="label">
                                                                                                    <label for="_accountprefix">
                                                                                                        手机</label>
                                                                                                </td>
                                                                                                <td class="value">
                                                                                                    <label for="_accountprefix" runat="server" id="Label3">
                                                                                                        <%# Eval("mobilephone") %>
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <small>&nbsp;</small>
                                                                                                </td>
                                                                                            </tr>

                                                                                        </tbody>
                                                                                    </table>
                                                                                    <hr />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <textarea runat="server" id="ttorderInof" style="width: 400px; height: 200px; display: none"></textarea>
            <!--foot start-->
            <uc2:Foot runat="server" ID="FootUC" />
            <!--foot end-->
        </div>
    </form>
</body>
</html>

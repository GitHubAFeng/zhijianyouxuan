<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qualification.aspx.cs" Inherits="Admin_shop_qualification" %>


<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家资质管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />

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


    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            WhitchActive(2);
            $("#loading-mask").hide();
        });

        function table_display2(t_id) {
            if ($("#" + t_id).css("display") == "none") {
                $("#" + t_id).attr("style", "display:");
            }
            else {
                $("#" + t_id).attr("style", "display:none");
            }
        }

        function DeletePic(t_id) {
            document.getElementById("ppic" + t_id).src = "../../images/nopic_02.jpg";
            document.getElementById("pic" + t_id).value = "0";
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
                                            <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" class="tab-item-link ">
                                                <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                            </li>
                                            <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                            <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                            <li><a href="Distancepaylist.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                            <li><a href="ShopLocal.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>

                                            <li><a href="AddPrinter.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>打印机</span> </a></li>

                                            <li><a href="settlecount.aspx?tid=<%= Request["tid"] %>" class="tab-item-link"><span>
                                                <span class="changed"></span><span class="error"></span>商家结算帐号信息</span> </a></li>

                                            <li><a href="qualification.aspx?tid=<%= Request["id"] %>" class="tab-item-link active"><span>
                                                <span class="changed"></span><span class="error"></span>商家资质管理</span> </a></li>
                                        </ul>


                                        <div style="visibility: visible;" class="content-header">
                                            <h3 class="icon-head head-customer">
                                                <label runat="server" id="pageType">
                                                    商家资质管理
                                                </label>
                                            </h3>
                                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                <p style="" class="content-buttons form-buttons">
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="history.go(-1);return false;"
                                                        Text="返回"></asp:Button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" Text="保存" OnClick="btSave_Click"
                                                        OnClientClick="showload_super();"></asp:Button>
                                                </p>
                                            </div>
                                        </div>
                                        <!--start-->
                                        <div id="order-addresses">
                                            <div class="">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">营业执照</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <div>
                                                                        是否显示：
                                                                        <asp:DropDownList ID="ddllicense" runat="server">
                                                                            <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <img src="/Images/nopic_02.jpg" name="ppic1" style="width: 280px; height: 250px; float: left" runat="server" id="ppic1" />
                                                                    <label>
                                                                        <asp:HiddenField runat="server" ID="pic1" />
                                                                        <input type="button" name="Submit" value="上传" onclick="table_display2('table100');" />
                                                                        <input type="button" name="Submit2" onclick="DeletePic('1')" value="删除" />
                                                                    </label>
                                                                    <div class="mynotice">
                                                                        请上传280*250的图片
                                                                    </div>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table100" style="display: none">
                                                                        <tr>
                                                                            <td align="left">&nbsp;&nbsp;&nbsp;<iframe name="upload0" marginheight="0" marginwidth="0" frameborder="0"
                                                                                scrolling="no" width="100%" height="115" bordercolor="#000000" src="../../admin/upfile/UpFile.aspx?id=pic1&WaterType=1"></iframe>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">餐饮服务许可证</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <div>
                                                                        是否显示：
                                                                        <asp:DropDownList ID="ddlcatering" runat="server">
                                                                            <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <img src="/Images/nopic_02.jpg" name="ppic2" style="width: 280px; height: 250px; float: left" runat="server" id="ppic2" />
                                                                    <label>
                                                                        <asp:HiddenField runat="server" ID="pic2" />
                                                                        <input type="button" name="Submit" value="上传" onclick="table_display2('table101');" />
                                                                        <input type="button" name="Submit2" onclick="DeletePic('2')" value="删除" />
                                                                    </label>
                                                                    <div class="mynotice">
                                                                        请上传280*250的图片
                                                                    </div>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table101" style="display: none">
                                                                        <tr>
                                                                            <td align="left">&nbsp;&nbsp;&nbsp;<iframe name="upload0" marginheight="0" marginwidth="0" frameborder="0"
                                                                                scrolling="no" width="100%" height="115" bordercolor="#000000" src="../../admin/upfile/UpFile.aspx?id=pic2&WaterType=1"></iframe>

                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
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
        </div>
    </form>
</body>
</html>

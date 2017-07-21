<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodSortDetail.aspx.cs" Inherits="shop_FoodSortDetail" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>餐品类别详情-<%= WebUtility.GetWebName()%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

</head>

<body>
    <form id="form1" runat="server">
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <epc:hint id="Hint1" runat="server" hintimageurl="../images/Control" />
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="shop_main">
                                    <div class="main-content">
                                        <div class="shop_menu">
                                            <ul>
                                                <li><a href="FoodSortList.aspx?">餐品类别列表</a></li>
                                                <li class="cur"><a href="FoodSortDetail.aspx">添加餐品类别</a></li>
                                                <li><a href="FoodList.aspx">餐品列表</a></li>
                                                <li><a href="FoodDetail.aspx">添加餐品</a></li>
                                                <li><a href="Foodimport.aspx">批量导入</a></li>

                                            </ul>
                                        </div>
                                        <h1 class="topbg">
                                            <asp:Label ID="lbTitle" runat="server" Text="添加餐品类别"></asp:Label></h1>
                                        <div class="usermima">
                                            <ul>
                                                <li><span class="left_span" style="width: 85px;">餐品类别名称：</span>
                                                    <epc:textbox runat="server" canbenull="必填" id="tbSortName" size="25" class="input_on"
                                                        maxlength="30"></epc:textbox>
                                                </li>
                                                <li><span class="left_span" style="width: 85px;">餐品类别排序：</span>
                                                    <epc:textbox runat="server" id="tbJorder" canbenull="必填" requiredfieldtype="数据校验"
                                                        width="60px" class=" required-entry required-entry input-text"></epc:textbox>
                                                </li>
                                                <li style="padding-left: 75px; padding-top: 10px;">
                                                    <asp:Button Text="确定" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn" />
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

            </div>
        </div>
    </form>
</body>
</html>
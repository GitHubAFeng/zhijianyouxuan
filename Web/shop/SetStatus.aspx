<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetStatus.aspx.cs" Inherits="shop_SetStatus" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家基本信息-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Admin/javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../Admin/javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/time.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        function table_display2(t_id) {
            if (t_id == "table100" && document.getElementById("table100").style.display == "none") {
                document.getElementById("table100").style.display = "";
                document.getElementById("table101").style.display = "none"
            }
            else {
                document.getElementById("table100").style.display = "none";
                document.getElementById("table101").style.display = "";
            }
        }

        function DeletePic(t_id) {
            document.getElementById("ppic" + t_id).src = "../../images/nopic.jpg";
            document.getElementById("pic1" + t_id).value = "0";
        }

    </script>
</head>

<body>
    <form id="form1" runat="server">

        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="shop_main">
                                <div class=" main-content ">
                                    <%-- <h1 class="topbg">营业状态管理</h1>--%>
                                    <div class="shop_menu">
                                        <ul>
                                            <li><a href="myshop.aspx">修改资料</a></li>
                                            <li><a href="TogoLocal.aspx">商家定位</a></li>
                                            <li class="cur"><a href="SetStatus.aspx">状态管理</a></li>
                                            <li><a href="myPromotion.aspx">促销活动</a></li>
                                            <li><a href="qualification.aspx">商家资质管理</a></li>
                                        </ul>
                                    </div>
                                    <div class="usermima">
                                        <ul>
                                            <li><span class="left_span">店铺状态：</span>
                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="sele" Width="90">
                                                    <asp:ListItem Text="正常营业" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="暂停营业" Value="0"></asp:ListItem>
                                                    <%-- <asp:ListItem Text="休息中" Value="-1"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </li>
                                            <li style="display: none"><span class="left_span">提示信息：</span>
                                                <epc:TextBox runat="server" ID="tbAlert" class="input_on"></epc:TextBox>本信息为暂停业时给订餐者的提示信息</li>
                                            <li style="padding-left: 100px; padding-top: 10px;">
                                                <asp:Button Text="保存信息" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn" />
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

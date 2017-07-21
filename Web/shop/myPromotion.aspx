<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myPromotion.aspx.cs" Inherits="shop_myPromotion" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>促销活动-<%= WebUtility.GetWebName()%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/QueryChina.js" type="text/javascript"></script>

    <script type="text/javascript">
        function queryfood() {
            var str = document.getElementById("tbName").value.trim();
            if (str == "") return;
            var arrRslt = makePy(str);
            $("#tbFoodNamePy").val(arrRslt[0]);
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
                                <div class="main-content">
                                    <div class="shop_menu">
                                        <ul>
                                            <li><a href="myshop.aspx">修改资料</a></li>
                                            <li><a href="TogoLocal.aspx">商家定位</a></li>
                                            <li><a href="SetStatus.aspx">状态管理</a></li>
                                            <li class="cur"><a href="myPromotion.aspx">促销活动</a></li>
                                            <li><a href="qualification.aspx">商家资质管理</a></li>
                                        </ul>
                                    </div>

                                    <div class="usermima">
                                        <div style="border-bottom: solid 1px #ccc; padding-bottom: 5px; margin-bottom: 10px; padding-left: 15px;">
                                            <h3>促销类型</h3>
                                        </div>

                                        <ul>
                                            <li><span class="left_span">&nbsp;</span>

                                                <asp:RadioButtonList runat="server" ID="rblshopptype" Enabled="false" RepeatDirection="Horizontal" onchange="shoppromotionchange()">
                                                    <asp:ListItem Value="0">无</asp:ListItem>
                                                    <asp:ListItem Value="10">商家独立促销</asp:ListItem>
                                                    <asp:ListItem Value="20">平台促销</asp:ListItem>

                                                </asp:RadioButtonList>
                                            </li>
                                            <li runat="server" id="webpromotionbox"><span class="left_span">&nbsp;</span>
                                                <asp:CheckBoxList runat="server" ID="tbPEnd" RepeatDirection="Horizontal" RepeatColumns="1" Enabled="false">
                                                </asp:CheckBoxList>
                                            </li>
                                        </ul>

                                        <div runat="server" id="promotionbox">
                                            <div style="border-bottom: solid 1px #ccc; padding-bottom: 5px; margin-bottom: 10px; padding-left: 15px;">
                                                <h3>我的促销</h3>
                                            </div>

                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                                style="border: 1px solid #ccc;">

                                                <tr>
                                                    <th>标题
                                                    </th>
                                                    <th style="width: 25%;">有效期
                                                    </th>
                                                    <th style="width: 12%">状态
                                                    </th>

                                                </tr>
                                                <asp:Repeater ID="rptFoodList" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <%#Eval("revevar1")%>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%# Convert.ToDateTime(Eval("startdate")).ToShortDateString()%>~<%# Convert.ToDateTime(Eval("enddate")).ToShortDateString()%>
                                                            </td>
                                                            <td>
                                                                <%#Convert.ToInt32(Eval("isopen")) == 1 ? "开启" : "关闭"%>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>

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
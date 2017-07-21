<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gradefavourable.aspx.cs"
    Inherits="Admin_Permission_gradefavourable" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>等级优惠-<%= WebUtility.GetMyName()%></title>
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/left.css" rel="stylesheet" type="text/css" />
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/usergrade.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
    </script>

    <script type="text/javascript">
        AddLoadFun(init);

        var Table;

        function init() {
            $(document).ready(function() {
                $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
                $(".grid_data tr:even").addClass("even pointer");

            });
        }

    </script>

    <style type="text/css">
        .data .datatd
        {
            line-height: 22px;
        }
        .trhead
        {
            text-align: center;
        }
        .grid table td.last
        {
            text-align: left;
        }
        .moduleitem span
        {
            width: 120px;
            display: inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
    <asp:HiddenField runat="server" ID="hidhas" />
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <!--banner end-->
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
                        <uc3:left ID="Adleft1" runat="server" />
                    </div>
                    <div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <div class="main-col-inner">
                            <div id="divMessages">
                            </div>
                            <ul id="diagram_tab" class="tabs-horiz">
                                <li><a href="uservipgradelist.aspx" id="diagram_tab_orders" title="用户等级" class="tab-item-link ">
                                    <span><span class="changed"></span><span class="error"></span>用户等级</span> </a>
                                </li>
                                <li><a href="gradefavourable.aspx" id="A1" title="等级优惠" class="tab-item-link active">
                                    <span><span class="changed"></span><span class="error"></span>等级优惠</span> </a>
                                </li>
                            </ul>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div style="visibility: visible;" class="content-header">
                                        <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                            <p style="" class="content-buttons form-buttons">
                                                <asp:Button ID="btSave" runat="server" CssClass="button_1" Text="保存" OnClientClick="return rp_save();"
                                                    OnClick="sava_Click"></asp:Button>
                                            </p>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="sales_order_grid_massaction" style="clear: both;">
                                <div style="font-size: 14px; color: Red;">
                                    提示：折扣请输入数字，10表示不折扣，9折请输入9，88折请输入8.8;积分倍数请输入数字</div>
                            </div>
                            <div id="Div1" style="clear: both; margin-top: 10px;">
                                <div class="hshop_class" align="center">
                                    <label runat="server" id="lbhead" style="font-size: 14px; color: #EA7601;">
                                        等级优惠管理
                                    </label>
                                </div>
                            </div>
                            <div class="grid">
                                <div class="hor-scroll" style="overflow: auto; height: auto">
                                    <table class="data" cellspacing="0" id="Table1">
                                        <col />
                                        <col width="50%" />
                                        <tbody class="grid_data">
                                            <tr class="pointer ">
                                                <th align="left" valign="middle" style="height: 24px; line-height: 24px; vertical-align: middle;">
                                                    等级名称
                                                </th>
                                                <th align="left" valign="middle" style="height: 24px; line-height: 24px; vertical-align: middle; display:none;">
                                                    服务费折扣
                                                </th>
                                                <th align="left" valign="middle" style="height: 24px; line-height: 24px; vertical-align: middle;">
                                                    菜品折扣
                                                </th>
                                                <th align="left" valign="middle" style="height: 24px; line-height: 24px; vertical-align: middle;display:none;">
                                                    积分倍数
                                                </th>
                                                <th align="left" valign="middle" style="height: 24px; line-height: 24px; vertical-align: middle; display:none;">
                                                    是否估先配送
                                                </th>
                                            </tr>
                                            <asp:Repeater ID="rptlist" runat="server">
                                                <ItemTemplate>
                                                    <tr class="pointer _<%#Eval("dataid") %> datatd" dataid="<%#Eval("dataid") %>" pid="<%#Eval("favourable.pid") %>">
                                                        <td class="">
                                                            <%# Eval("GradeName")%>
                                                        </td>
                                                        <td class="" style="display:none;">
                                                            <input id="tbsendmoneyDiscount" name="tbsendmoneyDiscount" value='<%# Eval("favourable.sendmoneyDiscount")%>'
                                                                reg="^[-+]?\d+(\.\d+)?$" tip="服务费折扣格式错误,请输入数字" style="width: 60px" class=" j_text"></input>折
                                                        </td>
                                                        <td class="">
                                                            <input id="tbfoodmoneyDiscount" name="tbfoodmoneyDiscount" value='<%# Eval("favourable.foodmoneyDiscount")%>'
                                                                reg="^[-+]?\d+(\.\d+)?$" tip="菜品折扣格式错误,请输入数字" style="width: 60px" class=" j_text"></input>折
                                                        </td>
                                                        <td class="" style="display:none;">
                                                            <input id="tbpointrat" name="tbpointrat" value='<%# Eval("favourable.pointrat")%>'
                                                                reg="^[-+]?\d+(\.\d+)?$" tip="积分倍数格式错误,请输入数字" style="width: 60px" class=" j_text"></input>倍
                                                        </td>
                                                        <td class="" style="display:none;">
                                                            <input type="radio" name="sendprior_<%#Eval("dataid")%>" <%# Eval("favourable.sendprior").ToString() == "1" ? "checked" : ""%>
                                                                value="1" />是
                                                            <input type="radio" name="sendprior_<%#Eval("dataid")%>" <%# Eval("favourable.sendprior").ToString() == "0" ? "checked" : ""%>
                                                                value="0" />否
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <uc2:Foot runat="server" ID="FootUC" />
        </div>
    </div>
    </form>
</body>
</html>

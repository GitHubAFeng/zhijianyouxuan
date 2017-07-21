<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showprinter.aspx.cs" Inherits="qy_54tss_Admin_showprinter" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="Adleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>打印机状态-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
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
                        <uc4:Adleft ID="Adleft1" runat="server" />
                    </div>
                    <div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="divMessages" style="color:#ea7601; margin-bottom:5px; font-size:14px;">
                                        <b>注意：</b>打印机离线（5分钟内未链接系统的打印机将被视为离线）
                                    </div>
                                    <fieldset class="AdminSearchform">
                                        <legend>查询条件 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    打印机编号
                                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="inputclass" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="filter-actions a-right">
                                                    <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <div class="scott">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                            PageSize="15" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" 页 "
                                            Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                        </webdiyer:AspNetPager>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <!--店名,联络人,电话,地址,打印机状态,打印机编号-->
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <col width="12%" />
                                                <col width="10%" />
                                                <col width="10%" />
                                                <col />
                                                <col width="12%" />
                                                <col width="8%" />
                                                <col width="15%" />
                                                <thead>
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">店名</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">联络人</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">电话</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">地址</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="store_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">打印机编号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">打印机状态</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rtpUserlist" runat="server" OnItemCommand="rptUserList_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="">
                                                                    <%# Eval("togoname") %>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("linkman") %>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("linktel") %>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("linkaddress") %>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("Printersn")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# GetStatus(Eval("LastLoginDate").ToString()) %>
                                                                </td>
                                                                <td class=" last">
                                                                    <a href="../Shop/ShopDetail.aspx?id=<%#Eval("togoid") %>">查看商店</a> |
                                                                    <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("DataID")%>' OnClientClick="return confirm('确认删除吗？');"
                                                                        runat="server" ID="delNew">删除</asp:LinkButton>
                                                                    | <a href="../Shop/AddPrinter.aspx?tid=<%#Eval("togoid") %>">编辑</a>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="scott">
                                        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="AspNetPager1">
                                        </webdiyer:AspNetPager>
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
    </form>
</body>
</html>

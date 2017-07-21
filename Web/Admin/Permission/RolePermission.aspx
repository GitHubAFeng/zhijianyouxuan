<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RolePermission.aspx.cs" Inherits="Admin_Permission_RolePermission" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分配权限-<%= WebUtility.GetMyName()%></title>
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/left.css" rel="stylesheet" type="text/css" />
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/permison.js?v=1" type="text/javascript"></script>

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

        function showsub(id, tag) {
            $("._" + id).toggle();
            var img = $(tag).attr("src");
            //alert(img);
            if (img != "../images/lines/rplus.gif") {
                $(tag).attr("src", "../images/lines/rplus.gif");
            }
            else {
                $(tag).attr("src", "../images/lines/rminus.gif");
            }
        }

        $(document).ready(function() {
            initpersion();
        })
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div style="visibility: visible;" class="content-header">
                                        <h3 class="icon-head head-customer">
                                            <asp:Label runat="server" ID="pageType">分配权限</asp:Label></h3>
                                        <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                            <p style="" class="content-buttons form-buttons">
                                                <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick="gourl('rolelist.aspx'); return false;"
                                                    Text="返回"></asp:Button>
                                                <asp:Button ID="btSave" runat="server" CssClass="button_1" Text="保存" OnClientClick="return rp_save();"
                                                    OnClick="sava_Click"></asp:Button>
                                            </p>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="sales_order_grid_massaction" style="clear: both;">
                            </div>
                            
                            <div class="mynotice" style=" display:block; color:Red; margin-bottom:10px;">提示：如果要把某功能分配给此角色，勾选后面的复选框即可</div>
                            
                            <div id="Div1" style="clear: both;">
                                <div class="hshop_class" align="center">
                                    <label runat="server" id="lbhead" style="font-size: 14px; color: #EA7601;">
                                        权限分配
                                    </label>
                                </div>
                            </div>
                            <div class="grid">
                                <div class="hor-scroll" style="overflow: auto; height: auto">
                                    <table class="data" cellspacing="0" id="Table1">
                                        <col width="15%" />
                                        <col />
                                        <tbody class="grid_data">
                                            <asp:Repeater ID="rtpNewsSortList" runat="server">
                                                <ItemTemplate>
                                                    <tr class="pointer" title="">
                                                        <td class="last" colspan="2">
                                                            <img align="absmiddle" src="../images/lines/rminus.gif" onclick="showsub(<%#Eval("ModuleID") %> , this)"><%# Eval("M_CName")%>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="rtpsub" runat="server" DataSource='<%# Eval("sublist") %>'>
                                                        <ItemTemplate>
                                                            <tr class="pointer _<%#Eval("M_ParentID") %> datatd" pc="<%#Eval("M_PageCode") %>">
                                                                <td class="">
                                                                    <img align="absmiddle" src="../images/lines/i.gif"><img align="absmiddle" style="padding-left: 10px;"
                                                                        src="<%#(Container.ItemIndex+1).ToString() == "1" ? "../images/lines/rminus.gif" : "../images/lines/tminus.gif" %>">
                                                                    <%# Eval("M_CName")%>
                                                                </td>
                                                                <td align="left" valign="middle" style="height: 24px; line-height: 24px; vertical-align: middle;"
                                                                    class="moduleitem">
                                                                    <span class="op_item">
                                                                        <input type="checkbox" value="2" class="myitem"></span>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                            </asp:Repeater>
                                            </ItemTemplate> </asp:Repeater>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminList.aspx.cs" Inherits="Admin_AdminList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="Adleft" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理员管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
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
        $(window).load(function () { $("#loading-mask").hide(); });
        $(document).ready(function () { init(); });

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
                alert("请选择要删除的管理员!");
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
                            <uc3:Adleft ID="Adleft1" runat="server" />
                        </div>
                        <div class="main-col" id="content">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="main-col-inner">
                                        <div id="divMessages">
                                        </div>
                                        <fieldset class="AdminSearchform">
                                            <legend>查询条件 </legend>
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                                <tr>
                                                    <td>
                                                        <span class="span12">
                                                            <asp:DropDownList runat="server" ID="ddlSearchType" AutoPostBack="true">
                                                                <asp:ListItem Text="用户名" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="姓名" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span>
                                                        <asp:TextBox ID="tbKeyword" runat="server" CssClass="inputclass" Style="border: 1px solid #878787; font-size: 14px;" />
                                                    </td>
                                                    <td class="filter-actions a-right">
                                                        <asp:Button runat="server" Style="margin-left: 10px;" ID="btSearch" class="form-button"
                                                            Text="搜索" OnClick="btSearch_Click" />
                                                        <button id="Button1" type="button" class="scalable " onclick="window.location.href = 'AdminDetail.aspx'"
                                                            style="">
                                                            <span>新增管理员</span>
                                                        </button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " UrlPagingTarget="_self" UrlPageIndexName="p" UrlPageSizeName="s"
                                                UrlPaging="True" PageIndexBoxClass="flattext" ShowPageIndex="True" PageSize="20"
                                                OnPageChanged="AspNetPager1_OnPageChanged" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                                TextAfterPageIndexBox=" 页 " Wrap="False">
                                            </webdiyer:AspNetPager>
                                        </div>
                                        <div id="sales_order_grid_massaction" style="clear: both;">
                                            <table class="massaction" cellpadding="0" cellspacing="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <a href="AdminDetail.aspx">添加</a> <span class="separator">|</span> <a href="#" onclick="javascript:Table.CheckAll()">全选</a> <span class="separator">|</span> <a href="#" onclick="javascript:Table.CheckNo()">取消选择</a><span class="separator">|</span> <a href="#" onclick="javascript:Table.ReCheck()">反向选择</a><span class="separator">|</span> <a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="lbDelsom" OnClientClick="return Del()" OnClick="DelList_Click">删除选定</asp:LinkButton>
                                                            </a>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="5%" />
                                                    <col />
                                                    <col width="15%" />
                                                    <col width="15%" style="display:none;" />
                                                    <col width="15%" />
                                                    <col width="10%" />
                                                    <thead>
                                                        <thead>
                                                            <tr class="headings">
                                                                <th>
                                                                    <span class="nobr">&nbsp;</span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                        class="sort-title">名称</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a href="#" name="store_id" title="asc" class="not-sort"><span
                                                                        class="sort-title">真实姓名</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">角色</span></a></span>
                                                                </th>
                                                                <th style="display:none;">
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">分区域管理员</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                        <!--sort-arrow-desc-->
                                                                        <span class="sort-title">登录时间</span></a></span>
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
                                                                    <td class="" width="20px">
                                                                        <input name="" value='<%# Eval("ID") %>' class="massaction-checkbox" type="checkbox">
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("AdminName")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("realname") %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Permistion(Eval("Permission").ToString())%>
                                                                    </td>
                                                                    <td class="" style="display:none;">
                                                                        <%# Convert.ToInt32(Eval("root")) == 1 ? "是" :"否"  %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Convert.ToDateTime(Eval("LastAccess")).ToShortDateString()%>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <a href="AdminDetail.aspx?id=<%#Eval("ID") %>">编辑</a> |
                                                                    <asp:LinkButton ID="lbdel" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'>删除</asp:LinkButton>
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

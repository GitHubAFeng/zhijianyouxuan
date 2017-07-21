<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopReviewList.aspx.cs" Inherits="Admin_Shop_ShopReviewList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家评论管理-<%= WebUtility.GetMyName()%></title>
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

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
        AddLoadFun(init);

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function HiddenList() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        function ShowList() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要显示的商家!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }

        $(document).ready(function() {
            WhitchActive(2);

            $("#aA2").addClass("active");

        });    
    </script>

    <style type="text/css">
        .inputclass
        {
            border: 1px solid #878787;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <asp:HiddenField runat="server" ID="hfflag" />
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
                        <uc4:togoleft runat="server" ID="left" />
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
                                                    <span class="span12">用户名：</span>
                                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="inputclass" Style="border: 1px solid #878787;
                                                        font-size: 14px;" />
                                                    <span class="span12">商家名：</span>
                                                    <asp:TextBox ID="tbshop" runat="server" CssClass="inputclass" Style="border: 1px solid #878787;
                                                        font-size: 14px;" />
                                                    <span class="span12">提交时间：</span><asp:TextBox ID="tbstarttime" onfocus="WdatePicker({readOnly:true})"
                                                        Width="90" runat="server" CssClass="inputclass" />
                                                    至
                                                    <asp:TextBox ID="tbendtime" onfocus="WdatePicker({readOnly:true})" Width="90" runat="server"
                                                        CssClass="inputclass" />
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
                                            PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" 页 "
                                            Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                        </webdiyer:AspNetPager>
                                    </div>
                                    <div id="sales_order_grid_massaction" style="clear: both;">
                                        <table class="massaction" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <a href="#" onclick="javascript:Table.CheckAll()">全选</a> <span class="separator">|</span>
                                                        <a href="#" onclick="javascript:Table.CheckNo()">取消选择</a><span class="separator">|</span>
                                                        <a href="#" onclick="javascript:Table.ReCheck()">反向选择</a><span class="separator">|</span>
                                                        <a href="#" onclick="return false">
                                                            <asp:LinkButton runat="server" ID="lbDelsom" OnClientClick="return Del()" OnClick="DelList_Click">删除选定</asp:LinkButton>
                                                        </a>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <col class="a-center" width="4%" />
                                                <col width="10%" />
                                                <col width="10%" />
                                                <col width="8%" />
                                                <col width="10%" />
                                               
                                                <col width="10%" />
                                                <thead>
                                                    <tr class="headings">
                                                        <th>
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"></a>
                                                            </span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                class="sort-title">编号</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">商家名称(点击编辑)</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                class="sort-title">是否查看</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                class="sort-title">提交时间</span></a></span>
                                                        </th>
                                                       <%-- <th>
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                class="sort-title">状态</span></a></span>
                                                        </th>--%>
                                                        <th class="no-link last">
                                                            <span class="nobr">操作</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rtpTogolist" runat="server" OnItemCommand="rtpTogolist_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="">
                                                                    <input name="" id="_inut" value="<%# Eval("dataid")%>" class="massaction-checkbox"
                                                                        type="checkbox">
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("username")%>
                                                                </td>
                                                                <td class="">
                                                                    <a href="ShopReviewdetail.aspx?id=<%# Eval("dataid")%>">
                                                                        <%#WebUtility.Left(Eval("TogoName").ToString(), 20)%></a>
                                                                </td>
                                                                <td class="">
                                                                    <%# isSee(Eval("rtime").ToString())%>
                                                                </td>
                                                                <td class="">
                                                                    <%#Convert.ToDateTime(Eval("posttime")).ToShortDateString()%>
                                                                </td>
                                                             <%--    <td class="">
                                                                    <%# getState(Eval("point").ToString())%>
                                                                </td>--%>
                                                                <td>
                                                                    <asp:LinkButton ID="lbtdelete" runat="server" CommandName="del" CommandArgument='<%#Eval("DataID")%>'
                                                                        OnClientClick="return confirm('确定要删除吗?');">&nbsp;删除&nbsp;</asp:LinkButton>
                                                                    |
                                                                    <asp:LinkButton ID="lbtupdate" runat="server" CommandName="update" CommandArgument='<%#Eval("DataID")%>'>&nbsp;查看&nbsp;</asp:LinkButton>
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
    </div>
    <!--foot start-->
    <uc2:Foot runat="server" ID="FootUC" />
    <!--foot end-->
    </form>
</body>
</html>

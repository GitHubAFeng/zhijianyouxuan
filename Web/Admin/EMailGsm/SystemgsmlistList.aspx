<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemgsmlistList.aspx.cs" Inherits="Admin_EMailGsm_SystemgsmlistList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="EmailGsmRecordleft" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家短信邮件营销记录查看管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/style.css" rel="stylesheet" type="text/css" />--%>
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
       $(document).ready(function(){init();});

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
                alert("请选择要删除的商家邮件记录!");
                return false;
            }
            document.getElementById("hidDels").value = ArrayToString(nums);
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
    <asp:HiddenField ID="hidDels" runat="server" />
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <!--banner end-->
        <!--center start-->
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns">
                    <div class="side-col" id="page:left">
                        <uc3:EmailGsmRecordleft ID="EmailGsmRecord1" runat="server" />
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
                                                    商家名称：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTogoName" runat="server" CssClass="inputclass" Style="border: 1px solid #878787;
                                                        font-size: 14px;" />
                                                    <span>状态</span>
                                                    <asp:DropDownList ID="ddlstatus" runat="server">
                                                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                                        <asp:ListItem Value="0">新增</asp:ListItem>
                                                        <asp:ListItem Value="1">进行中</asp:ListItem>
                                                        <asp:ListItem Value="2">完成</asp:ListItem>
                                                        <asp:ListItem Value="3">失败</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <span>类型</span>
                                                    <asp:DropDownList ID="ddlSendtype" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                                                        <asp:ListItem Value="1">短信</asp:ListItem>
                                                        <asp:ListItem Value="2">邮件</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <span>添加时间：</span>
                                                    <asp:TextBox ID="tbDate1" runat="server" onfocus="WdatePicker()" class="input_new_style"
                                                        Width="75px"></asp:TextBox>
                                                    至
                                                    <asp:TextBox ID="tbDate2" runat="server" onfocus="WdatePicker()" class="input_new_style"
                                                        Width="75px"></asp:TextBox>
                                                    <asp:Button ID="btSearch" runat="server" class="form-button" Text="搜索" OnClick="btSearch_Click" />
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
                                PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                            </webdiyer:AspNetPager>
                                    </div>
                                    <div id="sales_order_grid_massaction" style="clear: both;">
                                        <table class="massaction" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:" onclick="javascript:Table.CheckAll()">全选</a> <span class="separator">|</span>
                                                        <a href="javascript:" onclick="javascript:Table.CheckNo()">取消选择</a><span class="separator">|</span>
                                                        <a href="javascript:" onclick="javascript:Table.ReCheck()">反向选择</a><span class="separator">|</span>
                                                        <a href="javascript:" onclick="return false">
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
                                                <col class="a-center" width="5%" />
                                                <col width="10%" />
                                                <col />
                                                <col width="10%" />
                                                <col width="10%" />
                                                <col width="10%" />
                                                <col width="" />
                                                <col width="10%" />
                                                <col  />
                                                <thead>
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="real_order_id" title="asc"><span
                                                                    class="sort-title">编号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="real_order_id" title="asc"><span
                                                                    class="sort-title">商家名称</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="store_id" title="asc"><span
                                                                    class="sort-title">消费金额</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="store_id" title="asc"><span
                                                                    class="sort-title">类型</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="store_id" title="asc"><span
                                                                    class="sort-title">内容</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="created_at" title="asc"><span
                                                                    class="sort-title">添加时间</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="store_id" title="asc"><span
                                                                    class="sort-title">用户ID列表</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="javascript:" name="created_at" title="asc"><span
                                                                    class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpSetEmailRec" runat="server" OnItemCommand="rtpSetEmailRec_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="" width="20px">
                                                                        <input class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("DataId") %>'>
                                                                        </input>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("DataId")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# WebUtility.Left(Eval("TogoName"),15)%>
                                                                    </td>
                                                                    <td class="">
                                                                        ￥<%# Eval("DelMoney")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Convert.ToInt32(Eval("SentType")) == 1 ? "短信" : "邮件"%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# WebUtility.Left(Eval("Content"),10) %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Convert.ToDateTime(Eval("AddDate")).ToShortDateString() %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("UserIdList")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# showState(Eval("Status"))%>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <a href='SystemgsmlistDeTail.aspx?id=<%#Eval("DataId") %>'>编辑</a> |<asp:LinkButton
                                                                            ID="lbdel" runat="server" CommandArgument='<%# Eval("DataId") %>' CommandName="Del"
                                                                            OnClientClick="return DelConfirm();">删除</asp:LinkButton>
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

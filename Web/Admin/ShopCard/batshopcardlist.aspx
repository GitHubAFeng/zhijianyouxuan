<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batshopcardlist.aspx.cs"
    Inherits="Admin_batshopcardlist" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>优惠券批次管理-<%= WebUtility.GetMyName() %></title>
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
            return confirm('删除批次,相应优惠券也会被删除.确定删除吗？');
        }

      
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
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
                <div class="columns ">
                    <div class="side-col" id="page:left">
                        <uc3:left runat="server" ID="left" />
                    </div>
                    <div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="div5">
                                    </div>
                                    <fieldset class="AdminSearchform">
                                        <legend>优惠券批次查询</legend>
                                        <style type="text/css">
                                            .condition_table
                                            {
                                                margin-left: 15px;
                                            }
                                            .condition_table td
                                            {
                                                padding-bottom: 4px;
                                                padding-left: 4px;
                                            }
                                            .condition_table span
                                            {
                                                padding-right: 5px;
                                            }
                                            .condition_table .span_01
                                            {
                                                padding: 0 20px;
                                            }
                                            .condition_table .input_new_style
                                            {
                                                border: 1px solid #878787;
                                                font-size: 14px;
                                                float: right;
                                            }
                                        </style>
                                        <table border="0" cellpadding="0" cellspacing="0" class="condition_table" style="float: left;
                                            margin-right: 20px">
                                            <tr>
                                                <td align="right">
                                                    <span>批次名称：</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbckey" runat="server" CssClass="j_text" Width="160px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span>新增时间：</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="j_text" ID="tb_Start" onfocus="WdatePicker({readOnly:true})"
                                                        Width="75px"></asp:TextBox>
                                                    至
                                                    <asp:TextBox runat="server" ID="tb_End" CssClass="j_text" onfocus="WdatePicker({readOnly:true})"
                                                        Width="75px"></asp:TextBox>&nbsp;
                                                    <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
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
                                            SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                            Wrap="False">
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
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll" style="overflow: auto; height: auto;">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <col class="a-center" width="3%" />
                                                <col class="a-center" width="3%" />
                                                <col />
                                                <col width="7%" />
                                                <col width="7%" />
                                                <col width="15%" />
                                                <col width="7%" />
                                                <col width="8%" />
                                                <thead>
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">编号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">批次名称</span></a></span>
                                                            </th>
                                                           
                                                            <th>
                                                                <span class="nobr"><a href="#" name="store_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">类型</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">优惠券数量</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">新增时间</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">管理员</span></a></span>
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
                                                                    <input name="" value='<%# Eval("dataid") %>' class="massaction-checkbox" type="checkbox">
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("dataid")%>
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("title") %>
                                                                </td>
                                                              
                                                                <td class="">
                                                                    <%# Eval("mtype").ToString() == "1" ? "积分兑换券" : "电子优惠券"%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("cardcount")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("AddDate")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("AdminName")%>
                                                                </td>
                                                                <td class=" last">
                                                                     <a href='importshopcart.aspx?id=<%#Eval("dataid") %>'>查看</a> | 

                                                                    <asp:LinkButton ID="lbdel" runat="server" CommandName="del" OnClientClick="return confirm('删除批次,相应优惠券也会被删除.确定删除吗？');"
                                                                        CommandArgument='<%# Eval("dataid") %>'>删除</asp:LinkButton>
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
    </div>
    <!--foot end-->
    </form>
</body>
</html>

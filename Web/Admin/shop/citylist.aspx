<%@ Page Language="C#" AutoEventWireup="true" CodeFile="citylist.aspx.cs" Inherits="qy_54tss_Admin_manage_citylist" %>

<%@ Register Src="../Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="../Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>城市站管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" media="all" />
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

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        AddLoadFun(init);

        var Table;

        function init() {
            Table = new CheckTable("grid_table");

            $(document).ready(function() {
                $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
                $(".grid_data tr:even").addClass("even pointer");

            });
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的数据!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }
   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <!--banner end-->
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
                                    <div id="divMessages">
                                    </div>
                                    <fieldset class="AdminSearchform">
                                        <legend>城市站管理 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <span class="span12">关键字：</span><asp:TextBox ID="tbKeyword" runat="server" CssClass="j_text" />&nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td class="filter-actions a-right">
                                                    <div style="float: left">
                                                        <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <div class="scott">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="27%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                            ShowPageIndex="True" PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText="GO"
                                            TextAfterPageIndexBox=" 页 " Wrap="False">
                                        </webdiyer:AspNetPager>
                                    </div>
                                    <div id="sales_order_grid_massaction" style="clear: both;">
                                        <table class="massaction" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <a href="citydetail.aspx">添加</a> <span class="separator">|</span><a href="#" onclick="javascript:Table.CheckAll()">
                                                            全选</a> <span class="separator">|</span> <a href="#" onclick="javascript:Table.CheckNo()">
                                                                取消选择</a><span class="separator">|</span> <a href="#" onclick="javascript:Table.ReCheck()">
                                                                    反向选择</a><span class="separator">|</span> <a href="#" onclick="return false">
                                                                        <asp:LinkButton runat="server" ID="lbDelsom" OnClientClick="return Del()" OnClick="DelList_Click">删除选定</asp:LinkButton>
                                                                    </a>
                                                    </td>
                                                   
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll" style="overflow: auto; height: auto">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <colgroup>
                                                    <col class="a-center" width="5%" />
                                                    <col />
                                                    <col width="5%" />
                                                    
                                                    <col width="15%" />
                                                    <col width="30%" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="NewsFrom" title="asc"><span
                                                                    class="sort-title">城市</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="NewsFrom" title="asc"><span
                                                                    class="sort-title">排序</span></a></span>
                                                            </th>
                                                           
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="NewsFrom" title="asc"><span
                                                                    class="sort-title">添加时间</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpProductSortList" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="">
                                                                        <input class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("cid") %>'>
                                                                        </input>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("cname")%>&nbsp;
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("reveint")%>&nbsp;
                                                                    </td>
                                                                   
                                                                    <td class="">
                                                                        <%# Convert.ToDateTime( Eval("AddDate")).ToShortDateString()%>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <a href="citydetail.aspx?id=<%#Eval("cid") %>">设置</a>|
                                                                        <a href="autodispatchconfig.aspx?id=<%#Eval("cid") %>">自动调度设置</a>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </colgroup>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="scott">
                                        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="AspNetPager1">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc2:Foot runat="server" ID="FootUC" />
    </form>
</body>
</html>

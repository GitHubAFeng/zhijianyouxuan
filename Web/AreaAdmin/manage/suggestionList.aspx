<%@ Page Language="C#" AutoEventWireup="true" CodeFile="suggestionList.aspx.cs" Inherits="AreaAdmin_suggestionList" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>留言版-<%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" media="all" />
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

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
    </script>

    <script type="text/javascript">
        AddLoadFun(init);

        var Table;
        $(document).ready(function() { $("#tA5").addClass("active") });

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
                alert("请选择要删除的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function reset() {
            jQuery("#tbKeyword").html();
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
                    <div class="side-col" id="page:left"">
                        <uc3:left runat="server" ID="adleft" />
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
                                        <legend>留言版</legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <span class="span12">用户名：</span><asp:TextBox ID="tbKeyword" runat="server" CssClass="j_text" style=" width:120px;" />
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <span class="span12">提交时间：</span><asp:TextBox ID="tbstarttime" onfocus="WdatePicker({readOnly:true})"
                                                        CssClass="j_text" style=" width:80px;"  runat="server" />
                                                    至
                                                    <asp:TextBox ID="tbendtime" onfocus="WdatePicker({readOnly:true})"  CssClass="j_text" style=" width:80px;"  runat="server" />

                                                    &nbsp;
                                                        <asp:DropDownList ID="ddlstar" runat="server">
                                                            <asp:ListItem Value="-1" Text="审核状态"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="审核通过"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="审核失败"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    
                                                 &nbsp;
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
                                            HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                            PageSize="15" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" 页 "
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
                                        <div class="hor-scroll" style="overflow: auto; height: auto">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <colgroup>
                                                    <col class="a-center" width="5%" />
                                                    <col />
                                                    <col width="10%" />
                                                    <col width="5%" />
                                                    <col width="10%" />
                                                    <col width="5%" />
                                                    <col width="5%" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                    class="sort-title">反馈</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">提交时间</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">用户名</span></a></span>
                                                            </th>
                                                             <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a class="not-sort" href="#" name="created_at" title="asc">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">编辑</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">删除</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpNewsList" runat="server" OnItemCommand="rtpNewsList_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="" width="20px">
                                                                        <input class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("DataID") %>'>
                                                                        </input>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#WebUtility.Left(Eval("Word"), 25)%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Convert.ToDateTime(Eval("Time")).ToString()%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("UserName")%>&nbsp;
                                                                    </td>
                                                                     <td class="">
                                                                        <%#  getState( Eval("state"))%>&nbsp;
                                                                    </td>
                                                                    <td class=" ">
                                                                        <a href='suggestionDetail.aspx?id=<%#Eval("DataID") %>'>编辑</a>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("DataID") %>'
                                                                            CommandName="del" OnClientClick="return DelConfirm();">删除</asp:LinkButton>
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
        <!--foot start-->
        <uc2:Foot runat="server" ID="FootUC" />
        <!--foot end-->
    </div>
    </form>
</body>
</html>

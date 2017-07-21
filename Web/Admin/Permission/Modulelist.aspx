<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modulelist.aspx.cs" Inherits="Admin_Permission_Modulelist" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系統模快管理--<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/left.css" rel="stylesheet" type="text/css" />
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

    <script src="../../javascript/jquery-1.7.min.js" type="text/javascript"></script>
    
    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
    </script>

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
                alert("请编译要删除的内容!");
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


        function showsub(id ,tag) {
            $("._" + id).toggle();
            var img = $(tag).attr("src");
            if (img == "../images/lines/rplus.gif") {
                $(tag).attr("src", "../images/lines/rminus.gif");
            }
            else {
                $(tag).attr("src", "../images/lines/rplus.gif");
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <div class="wrapper">
        <uc1:TogoBanner runat="server" ID="Banner" />
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
                        <uc3:left ID="Adleft1" runat="server" />
                    </div>
                    <div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>
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
                                    </div>
                                    <div id="Div1" style="clear: both;">
                                         <div align="center" style="margin-top: 20px; background-image: url('../images/massaction_bg.gif');
                                            text-align: center; height: 24px; line-height: 24px; border: 1px solid #d1cfcf;">
                                            <label runat="server" id="lbhead" style="font-size: 14px; color: #ea7601;">
                                                系統模块管理
                                            </label>
                                        </div>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll" style="overflow: auto; height: auto">
                                            <table class="data" cellspacing="0" id="Table1">
                                                <col />
                                                <col width="15%" />
                                                <col width="20%" />
                                                <thead>
                                                    <tr class="headings">
                                                        <th>
                                                            <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                class="sort-title">模块名称</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a class="not-sort" href="#" name="NewsFrom" title="asc"><span
                                                                class="sort-title">排序</span></a></span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">操作</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rtpNewsSortList" runat="server" OnItemCommand="rtpNewsSortList_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="">
                                                                    <img align="absmiddle" src="../images/lines/rplus.gif" onclick="showsub(<%#Eval("ModuleID") %> ,this)" ><a href="moduledetail.aspx?id=<%#Eval("ModuleID") %>&pid=<%#Eval("M_ParentID") %>"><%# Eval("M_CName")%></a>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("M_OrderLevel")%>
                                                                </td>
                                                                <td class=" last">
                                                                    <asp:LinkButton ID="lbdel" runat="server" CommandArgument='<%# Eval("ModuleID") %>' CommandName="del"
                                                                        OnClientClick="return DelConfirm();">刪除</asp:LinkButton>
                                                                    | <a href='moduledetail.aspx?id=<%#Eval("ModuleID") %>&pid=<%#Eval("M_ParentID") %>'>
                                                                        编辑</a>| <a href='moduledetail.aspx?pid=<%#Eval("ModuleID") %>'>
                                                                        添加子模块</a>
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="rtpsub" runat="server" OnItemCommand="rtpNewsSortList_ItemCommand"
                                                                DataSource='<%# getsub(Eval("ModuleID")) %>'>
                                                                <ItemTemplate>
                                                                    <tr class="pointer _<%#Eval("M_ParentID") %>" title="" style=" display:none">
                                                                        <td class="">
                                                                            <img align="absmiddle" src="../images/lines/i.gif"><img align="absmiddle" style="padding-left: 10px;"
                                                                                src="../images/lines/tminus.gif">
                                                                            <a href="moduledetail.aspx?id=<%#Eval("ModuleID") %>&pid=<%#Eval("M_ParentID") %>">
                                                                                <%# Eval("M_CName")%></a>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("M_OrderLevel")%>
                                                                        </td>
                                                                        <td class=" last">
                                                                            <asp:LinkButton ID="lbdel" runat="server" CommandArgument='<%# Eval("ModuleID") %>'
                                                                                CommandName="del" OnClientClick="return DelConfirm();">刪除</asp:LinkButton>
                                                                            | <a href="moduledetail.aspx?id=<%#Eval("ModuleID") %>&pid=<%#Eval("M_ParentID") %>">
                                                                                编辑</a>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
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
    <uc2:Foot runat="server" ID="FootUC" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliverList.aspx.cs" Inherits="qy_55tuan_Admin_DeliverList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>配送员信息管理-<%=  WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js?v=0717" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="../javascript/spin.min.js"></script>




    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
        AddLoadFun(init);

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
                alert("请选择要删除的配送员信息!");
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
        <uc1:TogoBanner ID="TogoBanner1" runat="server" />
        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
        <div class="wrapper">
            <!--banner start-->
            <!--banner end-->
            <!--center start-->
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <div class="side-col" id="page:left">
                            <uc3:left ID="left1" runat="server" />
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
                                                    <td>姓名：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbKeyword" runat="server" CssClass="j_text" Width="120px" />&nbsp;

                                                     <asp:DropDownList ID="ddlcity" runat="server" CssClass="j_select" Width="100" AppendDataBoundItems="true">
                                                         <asp:ListItem Value="0">所有城市</asp:ListItem>
                                                     </asp:DropDownList>

                                                        &nbsp;
                                                    </td>
                                                    <td class="filter-actions a-right">
                                                        <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />&nbsp;&nbsp;
                                                    <asp:Button runat="server" ID="btAdd" class="form-button" Text="添加" OnClientClick="javascript:gourl('DeliverDetail.aspx');" />&nbsp;&nbsp;
                                                   
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>


                                        <fieldset class="AdminSearchform">
                                            <legend>骑士gps数据的清理(清除历史数据，可加快查询速度)</legend>
                                            <div style="padding: 20px;">

                                                <asp:Button runat="server" ID="btclear" class="form-button" Text="删除所有记录" OnClientClick="Loader.show('#btclear')" OnClick="clearGPSReclord_Click" />&nbsp;&nbsp;
                                                    
                                                <asp:Button runat="server" ID="btclear30day" CommandArgument="30" class="form-button" Text="删除30前的记录" OnClientClick="Loader.show('#btclear30day')" OnClick="delGPSReclord_Click" />&nbsp;&nbsp;
                                                 
                                                <asp:Button runat="server" ID="btclear7day" CommandArgument="7" class="form-button" Text="删除7天前的记录" OnClientClick="Loader.show('#btclear7day')" OnClick="delGPSReclord_Click" />&nbsp;&nbsp;
                                                  
                                            </div>
                                        </fieldset>


                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                                CustomInfoSectionWidth="27%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                                ShowPageIndex="True" PageSize="15" SubmitButtonClass="flatbutton" SubmitButtonText="GO"
                                                TextAfterPageIndexBox=" 页 " Wrap="False">
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
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll">
                                                <table class="data" cellspacing="0" id="grid_table">

                                                    <thead>
                                                        <thead>
                                                            <tr class="headings">
                                                                <th width="3%">
                                                                    <span class="nobr">&nbsp;</span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a class="not-sort" href="#" name="real_order_id" title="asc"><span
                                                                        class="sort-title">姓名</span></a></span>
                                                                </th>
                                                                <th width="12%">
                                                                    <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                        class="sort-title">电话</span></a></span>
                                                                </th>

                                                                <th width="12%">
                                                                    <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                        class="sort-title">工资</span></a></span>
                                                                </th>

                                                                <th width="12%">
                                                                    <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                        class="sort-title">城市</span></a></span>
                                                                </th>
                                                                <th width="12%">
                                                                    <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                        class="sort-title">群组</span></a></span>
                                                                </th>

                                                                <th width="8%">
                                                                    <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                        class="sort-title">骑士状态</span></a></span>
                                                                </th>
                                                                <th width="8%">
                                                                    <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                        class="sort-title">是否接单</span></a></span>
                                                                </th>
                                                                <th width="8%">
                                                                    <span class="nobr"><a class="not-sort" href="#" name="store_id" title="asc"><span
                                                                        class="sort-title">审核状态</span></a></span>
                                                                </th>
                                                                <th class="no-link last" width="12%">
                                                                    <span class="nobr" style="padding-top: 3px; *padding-top: 5px;">操作</span>
                                                                </th>
                                                            </tr>
                                                        </thead>

                                                        <tbody class="grid_data">
                                                            <asp:Repeater ID="rtpGifts" runat="server" OnItemCommand="rtpGifts_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr class="pointer" title="">
                                                                        <td class="" width="20px">
                                                                            <input class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("DataId") %>'>
                                                                            </input>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("Name")%>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("Phone")%>
                                                                        </td>
                                                                        <td class="">每单<font color="red"><%# Eval("CodeId")%></font>元+提成<font color="red"><%# Eval("Section")%></font>%
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("cityname")%>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("Groupname")%>
                                                                        </td>

                                                                        <td class="">
                                                                            <%#ParseState(Eval("Status"))%>
                                                                        </td>
                                                                        <td class="">
                                                                            <%#IsWorking(Eval("IsWorking"))%>
                                                                        </td>

                                                                        <td class="">
                                                                            <%#ApprovedState(Eval("IsApproved"))%>
                                                                        </td>

                                                                        <td class=" last" align="center">
                                                                            <a href='DeliverDetail.aspx?id=<%#Eval("DataId") %>&cityid=<%#Eval("Inve1") %>'>编辑</a> | 
                                                                            
                                                                        <asp:LinkButton ID="lbApproved" runat="server" CommandArgument='<%# Eval("DataId") %>' CommandName="Approved">审核</asp:LinkButton>
                                                                            | 

                                                                        <asp:LinkButton ID="lbdel" runat="server" CommandArgument='<%# Eval("DataId") %>' CommandName="Del" OnClientClick="return DelConfirm();">删除</asp:LinkButton>
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

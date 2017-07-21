<%@ Page Language="C#" AutoEventWireup="true" CodeFile="packagelist.aspx.cs" Inherits="qy_54tss_Admin_Shop_packagelist" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>套餐列表－<%= WebUtility.GetMyName() %></title>
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
        $(window).load(function () { $("#loading-mask").hide(); });
        AddLoadFun(init);

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要进行操作的数据!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function Fun() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要进行操作的数据!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        $(document).ready(function () { });
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdDels" runat="server" />
        <asp:HiddenField runat="server" ID="hidTogoId" />
        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
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
                                            <legend>查询条件 </legend>
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                                <tr>
                                                    <td>
                                                        <span class="span12">套餐名称：</span>
                                                        <asp:TextBox ID="tbKeyword" runat="server" CssClass="inputclass" Style="border: 1px solid #878787; font-size: 14px;" />
                                                        <asp:DropDownList runat="server" ID="ddlallnum" Width="80px" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="-1">状态</asp:ListItem>
                                                            <asp:ListItem Value="0">正常</asp:ListItem>
                                                            <asp:ListItem Value="1">下线</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td class="filter-actions a-right">
                                                        <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                        <asp:Button runat="server" ID="Button1" class="form-button" Text="添加" OnClick="add_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>

                                        <div class="mynotice" style="display:block;">说明：状态为正常的套餐才会显示，排序一栏数字越大显示越在前，当已售大于总量后此商品不能再订购了;</div>

                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                                PageSize="40" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" 页 "
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
                                                            </a><span class="separator">|</span><a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="lbDownFood" OnClientClick="return Fun()" OnClick="lbDownFood_Click">批量下架</asp:LinkButton>
                                                            </a><span class="separator">|</span><a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="lbUpFood" OnClientClick="return Fun()" OnClick="lbUpFood_Click">批量上架</asp:LinkButton>
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
                                                    <col width="13%" />
                                                    <col width="5%" />
                                                    <col width="5%" />
                                                    <col width="5%" />
                                                    <col width="5%" />
                                                    <col width="14%" />
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
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">供应时间</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">总量</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">已售</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">排序</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rptFoodlist" runat="server" OnItemCommand="rptUserList_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer item<%# Eval("pid")%>">
                                                                    <td class="" width="20px">
                                                                        <input name="" id="_inut" value="<%# Eval("pid")%>" class="massaction-checkbox" type="checkbox">
                                                                    </td>

                                                                    <td class="">
                                                                        <%# Eval("title")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Convert.ToDateTime(Eval("starttime")).ToShortTimeString()%>~
                                                                    <%# Convert.ToDateTime(Eval("endtime")).ToShortTimeString()%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("state").ToString().Trim()=="1"?"已下架":"正常"%>
                                                                    </td>


                                                                    <td class="">
                                                                        <asp:TextBox runat="server" ID="tbNum" CssClass="j_text"
                                                                            MaxLength="5" Style="width: 40px;" onkeypress="return only_num(event)" Text='<%# Eval("Num") %>' reg="^\d+$" tip="总量格式错误,请输入整数"></asp:TextBox>
                                                                    </td>

                                                                    <td class="">
                                                                        <asp:TextBox runat="server" ID="tbcnum" CssClass="j_text"
                                                                            MaxLength="5" Style="width: 40px;" onkeypress="return only_num(event)" Text='<%# Eval("cnum") %>' reg="^\d+$" tip="已售格式错误,请输入整数"></asp:TextBox>
                                                                    </td>

                                                                    <td class="">
                                                                        <asp:TextBox runat="server" ID="tbsortnum" CssClass="j_text"
                                                                            MaxLength="5" Style="width: 40px;" onkeypress="return only_num(event)" Text='<%# Eval("sortnum") %>' reg="^\d+$" tip="排序格式错误,请输入整数"></asp:TextBox>
                                                                    </td>

                                                                    <td class=" last">

                                                                        <asp:Button runat="server" ID="tbupdate" Text="更新" CommandName="update" CommandArgument='<%# Eval("pid")%>' OnClientClick='<%# "javascript:return j_submitdata(\""+"item" + Eval("pid") + "\");" %>' />

                                                                        <a href='packageDetail.aspx?id=<%#Eval("pid")%>&tid=<%# Eval("shopid") %>'>编辑</a>
                                                                        <span class="separator">|</span>
                                                                        <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("pid")%>' OnClientClick="return DelConfirm();"
                                                                            runat="server" ID="del">删除</asp:LinkButton>

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

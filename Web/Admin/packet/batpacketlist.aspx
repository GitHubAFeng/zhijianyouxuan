﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batpacketlist.aspx.cs" Inherits="Admin_packet_batpacketlist" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>红包批次管理-<%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
        AddLoadFun(init);
        Request =
        {
            QueryString: function (item) {
                var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
                return svalue ? svalue[1] : svalue;
            }
        }
        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
            var d = Request.QueryString("state");
            $("#ali" + d).addClass("active");
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

        function jDel() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要操作的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        //绑定优惠券
        function checksetcard() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要操作的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            var tbuid = $("#tbuid").val();
            if (tbuid == "") {
                alert("请输入用户编号");
                return false;
            }
            return true;
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
                                        <div id="divMessages">
                                        </div>
                                        <fieldset class="AdminSearchform">
                                            <legend>红包查询</legend>
                                            <table border="0" cellpadding="0" cellspacing="0" class="condition_table" style="float: left; margin-right: 20px">
                                                <tr>
                                                    <td align="right">
                                                        <span>红包编号：</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbckey" runat="server" CssClass="j_text"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>有效期：</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="j_text" ID="tb_Start" onfocus="WdatePicker({readOnly:true})"
                                                            Width="75px"></asp:TextBox>
                                                        至
                                                    <asp:TextBox runat="server" ID="tb_End" CssClass="j_text" onfocus="WdatePicker({readOnly:true})"
                                                        Width="75px"></asp:TextBox>
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
                                                TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                                ShowPageIndex="True" PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                                TextAfterPageIndexBox=" 页 " Wrap="False" AlwaysShow="true">
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
                                        <div class="clear">
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll" style="overflow: auto;">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="5%" />
                                                    <col width="12%" />
                                                    <col />
                                                    <col width="12%" />
                                                    <col width="10%" />
                                                    <col width="8%" />
                                                    <col width="16%" />
                                                    <col width="15%" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">红包编号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">领取的手机号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="store_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">红包个数</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">红包获取方式</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">金额获取方式</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">金额</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">有效期</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">使用条件</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">操作</span></a></span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpUserlist" runat="server" OnItemCommand="rptUserList_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="" width="20px">
                                                                        <input name="" value='<%# Eval("id") %>' class="massaction-checkbox" type="checkbox">
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("id")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("pulltel") %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("reveint") %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# WebUtility.GetStylePacket(Eval("pid").ToString()) %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# WebUtility.GetStyleByPacket(Eval("reveint1").ToString()) %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("money") %>

                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("state")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("revevar1")%>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <a href='importpacket.aspx?id=<%#Eval("id") %>'>查看</a> | 

                                                                    <asp:LinkButton ID="lbdel" runat="server" CommandName="del" OnClientClick="return confirm('确定删除吗？');"
                                                                        CommandArgument='<%# Eval("id") %>'>删除</asp:LinkButton>
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


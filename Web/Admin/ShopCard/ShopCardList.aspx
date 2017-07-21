<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopCardList.aspx.cs" Inherits="Admin_card_cardlistShopCardList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>优惠券管理-<%= WebUtility.GetMyName() %></title>
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
            $("#ali" + d).addClass("active");;
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

        function sentcode() {
            var input = document.getElementById('btsentcode');
            input.setAttribute('disabled', 'disabled');
            document.getElementById('btsentcode').value = '正在发送验证码';

            jQuery.ajax(
                {
                    type: "post",
                    url: "../Ajax/SendGmsCode.aspx",
                    data: "t=" + new Date().getTime(),
                    success: function (msg) {
                        if (msg == "1") {
                            document.getElementById('btsentcode').value = '验证码发送成功';
                            $("#btsentcode").removeAttr("disabled");
                        }
                        else {
                            alert('验证码发送失败');
                            $("#btsentcode").removeAttr("disabled");
                            document.getElementById('btsentcode').value = '重新发送';
                        }
                    }
                })
        }

        //check uid
        function checkuid() {
            var tbuid = $("#tbuid").val() + "";
            if (tbuid == "") {
                return;
            }
            jQuery.ajax(
                {
                    type: "post",
                    url: "../Ajax/AjaxCheck.aspx",
                    data: "t=" + new Date().getTime() + "&type=uid&value=" + tbuid,
                    success: function (msg) {
                        var user = eval("(" + msg + ")");
                        if (user.uid == "0") {
                            alert("此用户不存在，请重新输入");
                        }
                        else {
                            $("#user_msg").html("当前用户：" + user.uname);
                            $("#hfischeck").val("1");

                        }
                    }
                })
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
                                        <div class="mynotice" style="display: block; color: Red; padding: 3px; margin-bottom: 5px;">
                                            提示：优惠券激活后方可正常使用
                                        </div>
                                        <fieldset class="AdminSearchform">
                                            <legend>优惠券查询</legend>
                                            <table border="0" cellpadding="0" cellspacing="0" class="condition_table" style="float: left; margin-right: 20px">
                                                <tr>
                                                    <td align="right">
                                                        <span>券号：</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbckey" runat="server" CssClass="j_text"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>生成时间：</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="j_text" ID="tb_Start" onfocus="WdatePicker({readOnly:true})"
                                                            Width="75px"></asp:TextBox>
                                                        至
                                                    <asp:TextBox runat="server" ID="tb_End" CssClass="j_text" onfocus="WdatePicker({readOnly:true})"
                                                        Width="75px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>绑定状态：</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_Operator" runat="server">
                                                            <asp:ListItem Value="-1">绑定状态</asp:ListItem>
                                                            <asp:ListItem Value="0">未绑定</asp:ListItem>
                                                            <asp:ListItem Value="1">已绑定</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>激活状态：</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlinve2" runat="server">
                                                            <asp:ListItem Value="-1">激活状态</asp:ListItem>
                                                            <asp:ListItem Value="0">未激活</asp:ListItem>
                                                            <asp:ListItem Value="1">已激活</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>使用状态：</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlisused" runat="server">
                                                            <asp:ListItem Value="-1">使用状态</asp:ListItem>
                                                            <asp:ListItem Value="0">未使用</asp:ListItem>
                                                            <asp:ListItem Value="1">已使用</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>管理员：</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddladmin" runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="-1">管理员</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                    <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" cellpadding="0" cellspacing="0" class="condition_table" style="float: left; margin-right: 20px">

                                                <tr>
                                                    <td align="right">
                                                        <span>&nbsp;</span>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="Button2" class="form-button" Text="激活当前选择记录" OnClick="part_Click"
                                                            OnClientClick="return jDel()" />
                                                        <asp:Button runat="server" ID="Button1" class="form-button" Text="激活当前搜索记录" OnClick="all_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>用户编号：</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbuid" runat="server" CssClass="j_text" Style="width: 80px" onkeypress="return only_num(event)"></asp:TextBox><div class="mynotice notice" id="user_msg" style="color: Red; margin-left: 5px;">
                                                            输入要绑定的用户编号
                                                        </div>
                                                        <asp:HiddenField runat="server" ID="hfischeck" Value="0" />
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span>&nbsp;</span>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btbdcard" class="form-button" Text="绑定选中优惠券" OnClick="bd_Click"
                                                            OnClientClick="return checksetcard();" />

                                                        <input type="button" value="批量发优惠券" onclick="gourl('giveCardUsers.aspx')" class="form-button"  />

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
                                            <div class="hor-scroll" style="overflow: auto; height: ">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="5%" />
                                                    <col width="12%" />
                                                    <col />
                                                    <col width="20%" />
                                                    <col width="6%" />
                                                    <col width="8%" />
                                                    <col width="8%" />
                                                    <col width="8%" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">券号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">批次名称</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="store_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">形式</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">激活状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">使用状态</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                    <!--sort-arrow-desc-->
                                                                    <span class="sort-title">会员帐号</span></a></span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpUserlist" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="" width="20px">
                                                                        <input name="" value='<%# Eval("CID") %>' class="massaction-checkbox" type="checkbox">
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("ckey")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("title") %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("cardnum") %>

                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("Inve2").ToString() == "0" ? "未激活":"已激活"%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("state").ToString() == "0" ? "未绑定" : "已绑定"%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("isused").ToString() == "0" ? "未使用" : "已使用"%>
                                                                    </td>
                                                                    <td class="no-link last">
                                                                        <%#Eval("username")%>&nbsp;
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

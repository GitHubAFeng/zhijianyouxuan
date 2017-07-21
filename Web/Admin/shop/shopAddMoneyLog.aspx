<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopAddMoneyLog.aspx.cs"
    Inherits="qy_54tss_Admin_User_shopAddMoneyLog" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register TagPrefix="uc3" Src="~/Admin/Adleft.ascx" TagName="left" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家收支记录-<%= WebUtility.GetMyName()%></title>
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
            $("#loading-mask").hide();
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的数据!");
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

        function GotoAddFood() {
            window.location.href = "shopAddMoney.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        $(document).ready(function () { init(); });

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidDels" runat="server" />
        <asp:HiddenField runat="server" ID="hidTogoId" />
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
                        <uc3:left ID="Left1" runat="server" />
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
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; height: 25px;">
                                                <tr>
                                                    <td>
                                                        <span class="span12">商家名：</span>
                                                        <asp:TextBox ID="tbKeyword" runat="server" CssClass="j_text w120" />&nbsp; &nbsp;
                                                    </td>
                                                    <td>时间：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="j_text" ID="tbStartTime" Width="80px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                        至
                                                    <asp:TextBox runat="server" ID="tbEndTime" CssClass="j_text" Width="80px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlpaymodel" runat="server" class="j_select">
                                                            <asp:ListItem Value="-1">收支类型</asp:ListItem>
                                                            <asp:ListItem Value="1">收入</asp:ListItem>
                                                            <asp:ListItem Value="2">支出</asp:ListItem>

                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlPayType" runat="server" class="j_select">
                                                            <asp:ListItem Value="-1">交易类型</asp:ListItem>
                                                            <asp:ListItem Value="0">后台操作</asp:ListItem>

                                                            <asp:ListItem Value="1">提现</asp:ListItem>
                                                            <asp:ListItem Value="7">订单结算</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td>&nbsp; &nbsp;<asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索"
                                                        OnClick="btSearch_Click" />
                                                    </td>
                                                    <td>&nbsp; &nbsp;
                                                    <asp:Button runat="server" ID="btadd" class="form-button" Text="为此商家充值或者扣款" OnClientClick="GotoAddFood();return false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <fieldset class="AdminSearchform" runat="server" id="userinfofieldset">
                                            <legend runat="server" id="username_legend"></legend>
                                            <div style="clear: both; padding: 10px;">
                                                <span>当前可用余额:<span runat="server" id="lbmoney" style="margin-left: 20px; font-weight: bold; color: Red">0</span></span>
                                            </div>
                                        </fieldset>
                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " UrlPagingTarget="_self" UrlPageIndexName="p" UrlPageSizeName="s"
                                                UrlPaging="True" PageIndexBoxClass="flattext" ShowPageIndex="True" PageSize="20"
                                                SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                                OnPageChanged="AspNetPager1_PageChanged" Wrap="False">
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
                                            <div class="hor-scroll">
                                                <table class="data grid_table" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="5%" />
                                                    <col width="10%" />
                                                    <col width="10%" />
                                                    <col width="8%" />
                                                    <col width="10%" />

                                                    <col />
                                                    <thead>
                                                        <thead>
                                                            <tr class="headings">
                                                                <th>
                                                                    <span class="nobr">&nbsp;</span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">姓名</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">金额</span></a></span>
                                                                </th>
                                                                <th>
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">交易类型</span></a></span>
                                                                </th>





                                                                <th class="">
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">时间</span></a></span>
                                                                </th>

                                                                <th class="no-link last">
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">说明</span></a></span>
                                                                </th>


                                                            </tr>
                                                        </thead>
                                                        <tbody class="grid_data">
                                                            <asp:Repeater ID="rtpGifts" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class="pointer" title="">
                                                                        <td align="center">
                                                                            <input class="massaction-checkbox" name="" type="checkbox" value='<%# Eval("dataid") %>'>
                                                                            </input>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("TogoName")%>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("AddMoney")%>
                                                                        </td>
                                                                         <td class="">
                                                                            <%# Hangjing.WebCommon.WebHelper.shopRecharge(Eval("PayType").ToString())%>[ <%# Hangjing.WebCommon.WebHelper.shopRechargeState(Eval("PayState").ToString())%>]
                                                                        </td>

                                                                        


                                                                        <td class="">
                                                                            <%# Eval("AddDate").ToString() %>
                                                                        </td>
                                                                        <td class="last">
                                                                            <%# Eval("inve2").ToString() %>
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

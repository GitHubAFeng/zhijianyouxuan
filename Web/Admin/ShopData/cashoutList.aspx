<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cashoutList.aspx.cs" Inherits="Admin_shop_cashoutList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>提现申请-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <link href="/javascript/jbox/Skins/jbox.css" rel="stylesheet" />
    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>
    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () { init(); });

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
                alert("请选择要删除的订单!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <asp:HiddenField ID="hfcid" runat="server" Value="0" />
        <asp:HiddenField ID="hfflag" runat="server" />
        <asp:HiddenField ID="hdDels" runat="server" />
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
                            <uc4:togoleft ID="togoleft1" runat="server" />
                        </div>

                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>
                                <fieldset class="AdminSearchform">
                                    <legend>查询条件 </legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left"
                                        class="condition_table">

                                        <tr>
                                            <td align="right">
                                                <span style="width: 160px;">商家ID： </span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbTogoID" runat="server" CssClass="j_text" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span style="width: 160px;">商家：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="tbTogoName" CssClass="j_text" Width="100px" />
                                            </td>
                                        </tr>

                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left"
                                        class="condition_table">
                                        <tr>
                                            <td align="right">
                                                <span class="span12" style="width: 160px;">提交时间：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" CssClass="inputclass" ID="tbStartTime" onfocus="WdatePicker({readOnly:true})"
                                                    Width="75"></asp:TextBox>
                                                <asp:Label runat="server" ID="lbTemp" Text="至"></asp:Label>
                                                <asp:TextBox runat="server" ID="tbEndTime" CssClass="inputclass" onfocus="WdatePicker({readOnly:true})"
                                                    Width="75"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="span12" style="width: 160px;">状态：</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlstate" class="j_select">
                                                    <asp:ListItem Text="状态" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="未处理" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="已打款" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="拒绝" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="商家取消" Value="3"></asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click"
                                                    OnClientClick="loading(); return true;" />

                                            </td>
                                        </tr>


                                    </table>
                                </fieldset>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
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
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="5%" />
                                                    <col width="12%" />
                                                    <col width="5%" />
                                                    <col width="15%" />
                                                    <col width="7%" />
                                                    <col />
                                                    <col width="5%" />
                                                    <col width="10%" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">商家</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">提现金额</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="created_at" class="not-sort"><span class="sort-title">申请时间</span></a></span>
                                                            </th>

                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">备注</span></a></span>
                                                            </th>

                                                            <th>
                                                                <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">处理人</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr last">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpOrderlist" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="">
                                                                        <input name="" id="_inut" value="<%# Eval("dataid")%>" class="massaction-checkbox"
                                                                            type="checkbox">
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("TogoName") %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("AddMoney").ToString()%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%#Eval("AddDate").ToString()%>
                                                                       
                                                                    </td>

                                                                    <td class="" id="trstatebox<%# Eval("dataid")%>">
                                                                        <%# Hangjing.WebCommon.WebHelper.shopRechargeState(Eval("PayState").ToString())%>
                                                                    </td>
                                                                    <td class="">
                                                                        <label id="remark_<%# Eval("dataid")%>"><%# Eval("inve2")%></label>
                                                                    </td>


                                                                    <td class="">
                                                                        <%#Eval("admin")%>
                                                                    </td>

                                                                    <td class=" last">

                                                                        <div style="<%#Convert.ToInt32(Eval("PayState")) == 0 ? "": "display:none"%>" id="bt_box_<%# Eval("dataid")%>">
                                                                            <input type="button" value="处理" onclick="opOrder(<%# Eval("dataid")%>,'inve2')" />
                                                                        </div>
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
        </div>

    </form>
</body>
</html>

<script src="/javascript/jbox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
<script type="text/javascript"> 

    var obCashOut = null;

    ///提现对像
    function CashOutObject(cid,msgbox){
        this.cid = cid;
        this.msgbox = msgbox;//备注输入款id
    }
    CashOutObject.prototype.SetState = function(state)
    {
        var id = this.cid;
        var msg = $("#tbremark").val();
        var flag = this.checkData(state);
        if (flag == false) {
            return flag;
        }

        var statemsg = "已打款";
        if (state == 2) {
            statemsg = "拒绝";
        }

        var url = "/ajaxHandler.ashx";
        var para = "t=" + new Date().getTime() + "&id=" + id + "&method=setCashOut&msg=" + escape(msg)+"&state="+state;
        showload_super();
        jQuery.ajax(
        {
            type: "post",
            url: url,
            data: para,
            success: function (msg) {
            
                hideload_super();
                alert("操作成功");
                $("#bt_box_"+id).hide();
                $("#trstatebox"+id).html(statemsg);

                $("#remark_"+id).html($("#tbremark").val());

                $.jBox.close();
            }
        })
    }
    CashOutObject.prototype.checkData= function(state)
    {
        var box = $("#"+this.msgbox);
        var msg = box.val();
        if (state == 2 && msg == "") {
            alert("请备注拒绝理由");
            return false;
        }
        return true;
    }


    //处理订单
    function opOrder(cid,msgbox) {
        obCashOut = new CashOutObject(cid,msgbox);
        var html = "<div style='padding: 10px;'> <div>备注：</div><textarea style=\"height: 80px; width: 300px;\" id='tbremark'>"+($("#remark_"+cid).html())+"</textarea><div style=\"margin-top:10px;\">";
        html += "<input type=\"button\" style=\"margin-left: 30px;\" value=\"完成\" onclick=\"obCashOut.SetState(1)\" /><input type=\"button\" style=\"margin-left: 30px;\" value=\"拒绝\" onclick=\"obCashOut.SetState(2)\" /></div></div>";
       
        $.jBox(html, { title: "处理提现", buttons: {}  });
    }


</script>

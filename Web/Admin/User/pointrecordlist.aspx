<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pointrecordlist.aspx.cs"
    Inherits="qy_54tss_Admin_User_pointrecordlist" ValidateRequest="false" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员积分记录-<%= WebUtility.GetMyName() %></title>
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
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />

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
                alert("请选择要删除的会员!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function jDel() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要重置密码的会员!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }
        function ShowMsg() {
            $("#windown-boxfix").show();
            locationdiv();
            return false;
        }


        function checkmsg_part() {
            var tbTitle = $("#tbTitle").val();
            if (tbTitle == "") {
                alert("请输入标题!");
                return false;
            }

            var hfsendtype = $("#hfsendtype").val() + "";
            if (hfsendtype == "1") {
                var nums = Table.GetChecks();
                if (nums == undefined || nums.length == 0) {
                    alert("请选择要操作的用户!");
                    return false;
                }
                document.getElementById("hdDels").value = ArrayToString(nums);
            }
            KE.sync('tbIntroduce');
            showloadfix();
            return true;
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <asp:HiddenField ID="hfsendtype" runat="server" Value="0" />
    <asp:HiddenField ID="hftype" runat="server" Value="0" />
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
    <div class="wrapper">
        <uc1:TogoBanner runat="server" ID="Banner" />
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
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
                                        <legend>查询条件 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left"
                                            class="condition_table">
                                            <tr>
                                                <td align="right">
                                                    <span>帐号：</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_Name" runat="server" CssClass="j_text"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span>注册时间：</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="j_text" ID="tb_Start" onfocus="WdatePicker({readOnly:true})"
                                                        Width="75px"></asp:TextBox>
                                                    至
                                                    <asp:TextBox runat="server" ID="tb_End" CssClass="j_text" onfocus="WdatePicker({readOnly:true})"
                                                        Width="75px"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:Button runat="server" ID="Button2" class="form-button" Text="搜索" OnClick="btSearch_Click" />
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
                                            PageSize="50" SubmitButtonClass="flatbutton" SubmitButtonText="GO" TextAfterPageIndexBox=" 页 "
                                            Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                        </webdiyer:AspNetPager>
                                    </div>
                                    <div id="sales_order_grid_massaction" style="clear: both;">
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <col class="a-center" width="5%" />
                                                <col width="10%" />
                                                <col width="5%" />
                                                <col width="15%" />
                                                <col />
                                                <thead>
                                                    <tr class="headings">
                                                        <th align="center">
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"></a>
                                                            </span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                class="sort-title">帐号</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                class="sort-title">积分</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">时间</span></a></span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                class="sort-title">说明</span></a></span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rptCustomerList" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="">
                                                                    <input name="" id="_inut" value="<%# Eval("dataid")%>" class="massaction-checkbox"
                                                                        type="checkbox">
                                                                    <asp:HiddenField runat="server" ID="hidDataId" Value='<%# Eval("DataId")%>' />
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("uname")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("Point")%>
                                                                </td>
                                                                <td class="">
                                                                    <%#  Eval("Time") %>
                                                                </td>
                                                                <td class=" last" style=" text-align:left; padding-left:5px;">
                                                                   <%# Eval("event")%> 
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
        <uc2:Foot runat="server" ID="FootUC" />
    </div>
    </form>
</body>
</html>

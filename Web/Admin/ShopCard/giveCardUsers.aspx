<%@ Page Language="C#" AutoEventWireup="true" CodeFile="giveCardUsers.aspx.cs" Inherits="Admin_User_giveCardUsers" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="userleft" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量赠送优惠券-<%= WebUtility.GetMyName()%></title>
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });

        var Table;
        function init() {
            // Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        $(document).ready(function () {
            init();
        })

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

        function getexcel() {
            var url = "SearchExcelOrder.aspx?type=1";
            window.open(url);
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdDels" runat="server" />
        <!--加载中显示的div-->
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <!--banner start-->
            <uc1:TogoBanner runat="server" ID="Banner" />
            <!--banner end-->
            <!--center start-->
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <uc3:userleft ID="userleft1" runat="server" />
                        &nbsp;<div class="main-col" id="content">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="main-col-inner">
                                        <div id="divMessages">
                                        </div>





                                        <div class="box-left">




                                            <fieldset class="AdminSearchform">
                                                <legend>第一步，搜索用户 </legend>
                                                <style type="text/css">
                                                    .condition_table {
                                                        margin-left: 15px;
                                                    }

                                                        .condition_table td {
                                                            padding-bottom: 4px;
                                                            padding-left: 4px;
                                                        }

                                                        .condition_table span {
                                                            padding-right: 5px;
                                                        }

                                                        .condition_table .span_01 {
                                                            padding: 0 20px;
                                                        }

                                                        .condition_table .input_new_style {
                                                            border: 1px solid #878787;
                                                            font-size: 14px;
                                                            float: right;
                                                        }
                                                </style>
                                                <table border="0" cellpadding="0" cellspacing="0" class="condition_table" style="margin-left: 10px; float: left">
                                                    <tr>
                                                        <td align="right">
                                                            <span style="width: 160px;">姓名：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tb_UserName" runat="server" CssClass="j_text" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>帐号：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tb_Name" runat="server" CssClass="j_text"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>积分：</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddl_Operator" runat="server" class="j_select">
                                                                <asp:ListItem Value="<" Selected="True">小于</asp:ListItem>
                                                                <asp:ListItem Value="=">等于</asp:ListItem>
                                                                <asp:ListItem Value=">">大于</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="tb_Point" runat="server" CssClass="j_text" Width="60px" onkeypress="return only_num(event)"></asp:TextBox>分
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>邮箱：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tb_Email" runat="server" CssClass="j_text"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>注册时间：</span>
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
                                                            <span>订餐时间：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="j_text" ID="tborderStart" onfocus="WdatePicker({readOnly:true})"
                                                                Width="75px"></asp:TextBox>
                                                            至
                                                    <asp:TextBox runat="server" ID="tborderEnd" CssClass="j_text" onfocus="WdatePicker({readOnly:true})"
                                                        Width="75px"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                </table>
                                                <table border="0" cellpadding="0" cellspacing="0" class="condition_table" style="margin-left: 10px; float: left">
                                                    <tr>
                                                        <td align="right">
                                                            <span>会员ID：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbDataID" runat="server" CssClass="j_text"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>会员状态：</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlstate" runat="server" class="j_select">
                                                                <asp:ListItem Value="-1">会员状态</asp:ListItem>
                                                                <asp:ListItem Value="0">正常</asp:ListItem>
                                                                <asp:ListItem Value="1">黑名单</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:DropDownList ID="ddlPayPWDAnswer" runat="server" class="j_select">
                                                                <asp:ListItem Value="-1">会员类型</asp:ListItem>
                                                                <asp:ListItem Value="0">普通</asp:ListItem>
                                                                <asp:ListItem Value="1">分销商</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>性别：</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlsex" runat="server" class="j_select">
                                                                <asp:ListItem Value="-1" Selected="True">所有</asp:ListItem>
                                                                <asp:ListItem Value="0">男</asp:ListItem>
                                                                <asp:ListItem Value="1">女</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>余额：</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddl_usermoney" runat="server" class="j_select">
                                                                <asp:ListItem Value="<" Selected="True">小于</asp:ListItem>
                                                                <asp:ListItem Value="=">等于</asp:ListItem>
                                                                <asp:ListItem Value=">">大于</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="tb_userMoney" runat="server" CssClass="j_text" Width="60px" onkeypress="return only_num(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>手机：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tb_Phone" runat="server" CssClass="j_text"></asp:TextBox>


                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td align="right">
                                                            <span>订餐次数：</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlordercheck" runat="server" class="j_select">
                                                                <asp:ListItem Value="<" Selected="True">小于</asp:ListItem>
                                                                <asp:ListItem Value="=">等于</asp:ListItem>
                                                                <asp:ListItem Value=">">大于</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="tbordertimes" runat="server" CssClass="j_text" Width="60px" onkeypress="return only_num(event)"></asp:TextBox>


                                                            <asp:Button runat="server" ID="Button1" class="form-button" Text="搜索" OnClick="btSearch_Click" />

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
                                                    PageSize="15" SubmitButtonClass="flatbutton" SubmitButtonText="GO" TextAfterPageIndexBox=" 页 "
                                                    Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                                </webdiyer:AspNetPager>
                                            </div>
                                            <div id="sales_order_grid_massaction" style="clear: both;">
                                                </table>
                                            </div>
                                            <div class="grid">
                                                <div class="hor-scroll">
                                                    <table class="data" cellspacing="0" id="grid_table">

                                                        <col width="6%" />

                                                        <col width="20%" />
                                                        <col />


                                                        <thead>
                                                            <tr class="headings">

                                                                <th>
                                                                    <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                        class="sort-title">编号</span></a></span>
                                                                </th>

                                                                <th>
                                                                    <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                        <!--sort-arrow-desc-->
                                                                        <span class="sort-title">账户</span></a></span>
                                                                </th>



                                                                <th class="no-link last">
                                                                    <span class="nobr">订餐次数</span>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody class="grid_data">
                                                            <asp:Repeater ID="rptCustomerList" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class="pointer" title="">

                                                                        <td class="">
                                                                            <%# Eval("dataid")%>
                                                                        </td>
                                                                        <td class="">
                                                                            <%# Eval("name")%>&nbsp;
                                                                        </td>



                                                                        <td class=" last">

                                                                            <%# Eval("UC_ID")%>&nbsp;


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

                                        </div>

                                        <div class=" box-right">


                                            <fieldset class="AdminSearchform">
                                                <legend>第二步，赠送优惠券 </legend>

                                                <table border="0" cellpadding="0" cellspacing="0" class="condition_table" style="margin-left: 10px; float: left; margin-top: 20px; line-height:35px;">

                                                    <tr>
                                                        <td align="right">
                                                            <span>赠送优惠券：</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlbatcard" runat="server" class="j_select">
                                                            </asp:DropDownList>

                                                           

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span>验证码：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbvcode" runat="server" CssClass="j_text" Width="100"></asp:TextBox> <img src="/VCode.aspx" onclick="this.src = '/VCode.aspx?t='+new Date().getTime();"
                        alt="点击换一张" style="padding-left: 10px; cursor: pointer; vertical-align:bottom;" id="codepic" />


                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                             <asp:Button ID="Button2" runat="server" class="form-button" OnClick="send_Click" OnClientClick="showload_super();" Text="发送" />

                                                        </td>
                                                    </tr>



                                                </table>

                                            </fieldset>

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

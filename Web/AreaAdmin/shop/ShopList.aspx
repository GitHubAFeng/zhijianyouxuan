<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopList.aspx.cs" Inherits="AreaAdmin_Shop_ShopList" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="togoleft" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate">
    <meta http-equiv="expires" content="Mon, 23 Jan 1978 12:52:30 GMT">
    <title>餐馆管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../../css/showbuild.css" rel="stylesheet" type="text/css" />
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
                alert("请选择要删除的餐馆!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function HiddenList() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要隐藏的餐馆!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        function ShowList() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要显示的餐馆!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        $(document).ready(function () {

            var hffalg = $("#hfflag").val();
            if (hffalg == "") {
                $("#A1").addClass("active");
            }
            else {
                $("#A5").addClass("active");
            }
        });

        function openurl(url) {
            window.open(url);
        }

        function go(tag) {
            var v = $(tag).val() + "";
            if (v != "0") {
                //选中第一个
                $(tag).val("0");
                $(".pointer").removeClass("j_invalid");
                $(tag).parent().parent().addClass("j_invalid")
                window.open(v);
            }
        }
    </script>

    <style type="text/css">
        .j_invalid {
            background-color: #D3A5BA !important;
        }
    </style>
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
        <asp:HiddenField ID="hdDels" runat="server" />
        <asp:HiddenField runat="server" ID="hfflag" />
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
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="main-col-inner">
                                        <div id="divMessages">
                                        </div>
                                        <fieldset class="AdminSearchform">
                                            <legend>查询条件 </legend>
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
                                                        float: none;
                                                    }
                                            </style>
                                            <table border="0" cellpadding="0" cellspacing="0" class="condition_table">
                                                <tr>
                                                    <td>
                                                        <span>餐馆名称：</span>
                                                        <asp:TextBox ID="tb_TogoName" runat="server" class="input_new_style"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span>餐馆电话：</span>
                                                        <asp:TextBox ID="tb_Tel" runat="server" class="input_new_style" />
                                                    </td>
                                                    <td>
                                                        <span>餐馆地址：</span>
                                                        <asp:TextBox ID="tb_Address" runat="server" class="input_new_style" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>加盟时间：</span>
                                                        <asp:TextBox runat="server" ID="tb_Start" class="input_new_style" Width="75px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                        至
                                                    <asp:TextBox runat="server" ID="tb_End" class="input_new_style" Width="75px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span>审核：</span>
                                                        <asp:DropDownList ID="ddlstar" runat="server">
                                                            <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="审核通过"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="审核未通过"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <%--<td>
                                                        <span>所在城市：</span>
                                                        <asp:DropDownList ID="DDLArea" runat="server" Width="100" AppendDataBoundItems="true"
                                                            class="j_select">
                                                            <asp:ListItem Value="-1">所有城市</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>--%>
                                                    <td class="input_btn01">
                                                        <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click"
                                                            OnClientClick="loading(); return true;" />
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
                                                            </a><span class="separator">|</span> <a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="lbHiddenList" OnClientClick="HiddenList()" OnClick="lbHiddenList_Click">隐藏</asp:LinkButton>
                                                            </a><span class="separator">|</span> <a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="lbShowList" OnClientClick="ShowList()" OnClick="lbShowList_Click">显示</asp:LinkButton>
                                                            </a>


                                                              <span class="separator">|</span>
                                                                <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick="return confirm('确认要全部营业吗？')" OnClick="onlineall">全部营业</asp:LinkButton>

                                                             <span class="separator">|</span>
                                                                <asp:LinkButton runat="server" ID="LinkButton2"  OnClientClick="return confirm('确认要全部打烊吗？')"  OnClick="offlineall">全部打烊</asp:LinkButton>

                                                        </td>
                                                     
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="5%" />
                                                    <col width="5%" />
                                                    <col />
                                                    <col width="8%" />
                                                    <col width="7%" />
                                                    <col width="12%" />
                                                    <col width="5%" />
                                                    <col width="5%" />
                                                    <col width="5%" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"></a>
                                                                </span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">编号</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">餐馆名称(点击编辑)</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">是否隐藏</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">状态</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">餐馆电话</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">网站审核</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="billing_name" title="asc" class="not-sort"><span
                                                                    class="sort-title">佣金</span></a></span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">管理</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rtpTogolist" runat="server" OnItemCommand="rtpTogolist_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="">
                                                                        <input name="" id="_inut" value="<%# Eval("unid")%>" class="massaction-checkbox"
                                                                            type="checkbox">
                                                                        <asp:HiddenField runat="server" ID="hidDataId" Value='<%# Eval("unid")%>' />
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("unid")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <a href="ShopDetail.aspx?id=<%# Eval("unid")%>">
                                                                            <%#WebUtility.Left(Eval("Name").ToString(),20)%></a>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Convert.ToInt32(Eval("isdelete")) == 0 ? "正常" :"隐藏"  %>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# WebUtility.GetShopStatus(Eval("Status"), Eval("isbisness"))%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("comm")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# getState(Eval("Star"))%>
                                                                    </td>
                                                                    <td class="">

                                                                        <%#Eval("sn1").ToString().Trim() == "0" ? Eval("point")+"%" : Eval("point")+"元" %>
                                                                    </td>
                                                                    <td class=" last">
                                                                        <select id="ddltype" style="width: 100px" onchange="go(this)">
                                                                            <option value="0">选择操作</option>
                                                                            <%-- <option value="packagelist.aspx?tid=<%#Eval("unid") %>">套餐管理</option>--%>
                                                                            <option value="FoodSortList.aspx?tid=<%#Eval("unid") %>">分类管理</option>
                                                                            <option value="Distancepaylist.aspx?tid=<%#Eval("unid") %>">配送距离管理</option>
                                                                            <option value="FoodList.aspx?tid=<%#Eval("unid") %>">餐品管理</option>
                                                                            <option value="AddPrinter.aspx?tid=<%#Eval("unid") %>">打印机管理</option>
                                                                            <option value="OrderList.aspx?tid=<%#Eval("unid") %>">订单管理</option>
                                                                            <option value="settlecount.aspx?tid=<%#Eval("unid") %>">商家结算帐号信息</option>
                                                                            <option value="webStatisticsYear.aspx?tid=<%#Eval("unid") %>">统计</option>
                                                                            <option value="ShopLocal.aspx?tid=<%#Eval("unid") %>&cid=<%#Eval("cityid") %>">地图定位</option>
                                                                            <option value="qualification.aspx?tid=<%#Eval("unid") %>">商家资质管理</option>
                                                                        </select>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpressOrderList.aspx.cs"
    Inherits="qy_54tss_AreaAdmin_Sale_OrderListExpressOrderList" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>跑腿订单管理-
        <%= WebUtility.GetMyName() %></title>
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
                alert("请选择要删除的订单!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

    </script>

    <style type="text/css">
        .orderhistory-tishi
        {
            -moz-background-clip: border;
            -moz-background-origin: padding;
            -moz-background-size: auto auto;
            background-attachment: scroll;
            background-color: #FFF5CE;
            background-image: none;
            background-position: 0 0;
            background-repeat: repeat;
            border: 1px solid #FFE16A;
            margin-bottom: 10px;
            padding-bottom: 10px;
            padding-left: 8px;
            padding-top: 10px;
            width: 99%;
        }
        .btnew
        {
            _padding-top: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfflag" runat="server" />
    <asp:HiddenField ID="hdDels" runat="server" />
    <asp:HiddenField runat="server" ID="hfstate" Value="-1" />
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <!--banner end-->
        <!--center start-->
        <div class="middle" id="anchor-content">
            <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
                        <uc3:left runat="server" ID="adleft" />
                    </div>
                    <div class="main-col" id="content">
                        <div class="main-col-inner">
                            <div id="divMessages">
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <fieldset class="AdminSearchform">
                                        <legend>查询条件 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left"
                                            class="condition_table">
                                            <tr>
                                                <td align="right">
                                                    <span style="width: 160px;">用户名：</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="j_text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span style="width: 160px;">订单编号：</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tborder" runat="server" CssClass="j_text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span style="width: 160px;">发件电话：</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbtel" runat="server" CssClass="j_text"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left"
                                            class="condition_table">
                                            <tr>
                                                <td align="right">
                                                    <span class="span12" style="width: 160px;">时间： </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" CssClass="j_text" ID="tbStartTime" Width="80px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                    至
                                                    <asp:TextBox runat="server" ID="tbEndTime" CssClass="j_text" Width="80px" onfocus="WdatePicker({readOnly:true})"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span style="width: 160px;">订单状态：</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlOrderState" class="j_select">
                                                        <asp:ListItem Text="订单状态" Value="-2"></asp:ListItem>
                                                        <asp:ListItem Text="新增" Value="0"></asp:ListItem>
                                                         <asp:ListItem Text="已经调度" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="配送中" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="成功" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="取消" Value="6"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button runat="server" ID="Button1" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td align="right">
                                                    <span style="width: 160px;">城市：</span>
                                                </td>--%>
                                                <td align="right">
                                                    <%--<asp:TextBox ID="tbcityname" runat="server" CssClass="j_text" Width="100px" Text="所有城市"
                                                        onfocus="show_citytable()" />
                                                    <asp:HiddenField runat="server" ID="hfcid" Value="0" />
                                                    <a href="javascript:" onclick="clearcity()" class="a_clear_sel">取消选择</a> &nbsp;--%>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <div class="scott">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="27%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                            ShowPageIndex="True" PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText="GO"
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
                                                        
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <col class="a-center" width="3%" />
                                                <col width="6%" />
                                                <col width="6%" />
                                                <col width="6%" />
                                                 <col width="6%" />
                                                <col width="6%" />
                                                <col width="17%" />
                                              
                                                <col width="6%" />
                                                <col width="6%" />
                                                <col width="8%" />
                                                <thead>
                                                    <tr class="headings">
                                                        <th>
                                                            <span class="nobr"><a href="#" class="not-sort"></a></span>
                                                        </th>
                                                         <th>
                                                            <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">订单号</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">用户名</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">发件人</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">发件电话</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">运费</span></a></span>
                                                        </th>
                                                       
                                                        <th>
                                                            <span class="nobr"><a href="#" name="created_at" class="not-sort">
                                                                <!--sort-arrow-desc-->
                                                                <span class="sort-title">订单时间</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">状态</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" class="not-sort"><span class="sort-title">处理人</span></a></span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr" style="padding-top: 2px; *padding-top: 5px;">操作</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rtpOrderlist" runat="server" OnItemCommand="rtpOrderlist_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td align="center">
                                                                    <input name="" id="_inut" value="<%# Eval("dataid")%>" class="massaction-checkbox"
                                                                        type="checkbox">
                                                                </td>
                                                                 <td class="">
                                                                    <%#Eval("Orderid") %>&nbsp;
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("CustomerName") %>&nbsp;
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("UserName") %>
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("tel") %>
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("sendmoney")%>
                                                                </td>
                                                               
                                                                <td class="">
                                                                    <%#Eval("OrderTime").ToString()%>
                                                                </td>
                                                                <td class="">
                                                                    <%#WebUtility.TurnExpressOrderState(Eval("State").ToString())%>
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("writer")%>&nbsp;
                                                                </td>
                                                                <td class=" last">
                                                                    <a href='ExpressDetail.aspx?id=<%# Eval("dataid")%>'>查看</a>
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
        </div>
        <uc2:Foot runat="server" ID="FootUC" />
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliverOutputValue.aspx.cs" Inherits="qy_55tuan_Admin_DeliverOutputValue" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单量对比-<%=  WebUtility.GetMyName() %></title>
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
    <script src="../javascript/datemanager.js"></script>
    <script src="../javascript/Common.js" type="text/javascript"></script>
    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="/Admin/javascript/echarts/echarts-all.js" type="text/javascript"></script>
    <script src="/Admin/javascript/echarts/echartsApp.js?v=1124" type="text/javascript"></script>



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


        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }

        $(document).ready(function () {
            DeliverOutputValue(1);
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField runat="server" ID="hfxjson" />
        <asp:HiddenField runat="server" ID="hfyjson" />

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

                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>
                                <fieldset class="AdminSearchform">
                                    <legend>查询条件 </legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;" class="postdata">
                                        <tr>

                                            <td>
                                                <span>骑士：</span><asp:TextBox ID="tbKeyword" runat="server" CssClass="j_text" Width="120px" />&nbsp;

                                                     <asp:DropDownList ID="ddlcity" runat="server" CssClass="j_select" Width="100" AppendDataBoundItems="true">
                                                         <asp:ListItem Value="0">所有城市</asp:ListItem>
                                                     </asp:DropDownList>



                                            

                                               

                                            </td>

                                        </tr>
                                        <tr style="line-height: 40px;">

                                            <td>
                                                <span>商家：</span><asp:TextBox ID="tbshopname" runat="server" CssClass="j_text" Width="120px" />&nbsp;

                                                  

                                             
                                            </td>

                                        </tr>
                                        <tr style="line-height: 40px;">

                                            <td>
                                                <span>时间1：</span>



                                                <asp:TextBox runat="server" CssClass="inputclass" ID="tbStartTime" Text="" onfocus="WdatePicker({readOnly:true})" reg="\S" tip="时间1不能为空"
                                                    Width="75"></asp:TextBox>
                                                <asp:Label runat="server" ID="lbTemp" Text="至"></asp:Label>
                                                <asp:TextBox runat="server" ID="tbEndTime" CssClass="inputclass" Text="" onfocus="WdatePicker({readOnly:true})" reg="\S" tip="时间1不能为空"
                                                    Width="75"></asp:TextBox>


                                                <asp:Repeater runat="server" ID="rpttimesum1">
                                                    <ItemTemplate>
                                                        <span style="margin-left: 20px;">日均单量:<span style="margin-left: 20px; font-weight: bold; color: Red"><%# Eval("CountKey") %></span>单</span>
                                                       
                                                        <span style="margin-left: 20px;">平均金额:<span style="margin-left: 20px; font-weight: bold; color: Red"><%# Eval("picstr") %></span></span>
                                                    </ItemTemplate>
                                                </asp:Repeater>



                                            </td>

                                        </tr>
                                        <tr style="line-height: 40px;">

                                            <td>
                                                <span>时间2：</span>



                                                <asp:TextBox runat="server" CssClass="inputclass" ID="tbStartTime2" Text="" onfocus="WdatePicker({readOnly:true})" 
                                                    Width="75"></asp:TextBox>
                                                <asp:Label runat="server" ID="Label1" Text="至"></asp:Label>
                                                <asp:TextBox runat="server" ID="tbEndTime2" CssClass="inputclass" Text="" onfocus="WdatePicker({readOnly:true})" 
                                                    Width="75"></asp:TextBox>


                                                <asp:Repeater runat="server" ID="rpttimesum2">
                                                    <ItemTemplate>
                                                        <span style="margin-left: 20px;">日均单量:<span style="margin-left: 20px; font-weight: bold; color: Red"><%# Eval("CountKey") %></span>单</span>
                                                      
                                                        <span style="margin-left: 20px;">平均金额:<span style="margin-left: 20px; font-weight: bold; color: Red"><%# Eval("picstr") %></span></span>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </td>
                                        </tr>

                                        <tr style="line-height: 40px;">

                                            <td>
                                                <asp:Button runat="server" ID="btSearch" class="form-button" Text="开始对比" OnClick="btSearch_Click" OnClientClick="return j_submitdata('postdata')" />&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>

                                <div id="sales_order_grid_massaction" style="clear: both;">
                                </div>

                                <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none" runat="server">
                                    <li><a id="menu_1" class="tab-item-link active" href="javascript:" onclick="DeliverOutputValue(1)">
                                        <span><span class="changed"></span><span class="error"></span>订单数</span> </a>
                                    </li>
                                  
                                    <li><a id="menu_3" class="tab-item-link " href="javascript:" onclick="DeliverOutputValue(3)"><span>
                                        <span class="changed"></span><span class="error"></span>金额</span> </a></li>

                                </ul>

                                <div class="grid">
                                    <div class="hor-scroll">
                                        <div id="cancaschars" style="height: 400px;"></div>
                                    </div>
                                </div>


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


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopPromotion.aspx.cs" Inherits="Admin_shop_shopPromotion"
    ValidateRequest="false" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家促销活动-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->
    <link href="../../javascript/jbox/Skins/jbox.css" rel="stylesheet" />

    <script src="../javascript/jquery-1.7.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>


    <script src="../javascript/Common.js" type="text/javascript"></script>
    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="../javascript/QueryChina.js" type="text/javascript"></script>
    <script src="../../javascript/jbox/jquery.jBox-2.3.min.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });
        $(document).ready(function () {
            shoppromotionchange();
        });


        //优惠类型变化 
        function shoppromotionchange() {
            var discounttype = $("input[name='rblshopptype']:checked").val();
            $(".promotionitem").hide();
            $(".rblshopptype" + discounttype).show();
        }

        function adddiscount(Discountid) {
            var title = Discountid == 0 ? "添加促销活动" : "编辑促销活动";
            var url = "shopPromotiondetail.aspx?id=" + Discountid + "&tid=" + request("tid");


            $.jBox.open("get:" + url, title, 550, 400, { buttons: { '关闭': true } });
        }


        //优惠类型变化 
        function discountchange() {

            var discounttype = $("input[name='tbptype']:checked").val();
            $(".payitem").hide();
            $(".tbptype" + discounttype).show();
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">

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
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>

                                <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none">
                                    <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" class="tab-item-link ">
                                        <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                    </li>
                                    <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                    <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                    <li><a href="Distancepaylist.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                    <li><a href="ShopLocal.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>

                                    <li><a href="AddPrinter.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                        <span class="changed"></span><span class="error"></span>打印机</span> </a></li>


                                    <li><a href="shopPromotion.aspx?tid=<%= Request["tid"] %>" class="tab-item-link active"><span>
                                        <span class="changed"></span><span class="error"></span>促销活动</span> </a></li>

                                </ul>

                                <div style="visibility: visible;" class="content-header">
                                    <h3 class="icon-head head-customer">
                                        <asp:Label runat="server" ID="pageType">促销活动</asp:Label></h3>
                                    <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                        <p style="" class="content-buttons form-buttons">
                                        </p>
                                    </div>

                                </div>



                                <!--start-->

                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-billing-address">促销类型</h4>
                                            </div>
                                            <fieldset class="np">
                                                <div class="order-address" id="order-billing_address_fields">
                                                    <div class="content">
                                                        <div class="hor-scroll">
                                                            <table class="form-list " cellspacing="0" style="width: 48%; float: left;">
                                                                <tbody>
                                                                    <tr>

                                                                        <td class="value">
                                                                            <asp:RadioButtonList runat="server" ID="rblshopptype" RepeatDirection="Horizontal" onchange="shoppromotionchange()">
                                                                                <asp:ListItem Value="0">无</asp:ListItem>
                                                                                <asp:ListItem Value="10">商家独立促销</asp:ListItem>
                                                                                <asp:ListItem Value="20">平台促销</asp:ListItem>

                                                                            </asp:RadioButtonList>

                                                                            <div class=" notice">选择“商家独立促销”需要单独添加相关促销规则，选择“平台促销”后，请勾选参与的活动</div>
                                                                        </td>

                                                                    </tr>



                                                                    <tr runat="server" id="webpromotionbox" class="promotionitem rblshopptype20">
                                                                        
                                                                        <td class="value">
                                                                            <asp:CheckBoxList runat="server" ID="tbPEnd" RepeatDirection="Horizontal" RepeatColumns="1">
                                                                            </asp:CheckBoxList>
                                                                        </td>
                                                                    </tr>


                                                                    <tr>

                                                                        <td class="value">
                                                                            <asp:Button ID="btsavetype" runat="server" Style="margin-left: 120px;" CssClass="button_1" OnClick="Savetype_Click"
                                                                                Text="保存"></asp:Button>
                                                                        </td>

                                                                    </tr>



                                                                </tbody>
                                                            </table>




                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>




                                <div class="entry-edit promotionitem rblshopptype10" runat="server" id="promotionbox">
                                    <div id="" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-billing-address">商家独立促销
                                                    <input type="button" value="添加" class="button_1" style="margin-left: 20px;" onclick="adddiscount(0)" /></h4>
                                            </div>
                                            <fieldset class="np">
                                                <div class="order-address" id="">
                                                    <div class="content">


                                                        <div class="grid">
                                                            <div class="hor-scroll">

                                                                <table class="data" cellspacing="0" id="grid_table">

                                                                    <col width="25%" />
                                                                    <col />
                                                                    <col width="10%" />
                                                                    <col width="20%" />
                                                                    <thead>
                                                                        <tr class="headings">

                                                                            <th>
                                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                                    class="sort-title">标题</span></a></span>
                                                                            </th>
                                                                            <th>
                                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                                                    <!--sort-arrow-desc-->
                                                                                    <span class="sort-title">有效期</span></a></span>
                                                                            </th>
                                                                            <th>
                                                                                <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort"><span
                                                                                    class="sort-title">状态</span></a></span>
                                                                            </th>

                                                                            <th class="no-link last">
                                                                                <span class="nobr">操作</span>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody class="grid_data">
                                                                        <asp:Repeater ID="rtpTogolist" runat="server" OnItemCommand="rptUserList_ItemCommand">
                                                                            <ItemTemplate>
                                                                                <tr class="pointer" title="">

                                                                                    <td class="">
                                                                                        <%#WebUtility.Left(Eval("revevar1").ToString(), 15)%>
                                                                                    </td>

                                                                                    <td class="">
                                                                                        <%# Convert.ToDateTime(Eval("startdate")).ToShortDateString()%>~<%# Convert.ToDateTime(Eval("enddate")).ToShortDateString()%>
                                                                                    </td>
                                                                                    <td class="">
                                                                                        <%#Convert.ToInt32(Eval("isopen")) == 1 ? "开启" : "关闭"%>
                                                                                    </td>
                                                                                    <td class=" last">
                                                                                        <a href='javascript:' onclick="adddiscount(<%#Eval("pid") %>)">编辑</a>  | 
                                                                         <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("pid")%>' OnClientClick="return DelConfirm();"
                                                                             runat="server" ID="del">删除</asp:LinkButton></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>



                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <!--end-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--foot start-->
        <uc2:Foot runat="server" ID="FootUC" />
        <!--foot end-->
    </form>
</body>
</html>



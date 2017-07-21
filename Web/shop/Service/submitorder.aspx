<%@ Page Language="C#" AutoEventWireup="true" CodeFile="submitorder.aspx.cs" Inherits="qy_54tss_Ashopdmin_Service_submitorder" %>

<%@ Register Src="CrmTop.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>客服系统-<%= WebUtility.GetMyName()%></title>
    <link type="text/css" rel="stylesheet" href="css/indis_style.css" />

    <script src="../../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        //显示这个分类的菜,0表示全部
        function showsort(sortid) {
            $(".smooth").removeClass("class_hover");
            $("#j_s_" + sortid).addClass("class_hover");
            if (sortid != 0) {
                $(".sortitem").hide();
                $("._" + sortid).show();
            }
            else {
                $(".sortitem").show();
            }
        }

        $(document).ready(function() {



        })

        function addfood() {
            var id = Request("id");
            window.location = "showMenu.aspx?id=" + id + "&tel=" + Request("tel");
        }
       
    </script>

    <style type="text/css">
        .noseeitem {
            display: none;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="wrap">
            <asp:HiddenField runat="server" ID="hftid" />
            <asp:HiddenField runat="server" ID="hidTogoName" />
            <asp:HiddenField runat="server" ID="hfcode" />
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="infaq_con">
                <div class="indis_right" style="width: 100%">
                    <div class="indis_right_top">
                        <div class="infaq_list_title">
                            <input type="button" onclick="addfood('OrderCrm.aspx?type=change&tel=<%= Request["tel"] %>    ')"
                                value="继续点餐" style="margin: 10px;" class="user_deng" />
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <div class="disy_type_title ">
                                        <span class="more"></span>会员信息
                                    </div>
                                    <div class="infaq_infor">
                                        <p style="margin-top: 5px;">
                                            姓名：<asp:TextBox runat="server" ID="tbuname" CssClass="j_text noaccessitem"></asp:TextBox>
                                        </p>
                                        <p>
                                            电话：<asp:TextBox runat="server" ID="tbtel" CssClass="j_text noaccessitem"></asp:TextBox>
                                        </p>
                                        <p style="display: none;">
                                            楼宇：<asp:TextBox runat="server" ID="tbbuildingname" CssClass="j_text noaccessitem"
                                                Enabled="false"></asp:TextBox>
                                        </p>
                                        <p>
                                            地址：<asp:TextBox runat="server" ID="tbaddress" CssClass="j_text keyaddress noaccessitem"
                                                Width="210px"></asp:TextBox>
                                        </p>

                                        <p>
                                            <span class="float_l" style="margin-right: 3px;">时间：</span>

                                            <asp:DropDownList runat="server" ID="ddltime" Style="width: 80px;">
                                                <asp:ListItem> </asp:ListItem>
                                            </asp:DropDownList>

                                        </p>
                                        <p style="display:none;">
                                            就餐人数：<asp:TextBox runat="server" ID="tbpeople" CssClass="j_text keyaddress noaccessitem"
                                                Width="60" Text="1"></asp:TextBox>人
                                        </p>
                                        <p>
                                            备注：<input class="j_text w_140p" name="" type="text" runat="server" id="tbremark"
                                                style="width: 360px;" />
                                        </p>
                                    </div>
                                    <div class="dish_type_unit">
                                        <asp:Repeater runat="server" ID="rptFoodSort">
                                            <ItemTemplate>
                                                <div class="sortitem">
                                                    <div class="disy_type_title ">
                                                        <span class="more"></span>
                                                        <%# Eval("togoname") %>
                                                    </div>
                                                    <ul class="hdish_ul_sub">
                                                        <li><span style="width: 35%">菜品</span> <span style="width: 15%">单价</span> <span style="width: 10%">数量</span> <span style="width: 15%">小计</span><span style="width: 15%">&nbsp;</span>
                                                        </li>
                                                        <asp:Repeater runat="server" ID="rptFoodList" DataSource='<%# Eval("ItemList") %>'>
                                                            <ItemTemplate>
                                                                <li><span style="width: 35%"><a href="javascript:void(0)" title="<%#Eval("PName")%>">
                                                                    <%# WebUtility.Left(Eval("PName"), 12)%></a></span> <span style="width: 15%">￥<%#  Convert.ToDecimal(Eval("PPrice")) %></span>
                                                                    <span style="width: 10%">
                                                                        <%#Eval("pnum")%></span> <span style="width: 15%">￥<%#  Convert.ToDecimal(Eval("PPrice")) * Convert.ToInt32(Eval("pnum"))%></span>
                                                                    <span style="width: 15%">&nbsp;</span>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="sortitem">
                                            <ul class="hdish_ul_sub">
                                                <li>
                                                    
                                                     <span style="width: 23%">打包费：<label runat="server" id="lbpackage"></label>元</span>
                                                    <span style="width: 23%">送餐费：<label runat="server" id="lbsendmony"></label>元</span>
                                                    <span style="width: 23%">份数：<label runat="server" id="lbnum"></label></span> <span
                                                        style="width: 23%">总金额：<label runat="server" id="lballmoney"></label>元</span>
                                                </li>
                                            </ul>
                                        </div>
                                        <div style="margin: 10px; text-align: center">
                                            <asp:Button runat="server" ID="tbadd" OnClick="add_click" Text="提交订单" CssClass="submit_order_btn"
                                                OnClientClick='hideload_super();' />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showexpressorder.aspx.cs" Inherits="Html5.showexpressorder" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <script src="javascript/jquery.js"></script>

</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="myexpresslist.aspx" data-ajax="false" id="back" class=" top_left"></a>
            <h1>订单详情</h1>

        </div>

        <asp:Repeater runat="server" ID="rptorder">
            <ItemTemplate>
                <div class="container">
                    <ul class="my_order_list">
                        <li>
                            <div class="order-tit">
                                <span class="time" style="	font-weight: bold;"><strong>取件信息</strong></span>

                            </div>

                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>取件地址：</strong>
                                    <label class="w_txt">
                                        <%# Eval("Address")%>
                                    </label>
                                </span>
                            </div>
                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>取件电话：</strong>
                                    <label class="w_txt">
                                        <%# Eval("Tel")%>
                                    </label>
                                </span>
                            </div>
                            <div class="order-tit" style="border-bottom: none;">
                                <span class="time" data-icon="false">
                                    <strong>联系人：</strong>
                                    <label class="w_txt">
                                        <%# Eval("UserName")%>
                                    </label>
                                </span>
                            </div>

                        </li>
                    </ul>

                    <ul class="my_order_list">
                        <li>
                            <div class="order-tit">
                                <span class="time" style="	font-weight: bold;"><strong>收件信息</strong></span>

                            </div>

                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>收件地址：</strong>
                                    <label class="w_txt">
                                        <%# Eval("Oorderid")%>
                                    </label>
                                </span>
                            </div>
                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>收件电话：</strong>
                                    <label class="w_txt">
                                        <%# Eval("ReveVar")%>
                                    </label>
                                </span>
                            </div>
                            <div class="order-tit" style="border-bottom: none;">
                                <span class="time" data-icon="false">
                                    <strong>联系人：</strong>
                                    <label class="w_txt">
                                        <%# Eval("callmsg")%>
                                    </label>
                                </span>
                            </div>

                        </li>
                    </ul>


                    <ul class="my_order_list">
                        <li>
                            <div class="order-tit">
                                <span class="time" style="	font-weight: bold;"><strong>其他信息</strong></span>
                            </div>

                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>订单号：</strong>
                                    <label class="w_txt">
                                        <%# Eval("orderid") %>[<%# Hangjing.WebCommon.WebHelper.TurnExpressOrderState(Eval("State").ToString())%>]
                                    </label>

                                </span>
                            </div>


                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>取件时间 ：</strong>

                                    <label class="w_txt">
                                        <%# Eval("SentTime")%>
                                    </label>
                                </span>
                            </div>

                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>备注：</strong>

                                    <label class="w_txt">
                                        <%# Eval("Remark")%>
                                    </label>
                                </span>
                            </div>

                             <div class="order-tit" style="<%# Convert.ToInt32(Eval("callcount")) == 0 ? "display:none;": ""  %>">
                                <span class="time" data-icon="false">
                                    <strong>商品金额：</strong>
                                    <label class="w_txt">
                                        <%# Eval("TotalPrice")%>元
                                    </label>

                                </span>
                            </div>

                            <div class="order-tit" style="border:none;">
                                <span class="time" data-icon="false">
                                    <strong>配送费：</strong>
                                    <label class="w_txt">
                                        <%# Eval("sendmoney")%>元
                                    </label>

                                </span>
                            </div>

                        </li>
                    </ul>


                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</body>
</html>

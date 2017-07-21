<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printorder.aspx.cs" Inherits="Admin_order_printorder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单打印-商家中心-<%= WebUtility.GetWebName() %></title>

    <script type="text/javascript">
        window.print();
    </script>

    <style type="text/css">
        body {
            font-size: 12px;
            line-height: 18px;
        }

        h1, ul, li, p {
            font-size: 12px;
            padding: 0;
            margin: 0;
        }

        h1 {
            text-align: center;
            line-height: 20px;
        }

        p {
            padding-left: 5px;
        }

        td {
            padding: 0px 2px;
        }

        li {
            list-style: none;
            padding-left: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater runat="server" ID="rptorder">
            <ItemTemplate>
                <div style="width: 210px;">
                    <h1>
                        <%# Eval("togoname") %></h1>
                    <ul style="border-bottom: 1px dashed #666666; margin-bottom: 5px; padding-bottom: 5px;">
                        <li><span>单号:</span><label><%# Eval("orderid") %></label></li>
                        <li><span>订单时间:</span><label><%# Eval("OrderDateTime")%></label></li>
                        <li><span>用户名:</span><label><%# Eval("CustomerName")%></label></li>
                        <li><span>收货人:</span><label><%# Eval("OrderRcver")%></label></li>
                        <li><span>送餐时间:</span><label><%# Eval("SendTime")%></label></li>
                        <li><span>手机:</span><label><%# Eval("CallPhoneNo")%></label></li>
                        <li><span>备注:</span><label><%# Eval("OrderAttach")%></label></li>
                    </ul>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tbody>
                            <tr>
                                <th>商品名称
                                </th>
                                <th>数量
                                </th>
                                <th>单价
                                </th>
                                <th>小计
                                </th>
                            </tr>
                            <asp:Repeater runat="server" ID="Repeater1" DataSource='<%# getproduct(Eval("orderid")) %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("foodname")%>
                                        </td>
                                        <td class="">
                                            <%#Eval("FCounts")%>
                                        </td>
                                        <td class="">￥<%# Convert.ToDecimal( Eval("foodPrice")) %>
                                        </td>
                                        <td>￥<%# Convert.ToDecimal(Eval("foodPrice")) * Convert.ToInt32(Eval("FCounts"))%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <ul style="border-top: 1px dashed #666666; margin-top: 5px; padding-top: 5px;">
                        <li>送餐费：<%# Eval("SendFee")%>元</li>
                        <li>金额：<%# Eval("OrderSums")%>元</li>
                        <li>类型：<%# Eval("ReveInt2").ToString() == "0"?"外卖":"堂食"%></li>
                        <li>支付方式：<%#  WebUtility.TurnPayModel(Eval("paymode").ToString()) %></li>
                        <li>支付状态：<%# WebUtility.TurnPayState(Eval("paystate")) %></li>
                        <li>收货地址：<%# Eval("AddressText")%></li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>

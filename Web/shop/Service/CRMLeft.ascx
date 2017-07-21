<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CRMLeft.ascx.cs" Inherits="shopqy_54tss_Admin_Service_CRMLeft" %>


<script src="javascript/togoshoppingcart.js" type="text/javascript"></script>

<div class="infaq_left">
    <h4>
        电话弹屏界面</h4>
    <div class="infaq_infor">
        <p>
            姓名：<asp:TextBox runat="server" ID="tbuname" CssClass="j_text noaccessitem"></asp:TextBox></p>
        <p>
            电话：
            <asp:TextBox runat="server" ID="tbtel" CssClass="j_text noaccessitem" Width="100px"></asp:TextBox>&nbsp;
            <a href="javascript:getmyaddress()" class="infaq_address_sure_btn">获取地址</a>  
            <label runat="server" id="lbtelmsg" style="color: Red; font-weight: bold; "></label>
        </p>
        <p>
            地址：<asp:TextBox runat="server" ID="tbaddress" CssClass="j_text keyaddress noaccessitem"
                Width="180"></asp:TextBox>
            <asp:HiddenField runat="server" ID="add_dataid" Value="0" />
        </p>
        <p class=" noseeitem" style=" text-align:center;">
             <a href="javascript:j_getGeocoder()"
                class="infaq_address_sure_btn">搜索</a></p>
        <div class=" noseeitem">
            <ul class="myadd_ul">
                <asp:Repeater runat="server" ID="rptaddress">
                    <ItemTemplate>
                        <li>
                           <input type="radio" id="addrlist_<%# Eval("dataid") %>" name="addressradio1" <%#(Container.ItemIndex+1).ToString() == "1" ? "checked" : "" %>
                                class="radio1 <%#(Container.ItemIndex+1).ToString() == "1" ? "first_addr" : "" %>"
                                onclick="setaddress(this);" value="<%# Eval("Receiver") %>^<%# Eval("Address")%>^<%# Eval("lat") %>^<%# Eval("lng")%>^<%# Eval("dataid") %>^<%# Eval("BuildingID") %>" />
                           <label for="addrlist_<%# Eval("dataid") %>"><%# Eval("Receiver")%>&nbsp;<%# Eval("Address") %></label>


                           
                            
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <h5 class=" noseeitem">
            <a href="javascript:add_addr()">添加地址</a>&nbsp;&nbsp;&nbsp;<a href="javascript:edit_addr()">编辑地址</a>&nbsp;&nbsp;&nbsp;<a
                href="javascript:del_addr()">删除地址</a></h5>
    </div>
    <div class="infaq_order_list">
        <h6>
            餐品明细：</h6>
        <div class="infaq_shopcar">
            <div  id="cartContent"></div>
            <p align="center">
                <input type="button" value="清空餐盒" onclick="deleteallcart();" class="clear_box_btn" />
                <input type="button" value="提交订单" onclick="CheckCart();" class="submit_order_btn" />
            </p>
        </div>
    </div>
    <div class="infaq_order_list">
        <h6>
            最近的一次历史订单：</h6>
        <div class="infaq_history_order">
            <asp:Repeater runat="server" ID="rptOrderList1">
                <ItemTemplate>
                    <dl>
                        <dt>－[<%# Convert.ToDateTime(Eval("OrderDateTime")).ToString("MM-dd HH:mm")%>]</dt>
                        <dd>
                            <span class="orange">
                                <%# WebUtility.Left(Eval("OrderRcver"), 4)%></span>在<span class="orange"><a href="javascript:void(0)">【<%#Eval("togoname") %>】</a></span>点了餐。消费<span
                                    class="orange"><%#Eval("OrderSums")%></span>元。</dd>
                    </dl>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function() {
        ListCart();
    })
</script>


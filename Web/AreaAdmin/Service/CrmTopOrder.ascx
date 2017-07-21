<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrmTopOrder.ascx.cs" Inherits="qy_54tss_AreaAdmin_Service_CrmTopOrder" %>

<script src="../../javascript/jCommon.js" type="text/javascript"></script>

<div class="indis_top">
    <a href="../../index.aspx" class="float_l padding_l5">
        <img src="../../images/logo.png" style=" height:60px; width:141" /></a><span class="top_title">订单管理中心</span><span class="fa margin_t3">操作员：<label
            runat="server" id="lbadminname"></label></span><p>
                <label runat="server" id="snDate">
                </label>
            </p>
    <p>
        <a id="callback" runat="server" href="../basic.aspx">返回后台</a><asp:Literal ID="Litorderlist" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;<asp:Literal ID="lithref"
            runat="server"></asp:Literal>
    </p>
</div>



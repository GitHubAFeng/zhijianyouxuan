<%@ Page Language="C#" AutoEventWireup="true" CodeFile="buildtable1.aspx.cs" Inherits="buildtable1" %>

<ul>
    <asp:repeater id="rptTogolist" runat="server">
        <ItemTemplate>
    <li><a href="#"><%#Eval("Name")%></a><label></label></li>
    </ItemTemplate>
    </asp:repeater>
</ul>

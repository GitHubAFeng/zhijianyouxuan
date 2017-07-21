<%@ Page Language="C#" AutoEventWireup="true" CodeFile="buildtable_1.aspx.cs" Inherits="qy_55tuan_Admin_buildtable_1ddd" %>

<link href="<%=WebUtility.GetUrl("~/Admin/css/building.css")%>" rel="stylesheet"
    type="text/css" />

<script src="<%=WebUtility.GetUrl("~/Admin/javascript/jquery-1.7.min.js")%>" type="text/javascript"></script>

<script src="<%=WebUtility.GetUrl("~/Admin/javascript/getcityorsecion.js")%>" type="text/javascript"></script>

<div id="tabpopup1_citytab" style="width: 702px; margin-left: 0px; background-color: rgb(255, 255, 255);
    display: block; background: #fff; position: absolute;">
    <div style="height: 30px; line-height: 30px; background: #E7F1CC;">
        <span style="float: right; padding-right: 10px;">
            <a href="javascript:closediv();" class=" tabclose ">关闭</a>
        </span>
        <span style="padding-left: 20px">请选择城市</span></div>

    <script type="text/javascript">

        function enterIn(evt) {
            if (evt.keyCode == 13 || evt.which == 13) {
                if (navigator.userAgent.indexOf("MSIE") > 0) {
                    getkeyFix_city(1);
                    event.returnValue = false;
                }
                if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
                    getkeyFix_city(1);
                    return false;
                }
                return false;
            }
            else {

            }
        }

    </script>

    <div class="ContentboxTab">
        <div class="solid_line">
        </div>
        <div class="choice_csroll">
            <div id="div_citylist">
                <asp:repeater runat="server" id="rptcity">
                                    <ItemTemplate>
                                    <div class="building_list"><a href="javascript:select_mycity('<%# Eval("cname") %>' , <%# Eval("cid") %>, '0', '0')"><%# Eval("cname") %></a></div>
                                    </ItemTemplate>
                                </asp:repeater>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</div>

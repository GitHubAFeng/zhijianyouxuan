<%@ Page Language="C#" AutoEventWireup="true" CodeFile="citys.aspx.cs" Inherits="citys" %>

<%@ Register Src="Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="Foot.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择您的所在的城市 -
        <%= WebUtility.GetWebName() %></title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="css/city.css" rel="stylesheet" type="text/css" />
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />
    <meta http-equiv="Cache-Control" content="max-age=7200" />

    <script src="javascript/jquery-1.7.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hfcityjson" Value="1" />
        <uc1:TogoBanner runat="server" ID="TogoBanner1" />
        <div class="wrap">
            <div class="topcity" style="min-height: 400px;">
                <div class="index_bo">
                    <div class="citySelect">
                        <div class="city_tit">
                            <h2>选择你所在的城市</h2>
                        </div>
                        <div class="citysCon">
                            <asp:Repeater runat="server" ID="rptcity">
                                <ItemTemplate>
                                    <dl id="clist" class="clearfix">
                                        <dt>
                                            <%# Eval("firstletter")%>.</dt>
                                        <dd>
                                            <asp:Repeater runat="server" ID="rptsubcity" DataSource='<%# Eval("citylist")%>'>
                                                <ItemTemplate>
                                                    <a href="javascript:" id="city_<%#Eval("cid")%>" onclick="sel_city(<%#Eval("cid")%>,'<%# Eval("cname")%>','<%# Eval("ReveVar")%>');return false;" style="margin-left: 5px; margin-right: 5px;">
                                                        <%# Eval("cname")%></a>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </dd>
                                    </dl>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc2:Footer runat="server" ID="foot" />
    </form>
</body>
</html>

<script src="javascript/jCommon.js" type="text/javascript"></script>

<script type="text/javascript">


    //选择城市，跳转+cookie
    function sel_city(cid, cname, pinyin) {
        handlecookie("user_cityid", cid, { expires: 30, path: "/", secure: false });
        window.location = "index.aspx";
        return false;
    }

</script>


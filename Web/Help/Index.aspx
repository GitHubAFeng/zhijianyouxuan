<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Help_Index" %>

<%@ Register Src="~/Banner.ascx" TagName="banner" TagPrefix="top" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>帮助中心-<%= SectionProxyData.GetSetValue(3)%>
    </title>
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/help.css" rel="stylesheet" />
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />
    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../javascript/jCommon.js" type="text/javascript"></script>
    <script type="text/javascript">
    
        $(document).ready(function(){
            initnav(8);
        } 
    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <!--top start-->
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <!-- top end-->
        <div id="help" class="wrap">
            <div class="hplace_bg">
                <span class="hplace_right"></span>
                <span class="color_1 hplace_house" id="dangqian" runat="server"><a href="/Index.aspx">首页</a> >>
                <a href="#">帮助中心</a></span>
            </div>
            <div class="help_con">
                <div class="about_leftsider">
                    <div class="about_leftsider_ul">
                        <h6 class="right_navbg_04">帮助中心</h6>
                        <dl>
                            <asp:Repeater ID="rptClass" runat="server">
                                <ItemTemplate>
                                    <dt class="<%#(Container.ItemIndex+1).ToString() =="1" ? "" :"" %>"> <i class="rightarrow"></i><a href="index.aspx?sid=<%# Eval("id") %>">
                                       <%# Eval("name") %></a></dt>
                                    <asp:Repeater ID="rptartic" runat="server" DataSource='<%# getsubartic(Eval("id")) %>'>
                                        <ItemTemplate>
                                            <dd>
                                                <a href="index.aspx?id=<%# Eval("dataid") %>">
                                                    <%# Eval("title") %></a></dd>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </dl>
                    </div>
                </div>
                <div class="about_rightsider" style="background-color: #fff;">
                    <h6 class="right_navbg_04" id="divtitmle" runat="server">关于我们</h6>
                    <div class="about_rightsider_intro" runat="server" id="divinfo">
                        <p>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

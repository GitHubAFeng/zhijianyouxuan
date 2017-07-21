<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommentDetail.aspx.cs" Inherits="shop_CommentDetail" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>评论详细信息-<%= WebUtility.GetWebName()%>
    </title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function chekckdata() {
            var v = $("#tbrcontent").val() + "";
            if (v == "") {
                jtip("请输入回复内容。");
                return false;
            }
            return true;
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">

        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="shop_main">
                                <div class="main-content">
                                    <%--<h1 class="topbg">查看评论</h1>--%>
                                    <div class="review_shop">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="review_shop_table">
                                            <tr>
                                                <th width="80%">内容
                                                </th>
                                                <th>评论者
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p class="rate">
                                                        <asp:Literal ID="LitContent" runat="server"></asp:Literal>
                                                    </p>
                                                    <span class="date_t">
                                                        <asp:Label ID="lbtime" runat="server"></asp:Label></span>
                                                </td>
                                                <td>
                                                    <span class="orange_se">
                                                        <asp:Label ID="lbUsername" runat="server"></asp:Label></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox runat="server" ID="tbrcontent" TextMode="MultiLine" Width="510px" Height="60px" CssClass="area"></asp:TextBox>
                                                    <asp:Button Text="回复" runat="server" ID="Button2" class="subBtn" OnClientClick="return chekckdata();"
                                                        OnClick="Save_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </form>
</body>
</html>

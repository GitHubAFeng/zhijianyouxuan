<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyInfo.aspx.cs" Inherits="UserHome_MyInfo" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员中心-个人信息-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function setIcon(path) {
            document.getElementById("Left_imghead").src = path;
        }
    </script>

</head>


<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp_con">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <uc2:LeftBanner runat="server" ID="Left" />
                    <div class="rightmenu_cont">
                        <div class="rightmenu_cen">
                            <h1 class="topbg">修改资料</h1>
                            <div class="usermima">
                                <ul>
                                    <li><span class="left_span">手机：</span>
                                        <asp:TextBox runat="server" ID="tbTel" size="25" class="text" MaxLength="11" reg="^1[0-9]{10}$" Enabled="false" tip="手机号码格式错误" BorderStyle="None"></asp:TextBox>
                                    </li>
                                    <li><span class="left_span">昵称：</span>
                                        <asp:TextBox runat="server" ID="tbname" size="25" class="text" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li><span class="left_span">邮箱：</span>
                                        <asp:TextBox runat="server" ID="tbemail" size="25"
                                            class="text"></asp:TextBox>
                                    </li>
                                    <li><span class="left_span">Q Q：</span>
                                        <asp:TextBox runat="server" ID="tbQQ" size="25" class="text" MaxLength="15"></asp:TextBox>
                                    </li>
                                    <li><span class="left_span">姓名：</span>
                                        <asp:TextBox runat="server" ID="tbRealName" size="25" class="text" MaxLength="30"></asp:TextBox>
                                    </li>
                                    <li><span class="left_span">性别：</span>
                                        <asp:RadioButtonList runat="server" ID="rblsex" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="男"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="女"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </li>
                                    <li><span class="left_span">生日：</span> 年
                                        <asp:TextBox runat="server" ID="tbbirthday"
                                            class="text" MaxLength="15" Width="50px"></asp:TextBox>
                                        月
                                        <asp:TextBox runat="server" ID="tbmonth" class="text" MaxLength="15" Width="50px"></asp:TextBox>
                                        日
                                        <asp:TextBox runat="server" ID="tbday" class="text" MaxLength="15" Width="50px"></asp:TextBox>
                                    </li>
                                    <li><span class="left_span">头像：</span>
                                        <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                        <asp:HiddenField ID="FolderType" Value="0" runat="server" />
                                        <asp:HiddenField ID="WaterType" Value="0" runat="server" />
                                        <div style="float: left">
                                            <img border="0" src="Images/unopic.gif" id="ImgUrl" alt="" style="width: 70px; height: 70px;"
                                                runat="server" />
                                        </div>
                                        <div style="float: left; margin-left: 0px;">
                                            <input id="txtupload" type="button" value="上传" style="margin-left: 20px; font-size: 12px;"
                                                onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" />请上传52*52的图片<br />
                                            <div id="rowTest" style="display: none">
                                                <iframe name="tag" src="../admin/upfile/Upload.html?Links" style="width: 342px; height: 70px"
                                                    frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
                                            </div>
                                            <div id="Upload">
                                            </div>
                                        </div>
                                    </li>
                                    <li style="padding-left:100px;">
                                        <asp:Button Text="保存信息" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn"
                                            OnClientClick="return j_submitdata('usermima');" />
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

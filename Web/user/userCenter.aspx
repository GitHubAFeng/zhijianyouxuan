<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userCenter.aspx.cs" Inherits="UserHome_userCenter" %>

<%@ Register Src="~/Banner.ascx" TagName="TopBanner" TagPrefix="uc1" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register Src="~/Foot.ascx" TagName="Foot" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-个人信息-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>

    <script src="javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function setIcon(path) {
            document.getElementById("Left_imgusericon").src = path;
        }
    </script>

    <style type="text/css">
        .rightmenu_cont
        {
            background-color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:TopBanner runat="server" ID="Top" />
    <div class="warp_con">
        <uc2:LeftBanner runat="server" ID="Left" />
        <div class="rightmenu_cont">
            <h1 class="topbg">
                欢迎回来</h1>
            <div class="usermima">
                <ul>
                    <li><span class="left_span">真实姓名：</span>
                        <asp:TextBox runat="server" ID="tbRealName" size="25" class="input_on" MaxLength="30"></asp:TextBox>
                    </li>
                    <li><span class="left_span">昵称：</span>
                        <asp:TextBox runat="server" ID="tbname" size="25" class="input_on" MaxLength="10"
                            ReadOnly="true"></asp:TextBox>
                    </li>
                    <li><span class="left_span">手机：</span>
                        <asp:TextBox runat="server" ID="tbTel" size="25" class="input_on" MaxLength="12"></asp:TextBox>
                    </li>
                    <li><span class="left_span">Q Q：</span>
                        <asp:TextBox runat="server" ID="tbQQ" size="25" class="input_on" MaxLength="12"></asp:TextBox>
                    </li>
                    <li><span class="left_span">所在地区：</span>
                        <asp:TextBox runat="server" ID="tbMsn" size="25" class="input_on" MaxLength="20"></asp:TextBox>
                    </li>
                    <li style="width: 600px;"><span class="left_span">头像：</span>
                        <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                        <asp:HiddenField ID="FolderType" Value="0" runat="server" />
                        <asp:HiddenField ID="WaterType" Value="0" runat="server" />
                        <img border="0" src="Images/unopic.gif" id="ImgUrl" alt="" style="width: 52px; height: 52px;
                            float: left" runat="server" />
                        <div style="float: left; margin-left:30px;">
                            <input id="txtupload" type="button" value="上传" style=" margin-left:20px;" onclick="return document.getElementById('rowTest').style.display='block';return txtupload_onclick();" />请上传52*52的图片<br />
                            <div id="rowTest" style="display: none">
                                <iframe name="tag" src="../admin/upfile/Upload.html?Links" style="width: 442px; height: 100px"
                                    frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight">
                                </iframe>
                            </div>
                            <div id="Upload">
                            </div>
                        </div>
                    </li>
                  
                </ul>
            </div>
        </div>
    </div>
    <uc3:Foot runat="server" ID="foot" />
    </form>
</body>
</html>

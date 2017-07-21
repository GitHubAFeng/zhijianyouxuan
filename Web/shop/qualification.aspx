<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qualification.aspx.cs" Inherits="shop_qualification" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家基本信息-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="../Admin/css/time.css" rel="stylesheet" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Admin/javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../Admin/javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/time.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="js/getbuild.js" type="text/javascript"></script>

    <script src="js/togobuilding.js" type="text/javascript"></script>

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <script src="../Admin/javascript/QueryChina.js" type="text/javascript"></script>

    <script type="text/javascript">

        function table_display2(t_id) {
            if ($("#" + t_id).css("display") == "none") {
                $("#" + t_id).attr("style", "display:");
            }
            else {
                $("#" + t_id).attr("style", "display:none");
            }
        }

        function DeletePic(t_id) {
            document.getElementById("ppic" + t_id).src = "../../images/nopic_02.jpg";
            document.getElementById("pic" + t_id).value = "0";
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidBuilding" />
        <asp:HiddenField runat="server" ID="BuildingGrade" />

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
                                <div class=" main-content ">
                                    <div class="shop_menu">
                                        <ul>
                                            <li><a href="myshop.aspx">修改资料</a></li>
                                            <li><a href="TogoLocal.aspx">商家定位</a></li>
                                            <li><a href="SetStatus.aspx">状态管理</a></li>
                                            <li><a href="myPromotion.aspx">促销活动</a></li>
                                            <li class="cur"><a href="qualification.aspx">商家资质管理</a></li>
                                        </ul>
                                    </div>
                                    <div class="usermima">
                                        <ul>
                                            <li><span class="left_span">营业执照：</span>
                                                <img src="/Images/nopic_02.jpg" name="ppic1" style="width: 280px; height: 250px; float: left" runat="server" id="ppic1" />
                                                <label>
                                                    <asp:HiddenField runat="server" ID="pic1" />
                                                    <input type="button" name="Submit" value="上传" onclick="table_display2('table100');" />
                                                    <input type="button" name="Submit2" onclick="DeletePic('1')" value="删除" />
                                                </label>
                                                <div class="mynotice">
                                                    请上传280*250的图片
                                                </div>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table100" style="display: none">
                                                    <tr>
                                                        <td align="left">&nbsp;&nbsp;&nbsp;<iframe name="upload0" marginheight="0" marginwidth="0" frameborder="0"
                                                            scrolling="no" width="100%" height="115" bordercolor="#000000" src="../../admin/upfile/UpFile.aspx?id=pic1&WaterType=1"></iframe>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </li>
                                            <li><span class="left_span">餐饮服务许可证：</span>

                                                <img src="/Images/nopic_02.jpg" name="ppic2" style="width: 280px; height: 250px; float: left" runat="server" id="ppic2" />
                                                <label>
                                                    <asp:HiddenField runat="server" ID="pic2" />
                                                    <input type="button" name="Submit" value="上传" onclick="table_display2('table101');" />
                                                    <input type="button" name="Submit2" onclick="DeletePic('2')" value="删除" />
                                                </label>
                                                <div class="mynotice">
                                                    请上传280*250的图片
                                                </div>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table101" style="display: none">
                                                    <tr>
                                                        <td align="left">&nbsp;&nbsp;&nbsp;<iframe name="upload0" marginheight="0" marginwidth="0" frameborder="0"
                                                            scrolling="no" width="100%" height="115" bordercolor="#000000" src="../../admin/upfile/UpFile.aspx?id=pic2&WaterType=1"></iframe>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </li>
                                            <li style="padding-left: 100px; padding-top: 15px;">
                                                <asp:Button Text="保存信息" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn"
                                                    OnClientClick="showload_super();" />
                                            </li>
                                        </ul>
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

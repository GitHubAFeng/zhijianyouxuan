<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyAddress.aspx.cs" Inherits="UserHome_MyAddress"
    ValidateRequest="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-我的地址簿-<%= SectionProxyData.GetSetValue(3)%></title>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>

    <script src="javascript/mymap.js" type="text/javascript"></script>

    <script src="javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        function checkPhone() {
            var phone = document.getElementById("tbCellPhone");
            var msg = jQuery("#phonemsg");
            if (phone.value.search("^[-]?\\d+[.]?\\d*$") == 0 && phone.value.length == 11) {
                msg.html("");
            }
            else {
                msg.html("电话号码格式不正确!");
            }
        }

        function checksumit() {
            var tbReceiveName = document.getElementById("tbReceiveName");
            if (tbReceiveName.value == "") {
                tipsWindown('提示信息', 'text:请输入收货人!', '250', '150', 'true', '1000', 'true', 'text');
                return false;
            }
            tbReceiveName = document.getElementById("tbCellPhone");
            if (tbReceiveName.value == "") {
                tipsWindown('提示信息', 'text:请输入移动电话', '250', '150', 'true', '1000', 'true', 'text');
                return false;
            }
            var hidlocalflag = $("#hidlocalflag").val();
            if (hidlocalflag == "1") {
                tipsWindown('提示信息', 'text:请进行地图定位', '250', '150', 'true', '1000', 'true', 'text');
                return false;
            }
            return true;
        }

        $(document).ready(function () {
            var action = request("action");
            if (action == "auth") {
                var info = $("#divtip").html();
                $.jBox(info, { title: '温馨提示' });
            }
            initialize();
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hidlocalflag" Value="0" />
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp">
            <asp:HiddenField runat="server" ID="hfbid" Value="0" />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <uc2:LeftBanner runat="server" ID="Left" />
                    <div class="rightmenu_cont">
                        <div class="rightmenu_cen">
                            <h1 class="topbg">我的收货地址</h1>
                            <asp:HiddenField ID="hdState" runat="server" />
                            <div class="my_address" runat="server" id="my_address">
                                <div class="often">
                                    常用收货地址：
                                </div>
                                <ul>
                                    <li>收货人：<%=username%></li>
                                    <li>收货地址：<%=useraddress %></li>
                                    <li>移动电话：<%=userphone %></li>
                                </ul>
                            </div>
                            <asp:Repeater ID="rtpAddressList" runat="server" OnItemCommand="rtpAddressList_ItemCommand">
                                <ItemTemplate>
                                    <div class="other_address">
                                        <div class="number">
                                            <%#Container.ItemIndex + 1%>
                                        </div>
                                        <ul>
                                            <li>收货人：<%#Eval("receiver")%></li>
                                            <li>详细地址：<%#Eval("address")%><%#Eval("phone")%></li>
                                            <li>移动电话：<%#Eval("mobilephone")%></li>
                                            <li>
                                                <asp:LinkButton ID="lbtDel" runat="server" CommandName="del" CommandArgument='<%#Eval("DataID")%>' OnClientClick="return confirm('确认删除吗？')" class="delete_altef">删除</asp:LinkButton>&nbsp; 
                                                <a href="MyAddress.aspx?id=<%#Eval("DataID")%>&pri=<%#Eval("pri")%>"  class="delete_altef">修改</a>
                                                <asp:LinkButton ID="lbdefaut1" runat="server" CommandName="setdefaut" CommandArgument='<%#Eval("DataID")%>'
                                                    class="delete_altef"> 设为默认</asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="pages">
                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                    HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                    TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                    ShowPageIndex="True" PageSize="2" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                    TextAfterPageIndexBox=" 页 " Wrap="False">
                                </webdiyer:AspNetPager>
                            </div>
                            <div class="changyongaddress">
                                <div class="often">
                                    输入新的收货地址：
                                </div>
                                <div class="consignee">
                                    <ul>
                                        <li><span class="spanleft">订 购 人：</span>
                                            <asp:TextBox ID="tbReceiveName" runat="server" AutoCompleteType="Disabled" CssClass="text"></asp:TextBox>
                                            <span class="red" style="color: Red">*</span> </li>
                                        <li><span class="spanleft">移动电话：</span>
                                            <asp:TextBox ID="tbCellPhone" runat="server" MaxLength="11" AutoCompleteType="Disabled"
                                                CssClass="text"></asp:TextBox>
                                            <span class="red" style="color: Red">*</span>&nbsp;<span style="color: Red;" id="phonemsg"></span>
                                        </li>
                                        <li>
                                            <span class="spanleft">收货地址：
                                            </span>
                                            <input name="" type="text" runat="server" id="tbAddress" class="text" style="width: 220px" />

                                            <input id="btnShowmap" type="button" value="开始搜索并标注我的位置" onclick="setPlace();" class="search_bul1" />
                                            <br />
                                            <span class="red paddingright7" id="addressmsg"></span>
                                            <br />
                                            <span class="spanleft">门牌号：
                                            </span>
                                            <input name="" type="text" runat="server" id="tbphone" class="text" style="width: 120px" />
                                            <p>
                                                为确保饭菜准确送到您手中，请搜索地图后，放大地图点击红色图标，确认我的详细地址。
                                            </p>
                                            <p>
                                                地图搜索提示：1、地图上点击，重新进行定位；2、按住鼠标拖放、缩放地图；确认我的详细位置。
                                            </p>
                                            <div id="map_search" class="search-position">
                                                <input type="hidden" id="areaid" value="" />
                                                <input type="hidden" id="areaname" value="" />
                                                </label>
                                            </div>
                                            <div style="border: 1px solid #EEEEEE; float: left; height: 353px; margin: 18px 0 0 13px; width: 550px;"
                                                id="map">
                                                <div style="width: 680px; height: 353px; position: relative; background-color: rgb(229, 227, 223);"
                                                    id="map_canvas">
                                                </div>
                                            </div>
                                            <asp:HiddenField runat="server" ID="HiddenField1" />
                                            <div class="clear">
                                            </div>

                                        </li>

                                        <li style="text-align: center;margin:15px 0;">
                                            <asp:Button Text="保存信息" runat="server" ID="btSave" OnClientClick="return checksumit();"
                                                class="subBtn" OnClick="btSave_Click" />
                                        </li>
                                    </ul>
                                </div>
                            </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="divtip" style="display: none;">

            <div style="padding: 20px; line-height: 25px;">
                <div>亲爱的用户，您还没有设置配送信息。</div>
                <div>完善配送信息后，订餐更方便。</div>
                <div>点击“确定”开始设置。</div>

            </div>

        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopCommentList.aspx.cs"
    Inherits="UserHome_CommentList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-我的商家评论-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function handleoption(evt) {
            var id = (evt.target) ? evt.target.id : ((evt.srcElement) ? evt.srcElement.id : null);   //label       
            var lboption = document.getElementById(id).innerHTML;
            var _dataid = new Array();
            _dataid = id.split('_');

            var spopinion = document.getElementById("spopinion_" + _dataid[1]);


            if (navigator.userAgent.indexOf("MSIE") > 0) {
                var _lboption = document.getElementById(id).innerText;
                if (_lboption == '显示评论') {
                    jQuery("#spopinion_" + _dataid[1]).slideDown("slow");
                    jQuery("#" + id).html("隐藏评论");
                }
                else {
                    jQuery("#spopinion_" + _dataid[1]).slideUp("slow");
                    jQuery("#" + id).html("显示评论");

                }
            }

            if (navigator.userAgent.indexOf("Firefox") > 0) {
                if (lboption.trim() == "显示评论" || lboption == '显示评论') {
                    jQuery("#spopinion_" + _dataid[1]).slideDown("slow");
                    jQuery("#" + id).html("隐藏评论");
                }
                else {
                    jQuery("#spopinion_" + _dataid[1]).slideUp("slow");
                    jQuery("#" + id).html("显示评论");

                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <uc2:LeftBanner runat="server" ID="Left" />
                    <div class="rightmenu_cont">
                        <div class="rightmenu_cen">
                            <h1 class="topbg">餐馆评论</h1>
                            <div class="myrfeview">
                                <ul>
                                    <li>商家名称</li>
                                    <li>服务等级</li>
                                    <li>口味等级</li>
                                    <li>速度等级</li>
                                    <li>发表时间</li>
                                </ul>
                                <asp:Repeater ID="rptComment" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <%# WebUtility.Left(Eval("togoname"), 12) %>
                                            </li>
                                            <li>
                                                <%# WebUtility.ShowPentacle2(Eval("ServiceGrade"))%><%# WebUtility.ShowPentacleBlank(5- Convert.ToInt32( Eval("ServiceGrade")))%></li>
                                            <li>
                                                <%# WebUtility.ShowPentacle2(Eval("FlavorGrade"))%><%# WebUtility.ShowPentacleBlank(5 - Convert.ToInt32(Eval("FlavorGrade")))%></li>
                                            <li>
                                                <%# WebUtility.ShowPentacle2(Eval("SpeedGrade"))%><%# WebUtility.ShowPentacleBlank(5 - Convert.ToInt32(Eval("SpeedGrade")))%></li>
                                            <li class="gray_02">
                                                <%#Convert.ToDateTime(Eval("posttime")).ToShortDateString()%></li>
                                        </ul>
                                        <ul>
                                            <li class="myrfeview">
                                                <label id='lboption_<%#Eval("dataid")%>'>
                                                    详细信息</label>

                                            </li>

                                        </ul>
                                        <ul>
                                            <li>
                                                <div id='spopinion_<%#Eval("dataid")%>'>
                                                    <%# Eval("Comment")%>
                                                </div>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div id="noRecord" runat="server" class="no_infor">
                                暂无评论！
                            </div>
                            <div class="pages">
                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                    HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                    TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                    ShowPageIndex="True" PageSize="5" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                    TextAfterPageIndexBox=" 页 " Wrap="False">
                                </webdiyer:AspNetPager>
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

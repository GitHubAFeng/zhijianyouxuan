<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mychange.aspx.cs" Inherits="UserHome_mychange" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-我的礼品兑换-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="css/common.css" rel="stylesheet" type="text/css"/>
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="Style/divDialog.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function handleoption(evt) {
            var id = (evt.target) ? evt.target.id : ((evt.srcElement) ? evt.srcElement.id : null);   //label       
            var lboption = document.getElementById(id).innerHTML;
            var _dataid = new Array();
            _dataid = id.split('_');

            var spopinion = document.getElementById("spopinion_" + _dataid[1]);


            if (navigator.userAgent.indexOf("MSIE") > 0) {
                var _lboption = document.getElementById(id).innerText;
                if (_lboption == '显示详细') {
                    jQuery("#spopinion_" + _dataid[1]).slideDown("slow");
                    jQuery("#" + id).html("隐藏");
                }
                else {
                    jQuery("#spopinion_" + _dataid[1]).slideUp("slow");
                    jQuery("#" + id).html("显示详细");

                }
            }

            if (navigator.userAgent.indexOf("Firefox") > 0) {
                if (lboption.trim() == "显示详细" || lboption == '显示详细') {
                    jQuery("#spopinion_" + _dataid[1]).slideDown("slow");
                    jQuery("#" + id).html("隐藏");
                }
                else {
                    jQuery("#spopinion_" + _dataid[1]).slideUp("slow");
                    jQuery("#" + id).html("显示详细");

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
                        <h1 class="topbg">
                            礼品兑换</h1>
                        <div class="myrfeview">
                            <ul>
                                <li class=" myrfeview180px" style="width: 280px">兑换礼品</li>
                                <li class="myrfeview350px" style="width: 80px">所用积分</li>
                                <li class="myrfeview350px" style="width: 120px">状态</li>
                                <li class="myrfeview350px" style="width: 150px">时间</li>
                            </ul>
                            <asp:Repeater ID="rptComment" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li class="orange myrfeview180px" style="width: 280px">
                                            <%# WebUtility.Left(Eval("GiftName"), 16)%>
                                        </li>
                                        <li class="myrfeview350px" style="width: 80px">
                                            <%# Eval("PayIntegral")%>
                                        </li>
                                        <li class="myrfeview350px" style="width: 120px">
                                            <%# GetState( Eval("State")) %>
                                        </li>
                                        <li class="myrfeview350px" style="width: 150px">
                                            <%# Convert.ToDateTime(Eval("Cdate")).ToShortDateString()%></li>
                                    </ul>
                                    <ul>
                                        <li class="myrfeview" style="width: 700px">
                                            <label id='lboption_<%#Eval("IntegralId")%>' style="border-bottom-color: Green; color:red;
                                                float: left; margin-right: 15px;">
                                                详细信息</label>
                                            <div id='spopinion_<%#Eval("IntegralId")%>'>
                                                <span>收货人：<%#Eval("Person")%></span> <span>电话：<%#Eval("Phone")%></span> <span>地址：<%#Eval("Address")%></span>
                                            </div>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div id="noRecord" runat="server" class="no_infor">
                            暂无兑换的礼品！
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

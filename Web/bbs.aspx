<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bbs.aspx.cs" Inherits="bbs" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>留言版 - <%= SectionProxyData.GetSetValue(3)%>
    </title>
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link href="css/review.css" rel="stylesheet" />
    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            initnav(6);
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="admin/images/Control" />
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:Banner ID="Banner1" runat="server" />
        <!--top end-->
        <div class="wrap">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!--主体部分-->
                    <div class="hplace_bg">
                        <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 留言版</span>
                    </div>
                    <div class="review_bg">
                        <div class="else_left_side">
                            <div class="review">
                                <div class="right_navbg_04">
                                    发布留言
                                </div>
                                <div class="review_publish">
                                    <p style="padding-bottom: 0;">
                                        <em class="float_r f12">你还可以输入<font class="f16 fa fb" id="wordqutility">140</font>个字</em>留言内容
                                    </p>
                                    <textarea name="" class="review_textarea" runat="server" id="textarea" onkeyup="ttowrdcheck()"
                                        sub_id="div_face_tab"></textarea>
                                    <p class="float_r review_btn">
                                        <asp:Button runat="server" ID="btLogin" CssClass="common_button" OnClientClick="return checkQ();"
                                            OnClick="btsave_Click" Text="留言" />
                                    </p>
                                </div>
                            </div>
                            <div class="review">
                                <div class="right_navbg_04">
                                    所有留言
                                </div>
                                <asp:Repeater ID="rptMessagelsit" runat="server">
                                    <ItemTemplate>
                                        <div class="review_con">
                                            <div class="review_con_left">
                                                <img src="<%# WebUtility.ShowPic(Eval("pic").ToString()) %>" width="54" height="54" />
                                            </div>
                                            <div class="review_con_right">
                                                <h4>
                                                    <span>
                                                        <%#Eval("username")%></span></h4>
                                                <p>
                                                    <%# Eval("word") %>
                                                </p>
                                                <p class="f12">
                                                    <%# Eval("time") %>发布
                                                </p>

                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="pages">
                                    <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                        CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                        HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                        CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                        TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                        ShowPageIndex="True" PageSize="10" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                        TextAfterPageIndexBox=" 页 " Wrap="False">
                                    </webdiyer:AspNetPager>
                                </div>
                            </div>
                        </div>
                        <div class="right_side">
                            <div class="cen_con">
                                <div class="right_navbg_04">
                                    网站说明
                                </div>
                                <div class="diantab2_info" style="color: #000;">
                                    <%= SectionProxyData.GetSetValue(36) %>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <foot:foot ID="foot" runat="server" />
    </form>
</body>
</html>

<script type="text/javascript">

    function ttowrdcheck() {
        var youmsg = document.getElementById("wordqutility");
        var wordlength = document.getElementById("textarea").value.length;
        if (wordlength < 140) {
            youmsg.innerHTML = 140 - wordlength;
        }
        else {
            youmsg.innerHTML = 0;
            tipsWindown('提示信息', 'text:已经超过字数限制!', '250', '150', 'true', '1000', 'true', 'text');
        }
    }

    function checkQ() {
        var hfuid = $("#hfuserid").val() + "";
        if (hfuid == "0" || hfuid == "-1") {
            jtipsWindown('提示信息', 'id:divShowContent', '520', '350', 'true', '', 'true', 'text');
            return false;
        }
        var wordlength = document.getElementById("textarea").value.length;
        if (wordlength < 1) {
            tipsWindown('提示信息', 'text:字数太少了,请至少输入法1个汉字!', '250', '150', 'true', '2000', 'true', 'text');
            return false;
        }
        else {
            if (wordlength > 140) {
                tipsWindown('提示信息', 'text:已经超过字数限制!', '250', '150', 'true', '1000', 'true', 'text');
                return false;
            }
            else {
                return true;
            }
        }
    }

</script>


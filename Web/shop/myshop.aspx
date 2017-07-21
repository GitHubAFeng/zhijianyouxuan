<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myshop.aspx.cs" Inherits="shop_myshop" ValidateRequest="false" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家基本信息-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
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


    <script src="../Admin/javascript/QueryChina.js" type="text/javascript"></script>


    <script type="text/javascript">
        function table_display2(t_id) {
            if (t_id == "table100" && document.getElementById("table100").style.display == "none") {
                document.getElementById("table100").style.display = "";
                document.getElementById("table101").style.display = "none"
            }
            else {
                document.getElementById("table100").style.display = "none";
                document.getElementById("table101").style.display = "";
            }
        }


        function DeletePic(t_id) {
            document.getElementById("ppic" + t_id).src = "../../images/nopic.jpg";
            document.getElementById("pic1" + t_id).value = "0";
        }

        function queryChinese() {
            var str = document.getElementById("tbName").value.trim();
            if (str == "") return;
            var arrRslt = makePy(str);
            var vaccount = $("#tbLoginName").val() + "";
            if (vaccount == "") {
                $("#tbLoginName").val(arrRslt[0]);
                // $("#tbPassword").val(arrRslt[0]);
            }
            $("#tbNamePy").val(arrRslt[0]);
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
                                            <li class="cur"><a href="myshop.aspx">修改资料</a></li>
                                            <li><a href="TogoLocal.aspx">商家定位</a></li>
                                            <li><a href="SetStatus.aspx">状态管理</a></li>
                                            <li><a href="myPromotion.aspx">促销活动</a></li>
                                            <li><a href="qualification.aspx">商家资质管理</a></li>
                                        </ul>
                                    </div>
                                    <div class="usermima">
                                        <ul>
                                            <li><span class="left_span">餐馆名称：</span>
                                                <epc:TextBox runat="server" ID="tbName" CanBeNull="必填" Width="190px" class="input_on"
                                                    onblur="queryChinese()">
                                                </epc:TextBox>
                                            </li>
                                            <li><span class="left_span">餐馆名拼音：</span>
                                                <epc:TextBox runat="server" ID="tbNamePy" CanBeNull="必填" Width="190px" class="required-entry input-text">
                                                </epc:TextBox>
                                            </li>
                                            <li><span class="left_span">分类：</span>
                                                <asp:CheckBoxList runat="server" ID="cblCategory" RepeatColumns="3">
                                                </asp:CheckBoxList>
                                            </li>

                                            <li><span class="left_span">联系人：</span>
                                                <epc:TextBox runat="server" ID="tbCommPerson" CanBeNull="必填" Width="190px" class="required-entry input-text">
                                                </epc:TextBox>
                                            </li>
                                            <li><span class="left_span">电话：</span>
                                                <epc:TextBox runat="server" ID="tbComm" CanBeNull="必填" Width="190px" class="required-entry input-text"
                                                    Text="">
                                                </epc:TextBox>
                                            </li>
                                            <li><span class="left_span">地址：</span>
                                                <epc:TextBox runat="server" ID="tbAddress" CanBeNull="必填" Width="190px" class="required-entry input-text">
                                                </epc:TextBox>
                                            </li>
                                            <li><span class="left_span">订单刷新时间间隔：</span>
                                                <epc:TextBox runat="server" ID="tbtbRefresh" CanBeNull="必填" Text="30" Width="40px" class="required-entry input-text"> 
                                                </epc:TextBox> 秒/次
                                            </li>
                                            <li>
                                                <li><span class="left_span">营业时间：</span>
                                                    <epc:TextBox runat="server" ID="TextBoxtime" Width="80px" onfocus="timeinit('TextBoxtime');"
                                                        class="equired-entry input-text" CanBeNull="必填" RequiredFieldType="营业时间">
                                                    </epc:TextBox>至<epc:TextBox runat="server" ID="TtimeEnd" Width="80px" onfocus="timeinit('TtimeEnd');"
                                                        class="required-entry input-text" CanBeNull="必填" RequiredFieldType="营业时间"></epc:TextBox>
                                                    <div style="margin-left: 110px;">
                                                        <span style="color: Red;">注意：请选择分钟,如：1:00</span>
                                                    </div>
                                                </li>
                                                <li><span class="left_span">营业时间：</span>
                                                    <epc:TextBox runat="server" ID="TextBoxtime2" Width="80px" onfocus="timeinit('TextBoxtime2');"
                                                        class="equired-entry input-text" CanBeNull="必填" RequiredFieldType="营业时间">
                                                    </epc:TextBox>至<epc:TextBox runat="server" ID="TtimeEnd2" Width="80px" onfocus="timeinit('TtimeEnd2');"
                                                        class="required-entry input-text" CanBeNull="必填" RequiredFieldType="营业时间"></epc:TextBox>
                                                    <div style="margin-left: 110px;">
                                                        <span style="color: Red;">注意：请选择分钟,如：1:00</span>
                                                    </div>
                                                </li>
                                                <li style="display: none;"><span class="left_span">电话订餐时间一：</span>
                                                    <epc:TextBox runat="server" ID="tbbisnessStart1" onfocus="timeinit('tbbisnessStart1');"
                                                        CanBeNull="必填" Width="80px" class="required-entry input-text">
                                                    </epc:TextBox>至
                                        <epc:TextBox runat="server" ID="tbbisnessend1" onfocus="timeinit('tbbisnessend1');"
                                            CanBeNull="必填" Width="80px" class="required-entry input-text">
                                        </epc:TextBox>
                                                </li>
                                                <li style="display: none;"><span class="left_span">电话订餐时间二：</span>
                                                    <epc:TextBox runat="server" ID="tbbisnessStart2" onfocus="timeinit('tbbisnessStart2');"
                                                        CanBeNull="必填" Width="80px" class="required-entry input-text">
                                                    </epc:TextBox>至
                                        <epc:TextBox runat="server" ID="tbbisnessend2" onfocus="timeinit('tbbisnessend2');"
                                            CanBeNull="必填" Width="80px" class="required-entry input-text">
                                        </epc:TextBox>
                                                </li>
                                                <li style="display: none;"><span class="left_span">送餐时间一：</span>
                                                    <epc:TextBox runat="server" ID="tbTime1Start" Width="80px" onfocus="timeinit('tbTime1Start');"
                                                        class="required-entry input-text" CanBeNull="必填">
                                                    </epc:TextBox>至<epc:TextBox runat="server" ID="tbTime1End" Width="80px" onfocus="timeinit('tbTime1End');"
                                                        class="required-entry input-text" CanBeNull="必填"></epc:TextBox>
                                                </li>
                                                <li style="display: none;"><span class="left_span">送餐时间二：</span>
                                                    <epc:TextBox runat="server" ID="tbTime2Start" Width="80px" onfocus="timeinit('tbTime2Start');"
                                                        class="equired-entry input-text" CanBeNull="必填">
                                                    </epc:TextBox>至
                                        <epc:TextBox runat="server" ID="tbTime2End" Width="80px" onfocus="timeinit('tbTime2End');"
                                            class="required-entry input-text" CanBeNull="必填">
                                        </epc:TextBox>
                                                </li>
                                                <li style="display: none;"><span class="left_span">最低起送价格：</span>
                                                    <epc:TextBox runat="server" ID="tbSendLimit" CanBeNull="必填" Width="60px" class="required-entry input-text"
                                                        RequiredFieldType="数据校验">
                                                    </epc:TextBox>(元) </li>
                                                <li><span class="left_span">配送时间：</span>
                                                    <epc:TextBox runat="server" Text="45" ID="tbSentTime" CanBeNull="必填" RequiredFieldType="数据校验"
                                                        Width="60px" class="required-entry input-text">
                                                    </epc:TextBox>分钟(填写数字) </li>
                                                <li style="display: none;"><span class="left_span">配送费用：</span>
                                                    餐品金额满<epc:TextBox runat="server" ID="tbPTimes" Width="60px" Text="0"
                                                        class="required-entry input-text"></epc:TextBox>
                                                    ，免配送费(0表示不启用 ) </li>

                                                <li><span class="left_span">店铺活动：</span>
                                                    <epc:TextBox runat="server" ID="tbspecial" Width="190px" TextMode="MultiLine" MaxLength="200" class="required-entry input-text">
                                                    </epc:TextBox>
                                                </li>
                                                <li><span class="left_span">图片：</span>
                                                    <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                    <asp:HiddenField ID="FolderType" Value="0" runat="server" />
                                                    <asp:HiddenField ID="WaterType" Value="1" runat="server" />
                                                    &nbsp; <a href="" id="txturldz" target="_blank">
                                                        <img border="0" src="../admin/Images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 145px; height: 95px"
                                                            runat="server" /></a><br />
                                                    <div style="padding-left: 60px;">
                                                        <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" />请上传145*95的图片<br />
                                                        <div id="rowTest" style="display: none">
                                                            <iframe name="tag" src="../admin/upfile/Upload.html?Links" style="width: 330px; height: 130px"
                                                                frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
                                                        </div>
                                                        <div id="Upload">
                                                        </div>
                                                    </div>
                                                </li>

                                                <li style="display: none;"><span class="left_span">商家简介：</span>
                                                    <FCKeditorV2:FCKeditor ID="tbIntroduce" runat="server" ToolbarSet="ShopDetail" BasePath="../admin/fckeditor/"
                                                        Width="400px" Height="300px">
                                                    </FCKeditorV2:FCKeditor>
                                                </li>
                                                <li><span class="left_span">邮箱：</span>
                                                    <epc:TextBox runat="server" ID="tbemail" RequiredFieldType="电子邮箱" Width="190px" class="required-entry input-text"
                                                        name="invalue">
                                                    </epc:TextBox>
                                                </li>
                                                <li><span class="left_span">登录帐号：</span>
                                                    <epc:TextBox runat="server" ID="tbLoginName" CanBeNull="必填" Width="190px" class="required-entry input-text"
                                                        name="invalue">
                                                    </epc:TextBox>
                                                </li>
                                                <li><span class="left_span">登录密码：</span>
                                                    <epc:TextBox runat="server" ID="tbPassword" class="required-entry input-text" Width="120px">
                                                    </epc:TextBox><div class="mynotice">
                                                        不修改密码则此输入框留空即可
                                                    </div>
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
                <div class="time_div" id="timediv" style="display: none">
                    <div class="time_div_top">
                    </div>
                    <div class="time_div_con">
                        <div style="clear: both; font-size: 12px; border-bottom: 1px solid #ccc; padding-left: 10px; padding-bottom: 5px;">
                            <h1 style="float: right; padding-right: 5px; cursor: pointer; font-size: 12px;" onclick="$('#timediv').hide()">关闭</h1>
                            请选择时间
                        </div>
                        <div class="con_left">
                            <h1>时
                            </h1>
                            <div style="border-right: 2px solid #CCC; height: 110px;">
                                <ul class="key_ul">
                                    <li onclick="sethour(this)">0</li>
                                    <li onclick="sethour(this)">1</li>
                                    <li onclick="sethour(this)">2</li>
                                    <li onclick="sethour(this)">3</li>
                                    <li onclick="sethour(this)">4</li>
                                    <li onclick="sethour(this)">5</li>
                                    <li onclick="sethour(this)">6</li>
                                    <li onclick="sethour(this)">7</li>
                                    <li onclick="sethour(this)">8</li>
                                    <li onclick="sethour(this)">9</li>
                                    <li onclick="sethour(this)">10</li>
                                    <li onclick="sethour(this)">11</li>
                                    <li onclick="sethour(this)">12</li>
                                    <li onclick="sethour(this)">13</li>
                                    <li onclick="sethour(this)">14</li>
                                    <li onclick="sethour(this)">15</li>
                                    <li onclick="sethour(this)">16</li>
                                    <li onclick="sethour(this)">17</li>
                                    <li onclick="sethour(this)">18</li>
                                    <li onclick="sethour(this)">19</li>
                                    <li onclick="sethour(this)">20</li>
                                    <li onclick="sethour(this)">21</li>
                                    <li onclick="sethour(this)">22</li>
                                    <li onclick="sethour(this)">23</li>
                                </ul>
                            </div>
                        </div>
                        <div class="con_right">
                            <h1>分</h1>
                            <ul class="key_ul" style="margin-left: 7px;">
                                <li onclick="setminute(this)">00</li>
                                <li onclick="setminute(this)">15</li>
                                <li onclick="setminute(this)">30</li>
                                <li onclick="setminute(this)">45</li>
                            </ul>
                        </div>
                    </div>
                    <div class="time_div_bottom">
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>

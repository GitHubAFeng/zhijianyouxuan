<%@ Page Language="C#" AutoEventWireup="true" CodeFile="applyshop.aspx.cs" Inherits="applyshop" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家加盟 - <%= SectionProxyData.GetSetValue(3)%>
    </title>
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link type="text/css" href="css/applyshop.css" rel="stylesheet" />
    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="javascript/ShowDivDialog.js" type="text/javascript"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            initnav(5);
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
                        <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 商家加盟</span>
                    </div>
                    <div class="help_con clearfix">
                        <div class="right_navbg_04">加盟流程</div>
                        <ul class="steps clearfix">
                            <li class="have">
                                <div class="imgshow"><span class="dian"></span><span class="line"></span></div>
                                <p>访问网站</p>
                            </li>
                            <li class="cul">
                                <div class="imgshow"><span class="dian"></span><span class="line"></span></div>
                                <p>填写申请表</p>
                            </li>
                            <li class="will">
                                <div class="imgshow"><span class="dian"></span><span class="line"></span></div>
                                <p>管理员审核</p>
                            </li>
                            <li class="will" style="width: 45px;">
                                <div class="imgshow"><span class="dian"></span></div>
                                <p>加盟成功</p>
                            </li>
                        </ul>

                        <div class="right_navbg_04">加盟申请表</div>
                        <div class="infor_tit">联系信息</div>
                        <div class="joinin_con postdata">
                            <p>
                                <span class="le_in"><span class="red">*</span>联系人</span>
                                <input type="text" class="o_text" runat="server" id="tbCommPerson" reg="\S" tip="联系人不能为空" />
                            </p>
                            <p>
                                <span class="le_in"><span class="red">*</span>联系电话</span>
                                <input type="text" class="o_text" runat="server" id="tbComm" reg="\d+$" tip="联系电话不能为空" />
                            </p>
                            <p>
                                <span class="le_in"><span class="red">*</span>商家名称</span>
                                <input type="text" class="o_text" runat="server" id="tbName" reg="\S" tip="商家名称不能为空" />
                            </p>
                            <p>
                                <span class="le_in"><span class="red">*</span>商家地址</span>
                                <input type="text" class="o_text" runat="server" id="tbAddress" tip="商家地址不能为空" />
                            </p>
                            <p>
                                <span class="le_in">商家简介</span>
                                <textarea class="o_text" runat="server" style="width:500px; height: 100px;" id="tbIntroduce"></textarea>
                            </p>
                             <p>
                                 <span class="le_in"><span class="red">*</span>验证码</span>
                                 <input style="width:100px;" type="text" class="o_text" runat="server" id="tbadcode" reg="\S" tip="请输入验证码" /> 
                                 <img src="VCode.aspx" onclick="this.src = 'VCode.aspx?t='+new Date().getTime();" alt="Click to change" style="padding-left: 10px; cursor: pointer; vertical-align: text-bottom;" />
                             </p>
                            
                             <div class="btn">
                                <asp:Button runat="server" ID="btLogin" CssClass="common_button" OnClientClick="return j_submitdata('postdata');" OnClick="BTsave_Click"
                                    Text="立即申请" />
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodDetail.aspx.cs" Inherits="shop_FoodDetail" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>餐品详细-<%= WebUtility.GetWebName()%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />

    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/QueryChina.js" type="text/javascript"></script>

    <script src="/javascript/foodmanage.js" type="text/javascript"></script>
    <link href="/javascript/jbox/Skins/jbox.css" rel="stylesheet" />


    <script type="text/javascript">
        $(document).ready(function () {
            MaxPerDayShow();
        });


        function queryfood() {
            var str = document.getElementById("tbName").value.trim();
            if (str == "") return;
            var arrRslt = makePy(str);
            $("#tbFoodNamePy").val(arrRslt[0]);
        }
        function MaxPerDayShow()
        {
            var state = $("#DropPerDay").val();
            if (state == "0") {
                $("#liMaxPerDay").hide();
            }
            else
            {
                $("#liMaxPerDay").show();
            }
        }
    </script>

</head>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidTogoId" />
        <asp:HiddenField runat="server" ID="hidDataId" />
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
                                    <div class="shop_menu">
                                        <ul>
                                            <li><a href="FoodSortList.aspx?">餐品类别列表</a></li>
                                            <li><a href="FoodSortDetail.aspx">添加餐品类别</a></li>
                                            <li><a href="FoodList.aspx">餐品列表</a></li>
                                            <li class="cur"><a href="FoodDetail.aspx">添加餐品</a></li>
                                            <li><a href="Foodimport.aspx">批量导入</a></li>

                                        </ul>
                                    </div>
                                    <h1 class="topbg">
                                        <asp:Label ID="lbTitle" runat="server" Text="添加餐品"></asp:Label></h1>
                                    <div class="usermima">
                                        <ul>
                                            <li><span class="left_span">餐品名称：</span>
                                                <asp:HiddenField ID="HdFoodNO" runat="server" Value="" />
                                                <epc:TextBox runat="server" ID="tbName" Size="25" onblur="queryfood()" class="input_on"
                                                    MaxLength="30"></epc:TextBox>
                                            </li>
                                            <li><span class="left_span">餐品拼音：</span>
                                                <epc:TextBox runat="server" ID="tbFoodNamePy" Size="25" class="input_on" MaxLength="30"></epc:TextBox>(填写首字母)
                                            </li>
                                            <li><span class="left_span">图片：</span>
                                                <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                <asp:HiddenField ID="FolderType" Value="0" runat="server" />
                                                <asp:HiddenField ID="WaterType" Value="1" runat="server" />
                                                <img border="0" src="../admin/Images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 120px; height: 120px; float: left;"  runat="server" />
                                                <div>
                                                    <input id="txtupload" type="button" value="上传" style="margin-left: 10px;" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" />请上传200*200的图片<br />
                                                    <div id="rowTest" style="display: none">
                                                        <iframe name="tag" src="../admin/upfile/Upload.html?Links" style="width: 320px; height: 130px"
                                                            frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
                                                    </div>
                                                    <div id="Upload">
                                                    </div>
                                                </div>
                                            </li>
                                            <li><span class="left_span">餐品类别：</span>
                                                <asp:DropDownList ID="DropFoodType" runat="server">
                                                </asp:DropDownList>
                                            </li>
                                            <li runat="server" id="pricebox"><span class="left_span">价格：</span>
                                                <epc:TextBox runat="server" ID="tbPrice" Width="60" RequiredFieldType="数据校验" CanBeNull="必填"
                                                    class="input_on" MaxLength="5"></epc:TextBox>元 </li>


                                            <li><span class="left_span">打包费：</span>
                                                <epc:TextBox runat="server" ID="tbFullPrice" Width="60" RequiredFieldType="数据校验" CanBeNull="必填"
                                                    class="input_on" MaxLength="5"></epc:TextBox>元 </li>
                                            <li style="display: none"><span class="left_span">剩余数量：</span>
                                                <asp:TextBox runat="server" ID="tbRemains" size="25" Text="1" class="input_on" MaxLength="30"></asp:TextBox>
                                            </li>
                                            <li><span class="left_span">库存选择：</span>
                                               <asp:DropDownList ID="DropPerDay" runat="server" onchange="MaxPerDayShow()">
                                                   <asp:ListItem Value="0">无限库存</asp:ListItem>
                                                   <asp:ListItem Value="1">手动库存</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li id="liMaxPerDay"><span class="left_span">库存：</span>
                                                <asp:TextBox runat="server" ID="tbMaxPerDay" size="10" Text="100000" class="input_on" MaxLength="30"></asp:TextBox>
                                            </li>
                                            <li><span class="left_span">排序：</span>
                                                <asp:TextBox runat="server" ID="tbOrderNum" Style="width: 50px;" Text="5" class="input_on" MaxLength="5"></asp:TextBox>
                                                数字大，排在前
                                            </li>
                                            <li style="display: none"><span class="left_span">是否推荐：</span>
                                                <asp:RadioButtonList runat="server" ID="rblIsRecommend" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">否</asp:ListItem>
                                                    <asp:ListItem Value="1">是</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </li>

                                            <li><span class="left_span">简介：</span>
                                                <asp:TextBox runat="server" ID="tbtaste" size="" class="input_on" TextMode="MultiLine"
                                                    Width="500px" Height="50px"></asp:TextBox>
                                            </li>
                                            <li style="text-align: center;">
                                                <asp:Button Text="确定" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn" />
                                            </li>
                                        </ul>

                                        <div runat="server" id="otherbox">
                                            <div style="margin-top: 20px;">
                                                <div style="line-height: 30px; margin-bottom: 10px;">

                                                    <input type="button" class="subBtn" value="添加规格" onclick="addsytle();" style="<%= Convert.ToInt32(SectionProxyData.GetSetValue(69)) == 1 ? "": "display:none"  %>; margin-left: 0" />
                                                </div>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                                    style="border: 1px solid #ccc;">
                                                    <tr>
                                                        <th style="width: 60%">规格名称
                                                        </th>

                                                        <th style="width: 20%">价格
                                                        </th>

                                                        <th style="width: 20%">操作
                                                        </th>
                                                    </tr>
                                                    <asp:Repeater ID="rptFoodlist" runat="server" OnItemCommand="rptFoodlist_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">

                                                                <td class="">
                                                                    <%#Eval("Title")%>&nbsp;
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("Price")%>&nbsp;
                                                                </td>
                                                                <td class="last">
                                                                    <a href="javascript:edit(<%# Eval("dataid") %>)">编辑</a>

                                                                    <span style="<%= Convert.ToInt32(SectionProxyData.GetSetValue(69)) == 1 ? "": "display:none"  %>">|
                                                                        <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("DataId")%>' OnClientClick="return DelConfirm();" runat="server" ID="LinkButton1">删除</asp:LinkButton>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                            <div style="<%= Convert.ToInt32(SectionProxyData.GetSetValue(70)) == 1 ? "": "display:none"  %>; margin-top: 20px;">
                                                <div style="line-height: 30px; margin-bottom: 10px;">
                                                    <input type="button" class="subBtn" value="增加属性" onclick="addattr();" style="margin-left: 0" />
                                                </div>

                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                                    style="border: 1px solid #ccc;">
                                                    <tr>
                                                        <th style="width: 60%">名称
                                                        </th>
                                                        <th style="width: 20%">必选
                                                        </th>
                                                        <th style="width: 20%">操作
                                                        </th>
                                                    </tr>
                                                    <asp:Repeater ID="rptattr" runat="server" OnItemCommand="rptattr_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="">
                                                                    <%#Eval("Title")%>&nbsp;
                                                                </td>
                                                                <td class="">
                                                                    <%#Eval("inve1").ToString() == "0" ? "否":"是"%>&nbsp;
                                                                </td>
                                                                <td class="last">
                                                                    <a href="javascript:editattr(<%# Eval("dataid") %>)">编辑</a> |
                                                                       <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("DataId")%>' OnClientClick="return DelConfirm();" runat="server" ID="del">删除</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
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

<script src="/javascript/jbox/jquery.jBox-2.3.min.js" type="text/javascript"></script>

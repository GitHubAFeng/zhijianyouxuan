<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodSortList.aspx.cs" Inherits="shop_FoodSortList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>餐品类别-<%= WebUtility.GetWebName()%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css?v=1217" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <link href="Style/divDialog.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../JavaScript/tinybox.js" type="text/javascript"></script>
    <script src="../Admin/javascript/spin.min.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
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
                                            <li class="cur"><a href="FoodSortList.aspx?">餐品类别列表</a></li>
                                            <li><a href="FoodSortDetail.aspx">添加餐品类别</a></li>
                                            <li><a href="FoodList.aspx">餐品列表</a></li>
                                            <li><a href="FoodDetail.aspx">添加餐品</a></li>
                                            <li><a href="Foodimport.aspx">批量导入</a></li>

                                        </ul>
                                    </div>

                                    <div class="usermima">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listorder_table"
                                            style="border: 1px solid #ccc;">
                                            <asp:Repeater ID="rptFoodSortList" runat="server" OnItemCommand="rptFoodSortList_ItemCommand">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th>类别名称
                                                        </th>
                                                        <th style="width: 20%;">图片
                                                        </th>
                                                        <th style="width: 20%;">排序
                                                        </th>
                                                        <th style="width: 20%;">操作
                                                        </th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Eval("SortName")%>
                                                        </td>
                                                        <td class="">
                                                            <img src="<%# WebUtility.ShowPic(Eval("pic").ToString()) %>" height="50" width="50" id="sort_pic_<%# Eval("sortid") %>" onclick="showmap(<%# Eval("sortid") %>);return false;">
                                                            <a href="javascript:" onclick="showmap(<%# Eval("sortid") %>);return false;">上传图片</a>
                                                        </td>
                                                        <td>
                                                            <%# Eval("jorder")%>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtdelete" runat="server" CommandName="del" OnClientClick="return  confirm('确定删除吗？')"
                                                                CommandArgument='<%#Eval("SortID")%>'>删除</asp:LinkButton>
                                                            |
                                                    <asp:LinkButton ID="lbtupdate" runat="server" CommandName="update" CommandArgument='<%#Eval("SortID")%>'>修改</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <div id="noRecord" runat="server" class="no_infor">
                                        暂无餐品类别！
                                    </div>
                                    <div class="pages">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                            ShowPageIndex="True" PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                            TextAfterPageIndexBox=" 页 " Wrap="False">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </div>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div style="padding-top: 20px; background-color: #fff; display: none; position: absolute; width: 410px;" id="uploadcontainer" class="uploadcontainer">
                        <h1>
                            <a href='javascript:;' onclick="$('#uploadcontainer').hide()" class="close">
                                <img src='../Images/window_close.gif' alt='关闭窗口' />
                            </a>
                            请上传50*50的图片</h1>
                        <asp:HiddenField ID="hfsid" runat="server" Value="0" />
                        <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                        <asp:HiddenField ID="FolderType" Value="1" runat="server" />
                        <asp:HiddenField ID="WaterType" Value="0" runat="server" />

                        <img border="0" src="/images/nopic_02.jpg" id="ImgUrl" alt="" style="width: 100px; height: 100px"
                            runat="server" /><br />
                        <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block'; return txtupload_onclick();" /><br />
                        <div id="rowTest" style="display: none">
                            <iframe name="tag" src="/Admin/upfile/Upload.html?Links" style="width: 400px; height: 100px"
                                frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
                        </div>
                        <div id="Upload">
                        </div>
                        <br />
                        <input type="button" value="确定" onclick="goog()" id="btsubmit" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function goog() {

        Loader.show("#btsubmit");

        var id = $("#hfsid").val() + "";
        var pic = $("#ImgUrl1").val();
        var para = "id=" + id + "&pic=" + pic + "&method=uploadFoodSortPic";
        jQuery.ajax(
        {
            type: "post",
            url: "/ajaxHandler.ashx",
            data: para,
            success: function (msg) {
                alert('上传成功.');
                $("#sort_pic_" + id).attr("src", pic.replace("~", ""));
                Loader.hide();
                $("#uploadcontainer").hide();

            }
        })
    }
    function showmap(aid) {
        $("#hfsid").val(aid);
        ShowTip(document.getElementById("sort_pic_" + aid), "uploadcontainer", -400, 60);
    }

    //显示一个div
    //显示需要定位
    //obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
    //addx、addy是相对与obj的偏移量，就是div显示的位置
    function ShowTip(obj, objdiv, addx, addy) {
        var x = getposOffset_top(obj, 'left');
        var y = getposOffset_top(obj, 'top');

        var div_obj = document.getElementById(objdiv);
        div_obj.style.left = (x + addx) + 'px';
        div_obj.style.top = (y + addy) + 'px';
        div_obj.style.display = "inline";
    }


</script>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodSortList.aspx.cs" Inherits="Admin_Shop_FoodSortList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家餐品分类-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="../javascript/spin.min.js"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
        AddLoadFun(init);

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的商家!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }
        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }
    </script>

    <script language="javascript" type="text/javascript">

        function GotoAddFoodSort() {
            window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
        function GotoAddFood() {
            window.location.href = "FoodDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoAddPrinter() {
            window.location.href = "AddPrinter.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoAddFoodSort() {
            window.location.href = "FoodSortDetail.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }

        function GotoFood() {
            window.location.href = "FoodList.aspx?tid=" + document.getElementById("hidTogoId").value + "";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdDels" runat="server" />
        <asp:HiddenField runat="server" ID="hidTogoId" />
        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <!--banner start-->
            <uc1:TogoBanner runat="server" ID="Banner" />
            <!--banner end-->
            <!--center start-->
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <div class="side-col" id="page:left">
                            <uc3:left runat="server" ID="left" />
                        </div>
                        <div class="main-col" id="content">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="main-col-inner">
                                        <div id="divMessages">
                                        </div>


                                        <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none">
                                            <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" class="tab-item-link ">
                                                <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                            </li>
                                            <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link active"><span>
                                                <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                            <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                            <li><a href="Distancepaylist.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                            <li><a href="ShopLocal.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>

                                            <li><a href="AddPrinter.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>打印机</span> </a></li>

                                        </ul>




                                        <fieldset class="AdminSearchform">
                                            <legend>
                                                <asp:Label runat="server" ID="lbTogoName"></asp:Label></legend>
                                            <ul class="FunctionUl">
                                                <li>
                                                    <button type="button" class="scalable " onclick="GotoAddFoodSort()" style="">
                                                        <span>添加餐品分类</span></button></li>
                                                <li>
                                                    <button type="button" class="scalable " onclick="GotoAddFood()" style="">
                                                        <span>为此商家添加菜单</span></button></li>
                                                <li>
                                                    <button type="button" class="scalable " onclick="GotoFood()" style="">
                                                        <span>查看菜品</span></button></li>

                                            </ul>
                                        </fieldset>
                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                                CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " UrlPagingTarget="_self" UrlPageIndexName="p" UrlPageSizeName="s"
                                                UrlPaging="True" PageIndexBoxClass="flattext" ShowPageIndex="True" PageSize="20"
                                                SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                                Wrap="False">
                                            </webdiyer:AspNetPager>
                                        </div>
                                        <div id="sales_order_grid_massaction" style="clear: both;">
                                            <table class="massaction" cellpadding="0" cellspacing="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <a href="#" onclick="javascript:Table.CheckAll()">全选</a> <span class="separator">|</span>
                                                            <a href="#" onclick="javascript:Table.CheckNo()">取消选择</a><span class="separator">|</span>
                                                            <a href="#" onclick="javascript:Table.ReCheck()">反向选择</a><span class="separator">|</span>
                                                            <a href="#" onclick="return false">
                                                                <asp:LinkButton runat="server" ID="lbDelsom" OnClientClick="return Del()" OnClick="DelList_Click">删除选定</asp:LinkButton>
                                                            </a>
                                                        </td>
                                                      
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="grid">
                                            <div class="hor-scroll">
                                                <table class="data" cellspacing="0" id="grid_table">
                                                    <col class="a-center" width="5%" />
                                                    <col />
                                                    <col width="10%" />
                                                    <col width="10%" />
                                                    <col width="10%" />
                                                    <thead>
                                                        <tr class="headings">
                                                            <th>
                                                                <span class="nobr">&nbsp;</span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">餐品类别名称</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                    class="sort-title">排序</span></a></span>
                                                            </th>
                                                            <th>
                                                                <span class="nobr">图片</span>
                                                            </th>
                                                            <th class="no-link last">
                                                                <span class="nobr">操作</span>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="grid_data">
                                                        <asp:Repeater ID="rptSortlist" runat="server" OnItemCommand="rptSortlist_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="pointer" title="">
                                                                    <td class="" width="20px">
                                                                        <input name="inut" value="<%# Eval("sortid")%>" class="massaction-checkbox"
                                                                            type="checkbox">
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("SortName")%>
                                                                    </td>
                                                                    <td class="">
                                                                        <%# Eval("jorder")%>
                                                                    </td>

                                                                    <td class="">
                                                                        <img src="<%# WebUtility.ShowPic(Eval("pic").ToString()) %>" height="50" width="50" id="sort_pic_<%# Eval("sortid") %>" onclick="showmap(<%# Eval("sortid") %>);return false;">
                                                                        <a href="javascript:" onclick="showmap(<%# Eval("sortid") %>);return false;">上传图片</a>
                                                                    </td>

                                                                    <td class=" last">
                                                                        <a href='FoodSortDetail.aspx?id=<%#Eval("sortid") %>&tid=<%# Eval("togonum") %>'>编辑</a>
                                                                        <span class="separator">|</span>
                                                                        <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("sortid")%>' OnClientClick="return DelConfirm();"
                                                                            runat="server" ID="del">删除</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="scott">
                                            <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="AspNetPager1">
                                            </webdiyer:AspNetPager>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <!--foot start-->
            <uc2:Foot runat="server" ID="FootUC" />
            <!--foot end-->
        </div>

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
                <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 400px; height: 100px"
                    frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
            </div>
            <div id="Upload">
            </div>
            <br />
            <input type="button" value="确定" onclick="goog()" id="btsubmit" />
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

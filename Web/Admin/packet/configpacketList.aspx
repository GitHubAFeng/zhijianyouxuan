<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configpacketList.aspx.cs" Inherits="Admin_packet_configpacketList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>红包设置管理-<%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });
        AddLoadFun(init);
        Request =
        {
            QueryString: function (item) {
                var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
                return svalue ? svalue[1] : svalue;
            }
        }
        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
            var d = Request.QueryString("state");
            $("#ali" + d).addClass("active");
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function jDel() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要操作的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true;
        }

        //绑定优惠券
        function checksetcard() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要操作的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            var tbuid = $("#tbuid").val();
            if (tbuid == "") {
                alert("请输入用户编号");
                return false;
            }
            return true;
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }
        function AddPacket() {
            window.location.href = "userpacketDetail.aspx";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdDels" runat="server" />
        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
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
                            <div class="main-col-inner">
                                <div id="div5">
                                </div>
                                <div id="divMessages">
                                </div>
                                <div class=" notice" style="margin-bottom: 10px;">提示：如果不启用某个获得红包方式，客户将不获得红包。</div>
                                <div class="clear">
                                </div>
                                <div align="center" class="hshop_class">
                                    <label runat="server" id="lbpname" style="font-size: 14px; color: #EA7601">
                                        红包配置管理</label>
                                </div>
                                <div class="grid">
                                    <div class="hor-scroll" style="overflow: auto;">
                                        <table class="data" cellspacing="0" id="grid_table">
                                            <col class="a-center" width="5%" />
                                               <col width="6%" />
                                            <col />
                                            <col width="15%" />
                                            <col width="6%" />
                                            <col width="8%" />
                                            <col width="18%" />
                                            <col width="15%" />
                                            <thead>
                                                <tr class="headings">
                                                    <th>
                                                        <span class="nobr">
                                                            <a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                            class="sort-title">编号</span></a></span>
                                                    </th>
                                                     <th>
                                                        <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                            class="sort-title">发放位置</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                            class="sort-title">获得红包方式</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a href="#" name="store_id" title="asc" class="not-sort"><span
                                                            class="sort-title">是否开启</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                            <!--sort-arrow-desc-->
                                                            <span class="sort-title">红包个数</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                            <!--sort-arrow-desc-->
                                                            <span class="sort-title">金额</span></a></span>
                                                    </th>
                                                    <th>
                                                        <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                            <!--sort-arrow-desc-->
                                                            <span class="sort-title">使用条件</span></a></span>
                                                    </th>
                                                    <th class="no-link last">
                                                        <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                            <!--sort-arrow-desc-->
                                                            <span class="sort-title">有效期</span></a></span>
                                                    </th>
                                                    <th class="no-link last">
                                                        <span class="nobr"><a href="#" name="created_at" title="asc" class="not-sort">
                                                            <!--sort-arrow-desc-->
                                                            <span class="sort-title">编辑</span></a></span>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody class="grid_data">
                                                <asp:Repeater ID="rtpUserlist" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="pointer" title="">
                                                            <td class="" width="20px">
                                                                <%# Eval("dataid") %>
                                                            </td>
                                                              <td class="">
                                                                <%# Convert.ToInt32(Eval("autotype")) == 0 ? "注册发红包" :"下单发红包"  %>
                                                            </td>
                                                            <td class="">
                                                                <%# WebUtility.GetStyleByPacket(Eval("autotype").ToString()) %>
                                                            </td>
                                                            <td class="">
                                                                <%# Convert.ToInt32(Eval("isopen")) == 0 ? "关闭" :"开启"  %>
                                                            </td>
                                                            <td class="">
                                                                <%#Eval("reveint1") %>

                                                            </td>
                                                            <td class="">
                                                                <%#Eval("distance") %>

                                                            </td>
                                                            <td class="">
                                                                <%#Eval("revevar1")%>
                                                            </td>
                                                            <td class="">
                                                                <%# Eval("revevar2")%>&nbsp;天
                                                            </td>
                                                            <td class="">
                                                                <a href="userpacketDetail.aspx?id=<%# Eval("dataid") %>">编辑</a>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--foot start-->
            <uc2:Foot runat="server" ID="FootUC" />
        </div>
        <!--foot end-->
    </form>
</body>
</html>



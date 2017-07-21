<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Deliverordersum.aspx.cs" Inherits="qy_54tss_Admin_Sale_valetorder" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>配送员结算-
        <%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        AddLoadFun(init);

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();



        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的订单!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function jDel() {
            var ddlgroup = $("#ddlgroup").val();
            if (ddlgroup === "-1") {
                alert("请选择要发送的群组!");
                return false;
            }
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要操作的记录!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            showload_super();
            return true;
        }



        ///type=0表示搜索昨天的，1表示今天的。
        function daysearch(type) {
            var timestart = "";
            var timeend = "";
            if (type == "0") {
                timestart = $("#hfyestoday").val() + "";
                timeend = $("#hfyestoday").val() + "";
            }
            else {
                timestart = $("#hftoday").val() + "";
            }
            $("#tbStartTime").val(timestart);
            $("#tbEndTime").val(timeend);
        }


    </script>

    <style type="text/css">
        .summary {
            border: 1px solid #ccc;
        }

            .summary th {
                color: #333333;
                text-align: center;
                border: 1px solid #ccc;
            }

            .summary td {
                color: #333333;
                text-align: center;
                border: 1px solid #ccc;
                height: 25px;
                line-height: 25px;
            }

            .summary .tableft {
                padding-left: 3px;
                text-align: left;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hfyestoday" runat="server" />
        <asp:HiddenField ID="hftoday" runat="server" />


        <asp:HiddenField ID="hfflag" runat="server" />
        <asp:HiddenField ID="hdDels" runat="server" />
        <asp:HiddenField runat="server" ID="hfstate" Value="-1" />
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
                            <uc3:left runat="server" ID="adleft" />
                        </div>
                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>
                                <fieldset class="AdminSearchform">
                                    <legend>查询条件 </legend>

                                    <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; float: left;"
                                        class="condition_table">


                                        <tr>
                                            <td align="right" class="tab_label">
                                                <span>骑士名称：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbshopname" runat="server" CssClass="j_text"></asp:TextBox>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td align="right" class="tab_label">
                                                <span>时间：</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" CssClass="j_text" ID="tbStartTime" Width="140px" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                至
                                                    <asp:TextBox runat="server" ID="tbEndTime" CssClass="j_text" Width="140px" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>


                                            </td>
                                        </tr>


                                        <tr>
                                            <td align="right" class="tab_label">
                                                <span style="width: 160px;">订单状态：</span>
                                            </td>
                                            <td>
                                                <asp:CheckBoxList runat="server" ID="rblstate" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>





                                        <tr>
                                            <td align="right" class="tab_label"></td>
                                            <td>
                                                <asp:Button runat="server" ID="Button1" class="form-button" Text="搜索" OnClick="btSearch_Click" />

                                                <asp:Button runat="server" ID="Button5" class="form-button" Text="查询昨天" OnClick="btSearch_Click" OnClientClick="daysearch(0)" />
                                                <asp:Button runat="server" ID="Button6" class="form-button" Text="查询今天" OnClick="btSearch_Click" OnClientClick="daysearch(1)" />


                                                 <asp:Button runat="server" ID="Button2" class="form-button" Text="导出EXCEL" OnClick="out_Click" />

                                            </td>
                                        </tr>
                                    </table>
                                    <div class="clear">
                                    </div>

                                </fieldset>
                                <fieldset class="AdminSearchform">
                                    <legend>统计</legend>
                                    <div style="clear: both; padding: 10px;">
                                        <span>总订单量:<span runat="server" id="lbordercount" style="margin-left: 20px; font-weight: bold; color: Red">0</span></span>
                                        <span style="margin-left: 20px;">总金额:<span runat="server" id="lballprice" style="margin-left: 20px; font-weight: bold; color: Red">0</span>元</span>
                                    </div>
                                </fieldset>

                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                    <ContentTemplate>
                                        <div class="scott">
                                            <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                                CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                                HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                                CustomInfoSectionWidth="27%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                                TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                                ShowPageIndex="True" PageSize="50" SubmitButtonClass="flatbutton" SubmitButtonText="GO"
                                                TextAfterPageIndexBox=" 页 " Wrap="False" AlwaysShow="true">
                                            </webdiyer:AspNetPager>
                                        </div>
                                        <div id="sales_order_grid_massaction" style="clear: both;">
                                        </div>

                                        <table width="100%" class="summary">
                                            <tbody>
                                                <tr>

                                                    <th style="width: 5%">骑士编号
                                                    </th>

                                                    <th style="width: 15%">骑士
                                                    </th>

                                                    <th style="width: 8%">基本工资
                                                    </th>
                                                     <th style="width: 8%">配送费提成
                                                    </th>
                                                     <th style="width: 8%">总工资
                                                    </th>
                                                    


                                                    <th style="width: 14%">总金额
                                                    </th>
                                                    <th style="width: 8%">订单数
                                                    </th>

                                                    <th style="width: 8%">应收款
                                                    </th>
                                                    <th style="width: 8%">应结款
                                                    </th>
                                                </tr>
                                                <asp:Repeater runat="server" ID="rptsum">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("IsDelete")%>
                                                            </td>
                                                            <td class="tableft">
                                                                <%# Eval("togoname") %>
                                                            </td>
                                                              <td>
                                                                <%# Eval("basewage")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("percentagewage")%>
                                                            </td>

                                                             <td>
                                                                <%# Eval("allwage")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("allprice")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("allcount")%>
                                                            </td>

                                                             <td>
                                                                <%# Eval("getmoney")%>
                                                            </td>

                                                             <td>

                                                                 <%# Eval("deliverpayweb")%>
                                                                
                                                                 
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <div class="clear">
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

            </div>
            <uc2:Foot runat="server" ID="FootUC" />
            <!--foot end-->
        </div>
    </form>
</body>
</html>

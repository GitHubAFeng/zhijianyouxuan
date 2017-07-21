<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uservipgradelist.aspx.cs"
    Inherits="Admin_shop_uservipgradelist" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户等级管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->
    <link href="../css/shopadmin.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        AddLoadFun(init);



        var Table;
        function init() {
            // Table = new CheckTable("rptsubItem");
            $("#rptsubItem tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
            $("#rptsubItem tr:even").addClass("even pointer");
            $("#loading-mask").hide();
            Request =
            {
                QueryString: function(item) {
                    var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i"));
                    return svalue ? svalue[1] : svalue;
                }
            }
            $("#A" + Request.QueryString("id")).addClass("active");
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

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
        }


        function scanerProduct(tid) {
            window.open('../../ticket/showticket.aspx?id=' + tid);
        }

        function showmap(aid) {
            var iTop = (window.screen.availHeight - 30 - 400) / 2;       //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - 600) / 2;        //获得窗口的水平位置;
            window.open("uploadpic.aspx?id=" + aid, "newwindow", "height=360, width=400, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no,top=" + iTop + ",left=" + iLeft);
        }
    </script>

    <style type="text/css">
        .grid th
        {
            border: 1px solid #DADFE0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
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
                                    <ul id="diagram_tab" class="tabs-horiz">
                                        <li><a href="uservipgradelist.aspx" id="diagram_tab_orders" title="用户等级" class="tab-item-link active">
                                            <span><span class="changed"></span><span class="error"></span>用户等级</span> </a>
                                        </li>
                                        <li><a href="gradefavourable.aspx" id="A1" title="等级优惠" class="tab-item-link "><span>
                                            <span class="changed"></span><span class="error"></span>等级优惠</span> </a></li>
                                    </ul>
                                    <div class="scott">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " UrlPagingTarget="_self" UrlPageIndexName="p" UrlPageSizeName="s"
                                            UrlPaging="True" PageIndexBoxClass="flattext" ShowPageIndex="True" PageSize="50"
                                            SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                                            Wrap="False">
                                        </webdiyer:AspNetPager>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <div align="center" class="hshop_class">
                                                <label runat="server" id="lbpname" style="font-size: 14px; color: #EA7601">
                                                    用户等级管理</label>
                                            </div>
                                            <asp:GridView ID="rptsubItem" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                                                OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView_RowCancelingEdit"
                                                OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID号">
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>
                                                            <asp:Label runat="server" ID="lb_id" Text='<%# Eval("dataid") %>' Style="display: none"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="等级名称">
                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                        <ItemTemplate>
                                                            <%#Eval("GradeName")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <epc:TextBox ID="Lbl_classname" Text='<%#Eval("GradeName")%>' runat="server"></epc:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="等级图标">
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                        <ItemTemplate>
                                                            <img src="<%# WebUtility.ShowPic(Eval("Reve2").ToString()) %>" />
                                                            <a href="#" onclick="showmap(<%# Eval("dataid") %>);return false;">上传图片</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="兑换所需积分" Visible="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                        <ItemTemplate>
                                                            <%#Eval("GaiPoint")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <epc:TextBox ID="Lbl_GaiPoint" Text='<%#Eval("GaiPoint")%>' runat="server" Width="60"
                                                                onkeypress="return only_num(event)"></epc:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="排序">
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                        <ItemTemplate>
                                                            <%#Eval("Reve1")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <epc:TextBox ID="Lbl_priority" Text='<%#Eval("Reve1")%>' runat="server" Width="60"
                                                                onkeypress="return only_num(event)"></epc:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
                                                    <asp:TemplateField HeaderText="操作">
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("dataid")%>' OnClientClick="return DelConfirm();"
                                                                runat="server" ID="delNew">删除</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div id="nodata" runat="server" style="display: none;">
                                                没有任何数据!</div>
                                            <div align="center" class="hshop_class" style="margin-top: 15px;">
                                                <label runat="server" id="lbadd" style="font-size: 14px; color: #EA7601">
                                                    添加用户等级
                                                </label>
                                            </div>
                                            <div style="text-align: center">
                                                <table style="clear: both; width: 100%;" class="checkdata">
                                                    <tr>
                                                        <th style="width: 60%; text-align: center">
                                                            名称
                                                        </th>
                                         
                                                        <th style="width: 40%; text-align: center">
                                                            排序
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <epc:TextBox runat="server" ID="tbclassname" reg="\S" tip="名称不能为空"></epc:TextBox>
                                                        </td>
                                                     
                                                        <td>
                                                            <epc:TextBox Width="60" Text="1" runat="server" ID="tbpri" onkeypress="return only_num(event)"
                                                                reg="^\d+$" tip="排序栏请输入数字"></epc:TextBox><div class="mynotice">
                                                                    数字大，排序在前</div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                      
                                                        <td>
                                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                                OnClientClick="return j_submitdata('checkdata')" Text="保存"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="scott">
                                        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="AspNetPager1">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc2:Foot runat="server" ID="FootUC" />
    </form>
</body>
</html>

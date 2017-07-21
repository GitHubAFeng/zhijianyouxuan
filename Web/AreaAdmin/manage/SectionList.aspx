<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SectionList.aspx.cs" Inherits="qy_54tss_AreaAdmin_SectionList" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>区域管理 -
        <%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" media="all" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
    </script>

    <script type="text/javascript">
        AddLoadFun(init);

        var Table;

        function init() {
            Table = new CheckTable("grid_table");

            $(document).ready(function() {
                $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
                $(".grid_data tr:even").addClass("even pointer");

            });
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的产品!");
                return false;
            }
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }


        $(document).ready(function() { $("#A4").addClass("active") });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <div class="wrapper">
        <!--banner start-->
        <uc1:TogoBanner runat="server" ID="Banner" />
        <!--banner end-->
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
                                    <fieldset class="AdminSearchform">
                                        <legend>区域搜索 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <span class="span12">区域名称 </span>
                                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="j_text" />
                                                    <asp:DropDownList ID="ddlcity" runat="server" CssClass="j_select" Width="100" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">所有城市</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="filter-actions a-right">
                                                    <asp:Button runat="server" ID="btSearch" class="form-button" Text="搜索" OnClick="btSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <div class="scott">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="27%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                            ShowPageIndex="True" PageSize="20" SubmitButtonClass="flatbutton" SubmitButtonText="GO"
                                            TextAfterPageIndexBox=" 页 " Wrap="False">
                                        </webdiyer:AspNetPager>
                                    </div>
                                    <div id="sales_order_grid_massaction" style="clear: both;">
                                    </div>
                                    <div id="Div1" style="clear: both;">
                                        <div align="center" style="margin-top: 20px; background-image: url('../images/admin_bg_1.gif');
                                            text-align: center; height: 20px;">
                                            <label runat="server" id="mycityname" style="font-size: 14px; color: #fff; padding: 3px;">
                                                区域管理
                                            </label>
                                        </div>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <asp:GridView ID="rptsubItem" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                                                OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView_RowCancelingEdit"
                                                OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID号">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lb_id" Text='<%# Eval("SectionID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="名称">
                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                        <ItemTemplate>
                                                            <%#Eval("SectionName")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <epc:TextBox ID="Lbl_classname" Text='<%#Eval("SectionName")%>' runat="server"></epc:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="排序">
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                        <ItemTemplate>
                                                            <%#Eval("pri")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <epc:TextBox ID="Lbl_priority" Text='<%#Eval("pri")%>' runat="server" Width="60"
                                                                onkeypress="return only_num(event)"></epc:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
                                                    <asp:TemplateField HeaderText="操作">
                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("SectionID")%>' OnClientClick="return DelConfirm();"
                                                                runat="server" ID="delNew">删除</asp:LinkButton>
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div id="nodata" runat="server" style="display: none;">
                                                没有任何数据!</div>
                                            <div align="center" style="margin-top: 20px; background-image: url('../images/admin_bg_1.gif');
                                                text-align: center; height: 20px;">
                                                <label runat="server" id="lbadd" style="font-size: 14px; color: #fff; padding: 3px;">
                                                    添加区域
                                                </label>
                                            </div>
                                            <div style="text-align: center;" runat="server" id="divadd">
                                                <table style="clear: both; width: 100%">
                                                    <tr>
                                                        <th style="width: 30%; text-align: center">
                                                            城市
                                                        </th>
                                                        <th style="width: 40%; text-align: center">
                                                            区域名称
                                                        </th>
                                                        <th style="width: 30%; text-align: center">
                                                            排序
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="DDLArea" runat="server" Width="70"  CssClass="j_select">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <epc:TextBox Width="130px" MaxLength="4" runat="server" ID="tbclassname"></epc:TextBox>
                                                        </td>
                                                        <td>
                                                            <epc:TextBox Width="60" Text="1" MaxLength="4" runat="server" ID="tbpri" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                                                onafterpaste="this.value=this.value.replace(/\D/g,'1')" onkeypress="return only_num(event)"></epc:TextBox><div
                                                                    class="mynotice">
                                                                    数字大，排序在前</div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                                Text="保存"></asp:Button>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <uc2:Foot runat="server" ID="FootUC" />
    </div>
    <!--foot start-->
    <!--foot end-->
    </form>
</body>
</html>

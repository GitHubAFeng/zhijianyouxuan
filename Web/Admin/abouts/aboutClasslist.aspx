<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aboutClasslist.aspx.cs" Inherits="Admin_abouts_aboutClasslist" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc4" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register TagPrefix="uc3" Src="~/Admin/Adleft.ascx" TagName="left" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文章分类管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function() { $("#loading-mask").hide(); });
        AddLoadFun(init);

        var Table;
        function init() {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function() { $(this).addClass("on-mouse"); }).mouseout(function() { $(this).removeClass("on-mouse"); });
            $(".grid_data tr:even").addClass("even pointer");
            $("#loading-mask").hide();
        }

        function Del() {
            var nums = Table.GetChecks();
            if (nums == undefined || nums.length == 0) {
                alert("请选择要删除的分类!");
                return false;
            }
            document.getElementById("hidDels").value = ArrayToString(nums);
            return DelConfirm();
        }

        function loading() {
            $("#loading-mask").show();
        }

        function loadover() {
            $("#loading-mask").hide();
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
    <asp:HiddenField ID="hidDels" runat="server" />
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="images/Control" />
    <div class="wrapper">
        <!--banner start-->
        <uc4:TogoBanner runat="server" ID="Banner" />
        <!--banner end-->
        <!--center start-->
        <div class="middle" id="anchor-content">
             <div id="page:main-container">
                <div class="columns ">
                    <div class="side-col" id="page:left">
                    <uc3:left ID="Left1" runat="server" />
                    </div>
                    <div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>
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
                                        <div align="center" style="margin-top: 20px; background-image: url('../images/admin_bg_1.gif');
                                            text-align: center; height: 20px;">
                                            <label runat="server" id="Label1" style="font-size: 14px;">
                                                   分类管理(已经的分类请不要删除)
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
                                                            <asp:Label runat="server" ID="lb_id" Text='<%# Eval("id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="名称">
                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                        <ItemTemplate>
                                                           <%#Eval("Name")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                              <asp:TextBox ID="Lbl_classname" Text='<%#Eval("Name")%>' runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="排序">
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                        <ItemTemplate>
                                                            <%#Eval("FullId")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                              <asp:TextBox ID="Lbl_priority" Text='<%#Eval("FullId")%>' runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
                                                    <asp:TemplateField HeaderText="操作">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("id")%>' OnClientClick="return DelConfirm();"
                                                                runat="server" ID="delNew">删除</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div id="nodata" runat="server" style="display: none;">
                                                没有任何数据!</div>
                                            <div align="center" style="margin-top: 20px; background-image: url('../images/admin_bg_1.gif');
                                                text-align: center; height: 20px;">
                                                <label runat="server" id="lbadd" style="font-size: 14px;">
                                                </label>
                                            </div>
                                            <div style="text-align: center;" runat="server" id="divadd">
                                                <table style="clear: both; width: 100%">
                                                    <tr>
                                                        <th style="width: 50%; text-align: center">
                                                            名称
                                                        </th>
                                                        <th style="width: 50%; text-align: center">
                                                            排序
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tbclassname"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <epc:TextBox Width="60" Text="1" MaxLength="4" runat="server" ID="tbpri" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                                                onafterpaste="this.value=this.value.replace(/\D/g,'')"></epc:TextBox><div class="mynotice">数字大，排序在前</div>
                                                        </td>
                                                    </tr>
                                                    <tr>
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
        <!--foot start-->
        <uc2:Foot runat="server" ID="FootUC" />
        <!--foot end-->
    </div>
    </form>
</body>
</html>


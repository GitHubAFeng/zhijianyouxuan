<%@ Page Language="C#" AutoEventWireup="true" CodeFile="moduledetail.aspx.cs" Inherits="Admin_Permission_moduledetail" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register TagPrefix="uc3" Src="~/Admin/Adleft.ascx" TagName="left" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>模块管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
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

    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });

        //給一個輸入框賦值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }

    </script>

    <script language="javascript" type="text/javascript">

        function GotoBuildingDetail() {
            window.location.href = "BuildingDetail.aspx";
        }
        function GotoADList() {
            window.location.href = "Modulelist.aspx";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <!--加載中顯示的div-->
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
                        <uc3:left ID="Left1" runat="server" />
                        <div class="main-col" id="content">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="main-col-inner">
                                        <div id="divMessages">
                                        </div>
                                        <div style="visibility: visible;" class="content-header">
                                            <h3 class="icon-head head-customer">
                                                <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                            <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                                <p style="" class="content-buttons form-buttons">
                                                    <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClientClick='GotoADList(); return false;'
                                                        Text="返回"></asp:Button>
                                                    <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                        Text="保存"></asp:Button>
                                                </p>
                                            </div>
                                        </div>
                                        <!--start-->
                                        <div class="entry-edit">
                                            <div id="customer_info_tabs_account_content" style="">
                                                <div class="entry-edit">
                                                    <div class="entry-edit-head">
                                                        <h4 class="icon-head head-billing-address">模块信息</h4>
                                                    </div>
                                                    <fieldset class="np">
                                                        <div class="order-address" id="order-billing_address_fields">
                                                            <div class="content">
                                                                <div class="hor-scroll">
                                                                    <table class="form-list" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountprefix">
                                                                                        模块名称<span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbM_CName" CanBeNull="必填" Width="400px" class=" required-entry required-entry input-text"></epc:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        编辑父类 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:DropDownList ID="tbM_ParentID" runat="server" Width="100" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Value="0">一级模块</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        排序 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <epc:TextBox runat="server" ID="tbM_OrderLevel" RequiredFieldType="数据校验" Text="1"
                                                                                        CanBeNull="必填" Width="50px" class=" required-entry required-entry input-text"></epc:TextBox><div
                                                                                            class="mynotice">
                                                                                            数字大，排在前。
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="label">
                                                                                    <label for="_accountfirstname">
                                                                                        內容 <span class="required">*</span></label>
                                                                                </td>
                                                                                <td class="value">
                                                                                    <asp:TextBox runat="server" ID="tbM_Directory" TextMode="MultiLine" Style="width: 500px; height: 50px;"></asp:TextBox>
                                                                                    <div class="mynotice" style="display: block; margin-top: 10px;">
                                                                                        输入此模块涉及之页面名称，多个页面用逗号","隔开。(一级模快留空即可)
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <small>&nbsp;</small>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="hide">

                                        <div class="clear"></div>
                                        <div class="mynotice" style="margin-top: 10px; margin-bottom: 10px;">提示：操作项"查看"的权值必须设置成2,"添加"的权值必须设置成4,"编辑"的权值必须设置成8,"删除"的权值必须设置成16</div>
                                        <div class="clear"></div>
                                        <div style="width: 100%; margin-top: 10px;" runat="server" id="divitems">
                                            <div id="sales_order_grid_massaction" style="clear: both;">
                                                <div class="hshop_class" align="center">
                                                    <label runat="server" id="Label1" style="font-size: 14px; color: #EA7601">
                                                        操作项目管理
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
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lb_id" Text='<%# Eval("mid") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="名称">
                                                                <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                                <ItemTemplate>
                                                                    <%#Eval("pername")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="Lbl_name" CssClass="j_text" Width="100" Text='<%#Eval("pername")%>' runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="权值">
                                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                <ItemTemplate>
                                                                    <%#Eval("pvalue")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="Lbl_Price" CssClass="j_text" Text='<%#Eval("pvalue")%>' runat="server" Width="60"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="排序">
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                                <ItemTemplate>
                                                                    <%#Eval("ReveInt")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="Lbl_Inve1" CssClass="j_text" Text='<%#Eval("ReveInt")%>' runat="server" Width="60"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
                                                            <asp:TemplateField HeaderText="操作">
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("mid")%>' OnClientClick="return DelConfirm();"
                                                                        runat="server" ID="delNew">删除</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div id="nodata" runat="server" style="display: none;">
                                                        没有任何数据!
                                                    </div>
                                                    <div class="hshop_class" align="center" style="margin-top: 10px;">
                                                        <label runat="server" id="lbadd" style="font-size: 14px; color: #EA7601">
                                                        </label>
                                                    </div>
                                                    <div style="text-align: center;" runat="server" id="divadd">
                                                        <table style="clear: both; width: 100%">
                                                            <tr>
                                                                <th style="width: 40%; text-align: center">名称
                                                                </th>
                                                                <th style="width: 30%; text-align: center">权值
                                                                </th>
                                                                <th style="width: 30%; text-align: center">排序
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <epc:TextBox runat="server" ID="tbpername"></epc:TextBox>
                                                                </td>
                                                                <td>
                                                                    <epc:TextBox Width="60" Text="1" MaxLength="4" runat="server" ID="tbpvalue" onkeypress="return onlynum(event)"></epc:TextBox>
                                                                    <div class="mynotice" style="display: block; margin-top: 5px;">
                                                                        请输入2的指数后的值，不能重复
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <epc:TextBox Width="60" Text="1" MaxLength="4" runat="server" ID="tbReveInt" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                                                        onafterpaste="this.value=this.value.replace(/\D/g,'')" onkeypress="return onlynum(event)"></epc:TextBox><div
                                                                            class="mynotice" style="display: block; margin-top: 5px;">
                                                                            数字大，排在前
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button2" runat="server" CssClass="button_1" Text="保存" OnClick="master_Add"
                                                                        OnClientClick="return matercheck()"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <script type="text/javascript">
                                                            function matercheck() {
                                                                var tbtitle = $("#tbtitle").val() + "";
                                                                if (tbtitle == "") {
                                                                    alert('请输入名称');
                                                                    return false;
                                                                }
                                                                return true;
                                                            }
                                                        </script>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                            </div>

                                        <!--end-->
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

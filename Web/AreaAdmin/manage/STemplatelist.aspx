<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STemplatelist.aspx.cs" Inherits="qy_54tss_AreaAdmin_shop_STemplatelist" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>备注快捷选项管理-<%= WebUtility.GetMyName() %></title>
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

        function IsNull() {
            if ($("#tbclassname").val() == "") {
                alert("名称不能为空!");
                return false;
            }
            return true;
        }
        
    </script>

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
                                                <label runat="server" id="lbpname" style="font-size: 12px; color:#EA7601;">
                                                    备注快捷选项(排序值越大，排序越在前)
                                                </label>
                                            </div>
                                            <div style="margin-bottom:15px;">
                                            <asp:GridView ID="rptsubItem" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                                                OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridView_RowCancelingEdit"
                                                OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemTemplate>
                                                        　<%# Container.DataItemIndex + 1%> 
                                                            <asp:Label runat="server" ID="lb_id" Text='<%# Eval("id") %>' style=" display:none"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="名称">
                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                        <ItemTemplate>
                                                            <%#Eval("classname")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <epc:TextBox ID="Lbl_classname" Text='<%#Eval("classname")%>' runat="server"></epc:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="排序">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemTemplate>
                                                            <%#Eval("Priority")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <epc:TextBox ID="Lbl_priority" Width="60" Text='<%#Eval("Priority")%>' runat="server" onkeypress="return only_num(event)"></epc:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                             
                                                    <asp:CommandField HeaderText="编辑" ShowEditButton="True"/>
                                                    <asp:TemplateField HeaderText="操作">
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("id")%>' OnClientClick="return DelConfirm();"
                                                                runat="server" ID="delNew">删除</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            </div>
                                            <div id="nodata" runat="server" style="display: none;">
                                                没有任何数据!</div>
                                            <div align="center"  class="hshop_class">
                                                <label runat="server" id="lbadd" style="font-size: 12px;">
                                                    添加
                                                </label>
                                            </div>
                                            <div style="text-align: center">
                                                <table style="clear: both; width: 100%;">
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
                                                            <epc:TextBox runat="server" ID="tbclassname" ></epc:TextBox>
                                                        </td>
                                                        <td>
                                                            <epc:TextBox Width="60" Text="1" runat="server" ID="tbpri" onkeypress="return only_num(event)"></epc:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClientClick="return IsNull()" OnClick="btSave_Click"
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
    </div>
    <uc2:Foot runat="server" ID="FootUC" />
    </form>
</body>
</html>

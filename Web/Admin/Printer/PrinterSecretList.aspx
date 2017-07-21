<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrinterSecretList.aspx.cs"
    Inherits="qy_54tss_Admin_Printer_PrinterSecretList" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="printerleft" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>打印机列表－<%= WebUtility.GetMyName()%></title>
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function(){$("#loading-mask").hide();});
        AddLoadFun(init);

        var Table;
        function init() 
        {
            Table = new CheckTable("grid_table");
            $(".grid_data tr").mouseover(function() {$(this).addClass("on-mouse");}).mouseout(function() {$(this).removeClass("on-mouse");});
            $(".grid_data tr:even").addClass("even pointer");  
            $("#loading-mask").hide();
        }
        
        function Del()
        {
            var nums = Table.GetChecks();
            if(nums == undefined || nums.length == 0)
            {
                alert("请选择要进行操作的打印!");
                return false;
            }            
            document.getElementById("hdDels").value = ArrayToString(nums);
            return DelConfirm();
        }   
        
        function Fun()
        {
            var nums = Table.GetChecks();
            if(nums == undefined || nums.length == 0)
            {
                alert("请选择要进行操作的打印!");
                return false;
            }            
            document.getElementById("hdDels").value = ArrayToString(nums);
            return true();
        }   
        
        function loading()
        {  
            $("#loading-mask").show();
        }
        
        function loadover()
        {
            $("#loading-mask").hide();
        }
    </script>

    <script language="javascript" type="text/javascript">
        
        function GotoAllList()
        {
            window.location.href = "PrinterSecretList.aspx";
        }
        function GotoUsedList()
        {
            window.location.href = "PrinterSecretList.aspx?type=1";
        }
        function GotoNotUsedList()
        {
            window.location.href = "PrinterSecretList.aspx?type=0";
        }
        function GotoAdd()
        {
            window.location.href = "AddPrinterSecret.aspx";
        }
       
    </script>

    <style type="text/css">
        .p-left
        {
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdDels" runat="server" />
    <asp:HiddenField runat="server" ID="hidTogoId" />
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
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
                    <uc3:printerleft ID="printerleft1" runat="server" />
                    &nbsp;<div class="main-col" id="content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>
                                    <fieldset class="AdminSearchform">
                                        <legend>查询条件 </legend>
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px; margin-bottom:10px; margin-top:5px; height:25px;">
                                            <tr>
                                                <td>
                                                    <span class="span12">
                                                        <asp:DropDownList runat="server" ID="ddlSearchType" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchType_SelectedIndexChanged">
                                                            <asp:ListItem Text="打印机编号" Value="Num"></asp:ListItem>
                                                            <asp:ListItem Text="打印机Sn" Value="Sn"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </span>
                                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="inputclass" Style="border: 1px solid #878787;
                                                        font-size: 14px;" />
                                                </td>
                                                <td class="filter-actions ">
                                                    <asp:Button runat="server" ID="btSearch" class="form-button p-left" Text="搜索" OnClick="btSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr style="display:none">
                                                <td>
                                                    <button type="button" class="scalable back " onclick="GotoAllList()" style=" margin-left:20px;">
                                                        <span>所有打印机</span></button>
                                                    <button type="button" class="scalable back " onclick="GotoUsedList()" style="">
                                                        <span>在使用的打印机</span></button>
                                                    <button type="button" class="scalable back " onclick="GotoNotUsedList()" style="">
                                                        <span>未使用的打印机</span></button>
                                                    <button type="button" class="scalable " onclick="GotoAdd()" style="">
                                                        <span>新增打印机</span></button>
                                                </td>
                                            </tr>
                                        </table>
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
                                                            <asp:LinkButton runat="server" ID="lbDelsom" OnClientClick="Del()" OnClick="DelList_Click">删除选定</asp:LinkButton>
                                                        </a></a>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="grid">
                                        <div class="hor-scroll">
                                            <table class="data" cellspacing="0" id="grid_table">
                                                <col class="a-center" width="20" />
                                                <col width="70" />
                                                <col width="70" />
                                                <col width="70" />
                                                <col width="70" />
                                                <col width="70" />
                                                <col width="50" />
                                                <col width="100" />
                                                <thead>
                                                    <tr class="headings">
                                                        <th align=center>
                                                            <span class="nobr">&nbsp;</span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">打印机sn1</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">打印机sn2</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">打印机key</span></a></span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">对应商店</span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">手机号码</span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">状态</span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr" style="padding-top:3px; *padding-top:5px;">操作</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rptPrinterList" runat="server" OnItemCommand="rptPrinterList_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="">
                                                                    <input name="" id="_inut" value="<%# Eval("DataId")%>" class="massaction-checkbox"
                                                                        type="checkbox">
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("PrinterNum")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("PrinterSn")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("PrinterKey")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# GetTogo(Eval("PrinterNum"))%>&nbsp;
                                                                </td>
                                                                <td class="">
                                                                    <%# Getphone(Eval("PrinterNum"))%>&nbsp;
                                                                </td>
                                                                <td class="">
                                                                    <%# Getcount(Eval("PrinterNum"))%>&nbsp;
                                                                </td>
                                                                <td class=" last" align=center>
                                                                    <a href='AddPrinterSecret.aspx?id=<%#Eval("DataId")%>'>编辑</a> <span class="separator">
                                                                        |</span>
                                                                    <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("DataId")%>' OnClientClick="return DelConfirm();"
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
    </div>
    <!--foot start-->
    <uc2:Foot runat="server" ID="FootUC" />
    <!--foot end-->
    </form>
</body>
</html>


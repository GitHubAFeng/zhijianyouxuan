<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebBasic.aspx.cs" Inherits="Admin_WebBasic" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统参数设置-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function(){$("#loading-mask").hide();});
        AddLoadFun(init);

        var Table;
        function init() 
        {
            $(".grid_data tr").mouseover(function() {$(this).addClass("on-mouse");}).mouseout(function() {$(this).removeClass("on-mouse");});
            $(".grid_data tr:even").addClass("even pointer");  
        }

        function loading()
        {  
            //$("#loading-mask").show();
        }
        
        function loadover()
        {
            //$("#loading-mask").hide();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdDels" runat="server" />
            <asp:HiddenField runat="server" ID="hidTogoId" />
            <!--加载中显示的div-->
            <div id="loading-mask" style="display: none;">
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
                            <div class="side-col" id="page:left">
                                <uc3:left runat="server" ID="left" />
                            </div>
                            <div class="main-col" id="content">
                                <div class="main-col-inner">
                                    <div id="divMessages">
                                    </div>
                                    <div id="sales_order_grid_massaction" style="clear: both;">
                                        <table class="massaction" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        系统参数设置
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
                                                <col width="10%" />
                                                <col width="30%" />
                                                <col />
                                                <col width="8%" />
                                                <thead>
                                                    <tr class="headings">
                                                        <th>
                                                            <span class="nobr">编号</span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">参数名称</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">参数值</span></a></span>
                                                        </th>
                                                        <th>
                                                            <span class="nobr"><a href="#" name="real_order_id" title="asc" class="not-sort"><span
                                                                class="sort-title">说明</span></a></span>
                                                        </th>
                                                        <th class="no-link last">
                                                            <span class="nobr">操作</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="grid_data">
                                                    <asp:Repeater ID="rptList" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="pointer" title="">
                                                                <td class="" width="20px">
                                                                    <%# Eval("dataid")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("key")%>
                                                                </td>
                                                                <td class="">
                                                                    <%# WebUtility.Left( WebUtility.NoHTML(Eval("Value").ToString() ), 40)%>
                                                                </td>
                                                                <td class="">
                                                                    <%# Eval("inve1")%>
                                                                </td>
                                                                <td class=" last">
                                                                    <%# setLink(Eval("key"),Eval("Value"), Eval("DataId"), Eval("stype"))%>
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
                    <!--foot start-->
                    <uc2:Foot runat="server" ID="FootUC" />
                    <!--foot end-->
                    <asp:HiddenField runat="server" ID="hidDataId" />
                </div>
                <!--弹出的窗口-->
                <div id="windown-boxfix" style="left: 544px; top: 248px; margin: auto; z-index: 99998;
                    display: none; background-color: #fff;">
                    <div id="windown-titlefix" style="width: 330px; cursor: move;">
                        <h2>
                            更新参数值</h2>
                        <span id="windown-closefix" onclick="hideobjdiv()">关闭</span>
                    </div>
                    <div id="windown-content-borderfix ">
                        <div id="windown-contentfix" style="width: 320px; height: 150px;" class="windown-textfix">
                            <ul style="margin: 0 auto; width: 260px;">
                                <li>
                                    <table cellspacing="0" cellpadding="0" border="0" style="margin-top: 30px;">
                                        <tbody>
                                            <tr>
                                                <td style="padding-bottom: 6px;">
                                                    参数名称：
                                                </td>
                                                <td style="padding-bottom: 10px;">
                                                    <asp:TextBox runat="server" ID="tbKey" Style="border: 1px solid #A6C9E1; height: 20px;"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 10px;">
                                                    参数值：
                                                </td>
                                                <td style="padding-bottom: 6px;">
                                                    <asp:TextBox runat="server" ID="tbValue" Style="border: 1px solid #A6C9E1; height: 20px;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 6px;" colspan="2">
                                                    <asp:Button runat="server" ID="tbSave" Text="保存" OnClick="btSave_Click" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div style="height: 701px; opacity: 0.5; z-index: 999901; display: none;" id="windownbgfix">
                </div>

                <script language="javascript" type="text/javascript">
                    function SetKeyValue(key1, value1,dataid)
                    {
                        $("#hidDataId").val(dataid);
                        $("#tbKey").val(key1);
                        $("#tbValue").val(value1);
                        locationdiv();
                    }
                </script>

                <script language="javascript" type="text/javascript">
                    function SetKeyValue(key1, value1,dataid)
                    {
                        $("#hidDataId").val(dataid);
                        $("#tbKey").val(key1);
                        $("#tbValue").val(value1);
                        locationdiv();
                    }
                </script>

                <script type="text/javascript">                                    function hideobjdiv()                     {                        document.getElementById("windown-boxfix").style.display = "none";                        document.getElementById("windownbgfix").style.display = "none";                    }                                        function locationdiv()                     {                        var height = 150;                        var width = 320;                        //$("#lbtogo").html(togoname);                        var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;                        var _version = $.browser.version;                        if (_version == 6.0)                         {                            $("#windown-boxfix").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });                        }                        else                         {                            $("#windown-boxfix").css({ left: "50%", top: "50%", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });                        };                        document.getElementById("windownbgfix").style.display = "block";                        $("#windownbgfix").animate({opacity:"0.5"},"normal");                        document.getElementById("windown-boxfix").style.display = "block";                    }                                                                            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

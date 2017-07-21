<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserCount.aspx.cs" Inherits="Admin_User_UserCount" %>

<%@ Register TagPrefix="ofc" Namespace="OpenFlashChart" Assembly="OpenFlashChart" %>
<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="userleft" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户统计-<%= WebUtility.GetMyName()%></title>
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

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(window).load(function() {
            $("#loading-mask").hide();
        });
        
        function initMode()
       {
            var param = document.location.search;
            var index = param.indexOf("?");
            if(index != -1)
            {
                index = param.indexOf("=");
                if(index != -1)
                {
                    param = param.substr(index + 1);
                    var txt = "";
                    switch(param)
                    {
                        case "day":txt = "日统计模式";break;
                        case "month":txt = "月统计模式";break;
                        case "year":txt = "年统计模式"; break;
                    }
                    $("#lb_Title").text(txt);
               }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
       }
       $(function(){
            initMode();
            
            $("input.countCss").click(
            function()
            {
                window.location = "UserCount.aspx?type=" + $(this).attr("info");
            }
            );
       });
    </script>

</head>
<body>
    <form id="form1" runat="server">
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
                    <uc3:userleft ID="userleft1" runat="server" />
                    &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="main-col" id="content">
                        <!--start-->
                        <div class="main-col-inner">
                            <div id="divMessages">
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div style="visibility: visible;" class="content-header">
                                        <h3 class="icon-head head-customer">
                                            <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                        <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                            <p style="" class="content-buttons form-buttons">
                                                <input type="button" class="countCss" value="查看当日用户数量统计" info="hour" />
                                                <input type="button" class="countCss" value="查看日用户数量统计" info="day">
                                                <input type="button" class="countCss" value="查看月用户数量统计" info="month">
                                                <input type="button" class="countCss" value="查看年用户数量统计" info="year" />
                                            </p>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="entry-edit">
                                <div id="customer_info_tabs_account_content" style="">
                                    <!--CountOrderData.aspx?type=hour-->
                                    <div class="entry-edit">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-billing-address">
                                                        <asp:Label runat="server" ID="lb_Title"></asp:Label></h4>
                                                    <asp:TextBox Style="border: 1px solid #878787; font-size: 14px;" ID="tb_Start" onfocus="WdatePicker({readOnly:true})"
                                                        runat="server"></asp:TextBox>
                                                    <span>~</span><asp:TextBox Style="border: 1px solid #878787; font-size: 14px;" ID="tb_End"
                                                        onfocus="WdatePicker({readOnly:true})" runat="server"></asp:TextBox>
                                                    <asp:Button ID="bt_Count" runat="server" Text="统计" OnClick="bt_Count_Click" />
                                                </div>
                                                <fieldset class="np">
                                                    <div class="order-address">
                                                        <div class="content">
                                                            <div class="hor-scroll" id="divDay">
                                                                <ofc:OpenFlashChartControl ID="chart" runat="server" Height="400" Width="600">
                                                                </ofc:OpenFlashChartControl>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <!--end-->
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

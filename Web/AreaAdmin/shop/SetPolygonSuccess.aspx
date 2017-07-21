<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPolygonSuccess.aspx.cs" Inherits="AreaAdmin_SetPolygonSuccess" %>


<%@ Register Src="../Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="../Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家设置成功-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/ScollTop.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(window).load(function(){$("#loading-mask").hide();});

        $(document).ready(function(){ WhitchActive(2)});      
    </script>
</head>
<body onunload="GUnload()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hidPolygon" runat="server" />
            <!--加载中显示的div-->
            <div id="loading-mask">
                <p class="loader" id="loading_mask_loader">
                    <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                    请等待...</p>
            </div>
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
                                    <div id="messages">
                                    </div>
                                    <div style="visibility: visible;" class="content-header">
                                        <h3 class="icon-head head-customer" runat="server" id="h3content">
                                            商家设置成功</h3>
                                        
                                    </div>
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-edit-form fieldset-legend">
                                                        商家设置成功</h4>
                                                    <div class="form-buttons">
                                                    </div>
                                                </div>
                                                <div class="fieldset " id="_accountbase_fieldset">
                                                    <div class="hor-scroll">
                                                        <table class="form-list" cellspacing="0">
                                                            <tbody>
                                                        <tr>
                                                            <td colspan="3" class="hidden">
                                                              <asp:Literal runat="server" ID="litUrl"></asp:Literal>
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--foot start-->
                <uc2:Foot runat="server" ID="FootUC" />
                <!--foot end-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

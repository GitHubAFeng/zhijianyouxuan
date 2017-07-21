<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paisong.aspx.cs" Inherits="TogoHome_paisong" %>

<%@ Register Src="~/shop/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>配送区域管理-商家中心-<%= WebUtility.GetWebName() %></title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/time.css" rel="stylesheet" type="text/css" />
    <link href="css/building.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="js/JSBulid.js?v=11" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
        var sortid = $("#DDLArea").val();
        getbuild(1, -1, -1, -1, 1, sortid, 1);
        })
    </script>

    <style type="text/css">
        #cblsearch
        {
            padding-left: 20px;
        }
        .building_list
        {
            text-align: left;
            padding-left: 5px;
        }
        .usermima li
        {
            float: left;
            clear: none;
            line-height: 25px;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="warp">
        <div class="main">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="center">
                        <uc2:LeftBanner ID="Left" runat="server" />
                        <div class="rightmenu_cont">
                            <h1 class="topbg">
                                <asp:Label ID="lbTitle" runat="server" Text="设置建筑物"></asp:Label></h1>
                            <div class="usermima">
                                <ul>
                                    <li>
                                        <div id="tabpopup1" style="width: 710px; margin-left: 0px; border: 1px solid rgb(88, 88, 88);
                                            background-color: rgb(255, 255, 255); z-index: 2000">
                                            <div class="MenuboxTab" id="section">
                                                <ul id="mysection">
                                                    <li id="one0" class="sbuild hover" style="float: left;"><a href='javascript:getbuild(1 , -1 , -1 , -1 ,1 ,-1 , 0);'>
                                                        全部</a></li>
                                                    <asp:Repeater ID="rptSectinList" runat="server">
                                                        <ItemTemplate>
                                                            <li id="one<%# Eval("SectionID")%>" class="sbuild " style="float: left;"><a href='javascript:getbuild(0 , <%# Eval("SectionID")%> , -1 , -1 ,1 ,-1, 0);'>
                                                                <%# Eval("Sectionname")%></a></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <input id="curSection" value="-1" type="hidden" />
                                            <asp:DropDownList ID="DDLArea" runat="server" Width="70" class=" required-entry required-entry input-text"
                                                Style="visibility: hidden" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">选择城市</asp:ListItem>
                                            </asp:DropDownList>
                                            <input id="tbCode" type="hidden" />
                                            <input id="tbbuild" type="hidden" />
                                            <div class="ContentboxTab">
                                                <div class="solid_line">
                                                </div>
                                                <div class="serach_OfficeBuilding">
                                                    搜索：
                                                    <input name="textfield" type="text" class="j_text" id="bkey" value="" />
                                                    <input type="button" style="" name="button2" id="button2" value="搜索" onclick=" return getkeyFix(0);"
                                                        class="search_bul" />
                                                </div>
                                                <div class="letter">
                                                    <a id="Aa" href="javascript:GetByCFixUC('A');">A</a> <a id="B" href="javascript:GetByCFixUC('B');">
                                                        B</a> <a id="C" href="javascript:GetByCFixUC('C');">C</a> <a id="D" href="javascript:GetByCFixUC('D');">
                                                            D</a> <a id="E" href="javascript:GetByCFixUC('E');">E</a> <a id="F" href="javascript:GetByCFixUC('F');">
                                                                F</a> <a id="G" href="javascript:GetByCFixUC('G');">G</a> <a id="H" href="javascript:GetByCFixUC('H');">
                                                                    H</a> <a id="I" href="javascript:GetByCFixUC('I');">I</a> <a id="J" href="javascript:GetByCFixUC('J');">
                                                                        J</a> <a id="K" href="javascript:GetByCFixUC('K');">K</a> <a id="L" href="javascript:GetByCFixUC('L');">
                                                                            L</a> <a id="M" href="javascript:GetByCFixUC('M');">M</a>
                                                    <a id="N" href="javascript:GetByCFixUC('N');">N</a> <a id="O" href="javascript:GetByCFixUC('O');">
                                                        O</a> <a id="P" href="javascript:GetByCFixUC('P');">P</a> <a id="Q" href="javascript:GetByCFixUC('Q');">
                                                            Q</a> <a id="R" href="javascript:GetByCFixUC('R');">R</a> <a id="S" href="javascript:GetByCFixUC('S');">
                                                                S</a> <a id="T" href="javascript:GetByCFixUC('T');">T</a> <a id="U" href="javascript:GetByCFixUC('U');">
                                                                    U</a> <a id="V" href="javascript:GetByCFixUC('V');">V</a> <a id="W" href="javascript:GetByCFixUC('W');">
                                                                        W</a> <a id="X" href="javascript:GetByCFixUC('X');">X</a> <a id="Y" href="javascript:GetByCFixUC('Y');">
                                                                            Y</a> <a id="Z" href="javascript:GetByCFixUC('Z');">Z</a>
                                                </div>
                                                <div class="choice_csroll" style="padding-bottom: 30px;">
                                                    <div style="margin-bottom: 5px;">
                                                        <a href="javascript:BuildCheckAll()">全选</a> <a href="javascript:BuildRiCheck()">反选</a></div>
                                                    <div id="div_section">
                                                        <div style="text-align: center; padding-top: 30px;">
                                                            <div>
                                                                选择区域，查看地标</div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <table class="form-list" cellspacing="0" style="display: none;">
                                            <tbody>
                                                <tr>
                                                    <td class="label">
                                                        <label for="_accountprefix">
                                                            配送路段<span class="required">*</span></label>
                                                    </td>
                                                    <td class="value">
                                                        <div id="tbBuilding" runat="server">
                                                        </div>
                                                        <asp:TextBox runat="server" ID="hfids" Width="190px" class=" required-entry required-entry input-text"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <small>&nbsp;</small>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </li>
                                    <li class="padding90px" style="clear: both; margin-top: 10px; margin-bottom: 20px;
                                        float: right;">
                                        <asp:Button Text="确定" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn" />
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>

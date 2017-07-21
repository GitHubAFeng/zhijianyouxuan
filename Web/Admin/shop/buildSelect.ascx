<%@ Control Language="C#" AutoEventWireup="true" CodeFile="buildSelect.ascx.cs" Inherits="admin_buildSelect" %>
<div id="tabpopup1" style="width: 702px; margin-left: 0px; border: 3px solid rgb(88, 88, 88);
    background-color: rgb(255, 255, 255); position: absolute; left: 450px; display: none; 
    top: 280px; z-index: 2000">
    <div style="height: 30px; line-height: 30px; background: #E7F1CC;">
        <span style="float: right; padding-right: 10px;"><a href="#" onclick="$('#windownbg').hide();closediv(); return false;">
            关闭</a></span><span style="padding-left: 20px">选择您所在的建筑物</span></div>
        <div class="MenuboxTab" id="section">
        <ul>
             <li id="one0" class="sbuild "><a href='javascript:getbuild(1 , 0 , -1 , -1 ,1 ,-1 , 0);'>
                        全部</a></li>
            <asp:Repeater ID="rptSectinList" runat="server">
                <ItemTemplate>
                    <li id="one<%# Eval("SectionID")%>" class="sbuild "><a href='javascript:getbuild(0 , <%# Eval("SectionID")%> , -1 , -1 ,1 ,-1, 0);'>
                        <%# Eval("Sectionname")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>

    <div class="ContentboxTab">
        <div class="solid_line">
        </div>
        <div class="serach_OfficeBuilding">
            搜索写字楼/小区：
            <input name="textfield" type="text" class="inputBorder" id="bkey" value="" />
            <input type="button" style="" name="button2" id="button2" value="搜索" onclick=" return getkeyFix(0);"
                class="search_bul" />
        </div>
        <div class="letter">
            <a id="A" href="javascript:GetByCFixUC('A');">A</a> <a id="B" href="javascript:GetByCFixUC('B');">
                B</a> <a id="C" href="javascript:GetByCFixUC('C');">C</a> <a id="D" href="javascript:GetByCFixUC('D');">
                    D</a> <a id="E" href="javascript:GetByCFixUC('E');">E</a> <a id="F" href="javascript:GetByCFixUC('F');">
                        F</a> <a id="G" href="javascript:GetByCFixUC('G');">G</a> <a id="H" href="javascript:GetByCFixUC('H');">
                            H</a> <a id="I" href="javascript:GetByCFixUC('I');">I</a> <a id="J" href="javascript:GetByCFixUC('J');">
                                J</a> <a id="K" href="javascript:GetByCFixUC('K');">K</a> <a id="L" href="javascript:GetByCFixUC('L');">
                                    L</a> <a id="M" href="javascript:GetByCFixUC('M');">M</a> <a id="N" href="javascript:GetByCFixUC('N');">
                                        N</a> <a id="O" href="javascript:GetByCFixUC('O');">O</a>
            <a id="P" href="javascript:GetByCFixUC('P');">P</a> <a id="Q" href="javascript:GetByCFixUC('Q');">
                Q</a> <a id="R" href="javascript:GetByCFixUC('R');">R</a> <a id="S" href="javascript:GetByCFixUC('S');">
                    S</a> <a id="T" href="javascript:GetByCFixUC('T');">T</a> <a id="U" href="javascript:GetByCFixUC('U');">
                        U</a> <a id="V" href="javascript:GetByCFixUC('V');">V</a> <a id="W" href="javascript:GetByCFixUC('W');">
                            W</a> <a id="X" href="javascript:GetByCFixUC('X');">X</a> <a id="Y" href="javascript:GetByCFixUC('Y');">
                                Y</a> <a id="Z" href="javascript:GetByCFixUC('Z');">Z</a>
        </div>
        <div class="choice_csroll">
            <div id="div_section">
            </div>
        </div>
    </div>
</div>

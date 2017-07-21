<%@ Control Language="C#" AutoEventWireup="true" CodeFile="testbuildSelect.ascx.cs" Inherits="AreaAdmin_testbuildSelect" %>
<div id="tabpopup1_tab" style="width: 702px; margin-left: 0px; 
    background-color: rgb(255, 255, 255);  display:block; background: #fff;">
    <div style="height: 30px; line-height: 30px; background: #E7F1CC;">
        <span style="float: right; padding-right: 10px;"><a href="javascript:closediv();" id="b_table_layer">关闭</a></span><span
            style="padding-left: 20px">选择区域，再选择用户所在建筑物</span></div>
    <div class="MenuboxTab" id="section">
        <input id="curSection" value="-1" type="hidden" />
        <input id="tbCode" type="hidden" />
        <input id="tbbuild" type="hidden" />
        <ul id="ul_tab_city">
            <asp:Repeater ID="rptcity" runat="server">
                <ItemTemplate>
                    <li id="cone<%# Eval("cid")%>" class="cityitem "><a href='javascript:getsectionbycity(<%# Eval("cid")%>);'>
                        <%# Eval("cname")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="clear">
        </div>
        <div class="spline">
        </div>
        <ul id="ul_tab_section">
        </ul>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
    
    <script type="text/javascript">

        function enterIn(evt) {
            if (evt.keyCode == 13 || evt.which == 13) {
                if (navigator.userAgent.indexOf("MSIE") > 0) {
                    getkeyFix(1); 
                    event.returnValue = false;
                }
                if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
                    getkeyFix(1);
                    return false;
                }
                return false;
            }
            else {

            }

        }
    </script>

    <div class="ContentboxTab">
        <div class="solid_line">
        </div>
        <div class="serach_OfficeBuilding">
            搜索区域/小区：
            <input name="textfield" type="text" class="inputBorder" id="bkey" value="" onkeydown="return enterIn(event); " />
            <input type="button" style="" name="button2" id="button2" value="搜索" onclick=" return getkeyFix(0);"
                class="search_bul" />
           
        </div>
        <div class="letter">
            <a id="A" href="javascript:GetByCFix('A');">A</a> <a id="B" href="javascript:GetByCFix('B');">
                B</a> <a id="C" href="javascript:GetByCFix('C');">C</a> <a id="D" href="javascript:GetByCFix('D');">
                    D</a> <a id="E" href="javascript:GetByCFix('E');">E</a> <a id="F" href="javascript:GetByCFix('F');">
                        F</a> <a id="G" href="javascript:GetByCFix('G');">G</a> <a id="H" href="javascript:GetByCFix('H');">
                            H</a> <a id="I" href="javascript:GetByCFix('I');">I</a> <a id="J" href="javascript:GetByCFix('J');">
                                J</a> <a id="K" href="javascript:GetByCFix('K');">K</a> <a id="L" href="javascript:GetByCFix('L');">
                                    L</a> <a id="M" href="javascript:GetByCFix('M');">M</a>
            <a id="N" href="javascript:GetByCFix('N');">N</a> <a id="O" href="javascript:GetByCFix('O');">
                O</a> <a id="P" href="javascript:GetByCFix('P');">P</a> <a id="Q" href="javascript:GetByCFix('Q');">
                    Q</a> <a id="R" href="javascript:GetByCFix('R');">R</a> <a id="S" href="javascript:GetByCFix('S');">
                        S</a> <a id="T" href="javascript:GetByCFix('T');">T</a> <a id="U" href="javascript:GetByCFix('U');">
                            U</a> <a id="V" href="javascript:GetByCFix('V');">V</a> <a id="W" href="javascript:GetByCFix('W');">
                                W</a> <a id="X" href="javascript:GetByCFix('X');">X</a> <a id="Y" href="javascript:GetByCFix('Y');">
                                    Y</a> <a id="Z" href="javascript:GetByCFix('Z');">Z</a>
        </div>
        <div class="choice_csroll">
            <div id="div_section" class="div_section">
                <div style="text-align: center; padding-top: 30px;">
                    选择您所在的城市->区域->标志建筑
                </div>
            </div>
        </div>
    </div>
</div>

﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="getBuild.ascx.cs" Inherits="control_getBuild" %>
<div id="tabpopup4" style="width: 702px; margin-left: 0px; border: 3px solid rgb(51, 51, 51);
    background-color: rgb(255, 255, 255); position: absolute; left: 10px; display: none;
    top: 0px; background: #fff; z-index: 111;">
    <div style="height: 30px; line-height: 30px; background: #E7F1CC;">
        <span style="float: right; padding-right: 10px;"><a href="javascript:closediv('tabpopup1');">
            关闭</a></span><span style="padding-left: 20px">选择您所在的区域</span></div>
    <div class="MenuboxTab" id="section" style="margin-bottom: 10px;">
        <input id="curSection" value="16" type="hidden" />
        <input id="tbCode" type="hidden" />
        <input id="tbbuild" type="hidden" />
        <div id="divfirst">
          <%--  <ul>
                <li class="sbuild hover" id="build_100000000000"><a href="javascript:getbuild(100000000000,2,'divsecond')">
                    北京</a></li>
                    
                    </ul>--%>
        </div>
        
          <ul>
             <li id="one0" class="sbuild hover"><a href='javascript:getbuild(1 , -1 , -1 , -1 ,1 ,-1 , 0);'>
                        全部</a></li>
            <asp:Repeater ID="rptSectinList" runat="server">
                <ItemTemplate>
                    <li id="one<%# Eval("SectionID")%>" class="sbuild "><a href='javascript:getbuild(<%# Eval("SectionID")%>);'>
                        <%# Eval("Sectionname")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div id="divsecond">
        </div>
    </div>
    <div class="ContentboxTab" style="margin-top: 5px;">
        <div class="solid_line">
        </div>
        <div class="serach_OfficeBuilding" style=" margin-bottom:10px;">
            搜素写字楼(可输入拼音首字母)：
            <input name="textfield" type="text" class="inputBorder" id="bkey" value="" onkeydown="return enterIn(event); " style=" height:20px;"  />
            <input type="button" style="" name="button2" id="button2" value="搜索" onclick=" return getkeyFix(1);"
                class="search_bul" />
        </div>
        <div class="choice_csroll">
            <div id="div_section">
            </div>
             
             <div id="div_build">
            </div>
        </div>
    </div>
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
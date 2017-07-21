<%@ Control Language="C#" AutoEventWireup="true" CodeFile="getBuild1.ascx.cs" Inherits="control_getBuild1" %>
<div id="tabpopup5" style="width: 702px; margin-left: 0px; border: 3px solid rgb(51, 51, 51);
    background-color: rgb(255, 255, 255); position: absolute; left: 10px; display: none;
    top: 0px; background: #fff; z-index: 111;">
    <div style="height: 30px; line-height: 30px; background: #E7F1CC;">
        <span style="float: right; padding-right: 10px;"><a href="javascript:closediv('tabpopup5');">
            关闭</a></span><span style="padding-left: 20px">选择建筑物</span></div>
    <div class="MenuboxTab" id="section" style="margin-bottom: 10px;">
        <input id="curSection" value="16" type="hidden" />
        <input id="tbCode" type="hidden" />
        <input id="tbbuild" type="hidden" />
        <div id="divfirst">
            <ul>
                <li class="sbuild hover" id="build_100000000000"><a href="javascript:getbuild(100000000000,2,'divsecond')">
                    北京</a></li></ul>
        </div>
        <div id="divsecond">
        </div>
    </div>
    <div class="ContentboxTab" style="margin-top: 5px;">
        <div class="solid_line">
        </div>
        <div class="choice_csroll">
            <div id="div_section">
            </div>
             <div id="div_build">
            </div>
        </div>
    </div>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="stylebanner.ascx.cs" Inherits="Html5.stylebanner" %>

<div class=" headercart mrg_b10 mrg_t10" id="selectfood" style="display: ;">
    <div class="lunch_box_frame">
        <div class="lunch_box_bg">
            <div class="lunch_box_01" style="line-height: 25px;">
                <span class="color_3a mrg_l15" style="float: right; padding-right: 5px;">


                    <input name="" style="display:none;" type="text" class="text_style" id="cartnum" value="1" onkeyup="this.value=this.value.replace(/\D/g,'1')"
                        onafterpaste="this.value=this.value.replace(/\D/g,'1')" maxlength="3" /><label id="lbunit"></label></span><strong
                            class="mrg_r50" style="font-size: 14px;" id="cartname">dddd</strong>
            </div>
            <input id="hfcname" type="hidden" />
            <input id="hfcprice" type="hidden" />
            <input id="hfsid" type="hidden" />
            <input id="hfpid" type="hidden" />
            <div class="lunch_box_03">
                <ul id="divstyle">
                </ul>
            </div>
            <div id="cartother">
            </div>
        </div>
        <div id="cartmaterial" style="margin-bottom: 5px;">
        </div>


        <div class="clear">
        </div>
        <div class="lunch_box_03" style="padding-bottom: 10px;">

            <div style="display: none;">
                <strong class=" float_l mrg_t5">备注：</strong><textarea name="" id="cartremark" cols=""
                    rows="" class="textarea_style"></textarea>



            </div>

            <div style="text-align:right;">

                <input name="" type="button" class="modity_button"
                    value="确认" onclick="cartok(0);" /><input name="" type="button" class="modity_button x"
                        value="取消" />

            </div>
        </div>
    </div>
</div>


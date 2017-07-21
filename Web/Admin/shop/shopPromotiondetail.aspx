<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopPromotiondetail.aspx.cs" Inherits="Admin_shop_addPromotion"
    ValidateRequest="false" %>

<%@ Register Src="~/Admin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/Admin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>促销详情</title>
    <link href="../../javascript/jbox/Skins/jbox.css" rel="stylesheet" />

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>


    <script src="../javascript/Common.js" type="text/javascript"></script>
    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>



    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });
        $(document).ready(function () {
            discountchange();
        });

    </script>

    <script language="javascript" type="text/javascript">


        //优惠类型变化 
        function discountchange() {

            var discounttype = $("input[name='tbptype']:checked").val();
            $(".payitem").hide();
            $(".tbptype" + discounttype).show();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">

        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />


        <div id="page:main-container">
            <div class="columns ">

               <div class="content" style="width: 500px; padding: 20px;">
                    <div class="hor-scroll">
                        <table class="form-list " cellspacing="0" >
                            <tbody>

                                <tr>
                                    <td class="left_td">
                                        <label for="_accountfirstname">
                                            开启<span class="required">*</span></label>
                                    </td>
                                    <td class="value">
                                        <asp:RadioButtonList runat="server" ID="tbisopen" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>

                                </tr>


                                <tr>
                                    <td class="left_td">
                                        <label for="_accountprefix">
                                            标题：<span class="required">*</span></label>
                                    </td>
                                    <td class="value">
                                        <epc:TextBox runat="server" ID="tbrevevar1" CanBeNull="必填" Width="290px" class="required-entry input-text"></epc:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="left_td">
                                        <label for="_accountprefix">
                                            有效期<span class="required">*</span></label>

                                    </td>
                                    <td class="value">
                                        <epc:TextBox runat="server" ID="tbstartdate" Width="90px"
                                            class="equired-entry input-text" CanBeNull="必填" Text="" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></epc:TextBox>至<epc:TextBox
                                                runat="server" ID="tbenddate" Width="90px" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})" class="required-entry input-text"
                                                CanBeNull="必填" Text=""></epc:TextBox><br />
                                    </td>

                                </tr>

                                <tr style="display: none;">
                                    <td class="left_td">
                                        <label for="_accountprefix">
                                            时间<span class="required">*</span></label>
                                    </td>
                                    <td class="value">
                                        <epc:TextBox runat="server" ID="tbstarttime" Width="90px"
                                            class="equired-entry input-text" Text="00:00" RequiredFieldType="营业时间" onfocus="WdatePicker({readOnly:true,dateFmt:'HH:mm'})"></epc:TextBox>至<epc:TextBox
                                                runat="server" ID="tbendtime" Width="90px" RequiredFieldType="营业时间" onfocus="WdatePicker({readOnly:true,dateFmt:'HH:mm'})" class="required-entry input-text"
                                                Text="00:00"></epc:TextBox>

                                        <div class="notice" style="margin-top: 5px;">全天设置成00:00 - 23:59即可</div>

                                    </td>

                                </tr>

                                <tr>
                                    <td class="left_td">
                                        <label for="_accountfirstname">
                                            类型<span class="required">*</span></label>
                                    </td>
                                    <td class="value">
                                        <asp:RadioButtonList runat="server" ID="tbptype" RepeatDirection="Vertical" onchange="discountchange()">
                                        </asp:RadioButtonList>
                                    </td>

                                </tr>

                                <tr class="payitem tbptype1">
                                    <td class="left_td"></td>
                                    <td class="value">首次下单立减
                                                                            <epc:TextBox runat="server" ID="tbminusmoney1" RequiredFieldType="正整数" Text="0"
                                                                                CanBeNull="必填" Width="50px" class=" required-entry required-entry input-text" reg="^\d+$" tip="金额格式错误,请输入整数"></epc:TextBox>元
                                    </td>

                                </tr>

                                <tr class="payitem tbptype20">
                                    <td class="left_td"></td>
                                    <td class="value">满
                                                                            <epc:TextBox runat="server" ID="tbovermoney20" RequiredFieldType="正整数" Text="0"
                                                                                CanBeNull="必填" Width="50px" class=" required-entry required-entry input-text" reg="^\d+$" tip="金额格式错误,请输入整数"></epc:TextBox>元免配送费
                                    </td>

                                </tr>

                                <tr class="payitem tbptype30">
                                    <td class="left_td"></td>
                                    <td class="value">满
                                                                            <epc:TextBox runat="server" ID="tbovermoney30" RequiredFieldType="正整数" Text="0"
                                                                                CanBeNull="必填" Width="50px" class=" required-entry required-entry input-text" reg="^\d+$" tip="金额格式错误,请输入整数"></epc:TextBox>元减少
                                                                            <epc:TextBox runat="server" ID="tbminusmoney30" RequiredFieldType="正整数" Text="0"
                                                                                CanBeNull="必填" Width="50px" class=" required-entry required-entry input-text" reg="^\d+$" tip="金额格式错误,请输入整数"></epc:TextBox>元
                                    </td>

                                </tr>

                                <tr class="payitem tbptype40">
                                    <td class="left_td"></td>
                                    <td class="value">提前
                                                                            <epc:TextBox runat="server" ID="tbovermoney40" RequiredFieldType="正整数" Text="0"
                                                                                CanBeNull="必填" Width="50px" class=" required-entry required-entry input-text" reg="^\d+$" tip="金额格式错误,请输入整数"></epc:TextBox>分钟减少
                                                                            <epc:TextBox runat="server" ID="tbminusmoney40" RequiredFieldType="正整数" Text="0"
                                                                                CanBeNull="必填" Width="50px" class=" required-entry required-entry input-text" reg="^\d+$" tip="金额格式错误,请输入整数"></epc:TextBox>元
                                    </td>

                                </tr>

                                <tr>
                                    <td class="left_td"></td>
                                    <td class="value">
                                        <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                            Text="保存"></asp:Button>

                                    </td>

                                </tr>





                            </tbody>
                        </table>




                    </div>
                </div>
            </div>
        </div>



    </form>
</body>
</html>



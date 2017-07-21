<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addFoodAttributes.aspx.cs"
    Inherits="Admin_Shop_addFoodAttributes" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品属性信息管理</title>
    <link href="/Admin/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/menu.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/Common.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/building.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="/Admin/css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="/Admin/css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="/Admin/css/ie7.css" media="all" />
    <![endif]-->

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>
    <script src="../javascript/jsrender.js"></script>
    <script src="/javascript/foodmanage.js"></script>



    <style type="text/css">
        .value {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hffid" Value="0" />
        <asp:HiddenField runat="server" ID="hftid" Value="0" />
        <asp:HiddenField runat="server" ID="hfdid" Value="0" />
        <asp:HiddenField runat="server" ID="hidStyle" />
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div id="content" style="width: 600px; padding-left: 10px;">
            <div class="hor-scroll">
                <table class="form-list attrbox" cellspacing="0">
                    <tbody>
                        <tr>
                            <td class="label">
                                <label for="_accountprefix">
                                    商品名称<span class="required">*</span></label>
                                <asp:HiddenField runat="server" ID="hidDataId" />
                            </td>
                            <td class="value">
                                <asp:Label ID="Lbfoodname" runat="server"></asp:Label>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>

                        <tr>
                            <td class="label">
                                <label for="_accountfirstname">
                                    选项类型 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <asp:DropDownList ID="DropSelectType" runat="server" Width="80px">
                                    <asp:ListItem Selected="True" Value="0">单选</asp:ListItem>
                                    <asp:ListItem Value="1">多选</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label for="_accountfirstname">
                                    是否必选 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <asp:DropDownList ID="ddlInve1" runat="server" Width="80px">
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <label for="_accountfirstname">
                                    属性标题 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <span>
                                    <epc:TextBox runat="server" name="invalue" ID="tbtitle" CanBeNull="必填" Width="160px" reg="\S" tip="属性标题不能为空"
                                        class=" required-entry required-entry input-text"></epc:TextBox>
                                </span>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>


                        <tr>
                            <td class="label">
                                <label for="_accountfirstname">
                                    属性选项及价格<span class="required">*</span></label>
                            </td>
                            <td class="value" colspan="2">

                                <table class="form-list foodtable" cellspacing="0" id="foodtable" style="width: 350px;">
                                    <tr>
                                        <th style="width: 60%">选项
                                        </th>
                                        <th>价格
                                        </th>
                                    </tr>

                                    <asp:Repeater runat="server" ID="rptfood">
                                        <ItemTemplate>

                                            <tr id="food_row_<%#(Container.ItemIndex+1) %>" class="hasitem">
                                                <td>
                                                    <input type="text" id="tbfoodname<%#(Container.ItemIndex+1) %>" reg="\S" value="<%# Eval("classname") %>" tip="属性选项不能为空" style="width: 100px;" class=" j_text fooditem"  />
                                                </td>
                                                <td>
                                                    <input type="text" reg="^[+]?\d+(\.\d+)?$" tip="价格格式错误,请输入数字" style="width: 60px;"  value="<%# Eval("pic") %>" class=" j_text" id="tbfoodcount<%#(Container.ItemIndex+1) %>" />&nbsp;&nbsp;<input type="button" value="删除" onclick="foodTable.delRow(<%#(Container.ItemIndex+1) %>)" />
                                                </td>
                                            </tr>



                                        </ItemTemplate>
                                    </asp:Repeater>


                                </table>


                                <div style="margin-top: 10px;">
                                    <input type="button" class="commonbutton" value="添加选项" onclick="foodTable.addrow()" />

                                </div>


                            </td>

                        </tr>


                        <tr style="display: none">
                            <td class="label">
                                <label for="_accountfirstname">
                                    商品属性 <span class="required">*</span></label>
                            </td>
                            <td class="value">
                                <asp:TextBox runat="server" ID="tbAttributes" TextMode="MultiLine" Width="300px"
                                    Height="100px"></asp:TextBox>
                                <br />
                                <br />
                                <div class="mynotice">
                                    说明：
                                </div>
                                请以此格式输入"<font color="red">去冰?2#少冰?3</font>"。每个子项以"#"分隔,名称与价格以"?"分隔
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <small>&nbsp;</small>
                            </td>
                            <td class="value">
                                <asp:Button ID="btSave" runat="server" CssClass="commonbutton" OnClick="btSave_Click" OnClientClick="return checkdata()"
                                    Text="保存"></asp:Button>
                            </td>
                            <td>
                                <small>&nbsp;</small>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <script id="myfooditem" type="text/x-jsrender">
            <tr id="food_row_{{:index}}">
                <td>
                    <input type="text" reg="\S" tip="属性选项不能为空" style="width: 100px;" class=" j_text fooditem" id="tbfoodname{{:index}}" />
                </td>
                <td>
                    <input type="text" reg="^[+]?\d+(\.\d+)?$" tip="价格格式错误,请输入数字" style="width: 60px;" value="0" class=" j_text" id="tbfoodcount{{:index}}" />&nbsp;&nbsp;<input type="button" value="删除" onclick="foodTable.delRow({{:index}})" />
                </td>
            </tr>
        </script>



    </form>
</body>
</html>


<script type="text/javascript">
      

    var id =  $("#hfdid").val();
    //编辑时，删除空行
    if (id != "0") {
        RowNum = $(".hasitem").length;
    }
    else {
        foodTable.addrow();
        foodTable.addrow();
    }



</script>

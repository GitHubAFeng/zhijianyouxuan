<%@ Page Language="C#" AutoEventWireup="true" CodeFile="buildorder.aspx.cs" Inherits="TogoHome_FoodDetailbuildorder" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>门店下单 - <%= WebUtility.GetWebName() %></title>
    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>


    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>

    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script src="../javascript/mymap.js" type="text/javascript"></script>
    <script src="../javascript/spin.min.js" type="text/javascript"></script>



    <style type="text/css">
        #cblweekday {
            float: left;
        }

        .mynotice {
            background-color: #F1FAFE;
            border: 1px solid #B1DFF3;
            padding: 3px;
            color: #0863C0;
            _padding-top: 6px;
            margin-left: 3px;
            display: inline;
        }

        .hidden {
            display: none;
        }
    </style>

    <script type="text/javascript">


        function showmap() {
            var tbaddress = $("#tbaddress").val() + "";
            if (tbaddress == "") {
                alert("请输入您的地址");
                return;
            }
            setPlace();
        }

        $(document).ready(function () {
            initialize();
        })

        function setLatLng(point) {
            document.getElementById("hidLat").value = point.lat;
            document.getElementById("hidLng").value = point.lng;
            return true;
        }


        function setPlace() {
            var cityname = $("#hfcityname").val();
            var address = document.getElementById("tbaddress").value;
            var local = new BMap.LocalSearch(cityname, {
                renderOptions: {
                    map: map,
                    autoViewport: true,
                    selectFirstResult: false
                }
            });
            local.search(address);
        }

        //点击获取用户地址
        function getmyaddress() {
            var tel = $("#tbtel").val();
            if (tel == "") {
                alert("请输入电话");
                return;
            }
            window.location = "buildorder.aspx?tel=" + tel + "&start=1";
            return false;
        }

    </script>



</head>

<body>
    <form id="form1" runat="server">

        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hidlocalflag" />

        <asp:HiddenField runat="server" ID="add_dataid" Value="0" />

        <asp:HiddenField runat="server" ID="hfcityname" Value="全国" />

        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <div class="shop_main">
                        <div class="main-content">


                            <h1 class="topbg">门店下单</h1>



                            <div class="usermima">
                                <ul>
                                    <li><span class="left_span">姓名<font color="red">*</font>：</span>
                                        <epc:TextBox runat="server" ID="tbuname" CanBeNull="必填" Width="160px" class="required-entry input_on"></epc:TextBox><span></span>
                                    </li>
                                    <li><span class="left_span">电话<font color="red">*</font>：</span>
                                        <epc:TextBox runat="server" ID="tbtel" CanBeNull="必填" Width="160px" class="required-entry input_on"></epc:TextBox>
                                        <span id="lbtelmsg" runat="server" style="color: red;"></span>
                                        <input type="button" class="subBtn" onclick="getmyaddress()" id="btgetaddress" value="获取地址" />
                                    </li>


                                    <li>
                                        <div class=" noseeitem" style="padding-left: 65px;">
                                            <ul class="myadd_ul">
                                                <asp:Repeater runat="server" ID="rptaddress">
                                                    <ItemTemplate>
                                                        <li>
                                                            <input type="radio" id="addrlist_<%# Eval("dataid") %>" name="addressradio1" <%#(Container.ItemIndex+1).ToString() == "1" ? "checked" : "" %>
                                                                class="radio1 <%#(Container.ItemIndex+1).ToString() == "1" ? "first_addr" : "" %>"
                                                                onclick="setaddress(this);" value="<%# Eval("Receiver") %>^<%# Eval("Address")%>^<%# Eval("lat") %>^<%# Eval("lng")%>^<%# Eval("dataid") %>^<%# Eval("BuildingID") %>" />
                                                            <label for="addrlist_<%# Eval("dataid") %>"><%# Eval("Receiver")%>&nbsp;<%# Eval("Address") %></label>

                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>

                                    </li>



                                    <li><span class="left_span">地址<font color="red"></font>：</span>
                                        <epc:TextBox runat="server" ID="tbaddress" placeholder="请输入地图定位的地址" Width="300px" class="required-entry input_on"></epc:TextBox><span><input
                                            type="button" value="地图标注" onclick="showmap()" class="subBtn" />
                                        </span></li>
                                    <li id="trmap">

                                        <div style="border: 1px solid #EEEEEE; display: inline; float: left; height: 353px; margin: 18px 0 0 13px; width: 550px;"
                                            id="map">
                                            <div style="width: 650px; height: 353px; position: relative; background-color: rgb(229, 227, 223);"
                                                id="map_canvas">
                                            </div>
                                        </div>
                                        <asp:HiddenField runat="server" ID="hdState" />
                                        <div class="clear">
                                        </div>
                                    </li>

                                    <li><span class="left_span">详细地址<font color="red">*</font>：</span>
                                        <epc:TextBox runat="server" ID="tbaddressdetail" placeholder="请输入详细地址" CanBeNull="必填" Width="300px" class="required-entry input_on"></epc:TextBox>
                                    </li>


                                    <li><span class="left_span">送餐份数<font color="red">*</font>：</span>
                                        <epc:TextBox runat="server" ID="tbfoodcount" CanBeNull="必填" Text="1"
                                            Width="90px" class="required-entry input_on"></epc:TextBox>
                                    </li>


                                    <li><span class="left_span">应收款<font color="red">*</font>：</span>
                                        <epc:TextBox runat="server" ID="tbPrice" CanBeNull="必填" Text="" RequiredFieldType="数据校验"
                                            Width="90px" class="required-entry input_on"></epc:TextBox>元 </li>

                                    <li><span class="left_span">时间<font color="red">*</font>：</span>
                                        <asp:DropDownList
                                            runat="server" ID="ddltime" Width="60px" CssClass="j_seclect">
                                        </asp:DropDownList>
                                    </li>
                                    <li><span class="left_span">备注<font color="red"></font>：</span>
                                        <epc:TextBox runat="server" ID="tbremark" TextMode="MultiLine" Width="300px" class="required-entry input_on"></epc:TextBox><span></span>
                                    </li>
                                    <li class="padding90px">
                                        <div style="text-align: center;">
                                            <asp:Button Text="确定" runat="server" ID="btSave" OnClick="btSave_Click" class="subBtn"
                                                OnClientClick="showload_super();" />
                                        </div>
                                    </li>
                                </ul>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

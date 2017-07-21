<%@ Page Language="C#" AutoEventWireup="true" CodeFile="locationmonitor.aspx.cs" Inherits="qy_54tss_AreaAdmin_GpsSet_locationmonitor"
    EnableEventValidation="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实时监控-<%= WebUtility.GetMyName() %></title>
    <link href="css/indis_style.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../../javascript/jbox/Skins/jbox.css" rel="stylesheet" />
    
    <script src="javascript/countUp.js"></script>
    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>
    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../javascript/Common.js?v=1515" type="text/javascript"></script>
    <script src="javascript/orderdeliver.js?v=1103" type="text/javascript"></script>
    <script src="../../javascript/jCommon.js" type="text/javascript"></script>
    <script src="javascript/locationmonitor.js"></script>




    <script type="text/javascript">
        var NearbyDelivery = [];

        var riderCount1 = $("#riderCount1");
        var timeoutCount1 = $("#timeoutCount1");
        var loadCount1 = $("#loadCount1");
        var totalCount1 = $("#totalCount1");


        $(document).ready(function () {

             riderCount1 = $("#riderCount1");
             timeoutCount1 = $("#timeoutCount1");
             loadCount1 = $("#loadCount1");
             totalCount1 = $("#totalCount1");


            var id = $("#hfflag").val();
            if (id == "1") {
                show_citytable();
            }
            delayURL();

            loadData();

        })

        function delayURL() {
            var timebox = $("#time");
            var delay = parseInt(timebox.html());
            if (delay > 0) {
                delay--;
                timebox.html(delay + "");
            }
            else {
                loadData();
                timebox.html("60");
            }
            setTimeout(delayURL, 1000);
        }


    </script>

    <style type="text/css">
        .system_shop {
            border: solid red 1px;
        }

        .col-md-3 {
            width: 25%;
        }

        .col-md-1, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-10, .col-md-11, .col-md-12 {
            float: left;
        }

        .panel-stat3 {
            background-color: rgb(255, 255, 255);
            border-radius: 2px;
            box-shadow: 0 1px 5px 0 rgba(0, 0, 0, 0.1);
            color: #666666;
            overflow: visible;
            padding: 15px 20px 10px;
            border: solid #ccc 1px;
            margin-right:20px;
            position:relative;
        }

            .panel-stat3 .corlabel {
                background-color: #fff;
                height: 32px;
                left: 0;
                position: absolute;
                top: 25px;
                width: 6px;
            }

            .panel-stat3 h5 {
                display: inline-block;
            }

            .panel-stat3 .info-wrap {
                display: inline-block;
                position: relative;
                top: 0;
                z-index: 999;
            }

            .panel-stat3 .fa-lg-wrap {
                min-height: 33px;
            }

        .padding-md h5 {
            font-size: 14px;
        }

        .padding-md h2 {
            font-size: 30px;
        }
       .padding-md h1, .h1,.padding-md h2, .h2, .padding-md h3, .h3 {
          margin-bottom:10px;
        
        }
      

       .padding-md h1,.padding-md h2,.padding-md h3,.padding-md h4,.padding-md h5,.padding-md h6, .h1, .h2, .h3, .h4, .h5, .h6 {
    color: inherit;
    font-family: inherit;
    font-weight: 500;
    line-height: 1.1;
}

      .padding-md h4, .h4,.padding-md h5, .h5,.padding-md h6, .h6 {
    margin-bottom: 10px;
    margin-top: 10px;
}

      .panel-stat3 .corlabel {
    background-color: #1cace1;
    height: 32px;
    left: 0;
    position: absolute;
    top: 25px;
    width: 6px;
}

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hfcityid" />
        <asp:HiddenField runat="server" ID="hfflag" Value="0" />
        <asp:HiddenField runat="server" ID="hf_curr_dids" />
        <asp:HiddenField runat="server" ID="hforderuserlatlng" />
        <asp:HiddenField runat="server" ID="hfexpressororder" Value="0" />

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>




        <div class="wrap">
            <div class="indis_top">
                <a href="../../index.aspx" class="float_l padding_l5">
                    <img src="../../images/logo.png" style="height: 75px;" /></a><span
                        class="top_title">查看商家配送员位置</span>


                <span class="fa margin_t3">操作员：<label runat="server"
                    id="lbadminname"></label></span>

                <span class="fa margin_t3" style="margin-left: 20px;">
                    <label id="time" style="color: Red;">
                        60</label>秒后自动刷新<label class="reflush_ico" style="margin-left: 5px; cursor: pointer; display: inline-block;" title="手动刷新" onclick="loadData()">&nbsp;</label>


                    <img src="/Admin/images/reflush.png" style="vertical-align: middle; cursor: pointer; display: none;" title="手动刷新" onclick="loadData()">
                </span>

                <%--<p>
                    <label runat="server" id="snDate">
                    </label>
                </p>--%>
                <p>
                    <a href="../basic.aspx">返回后台</a>
                </p>
            </div>


            <!-- /grey-container -->
            <div class="padding-md" style="padding: 20px 40px; margin-bottom:30px; " >
                <div class="row">
                    <div class="col-sm-6 col-md-3">
                        <div class="panel-stat3  bg-yellow">
                            <em class="corlabel"></em>

                            <h5>在岗骑手数</h5>
                            <div class="info-wrap">
                                <i class="fa fa-question-circle fa-lg"></i>

                            </div>
                            <h2 class="m-top-none" id="riderCount">0</h2>
                            <div class="fa-lg-wrap"><span class="m-left-xs"><span class="m-top-none" id="riderCount1">0</span>%骑手在岗</span></div>
                            <div class="refresh-button" data-target="rider">
                                <i class="fa fa-refresh"></i>
                            </div>
                            <div class="loading-overlay">
                                <i class="loading-icon fa fa-refresh fa-spin fa-lg"></i>
                            </div>
                        </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6 col-md-3">
                        <div class="panel-stat3  bg-blue">
                            <em class="corlabel"></em>

                            <h5>超时单占比</h5>
                            <div class="info-wrap">
                                <i class="fa fa-question-circle fa-lg"></i>

                            </div>
                            <h2 class="m-top-none"><span id="timeoutCount">0.0</span>%</h2>
                            <div class="fa-lg-wrap"><span class="m-left-xs">共<span class="m-top-none" id="timeoutCount1">0</span>单超时</span></div>
                            <div class="refresh-button" data-target="timeoutOrder">
                                <i class="fa fa-refresh"></i>
                            </div>
                            <div class="loading-overlay">
                                <i class="loading-icon fa fa-refresh fa-spin fa-lg"></i>
                            </div>
                        </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6 col-md-3">
                        <div class="panel-stat3  bg-blue">
                            <em class="corlabel"></em>

                            <h5>人均负载</h5>
                            <div class="info-wrap">
                                <i class="fa fa-question-circle fa-lg"></i>

                            </div>
                            <h2 class="m-top-none" id="loadCount">0</h2>
                            <div class="fa-lg-wrap" ><span class="m-left-xs" style="display:none">昨天同期人均负载：<span class="m-top-none" id="loadCount1">0</span></span></div>
                            <div class="refresh-button" data-target="perLoad">
                                <i class="fa fa-refresh"></i>
                            </div>
                            <div class="loading-overlay">
                                <i class="loading-icon fa fa-refresh fa-spin fa-lg"></i>
                            </div>
                        </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6 col-md-3">
                        <div class="panel-stat3  bg-yellow">
                            <em class="corlabel"></em>

                            <h5>今日总单量</h5>

                            <h2 class="m-top-none" id="totalCount">0</h2>
                            <div class="fa-lg-wrap"><span class="m-left-xs">昨天同期订单数：<span class="m-top-none" id="totalCount1">0</span></span></div>
                            <div class="refresh-button" data-target="totalOrder">
                                <i class="fa fa-refresh"></i>
                            </div>
                            <div class="loading-overlay">
                                <i class="loading-icon fa fa-refresh fa-spin fa-lg"></i>
                            </div>
                        </div>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>

            <div class="clear">
            </div>

            <div class="indis_con" style="border-top: 2px solid #f8a65a; margin-top:20px;">

                <div style="display: none" id="divDeliver"></div>


                <div id="map_canvas" style="width: 100%; height: 538px">
                </div>


                <div class="clear">
                </div>

            </div>
        </div>


        <script type="text/javascript">
            function loading() {

            }

            function loadover() {

            }


            function LoadDeliver(type) {

                delDeliverMaker();
                var hfcityid = $("#hfcityid").val();
                markerlist.length = 0;
                var sname = $("#ddlsection option:selected").text();
                $.ajax({
                    type: "get",
                    url: "../ajax/GetDeliverList.aspx",
                    cache: false,
                    dataType: "html",
                    data: "type=" + type + "&sname=" + escape(sname) + "&cid=" + hfcityid,
                    beforeSend: function (XMLHttpRequest) {
                        loading();
                    },
                    success: function (data, textStatus) {
                        //处理数据
                        $("#divDeliver").html(data);

                        loadover();

                        //显示所有配送员
                        var deliverlist = eval("(" + $("#deliverlistjson").val() + ")");
                        for (var i = 0; i < deliverlist.length; i++) {
                            var d_point = new BMap.Point(deliverlist[i].Lng, deliverlist[i].Lat); // 创建点坐标
                            var d_icon = new BMap.Icon('/Admin/Service/images/mapmarker/marker_' + deliverlist[i].workstate + '.png', new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                            var d_marker = new BMap.Marker(d_point, { icon: d_icon });
                            var deliverlabel = new BMap.Label(deliverlist[i].Name + "(" + deliverlist[i].OrderNum + ")", { offset: new BMap.Size(-28, -22) });
                            deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                            d_marker.setLabel(deliverlabel);
                            d_marker.setTitle(deliverlist[i].DataId);
                            map.addOverlay(d_marker);

                            d_marker.addEventListener("click", function (e) {

                                showDeliverBox(this.getTitle(), this);
                            });

                            markerlist.push(d_marker);
                        }

                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        //$("#loading").html("加载完成");
                    },
                    error: function () {
                        //请求出错处理
                    }

                });

                return false;

            }


        </script>

        <script type="text/javascript">
            var gzoom = 17;
            var marker = null;
            var markerlist = new Array(); //配送员
            var shopmarkerlist = new Array(); //商家
            var map = new BMap.Map("map_canvas"); // 创建地图实例
            map.enableScrollWheelZoom();
            var myGeo = new BMap.Geocoder();
            var _lat = parseFloat($("#hidLat").val());
            var _lng = parseFloat($("#hidLng").val());
            var initpoint = new BMap.Point(_lng, _lat); // 创建点坐标
            //图标
            var myIcon = new BMap.Icon("/Admin/images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });

            var cityname = $("#hfcityname").val();
            map.centerAndZoom(cityname, gzoom);

            map.addControl(new BMap.NavigationControl());  //缩放工具

            function setLatLng(point) {
                document.getElementById("hidLat").value = point.lat;
                document.getElementById("hidLng").value = point.lng;
                return true;
            }

            function showorder(shopid, orderid) {
                var sheight = window.screen.height - 70;
                var swidth = window.screen.width - 10;

                var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
                var url = "updateorder.aspx?id=" + shopid + "&oid=" + orderid;
                window.open(url, "newwindow", winoption);
            }


            function j_ShowWindow(msg) {
                var innerHTML = "<div style='background-color: #458BC9;height: 19px;padding: 3px 4px 0 10px;'>";
                innerHTML += "<div style='float:left;font-size:12px;color:#fff'>温馨提醒</div>";
                innerHTML += "<div style='float:right;'>";
                innerHTML += "<a href='javascript:HiddenWindow();'><img src='http://www.faneat.com/Images/window_close.gif' alt='关闭窗口' /> </a>";
                innerHTML += "</div></div>";
                innerHTML += "<div style='text-align:left;font-size:12px;width:95%;overflow:hidden;padding:5px; padding-left:10px;' id='divMassage'>";
                innerHTML += msg;
                innerHTML += "</div></div></div>";

                if (!document.getElementById("divMsg")) {
                    var div = document.createElement('div');
                    div.id = 'divMsg';
                    div.setAttribute('style', 'bottom:0px; right:20px; width:350px; height:200px; position: absolute;z-index: 100; background-color:#FFFFFF;border: 2px solid #447AA9;display:none;');
                    div.setAttribute('innerHTML', innerHTML);
                    div.innerHTML = innerHTML;
                    document.body.appendChild(div);
                    with (document.getElementById("divMsg").style) {
                        bottom = "0px";
                        right = "0px";
                        width = "350px";
                        height = "200px";
                        position = "absolute";
                        background = "#FFFFFF";
                        border = "2px solid #447AA9";
                    }
                }
                else {
                    document.getElementById("divMsg").style.display = "block";
                    document.getElementById("divMsg").setAttribute('innerHTML', innerHTML);
                    document.getElementById("divMsg").innerHTML = innerHTML;
                }
                //setTimeout(HiddenWindow, 3000);
            }
            function HiddenWindow() {
                if (document.getElementById("divMsg")) {
                    document.getElementById("divMsg").style.display = "none";
                }
            }

            function select_mycity(name, id) {
                var url = "locationmonitor.aspx?cid=" + id + "&cname=" + escape(name);
                window.location = url;

            }
        </script>

    </form>
</body>
</html>

<script src="../../javascript/jquery.floatDiv.js" type="text/javascript"></script>


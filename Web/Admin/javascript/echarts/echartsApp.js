/// <reference path="../../../javascript/JSintellisense/jquery-1.3.2-vsdoc2.js" />
/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at $time$.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/

///商家统计
function ShopTop10() {
    var xjson = $("#hfxjson").val();
    var yjson = eval("(" + $("#hfyjson").val() + ")");

    // 基于准备好的dom，初始化echarts图表
    var myChart = echarts.init(document.getElementById('cancaschars'));

    var option = {
        title: {
            text: '商家销量TOP10'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['订单量', '总金额', '利润']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                splitArea: { show: true }
            }
        ],
        series: [
            {
                name: '订单量',
                type: 'bar',
                data: []
            }
            ,
                {
                    name: '总金额',
                    type: 'bar',
                    data: []
                }
                 ,
                {
                    name: '利润',
                    type: 'bar',
                    data: []
                }
        ]
    };

    var xdata = xjson.split(",");
    for (var i in xdata) {
        option.xAxis[0].data.push(xdata[i]);
    }

    for (var n in yjson) {
        var x = yjson[n];
        option.series[0].data.push(parseFloat(x.ordercount));
        option.series[1].data.push(parseFloat(x.allprice));
        option.series[2].data.push(parseFloat(x.Shopprofit));
    }


    // 为echarts对象加载数据 
    myChart.setOption(option);
}

///商品统计
function foodTop10() {
    var xjson = $("#hfxjson").val();
    var yjson = eval("(" + $("#hfyjson").val() + ")");

    // 基于准备好的dom，初始化echarts图表
    var myChart = echarts.init(document.getElementById('cancaschars'));

    var option = {
        title: {
            text: '商品销量TOP10'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['销量']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                splitArea: { show: true }
            }
        ],
        series: [
            {
                name: '销量',
                type: 'line',
                data: [],
                tooltip: {             // Series config.
                    trigger: 'axis',
                    backgroundColor: 'black',
                    formatter: function (params) {
                        var res = '' + params[0].name;
                        res += '<br/>销量 : ' + params[0].value;
                        res += '<br/>价格 : ' + params[0].data.allprice;
                        res += '<br/>商家 : ' + params[0].data.shopname;

                        return res;
                    }
                }
            }
        ]
    };


    var xdata = xjson.split(",");
    for (var i in xdata) {
        option.xAxis[0].data.push(xdata[i]);
    }

    for (var n in yjson) {
        var x = yjson[n];
        option.series[0].data.push({ 'shopname': '' + x.shopname + '', 'allprice': '' + x.allprice + '', 'value': '' + x.ordercount + '' });
    }

    // 为echarts对象加载数据 
    myChart.setOption(option);
}

///用户统计
function userTop10() {
    var xjson = $("#hfxjson").val();
    var yjson = eval("(" + $("#hfyjson").val() + ")");

    // 基于准备好的dom，初始化echarts图表
    var myChart = echarts.init(document.getElementById('cancaschars'));

    var option = {
        title: {
            text: '会员订餐TOP10'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['订单量', '总金额']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                splitArea: { show: true }
            }
        ],
        series: [
            {
                name: '订单量',
                type: 'bar',
                data: []
            }
            ,
                {
                    name: '总金额',
                    type: 'bar',
                    data: []
                }
        ]
    };

    var xdata = xjson.split(",");
    for (var i in xdata) {
        option.xAxis[0].data.push(xdata[i]);
    }

    for (var n in yjson) {
        var x = yjson[n];
        option.series[0].data.push(parseFloat(x.ordercount));
        option.series[1].data.push(parseFloat(x.allprice));
    }


    // 为echarts对象加载数据 
    myChart.setOption(option);
}


///订单每天时间分布
function ordertimeshow() {
    var xjson = $("#hfxjson").val();
    var yjson = eval("(" + $("#hfyjson").val() + ")");

    // 基于准备好的dom，初始化echarts图表
    var myChart = echarts.init(document.getElementById('cancaschars'));

    var option = {
        title: {
            text: '订单时段分布'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['订单量', '总金额']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                data: ['0', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '23']
            }
        ],
        yAxis: [
            {
                type: 'value',
                splitArea: { show: true }
            }
        ],
        series: [
            {
                name: '订单量',
                type: 'bar',
                data: []
            }
            ,
                {
                    name: '总金额',
                    type: 'bar',
                    data: []
                }
        ]
    };

    var xdata = xjson.split(",");
    for (var i in xdata) {
        option.xAxis[0].data.push(xdata[i]);
    }

    for (var n in yjson) {
        var x = yjson[n];
        option.series[0].data.push(parseFloat(x.ordercount));
        option.series[1].data.push(parseFloat(x.allprice));
    }

    // 为echarts对象加载数据 
    myChart.setOption(option);
}

///订单来源
function ordersource() {
    var xjson = $("#hfxjson").val();
    var yjson = eval("(" + $("#hfyjson").val() + ")");

    // 基于准备好的dom，初始化echarts图表
    var myChart = echarts.init(document.getElementById('cancaschars'));
    option = {
        title: {
            text: '订单来源分布',
            subtext: '统计各客户端订单比例',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            x: 'left',
            data: []
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: {
                    show: true,
                    type: ['pie', 'funnel'],
                    option: {
                        funnel: {
                            x: '25%',
                            width: '50%',
                            funnelAlign: 'left',
                            max: 1548
                        }
                    }
                },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        series: [
            {
                name: '订单来源分布',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: []
            }
        ]
    };


    var xdata = xjson.split(",");
    for (var i in xdata) {
        option.legend.data.push(xdata[i]);
    }

    for (var n in yjson) {
        var x = yjson[n];
        option.series[0].data.push({ name: '' + x.name + '', allprice:x.allprice, value:x.ordercount });
    }

    // 为echarts对象加载数据 
    myChart.setOption(option);


}



///配送员产值表
function DeliverOutputValue(type) {
    var xjson = $("#hfxjson").val();
    if (xjson.length == 0) {
        return;
    }
    var yjson = eval("(" + $("#hfyjson").val() + ")");

    var unitstr = "";
    if (type == 1) {
        unitstr = "单";
    }
    else {
        unitstr = "元";
    }

    $(".active").removeClass("active");
    $("#menu_" + type).addClass("active");


    // 基于准备好的dom，初始化echarts图表
    var myChart = echarts.init(document.getElementById('cancaschars'));

    var star1 = $("#tbStartTime").val();
    var star2 = $("#tbStartTime2").val();

    var option = {
        title: {
            text: '配送员产值表'
        },
        tooltip: {
            trigger: 'axis',
            formatter: function (params) {
                var res = new Date().createByDate(star1).DateAdd('d', parseInt(params[0].name)).format("yyyy-MM-dd") + '&nbsp;&nbsp;' + params[0].value + unitstr;
                res += "<br />" + new Date().createByDate(star2).DateAdd('d', parseInt(params[1].name)).format("yyyy-MM-dd") + '&nbsp;&nbsp;' + params[1].value + unitstr;;

                return res;
            }
        },
        legend: {
            data: ['时间1', '时间2']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value',
                splitArea: { show: true }
            }
        ],
        series: [
            {
                name: '时间1',
                type: 'line',
                data: []
            }
            ,
            {
                name: '时间2',
                type: 'line',
                data: []
            }
        ]
    };

    var xdata = xjson.split(",");
    for (var i in xdata) {
        option.xAxis[0].data.push(xdata[i]);
    }

    for (var n in yjson) {
        var x = yjson[n];

        switch (type) {
            case 1:
                option.series[0].data.push(parseFloat(x.ordercount_1));
                option.series[1].data.push(parseFloat(x.ordercount_2));
                break;
            case 2:
                option.series[0].data.push(parseFloat(x.sendfee_1));
                option.series[1].data.push(parseFloat(x.sendfee_2));
                break;
            case 3:
                option.series[0].data.push(parseFloat(x.allprice_1));
                option.series[1].data.push(parseFloat(x.allprice_2));
                break;

        }

    }


    // 为echarts对象加载数据 
    myChart.setOption(option);
}
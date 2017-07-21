<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="wap.index" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>

    <link type="text/css" rel="stylesheet" href="css/style.css?v=2016071431" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=2016071431" />





    <script src="javascript/jquery.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=fMnzyhYs0D1cBEl5iGMQ0Dlg"></script>
    <style>
        .searchbox {
            width: 100%;
            height: 36px;
            background-color: #f39800;
            vertical-align: top;
            max-width: 640px;
            margin: 0 auto;
            padding-top: 2px;
        }

        .searchblock {
            width: 74%;
            position: relative;
            background-color: #fff;
            border-radius: 4px;
            text-align: center;
            float: left;
            margin-top: 2px;
        }

        .header_search {
            margin: 0;
            padding: 0;
            outline: none;
            border: none;
            height: 30px;
            color: #999;
            width: 100%;
            padding-left: 5px;
            border-radius: 4px;
        }

        .searchbox button {
            position: absolute;
            right: 4px;
            top: 0;
            width: 30px;
            height: 30px;
            border: none;
            font-size: 16px;
            background-image: url(/images/ico-b-search.png);
            background-repeat: no-repeat;
            background-size: 24px;
            background-position: 5px center;
            background-color: #fff;
        }

        .searchbox .cities {
            float: left;
            width: 20%;
            padding: 0;
            margin: 0;
            border: 0;
            height: 30px;
            margin-left: 5px;
            font-size: 14px;
            background-color: #fff;
            color: #333;
            margin-top: 2px;
            margin-right: 2%;
            outline: none;
            border-radius: 4px;
        }

            .searchbox .cities option {
                padding: 0;
                margin: 0;
                border: 0;
                line-height: 30px;
            }
    </style>
</head>

<body>
    <input type="hidden" runat="server" id="hfcityjson" />
    <input type="hidden" runat="server" id="lat" />
    <input type="hidden" runat="server" id="lng" />
    <input type="hidden" runat="server" id="hfstatus" />
    <input type="hidden" runat="server" id="hfordersubmit" value="1" />
    <input type="hidden" runat="server" id="hfislocate" value="0" />
    <input id="hfret" runat="server" type="hidden" value="0" />
    <div class="page">
        <div id="page_title">

            <h1>选择送达地址</h1>

        </div>
        <div class="searchbox">

            <select class="cities" id="tbcityname" onchange="getcity('tbcityname')">

                <asp:Repeater ID="rptCtiy" runat="server">
                    <ItemTemplate>
                        <option value="<%# Eval("cname") %>"><%# Eval("cname") %></option>

                    </ItemTemplate>
                </asp:Repeater>

            </select>

            <div class="searchblock">
                <input class="header_search" type="text" placeholder="请输入地址" value="" name="keyaddress" id="keyaddress" />
                <button type="button" id="btsearch" onclick="setPlace();"></button>
            </div>
        </div>
        <div class="container">



            <div>
                <div class="geo-search-results ng-scope ui_show ng-hide" id="addressbox">
                </div>
            </div>
            <ul class="addlist">
                <li class="p_title">当前地址</li>
                <li class="nowadd">
                    <label id="lbautomyaddress" style="color: red;" onclick="useCurrLocation()">正在获取...</label><span class="mess_ch" onclick="auto_location()"><i class="ico-open"></i>重新定位</span></li>
            </ul>
            <ul class="addlist">
                <li class="p_title" style="border-bottom: none;">附近地址</li>

            </ul>
            <div>
                <div class="geo-search-results ng-scope ui_show ng-hide" id="POIbox">
                </div>
            </div>
        </div>
    </div>



    <div class="gmap" id="map" style="display: none;">
        <div style="width: 100%; height: 300px; position: relative; background-color: rgb(229, 227, 223);" id="map_canvas">
        </div>
    </div>

    <script id="addressTemplate" type="text/x-jsrender">
        {{for #data}}
            <div class="geo-search-result ng-scope">
                <a class="gsr-detail" href="javascript:" onclick="gotoorder('{{:lat}}','{{:lng}}','{{:address}}')">
                    <p class="spot ng-binding">{{:title}}</p>
                    <p class="address ng-binding">{{:address}}</p>
                </a>
            </div>
        {{/for}}
        
    </script>
</body>
</html>

<script src="javascript/jCommon.js?c=1" type="text/javascript"></script>
<script src="javascript/eventwrapper.min.js" type="text/javascript"></script>
<script src="javascript/locationmap.js?v=2016071900"></script>
<script src="javascript/jsrender.js"></script>

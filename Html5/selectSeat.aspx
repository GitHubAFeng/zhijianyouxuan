<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectSeat.aspx.cs" Inherits="Html5.selectSeat" %>

<%@ Register Src="~/stylebanner.ascx" TagName="stylebanner" TagPrefix="uc4" %>


<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/pictip.css" rel="stylesheet" />
    <link href="css/cart.css" rel="stylesheet" />

    <script src="javascript/jquery.js" type="text/javascript"></script>
    <script src="javascript/fastclick.js"></script>
    <script type="text/javascript">

        $(function () {
            FastClick.attach(document.body);
        });


    </script>

    <style type="text/css">
        .sweet-alert h2 {
            display: none;
        }

        #page_title {
            position: fixed;
        }

        .shop_detail {
            height: 44px;
            line-height: 44px;
        }
    </style>
</head>

<body id="page_allMenu">
    <input type="hidden" id="hnTogoBusiness" runat="server" />
    <input type="hidden" id="hnTogoStatus" runat="server" />
    <input type="hidden" id="hidTogoName" runat="server" />
    <input type="hidden" runat="server" id="hftogotype" />
    <input type="hidden" runat="server" id="hfminimoney" /><%--起送价--%>
    <input type="hidden" runat="server" id="hffreemoney" /><%--满多少免配送费--%>
    <input type="hidden" runat="server" id="hfsendfree" /><%--配送费--%>

    <input type="hidden" id="hfstyle" runat="server" />
    <input type="hidden" id="hfattr" runat="server" />

    <div class="page">
        <div id="page_title">
            <a href="#" id="back" runat="server" class=" top_left"></a>
            <h1 id="h1togoname" runat="server"></h1>
            <a id="togourl" runat="server" class="top_shop top_right"></a>
        </div>

        <div class="shop_detail" style="position: fixed;">
            <ul>
                <li class="cur_con"><a href="ShowTogo.aspx?id=<%=Request["id"] %>">点菜</a></li>
                <li><a href="Feedback.aspx?id=<%=Request["id"] %>">评价</a></li>
                <li><a href="shopdetail.aspx?id=<%=Request["id"] %>">商家</a></li>
            </ul>
        </div>

       





    </div>



    <div class="mModal sweet-overlay" style="z-index: 900; display: none;">
    </div>
    <div class="mDialog freeSet" style="z-index: 901; display: none; margin-top: 0px;"
        data-ffix-top="99" id="pictip">
        <div class="content">

            <uc4:stylebanner runat="server" ID="mycart1" />

        </div>
        <a class="x" href="javascript:void(0)">X</a>
    </div>


    <script id="stylemodel" type="text/x-jsrender">
        <li>
            <input type="radio" stylename="{{:Title}}" onclick="selectme(this)" name="mystyle" value="{{:DataId}}" id="{{:DataId}}" price="{{:Price}}"><label for="{{:DataId}}">{{:Title}}(￥{{:Price}})</label></li>
    </script>





    <script id="catfoodlist" type="text/x-jsrender">
        {{for #data}}  
        <tr style="position: relative;">
            <td class="name" style="width: 50%">{{:name}}</td>
            <td class="price" style="width: 20%; vertical-align: middle;">￥{{:price+addprice}}</td>
            <td style="width: 30%; vertical-align: middle;" class="price">

                <div class="cart_con" style="position: static;" data-id="{{:id}}" data-name="{{:name}}">
                    <a class="cicon needsclick" onclick="subitem(this,{{:#getIndex()}})">-</a>
                    <span class="cicon mid " id="cart_food_{{:#getIndex()}}">{{:number}}</span>
                    <a class="cicon needsclick" onclick="additem(this,{{:#getIndex()}})">+</a>
                </div>
                <div class=" clear"></div>
            </td>
        </tr>

        {{/for}}
    </script>

    <script id="attrmodel" type="text/x-jsrender">
        {{for #data}}
      
        <div class="cartattr lunch_box_04 clear">

            <h4 id="jh4_{{: #getIndex()}}">{{:Title}}</h4>
            <ul>
                {{for attritems}}

                
             
              <li>{{if #parent.parent.data.SelectType == 0}}



                       <input type="radio" onclick="selectme0({{:FoodtId}}, '{{:name}}', '{{:price}}', '{{:DataId}}', '{{:Title}}')" name="attr_{{:DataId}}"
                           id="attr_{{:FoodtId}}_{{:DataId}}_{{:#getIndex()}}" value="{{:price}}" myname="{{:name}}" cart="jj_{{:#parent.parent.getIndex()}}" price="{{:price}}"><label for="attr_{{:FoodtId}}_{{:DataId}}_{{:#getIndex()}}">{{:name}}(￥{{:price}})</label></li>


                {{else}}

                      <input type="checkbox" onclick="selectme1({{:FoodtId}}, '{{:name}}', '{{:price}}', '{{:DataId}}', '{{:Title}}')" name="box_{{:DataId}}"
                          id="box_{{:FoodtId}}_{{:DataId}}_{{:#getIndex()}}" value="{{:price}}" myname="{{:name}}" cart="jj_{{:#parent.parent.getIndex()}}" price="{{:price}}"><label for="box_{{:FoodtId}}_{{:DataId}}_{{:#getIndex()}}">{{:name}}(￥{{:price}})</label></li>



                    {{/if}}




                {{/for}}

                {{if SelectType == 0}}

                <li><a href="javascript:cancelRadio({{:#parent.parent.data.FoodtId}},{{:#parent.parent.data.DataId}})">取消</a></li>

                {{/if}}
            </ul>
        </div>


        {{/for}}


    </script>





</body>
</html>

<script src="javascript/jquery.js" type="text/javascript"></script>
<script src="javascript/jCommon.js?v=1" type="text/javascript"></script>
<script src="javascript/shopcarttool.js?v=1" type="text/javascript"></script>
<script src="javascript/showtogo.js?v=1" type="text/javascript"></script>
<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/jsrender.js"></script>
<script type="text/javascript">

    var clientHeight = document.documentElement.clientHeight;

    $("#foodbox").css({ "height": (clientHeight - 89) + "px" });

</script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sharelist.aspx.cs" Inherits="Html5.sharelist" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1" />
    <script type="text/javascript" src="javascript/jquery.js"></script>
    <style>
        #shareit {
            -webkit-user-select: none;
            display: none;
            position: absolute;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.85);
            text-align: center;
            top: 0;
            left: 0;
            z-index: 105;
        }

            #shareit img {
                max-width: 100%;
            }

        .arrow {
            position: absolute;
            right: 10%;
            top: 5%;
        }

        #share-text {
            margin-top: 400px;
            color: #fff;
        }
    </style>
</head>


<body>
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" data-ajax="false" id="back" class=" top_left"></a>
            <h1>待分享的红包</h1>
        </div>

        <div class="container">

            <input type="hidden" id="shareurl" />


            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rptorder">
                    <ItemTemplate>
                        <li>


                            <a href="sharedetail.aspx?id=<%#Eval("id")%>">

                                <div class="order-info">
                                    <p class="f14">编号：<span class="red"><%#Eval("id")%></span></p>
                                    <p class="grey">红包个数：<%#Eval("reveint")%></p>
                                </div>


                            </a>




                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>


    <div class="con-btn" id="pages" runat="server"></div>

    <div id="shareit">
        <img class="arrow" src="images/share-it.png" />
        <div id="share-text">点击右上角分享</div>
    </div>


</body>
</html>

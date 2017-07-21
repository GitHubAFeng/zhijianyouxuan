﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testorder.aspx.cs" Inherits="Html5.testorder" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>wxdemo_new</title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/home.css?v=111" />


    <style type="text/css">

        
/************** new loader **********/
.loadEffect{
            width: 100px;
            height: 100px;
            position: relative;
            margin: 0 auto;
            margin-top:100px;
        }
        .loadEffect span{
            display: inline-block;
            width: 20px;
            height: 20px;
            border-radius: 50%;
            background: lightgreen;
            position: absolute;
            animation:load 1.04s infinite;
            -webkit-animation: load 1.04s ease infinite;
        }
        @keyframes load
        {
            0%{
                transform: scale(1.2);
                opacity: 1;
            }
            100%{
                transform: scale(.3);
                opacity: 0.5;
            }
        }
        @-webkit-keyframes load{
            0%{
                -webkit-transform: scale(1.2);
                opacity: 1;
            }
            100%{
                -webkit-transform: scale(.3);
                opacity: 0.5;
            }
        }
        .loadEffect span:nth-child(1){
            left: 0;
            top: 50%;
            margin-top:-10px;
            -webkit-animation-delay:0.13s;
        }
        .loadEffect span:nth-child(2){
            left: 14px;
            top: 14px;
            -webkit-animation-delay:0.26s;
        }
        .loadEffect span:nth-child(3){
            left: 50%;
            top: 0;
            margin-left: -10px;
            -webkit-animation-delay:0.39s;
        }
        .loadEffect span:nth-child(4){
            top: 14px;
            right:14px;
            -webkit-animation-delay:0.52s;
        }
        .loadEffect span:nth-child(5){
            right: 0;
            top: 50%;
            margin-top:-10px;
            -webkit-animation-delay:0.65s;
        }
        .loadEffect span:nth-child(6){
            right: 14px;
            bottom:14px;
            -webkit-animation-delay:0.78s;
        }
        .loadEffect span:nth-child(7){
            bottom: 0;
            left: 50%;
            margin-left: -10px;
            -webkit-animation-delay:0.91s;
        }
        .loadEffect span:nth-child(8){
            bottom: 14px;
            left: 14px;
            -webkit-animation-delay:1.04s;
        }

/************** new loader **********/

    </style>


</head>
<body>
    <input id="hfpage" runat="server" type="hidden" />
    <input id="hfcpage" runat="server" type="hidden" value="1" />
    <header id="page_title">
        <div class="container">
            <h1>选择区域</h1>
        </div>
    </header>
    <div class="container">
        <ul id="shoplist">
        </ul>
        <div class="con-btn" id="pages" runat="server">
        </div>

        <div class="loadEffect">
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
        </div>
    </div>

</body>
</html>


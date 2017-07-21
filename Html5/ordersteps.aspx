<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ordersteps.aspx.cs" Inherits="Html5.ordersteps" %>

<!DOCTYPE html>

<html >
<head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>微信下单流程</title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1" />
    <style type="text/css">
        .container {
            padding-bottom: 0;
            position: relative;
        }

            .container img {
                width: 100%;
            }

            .container .wx-qr {
                position: absolute;
                top: 16.9%;
                right: 12%;
                width: 15.5%;
                max-width: 100px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page" style="padding-bottom: 0;">
            <div id="page_title">
                <a href="TogoList.aspx" id="back" runat="server" class=" top_left"></a>
                <h1>下单流程</h1>
            </div>
            <div class="container">
                <img src="images/app_img.jpg" class="wx-qr" />
                <img src="images/ordersteps.jpg" />
            </div>
        </div>
    </form>
</body>
</html>

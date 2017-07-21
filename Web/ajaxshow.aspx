<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxshow.aspx.cs" Inherits="ajaxshow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="css/sytle.css" rel="stylesheet" type="text/css" />
    <link href="css/news.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .annouce_left
        {
            width: 720px;
            float: left;
        }
        .annouce_left h1
        {
            font-size: 18px;
            color: #000000;
            font-weight: bold;
            text-align: center;
            height: 40px;
            line-height: 40px;
        }
        .content_div
        {
            width: 670px;
            margin: 10px;
        }
        .right_ul li
        {
            background: url(Images/dian.jpg) no-repeat 0 50%;
            padding-left: 15px;
            _padding-left: 30px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div style="margin-bottom: 20px;">
        <div class="annouce_left" style="border: 1px solid #EAEAEA; margin-bottom: 20px;">
            <div style="border-bottom: 1px dotted #ccc; margin: 0px 10px; text-align: center;">
                <h1 runat="server" id="newstitle">
                </h1>
                <div runat="server" id="divtime">
                </div>
            </div>
            <div class="content_div" runat="server" id="newsContent">
            </div>
        </div>
    </div>
    </form>
</body>
</html>

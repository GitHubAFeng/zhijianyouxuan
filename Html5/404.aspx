<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Html5._04" %>

<!DOCTYPE html>
<html>
<!--[if lt IE 7]> <html class="no-js ie6 oldie" lang="en"> <![endif]-->
<!--[if IE 7]>    <html class="no-js ie7 oldie" lang="en"> <![endif]-->
<!--[if IE 8]>    <html class="no-js ie8 oldie" lang="en"> <![endif]-->
<!--[if gt IE 8]> <html class="no-js" lang="zh_CN"> <!--<![endif]-->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>404 - 文件没有找到</title>


    <link rel="stylesheet" href="css/404.css">

    <script src="javascript/jquery.js"></script>

    <script src="javascript/404.js"></script>

</head>
<body>
    <div id="error-container">
        <div id="error">
            <div id="pacman">
            </div>
        </div>
        <div id="container">
            <div id="title">
                <h1>
                    对不起, 您访问的页面不存在!  </h1>
            </div>
           
            <div id="footer">
               <%= SectionProxyData.GetSetValue(47) %>
            </div>
        </div>
    </div>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= SectionProxyData.GetSetValue(3)%>
    </title>
    <style type="text/css">
        <!
        -- *
        {
            margin: 0;
            padding: 0;
            color: #333;
        }
        a img
        {
            border: none;
        }
        a:link, a:visited
        {
            color: #ff6000;
        }
        a:hover, a:active
        {
            color: #f60;
        }
        #wrap
        {
            width: 500px;
            height: 200px;
            padding: 30px;
            margin:100px auto 0;
            border: 1px solid #ccc;
            position: relative;
            font: 12px Arial, "宋体";
            -moz-box-shadow: 0 0 10px #ccc;
            -webkit-box-shadow: 0 0 10px #ccc;
            box-shadow: 0 0 10px #ccc;
        }
        #wrap h1
        {
            position: absolute;
            top: 200px;
            left: 420px;
            overflow: hidden;
            -moz-transition: all .3s ease-in-out;
            -webkit-transition: all .3s ease-in-out;
            font-size:20px;
            
        }
        #wrap h1 a{text-decoration:none;}
        #wrap h1 a:hover{text-decoration:underline;}
        #wrap h2
        {
            font-size: 16px;
            color: #925842;
            font-weight: bold;
            margin-top:0;
        }
        #content
        {
            line-height: 25px;
            margin-top: 20px;
        }
        #content h3
        {
            font-size: 14px;
            font-weight: bold;
            color: #999;
            margin-bottom:5px;
        }
        #content hr
        {
            height: 1px;
            border: none;
            border-top: 1px solid #ccc;
            margin-bottom: 10px;
        }
        -- ></style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrap">
        <h1>
            <a href="index.aspx" title="">
                <%= SectionProxyData.GetSetValue(2) %></a></h1>
        <h2>
            对不起，没有找到您需要的页面！</h2>
        <div id="content">
            <h3>
                DETAILS</h3>
            <hr />
            404错误的原因可能是：<br />
            1、您访问的页面不存在。<br />
            2、网站正在更新，系统正在编译。<br />
            <p><a href="index.aspx">返回首页</a></p>
        </div>
    </div>
    </form>
</body>
</html>

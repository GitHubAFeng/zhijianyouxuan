<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>服务暂时不可用，请稍候再试</title>
</head>
<body>
    <center>
        <div style="margin-top: 60px; width: 400px; text-align: left;">
            <div style="border-bottom: 3px solid #999; padding-bottom: 5px;">
                <img src="images/timeout.gif" alt="服务暂时不可用，请稍候再试" height="23" width="369">
            </div>
            <div style="">
            </div>
            
            <div style="padding: 20px 0; font-size: 14px; line-height: 150%;">
                你访问的页面由于一些原因暂时不可用，请稍候再试。<br>
            </div>
            <div style="border-top: dashed 1px #999; text-align: right; padding-top: 4px;">
                <a href="index.aspx">
                    <img src="images/logo.png"border="0" width="100"/></a>
            </div>
        </div>
    </center>
</body>
</html>

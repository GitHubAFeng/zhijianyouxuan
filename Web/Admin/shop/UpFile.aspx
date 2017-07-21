<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpFile.aspx.cs" Inherits="member_Up1File" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传图片</title>
    <style type="text/css">
        <!
        -- td
        {
            font-size: 12px;
            font-family: "宋体";
        }
        A:link
        {
            color: #039;
            text-decoration: underline;
        }
        A:visited
        {
            color: #039;
            text-decoration: underline;
        }
        A:hover
        {
            color: #f60;
            text-decoration: underline;
        }
        .left:link
        {
            text-decoration: none;
            color: #000000;
            font-size: 14.8px;
        }
        .left:visited
        {
            color: #111111;
            text-decoration: none;
            font-size: 14.8px;
        }
        .left:hover
        {
            text-decoration: underline;
            color: #FF6701;
            font-size: 14.8px;
        }
        .left1:link
        {
            text-decoration: none;
            color: #925842;
            font-size: 14.8px;
            font-weight: bold;
        }
        .left1:visited
        {
            color: #925842;
            text-decoration: none;
            font-size: 14.8px;
            font-weight: bold;
        }
        .left1:hover
        {
            text-decoration: underline;
            color: #925842;
            font-size: 14.8px;
            font-weight: bold;
        }
        .top:link
        {
            text-decoration: none;
            color: #0066cc;
            font-size: 12px;
        }
        .top:visited
        {
            color: #0066cc;
            text-decoration: none;
            font-size: 12px;
        }
        .top:hover
        {
            text-decoration: underline;
            color: #925842;
            font-size: 12px;
        }
        .top1:link
        {
            text-decoration: none;
            color: #000000;
            font-size: 12px;
        }
        .top1:visited
        {
            color: #000000;
            text-decoration: none;
            font-size: 12px;
        }
        .top1:hover
        {
            text-decoration: underline;
            color: #925842;
            font-size: 12px;
        }
        .leftnav
        {
            background-color: E6E6E6;
            text-align: right;
            padding: 6px 10px 6px 0px;
            cursor: hand;
        }
        .leftnav1
        {
            background-color: ffffff;
            text-align: right;
            padding: 6px 10px 6px 0px;
        }
        .leftnav2
        {
            background-color: f2f2f2;
            text-align: right;
            padding: 6px 10px 6px 0px;
            cursor: hand;
        }
        .topnav
        {
            background-color: ccffff;
            text-align: center;
        }
        .topnav1
        {
            background-color: ffffff;
            text-align: center;
        }
        .note
        {
            color: #999999;
        }
        .homebg
        {
            background-repeat: no-repeat;
            background-position: right bottom;
        }
        .bdr1
        {
            border-color: #EDEDED;
            border-style: solid;
            border-top-width: 5px;
            border-right-width: 5px;
            border-bottom-width: 5px;
            border-left-width: 5px;
            padding: 0px 0px 0px 0px;
        }
        .bdr2
        {
            border-color: #CCCCCC;
            border-style: solid;
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            padding: 12px 12px 12px 12px;
        }
        .bdr3
        {
            border-color: #CC6633;
            border-style: solid;
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            padding: 12px 12px 12px 12px;
        }
        .tbg1
        {
            background-color: CC6633;
            font-size: 14.8px;
            color: #ffffff;
            padding: 4px 4px 4px 4px;
            font-weight: bold;
        }
        .tbg2
        {
            background-color: FFF7D8;
            font-size: 12px;
            color: #000000;
            padding: 4px 4px 4px 4px;
        }
        .tbg3
        {
            background-color: CC6633;
            font-size: 16px;
            color: #ffffff;
            padding: 4px 4px 4px 4px;
            font-weight: bold;
        }
        .fbg1
        {
            border-color: #FFCC99;
            border-style: solid;
            border-top-width: 2px;
            border-right-width: 0px;
            border-bottom-width: 0px;
            border-left-width: 0px;
            padding: 4px 4px 4px 4px;
            background-color: #F1EEEE;
            color: #CC3300;
            font-weight: bold;
        }
        .formtop
        {
            text-align: right;
            vertical-align: top;
            padding-top: 6px;
        }
        .X
        {
            font-size: 24px;
            font-weight: bold;
        }
        .L
        {
            font-size: 16px;
            font-weight: bold;
        }
        .M
        {
            font: bold 14.8px 宋体;
        }
        .C
        {
            font-size: 14.8px;
        }
        .S
        {
            font-size: 12px;
        }
        .lh13
        {
            line-height: 130%;
        }
        .lh17
        {
            line-height: 170%;
        }
        .padded
        {
            padding: 12px 12px 12px 12px;
        }
        .temp
        {
            color: #999999;
        }
        .rd14
        {
            font-size: 14px;
            font-weight: bold;
            color: #cc0000;
        }
        .sg
        {
            color: #666666;
        }
        .pj1x
        {
            color: #006600;
            font-weight: bold;
            font-size: 12px;
        }
        .pj2x
        {
            font-size: 12px;
            font-weight: bold;
        }
        .pj3x
        {
            font-size: 12px;
            font-weight: bold;
            color: #FF0000;
        }
        .pj1
        {
            color: #006600;
            font-weight: bold;
            font-size: 14px;
        }
        .pj2
        {
            font-size: 14px;
            font-weight: bold;
        }
        .pj3
        {
            font-size: 14px;
            font-weight: bold;
            color: #FF0000;
        }
        /*以下是新的样式*/.linkstyle1:link
        {
            text-decoration: underline;
            color: #FFFFFF;
            font-size: 12px;
        }
        .linkstyle1:visited
        {
            color: #FFFFFF;
            text-decoration: underline;
            font-size: 12px;
        }
        .linkstyle1:hover
        {
            text-decoration: underline;
            color: #925842;
            font-size: 12px;
        }
        .linkstyle2:link
        {
            text-decoration: underline;
            color: #000000;
            font-size: 14.8px;
        }
        .linkstyle2:visited
        {
            color: #000000;
            text-decoration: underline;
            font-size: 14.8px;
        }
        .linkstyle2:hover
        {
            text-decoration: underline;
            color: #925842;
            font-size: 14.8px;
        }
        .linkstyle3:link
        {
            text-decoration: underline;
            color: #925842;
            font-size: 14.8px;
            font-weight: bold;
        }
        .linkstyle3:visited
        {
            color: #925842;
            text-decoration: underline;
            font-size: 14.8px;
            font-weight: bold;
        }
        .linkstyle3:hover
        {
            text-decoration: underline;
            color: #925842;
            font-size: 14.8px;
            font-weight: bold;
        }
        .font_13_white
        {
            font-size: 13px;
            font-family: "宋体";
            color: #FFFFFF;
        }
        .index_title
        {
            font-size: 14.8px;
            font-family: "宋体";
            color: #000000;
            padding-top: 2px;
            font-weight: bold;
        }
        .index_content
        {
            font-size: 14.8px;
            font-family: "宋体";
            color: #000000;
            line-height: 150%;
        }
        .content_border
        {
            border-style: solid;
            border-width: 0px 1px 1px 1px;
            border-color: 000000 BDD7F7 BDD7F7 BDD7F7;
        }
        .subbutton
        {
            font-size: 14px;
            cursor: hand;
            line-height: 19px;
        }
        .nobianbuttonborder
        {
            border-top: #FFFFFF 5 solid;
            background: #F1F1F1;
            border-bottom: #cccccc 1 solid;
            text-align: center;
            font-size: 12px;
            font: bold;
            height: 25px;
        }
        .outborder
        {
            border-right: #999999 1px solid;
            border-top: #999999 1px solid;
            border-left: #999999 1px solid;
            border-bottom: #999999 1px solid;
        }
        .red
        {
            font-weight: bold;
            color: #cc3300;
        }
        .red:link
        {
            color: #cc3300;
        }
        .red:visited
        {
            color: #cc3300;
        }
        .red:hover
        {
            color: #f60;
        }
        .lh15
        {
            line-height: 150%;
        }
        .lh13
        {
            line-height: 130%;
        }
        .lh18
        {
            font-size: 14px;
            line-height: 150%;
        }
        -- ></style>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="background-color: #FFF8EE;">
    <form id="form1" runat="server">
    <center>
        <table width="100%" class="fbg1">
            <tr>
                <td>
                    上传图片
                    <!---->
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="1" cellpadding="5" width="100%" align="center">
            <tr>
                <td>

                    <asp:FileUpload runat="server" ID="fileUp" />
                    <asp:Button runat="server" ID="btSave" Text="确 定" class="button2" 
                        onclick="btSave_Click"/>
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>

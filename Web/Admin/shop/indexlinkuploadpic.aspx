<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indexlinkuploadpic.aspx.cs" Inherits="qy_54tss_Admin_uploadpic_brandindexlinkuploadpic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传图片</title>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-top: 20px;">
        <asp:HiddenField ID="hfsid" runat="server" Value="0" />
        <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
        <asp:HiddenField ID="FolderType" Value="1" runat="server" />
        <asp:HiddenField ID="WaterType" Value="0" runat="server" />
        &nbsp; <a href="####" id="txturldz" target="_blank">
            <img border="0" src="/images/nopic_02.jpg" id="ImgUrl" alt="" style="width: 57px;
                height: 57px" runat="server" /></a><br />
        <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display='block';return txtupload_onclick();" />请上传57*57的图片<br />
        <div id="rowTest" style="display: none">
            <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 400px; height: 100px"
                frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight">
            </iframe>
        </div>
        <div id="Upload">
        </div>
        <br />
        <input type="button" value="确定" onclick="goog()" />
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function goog() {
        var id = $("#hfsid").val() + "";
        var pic = $("#ImgUrl1").val();
        var para = "method=uploadpic&id=" + id + "&pic=" + pic + "&type=ShopData";
        jQuery.ajax(
        {
            type: "post",
            url: "../../ajaxHandler.ashx",
            data: para,
            success: function(msg) {
                window.opener.location.reload(true);
                alert('上传成功.');
                window.close();
            }
        })
    }
</script>
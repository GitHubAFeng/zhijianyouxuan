<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Foodimport.aspx.cs" Inherits="TogoHome_Foodimport" Async="true" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品导入-<%= WebUtility.GetWebName() %></title>
    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />


    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>


    <style type="text/css">
        #cblweekday {
            float: left;
        }

        .mynotice {
            background-color: #F1FAFE;
            border: 1px solid #B1DFF3;
            padding: 3px;
            color: #0863C0;
            _padding-top: 6px;
            margin-left: 3px;
            display: inline;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <div class="shop_main">

                        <div class="main-content">
                            <div class="shop_menu">
                                <ul>
                                    <li><a href="FoodSortList.aspx?">餐品类别列表</a></li>
                                    <li><a href="FoodSortDetail.aspx">添加餐品类别</a></li>
                                    <li><a href="FoodList.aspx">餐品列表</a></li>
                                    <li><a href="FoodDetail.aspx">添加餐品</a></li>
                                    <li class="cur"><a href="Foodimport.aspx">批量导入</a></li>
                                </ul>
                            </div>
                            <div class="">

                                <div class="usermima">
                                    <ul>
                                        <li><span class="left_span">选择文件 <font color="red">*</font>：</span>


                                            <asp:FileUpload runat="server" ID="fuFoodExcel" />

                                            <br />

                                            <a href="/upload/demo.xls" runat="server" id="a_downloadexcel">下载Excel模版</a>&nbsp;&nbsp;<asp:Button
                                                ID="tbinfood" runat="server" class="subBtn" OnClick="in_Click" OnClientClick="showload_super();" Text="菜单导入" />&nbsp;&nbsp;<asp:Button
                                                    Text="删除菜品" runat="server" ID="Button1" OnClick="del_Click" class="subBtn" OnClientClick="return confirm('确认要删除吗？');" />


                                            &nbsp;&nbsp;<asp:Button
                                                Text="删除分类" runat="server" ID="Button2" OnClick="delsort_Click" class="subBtn" OnClientClick="return confirm('确认要删除吗？');" />



                                            <asp:Button ID="Button3" runat="server" Text="上传帮助" CssClass="subBtn" OnClientClick="tipsWindown('上传帮助','id:divhelp','250','150','true','','true','text');return false;" />

                                        </li>


                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div id="divhelp" style="display: none">
                <div style="text-align: left; padding: 10px; line-height: 24px;">
                    <p>
                        1,点击下载Excel,根据excel模板输入菜品信息
                    </p>
                    <p>
                        2,输入完成后，直接上传就可以了。请按照excel模板输入信息
                    </p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>


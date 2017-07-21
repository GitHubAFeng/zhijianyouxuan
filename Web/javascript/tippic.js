//鼠标悬停效果
$(function () {
    xOffset = 10;
    yOffset = 30;



    $("a.tooltip").mouseover(function (e) {
        this.myTitle = this.title;
        this.title = "";
        var imgTitle = this.myTitle ? "<br/>" + this.myTitle : "";

        var tooltip = "<div id='tooltip'><img src='" + this.href + "' alt='产品预览图'/>" + imgTitle + "<\/div>"; //创建 div 元素

        var srcstr = $(this).attr("href");
        if (srcstr == "/images/nopic_02.jpg") {
             tooltip = ""; //创建 div 元素
        }
       
        $("body").append(tooltip);	//把它追加到文档中
        $("#tooltip")
            .css({
                "top": (e.pageY - xOffset) + "px",
                "left": (e.pageX + yOffset) + "px"
            }).show("fast");	  //设置x坐标和y坐标，并且显示
    }).mouseout(function () {
        this.title = this.myTitle;
        $("#tooltip").remove();	 //移除
    }).mousemove(function (e) {
        $("#tooltip")
            .css({
                "top": (e.pageY - xOffset) + "px",
                "left": (e.pageX + yOffset) + "px"
            });
    });
})
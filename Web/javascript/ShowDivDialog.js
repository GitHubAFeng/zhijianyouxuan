/*弹出div对话框*/
/*参数：[可选参数在调用时可写可不写,其他为必写]
    title:	窗口标题
  content:  内容(可选内容为){ text | id | img | url | iframe }
    width:	内容宽度
   height:	内容高度
	 drag:  是否可以拖动(ture为是,false为否)
     time:	自动关闭等待的时间，为空是则不自动关闭
   showbg:	[可选参数]设置是否显示遮罩层(0为不显示,1为显示)
  cssName:  [可选参数]附加class名称
 ------------------------------------------------------------------------*/
 //示例:
 //simpleWindown("例子","text:例子","500","400","true","3000","0","exa")
 /*
 	$("#text1").click(function() {
		tipsWindown("标题","text:提示信息内容","250","150","true","","true","text")
	});
	$("#text2").click(function() {
		tipsWindown("标题","text:我不能拖动，而且还没遮罩背景","250","150","false","","false","text")
	});
	$("#text3").click(function() {
		tipsWindown("标题","text:我不能拖动，但3秒钟后我会自动消失","250","150","false","3000","true","text")
	});

	$("#id1").click(function() {
		tipsWindown("标题","id:dialogContent","500","300","true","","true","id")
	});

	$("#img1").click(function() {
		tipsWindown("图片","img:http://leotheme.cn/wp-content/uploads/2008/09/tr2xcp6yw4o5xebj9s.jpg","500","230","true","","true","img")
	});

	$("#url1").click(function(){
		tipsWindown("标题","url:get?test.html","250","150","true","","true","text");
	});

	$("#iframe1").click(function(){
		tipsWindown("标题","iframe:http://leotheme.cn","950","527","true","","true","leotheme");
	});
 */
var showWindown = true; //设置弹出窗是否已经显示
var templateSrc = "/";  //设置loading.gif路径
function tipsWindown(title, content, width, height, drag, time, showbg, cssName) {
    $("#windown-box").remove(); //清除内容
    var width = width >= 950 ? this.width = 950 : this.width = width;     //设置最大窗口宽度
    var height = height >= 527 ? this.height = 527 : this.height = height;   //设置最大窗口高度
    if (showWindown == true) {
        var simpleWindown_html = new String;
        simpleWindown_html = "<div id=\"windownbg\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.8);opacity:0.8;z-index: 999901;background: none repeat scroll 0% 0% rgb(50, 50, 50); \"></div>";
        simpleWindown_html += "<div id=\"windown-box\">";
        simpleWindown_html += "<div id=\"windown-title\"><h2></h2><span id=\"windown-close\"><a href='javascript:'> </a></span></div>";
        simpleWindown_html += "<div id=\"windown-content-border\"><div id=\"windown-content\"></div></div>";
        simpleWindown_html += "</div>";
        $("body").append(simpleWindown_html);
        show = false;
    }
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "text":
            $("#windown-content").html(content);
            break;
        case "id":
            $("#windown-content").html($("#" + content + "").html());
            break;
        case "img":
            $("#windown-content").ajaxStart(function() {
                $(this).html("<img src='" + templateSrc + "images/loading.gif' class='loading' />");
            });
            $.ajax({
                error: function() {
                    $("#windown-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#windown-content").html("<img src=" + content + " alt='' />");
                }
            });
            break;
        case "url":
            var content_array = content.split("?");
            $("#windown-content").ajaxStart(function() {
                $(this).html("<img src='" + templateSrc + "images/loading.gif' class='loading' />");
            });
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#windown-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#windown-content").html(html);
                }
            });
            break;
        case "iframe":
            $("#windown-content").ajaxStart(function() {
                $(this).html("<img src='" + templateSrc + "images/loading.gif' class='loading' />");
            });
            $.ajax({
                error: function() {
                    $("#windown-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#windown-content").html("<iframe src=\"" + content + "\" width=\"100%\" height=\"" + parseInt(height) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");
                }
            });
    }
    $("#windown-title h2").html(title);
    if (showbg == "true") { $("#windownbg").show(); } else { $("#windownbg").remove(); };
    $("#windownbg").animate({ opacity: "0.8" }, "normal"); //设置透明度
    $("#windown-box").show();
    if (height >= 527) {
        $("#windown-title").css({ width: (parseInt(width) + 22) + "px" });
        $("#windown-content").css({ width: (parseInt(width) + 17) + "px", height: height + "px" });
    } else {
        $("#windown-title").css({ width: (parseInt(width) + 10) + "px" });
        $("#windown-content").css({ width: width + "px", height: height + "px" });
    }
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    //var _version = $.browser.version;
    //if (_version == 6.0) {
    //    $("#windown-box").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    //} else {
        $("#windown-box").css({ left: "50%", top: "50%", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    //};
    var Drag_ID = document.getElementById("windown-box"), DragHead = document.getElementById("windown-title");

    var moveX = 0, moveY = 0, moveTop, moveLeft = 0, moveable = false;
    //if (_version == 6.0) {
        moveTop = est;
    //} else {
        moveTop = 0;
    //}
    var sw = Drag_ID.scrollWidth, sh = Drag_ID.scrollHeight;
    DragHead.onmouseover = function(e) {
        if (drag == "true") { DragHead.style.cursor = "move"; } else { DragHead.style.cursor = "default"; }
    };
    DragHead.onmousedown = function(e) {
        if (drag == "true") { moveable = true; } else { moveable = false; }
        e = window.event ? window.event : e;
        var ol = Drag_ID.offsetLeft, ot = Drag_ID.offsetTop - moveTop;
        moveX = e.clientX - ol;
        moveY = e.clientY - ot;
        document.onmousemove = function(e) {
            if (moveable) {
                e = window.event ? window.event : e;
                var x = e.clientX - moveX;
                var y = e.clientY - moveY;
                if (x > 0 && (x + sw < cw) && y > 0 && (y + sh < ch)) {
                    Drag_ID.style.left = x + "px";
                    Drag_ID.style.top = parseInt(y + moveTop) + "px";
                    Drag_ID.style.margin = "auto";
                }
            }
        }
        document.onmouseup = function() { moveable = false; };
        Drag_ID.onselectstart = function(e) { return false; }
    }
    $("#windown-content").attr("class", "windown-" + cssName);

    //定时器 事件
    var closeWindown = function() {
        $("#windownbg").remove();
        $("#windown-box").fadeOut("slow", function() { $(this).remove(); });
    }

    if (time == "" || typeof (time) == "undefined") {
        //关闭事件
        $("#windown-close").click(function() {
            $("#windownbg").remove();
            $("#windown-box").fadeOut("slow", function() { $(this).remove(); });
            //alert("执行回调函数");
        });
    }
    else {
        //关闭事件
        $("#windown-close").click(function() {
            $("#windownbg").remove();
            $("#windown-box").fadeOut("slow", function() { $(this).remove(); });
            //alert("执行回调函数");
        });
        setTimeout(closeWindown, time);
    }
    hideload_super();
}

function Test()
{
    alert("执行回调函数");
}

function jtip(msg) {
    tipsWindown('提示信息', 'text:'+msg+'', '250', '150', 'true', '2000', 'true', 'text');
}

function jerror(msg) {
    tipsWindown('提示信息', 'text:系统繁忙，请稍后...', '250', '150', 'true', '2000', 'true', 'text');
}

/********************************************************************************************************/
  var ImgObj = new Image();//建立一个图像对象 
  var AllImgExt=".jpg|.jpeg|.gif|.bmp|.png|"//全部图片格式类型 
  var FileObj,ImgFileSize,ImgWidth,ImgHeight,FileExt,ErrMsg,FileMsg,HasCheked,IsImg//全局变量 图片相关属性 

  //以下为限制变量 
  var AllowExt=".jpg|.gif|"//允许上传的文件类型 &#320;为无限制 每个扩展名后边要加一个"|" 小写字母表示 
  //var AllowExt=0 
  var AllowImgFileSize=500;//允许上传图片文件的大小 0为无限制  单位：KB  
  var AllowImgWidth=1024;//允许上传的图片的宽度 &#320;为无限制　单位：px(像素) 
  var AllowImgHeight=1024;//允许上传的图片的高度 &#320;为无限制　单位：px(像素) 

  HasChecked=false; 

  function CheckProperty(obj)//检测图像属性 
  { 
       FileObj=obj; 
       if(ErrMsg!="")//检测是否为正确的图像文件　返回出错信息并重置 
       { 
             ShowMsg(ErrMsg,false); 
             return false;//返回 
       } 

       if(ImgObj.readyState!="complete")//如果图像是未加载完成进行循环检测 
       { 
             setTimeout("CheckProperty(FileObj)",500); 
             return false; 
       } 

       ImgFileSize=Math.round(ImgObj.fileSize/1024*100)/100;//取得图片文件的大小 
       ImgWidth=ImgObj.width//取得图片的宽度 
       ImgHeight=ImgObj.height;//取得图片的高度 
       FileMsg="\n图片大小:"+ImgWidth+"*"+ImgHeight+"px"; 
       FileMsg=FileMsg+"\n图片文件大小:"+ImgFileSize+"Kb"; 
       FileMsg=FileMsg+"\n图片文件扩展名:"+FileExt; 

       if(AllowImgWidth!=0&&AllowImgWidth<ImgWidth) 
       ErrMsg=ErrMsg+"\n图片宽度超过限制。请上传宽度小于"+AllowImgWidth+"px的文件，当前图片宽度为"+ImgWidth+"px"; 

       if(AllowImgHeight!=0&&AllowImgHeight<ImgHeight) 
       ErrMsg=ErrMsg+"\n图片高度超过限制。请上传高度小于"+AllowImgHeight+"px的文件，当前图片高度为"+ImgHeight+"px"; 

       if(AllowImgFileSize!=0&&AllowImgFileSize<ImgFileSize) 
       ErrMsg=ErrMsg+"\n图片文件大小超过限制。请上传小于"+AllowImgFileSize+"KB的文件，当前文件大小为"+ImgFileSize+"KB"; 

       if(ErrMsg!="") 
       ShowMsg(ErrMsg,false); 
       else 
       ShowMsg(FileMsg,true); 
 } 

 ImgObj.onerror=function(){ErrMsg='\n图片格式不正确或者图片已损坏!'} 

 function ShowMsg(msg,tf)//显示提示信息 tf=true 显示文件信息 tf=false 显示错误信息 msg-信息内容 
 { 
       msg=msg.replace("\n","<li>"); 
       msg=msg.replace(/\n/gi,"<li>"); 
       if(!tf) 
       { 
             document.all.UploadButton.disabled=true; 
             FileObj.outerHTML=FileObj.outerHTML; 
             MsgList.innerHTML=msg; 
             HasChecked=false; 
       } 
       else 
       { 
             document.all.UploadButton.disabled=false; 
             if(IsImg) 
                PreviewImg.innerHTML="<img src='"+ImgObj.src+"' width='80' height='90'>" 
             else 
                PreviewImg.innerHTML="非图片文件"; 
             MsgList.innerHTML=msg; 
             HasChecked=true; 
        } 
  } 

  function CheckExt(obj) 
  {        
        ErrMsg=""; 
        FileMsg=""; 
        FileObj=obj; 
        IsImg=false; 
        HasChecked=false; 
        PreviewImg.innerHTML="预览区"; 
        if(obj.value=="")return false; 
        MsgList.innerHTML="文件信息处理中..."; 
            
        document.getElementById("UploadButton").disabled = true;  //修改后
        FileExt=document.getElementById("FileImg").value.substr(document.getElementById("FileImg").value.lastIndexOf(".")).toLowerCase();
           
            
        if( AllowExt.indexOf(FileExt+"|") == -1) //判断文件类型是否允许上传 
        { 
              ErrMsg="\n该文件类型不允许上传。请上传 "+AllowExt+" 类型的文件，当前文件类型为"+FileExt; 
              ShowMsg(ErrMsg,false); 
              return false; 
        } 

        if(AllImgExt.indexOf(FileExt+"|")!=-1)//如果图片文件，则进行图片信息处理 
        { 
              IsImg=true; 
              ImgObj.src=obj.value; 
              CheckProperty(obj); 
              return false; 
        } 
        else 
        { 
              FileMsg="\n文件扩展名:"+FileExt; 
              ShowMsg(FileMsg,true); 
         }  
  } 
            
 function showDiv()
 {
     document.getElementById("updateImg").style.display = "block";
     document.getElementById("mask_info").style.display = "block";      
 }
 
 function hideDiv()
 {
     document.getElementById("updateImg").style.display = "none";
     document.getElementById("mask_info").style.display = "none";
 }
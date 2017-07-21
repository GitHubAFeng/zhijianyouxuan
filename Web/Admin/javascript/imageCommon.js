/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : $codebesideclassname$
 * Function :  图片显示自动适应大小
 * Created by jijunjian at $time$.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
var flag = false;
function DrawImage(ImgD) {
    var image = new Image();
    image.src = ImgD.src;
    if (image.width > 0 && image.height > 0) {
        flag = true;
        if (image.width / image.height >= 700 / 700) {
            if (image.width > 400) {
                ImgD.width = 400;
                ImgD.height = (image.height * 400) / image.width;
            }
            else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        }
        else {
            if (image.height > 400) {
                ImgD.height = 400;
                ImgD.width = (image.width * 400) / image.height;
            }
            else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        }
    }
}     
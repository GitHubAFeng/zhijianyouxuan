function GetPrinterSn() {
    var PrinterNum = document.getElementById("tbPrinterNum").value;
    var reqUrl = "../ajax/GetPrinterInfo.aspx?num=" + PrinterNum + "&t=" + new Date();

    jQuery.ajax(
    {
        type: "get",
        url: reqUrl,
        cache: false,
        success: function (responseStr) {
            if (responseStr == "0") {
                tipsWindown('错误信息', 'text:此打印机已经在其他商家使用,如需要使用此打印机则进入打印机管理中设置此打印机为未使用状态', '440', '150', 'true', '', 'true', 'text')
            }
            else if (responseStr == "-1") {
                tipsWindown('错误信息', 'text:不存在此打印机编号，请确认系统中已经存在此打印机，查看打印机管理－>打印机列表 中是否存在该打印机', '440', '150', 'true', '', 'true', 'text')
            }
            else {
                var objDiv = document.getElementById("tbPrinterSn");
                objDiv.value = responseStr;
            }
        }
    })
}

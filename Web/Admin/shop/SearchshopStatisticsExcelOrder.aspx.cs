using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Net;
using org.in2bits.MyXls;
using System.Collections.Generic;
using System; //生成excel时需要使用的引用

public partial class Admin_shop_SearchshopStatisticsExcelOrder : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }
    Custorder bll = new Custorder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HjNetHelper.GetQueryString("type") == "1")
            {
                //由前一个页面的检索条件直接获取Excel
                if (!string.IsNullOrEmpty(WebUtility.FixgetCookie("webStatisticsExcleOrder")))
                {
                    SqlWhere = WebUtility.FixgetCookie("webStatisticsExcleOrder");

                    //保存此状态
                    //WebUtility.FixdelCookie("ExcelSearchOrderSqlWhere");
                    //WebUtility.FixsetCookie("ExcelSearchOrderSqlWhere", SqlWhere, 1);
                }
                else
                {
                    SqlWhere = "1=1";
                }
                GetExcel();
            }
        }
    }

    protected void GetExcel()
    {
        string year = WebUtility.FixgetCookie("ddlyear");

        org.in2bits.MyXls.XlsDocument doc = new XlsDocument();

        //根据当前时间生成文件名 TODO;增加用户名

        doc.FileName = "OrderRecord.xls";//" + WebUtility.GetTime() + "
        doc.SummaryInformation.Author = "IHangjing"; //作者
        doc.SummaryInformation.Subject = "Order Record";
        doc.DocumentSummaryInformation.Company = "IHangjing.com";

        doc.Workbook.ProtectContents = true;

        int RowCount = bll.GetCount(SqlWhere);//TODO;改成链接查询的获取记录数目函数

        IList<OrderCountInfo> list = new List<OrderCountInfo>();
        list = bll.GetOrderCount(4, year, "", "", SqlWhere);

        string sheetName = "OrderRecord";

        Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
        Cells cells = sheet.Cells;

        int rowMin = 1;
        int rowCount = RowCount;
        int colMin = 1;
        int colCount = 6;     //11列
        string cellName = "";  //列名
        string cellValue = ""; //列的值

        //设置列的格式
        ColumnInfo colInfo = new ColumnInfo(doc, sheet);//生成列格式对象
        colInfo.ColumnIndexStart = 0;
        colInfo.ColumnIndexEnd = 6;
        colInfo.Width = 6 * 1024;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo);

        ColumnInfo colInfo2 = new ColumnInfo(doc, sheet);//生成列格式对象
        //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
        colInfo2.ColumnIndexStart = 7;    //起始列为第二列
        colInfo2.ColumnIndexEnd = 8;      //终止列为第六列
        colInfo2.Width = 1 * 2048;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo2);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

        ColumnInfo colInfo3 = new ColumnInfo(doc, sheet);//生成列格式对象
        //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
        colInfo3.ColumnIndexStart = 1;    //起始列为第二列
        colInfo3.ColumnIndexEnd = 10;      //终止列为第六列
        colInfo3.Width = 6 * 2048;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo3);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

        ColumnInfo colInfo4 = new ColumnInfo(doc, sheet);//生成列格式对象
        colInfo4.ColumnIndexStart = 11;
        colInfo4.ColumnIndexEnd = 11;
        colInfo4.Width = 3 * 1024;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo4);

        for (int row = 0; row < list.Count + 1; row++)
        {

            if (row == 0)
            {
                for (int col = 1; col <= colCount; col++)
                {

                    switch (col)
                    {
                        case 1: cellName = "月份"; break;
                        case 2: cellName = "付款"; break;
                        case 3: cellName = "单数"; break;
                        case 4: cellName = "菜品总价"; break;
                        case 5: cellName = "饮品总价"; break;
                        case 6: cellName = "送餐费"; break;
                    }
                    org.in2bits.MyXls.Cell cell = cells.Add(rowMin + row, colMin + col - 1, cellName);//列名

                    //cell.TopLineStyle = 2;
                    //cell.TopLineColor = Colors.Black;
                    //cell.BottomLineStyle = 2;
                    //cell.BottomLineColor = Colors.Black;
                    //if (col == 1)
                    //{
                    //    cell.LeftLineStyle = 2;
                    //    cell.LeftLineColor = Colors.Black;
                    //}
                    //cell.RightLineStyle = 2;
                    //cell.RightLineColor = Colors.Black;

                    //cell.Font.Weight = FontWeight.Bold;
                    //cell.Pattern = 1;
                    //cell.PatternColor = Colors.Silver;

                }
            }
            else
            {
                for (int col = 1; col <= colCount; col++)
                {
                    switch (col)
                    {
                        case 1: cellValue = year + "-" + list[row-1].CountKey.ToString(); break;
                        case 2: cellValue = list[row - 1].CountDecimalValue.ToString(); break;
                        case 3: cellValue = list[row - 1].CountIntValue.ToString(); break;
                        case 4: cellValue = (list[row - 1].CountDecimalValue - list[row - 1].CountDrinkPrice - list[row - 1].CountSendFee).ToString(); break;
                        case 5: cellValue = list[row - 1].CountDrinkPrice.ToString(); break;
                        case 6: cellValue = list[row - 1].CountSendFee.ToString(); break;
                    }

                    org.in2bits.MyXls.Cell cell = cells.Add(rowMin + row, colMin + col - 1, cellValue);
                }
            }

        }

        AlertScript.RegScript(this.Page, " $('#loading-mask').hide();");

        doc.Send();     //将文档发送到浏览器
        //Response.Flush();
        //Response.End();
    }
}

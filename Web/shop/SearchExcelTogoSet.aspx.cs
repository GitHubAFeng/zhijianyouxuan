using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using org.in2bits.MyXls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class shop_SearchExcelTogoSet : System.Web.UI.Page
{
    Custorder dal = new Custorder();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //由前一个页面的检索条件直接获取Excel
            if (!string.IsNullOrEmpty(WebUtility.FixgetCookie("SearchExcelTogoSetSqlWhere")))
            {
                SqlWhere = WebUtility.FixgetCookie("SearchExcelTogoSetSqlWhere");
            }
            else
            {
                SqlWhere = "1=1";
            }
            GetExcel();
        }
    }

    protected void GetExcel()
    {
        org.in2bits.MyXls.XlsDocument doc = new XlsDocument();

        //根据当前时间生成文件名 TODO;增加用户名

        doc.FileName = "" + WebUtility.GetTime() + "TogoSet.xls";
        doc.SummaryInformation.Author = "chilema"; //作者
        doc.SummaryInformation.Subject = "TogoSet";
        doc.DocumentSummaryInformation.Company = "chilema";

        doc.Workbook.ProtectContents = true;

        int RowCount = dal.GetCount(SqlWhere);

        IList<CustorderInfo> list = new List<CustorderInfo>();
        list = dal.GetListFix(RowCount, 1, SqlWhere, "OrderDateTime", 1);

        string sheetName = "TogoSet";

        Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
        Cells cells = sheet.Cells;

        int rowMin = 1;
        int rowCount = RowCount;
        int colMin = 1;
        int colCount = 8; //列
        string cellName = "";  //列名
        string cellValue = ""; //列的值

        //设置列的格式
        ColumnInfo colInfo = new ColumnInfo(doc, sheet);//生成列格式对象
        colInfo.ColumnIndexStart = 0;
        colInfo.ColumnIndexEnd = 7;
        colInfo.Width = 7 * 1024;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo);



        for (int row = 0; row < list.Count + 1; row++)
        {
            if (row == 0)
            {
                for (int col = 1; col <= colCount; col++)
                {

                    switch (col)
                    {
                        case 1: cellName = "订单编号"; break;
                        case 2: cellName = "下单时间"; break;
                        case 3: cellName = "金额"; break;
                        case 4: cellName = "配送费"; break;
                        case 5: cellName = "支付方式"; break;
                        case 6: cellName = "支付方式"; break;
                        case 7: cellName = "结算金额"; break;
                        case 8: cellName = "结算状态"; break;
                    }
                    org.in2bits.MyXls.Cell cell = cells.Add(rowMin + row, colMin + col - 1, cellName);//列名

                    cell.TopLineStyle = 2;
                    cell.TopLineColor = Colors.Black;
                    cell.BottomLineStyle = 2;
                    cell.BottomLineColor = Colors.Black;
                    if (col == 1)
                    {
                        cell.LeftLineStyle = 2;
                        cell.LeftLineColor = Colors.Black;
                    }
                    cell.RightLineStyle = 2;
                    cell.RightLineColor = Colors.Black;

                    cell.Font.Weight = FontWeight.Bold;
                    cell.Pattern = 1;
                    cell.PatternColor = Colors.Silver;

                }
            }
            else
            {
                for (int col = 1; col <= colCount; col++)
                {
                    switch (col)
                    {
                        case 1: cellValue = list[row - 1].orderid; break;
                        case 2: cellValue = list[row - 1].OrderDateTime.ToString(); break;
                        case 3: cellValue = list[row - 1].OrderSums.ToString(); break;
                        case 4: cellValue = list[row - 1].SendFee.ToString(); break;
                        case 5: cellValue = WebUtility.TurnPayModel(list[row - 1].paymode.ToString()); break;
                        case 6: cellValue = list[row - 1].paystate == 1 ? "已支付" : "未支付"; break;
                        case 7: cellValue = list[row - 1].shopdiscountmoney.ToString(); break;
                        case 8: cellValue = list[row - 1].deliversiteid.ToString() == "0" ? "未结" : "已结"; break;

                    }
                    org.in2bits.MyXls.Cell cell = cells.Add(rowMin + row, colMin + col - 1, cellValue);
                }
            }

        }

        doc.Send();     //将文档发送到浏览器
        //Response.Flush();
        Response.End();
    }
}
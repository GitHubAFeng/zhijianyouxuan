﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using org.in2bits.MyXls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class Admin_Gifts_SearchExcelGift : System.Web.UI.Page
{
    Integral dal = new Integral();
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
            if (HjNetHelper.GetQueryString("type") == "1")
            {
                //由前一个页面的检索条件直接获取Excel
                if (!string.IsNullOrEmpty(WebUtility.FixgetCookie("SearchGiftsSqlWhere")))
                {
                    SqlWhere = WebUtility.FixgetCookie("SearchGiftsSqlWhere");
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
        org.in2bits.MyXls.XlsDocument doc = new XlsDocument();

        //根据当前时间生成文件名 TODO;增加用户名

        doc.FileName = "" + WebUtility.GetTime() + "UserRecord.xls";
        doc.SummaryInformation.Author = "chilema"; //作者
        doc.SummaryInformation.Subject = "User Record";
        doc.DocumentSummaryInformation.Company = "chilema";

        doc.Workbook.ProtectContents = true;

        int RowCount = dal.GetCount(SqlWhere);

        IList<IntegralInfo> list = new List<IntegralInfo>();
        list = dal.GetList(RowCount, 1, SqlWhere, "IntegralId", 1);

        string sheetName = "UserRecord";

        Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
        Cells cells = sheet.Cells;

        int rowMin = 1;
        int rowCount = RowCount;
        int colMin = 1;
        int colCount = 7;//13列
        string cellName = "";  //列名
        string cellValue = ""; //列的值

        //设置列的格式
        ColumnInfo colInfo = new ColumnInfo(doc, sheet);//生成列格式对象
        colInfo.ColumnIndexStart = 0;
        colInfo.ColumnIndexEnd = 5;
        colInfo.Width = 5 * 1024;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo);

        ColumnInfo colInfo2 = new ColumnInfo(doc, sheet);//生成列格式对象
        //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
        colInfo2.ColumnIndexStart = 6;    //起始列为第二列      //终止列为第六列
        colInfo2.Width = 4 * 2048;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo2);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

        ColumnInfo colInfo3 = new ColumnInfo(doc, sheet);//生成列格式对象
        //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
        colInfo3.ColumnIndexStart = 7;    //起始列为第二列
        colInfo3.ColumnIndexEnd = 10;      //终止列为第六列
        colInfo3.Width = 3 * 1024;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo3);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

        ColumnInfo colInfo4 = new ColumnInfo(doc, sheet);//生成列格式对象
        colInfo4.ColumnIndexStart = 11;
        colInfo4.ColumnIndexEnd = 11;
        colInfo4.Width = 4 * 2048;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo4);


        ColumnInfo colInfo5 = new ColumnInfo(doc, sheet);//生成列格式对象
        colInfo5.ColumnIndexStart = 11;
        colInfo5.ColumnIndexEnd = 12;
        colInfo5.Width = 6 * 1024;          //列的宽度计量单位为 1/256 字符宽
        sheet.AddColumnInfo(colInfo4);

        for (int row = 0; row < list.Count + 1; row++)
        {
            if (row == 0)
            {
                for (int col = 1; col <= colCount; col++)
                {

                    switch (col)
                    {
                        case 1: cellName = "兑换编号"; break;
                        case 2: cellName = "用户ID"; break;
                        case 3: cellName = "用户名"; break;
                        case 4: cellName = "礼品ID"; break;
                        case 5: cellName = "礼品名称"; break;
                        case 6: cellName = "所用积分"; break;
                        case 7: cellName = "兑换时间"; break;
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
                        case 1: cellValue = list[row - 1].IntegralId.ToString(); break;
                        case 2: cellValue = list[row - 1].CustId.ToString(); break;
                        case 3: cellValue = list[row - 1].UserName; break;
                        case 4: cellValue = list[row - 1].GiftsId.ToString(); break;
                        case 5: cellValue = list[row - 1].GiftName; break;
                        case 6: cellValue = list[row - 1].PayIntegral; break;
                        case 7: cellValue = list[row - 1].Cdate.ToString(); break;
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

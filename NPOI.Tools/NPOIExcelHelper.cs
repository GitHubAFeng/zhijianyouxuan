using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace MVCNPOIHelper.Common
{
    public static class NpoiExcelHelper
    {
        public enum ExcelType
        {
            Excel03 = 1,
            Excel07 = 2,

        }
        #region 公用方法


        public static ExcelType GetExcelVersion(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (extension != null && extension.ToLower() == ".xls")
                return ExcelType.Excel03;
            return ExcelType.Excel07;

        }

        public static MemoryStream RenderToExcelDt(DataTable table, ExcelColumns columns = null, ExcelType? excelType = null)
        {
            IWorkbook workbook;
            if (excelType == ExcelType.Excel03)
                workbook = new HSSFWorkbook();
            else
            {
                workbook = new XSSFWorkbook();
            }
            //Config is null,Initial 
            if (columns == null)
            {
                columns = new ExcelColumns();

                columns.Create(table);
            }
            else
            {
                var tableColumns = table.Columns;
                foreach (DataColumn tableColumn in tableColumns)
                {
                    foreach (var item in columns)
                    {
                        if (item.Key == tableColumn.ColumnName)
                        {
                            item.DType = tableColumn.DataType;
                        }

                    }
                }
            }
            var sheet = workbook.CreateSheet(columns.SheetName);
            columns.CreteTitleRow(sheet, workbook);
            int iCount = 0;
            int statrtRow = columns.DataRowStartIndex;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(statrtRow + i);
                var obj = table.Rows[i];
                foreach (var item in columns)
                {
                    if (obj.Table.Columns.Contains(item.Key))
                    {
                        var v = obj[item.Key];
                        if (v != null)
                        {
                            ICell cell = row.CreateCell(item.Index.Value);

                            if (columns.isStyle && item.DType != null)
                            {
                                try
                                {
                                    cell.CellStyle = item.Getcellstyle(workbook);
                                    cell.SetCellTypeValue(item.DType, v.ToString());
                                }
                                catch
                                {
                                    cell.SetCellValue(v.ToString());
                                }
                                ;

                            }
                            else
                            {
                                cell.SetCellValue(v.ToString());
                            }
                        }
                    }
                    //else
                    //{
                    //    ICell cell = row.CreateCell(item.Index.Value);
                    //    cell.SetCellValue("");
                    //    if (columns.isStyle)
                    //        cell.CellStyle = item.Getcellstyle(workbook);
                    //}

                }
            }
            var ms = new ExcelStream();
            ms.CanDispose = false;
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;

        }

        public static MemoryStream RenderToExcelIList(IList list, ExcelColumns columns = null, ExcelType? excelType = null)
        {
            var dt = DateTime.Now;
            IWorkbook workbook;
            if (excelType == ExcelType.Excel03)
                workbook = new HSSFWorkbook();
            else
            {
                workbook = new XSSFWorkbook();
            }
            //Config is null,Initial 
            if (columns == null)
            {
                columns = new ExcelColumns();

                columns.Create(list);
            }
            var sheet = workbook.CreateSheet(columns.SheetName);
            columns.CreteTitleRow(sheet, workbook);
            int iCount = 0;
            int statrtRow = columns.DataRowStartIndex;

            for (int i = 0; i < list.Count; i++)
            {
                int index = (int)i;
                IRow row = sheet.CreateRow(statrtRow + index);
                var obj = list[index];

                foreach (var item in columns)
                {
                    int value = item.Index.Value;

                    Type t;
                    object v = null;
                    if (item.Key.IndexOf(".") > 0)
                    {
                        string[] keys = item.Key.Split('.');
                        v =  obj.GetPropertyValue(keys[0], out t).GetPropertyValue(keys[1], out t);
                    }
                    else
                    {
                         v = obj.GetPropertyValue(item.Key, out t);
                    }
                   

                    if (v != null)
                    {
                        ICell cell = row.CreateCell(item.Index.Value);

                        // 
                        if (columns.isStyle && t != null)
                        {
                            cell.CellStyle = item.Getcellstyle(workbook);
                            cell.SetCellTypeValue(t, v.ToString());
                        }
                        else
                        {
                            cell.SetCellValue(v.ToString());
                        }
                    }
                    else
                    {
                        ICell cell = row.CreateCell(item.Index.Value);
                        cell.SetCellValue("");
                        if (columns.isStyle)
                            cell.CellStyle = item.Getcellstyle(workbook);
                    }

                }



            }

            var ms = new ExcelStream();
            ms.CanDispose = false;
            workbook.Write(ms);
            ms.Position = 0;
            ms.Flush();
            return ms;

        }
        public static void RenderToExcelIListSaveDisk(IList list, ExcelColumns columns = null, ExcelType? excelType = null,string url="")
        {
            var dt = DateTime.Now;
            IWorkbook workbook;
            if (excelType == ExcelType.Excel03)
                workbook = new HSSFWorkbook();
            else
            {
                workbook = new XSSFWorkbook();
            }
            //Config is null,Initial 
            if (columns == null)
            {
                columns = new ExcelColumns();

                columns.Create(list);
            }
            var sheet = workbook.CreateSheet(columns.SheetName);
            columns.CreteTitleRow(sheet, workbook);
            int iCount = 0;
            int statrtRow = columns.DataRowStartIndex;

            for (int i = 0; i < list.Count; i++)
            {
                int index = (int)i;
                IRow row = sheet.CreateRow(statrtRow + index);
                var obj = list[index];

                foreach (var item in columns)
                {
                    int value = item.Index.Value;

                    Type t;
                    var v = obj.GetPropertyValue(item.Key, out t);

                    if (v != null)
                    {
                        ICell cell = row.CreateCell(item.Index.Value);

                        // 
                        if (columns.isStyle && t != null)
                        {
                            cell.CellStyle = item.Getcellstyle(workbook);
                            cell.SetCellTypeValue(t, v.ToString());
                        }
                        else
                        {
                            cell.SetCellValue(v.ToString());
                        }
                    }
                    else
                    {
                        ICell cell = row.CreateCell(item.Index.Value);
                        cell.SetCellValue("");
                        if (columns.isStyle)
                            cell.CellStyle = item.Getcellstyle(workbook);
                    }

                }



            }
            //写Excel
            FileStream file = new FileStream(url, FileMode.OpenOrCreate);
            workbook.Write(file);
            file.Flush();
            file.Close();
        }
        private static void SetCellTypeValue(this ICell cell, Type t, string drValue)
        {
            switch (t.ToString())
            {
                case "System.String": //字符串类型
                    cell.SetCellValue(drValue);
                    break;
                case "System.DateTime": //日期类型
                    DateTime dateV;
                    DateTime.TryParse(drValue, out dateV);
                    cell.SetCellValue(dateV);


                    break;
                case "System.Boolean": //布尔型
                    bool boolV = false;
                    bool.TryParse(drValue, out boolV);
                    cell.SetCellValue(boolV);
                    break;
                case "System.Int16": //整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV = 0;
                    int.TryParse(drValue, out intV);
                    cell.SetCellValue(intV);
                    break;
                case "System.Decimal": //浮点型
                case "System.Double":
                    double doubV = 0;
                    double.TryParse(drValue, out doubV);
                    cell.SetCellValue(doubV);
                    break;
                case "System.DBNull": //空值处理
                    cell.SetCellValue("");
                    break;
                default:
                    cell.SetCellValue("");
                    break;
            }

        }
        public static MemoryStream RenderToExcel<T>(List<T> list, ExcelType type,
            Dictionary<string, string> map = null)
        {
            IWorkbook workbook;
            if (ExcelType.Excel03 == type)
            {

                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = new XSSFWorkbook();
            }

            var sheet = workbook.CreateSheet();
            workbook.SetSheetName(0, "sheet1");

            //    wb.setSheetName(0, sheetName, HSSFWorkbook..ENCODING_UTF_16);// 设置sheet中文编码；
            var dicColumn = GetColumnIndexDictionary<T>(map);
            CreateTitleRow<T>(sheet, map);
            for (int i = 0; i < list.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);
                InsertToSheet(row, dicColumn, list[i]);
            }
            var ms = new ExcelStream();
            ms.CanDispose = false;
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        public static MemoryStream RenderToExcel(DataTable table, ExcelType type, Dictionary<string, string> map = null)
        {
            var ms = new ExcelStream();
            ms.CanDispose = false;
            using (table)
            {
                IWorkbook workbook;
                if (ExcelType.Excel03 == type)
                {

                    workbook = new HSSFWorkbook();
                }
                else
                {
                    workbook = new XSSFWorkbook();
                }
                var columnInts = new Dictionary<int, string>();
                var sheet = workbook.CreateSheet();

                var headerRow = sheet.CreateRow(0);

                // handling header.
                if (map == null)
                {

                    foreach (DataColumn column in table.Columns)
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                    //If Caption not set, returns the ColumnName value
                }
                else
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        if (map.ContainsKey(column.Caption))
                        {
                            columnInts.Add(column.Ordinal, column.Caption);
                            headerRow.CreateCell(column.Ordinal).SetCellValue(map[column.Caption]);
                        }

                    }
                }

                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    var dataRow = sheet.CreateRow(rowIndex);
                    if (map == null)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        }
                    }
                    else
                    {
                        int ci = 0;
                        foreach (var kv in columnInts)
                        {
                            dataRow.CreateCell(ci).SetCellValue(row[kv.Key].ToString());
                            ci++;
                        }
                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                //   ms.Position = 0;
            }


            return ms;
        }

        //wcm
        public static MemoryStream RenderToExcel(DataTable table, string headerText, ExcelType type, Dictionary<string, string> map = null)
        {
            if (map == null) return new MemoryStream();

            var ms = new ExcelStream();
            ms.CanDispose = false;
            using (table)
            {
                IWorkbook workbook;
                if (ExcelType.Excel03 == type)
                {

                    workbook = new HSSFWorkbook();
                }
                else
                {
                    workbook = new XSSFWorkbook();
                }
                var columnInts = new Dictionary<int, string>();
                var sheet = workbook.CreateSheet();

                #region 样式
                ICellStyle dateStyle = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                int[] arrColWidth = new int[map.Count];
                string[] keys = map.Keys.ToArray();

                //取得列宽
                for (int i = 0; i < keys.Length; i++)
                {
                    arrColWidth[i] = Encoding.GetEncoding(936).GetBytes(map[keys[i]]).Length;
                }
                #endregion

                int rowIndex = 0;
                if (!string.IsNullOrEmpty(headerText))
                {
                    #region 表头及样式
                    {
                        IRow headerRow0 = sheet.CreateRow(rowIndex);

                        headerRow0.HeightInPoints = 25;
                        headerRow0.CreateCell(0).SetCellValue(headerText);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow0.GetCell(0).CellStyle = headStyle;
                        //sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
                    }
                    #endregion
                    rowIndex++;
                }

                #region 列头及样式
                {
                    IRow headerRow1 = sheet.CreateRow(rowIndex);

                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = HorizontalAlignment.Center;
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        headerRow1.CreateCell(i).SetCellValue(map[keys[i]]);
                        headerRow1.GetCell(i).CellStyle = headStyle;
                        //设置列宽
                        sheet.SetColumnWidth(i, (arrColWidth[i] + 1) * 256);
                    }
                    rowIndex++;
                }
                #endregion

                //foreach (DataColumn column in table.Columns)
                //{
                //    if (map.ContainsKey(column.Caption))
                //    {
                //        columnInts.Add(column.Ordinal, column.Caption);
                //    }
                //}
                // handling value.
                foreach (DataRow row in table.Rows)
                {
                    var dataRow = sheet.CreateRow(rowIndex);
                    if (map == null)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        }
                    }
                    else
                    {
                        //int ci = 0;
                        //foreach (var kv in columnInts)
                        //{
                        //    dataRow.CreateCell(ci).SetCellValue(row[kv.Key].ToString());
                        //    ci++;
                        //}
                        foreach (DataColumn column in table.Columns)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        }
                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                //   ms.Position = 0;
            }

            return ms;
        }

        //wcm
        public static void ExportByWeb(DataTable table, string fileName, string headerText, Dictionary<string, string> map)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式   
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";

            MemoryStream ms = RenderToExcel(table, headerText, ExcelType.Excel07, map);

            curContext.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(fileName, Encoding.UTF8)));
            curContext.Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
            curContext.Response.BinaryWrite(ms.ToArray());
            ms.Close();
            ms.Dispose();
            curContext.Response.End();
        }

        public static DataTable RenderFromExcel(Stream stream, ExcelColumns columns = null)
        {
            if (columns == null)
                return RenderFromExcel(stream, ExcelType.Excel07);
            var maps = columns.ToDictionary(excelColumn => excelColumn.Key, excelColumn => excelColumn.Name);
            return RenderFromExcel(stream, ExcelType.Excel07, maps);

        }

        public static List<T> RenderToList<T>(Stream stream, ExcelColumns columns = null)
        {
            if (columns == null)
                return RenderToList<T>(stream, ExcelType.Excel07);
            var maps = columns.ToDictionary(excelColumn => excelColumn.Key, excelColumn => excelColumn.Name);
            return RenderToList<T>(stream, ExcelType.Excel07, maps);

        }

        public static DataTable RenderFromExcel(Stream excelFileStream, ExcelType type, Dictionary<string, string> map = null)
        {
            using (excelFileStream)
            {
                IWorkbook workbook;
                if (ExcelType.Excel03 == type)
                {

                    workbook = new HSSFWorkbook(excelFileStream);
                }
                else
                {
                    workbook = new XSSFWorkbook(excelFileStream);
                }


                var sheet = workbook.GetSheetAt(0);//取第一个表
                var columInts = new Dictionary<int, string>();
                DataTable table = new DataTable();

                var headerRow = sheet.GetRow(0);//第一行为标题行
                int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
                int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

                if (map == null)
                {
                    //handling header.
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null) continue;
                        DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                        table.Columns.Add(column);
                    }
                }
                else
                {

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null) continue;

                        if (map.ContainsKey(headerRow.GetCell(i).ToString()))
                        {
                            columInts.Add(i, map[headerRow.GetCell(i).ToString()]);
                            DataColumn column = new DataColumn(map[headerRow.GetCell(i).ToString()]);
                            table.Columns.Add(column);

                        }
                    }
                }

                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    var row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    if (row != null)
                    {
                        if (map == null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                    dataRow[j] = row.GetCell(j).ToString(); // GetCellValue(row.GetCell(j));
                            }
                        }
                        else
                        {
                            int ci = 0;
                            foreach (var columInt in columInts)
                            {
                                if (row.GetCell(columInt.Key) != null) continue;

                                dataRow[ci] = row.GetCell(columInt.Key).ToString(); // GetCellValue(row.GetCell(j));

                                ci++;

                            }
                        }
                    }

                    table.Rows.Add(dataRow);
                }
                return table;



            }
        }

        public static List<T> RenderToList<T>(Stream stream, ExcelType type, Dictionary<string, string> map = null)
        {

            var list = new List<T>();
            using (stream)
            {
                IWorkbook workbook;
                if (ExcelType.Excel03 == type)
                {

                    workbook = new HSSFWorkbook(stream);
                }
                else
                {
                    workbook = new XSSFWorkbook(stream);
                }
                //var workbook = new HSSFWorkbook(stream); //从流内容创建Workbook对象
                var sheet = workbook.GetSheetAt(0); //获取第一个工作表
                var row = sheet.GetRow(0); //获取标题行
                var nameValuePair = FirstRowTitle(row, map);
                for (var i = 1; i <= sheet.LastRowNum; i++)
                {
                    var dataRow = sheet.GetRow(i);
                    if (dataRow != null)
                    {
                        var t = InsertToList<T>(dataRow, nameValuePair);
                        list.Add(t);
                    }
                }

                stream.Dispose();
                return list;
            }

        }


        public static List<T> RenderToListValidate<T>(Stream stream, ExcelColumns excelColumns = null)
        {
            if (excelColumns == null)
                return RenderToList<T>(stream, ExcelType.Excel07);
            var list = new List<T>();
            using (stream)
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook(stream);

                //var workbook = new HSSFWorkbook(stream); //从流内容创建Workbook对象
                var sheet = workbook.GetSheetAt(0); //获取第一个工作表
                var row = sheet.GetRow(0); //获取标题行
                int bj = 0;//导入索引排序
                foreach (var cell in row.Cells)
                {
                    if (excelColumns.ContainsKey(cell.ToString()))
                        excelColumns[cell.ToString()].Index = bj;
                    bj++;
                }
                //查找配置列
                var ex1 = excelColumns.FirstOrDefault(m => m.Required && !m.Index.HasValue);

                if (ex1 != null)
                    throw new Exception("未找到配置的列，配置列名为：" + ex1.Key);
                for (var i = 1; i <= sheet.LastRowNum; i++)
                {
                    var dataRow = sheet.GetRow(i);
                    if (dataRow != null)
                    {
                        T t = (T)Activator.CreateInstance(typeof(T));

                        var t1 = typeof(T);
                        for (int j = 0; j < dataRow.LastCellNum; j++)
                        {
                            var datacell = dataRow.GetCell(j);
                            object value = null;
                            if (datacell != null && datacell.CellType == CellType.Numeric)
                            {
                                //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                                if (HSSFDateUtil.IsCellDateFormatted(datacell)) //日期类型
                                {
                                    value = datacell.DateCellValue;
                                }
                                else //其他数字类型
                                {
                                    value = datacell.NumericCellValue;
                                }
                            }
                            else
                            {
                                value = datacell == null ? "" : datacell.ToString() as object;
                            }
                            var excelColumn = excelColumns.Get(j);
                            try
                            {
                                if (excelColumn != null)
                                {
                                    if (!excelColumns.IgnoreError)
                                    {
                                        if (excelColumn.Required)
                                        {
                                            if (string.IsNullOrEmpty(value.ToString().Trim()))
                                            {
                                                throw new Exception("数据列必填！");
                                            }
                                        }
                                        //公式计算
                                        if (excelColumn.CellFormula == 1)
                                        {
                                            ICell cell = dataRow.GetCell(j);
                                            if (cell.CellType == CellType.Formula && !string.IsNullOrEmpty(cell.CellFormula))
                                                value = cell.NumericCellValue;
                                        }
                                    }
                                    t.SetPropertyValueChangeType(excelColumn.Name,
                                        string.IsNullOrEmpty(value.ToString()) ? null : value);
                                }
                            }
                            catch (Exception ex)
                            {

                                throw new Exception("Excel第【" + (i + 1) + "】行数据中，列【" + excelColumn.Key + "】读取失败，失败原因：" + ex.Message);
                            }
                        }
                        list.Add(t);
                    }
                }

                stream.Dispose();
                return list;
            }
            return list;
        }


        #endregion




        #region 私有方法
        /// <summary>
        /// 添加到Excel内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="dicColumn"></param>
        /// <param name="t"></param>
        private static void InsertToSheet<T>(IRow row, Dictionary<int, string> dicColumn, T t)
        {
            int i = 0;
            foreach (var kv in dicColumn)
            {
                var o = t.GetPropertyValue(kv.Value);
                string v;
                if (o == null)
                    v = "";
                else
                {
                    v = o.ToString();
                }
                row.CreateCell(i).SetCellValue(v);
                i++;
            }
        }
        /// <summary>
        /// 添加得到列匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameValue"></param>
        /// <returns></returns>
        private static Dictionary<int, string> GetColumnIndexDictionary<T>(Dictionary<string, string> nameValue = null)
        {
            var colmunsDic = new Dictionary<int, string>();
            if (nameValue != null)
            {
                int i = 0;
                foreach (var kv in nameValue)
                {
                    colmunsDic.Add(i, kv.Key);
                    i++;
                }
            }
            else
            {
                var obj = typeof(T);
                PropertyInfo[] props = obj.GetProperties();
                int i = 0;
                foreach (PropertyInfo prop in props)
                {
                    colmunsDic.Add(i, prop.Name);
                    i++;
                }
            }
            return colmunsDic;
        }
        /// <summary>
        /// 填入Excel列表头
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="nameValue"></param>
        private static void CreateTitleRow<T>(ISheet sheet, Dictionary<string, string> nameValue = null)
        {
            var headRow = sheet.CreateRow(0);

            if (nameValue != null)
            {
                int i = 0;
                foreach (var kv in nameValue)
                {
                    headRow.CreateCell(i).SetCellValue(kv.Value);
                    i++;
                }
            }
            else
            {
                var obj = typeof(T);
                PropertyInfo[] props = obj.GetProperties();
                int i = 0;
                foreach (PropertyInfo prop in props)
                {
                    headRow.CreateCell(i).SetCellValue(prop.Name);
                    i++;
                }
            }
        }



        /// <summary>
        ///读取Excel 添加到List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="mapColumn"></param>
        /// <returns></returns>
        private static T InsertToList<T>(IRow row, Dictionary<int, string> mapColumn)
        {
            T t = (T)Activator.CreateInstance(typeof(T));

            var t1 = typeof(T);
            for (int i = 0; i < row.LastCellNum; i++)
            {
                var value = row.GetCell(i) == null ? "" : row.GetCell(i).ToString() as object;
                t.SetPropertyValueChangeType(mapColumn[i], value);
            }
            return t;
        }
        /// <summary>
        /// 读取第一行匹配
        /// </summary>
        /// <param name="titleRow"></param>
        /// <param name="nameValuePair"></param>
        /// <returns></returns>
        private static Dictionary<int, string> FirstRowTitle(IRow titleRow,
            Dictionary<string, string> nameValuePair = null)
        {
            var columnIndexValue = new Dictionary<int, string>();
            for (int i = 0; i < titleRow.LastCellNum; i++)
            {
                var value = titleRow.GetCell(i) == null ? "" : titleRow.GetCell(i).ToString().Trim();

                if (nameValuePair != null && nameValuePair.ContainsKey(value))
                {
                    columnIndexValue.Add(i, nameValuePair[value]);
                }
                else
                {
                    columnIndexValue.Add(i, value);
                }

            }
            return columnIndexValue;
        }



        #endregion
    }
    #region NopiExcel帮助类
    public class ExcelColumn
    {
        public Type DType { get; set; }
        /// <summary>
        /// 是否可空 导入时使用
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// 数据中名称（Excel列名称或者对象属性名称）
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 替换名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 列宽
        /// </summary>
        public int? Width { get; set; }
        /// <summary>
        /// 对应生成列下标
        /// </summary>
        public int? Index { get; set; }
        /// <summary>
        /// 设置公式
        /// </summary>
        public int CellFormula { get; set; }
        public override string ToString()
        {
            return this.Key;
        }
        /// <summary>
        /// 单元格对其方式，默认左对齐
        /// </summary>
        public string Alignment { get; set; }
        /// <summary>
        /// 取得NPOI对齐方式
        /// </summary>
        public HorizontalAlignment HAlignment
        {
            get
            {
                switch (Alignment)
                {
                    case "Center":
                        return HorizontalAlignment.Center;
                    case "General":
                        return HorizontalAlignment.General;
                    case "Left":
                        return HorizontalAlignment.Left;
                    case "Right":
                        return HorizontalAlignment.Right;
                    case "Fill":
                        return HorizontalAlignment.Fill;
                    case "Justify":
                        return HorizontalAlignment.Justify;
                    case "CenterSelection":
                        return HorizontalAlignment.CenterSelection;
                    case "Distributed":
                        return HorizontalAlignment.Distributed;
                }
                return HorizontalAlignment.Center;
            }


        }
        /// <summary>
        /// Excel Cell 格式设置
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 字体名称默认宋体
        /// </summary>
        public string FontName { get; set; }
        /// <summary>
        /// 粗体设置
        /// </summary>
        public short? BoldWeight { get; set; }


        public ICellStyle CellStyleStatic { get; set; }

        #region 定义单元格常用到样式
        public ICellStyle Getcellstyle(IWorkbook wb)
        {
            if (CellStyleStatic != null)
                return CellStyleStatic;
            ICellStyle cellStyle = wb.CreateCellStyle();

            CellStyleStatic = cellStyle;
            //定义几种字体  
            //也可以一种字体，写一些公共属性，然后在下面需要时加特殊的  
            IFont font = wb.CreateFont();
            font.FontHeightInPoints = 10;
            font.FontName = string.IsNullOrEmpty(FontName) ? "宋体" : FontName;
            font.Boldweight = BoldWeight.HasValue ? BoldWeight.Value : (short)1;

            if (!string.IsNullOrEmpty(Format))
            {

                //cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat(Format);
                //IDataFormat datastyle = wb.CreateDataFormat();

                //cellStyle.DataFormat = datastyle.GetFormat(Format);
                IDataFormat format = wb.CreateDataFormat();
                cellStyle.DataFormat = format.GetFormat(Format);
            }

            //边框  
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;

            //水平对齐  
            cellStyle.Alignment = HAlignment;

            //垂直对齐  
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            //自动换行  
            cellStyle.WrapText = false;

            ////缩进;当设置为1时，前面留的空白太大了。希旺官网改进。或者是我设置的不对  
            //cellStyle.Indention = 0;

            ////上面基本都是设共公的设置  

            cellStyle.SetFont(font);
            return cellStyle;


        }
        #endregion

    }

    /// <summary>
    ///防止 NPOI07 自动释放MemoryStram
    /// </summary>
    public class ExcelStream : System.IO.MemoryStream
    {
        protected override void Dispose(bool disposing)
        {
            if (CanDispose)
            {
                base.Dispose(disposing);
            }
        }

        public bool CanDispose { get; set; }
    }

    public enum HAlignment
    {
        General = 0,
        Left = 1,
        Center = 2,
        Right = 3,
        Fill = 4,
        Justify = 5,
        CenterSelection = 6,
        Distributed = 7,
    }

    public class ExcelColumns : ICollection<ExcelColumn>
    {

        public bool isStyle = false;
        public bool IgnoreError = true;
        public short? Height { get; set; } //行高
        public string Title = string.Empty;//是否有标题
        public string SheetName = "Sheet1";
        private Dictionary<string, ExcelColumn> _list = new Dictionary<string, ExcelColumn>();
        public int DataRowStartIndex { get; private set; }

        public Dictionary<string, ExcelColumn>.KeyCollection Keys
        {
            get { return _list.Keys; }
        }
        public bool ContainsKey(string key)
        {
            return _list.ContainsKey(key);
        }

        public ExcelColumn this[string key]
        {
            get { return _list[key]; }
        }

        /// <summary>
        /// 取得标题行
        /// </summary>
        /// <param name="row"></param>
        public void CreteTitleRow(ISheet sheet, IWorkbook workbook)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                IRow titlerow = sheet.CreateRow(0);


                ICell cell = titlerow.CreateCell(0);
                cell.SetCellValue(Title);
                ICellStyle style = workbook.CreateCellStyle();
                //设置单元格的样式：水平对齐居中
                style.Alignment = HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
                style.BorderBottom = BorderStyle.Thin;
                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;
                style.BorderTop = BorderStyle.Thin;

                //新建一个字体样式对象
                IFont font = workbook.CreateFont();
                //设置字体加粗样式
                font.Boldweight = short.MaxValue;
                font.FontHeightInPoints = 25;
                //使用SetFont方法将字体样式添加到单元格样式中 
                style.SetFont(font);

                //将新的样式赋给单元格
                cell.CellStyle = style;
                sheet.AddMergedRegion(new CellRangeAddress(DataRowStartIndex, DataRowStartIndex, 0, Count - 1));
                DataRowStartIndex = 1;
            }

            IRow row = sheet.CreateRow(DataRowStartIndex);
            int column = 0;
            foreach (var excelColumn in _list)
            {

                excelColumn.Value.Index = column;

                ICell cell = row.CreateCell(excelColumn.Value.Index.Value);
                cell.SetCellValue(excelColumn.Value.Name);
                if (isStyle)
                {
                    if (Height.HasValue)
                        row.Height = (short)(Height.Value * (short)20);
                    ICellStyle style = workbook.CreateCellStyle();
                    //设置单元格的样式：水平对齐居中
                    style.Alignment = HorizontalAlignment.Center;
                    style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                    //新建一个字体样式对象
                    IFont font = workbook.CreateFont();
                    //设置字体加粗样式
                    font.Boldweight = short.MaxValue;
                    //使用SetFont方法将字体样式添加到单元格样式中 
                    style.SetFont(font);
                    //将新的样式赋给单元格
                    cell.CellStyle = style;

                    // sheet.AddMergedRegion(new CellRangeAddress(DataRowStartIndex, DataRowStartIndex, 0, Count));
                    if (excelColumn.Value.Width.HasValue)
                        sheet.SetColumnWidth(column, excelColumn.Value.Width.Value * 256);
                }
                column++;
            }
            DataRowStartIndex++;



        }

        public IEnumerator<ExcelColumn> GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ExcelColumn item)
        {
            if (!_list.ContainsKey(item.Key))
            {
                _list.Add(item.Key, item);

                return;
            }
            throw new Exception("集合中以及保护此元素");
        }

        public void Clear()
        {
            _list.Clear();
        }

        public int? Index(string key)
        {
            if (_list.ContainsKey(key))
            {
                return _list[key].Index;
            }

            return null;
        }

        public string Name(int index)
        {
            var firstOrDefault = _list.Values.FirstOrDefault(m => m.Index == index);
            if (firstOrDefault != null)
                return firstOrDefault.Name;
            return null;
        }

        public ExcelColumn Get(int index)
        {
            var firstOrDefault = _list.Values.FirstOrDefault(m => m.Index == index);
            if (firstOrDefault != null)
                return firstOrDefault;
            return null;
        }
        public bool ContainsIndex(int index)
        {
            return _list.Values.Any(m => m.Index == index);
        }
        public string Name(string key)
        {
            if (_list.ContainsKey(key))
            {
                return _list[key].Name;
            }

            return key;
        }
        public bool Contains(ExcelColumn item)
        {
            return _list.ContainsKey(item.Key);

        }

        public void CopyTo(ExcelColumn[] array, int arrayIndex)
        {
            throw new Exception("Not found");
        }

        public bool Remove(ExcelColumn item)
        {
            return _list.Remove(item.Key);
        }

        public int Count { get { return _list.Count; } }

        // public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }


        #region

        public void Create(DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                Add(new ExcelColumn() { Key = column.Caption, Name = column.Caption, DType = column.DataType });
            }
        }

        public void Create(IList list)
        {
            if (list != null)
            {

                Type type = null;
                if (list.GetType().GenericTypeArguments != null && list.GetType().GenericTypeArguments.Length > 0)
                {
                    type = list.GetType().GenericTypeArguments[0];
                    var paras = type.GetConstructors()[0].GetParameters();

                    foreach (var each in type.GetProperties())
                    {
                        Add(new ExcelColumn() { Name = each.Name, Key = each.Name });
                    }
                }
                if(this.Count==0)
                {
                    type = list[0].GetType();
                    var parameters = type.GetConstructors()[0].GetParameters();
                    var args = new object[parameters.Length];

                    // 对应构造函数的每个参数，取同名属性的值
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Add(new ExcelColumn() { Name = parameters[i].Name, Key = parameters[i].Name });

                    }

                    //var instance = Activator.CreateInstance(type, args);
                    //return instance;
                }
            }
        }

        public static ExcelColumns Create(string xmlPath, string selectnode = null)
        {
            var excelColumns = new ExcelColumns();
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);
            if (string.IsNullOrEmpty(selectnode))
                selectnode = "root/import/row";
            var node = xmlDocument.SelectSingleNode(selectnode);
            if (node.Attributes["isStyle"] != null)
            {
                excelColumns.isStyle = Convert.ToBoolean(node.Attributes["isStyle"].Value);
            }
            if (node.Attributes["ignoreerr"] != null)
            {
                excelColumns.IgnoreError = Convert.ToBoolean(node.Attributes["ignoreerr"].Value);
            }
            if (node.Attributes["height"] != null)
            {
                excelColumns.Height = short.Parse(node.Attributes["height"].Value);
            }
            if (node.Attributes["title"] != null)
            {
                excelColumns.Title = (node.Attributes["title"].Value);
            }
            if (node.Attributes["sheetname"] != null)
            {
                excelColumns.SheetName = (node.Attributes["sheetname"].Value);
            }
            foreach (XmlNode item in node.ChildNodes)
            {
                var excelColumn = new ExcelColumn();
                excelColumn.Key = item.Attributes["key"].Value;
                excelColumn.Name = item.Attributes["name"].Value;
                if (item.Attributes["width"] != null && !string.IsNullOrEmpty(item.Attributes["width"].Value))
                {
                    excelColumn.Width = int.Parse(item.Attributes["width"].Value);
                }
                if (item.Attributes["cellFormula"] != null && !string.IsNullOrEmpty(item.Attributes["cellFormula"].Value))
                {
                    excelColumn.CellFormula = int.Parse(item.Attributes["cellFormula"].Value);
                }
                if (item.Attributes["alignment"] != null && !string.IsNullOrEmpty(item.Attributes["alignment"].Value))
                {
                    excelColumn.Alignment = (item.Attributes["alignment"].Value);
                }
                if (item.Attributes["format"] != null && !string.IsNullOrEmpty(item.Attributes["format"].Value))
                {
                    excelColumn.Format = (item.Attributes["format"].Value);
                }
                if (item.Attributes["fontname"] != null && !string.IsNullOrEmpty(item.Attributes["fontname"].Value))
                {
                    excelColumn.FontName = (item.Attributes["fontname"].Value);
                }
                if (item.Attributes["boldWeight"] != null && !string.IsNullOrEmpty(item.Attributes["boldWeight"].Value))
                {
                    excelColumn.BoldWeight = short.Parse(item.Attributes["boldWeight"].Value);
                }
                if (item.Attributes["required"] != null && !string.IsNullOrEmpty(item.Attributes["required"].Value))
                {
                    excelColumn.Required = Convert.ToBoolean(item.Attributes["required"].Value);
                }
                excelColumns.Add(excelColumn);
            }

            return excelColumns;
        }

        #endregion
    }





    #endregion
}

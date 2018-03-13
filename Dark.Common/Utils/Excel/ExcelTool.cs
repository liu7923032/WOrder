using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dark.Common.Attributes;
using Dark.Common.Extension;
using Dark.Common.Utils.Excel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;


namespace Dark.Common.Utils
{
    /// <summary>
    /// 操作Excel的通用类库
    /// </summary>
    public class ExcelTool<T> where T : class, new()
    {
        //0.工作簿
        private IWorkbook workbook;
        //1.日期的样式
        private ICellStyle _dateStyle;

        private ICellStyle _defaultStyle;

        private IFont _fontColor = null;

        private PropertyInfo[] properties;

        private bool IsHasColor { get; set; }

        private Type TType { get; set; }

        /// <summary>
        /// 设置列的最大矿都
        /// </summary>
        public int MaxColumnLength { get; set; }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="dateFormate"></param>
        public ExcelTool(string dateFormate = "yyyy-mm-dd")
        {

            workbook = new XSSFWorkbook();

            TType = typeof(T);
            //1:设置属性
            properties = TType.GetProperties();

            //2:设置Col的默认最大长度
            MaxColumnLength = 50;

            //3:默认的字体样式
            _defaultStyle = workbook.CreateCellStyle();
            _fontColor = workbook.CreateFont();
            _fontColor.Color = HSSFColor.Black.Index;
            _fontColor.FontHeightInPoints = 11;

            //4:设置日期样式
            _dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            _dateStyle.DataFormat = format.GetFormat(dateFormate);
            _dateStyle.SetFont(_fontColor);
            //2:检查是否有
            if (typeof(IHasFontColor).IsAssignableFrom(TType))
            {
                IsHasColor = true;
            }
        }



        #region 1.0 私有的公共方法 
        /// <summary>
        /// 构建有ExcelDataAttribute特性的字典数据
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, ExcelDataAttribute> GetTitles()
        {
            Dictionary<string, ExcelDataAttribute> dictionary = new Dictionary<string, ExcelDataAttribute>();
            //List<string> titles = new List<string>();
            Type type = typeof(T);
            foreach (var item in properties)
            {
                var attributes = item.GetCustomAttributes(typeof(ExcelDataAttribute), true);
                if (attributes.Length > 0)
                {
                    dictionary[item.Name] = attributes[0] as ExcelDataAttribute;

                    dictionary[item.Name].Property = item;
                }
            }
            return dictionary;
        }


        /// <summary>
        /// 给Cell 赋值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="type"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ICell SetCellValue(ICell cell, Type type, object entity, ICellStyle cellStyle = null)
        {
            //1.创建cell
            var typeName = string.Empty;
            //2.得到属性的类型
            if (type.IsCanNull())
            {
                typeName = type.GetGenericArguments()[0].FullName;
            }
            else
            {
                typeName = type.FullName;
            }
            //3.设置默认的样式
            cell.CellStyle = cellStyle ?? _defaultStyle;
            if (cellStyle == null)
            {
                cell.CellStyle.SetFont(_fontColor);
            }
            switch (typeName)
            {
                case "System.String":
                    {
                        cell.SetCellValue(entity.ToString());
                        break;
                    }
                case "System.Int16":
                case "System.Int32":
                case "System.Double":
                case "System.Decimal":
                    if (entity == null)
                    {
                        cell.SetCellValue(0);
                    }
                    else
                    {
                        cell.SetCellValue(Convert.ToDouble(entity));
                    }
                    break;
                case "System.DateTime":
                    DateTime dateV;
                    DateTime.TryParse(entity.ToString(), out dateV);
                    cell.SetCellValue(dateV.Date);
                    cell.CellStyle = _dateStyle;
                    break;
                default:

                    break;
            }

            //默认的字体样式

            return cell;
        }

        /// <summary>
        /// 设置Excel列宽度
        /// </summary>
        private void AutoSizeWidth(ISheet sheet)
        {
            var arrayDicColumW = new Dictionary<int, int>();
            var ie = sheet.GetRowEnumerator();
            while (ie.MoveNext())
            {
                var row = (IRow)ie.Current;
                var rowcells = row.Cells;
                for (int i = 0; i < rowcells.Count; i++)
                {
                    int length = System.Text.Encoding.Default.GetBytes(rowcells[i].ToString()).Length;

                    if (arrayDicColumW.ContainsKey(i))
                    {
                        int temp = arrayDicColumW[i];
                        if (length > temp)
                            arrayDicColumW[i] = length;
                    }
                    else
                    {
                        arrayDicColumW[i] = length;
                    }
                }
            }

            foreach (var item in arrayDicColumW)
            {
                int num = item.Value;
                if (num > MaxColumnLength)
                {
                    num = MaxColumnLength;
                }
                sheet.SetColumnWidth(item.Key, (num + 1) * 256);
            }
        }


        /// <summary>
        /// 通过excel导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        private Stream GetStream(string filePath, ISheet sheet)
        {
            //1:自动设置行
            AutoSizeWidth(sheet);
            //3:检查是否有文件夹,不存在那么就创建
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fName = $"temp_{Guid.NewGuid().ToString()}.xlsx";
            var fileName = $"{filePath}{fName}";
            //3:创建文件
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            //4:将数据写入的文件流中
            using (FileStream memoryStream = File.Open(fileName, FileMode.OpenOrCreate))
            {
                workbook.Write(memoryStream);
                workbook.Close();
            }
            //读取文件
            return File.OpenRead(fileName);
        }


        #endregion


        #region 2.0 Excel 下载
        private ISheet FillData(IReadOnlyList<T> list, string sheetName = "sheet1")
        {
            //1:构建sheet 
            ISheet sheet = workbook.CreateSheet(sheetName);
            //2:创建标题
            var titleRow = sheet.CreateRow(0);
            var dictTitles = GetTitles();
            var titles = dictTitles.GetKeys();
            var titleStyle = GetTitleStyle();
            for (int i = 0; i < titles.Count; i++)
            {
                var cell = titleRow.CreateCell(i);
                SetCellValue(cell, typeof(string), dictTitles[titles[i]].Name, titleStyle);
            }

            //3:创建body体
            var rowLen = list.Count;
            for (int i = 1; i <= rowLen; i++)
            {
                //3.1 创建行
                var row = sheet.CreateRow(i);
                //3.2 实体
                T tEntity = list[i - 1];

                if (IsHasColor)
                {
                    IHasFontColor hasFontColor = list[i - 1] as IHasFontColor;
                    _fontColor.Color = Convert.ToInt16(hasFontColor.FontColor);
                }

                //3.2 创建列
                for (int j = 0; j < titles.Count; j++)
                {
                    string title = titles[j];
                    ExcelDataAttribute excelData = dictTitles[title];
                    var property = excelData.Property;
                    var cell = row.CreateCell(j);
                    SetCellValue(cell, property.PropertyType, property.GetValue(tEntity));
                }
            }

            return sheet;
        }



        /// <summary>
        /// 通过list集合来获取
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public Stream GetExcelByList(string filePath, IReadOnlyList<T> list, string sheetName = "sheet")
        {
            ISheet sheet = FillData(list, sheetName);
            return GetStream(filePath, sheet);
        }
        #endregion


        #region 3.0 读取Excel数据


        /// <summary>
        /// 获取标题样式
        /// </summary>
        /// <returns></returns>
        private ICellStyle GetTitleStyle()
        {
            ICellStyle titleStyle = workbook.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.Left;
            titleStyle.FillForegroundColor = HSSFColor.Orange.Index;
            IFont font = workbook.CreateFont();
            font.Color = HSSFColor.Orange.Index;
            font.FontHeightInPoints = 12;
            titleStyle.SetFont(font);
            return titleStyle;
        }



        /// <summary>
        /// 下载Excel模板
        /// </summary>
        /// <returns></returns>
        public Stream GetExcelTpl(string filePath)
        {
            //初始化表格
            workbook = new XSSFWorkbook();
            //
            ISheet sheet = workbook.CreateSheet("Excel模板");

            IRow titleRow = sheet.CreateRow(0);

            var propTitles = GetTitles();
            //设置标题的样式 背景色是黄色,字体是蓝色
            var titleStyle = GetTitleStyle();
            // 赋值
            for (int i = 0; i < propTitles.Count(); i++)
            {
                var excelData = propTitles.ElementAt(i).Value;
                var cell = titleRow.CreateCell(i);
                SetCellValue(cell, excelData.Property.PropertyType, excelData.Name, titleStyle);
            }
            // 另存,接着读取返回
            return GetStream(filePath, sheet);
        }
        /// <summary>
        /// 通过excel来读取数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<T> GetDataByExcel(string filePath)
        {
            List<T> dataList = new List<T>();
            //读取文件
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //1:获取workbook
                workbook = new XSSFWorkbook(fs);

                //2:获取sheet
                ISheet sheet = workbook.GetSheetAt(0);

                //3:找到第一行
                var firstRow = sheet.GetRow(0);
                //4:找到类型的所有值
                var props = GetTitles();
                Dictionary<int, ExcelDataAttribute> propertyDict = new Dictionary<int, ExcelDataAttribute>();
                //4:获取第一行的所有列
                var cells = firstRow.Cells;
                for (int i = 0; i < cells.Count; i++)
                {
                    string title = cells[0].StringCellValue.Trim();
                    //通过title来找对应的属性
                    var excelData = props.FirstOrDefault(u => u.Value.Name.Equals(title)).Value;
                    propertyDict.Add(i, excelData);
                }

                //5:循环读取数据
                var rowLen = sheet.LastRowNum;
                for (int i = 1; i < rowLen + 1; i++)
                {
                    IRow row = sheet.GetRow(i);

                    T obj = new T();
                    for (int j = 0; j < cells.Count; j++)
                    {
                        var colValue = row.GetCell(j, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                        var excelData = propertyDict[j];
                        excelData.Property.SetValue(obj, colValue);
                    }
                    dataList.Add(obj);
                }
            }
            return dataList;
        }

        #endregion

    }


}

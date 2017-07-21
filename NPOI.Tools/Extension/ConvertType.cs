using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace bizone.library.core.Extension
{
    public static class ConvertType
    {
        public static Int32 ToInt32(this object obj, Int32 default_v)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static Int32 ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);

        }

        public static Int16 ToInt16(this object obj, Int16 default_v)
        {
            try
            {
                return Convert.ToInt16(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static Int16 ToInt16(this object obj)
        {
            return Convert.ToInt16(obj);
        }

        public static Int64 ToInt64(this object obj, Int64 default_v)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static Int64 ToInt64(this object obj)
        {
            return Convert.ToInt64(obj);
        }

        public static Decimal ToDecimal(this object obj, Decimal default_v)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static Decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);

        }

        public static Double ToDouble(this object obj, Double default_v)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static Double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);

        }

        public static DateTime ToDateTime(this object obj, DateTime default_v)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }

        public static Boolean ToBoolean(this object obj, Boolean default_v)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static Boolean ToBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static String ToString(this object obj, String default_v)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch (Exception)
            {
                return default_v;
            }
        }

        public static string ConvertStrLst2String(this List<string> inputValue)
        {
            string reValue = string.Empty;
            StringBuilder sbValue = new StringBuilder();
            foreach (string item in inputValue)
                sbValue.Append(item + ",");
            reValue = sbValue.ToString();
            if (reValue.EndsWith(","))
                reValue = reValue.Substring(0, reValue.Length - 1);
            return reValue;
        }

        public static List<T> ConvetDataTable2List<T>(this DataTable dtValue) where T : class
        {
            // 定义集合  
            List<T> ts = new List<T>();
            // 获得此模型的类型  
            Type type = typeof(T);
            //定义一个临时变量  
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dtValue.Rows)
            {
                T t = default(T);
                // 获得此模型的公共属性  
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性  
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量  
                    //检查DataTable是否包含此列（列名==对象的属性名）    
                    if (dtValue.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter  
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出  
                        //取值  
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性  
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中  
                ts.Add(t);
            }
            return ts;
        }

        public static DataTable ConvertLst2DataTable<T>(this List<T> lstValue) where T : class
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in lstValue)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = properties[i].GetValue(item);
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}

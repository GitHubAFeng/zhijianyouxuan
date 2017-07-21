using System;
using System.Reflection;

namespace System
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 根据对象属性名称设置属性值
        /// </summary>
        /// <param name="propertyName">属性名称，区分大小写</param>
        /// <param name="value">值</param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
          //  if (value == null) return;
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (propertyName.Equals(prop.Name))
                {
                    prop.SetValue(obj, value, null);
                    break;
                }
            }
        }
        /// <summary>
        /// 根据对象名称设置对象属性值 如类型不对 则更改类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValueChangeType(this object obj, string propertyName, object value)
        {
            //  if (value == null) return;
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (propertyName.Equals(prop.Name))
                {
                    Type prType = prop.PropertyType;
                    var v = Convert.ChangeType(value, prType);
                    prop.SetValue(obj, v, null);
                    break;
                }
            }
        }
        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性名称，区分大小写</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (propertyName.Equals(prop.Name))
                {
                    return prop.GetValue(obj, null);
                }
            }
            return null;
        }


        public static object ChangePropertyValue(this object obj, object ReturnObject)
        {
            PropertyInfo[] Hprops = obj.GetType().GetProperties();
            //循环未赋值model
            foreach (PropertyInfo prop in Hprops)
            {
                ReturnObject.SetPropertyValue(prop.Name, obj.GetPropertyValue(prop.Name));
            }
            return ReturnObject;
        }

        public static object ChangePropertyValue1(this object obj, object ReturnObject)
        {
            PropertyInfo[] Hprops = obj.GetType().GetProperties();
            //循环未赋值model
            foreach (PropertyInfo prop in Hprops)
            {
                if (obj.GetPropertyValue(prop.Name) != null && obj.GetPropertyValue(prop.Name).ToString() != "0001-01-01 00:00:00" && obj.GetPropertyValue(prop.Name).ToString() != "" && obj.GetPropertyValue(prop.Name).ToString() != "0001/1/1 0:00:00")
                {
                    ReturnObject.SetPropertyValue(prop.Name, obj.GetPropertyValue(prop.Name));
                }
            }
            return ReturnObject;
        }

        private static Exception getException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return getException(ex.InnerException);
            }
            else
            {
                return ex;
            }
        }

        public static string GetMessage(this Exception obj)
        {
            return getException(obj).Message;
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName">属性名称，区分大小写</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName, out Type type)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (propertyName.Equals(prop.Name))
                {
                    type = prop.PropertyType;
                    return prop.GetValue(obj, null);

                }
            }
            type = null;
            return null;
        }


    }
}

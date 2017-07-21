#region license
/*****************************************
*CopyRight (c) 2009-2013 HangJing Teconology. All Rights Reserved.
*Function :
*Created by jijunjian at 2014/1/11 9:13:58.
*E-Mail: jijunjian@ihangjing.com
*****************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Hangjing.Common
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary> 
        /// 获得枚举类型数据项（不包括空项）
        /// </summary> 
        /// <param name="enumType">枚举类型</param> 
        /// <returns></returns> 
        public static IList<ListItem> GetItems(this Type enumType)
        {
            if (!enumType.IsEnum)
                throw new InvalidOperationException();

            IList<ListItem> list = new List<ListItem>();

            // 获取Description特性 
            Type typeDescription = typeof(DescriptionAttribute);
            // 获取枚举字段
            FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                    continue;

                // 获取枚举值
                int value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);

                // 不包括空项
                if (value >= 0)
                {
                    string text = string.Empty;
                    object[] array = field.GetCustomAttributes(typeDescription, false);

                    if (array.Length > 0) text = ((DescriptionAttribute)array[0]).Description;
                    else text = field.Name; //没有描述，直接取值

                    //添加到列表
                    list.Add(new ListItem(text, value.ToString()));
                }
            }
            return list;
        }

        /// <summary>
        /// 订单来源转到DropDownList
        /// </summary>
        /// <param name="ddl"></param>
        public static void OrderSourceToDropDownList(DropDownList ddlSubject)
        {
            ddlSubject.DataSource = typeof(OrderSource).GetItems();
            ddlSubject.DataTextField = "Text";
            ddlSubject.DataValueField = "Value";
            ddlSubject.DataBind();
        }

        /// <summary>
        /// 数据项
        /// </summary>
        public class ListItem
        {
            public ListItem(string text, string valeu)
            {
                this.Text = text;
                this.Value = valeu;
            }

            /// <summary>
            /// 文字
            /// </summary>
            public string Text
            {
                set;
                get;
            }

            /// <summary>
            /// 值
            /// </summary>
            public string Value
            {
                set;
                get;
            }
        }
    }
}

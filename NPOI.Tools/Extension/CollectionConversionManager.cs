using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bizone.library.core.Extension
{
    public class CollectionConversionManager
    {
        private static Type[] _types = new Type[]
		{
		    typeof (Char),
			typeof (Decimal),
			typeof (Boolean),
			typeof (Int16),
			typeof (Int32),
			typeof (Int64),
			typeof (UInt16),
			typeof (UInt32),
			typeof (UInt64),
			typeof (Byte),
			typeof (SByte),
			typeof (Single),
			typeof (Double),
			typeof (String),
            typeof(DateTime),
            typeof (Nullable<Char>),
			typeof (Nullable<Decimal>),
			typeof (Nullable<Boolean>),
			typeof (Nullable<Int16>),
			typeof (Nullable<Int32>),
			typeof (Nullable<Int64>),
			typeof (Nullable<UInt16>),
			typeof (Nullable<UInt32>),
			typeof (Nullable<UInt64>),
			typeof (Nullable<Byte>),
			typeof (Nullable<SByte>),
			typeof (Nullable<Single>),
			typeof (Nullable<Double>),
            typeof(Nullable<DateTime>)
		};
        private static bool CanConvertDirectly(Type t)
        {
            if (t.Equals(typeof(String)) ||
                t.Equals(typeof(DateTime)) || t.Equals(typeof(Nullable<DateTime>)) ||
                t.IsEnum ||
                _types.Contains(t))
                return true;
            return false;
        }
        private static object ChangeType(object value, Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(type);
                type = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, type);
        }

        public static object NameValueCollection2Entity(NameValueCollection nv, Type obj_type, String prefix)
        {
            if (nv == null || nv.Count == 0)
                return null;

            // create a intance for this object and get all property
            object instance = Activator.CreateInstance(obj_type);
            PropertyInfo[] pi = instance.GetType().GetProperties();

            // in first invoke, prefix is null
            String name = (prefix == null) ? "" : (prefix + ".");

            foreach (PropertyInfo p in pi)
            {
                try
                {
                    Type nullableType = p.PropertyType;
                    if (Nullable.GetUnderlyingType(p.PropertyType) != null)
                    {
                        nullableType = Nullable.GetUnderlyingType(p.PropertyType);
                    }

                    if (CanConvertDirectly(nullableType))
                    {
                        //must check exist this key , otherwise exception will be thrown
                        if (nv.AllKeys.Contains(name + p.Name))
                        {
                            p.SetValue(instance,
                                        ChangeType(nv[name + p.Name], p.PropertyType),
                                        null);
                        }
                    }
                    else if (p.PropertyType.IsArray)
                    {
                        //arrary
                    }
                    else if (p.PropertyType.IsClass)
                    {
                        //class
                        p.SetValue(instance,
                                    NameValueCollection2Entity(nv, p.PropertyType, name + p.Name),
                                    null);
                    }
                }
                catch (Exception) { }
            }
            return instance;
        }
    }
}

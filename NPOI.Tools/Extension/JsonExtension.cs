using System;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace bizone.library.core.Extension
{
    public static class JsonExtension
    {
        public static string ToJsonString(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static T JosnDeserialize<T>(this string input, T def)
        {
            if (string.IsNullOrEmpty(input))
                return def;
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                return jsSerializer.Deserialize<T>(input);
            }
            catch (InvalidOperationException)
            {
                return def;
            }
        }

        public static T JosnDeserialize<T>(this string input)
        {
            T def=default(T);
            if (string.IsNullOrEmpty(input))
                return def;
            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                return jsSerializer.Deserialize<T>(input);
            }
            catch (InvalidOperationException)
            {
                return def;
            }
        }  
    }



    public static class NameValueExtension
    {
        public static object NV2Type(this NameValueCollection nv, Type obj_type)
        {
            return CollectionConversionManager.NameValueCollection2Entity(nv, obj_type, null);
        }

    }
}

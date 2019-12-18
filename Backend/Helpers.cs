using System.Collections.Generic;
using System.Reflection;

namespace Backend
{
    public static class Helpers
    {
        public static T MapTo<T>(object obj) where T : new()
        {
            if (obj == null) return default;
            T res = new T();
            Dictionary<string, PropertyInfo> OutProps = new Dictionary<string, PropertyInfo>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.CanWrite)
                {
                    OutProps.Add(property.Name, property);
                }
            }
            foreach (var InProp in obj.GetType().GetProperties())
            {
                if (InProp.CanRead && OutProps.TryGetValue(InProp.Name, out PropertyInfo OutProp))
                {
                    if (OutProp.PropertyType == InProp.PropertyType)
                    {
                        OutProp.SetValue(res, InProp.GetValue(obj));
                    }
                }
            }
            return res;
        }
    }
}
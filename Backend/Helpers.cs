using Backend.DataContracts;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void UpdateObject<T>(T oldobj, T newobj)
        {
            if (oldobj == null || newobj == null) throw new ArgumentNullException();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                bool Ignore = (from attrib in propertyInfo.CustomAttributes
                               where attrib.AttributeType == typeof(IgnoreInHelpersAttribute)
                               select attrib).Any();
                if (!Ignore && propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(oldobj, propertyInfo.GetValue(newobj));
                }
            }
        }
        public static ItemResult ToItemResult(this Item item)
        {
            ItemResult res = MapTo<ItemResult>(item);
            res.Categories = item.Categories.Select(x => x.Category.Name).ToArray();
            return res;
        }
    }
}
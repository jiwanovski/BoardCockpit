using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BoardCockpit.Helpers
{
    public static class Extensions
    {
        //public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        //    where TEnum : struct, IComparable, IFormattable, IConvertible
        //{
        //    var values = from TEnum e in Enum.GetValues(typeof(TEnum))
        //                 select new { Id = e, Name = e.ToString() };
        //    return new SelectList(values, "Id", "Name", enumObj);
        //}

        public static SelectList ToSelectList<T>(this T enumeration)
        {
            var source = Enum.GetValues(typeof(T));
            var items = new Dictionary<object, string>();
            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in source)
            {
                FieldInfo field = value.GetType().GetField(value.ToString());
                DisplayAttribute attrs = (DisplayAttribute)field.
                    GetCustomAttributes(displayAttributeType, false).First();
                items.Add(value, attrs.GetName());
            }

            return new SelectList(items, "Key", "Value", enumeration);

        }
    }
}
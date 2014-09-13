using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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
        //public static IHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
        //{
        //    var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

        //    var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

        //    var enumValues = Enum.GetValues(enumType).Cast<object>();

        //    var items = from enumValue in enumValues
        //                select new SelectListItem
        //                {
        //                    Text = GetResourceValueForEnumValue(enumValue),
        //                    Value = ((int)enumValue).ToString(),
        //                    Selected = enumValue.Equals(metadata.Model)
        //                };

        //    return html.DropDownListFor(expression, items, string.Empty, null);
        //}

        //private static string GetResourceValueForEnumValue<TEnum>(TEnum enumValue)
        //{
        //    var key = string.Format("{0}_{1}", enumValue.GetType().Name, enumValue);

        //    return Enums.ResourceManager.GetString(key) ?? enumValue.ToString();
        //}
        public static IHtmlString DisplayEnumFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, int>> ex, Type enumType)
        {
            var value = (int)ModelMetadata.FromLambdaExpression(ex, html.ViewData).Model;
            string enumValue = Enum.GetName(enumType, value);
            return new HtmlString(html.Encode(enumValue));
        }

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
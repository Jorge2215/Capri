using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Pampa.InSol.Common.Utils
{
    public static class EnumUtils
    {
        public const string LOGNAME = "APPLog";
        public const string USER_KEY = "USER_KEY_InSol";
        public const string FUNCTION_ITEM_KEY = "FUNCTION_ITEM_KEY_InSol";

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", "description");
        }

        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T GetValueOf<T>(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value);
        }

        public static string GetName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static List<SelectListItem> GetListItems<T>()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Enum enumValue in Enum.GetValues(typeof(T)))
            {
                list.Add(new SelectListItem() { Text = EnumUtils.GetDescriptionFromEnumValue(enumValue), Value = enumValue.ToString() });
            }

            return list;
        }
    }
}
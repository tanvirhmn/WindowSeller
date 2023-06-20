using Radzen.Blazor;
using StockModule.UI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StockModule.UI.Helpers
{
    public static class EnumExtensions
    {
        /// <summary>
        /// By enum value, get the DisplayAttribute.Description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetDescription(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            if (field == null) return value.ToString();
            var attribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        /// <summary>
        /// By enum type to Dictionary
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns>return enum int and DisplayAttribute.Description</returns>
        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            return Enum.GetValues(enumType)
                .Cast<Enum>()
                .ToDictionary(t => (int)(object)t, t => t.GetDisplayDescription());
        }
    }
}

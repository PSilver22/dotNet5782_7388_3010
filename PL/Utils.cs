using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using BL;

namespace PL
{
    public class Utils
    {
        /// <summary>
        /// Creates ComboBoxItem's for a nullable enum field.
        /// </summary>
        /// <param name="type">the enum type</param>
        /// <param name="nullTitle">the title for the num option</param>
        /// <returns>An IEnumerable of ComboBoxItem's for each enum variant as well as for null</returns>
        public static IEnumerable<ComboBoxItem> NullableComboBoxItems(Type type, string nullTitle)
        {
            return Enum.GetValues(type).Cast<Enum>()
                .Select(s => new ComboBoxItem {Content = GetEnumDescription(s), Tag = s})
                .Prepend(new ComboBoxItem {Content = nullTitle, Tag = null});
        }

        public static string GetEnumDescription(Enum val)
        {
            var info = val.GetType().GetField(val.ToString())!;
            var attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attrs?.Any() ?? false ? attrs!.First().Description : val.ToString();
        }
    }
}